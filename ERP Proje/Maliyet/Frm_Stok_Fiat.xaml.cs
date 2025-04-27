using ExcelDataReader;
using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Methods;
using Layer_UI.Satis.Evrak;
using Layer_UI.UserControls;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Maliyet
{
    /// <summary>
    /// Interaction logic for Frm_Stok_Fiat.xaml
    /// </summary>
    public partial class Frm_Stok_Fiat : Window
    {
        ExcelMethodsEPP excel = new();
        Cls_Fiyat fiyat = new();
        ObservableCollection<Cls_Fiyat> excelCollection = new();
        Dictionary<string, string> filterDic = new();
        int totalResult = 0;
        double lastOffset = 0;
        int pageNumber = 1;
        Cls_Arge arge = new();
        public Frm_Stok_Fiat()
        {
            try
            {
                InitializeComponent();
                cbx_kod1.ItemsSource = arge.GetDistinctKod1();
                cbx_fiyat_grup.ItemsSource = fiyat.GetFiyatGrubu();
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
            }
        }
        private void btn_stok_rehberi_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Stok_Karti_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    txt_stok_kodu.Text = frm.SelectedStokKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Rehberi Açılırken");
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if(filterDic != null)
                    filterDic.Clear();
                else
                    filterDic = new Dictionary<string, string>();

                if(!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    filterDic.Add("STOKKODU",txt_stok_kodu.Text);
                if(cbx_fiyat_grup.SelectedItem != null)
                    filterDic.Add("FIYATGRUBU", cbx_fiyat_grup.SelectedItem.ToString());
                if(cbx_kod1.SelectedItem != null)
                    filterDic.Add("KOD1", cbx_kod1.SelectedItem.ToString());

                excelCollection = fiyat.GetFiyatList(filterDic,1);
                if (excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    dg_FiyatGrubu.Visibility = Visibility.Collapsed;
                    stc_kaydet.Visibility = Visibility.Collapsed;
                    txt_result.Visibility = Visibility.Collapsed;
                    return;
                }
                if (excelCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;

                    dg_FiyatGrubu.Visibility = Visibility.Collapsed;
                    stc_kaydet.Visibility = Visibility.Collapsed;
                    txt_result.Visibility = Visibility.Collapsed;
                    return;
                }
                totalResult = fiyat.CountFiyatList(filterDic, 1);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                dg_FiyatGrubu.ItemsSource = excelCollection;
                dg_FiyatGrubu.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;
                txt_result.Visibility = Visibility.Visible;
                lastOffset = 0;
                pageNumber = 1;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private async void btn_excel_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    DataTable data = await Task.Run(() => ReadExcelFile(filePath)); 
                    if(data == null)
                    {
                        CRUDmessages.GeneralFailureMessage("Excel Bilgileri Alınırken");
                        return;
                    }
                    if (data.Rows.Count == 0)
                    {
                        CRUDmessages.QueryIsEmpty();
                        return;
                    }
                    Variables.Result_ = CRUDmessages.InsertOnayMessage(string.Format("{0} Adet Kayıt Yapılacak. Onaylıyor Musunuz?", data.Rows.Count));
                    if (!Variables.Result_)
                        return;
                    txt_result.Visibility = Visibility.Visible;
                    txt_result.Text = "Kayıt İşlemi Devam Ediyor...";

                    Variables.Result_ = await fiyat.InsertBulkFiyatAsync(data);
                    if(Variables.Result_)
                    {
                        if (dg_FiyatGrubu.Items.Count == 0)
                        {
                            txt_result.Text = string.Empty;
                            txt_result.Visibility = Visibility.Collapsed;
                            cbx_fiyat_grup.ItemsSource = fiyat.GetFiyatGrubu();
                        }
                        else
                        {
                            cbx_fiyat_grup.ItemsSource = fiyat.GetFiyatGrubu();
                            if (totalResult > 100)
                                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                            else
                                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";
                        }
                        CRUDmessages.InsertSuccessMessage("Fiyat", data.Rows.Count);
                        return;
                    }
                    else
                    {
                        if (dg_FiyatGrubu.Items.Count == 0)
                            txt_result.Text = string.Empty;
                        else
                        {
                            if (totalResult > 100)
                                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                            else
                                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";
                        }
                        CRUDmessages.InsertFailureMessage("Fiyat");
                        return;
                    }

                }
                else { Mouse.OverrideCursor = null; return;
                }

                //excelCollection = new();


                //foreach (DataRow row in dataTable.Rows)
                //{
                //    Cls_Fiyat model = new();

                //    model.StokKodu = row["STOKKODU"] is DBNull ? string.Empty : row["STOKKODU"].ToString();
                //    model.Fiyat = row["FIYAT"] is DBNull ? 0 : Convert.ToSingle(row["FIYAT"]);
                //    model.DovizTipi = row["FIYATDOVIZTIPI"] is DBNull ? 0 : Convert.ToInt32(row["FIYATDOVIZTIPI"]);
                //    model.Tarih = row["TARİH"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(row["TARİH"]);
                //    string stokKodu = row["STOKKODU"] is DBNull ? string.Empty : row["STOKKODU"].ToString();
                //    string fiyatGrup = string.Empty;
                //    if (stokKodu.Substring(0, 1) != "M" &&
                //        stokKodu.Substring(0, 3) != "SSH")
                //    {
                //        model.AlisSatis = "A";
                //        fiyatGrup = "H";
                //    }
                //    else
                //    {
                //        model.AlisSatis = "S";
                //        fiyatGrup = "VITA-";
                //    }
                //    DateTime tarih = row["tarih"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(row["tarih"]);
                //    fiyatGrup += tarih.Year.ToString();
                //    if (fiyatGrup.Substring(0, 1) == "H")
                //        fiyatGrup += tarih.Month.ToString();
                //    else
                //        fiyatGrup += string.Format("-{0}", tarih.Month);
                //    model.FiyatGrubu = fiyatGrup;

                //    excelCollection.Add(model);
                //}


                //if (excelCollection == null)
                //{
                //    CRUDmessages.GeneralFailureMessage("Aktarım İşlemi Esnasında");
                //    Mouse.OverrideCursor = null;
                //    return;
                //}
                //if (excelCollection.Count == 0)
                //{
                //    CRUDmessages.QueryIsEmpty();
                //    Mouse.OverrideCursor = null;
                //    return;
                //}
                
                //dg_FiyatGrubu.ItemsSource = excelCollection;
                //dg_FiyatGrubu.Visibility = Visibility.Visible;
                //txt_result.Text = "Toplam " + dg_FiyatGrubu.Items.Count + " adet Stok Kartı listeleniyor.";
                //txt_result.Visibility = Visibility.Visible;
                //lastOffset = 0;
                //pageNumber = 1;

                //Mouse.OverrideCursor = null;
                //CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("format"))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Excelde Format Hatası Mevcut"); Mouse.OverrideCursor = null; return;
                }
                CRUDmessages.GeneralFailureMessage("Excel Listelenirken"); Mouse.OverrideCursor = null; return;
            }
        }
        private async void btn_fiyat_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_listele.IsEnabled = false;
                btn_guncelle.IsEnabled = false;
                btn_excel_getir.IsEnabled = false;
                btn_sile.IsEnabled = false;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                txt_result.Text = "Kayıt yapılıyor...";
                Variables.ErrorMessage_ = string.Empty;

                var hatayaDusenReceteler = excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi...");
                if (hatayaDusenReceteler.Any())
                {
                    foreach (Cls_Fiyat item in excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi..."))
                        item.KayitDurum = "Kaydedilmedi...";
                }

                //stok kodları 35 karakterden büyük 11 karakterden küçük olamaz
                if (excelCollection.Where(s => s.StokKodu.Length > 35).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.StokKodu.Length > 35).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Stok Kodu 35 Karakterden Büyük Olamaz...";
                    }
                }
                if (excelCollection.Where(s => s.StokKodu.Length < 11).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.StokKodu.Length < 11).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Stok Kodu 11 Karakterden Küçük Olamaz...";
                    }
                }

                //dovtip kontrol
                if (excelCollection.Where(d => d.DovizTipi != 0 && d.DovizTipi != 1 &&
                                            d.DovizTipi != 2 &&
                                            d.DovizTipi != 3 ).Any()) 
                {
                    var hataliStokKodlari = excelCollection.Where(d => d.DovizTipi != 0 && d.DovizTipi != 1 &&
                                                                    d.DovizTipi != 2 &&
                                                                    d.DovizTipi != 3).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Döviz Tipi 0,1,2 veya 3 olabilir...";
                    }
                }
                
                //fiyat grubu kontrol
                if (excelCollection.Where(f => f.FiyatGrubu == string.Empty ).Any()) 
                {
                    var hataliStokKodlari = excelCollection.Where(f => f.FiyatGrubu == string.Empty).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Fiyat Grubu Boş Olamaz...";
                    }
                }
                else if(excelCollection.Where(f => f.FiyatGrubu.Length<4).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(f => f.FiyatGrubu.Length < 4).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Fiyat Grubu 4 Karakterden Küçük Olamaz...";
                    }
                }
                else if (excelCollection.Where(f => f.FiyatGrubu.Substring(0,1).Contains("H") == false &&
                                                    f.FiyatGrubu.Substring(0,4).Contains("VITA")).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(f => f.FiyatGrubu.Substring(0, 1).Contains("H") == false &&
                                                                    f.FiyatGrubu.Substring(0, 4).Contains("VITA")).ToList();

                    foreach (Cls_Fiyat item in hataliStokKodlari)
                    {
                        item.KayitDurum = " Fiyat Grubu 'H' yada 'VITA' ile Başlamalı...";
                    }
                }


                if (excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi...").Any())
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Hata Veren Stok Kartları Mevcut.");
                    totalResult = fiyat.CountFiyatList(filterDic, 1);
                    if (totalResult > 100)
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                    else
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                    btn_listele.IsEnabled = true;
                    btn_guncelle.IsEnabled = true;
                    btn_excel_getir.IsEnabled = true;
                    btn_sile.IsEnabled = true;
                    dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    Mouse.OverrideCursor = null;

                    dg_FiyatGrubu.Items.Refresh();
                    return;
                }

                excelCollection = await fiyat.InsertUpdateFiyatAsync(excelCollection);
                if (excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");

                    btn_listele.IsEnabled = true;
                    btn_guncelle.IsEnabled = true;
                    btn_excel_getir.IsEnabled = true;
                    btn_sile.IsEnabled = true;
                    dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_FiyatGrubu.ItemsSource = excelCollection;

                dg_FiyatGrubu.Items.Refresh();

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                CRUDmessages.InsertSuccessMessage("Fiyat", excelCollection.Count);
                btn_listele.IsEnabled = true;
                btn_guncelle.IsEnabled = true;
                btn_excel_getir.IsEnabled = true;
                btn_sile.IsEnabled = true;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            catch
            {
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";
                btn_listele.IsEnabled = true;
                btn_guncelle.IsEnabled = true;
                btn_excel_getir.IsEnabled = true;
                btn_sile.IsEnabled = true;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                CRUDmessages.GeneralFailureMessage("Fiyatlar Kaydedilirken"); Mouse.OverrideCursor = null;
            }
        }
        private void btn_stok_karti_sil(object sender, RoutedEventArgs e)
        {

            try
            {

                Variables.ErrorMessage_ = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Stok Kartı Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Stok Kartı Bilgileri Alınırken"); return; }

                Cls_Fiyat dataItem = row.Item as Cls_Fiyat;

                excelCollection.Remove(dataItem);

                dg_FiyatGrubu.ItemsSource = excelCollection;

                dg_FiyatGrubu.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        bool selectMiktarColumn = false;
        bool isPageUp = false;
        private void dg_scroll_down(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_FiyatGrubu, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > excelCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Fiyat> moreFiyatCollection = new();
                    moreFiyatCollection = fiyat.GetFiyatList(filterDic, pageNumber);
                    if (moreFiyatCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Fiyat Bilgileri Eklenirken");
                        Mouse.OverrideCursor = null;
                        pageNumber--;
                        return;
                    }
                    if (moreFiyatCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Fiyat> lastFiyatCollection = new ObservableCollection<Cls_Fiyat>
                                        (excelCollection.Union(moreFiyatCollection).ToList());
                        dg_FiyatGrubu.ItemsSource = lastFiyatCollection;
                        dg_FiyatGrubu.Items.Refresh();
                        excelCollection = lastFiyatCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastFiyatCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset = center;
                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {

                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_FiyatGrubu, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > excelCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Fiyat> moreexcelCollectionection = new();
                    moreexcelCollectionection = fiyat.GetFiyatList(filterDic, pageNumber);
                    if (moreexcelCollectionection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stok Bilgileri Eklenirken");
                        Mouse.OverrideCursor = null;
                        pageNumber--;
                        return;
                    }
                    if (moreexcelCollectionection.Count > 0)
                    {
                        ObservableCollection<Cls_Fiyat> lastexcelCollectionection = new ObservableCollection<Cls_Fiyat>
                                        (excelCollection.Union(moreexcelCollectionection).ToList());
                        dg_FiyatGrubu.ItemsSource = lastexcelCollectionection;
                        dg_FiyatGrubu.Items.Refresh();
                        excelCollection = lastexcelCollectionection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastexcelCollectionection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset = center;

                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
            }

        }
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        // Select the "Miktar" column of the row
                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
            }
        }
        private void btn_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;


                if (clickedButton.Parent is StackPanel stackPanel)
                {
                    foreach (UIElement element in stackPanel.Children)
                    {
                        if (element is ComboBox comboBox)
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        if (element is DatePicker datePicker)
                        {
                            datePicker.SelectedDate = null;
                        }
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private async void btn_sil_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Silinecek Liste Bulunamadı");
                    return;
                }
                if (excelCollection.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessage("Silinecek Liste Bulunamadı");
                    return;
                }
                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_)
                    return;

                btn_listele.IsEnabled = false;
                btn_guncelle.IsEnabled = false;
                btn_excel_getir.IsEnabled = false;
                btn_sile.IsEnabled = false;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

                txt_result.Text = "Silme İşlemi Devam Ediyor...";
                Variables.Result_ = await fiyat.DeleteFiyatAsync(excelCollection);
                if(Variables.Result_)
                    CRUDmessages.DeleteSuccessMessage("Fiyat", excelCollection.Count);
                else
                    CRUDmessages.DeleteFailureMessage("Fiyat");

                    Mouse.OverrideCursor = Cursors.Wait;
                    if (filterDic != null)
                        filterDic.Clear();
                    else
                        filterDic = new Dictionary<string, string>();

                    if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                        filterDic.Add("STOKKODU", txt_stok_kodu.Text);
                    if (cbx_fiyat_grup.SelectedItem != null)
                        filterDic.Add("FIYATGRUBU", cbx_fiyat_grup.SelectedItem.ToString());
                    if (cbx_kod1.SelectedItem != null)
                        filterDic.Add("KOD1", cbx_kod1.SelectedItem.ToString());

                    excelCollection = fiyat.GetFiyatList(filterDic, 1);
                    if (excelCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                        Mouse.OverrideCursor = null;
                        dg_FiyatGrubu.Visibility = Visibility.Collapsed;
                        stc_kaydet.Visibility = Visibility.Collapsed;
                        txt_result.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (excelCollection.Count == 0)
                    {
                        CRUDmessages.QueryIsEmpty();
                        Mouse.OverrideCursor = null;

                        dg_FiyatGrubu.Visibility = Visibility.Collapsed;
                        stc_kaydet.Visibility = Visibility.Collapsed;
                        txt_result.Visibility = Visibility.Collapsed;
                        return;
                    }
                    totalResult = fiyat.CountFiyatList(filterDic, 1);
                    if (totalResult > 100)
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                    else
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                    dg_FiyatGrubu.ItemsSource = excelCollection;
                    dg_FiyatGrubu.Visibility = Visibility.Visible;
                    stc_kaydet.Visibility = Visibility.Visible;
                    txt_result.Visibility = Visibility.Visible;
                    lastOffset = 0;
                    pageNumber = 1;

                btn_listele.IsEnabled = true;
                btn_guncelle.IsEnabled = true;
                btn_excel_getir.IsEnabled = true;
                btn_sile.IsEnabled = true;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                Mouse.OverrideCursor = null;

                    return;
                
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                btn_listele.IsEnabled = true;
                btn_guncelle.IsEnabled = true;
                btn_excel_getir.IsEnabled = true;
                btn_sile.IsEnabled = true;
                dg_FiyatGrubu.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }

        }

        public DataTable ReadExcelFile(string filePath)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        // Return the first DataTable (in case of multiple sheets)
                        return result.Tables[0];
                    }

                }
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(ex.ToString());
                return null;
            }
        }

    }
}

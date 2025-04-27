using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Methods;
using Layer_UI.Satis.Evrak;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Depo.DAT
{
    public partial class Frm_DAT_Rapor : Window
    {
        Dictionary<string, string> constraints = new Dictionary<string, string>();
        bool isCheckedDosemeli = false;
        bool isCheckedModuler = false;
        public Frm_DAT_Rapor()
        {
            InitializeComponent(); Window_Loaded();

            cbx_kod1.ItemsSource = depo.GetDistinctKod1();
            cbx_kod5.ItemsSource = depo.GetDistinctKod5();

        }
        private void Window_Loaded()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Left;
            this.Top = workArea.Top;
            this.Width = workArea.Width;
            this.Height = workArea.Height;
            this.Topmost = true;
            this.Topmost = false;

        }
        Cls_Depo depo = new();
        Variables variables = new();
        private ObservableCollection<Cls_Depo> depoCollection = new();
        private ObservableCollection<Cls_Depo> takipNoColl = new();
        private ObservableCollection<Cls_Depo> selectedTakipNoColl = new();
        private void btn_dat_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {


                dg_dat_liste.ItemsSource = null;
                dg_dat_liste.Items.Clear();
                Mouse.OverrideCursor = Cursors.Wait;

                if (constraints != null) { if (constraints.Count > 0) constraints.Clear(); }


                if (!string.IsNullOrWhiteSpace(txt_takip_no.Text))
                    constraints.Add("takipNo", txt_takip_no.Text);
                else
                    constraints.Add("takipNo", null);

                if (!string.IsNullOrWhiteSpace(txt_ham_kodu.Text))
                    constraints.Add("hamKodu", txt_ham_kodu.Text);
                else
                    constraints.Add("hamKodu", null);

                if (!string.IsNullOrWhiteSpace(txt_ham_adi.Text))
                    constraints.Add("hamAdi", txt_ham_adi.Text);
                else
                    constraints.Add("hamAdi", null);

                if (cbx_kod1.SelectedItem != null)
                    constraints.Add("kod1", cbx_kod1.SelectedItem.ToString());
                else
                    constraints.Add("kod1", null);

                if (cbx_kod5.SelectedItem != null)
                    constraints.Add("kod5", cbx_kod5.SelectedItem.ToString());
                else
                    constraints.Add("kod5", null);

                Variables.Counter_ = 0;
                if(selectedTakipNoColl != null)
                {
                    if(selectedTakipNoColl.Count > 0)
                    {
                        foreach (var item in selectedTakipNoColl)
                        {
                            if (!string.IsNullOrEmpty(item.TakipNo))
                            {
                                constraints.Add(string.Format("TakipNo{0}", Variables.Counter_), item.TakipNo);
                                Variables.Counter_++;
                            }
                        }
                    }
                }
                
                depoCollection = depo.PopulateDATKaydedilecekListesi(constraints, "Vita",selectedTakipNoColl.Count(),1);

                if (depoCollection == null)
                { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (depoCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("DAT Kayıtları"); Mouse.OverrideCursor = null; return; }

                dg_dat_liste.ItemsSource = depoCollection;

                lastOffset1 = 0;
                pageNumber1 = 1;
                totalResult1 = depo.CountDATKaydedilecekListesi(constraints, "Vita", selectedTakipNoColl.Count(), 1);
                if (totalResult1 > 100)
                    txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan " + totalResult1.ToString() + " adet Listeleniyor.";
                txt_result.Visibility = Visibility.Visible;

                var border = VisualTreeHelper.GetChild(dg_dat_liste, 0) as Decorator;
                if (border != null)
                {
                    var scrollViewer = border.Child as ScrollViewer;
                    if (scrollViewer != null)
                        scrollViewer.ScrollToVerticalOffset(0);
                }
                   

                Mouse.OverrideCursor = null;
            }

            catch { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private async void btn_excele_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;

                await Task.Run(() =>
                {


                    ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                    string filePath = string.Format("C:\\excel-c\\depo\\{0}_{1}", "DAT_Rapor", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                    string sheetName = "Dat_Rapor";

                    filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                    FileInfo fileInfo = new FileInfo(filePath);

                    var existingPackage = new ExcelPackage(fileInfo);

                    //şablon header kısmı
                    excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 5, 25);
                    //şablon sağdan soldan 1px
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 14, 1);
                    //sütun genişlikleri
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 12);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 19);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 44);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 12);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 12);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 8);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 8);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 13, 11);

                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:A122", "#FFFFFF");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:N1", "#FFFFFF");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "N1:N122", "#FFFFFF");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:M2", "#333F4F");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:M3", "#3B495B");

                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#FFFFFF", true);
                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Depo-DAT Rapor", "Calibri", 13, "#FFFFFF", true);

                    excelWorks.InsertImage(existingPackage, sheetName, imagePath, "logo", 13, 2, 57, 57);

                    DataTable dataTable = GetDataFromCollection(depoCollection);
                    int rowCount = dataTable.Rows.Count;
                    int columnCount = dataTable.Columns.Count;

                    excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                    excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                    excelWorks.TextWrap(existingPackage, sheetName, "B6:M" + rowCount + 6, true);

                    int i = 7;
                    while (i < rowCount + 7)
                    {
                        excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                        i++;
                    }

                    excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:M6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#FFFFFF", "Dat_Rapor");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("DAT Tablosu Excele Aktarıldı.");
                    });
                });
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
                Mouse.OverrideCursor = null;
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Depo> stokHareketCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Takip No");
            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Kod 5");
            dataTable.Columns.Add("Kod 1");
            dataTable.Columns.Add("Çıkış Depo");
            dataTable.Columns.Add("Giriş Depo");
            dataTable.Columns.Add("Çıkış Depo Bakiye");
            dataTable.Columns.Add("Giriş Depo Bakiye");
            dataTable.Columns.Add("Toplam İht Mik");
            dataTable.Columns.Add("Gönd Mik");
            dataTable.Columns.Add("Kalan Miktar");

            foreach (Cls_Depo item in stokHareketCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Takip No"] = item.TakipNo;
                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Kod 5"] = item.Kod5;
                    dataRow["Kod 1"] = item.Kod1;
                    dataRow["Çıkış Depo"] = item.CikisDepoKodu;
                    dataRow["Giriş Depo"] = item.GirisDepoKodu;
                    dataRow["Çıkış Depo Bakiye"] = item.CikisDepoBakiye;
                    dataRow["Giriş Depo Bakiye"] = item.GirisDepoBakiye;
                    dataRow["Toplam İht Mik"] = item.ToplamDATIhtiyacMiktar;
                    dataRow["Gönd Mik"] = item.GonderilenDATMiktar;
                    dataRow["Kalan Miktar"] = item.GonderilecekDATMiktar;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void dat_detay_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Depo gridItem = UIinteractions.GetDataItemFromButton<Cls_Depo>(sender);
                if (gridItem == null)
                {
                    CRUDmessages.GeneralFailureMessage("Dat Detay Bilgileri Listelenirken");
                    Mouse.OverrideCursor = null;
                }
                Cls_Depo? datDetay = new Cls_Depo
                {
                    TakipNoGenel = gridItem.TakipNo,
                    StokKodu = gridItem.StokKodu,
                };

                ObservableCollection<Cls_Depo> datDetayCollection = depo.GetDATDetay(datDetay);

                Frm_DAT_Detay _frm = new(datDetayCollection);
                var result = _frm.ShowDialog();
                if (result == false)
                {
                    depoCollection = depo.PopulateDATKaydedilecekListesi(constraints, "Vita",selectedTakipNoColl.Count());

                    if (depoCollection == null)
                    { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
                    if (depoCollection.Count == 0)
                    { CRUDmessages.QueryIsEmpty("DAT Kayıtları"); Mouse.OverrideCursor = null; return; }

                    dg_dat_liste.ItemsSource = depoCollection;

                    Mouse.OverrideCursor = null;
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Plan Adı Detayları Getirilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_takip_no_getir_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                

                if (Variables.Departman_.Contains("Moduler"))
                {
                    cx_sadece_moduler.IsChecked = true;
                    isCheckedModuler = true;
                   
                }
                else if (Variables.Departman_.Contains("Doseme"))
                {
                    cx_sadece_dosemeli.IsChecked = true;
                    isCheckedDosemeli = true;
                }
                takipNoColl = depo.GetTakipNos(isCheckedDosemeli, isCheckedModuler, string.Empty,1);

                totalResult = depo.CountTakipNos(isCheckedDosemeli, isCheckedModuler, string.Empty);
                lastOffset = 0;
                pageNumber = 1;
               
                lst_takip_no.ItemsSource = takipNoColl;
                PopupTakipNo.IsOpen = true;
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Popup Açılırken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_filtrele_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Depo> filteredColl = depo.GetTakipNos(isCheckedDosemeli,isCheckedModuler,txt_search.Text);
                if(filteredColl == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralMessage("Takip Numaraları Filtrelenirken");
                    return;
                }
                if (filteredColl.Count == 0)
                {
                    Mouse.OverrideCursor = null ;
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                lst_takip_no.ItemsSource= filteredColl;
                lst_takip_no.Items.Refresh();
                totalResult = depo.CountTakipNos(isCheckedDosemeli, isCheckedModuler, txt_search.Text);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";
                txt_result.Visibility = Visibility.Visible;
                lastOffset = 0;
                pageNumber = 1;
                var border = VisualTreeHelper.GetChild(lst_takip_no, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer != null)
                    scrollViewer.ScrollToVerticalOffset(0);
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralMessage("Takip Numaraları Filtrelenirken");
            }
        }
        private void cx_moduler_checked(object sender, EventArgs e)
        {
            if (cx_sadece_moduler.IsChecked == true)
            {
                isCheckedModuler = true;
                isCheckedDosemeli = false;
                cx_sadece_dosemeli.IsChecked = false;
            }
            else
                isCheckedModuler = false;;

        }
        private void cx_dosemeli_checked(object sender, EventArgs e)
        {
            if (cx_sadece_dosemeli.IsChecked == true)
            {
                isCheckedModuler = false;
                isCheckedDosemeli = true;
                cx_sadece_moduler.IsChecked = false;
            }
            else
                isCheckedDosemeli = false; ;

        }
        private void btn_exit_clicked(object sender, EventArgs e)
        {
            PopupTakipNo.IsOpen = false;
        }
        private void btn_takip_no_ekle_clicked(object sender, EventArgs e)
        {
            foreach(Cls_Depo item in lst_takip_no.Items)
            {
                if(item.IsChecked == true)
                {
                    if(!selectedTakipNoColl.Contains(item))
                        selectedTakipNoColl.Add(item);
                }
            }
        }
        double lastOffset = 0;
        int pageNumber = 1;
        int totalResult = 0;
        double lastOffset1 = 0;
        int pageNumber1 = 1;
        int totalResult1 = 0;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(lst_takip_no, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > takipNoColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Depo> moreTakipCollection = new();
                    moreTakipCollection = depo.GetTakipNos(isCheckedDosemeli, isCheckedModuler,txt_search.Text,pageNumber);
                    if (moreTakipCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Takip Nolar Listelenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTakipCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastMasterCollection = new ObservableCollection<Cls_Depo>
                                        (takipNoColl.Union(moreTakipCollection).ToList());
                        lst_takip_no.ItemsSource = lastMasterCollection;
                        lst_takip_no.Items.Refresh();
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
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(lst_takip_no, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > takipNoColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Depo> moreTakipCollection = new();
                    moreTakipCollection = depo.GetTakipNos(isCheckedDosemeli, isCheckedModuler,txt_search.Text,pageNumber);
                    if (moreTakipCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Takip Nolar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTakipCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastTakipCollection = new ObservableCollection<Cls_Depo>
                                        (takipNoColl.Union(moreTakipCollection).ToList());
                        lst_takip_no.ItemsSource = lastTakipCollection;
                        lst_takip_no.Items.Refresh();
                        takipNoColl = lastTakipCollection;
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
            }

        }
        private void dg_scroll_down1(object sender, ScrollEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(dg_dat_liste, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset1 = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset1 > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult1 > depoCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber1++;
                    ObservableCollection<Cls_Depo> moreDepoCollection = new();
                    moreDepoCollection = depo.PopulateDATKaydedilecekListesi(constraints, "Vita", selectedTakipNoColl.Count(), pageNumber1);
                    if (moreDepoCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave DAT Bilgileri Listelenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreDepoCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastMasterCollection = new ObservableCollection<Cls_Depo>
                                        (depoCollection.Union(moreDepoCollection).ToList());
                        dg_dat_liste.ItemsSource = lastMasterCollection;
                        dg_dat_liste.Items.Refresh();
                        depoCollection = lastMasterCollection;


                        if (pageNumber1 * 100 <= totalResult1)
                            txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan " + pageNumber1 * 100 + " adet Listeleniyor.";
                        else
                            txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan " + totalResult1.ToString() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset1 = center;
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
            }

        }
        private void mouse_wheel_pushed1(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(dg_dat_liste, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset1 = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset1 > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult1 > depoCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber1++;
                    ObservableCollection<Cls_Depo> moreDepoCollection = new();
                    moreDepoCollection = depo.PopulateDATKaydedilecekListesi(constraints, "Vita", selectedTakipNoColl.Count(), pageNumber1);
                    if (moreDepoCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave DAT Bilgileri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreDepoCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastDepoCollection = new ObservableCollection<Cls_Depo>
                                        (depoCollection.Union(moreDepoCollection).ToList());
                        dg_dat_liste.ItemsSource = lastDepoCollection;
                        dg_dat_liste.Items.Refresh();
                        depoCollection = lastDepoCollection;

                        if(pageNumber1 * 100 <= totalResult1)
                            txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan " + pageNumber1 * 100 + " adet Listeleniyor.";
                        else
                            txt_result.Text = "Toplam " + totalResult1.ToString() + " Adet Sonuçtan " + totalResult1.ToString() + " adet Listeleniyor.";


                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset1 = center;

                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
            }

        }
        private void btn_takip_no_clear(object sender, EventArgs e)
        {
            selectedTakipNoColl.Clear();
        }
        private void btn_show_takip_no(object sender, EventArgs e)
        {
            dg_takip_liste.ItemsSource = selectedTakipNoColl;
            PopupTakipNoSecilen.IsOpen = true;
        }
        private void btn_secilen_takip_sil_clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DataGridRow row = UIinteractions.FindVisualParent<DataGridRow>(button);

            if (row != null)
            {
                Cls_Depo item = row.Item as Cls_Depo;
                selectedTakipNoColl.Remove(item);
            }
            dg_takip_liste.ItemsSource = selectedTakipNoColl ;
            dg_takip_liste.Items.Refresh();
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

    }
}

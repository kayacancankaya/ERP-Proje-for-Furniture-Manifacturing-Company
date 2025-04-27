using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Arge.StokKarti
{
    /// <summary>
    /// Interaction logic for Frm_Stok_Ek_Bilgi_Ekle_Excel.xaml
    /// </summary>
    public partial class Frm_Stok_Ek_Bilgi_Ekle_Excel : Window
    {
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
        public Frm_Stok_Ek_Bilgi_Ekle_Excel()
        {
            InitializeComponent(); Window_Loaded();
        }
        ExcelMethodsEPP excel = new();
        Variables variables = new();
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> excelCollection = new();
        private void btn_stok_rehberi_clicked(object sender,RoutedEventArgs e)
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
                if(string.IsNullOrEmpty(txt_stok_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = arge.IfStokKoduExists(txt_stok_kodu.Text);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kartı Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                excelCollection = arge.PopulateStokKartiEkBilgilerList(txt_stok_kodu.Text);
                if(excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if(excelCollection == null)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_StokEkBilgi.ItemsSource = excelCollection    ;
                txt_pageResult.Text = "Toplam " + dg_StokEkBilgi.Items.Count + " adet Stok Kartı listeleniyor.";

                stack_panel_stok_ekbilgi_kaydet.Visibility = Visibility.Visible;
                dg_StokEkBilgi.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_excel_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                DataTable dataTable = new DataTable();
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    dataTable = excel.ReadExcelFile(filePath, "Stok_Ek_Bilgi", 2, 24, 8);
                }
                else { Mouse.OverrideCursor = null; return; }

                if (excelCollection != null)
                    excelCollection.Clear();


                foreach (DataRow row in dataTable.Rows)
                {
                    Cls_Arge dataModel = new()
                    {
                        StokKartiStokKodu = row["StokKartiStokKodu"] is DBNull ? string.Empty : row["StokKartiStokKodu"].ToString(),
                        StokAdi = row["stok_adi"] is DBNull ? string.Empty : row["stok_adi"].ToString(),
                        StokKodu = row["StokKodu"] is DBNull ? string.Empty : row["StokKodu"].ToString(),
                        KabaBoy = row["KabaBoy"] is DBNull ? 0 :  Convert.ToSingle(row["KabaBoy"]),
                        KabaEn = row["KabaEn"] is DBNull ? 0 :  Convert.ToSingle(row["KabaEn"]),
                        NetBoy = row["NetBoy"] is DBNull ? 0 :  Convert.ToSingle(row["NetBoy"]),
                        NetEn = row["NetEn"] is DBNull ? 0 :  Convert.ToSingle(row["NetEn"]),
                        BitmisBoy = row["BitmisBoy"] is DBNull ? 0 :  Convert.ToSingle(row["BitmisBoy"]),
                        BitmisEn = row["BitmisEn"] is DBNull ? 0 :  Convert.ToSingle(row["BitmisEn"]),
                        B1KB = row["B1KB"] is DBNull ? string.Empty : row["B1KB"].ToString(),
                        EKB1 = row["EKB1"] is DBNull ? string.Empty : row["EKB1"].ToString(),
                        EKB2 = row["EKB2"] is DBNull ? string.Empty : row["EKB2"].ToString(),
                        B2KB = row["B2KB"] is DBNull ? string.Empty : row["B2KB"].ToString(),
                        B1KBRecSiraNo = row["B1KBRecSiraNo"] is DBNull ? string.Empty : row["B1KBRecSiraNo"].ToString(),
                        EKB1RecSiraNo = row["EKB1RecSiraNo"] is DBNull ? string.Empty : row["EKB1RecSiraNo"].ToString(),
                        EKB2RecSiraNo = row["EKB2RecSiraNo"] is DBNull ? string.Empty : row["EKB2RecSiraNo"].ToString(),
                        B2KBRecSiraNo = row["B2KBRecSiraNo"] is DBNull ? string.Empty : row["B2KBRecSiraNo"].ToString(),
                        Aciklama = row["Aciklama"] is DBNull ? string.Empty :  row["Aciklama"].ToString(),
                        YuzeyDelik = row["YuzeyDelik"] is DBNull ? string.Empty : row["YuzeyDelik"].ToString(),
                        CumbaDelik = row["CumbaDelik"] is DBNull ? string.Empty :  row["CumbaDelik"].ToString(),
                        CncKanalBoyu = row["CncKanalBoyu"] is DBNull ? string.Empty : row["CncKanalBoyu"].ToString(),
                        MontajSure = row["MontajSure"] is DBNull ? string.Empty : row["MontajSure"].ToString(),
                        Method = row["Method"] is DBNull ? string.Empty : row["Method"].ToString()
                    };

                    excelCollection.Add(dataModel);
                }


                dg_StokEkBilgi.ItemsSource = excelCollection;

                txt_pageResult.Text = "Toplam " + dg_StokEkBilgi.Items.Count + " adet Stok Kartı listeleniyor.";

                stack_panel_stok_ekbilgi_kaydet.Visibility = Visibility.Visible;
                dg_StokEkBilgi.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("format"))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Excelde Format Hatası Mevcut"); Mouse.OverrideCursor = null; return;
                }
                CRUDmessages.GeneralFailureMessage("Excel Listelenirken"); Mouse.OverrideCursor = null;
            }
        }
        private async void btn_stok_ekbilgi_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                variables.ErrorMessage = string.Empty;

                var hatayaDusenReceteler = excelCollection.Where(i => i.ReceteKayitDurum != "Kaydedilmedi...");
                if (hatayaDusenReceteler.Any())
                {
                    foreach (Cls_Arge item in excelCollection.Where(i => i.ReceteKayitDurum != "Kaydedilmedi..."))
                        item.ReceteKayitDurum = "Kaydedilmedi...";
                }

                //stok kodları 35 karakterden büyük 11 karakterden küçük olamaz
                if (excelCollection.Where(s => s.StokKodu.Length > 35).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.StokKodu.Length > 35).ToList();

                    foreach (Cls_Arge item in hataliStokKodlari)
                    {
                        item.ReceteKayitDurum = item.MamulKodu + " Stok Kodu 35 Karakterden Büyük Olamaz...";
                    }
                }
             
                foreach (Cls_Arge item in excelCollection)
                {
                    Variables.Result_ = await arge.IfStokKoduExistsAsync(item.StokKodu);

                    if (!Variables.Result_)
                        item.ReceteKayitDurum = item.StokKodu + " Stok Kartı Bulunamadı...";
                }

                if(excelCollection.Where(m=>m.Method != "UPDATE" &&
                                            m.Method != "INSERT").Any())
                {
                    var methodEksikListe = excelCollection.Where(m => m.Method != "UPDATE" &&
                                                                    m.Method != "INSERT").ToList();
                    foreach (Cls_Arge item in methodEksikListe)
                        item.ReceteKayitDurum = "Method Belirtilmemiş ('UPDATE','INSERT')...";
                }

                if (excelCollection.Where(i => i.ReceteKayitDurum != "Kaydedilmedi...").Any())
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Hata Veren Stok Kartları Mevcut.\n Hatalı Stok Kartlarını Silerek veya Yeniden Excel Yükleyerek İlerleyebilirsiniz.");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                excelCollection = await arge.InsertUpdateStokEkBilgilerAsync(excelCollection);

                dg_StokEkBilgi.ItemsSource = excelCollection;

                dg_StokEkBilgi.Items.Refresh();

                txt_please_wait.Visibility = Visibility.Collapsed;

                CRUDmessages.GeneralSuccessMessage("İşlem");

            }
            catch
            {
                txt_please_wait.Visibility = Visibility.Collapsed;
                CRUDmessages.GeneralFailureMessage("Stok Kartları Kaydedilirken"); Mouse.OverrideCursor = null;
            }
        }
        private void btn_stok_karti_sil(object sender, RoutedEventArgs e)
        {

            try
            {
               
                variables.ErrorMessage = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Stok Kartı Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Stok Kartı Bilgileri Alınırken"); return; }

                Cls_Arge dataItem = row.Item as Cls_Arge;

                excelCollection.Remove(dataItem);

                dg_StokEkBilgi.ItemsSource = excelCollection;

                dg_StokEkBilgi.Items.Refresh();

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
    }
}

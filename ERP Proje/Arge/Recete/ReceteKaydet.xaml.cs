using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Recete
{
    /// <summary>
    /// Interaction logic for ReceteKaydet.xaml
    /// </summary>
    public partial class ReceteKaydet : Window
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
        public ReceteKaydet()
        {
            InitializeComponent(); Window_Loaded();
        }
        ExcelMethodsEPP excel = new();
        Variables variables = new();
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> excelCollection = new();
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
                    dataTable = excel.ReadExcelFile(filePath, 0, 2, 6, 5);
                }
                else { Mouse.OverrideCursor = null; return; }

                if (excelCollection != null)
                    excelCollection.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Cls_Arge arge = new Cls_Arge()
                    {
                        MamulKodu = row["Mamul Kodu"].ToString(),
                        HamKodu = row["Ham Kodu"].ToString(),
                        ReceteTuketimMiktar = Convert.ToDecimal(row["Miktar"]),
                        Opno = row["Sira"].ToString(),
                        ReceteAciklama = row["Aciklama"].ToString(),
                    };

                    excelCollection.Add(arge);
                }

                dg_ReceteEkle.ItemsSource = excelCollection;

                txt_pageResult.Text = "Toplam " + dg_ReceteEkle.Items.Count + " adet reçete listeleniyor.";

                stack_panel_recete_kaydet.Visibility = Visibility.Visible;
                dg_ReceteEkle.Visibility = Visibility.Visible;

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
        private async void btn_recete_kaydet_clicked(object sender, RoutedEventArgs e)
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
                if (excelCollection.Where(s => s.MamulKodu.Length > 35).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.MamulKodu.Length > 35).ToList();

                    foreach (Cls_Arge item in hataliStokKodlari)
                    {
                        item.ReceteKayitDurum = item.MamulKodu + " Stok Kodu 35 Karakterden Büyük Olamaz...";
                    }
                }
                if (excelCollection.Where(s => s.HamKodu.Length > 35).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.HamKodu.Length > 35).ToList();

                    foreach (Cls_Arge item in hataliStokKodlari)
                    {
                        item.ReceteKayitDurum = item.HamKodu + " Stok Kodu 35 Karakterden Büyük Olamaz...";
                    }
                }
                if (excelCollection.Where(s => s.MamulKodu.Length < 11).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.MamulKodu.Length < 11).ToList();

                    foreach (Cls_Arge item in hataliStokKodlari)
                    {
                        item.ReceteKayitDurum = item.MamulKodu + " Stok Kodu 11 Karakterden Büyük Olamaz...";
                    }
                }
                if (excelCollection.Where(s => s.HamKodu.Length < 11).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.HamKodu.Length < 11).ToList();

                    foreach (Cls_Arge item in hataliStokKodlari)
                    {
                        item.ReceteKayitDurum = item.HamKodu + " Stok Kodu 11 Karakterden Küçük Olamaz...";
                    }
                }

                foreach (Cls_Arge item in excelCollection)
                {
                    Variables.Result_ = await arge.IfStokKoduExistsAsync(item.HamKodu);

                    if (!Variables.Result_)
                        item.ReceteKayitDurum = item.HamKodu + " Stok Kartı Bulunamadı...";

                    Variables.Result_ = await arge.IfStokKoduExistsAsync(item.MamulKodu);

                    if (!Variables.Result_)
                        item.ReceteKayitDurum = item.HamKodu + " Stok Kartı Bulunamadı...";


                    Variables.Result_ = await arge.IfUrunAgaciExistsAsync(item.MamulKodu, item.HamKodu, item.Opno);

                    if (!Variables.Result_)
                        item.ReceteKayitDurum = "Ürün Ağacı Mevcut...";

                    Variables.ResultInt_ = await Cls_Arge.OpnoControl(item.Opno);
                    switch (Variables.ResultInt_)
                    {
                        case 1:
                            break;
                        case -1:
                            item.ReceteKayitDurum = item.Opno + " Kontrol Edilirken Sistemsel Bir Problem İle Karşılaşıldı... \n";
                            break;
                        case 2:
                            item.ReceteKayitDurum = item.Opno + " Uzunluğu 4 olmalı... \n";
                            break;
                        case 3:
                            item.ReceteKayitDurum = item.Opno + " Tüm Karakterler Rakam Olmalı... \n";
                            break;
                    }

                }

                if (excelCollection.Where(i => i.ReceteKayitDurum != "Kaydedilmedi...").Any())
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Hata Veren Reçeteler Mevcut.\n Hatalı Reçeteleri Silerek veya Yeniden Excel Yükleyerek İlerleyebilirsiniz.");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                excelCollection = await arge.InsertUrunAgaciAsync(excelCollection);

                dg_ReceteEkle.ItemsSource = excelCollection;

                dg_ReceteEkle.Items.Refresh();

                txt_please_wait.Visibility = Visibility.Collapsed;

                CRUDmessages.GeneralSuccessMessage("İşlem");

            }
            catch
            {
                txt_please_wait.Visibility = Visibility.Collapsed;
                CRUDmessages.GeneralFailureMessage("Reçeteler Kaydedilirken"); Mouse.OverrideCursor = null;
            }
        }
        private void btn_recete_sil(object sender, RoutedEventArgs e)
        {
            variables.IsTrue = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (variables.IsTrue)
                {
                    variables.ErrorMessage = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }

                    Cls_Arge dataItem = row.Item as Cls_Arge;

                    excelCollection.Remove(dataItem);

                    dg_ReceteEkle.ItemsSource = excelCollection;

                    dg_ReceteEkle.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
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

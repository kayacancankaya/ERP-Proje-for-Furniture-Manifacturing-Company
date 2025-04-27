using ExcelDataReader;
using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using Microsoft.Win32;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Maliyet
{
    /// <summary>
    /// Interaction logic for Frm_Varyant_Fiyat_Giris.xaml
    /// </summary>
    public partial class Frm_Varyant_Fiyat_Giris : Window
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
        public Frm_Varyant_Fiyat_Giris()
        {
            InitializeComponent(); Window_Loaded();
        }
        ExcelMethodsEPP excel = new();
        Cls_Arge arge = new();
        Variables variables = new Variables();
        ObservableCollection<VaryantFiyatGirisViewModel> excelCollection = new();
        private void btn_stok_rehberi_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Sablon_Rehberi frm = new();
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
                if (string.IsNullOrEmpty(txt_stok_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if(txt_stok_kodu.Text.Length != 11)
                {
                    CRUDmessages.GeneralFailureMessage("Stok Kodu 11 Karakterden Oluşmalı.");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = arge.IfStokKoduExists(txt_stok_kodu.Text);
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kartı Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                excelCollection = arge.PopulateVaryantFiyatGirList(txt_stok_kodu.Text);
                if (excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (excelCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_FiyatGrubu.ItemsSource = excelCollection;
                txt_pageResult.Text = "Toplam " + dg_FiyatGrubu.Items.Count + " adet Stok Kartı listeleniyor.";

                stack_panel_fiyat_kaydet.Visibility = Visibility.Visible;
                dg_FiyatGrubu.Visibility = Visibility.Visible;

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
                //Mouse.OverrideCursor = Cursors.Wait;

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                DataTable dataTable = new DataTable();
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    DataTable data = await Task.Run(() => ReadExcelFile(filePath));
                    if (data == null)
                    {
                        CRUDmessages.GeneralFailureMessage("Excel Bilgileri Alınırken");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                }
                else { Mouse.OverrideCursor = null; return; }

                //excelCollection = new();


                //foreach (DataRow row in dataTable.Rows)
                //{
                //    VaryantFiyatGirisViewModel model = new();

                //    model.StokAdi = row["STOKADI"] is DBNull ? string.Empty : row["STOKADI"].ToString();
                //    model.StokKodu = row["STOKKODU"] is DBNull ? string.Empty : row["STOKKODU"].ToString();
                //    model.FiyatGrubu = row["FIYATGRUBU"] is DBNull ? string.Empty : row["FIYATGRUBU"].ToString();

                //    model.A = row["A"] is DBNull ? 0 : Convert.ToSingle(row["A"]);
                //    model.B = row["B"] is DBNull ? 0 : Convert.ToSingle(row["B"]);
                //    model.C = row["C"] is DBNull ? 0 : Convert.ToSingle(row["C"]);
                //    model.D = row["D"] is DBNull ? 0 : Convert.ToSingle(row["D"]);
                //    model.E = row["E"] is DBNull ? 0 : Convert.ToSingle(row["E"]);
                //    model.F = row["F"] is DBNull ? 0 : Convert.ToSingle(row["F"]);
                //    model.G = row["G"] is DBNull ? 0 : Convert.ToSingle(row["G"]);
                //    model.H = row["H"] is DBNull ? 0 : Convert.ToSingle(row["H"]);
                //    model.HPlus = row["H+"] is DBNull ? 0 : Convert.ToSingle(row["H+"]);
                //    model.I = row["I"] is DBNull ? 0 : Convert.ToSingle(row["I"]);
                //    model.IPlus = row["I+"] is DBNull ? 0 : Convert.ToSingle(row["I+"]);
                //    model.J = row["J"] is DBNull ? 0 : Convert.ToSingle(row["J"]);
                //    model.JPlus = row["J+"] is DBNull ? 0 : Convert.ToSingle(row["J+"]);
                //    model.K = row["K"] is DBNull ? 0 : Convert.ToSingle(row["K"]);
                //    model.L = row["L"] is DBNull ? 0 : Convert.ToSingle(row["L"]);
                //    model.DosemeSure = row["DOSEMESURE"] is DBNull ? 0 : Convert.ToSingle(row["DOSEMESURE"]);

                //    excelCollection.Add(model);
                //}


                //dg_FiyatGrubu.ItemsSource = excelCollection;

                //txt_pageResult.Text = "Toplam " + dg_FiyatGrubu.Items.Count + " adet Stok Kartı listeleniyor.";

                //stack_panel_fiyat_kaydet.Visibility = Visibility.Visible;
                //dg_FiyatGrubu.Visibility = Visibility.Visible;

                //Mouse.OverrideCursor = null;
                //CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

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
        private async void btn_fiyat_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                variables.ErrorMessage = string.Empty;

                var hatayaDusenReceteler = excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi...");
                if (hatayaDusenReceteler.Any())
                {
                    foreach (VaryantFiyatGirisViewModel item in excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi..."))
                        item.KayitDurum = "Kaydedilmedi...";
                }

                //stok kodları 35 karakterden büyük 11 karakterden küçük olamaz
                if (excelCollection.Where(s => s.StokKodu.Length > 35).Any())
                {
                    var hataliStokKodlari = excelCollection.Where(s => s.StokKodu.Length > 35).ToList();

                    foreach (VaryantFiyatGirisViewModel item in hataliStokKodlari)
                    {
                        item.KayitDurum = item.StokKodu + " Stok Kodu 35 Karakterden Büyük Olamaz...";
                    }
                }

                foreach (VaryantFiyatGirisViewModel item in excelCollection)
                {
                    Variables.Result_ = await arge.IfStokKoduExistsAsync(item.StokKodu);

                    if (!Variables.Result_)
                        item.KayitDurum = item.StokKodu + " Stok Kartı Bulunamadı...";
                }


                if (excelCollection.Where(i => i.KayitDurum != "Kaydedilmedi...").Any())
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Hata Veren Stok Kartları Mevcut.\n Hatalı Stok Kartlarını Silerek veya Yeniden Excel Yükleyerek İlerleyebilirsiniz.");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                excelCollection = await arge.InsertUpdateVaryantFiyatAsync(excelCollection);
                if(excelCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_FiyatGrubu.ItemsSource = excelCollection;

                dg_FiyatGrubu.Items.Refresh();

                txt_please_wait.Visibility = Visibility.Collapsed;

                CRUDmessages.InsertSuccessMessage("Maliyet",excelCollection.Count);

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

                VaryantFiyatGirisViewModel dataItem = row.Item as VaryantFiyatGirisViewModel;

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
        public DataTable ReadExcelFile(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // If your Excel file has a header row
                        }
                    });

                    // Return the first DataTable (in case of multiple sheets)
                    return result.Tables[0];
                }
            }
        }
    }
}

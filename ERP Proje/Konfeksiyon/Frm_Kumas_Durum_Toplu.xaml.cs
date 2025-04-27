using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Konfeksiyon
{
    /// <summary>
    /// Interaction logic for Frm_Kumas_Durum_Toplu.xaml
    /// </summary>
    public partial class Frm_Kumas_Durum_Toplu : Window
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
        public Frm_Kumas_Durum_Toplu()
        {
            InitializeComponent(); Window_Loaded();
        }

        ExcelMethodsEPP excel = new();
        Cls_Uretim uretim = new();
        ObservableCollection<Cls_Uretim> excelCollection = new();
        private void btn_excel_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                DataTable dataTable = new DataTable();
                if (openFileDialog.ShowDialog() == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    string filePath = openFileDialog.FileName;
                    dataTable = excel.ReadExcelFile(filePath, "kumas_aktarim", 1, 2, 1);
                }
                else return;

                if (excelCollection != null)
                    excelCollection.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Cls_Uretim uretim = new Cls_Uretim
                    {
                        StokKodu = row["stok_kodu"].ToString(),
                        StokAdi = row["stok_adi"].ToString()
                    };

                    excelCollection.Add(uretim);
                }

                dg_StokEkle.ItemsSource = excelCollection;

                txt_pageResult.Text = "Toplam " + dg_StokEkle.Items.Count + " adet stok listeleniyor.";
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excel Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }
        private async void btn_listele_clicked(object sender, EventArgs e)
        {
            try
            {

                await Task.Run(() =>
                {

                    if (excelCollection == null)
                    {
                        CRUDmessages.QueryIsEmpty();
                        return;
                    }
                    if (excelCollection.Count == 0)
                    {
                        CRUDmessages.QueryIsEmpty();
                        return;
                    }
                });

                txt_please_wait.Visibility = Visibility.Visible;
                ObservableCollection<Cls_Uretim> resultCollection = new();
                resultCollection = await uretim.GetKumasUretimDurumAsync(excelCollection);

                if (resultCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Kumaş Bilgileri Alınırken");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }
                if (resultCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }
                excelCollection = resultCollection;

                dg_StokEkle.ItemsSource = excelCollection;
                dg_StokEkle.Items.Refresh();
                CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private async void btn_excele_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (excelCollection == null)
                {
                    CRUDmessages.NoInput();
                }
                if (excelCollection.Count == 0)
                {
                    CRUDmessages.NoInput();
                }
                txt_please_wait.Visibility = Visibility.Visible;
                await Task.Run(() =>
                {

                    ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                    string filePath = string.Format("C:\\excel-c\\uretim\\{0}_{1}", "KumasDurum", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                    string sheetName = "KumasDurum";

                    filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                    FileInfo fileInfo = new FileInfo(filePath);

                    var existingPackage = new ExcelPackage(fileInfo);

                    //şablon header kısmı
                    excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                    excelWorks.SetRowHeight(existingPackage, sheetName, 5, 20);

                    excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 1);

                    excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 24);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 54);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 18);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 20);

                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:AA1000", "#E6E6E7");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:F2", "#333F4F");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:F3", "#3B495B");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:A1000", "#FFFFFF");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "F1:F1000", "#FFFFFF");

                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Kumaş Durum", "Calibri", 13, "#ffffff", true);

                    DataTable dataTable = GetDataFromCollection(excelCollection);
                    int rowCount = dataTable.Rows.Count;
                    int columnCount = dataTable.Columns.Count;

                    excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                    excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                    excelWorks.TextWrap(existingPackage, sheetName, "B6:F" + rowCount + 6, true);

                    int i = 7;
                    while (i < rowCount + 7)
                    {
                        ExcelMethodsEPP.ColorConditionCellValue(existingPackage, sheetName, "D" + i, "uretildi", "#81FFBA", "uretilmedi", "#F46868");
                        ExcelMethodsEPP.TextConditionCellValue(existingPackage, sheetName, "E" + i, "01.01.0001", "-", null, null);
                        i++;
                    }

                    excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:E6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "KumasDurum");

                });
                txt_please_wait.Visibility = Visibility.Collapsed;
                MessageBox.Show("Kumaş Durum Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Uretim> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Üretim Durum");
            dataTable.Columns.Add("Son Üretim Tarih");


            foreach (Cls_Uretim item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Üretim Durum"] = item.UretimDurum;
                    dataRow["Son Üretim Tarih"] = item.SonUretimTarih.ToString("dd.MM.yyyy");

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void btn_stok_sil(object sender, RoutedEventArgs e)
        {
            Variables.Result_ = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (Variables.Result_)
                {
                    Variables.ResultString_ = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Stok Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Stok Bilgileri Alınırken"); return; }

                    Cls_Uretim dataItem = row.Item as Cls_Uretim;

                    excelCollection.Remove(dataItem);

                    dg_StokEkle.ItemsSource = excelCollection;

                    dg_StokEkle.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        bool selectMiktarColumn = false;


    }
}

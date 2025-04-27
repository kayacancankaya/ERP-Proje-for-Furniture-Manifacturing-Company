using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_IeKalanIhtiyacResult.xaml
    /// </summary>
    public partial class Frm_IeKalanIhtiyacResult : Window
    {

        ObservableCollection<Cls_Planlama> excelCollection = new();
        public Frm_IeKalanIhtiyacResult(ObservableCollection<Cls_Planlama> plan)
        {
            InitializeComponent();
            if (plan == null)
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Listelenirken");
                this.Close();
            }
            if (plan.Count == 0)
            {
                CRUDmessages.QueryIsEmpty();
                this.Close();
            }
            dg_IeKalan.ItemsSource = plan;
            excelCollection = plan;

        }

        private async void btn_export_to_excel_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                await Task.Run(() =>
                {

                    ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                    string filePath = string.Format("C:\\excel-c\\isemri\\{0}_{1}", "IsemriKalanIhtiyac", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                    string sheetName = "IsemriKalanIhtiyac";

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
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 22, 1);

                    excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 17);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 8);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 8);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 17);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 36);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 17);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 36);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 80);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 13, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 14, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 15, 36);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 16, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 17, 80);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 18, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 19, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 20, 11);
                    excelWorks.SetColumnWidth(existingPackage, sheetName, 21, 11);

                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:U2", "#333F4F");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:U3", "#3B495B");

                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                    excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "İşemri Kalan İhtiyaç", "Calibri", 13, "#ffffff", true);

                    DataTable dataTable = GetDataFromCollection(excelCollection);
                    int rowCount = dataTable.Rows.Count;
                    int columnCount = dataTable.Columns.Count;

                    excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                    excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                    excelWorks.TextWrap(existingPackage, sheetName, "B6:U" + rowCount + 6, true);

                    //int i = 7;
                    //while (i < rowCount + 7)
                    //{
                    //    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                    //    i++;
                    //}

                    excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:U6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "IsemriKalanIhtiyac");

                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B6:G6", "#C65911");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "M6", "#C65911");
                    excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "T6:U6", "#C65911");
                });
                txt_please_wait.Visibility = Visibility.Collapsed;
                MessageBox.Show("İşemri Kalan İhtiyaç Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Planlama> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Sipariş Numarası");
            dataTable.Columns.Add("Sip Sıra");
            dataTable.Columns.Add("Sip Durum");
            dataTable.Columns.Add("Referans İşemri");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("İşemrino");
            dataTable.Columns.Add("Mamul Kodu");
            dataTable.Columns.Add("Mamul Adı");
            dataTable.Columns.Add("Üretim Durum");
            dataTable.Columns.Add("İşemri Miktar");
            dataTable.Columns.Add("Üretilen Miktar");
            dataTable.Columns.Add("Kalan Miktar");
            dataTable.Columns.Add("Takipno");
            dataTable.Columns.Add("Ham Kodu");
            dataTable.Columns.Add("Kod1");
            dataTable.Columns.Add("Ham Adı");
            dataTable.Columns.Add("Ölçü Birimi");
            dataTable.Columns.Add("Reçete Birim Miktar");
            dataTable.Columns.Add("Reçete Toplam Miktar");
            dataTable.Columns.Add("Kalan İhtiyaç");

            foreach (Cls_Planlama item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    dataRow["Sipariş Numarası"] = item.SiparisNumarasi;
                    dataRow["Sip Sıra"] = item.SiparisSira;
                    dataRow["Sip Durum"] = item.SiparisDurum;
                    dataRow["Referans İşemri"] = item.ReferansIsemrino;
                    dataRow["Ürün Kodu"] = item.UrunKodu;
                    dataRow["İşemrino"] = item.Isemrino;
                    dataRow["Mamul Kodu"] = item.MamulKodu;
                    dataRow["Mamul Adı"] = item.MamulAdi;
                    dataRow["Üretim Durum"] = item.UretimDurum;
                    dataRow["İşemri Miktar"] = item.IsemriMiktar;
                    dataRow["Üretilen Miktar"] = item.UretilenMiktar;
                    dataRow["Kalan Miktar"] = item.KalanIsemriMiktar;
                    dataRow["Takipno"] = item.TakipNo;
                    dataRow["Ham Kodu"] = item.HamKodu;
                    dataRow["Kod1"] = item.Kod1;
                    dataRow["Ham Adı"] = item.HamAdi;
                    dataRow["Ölçü Birimi"] = item.OlcuBirimi;
                    dataRow["Reçete Birim Miktar"] = item.HamIhtiyacMiktarBirim;
                    dataRow["Reçete Toplam Miktar"] = item.HamIhtiyacMiktar;
                    dataRow["Kalan İhtiyaç"] = item.HamIhtiyacMiktarKalan;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContex associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {
                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up
                }
            }

        }


        private void btn_close_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_level_down_click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                if (parentWindow.WindowState == WindowState.Maximized)
                {
                    parentWindow.WindowState = WindowState.Normal;
                    parentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                else
                {
                    parentWindow.WindowState = WindowState.Maximized;
                }
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

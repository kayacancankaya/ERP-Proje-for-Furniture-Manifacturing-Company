using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Maliyet
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Fiyatsiz_Urun.xaml
    /// </summary>
    public partial class Frm_Siparis_Fiyatsiz_Urun : Window
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
        Cls_Siparis siparis = new();
        Variables variables = new();
        ObservableCollection<Cls_Siparis> fiyatsizlarCollection = new();
        public Frm_Siparis_Fiyatsiz_Urun()
        {
            InitializeComponent(); Window_Loaded();
            fiyatsizlarCollection = siparis.PopulateFiyatsizSiparisler();
            dg_Fiyatsiz_Siparisler.ItemsSource = fiyatsizlarCollection;
        }
        private void btn_execle_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\arge\\{0}_{1}", "FiyatiOlmayanlar", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Fiyati_Olmayanlar";

                filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);

                //şablon header kısmı
                excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 50);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 1);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 7);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 39);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 29);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 59);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 10);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:F2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:F3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Sevkiyat", "Calibri", 13, "#ffffff", true);

                if (fiyatsizlarCollection.Count == 0 ||
                    fiyatsizlarCollection == null)
                { CRUDmessages.GeneralFailureMessageCustomMessage("Fiyatsız Sipariş Bulunamadı."); Mouse.OverrideCursor = null; return; }

                DataTable dataTable = GetDataFromCollection(fiyatsizlarCollection);
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, sheetName, "B6:H" + rowCount + 6, true);

                int i = 7;
                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                    i++;
                }

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:F6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "UrunAgacsiz");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Liste Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
                throw;
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Siparis> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Sipariş No");
            dataTable.Columns.Add("Sipariş Sıra");
            dataTable.Columns.Add("Cari Adı");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("Ürün Adı");

            foreach (Cls_Siparis item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Sipariş No"] = item.Fisno;
                    dataRow["Sipariş Sıra"] = item.FisSira;
                    dataRow["Sipariş No"] = item.Fisno;
                    dataRow["Cari Adı"] = item.AssociatedCari.TeslimCariAdi;
                    dataRow["Ürün Kodu"] = item.StokKodu;
                    dataRow["Ürün Adı"] = item.StokAdi;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
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

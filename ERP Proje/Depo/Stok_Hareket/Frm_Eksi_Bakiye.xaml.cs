using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Depo.Stok_Hareket
{
    /// <summary>
    /// Interaction logic for Frm_Eksi_Bakiye.xaml
    /// </summary>
    public partial class Frm_Eksi_Bakiye : Window
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
        public Frm_Eksi_Bakiye()
        {
            InitializeComponent(); Window_Loaded();
            cbx_depo_kodu.ItemsSource = depo.GetDistinctDepoKodu();
        }

        Cls_Depo depo = new();
        Variables variables = new();
        private ObservableCollection<Cls_Depo> depoCollection = new();
        private void btn_eksi_bakiye_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

                if (cbx_depo_kodu.SelectedItem == null)
                { CRUDmessages.NoInput(); return; }


                dg_eksi_bakiye_liste.ItemsSource = null;
                dg_eksi_bakiye_liste.Items.Clear();
                Mouse.OverrideCursor = Cursors.Wait;

                int depoKodu = 0;

                depoKodu = Convert.ToInt32(cbx_depo_kodu.SelectedItem);

                if (depoKodu == 0 ||
                    depoKodu == null)
                { CRUDmessages.GeneralFailureMessage("Eksi Bakiye Listelenirken"); Mouse.OverrideCursor = null; return; }

                depoCollection = depo.PopulateEksiBakiyeListesi(depoKodu);

                if (depoCollection == null)
                { CRUDmessages.GeneralFailureMessage("Eksi Bakiye Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (depoCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("Eksi Bakiye"); Mouse.OverrideCursor = null; return; }

                dg_eksi_bakiye_liste.ItemsSource = depoCollection;

                Mouse.OverrideCursor = null;
            }

            catch { CRUDmessages.GeneralFailureMessage("Eksi Bakiye Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private void btn_excele_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\depo\\{0}_{1}", "Eksi_Bakiye", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Eksi_Bakiye";

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
                excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 1);
                //sütun genişlikleri
                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 19);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 44);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 9);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:A22", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:L1", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "L1:L22", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:K2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:K3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#FFFFFF", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Depo-Eksi Bakiye", "Calibri", 13, "#FFFFFF", true);

                excelWorks.InsertImage(existingPackage, sheetName, imagePath, "logo", 11, 2, 57, 57);

                DataTable dataTable = GetDataFromCollection(depoCollection);
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, sheetName, "B6:K" + rowCount + 6, true);

                int i = 7;
                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                    i++;
                }

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:K6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#FFFFFF", "Eksi_Bakiye");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Eksi Bakiye Tablosu Excele Aktarıldı.");
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

            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Kod 1");
            dataTable.Columns.Add("Bakiye");
            dataTable.Columns.Add("10 Bakiye");
            dataTable.Columns.Add("15 Bakiye");
            dataTable.Columns.Add("30 Bakiye");
            dataTable.Columns.Add("35 Bakiye");
            dataTable.Columns.Add("40 Bakiye");
            dataTable.Columns.Add("45 Bakiye");

            foreach (Cls_Depo item in stokHareketCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Kod 1"] = item.Kod1;
                    dataRow["Bakiye"] = item.BakiyeAranan;
                    dataRow["10 Bakiye"] = item.Bakiye10;
                    dataRow["15 Bakiye"] = item.Bakiye15;
                    dataRow["30 Bakiye"] = item.Bakiye30;
                    dataRow["35 Bakiye"] = item.Bakiye35;
                    dataRow["40 Bakiye"] = item.Bakiye40;
                    dataRow["45 Bakiye"] = item.Bakiye45;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
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

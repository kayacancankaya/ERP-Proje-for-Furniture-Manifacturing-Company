using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Evrak
{
    /// <summary>
    /// Interaction logic for frm_fatura_en.xaml
    /// </summary>
    public partial class frm_fatura_en : Window
    {
        Cls_Fatura cls_fatura = new();
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
        string filePath = string.Empty;
        public frm_fatura_en(string dosyaYolu, string irsaliyeNo)
        {
            InitializeComponent(); Window_Loaded();

            Mouse.OverrideCursor = Cursors.Wait;

            filePath = dosyaYolu;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                cls_fatura.IrsaliyeNo = irsaliyeNo;
                txt_fatura_no.Text = irsaliyeNo;
                txt_irsaliye_no.Text = irsaliyeNo;

                if (cls_fatura.IrsaliyeNo.Length > 0)
                {

                    dg_Fatura.ItemsSource = null;
                    dg_Fatura.Items.Clear();
                    cls_fatura.FaturaCollection = cls_fatura.populateFaturaView();
                    dg_Fatura.ItemsSource = cls_fatura.FaturaCollection;
                    cls_fatura.getCariInfo();

                    txt_sirket_adi.Text = cls_fatura.SirketAdi;
                    txt_adres.Text = cls_fatura.Adres;
                    txt_adres2.Text = cls_fatura.Adres2;
                    txt_email.Text = cls_fatura.Email;
                    txt_tel.Text = cls_fatura.Tel;
                    txt_vergi_no.Text = cls_fatura.VergiNo;
                    txt_kap_adedi.Text = cls_fatura.ToplamKapAdetString;
                    txt_tarih_fatura.Text = cls_fatura.TarihFatura;
                    txt_toplam_tutar.Text = cls_fatura.KumulatifToplamTutar.ToString();
                    double grandTotal = cls_fatura.Navlun * cls_fatura.ToplamTutar;
                    txt_grand_total.Text = grandTotal.ToString();
                }
                else { MessageBox.Show("Lütfen İrsaliye Numarası Giriniz."); }
                Mouse.OverrideCursor = null;
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

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                ContextMenu contextMenu = dataGrid.Resources["DataGridContextMenu"] as ContextMenu;
                if (contextMenu != null)
                {
                    dataGrid.ContextMenu = contextMenu;
                }
            }
        }

        private void btn_listele(object sender, RoutedEventArgs e)
        {

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                cls_fatura.IrsaliyeNo = txt_irsaliye_no.Text;
                txt_fatura_no.Text = txt_irsaliye_no.Text;
                if (cls_fatura.IrsaliyeNo.Length > 0)
                {

                    dg_Fatura.ItemsSource = null;
                    dg_Fatura.Items.Clear();
                    cls_fatura.FaturaCollection = cls_fatura.populateFaturaView();
                    dg_Fatura.ItemsSource = cls_fatura.FaturaCollection;
                    cls_fatura.getCariInfo();

                    txt_sirket_adi.Text = cls_fatura.SirketAdi;
                    txt_adres.Text = cls_fatura.Adres;
                    txt_adres2.Text = cls_fatura.Adres2;
                    txt_email.Text = cls_fatura.Email;
                    txt_tel.Text = cls_fatura.Tel;
                    txt_vergi_no.Text = cls_fatura.VergiNo;
                    txt_kap_adedi.Text = cls_fatura.ToplamKapAdetString;
                    txt_tarih_fatura.Text = cls_fatura.TarihFatura;
                    txt_toplam_tutar.Text = cls_fatura.KumulatifToplamTutar.ToString();
                    double grandTotal = cls_fatura.Navlun * cls_fatura.ToplamTutar;
                    txt_grand_total.Text = grandTotal.ToString();
                }
                else { MessageBox.Show("Lütfen İrsaliye Numarası Giriniz."); }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }
        }
        private void btn_excel_kaydet(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string sheetName = string.Format("invoice_{0}", txt_irsaliye_no.Text);
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";

                excelWorks.CreateExcelSheet(sheetName, filePath);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);


                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 4);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 6);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 30);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 6);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 8);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 8);

                excelWorks.InsertImage(existingPackage, sheetName, imagePath, "logo", 10, 1, 57, 57);

                excelWorks.MergeAndCenterCells(existingPackage, sheetName, "A1:J1");
                excelWorks.MergeAndCenterCells(existingPackage, sheetName, "A2:J2");
                excelWorks.MergeAndCenterCells(existingPackage, sheetName, "A3:J3");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A7:C7");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A8:C8");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A9:C9");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A10:C10");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A11:C11");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A12:C12");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D8:E8");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D9:E9");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F9:G9");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F10:G10");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H9:J9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H10:J10");

                excelWorks.ShrinkToFit(existingPackage, sheetName, "D8", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "D9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F10", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "H9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "H10", true);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A1", "Vita Bianca Mobilya Teks. İnş. İth. İhr. Paz. San. Ve Tic. Ltd Şti.", "Calibri", 11, "#0000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A2", "Pancar OSB Mahallesi 5. Cadde No:2", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A3", "Torbalı/İzmir/Turkey", "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A7", "Company Name:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A8", "Adress:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A9", "", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A10", "Email:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A11", "Phone:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A12", "Trade License No:", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "D7", txt_sirket_adi.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D8", txt_adres.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D9", txt_adres2.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D10", txt_email.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D11", txt_tel.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D12", txt_vergi_no.Text, "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "F9", "Serial No:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F10", "Date:", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "H9", txt_fatura_no.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "H10", txt_tarih_fatura.Text, "Calibri", 11, "#000000", false);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A12:J12");

                int rowCount = dg_Fatura.Items.Count;

                excelWorks.SetRowHeight(existingPackage, sheetName, 14, 44);
                DataTable dataToExport = GetDataFromDataGrid(dg_Fatura);
                excelWorks.ExportDataToExcel(dataToExport, existingPackage, sheetName, 14, 1);
                excelWorks.TextWrap(existingPackage, sheetName, "A14:J14", true);

                int i = 15;
                do
                {
                    excelWorks.ShrinkToFit(existingPackage, sheetName, "A" + i + ":J" + i, true);
                    i++;
                }
                while (i < rowCount + 15);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A" + (rowCount + 15) + ":J" + (rowCount + 15));

                excelWorks.SetRightAlignIndent(existingPackage, sheetName, "C" + (rowCount + 17) + ":C" + (rowCount + 23), 0);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 17), "Origin of Goods Are:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 18), "Logistic Company:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 19), "Port of Discharge:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 20), "ContainerNo:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 21), "Total,", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 23), "Only,", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 17), "Turkey", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 18), txt_alici.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 19), txt_port.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 20), txt_container.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 21), txt_kap_adedi.Text, "Calibri", 11, "#000000", false);




                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 17) + ":J" + (rowCount + 17));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 18) + ":J" + (rowCount + 18));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 19) + ":J" + (rowCount + 19));
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 17), string.Format("TOTAL CFR USD: {0}", txt_toplam_tutar.Text), "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 18), string.Format("TRANSPORTATION COST: {0}", txt_transportation_cost.Text), "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 19), string.Format("GRAND TOTAL CFR USD {0}", txt_grand_total.Text), "Calibri", 11, "#000000", true);

                Mouse.OverrideCursor = null;

                MessageBox.Show("İngilizce Fatura Excele Aktarıldı.");

                Frm_Packing_List frm_packing_list = new Frm_Packing_List(filePath, txt_irsaliye_no.Text);
                frm_packing_list.Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }
        }
        private static DataTable GetDataFromDataGrid(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            // Define the columns for your dataTable just once here
            dataTable.Columns.Add("No");
            dataTable.Columns.Add("Qty");
            dataTable.Columns.Add("Prod. Code");
            dataTable.Columns.Add("Prod. Name");
            dataTable.Columns.Add("Desc. of Goods");
            dataTable.Columns.Add("Color");
            dataTable.Columns.Add("Shp. Type");
            dataTable.Columns.Add("Unit Prc.");
            dataTable.Columns.Add("Unit");
            dataTable.Columns.Add("Tot. Prc.");

            // Populate the DataTable with data from the DataGrid
            foreach (var item in dataGrid.Items)
            {
                var faturaItem = item as Cls_Fatura;
                if (faturaItem != null)
                {
                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_fatura to the DataTable columns
                    dataRow["No"] = faturaItem.SiraNo;
                    dataRow["Qty"] = faturaItem.AdetTakim;
                    dataRow["Prod. Code"] = faturaItem.UrunKodu;
                    dataRow["Prod. Name"] = faturaItem.UrunAdi;
                    dataRow["Desc. of Goods"] = faturaItem.DescriptionOfGoods;
                    dataRow["Color"] = faturaItem.Color;
                    dataRow["Shp. Type"] = faturaItem.GonderimSekil;
                    dataRow["Unit Prc."] = faturaItem.BirimTutar;
                    dataRow["Unit"] = faturaItem.Birim;
                    dataRow["Tot. Prc."] = faturaItem.ToplamTutar;


                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

    }
}

using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Evrak
{
    /// <summary>
    /// Interaction logic for Frm_Packing_List.xaml
    /// </summary>
    public partial class Frm_Packing_List : Window
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
        public Frm_Packing_List(string dosyaYolu, string irsaliyeNo)
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
                    cls_fatura.FaturaCollection = cls_fatura.PopulatePackingListView();
                    dg_Fatura.ItemsSource = cls_fatura.FaturaCollection;
                    cls_fatura.getCariInfo();

                    txt_sirket_adi.Text = cls_fatura.SirketAdi;
                    txt_adres.Text = cls_fatura.Adres;
                    txt_adres2.Text = cls_fatura.Adres2;
                    txt_email.Text = cls_fatura.Email;
                    txt_tel.Text = cls_fatura.Tel;
                    txt_vergi_no.Text = cls_fatura.VergiNo;


                    txt_toplam_paket.Text = cls_fatura.KumulatifToplamPaket.ToString();
                    txt_toplam_set.Text = cls_fatura.KumulatifToplamSet.ToString();
                    txt_toplam_agirlik.Text = cls_fatura.KumulatifToplamAgirlik.ToString();

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

                txt_fatura_no.Text = txt_irsaliye_no.Text;

                if (cls_fatura.IrsaliyeNo.Length > 0)
                {

                    dg_Fatura.ItemsSource = null;
                    dg_Fatura.Items.Clear();
                    cls_fatura.FaturaCollection = cls_fatura.PopulatePackingListView();
                    dg_Fatura.ItemsSource = cls_fatura.FaturaCollection;
                    cls_fatura.getCariInfo();

                    txt_sirket_adi.Text = cls_fatura.SirketAdi;
                    txt_adres.Text = cls_fatura.Adres;
                    txt_adres2.Text = cls_fatura.Adres2;
                    txt_email.Text = cls_fatura.Email;
                    txt_tel.Text = cls_fatura.Tel;
                    txt_vergi_no.Text = cls_fatura.VergiNo;


                    txt_toplam_paket.Text = cls_fatura.KumulatifToplamPaket.ToString();
                    txt_toplam_set.Text = cls_fatura.KumulatifToplamSet.ToString();
                    txt_toplam_agirlik.Text = cls_fatura.KumulatifToplamAgirlik.ToString();

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
                string sheetName = string.Format("packing_list_{0}", txt_irsaliye_no.Text);
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";

                excelWorks.CreateExcelSheet(sheetName, filePath);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);


                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 4);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 12);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 13);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 12);
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
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "A14:C14");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D9:G9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D7:G7");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D8:G8");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D9:G9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D10:G10");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D11:G11");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D12:G12");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D14:G14");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "I9:J9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "I10:J10");

                excelWorks.ShrinkToFit(existingPackage, sheetName, "D8", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "D9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "H9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "H10", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "I9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "I10", true);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A1", "Vita Bianca Mobilya Teks. İnş. İth. İhr. Paz. San. Ve Tic. Ltd Şti.", "Calibri", 11, "#0000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A2", "Pancar OSB Mahallesi 5. Cadde No:2", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A3", "Torbalı/İzmir/Turkey", "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A7", "Company Name:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A8", "Adress:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A9", "", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A10", "Email:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A11", "Phone:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A12", "Trade License No:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A14", "Container No:", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "D7", txt_sirket_adi.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D8", txt_adres.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D9", txt_adres2.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D10", txt_email.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D11", txt_tel.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D12", txt_vergi_no.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D14", txt_container_no.Text, "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "H9", "Serial No:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "H10", "Date:", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "I9", txt_fatura_no.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "I10", txt_tarih_fatura.Text, "Calibri", 11, "#000000", false);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A14:J14");

                int rowCount = dg_Fatura.Items.Count;

                excelWorks.SetRowHeight(existingPackage, sheetName, 15, 44);
                DataTable dataToExport = GetDataFromDataGrid(dg_Fatura);
                excelWorks.ExportDataToExcel(dataToExport, existingPackage, sheetName, 15, 1);
                excelWorks.TextWrap(existingPackage, sheetName, "A15:J15", true);

                int i = 16;
                do
                {
                    excelWorks.ShrinkToFit(existingPackage, sheetName, "A" + i + ":J" + i, true);
                    i++;
                }
                while (i < rowCount + 16);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A" + (rowCount + 16) + ":J" + (rowCount + 16));

                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 17) + ":H" + (rowCount + 17));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 18) + ":H" + (rowCount + 18));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 19) + ":H" + (rowCount + 19));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "I" + (rowCount + 17) + ":J" + (rowCount + 17));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "I" + (rowCount + 18) + ":J" + (rowCount + 18));
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "I" + (rowCount + 19) + ":J" + (rowCount + 19));

                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 17), "TOTAL PACKAGES:", "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 18), "TOTAL SETS", "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 19), "TOTAL GROSS WEIGHT", "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "I" + (rowCount + 17), txt_toplam_paket.Text, "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "I" + (rowCount + 18), txt_toplam_set.Text, "Calibri", 11, "#000000", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "I" + (rowCount + 19), txt_toplam_agirlik.Text, "Calibri", 11, "#000000", true);

                Mouse.OverrideCursor = null;

                MessageBox.Show("Packing List Excele Aktarıldı.");

                frm_irsaliye irsaliye = new frm_irsaliye();
                irsaliye.Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }
        }
        private static DataTable GetDataFromDataGrid(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            // Define the columns for your dataTable just once here
            dataTable.Columns.Add("No");
            dataTable.Columns.Add("Qty.");
            dataTable.Columns.Add("Unit");
            dataTable.Columns.Add("Pack.");
            dataTable.Columns.Add("Total Pack.");
            dataTable.Columns.Add("Model Code");
            dataTable.Columns.Add("Model Name");
            dataTable.Columns.Add("Desc. of Goods");
            dataTable.Columns.Add("Color");
            dataTable.Columns.Add("Total Weight (Kg)");

            // Populate the DataTable with data from the DataGrid
            foreach (var item in dataGrid.Items)
            {
                var faturaItem = item as Cls_Fatura;
                if (faturaItem != null)
                {
                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_fatura to the DataTable columns
                    dataRow["No"] = faturaItem.SiraNo;
                    dataRow["Qty."] = faturaItem.AdetTakim;
                    dataRow["Unit"] = faturaItem.UrunBirim;
                    dataRow["Pack."] = faturaItem.PaketAdet;
                    dataRow["Total Pack."] = faturaItem.PaketToplam;
                    dataRow["Model Code"] = faturaItem.UrunKodu;
                    dataRow["Model Name"] = faturaItem.UrunAdi;
                    dataRow["Desc. of Goods"] = faturaItem.DescriptionOfGoods;
                    dataRow["Color"] = faturaItem.Color;
                    dataRow["Total Weight (Kg)"] = faturaItem.Brut;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

    }
}


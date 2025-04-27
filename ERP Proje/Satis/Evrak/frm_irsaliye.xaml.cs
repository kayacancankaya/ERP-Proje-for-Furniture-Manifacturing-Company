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
    /// Interaction logic for frm_irsaliye.xaml
    /// </summary>
    public partial class frm_irsaliye : Window
    {
        cls_Irsaliye cls_irsaliye = new();
        ExcelMethodsEPP excelMethodsEPP = new();
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
        public frm_irsaliye()
        {
            InitializeComponent(); Window_Loaded();

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

        private void btn_excel_kaydet(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelCreatePapers excelPapers = new();
                string fileName = string.Format("{0}", txt_irsaliye_no.Text);
                string filePath = "C:\\excel-c\\evrak\\" + fileName;
                string sheetName = "irsaliye";
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                int rowCount = dg_Irsaliye.Items.Count;
                int startingRow = 14;
                DataTable dataToExport = GetDataFromDataGrid(dg_Irsaliye);

                filePath = CreateExcelWorkbookAndIrsaliyeSheet(filePath, sheetName, imagePath, rowCount, startingRow, dataToExport);

                Mouse.OverrideCursor = null;

                MessageBox.Show("İrsaliye Excele Aktarıldı.");

                Mouse.OverrideCursor = null;

                frm_fatura frm_Fatura = new frm_fatura(filePath, txt_irsaliye_no.Text);
                frm_Fatura.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
            }
        }

        public string CreateExcelWorkbookAndIrsaliyeSheet(string dosyaYolu, string sheetName, string imagePath, int rowCount, int startingRow, DataTable dataToExport)
        {
            string filePath = excelMethodsEPP.CreateExcelFile(dosyaYolu, sheetName);

            FileInfo fileInfo = new FileInfo(filePath);

            var existingPackage = new ExcelPackage(fileInfo);

            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 1, 4);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 2, 10);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 3, 10);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 4, 27);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 5, 14);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 6, 10);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 7, 5);
            excelMethodsEPP.SetColumnWidth(existingPackage, sheetName, 7, 10);

            excelMethodsEPP.InsertImage(existingPackage, sheetName, imagePath, "logo", 8, 1, 70, 68);

            excelMethodsEPP.MergeAndCenterCells(existingPackage, sheetName, "A1:H1");
            excelMethodsEPP.MergeAndCenterCells(existingPackage, sheetName, "A2:H2");
            excelMethodsEPP.MergeAndCenterCells(existingPackage, sheetName, "A3:H3");
            excelMethodsEPP.MergeAndLeftAlignCells(existingPackage, sheetName, "C8:D8");
            excelMethodsEPP.MergeAndLeftAlignCells(existingPackage, sheetName, "C9:D9");

            excelMethodsEPP.ShrinkToFit(existingPackage, sheetName, "C8", true);
            excelMethodsEPP.ShrinkToFit(existingPackage, sheetName, "C9", true);

            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "A1", "Vita Bianca Mobilya Teks. İnş. İth. İhr. Paz. San. Ve Tic. Ltd Şti.", "Calibri", 11, "#0000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "A2", "Pancar OSB Mahallesi 5. Cadde No:2", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "A3", "Torbalı/İzmir/Türkiye", "Calibri", 11, "#000000", false);

            excelMethodsEPP.SetRightAlignIndent(existingPackage, sheetName, "B7:B12", 0);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B7", "Şirket Adı:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B8", "Adres:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B9", "", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B10", "Email:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B11", "Tel:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B12", "Vergi No:", "Calibri", 11, "#000000", false);

            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C7", txt_sirket_adi.Text, "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C8", txt_adres.Text, "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C9", txt_adres_2.Text, "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C10", txt_email.Text, "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C11", txt_tel.Text, "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C12", txt_vergi_no.Text, "Calibri", 11, "#000000", false);


            excelMethodsEPP.SetRightAlignIndent(existingPackage, sheetName, "E7:E12", 0);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E7", "Seri No:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E8", "Sevk İrsaliyesi:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E9", "Tanzim Tarihi:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E10", "Fiili Sevk Tarihi:", "Calibri", 11, "#000000", false);

            excelMethodsEPP.AddBottomBorder(existingPackage, sheetName, "A12:H12");

            excelMethodsEPP.SetRowHeight(existingPackage, sheetName, 14, 44);
            excelMethodsEPP.ExportDataToExcel(dataToExport, existingPackage, sheetName, 14, 1);

            do
            {
                excelMethodsEPP.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + startingRow + ":F" + startingRow);
                excelMethodsEPP.ShrinkToFit(existingPackage, sheetName, "B" + startingRow + ":F" + startingRow, true);
                startingRow++;
            }
            while (startingRow < rowCount + 15);

            excelMethodsEPP.SetVerticalAlignment(existingPackage, sheetName, "D14");
            excelMethodsEPP.AddBottomBorder(existingPackage, sheetName, "A" + (rowCount + 15) + ":H" + (rowCount + 15));

            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 16), "GÜMRÜK İŞLEMLERİ YAPILMAK ÜZERE GÜMRÜK SAHASINA SEVK EDİLMİŞTİR", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 18), "Sevk Ülkesi:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 19), "Şirket Adı:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 20), "Adres:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 21), "", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 22), "Email:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "B" + (rowCount + 23), "Tel:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.SetRightAlignIndent(existingPackage, sheetName, "B" + (rowCount + 18) + ":B" + (rowCount + 23), 0);

            excelMethodsEPP.MergeAndLeftAlignCells(existingPackage, sheetName, "C" + (rowCount + 20) + ":D" + (rowCount + 20));
            excelMethodsEPP.MergeAndLeftAlignCells(existingPackage, sheetName, "C" + (rowCount + 21) + ":D" + (rowCount + 21));

            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 18), "", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 19), "ALSATTA", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 20), "Zawia Refaya st. branch from", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 21), "Ahmed Mehdawi St. Benghazi - Libya", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 22), "", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 23), "", "Calibri", 11, "#000000", false);


            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 18), "İsim:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 19), "Araç Plaka:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 20), "Tel:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 21), "TC No:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 22), "Konteyner No:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 23), "Mühür:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 24), "Teslimat Limanı:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 25), "Gemi Adı & Seferi:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.WriteTextToCell(existingPackage, sheetName, "E" + (rowCount + 26), "Yükleme Limanı:", "Calibri", 11, "#000000", false);
            excelMethodsEPP.SetRightAlignIndent(existingPackage, sheetName, "E" + (rowCount + 18) + ":F" + (rowCount + 26), 0);

            return filePath;
        }
        private static DataTable GetDataFromDataGrid(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            // Define the columns for your dataTable just once here
            dataTable.Columns.Add("Sıra No");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("Ürün Adı");
            dataTable.Columns.Add("Malın Cinsi / Rengi");
            dataTable.Columns.Add("  ");
            dataTable.Columns.Add("   ");
            dataTable.Columns.Add("Miktar");
            dataTable.Columns.Add("Birim");

            // Populate the DataTable with data from the DataGrid
            foreach (var item in dataGrid.Items)
            {
                var irsaliyeItem = item as cls_Irsaliye;
                if (irsaliyeItem != null)
                {
                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Sıra No"] = irsaliyeItem.SiraNo;
                    dataRow["Ürün Kodu"] = irsaliyeItem.UrunKodu;
                    dataRow["Ürün Adı"] = irsaliyeItem.UrunAdi;
                    dataRow["  "] = "";
                    dataRow["   "] = "";
                    dataRow["Malın Cinsi / Rengi"] = irsaliyeItem.MalinCinsiRengi;
                    dataRow["Miktar"] = irsaliyeItem.Miktar;
                    dataRow["Birim"] = irsaliyeItem.Birim;


                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }


        private void btn_listele(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string irsaliyeNo = txt_irsaliye_no.Text;
            if (irsaliyeNo.Length > 0)
            {

                dg_Irsaliye.ItemsSource = null;
                dg_Irsaliye.Items.Clear();
                cls_irsaliye.IrsaliyeCollection = cls_irsaliye.populateIrsaliyeView(irsaliyeNo);
                dg_Irsaliye.ItemsSource = cls_irsaliye.IrsaliyeCollection;
                cls_irsaliye.getCariInfo();

                txt_sirket_adi.Text = cls_irsaliye.SirketAdi;
                txt_adres.Text = cls_irsaliye.Adres;
                txt_adres_2.Text = cls_irsaliye.Adres;
                txt_adres2.Text = cls_irsaliye.Adres2;
                txt_adres_2_2.Text = cls_irsaliye.Adres2;
                txt_email.Text = cls_irsaliye.Email;
                txt_email2.Text = cls_irsaliye.Email;
                txt_tel.Text = cls_irsaliye.Tel;
                txt_tel2.Text = cls_irsaliye.Tel;
                txt_vergi_no.Text = cls_irsaliye.VergiNo;

            }
            else { MessageBox.Show("Lütfen İrsaliye Numarası Giriniz."); }
            Mouse.OverrideCursor = null;
        }

    }
}

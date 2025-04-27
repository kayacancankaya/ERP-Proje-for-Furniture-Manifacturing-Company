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
    /// Interaction logic for frm_fatura.xaml
    /// </summary>
    public partial class frm_fatura : Window
    {

        Cls_Fatura cls_fatura = new Cls_Fatura();

        string filePath = string.Empty;
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
        public frm_fatura(string dosyaYolu, string irsaliyeNo)
        {
            InitializeComponent(); Window_Loaded();

            filePath = dosyaYolu;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                txt_irsaliye_no.Text = irsaliyeNo;
                txt_irsaliye_no_text.Text = irsaliyeNo;
                cls_fatura.IrsaliyeNo = irsaliyeNo;
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
                    txt_sevk_ulkesi.Text = cls_fatura.SevkUlkesi;
                    txt_kap_adedi.Text = cls_fatura.KapAdet.ToString();
                    //txt_tutar_yalniz.Text = cls_fatura.ToplamTutarYalniz;
                    txt_toplam_tutar_string.Text = cls_fatura.ToplamTutarMetin;
                    txt_brut_net.Text = cls_fatura.BrutNet;
                    txt_tarih_fatura.Text = cls_fatura.TarihFatura;
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
                txt_irsaliye_no_text.Text = txt_irsaliye_no.Text;
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
                    txt_sevk_ulkesi.Text = cls_fatura.SevkUlkesi;
                    txt_kap_adedi.Text = cls_fatura.KapAdet.ToString();
                    //txt_tutar_yalniz.Text = cls_fatura.ToplamTutarYalniz;
                    txt_toplam_tutar_string.Text = cls_fatura.ToplamTutarMetin;
                    txt_brut_net.Text = cls_fatura.BrutNet;
                    txt_tarih_fatura.Text = cls_fatura.TarihFatura;
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
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string sheetName = string.Format("fatura_{0}", txt_irsaliye_no.Text);
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";

                excelWorks.CreateExcelSheet(sheetName, filePath);


                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 4);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 6);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 40);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 6);
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
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "F7:G7");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "F8:G8");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "F9:G9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "F10:G10");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H7:J7");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H8:J8");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H9:J9");
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "H10:J10");

                excelWorks.ShrinkToFit(existingPackage, sheetName, "D8", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "D9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F7", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F8", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F9", true);
                excelWorks.ShrinkToFit(existingPackage, sheetName, "F10", true);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A1", "Vita Bianca Mobilya Teks. İnş. İth. İhr. Paz. San. Ve Tic. Ltd Şti.", "Calibri", 11, "#0000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A2", "Pancar OSB Mahallesi 5. Cadde No:2", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A3", "Torbalı/İzmir/Türkiye", "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "A7", "Şirket Adı:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A8", "Adres:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A9", "", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A10", "Email:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A11", "Tel:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "A12", "Vergi No:", "Calibri", 11, "#000000", false);

                excelWorks.WriteTextToCell(existingPackage, sheetName, "D7", txt_sirket_adi.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D8", txt_adres.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D9", txt_adres2.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D10", txt_email.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D11", txt_tel.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D12", txt_vergi_no.Text, "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "F8", "İrsaliye No:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F9", "Tanzim Tarih:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F10", "Fiili Sevk Tarihi:", "Calibri", 11, "#000000", false);


                excelWorks.WriteTextToCell(existingPackage, sheetName, "H8", txt_irsaliye_no_text.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "H9", txt_tarih_fatura.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "H10", txt_tarih_sevk.Text, "Calibri", 11, "#000000", false);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A12:J12");

                int rowCount = dg_Fatura.Items.Count;

                excelWorks.SetRowHeight(existingPackage, sheetName, 14, 44);
                DataTable dataToExport = GetDataFromDataGrid(dg_Fatura);
                excelWorks.ExportDataToExcel(dataToExport, existingPackage, sheetName, 14, 1);
                excelWorks.TextWrap(existingPackage, sheetName, "A14:J14", true);

                int i = 15;
                do
                {
                    excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "E" + i + ":F" + i);
                    excelWorks.ShrinkToFit(existingPackage, sheetName, "A" + i + ":I" + i, true);
                    i++;
                }
                while (i < rowCount + 15);

                excelWorks.AddBottomBorder(existingPackage, sheetName, "A" + (rowCount + 15) + ":J" + (rowCount + 15));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 24) + ":E" + (rowCount + 24));

                excelWorks.SetRightAlignIndent(existingPackage, sheetName, "C" + (rowCount + 17) + ":C" + (rowCount + 26), 0);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 17), "Kap Adedi:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 18), "Brüt / Net:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 19), "Ödeme Şekli:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 20), "Banka:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 21), "İmalatçı:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 22), "Tutar:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 23), "Alıcı (Konsinye):", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 24), "Adres:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 25), "Navlun:", "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "C" + (rowCount + 26), "Sevk Ülkesi:", "Calibri", 11, "#000000", false);
                excelWorks.SetRightAlignIndent(existingPackage, sheetName, "C" + (rowCount + 18) + ":C" + (rowCount + 23), 0);


                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 19) + ":J" + (rowCount + 19));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 20) + ":J" + (rowCount + 20));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 21) + ":J" + (rowCount + 21));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 22) + ":J" + (rowCount + 22));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 23) + ":J" + (rowCount + 23));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 24) + ":J" + (rowCount + 24));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 25) + ":J" + (rowCount + 25));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 26) + ":J" + (rowCount + 26));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 27) + ":J" + (rowCount + 27));
                excelWorks.MergeAndLeftAlignCells(existingPackage, sheetName, "D" + (rowCount + 28) + ":J" + (rowCount + 28));

                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 17), txt_kap_adedi.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 18), txt_brut_net.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 19), txt_odeme_sekli.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 20), txt_banka.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 21), txt_imalatci.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 22), txt_tutar_yalniz.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 23), txt_alici.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 24), txt_alici_adres.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 25), txt_navlun.Text, "Calibri", 11, "#000000", false);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "D" + (rowCount + 26), txt_sevk_ulkesi.Text, "Calibri", 11, "#000000", false);

                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F" + (rowCount + 17) + ":J" + (rowCount + 17));
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F" + (rowCount + 17), txt_toplam_tutar_string.Text, "Calibri", 11, "#000000", true);

                Mouse.OverrideCursor = null;

                MessageBox.Show("Fatura Excele Aktarıldı.");

                frm_fatura_en frm_Fatura_En = new frm_fatura_en(filePath, txt_irsaliye_no.Text);
                frm_Fatura_En.Show();
                this.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }

        }
        private static DataTable GetDataFromDataGrid(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            // Define the columns for your dataTable just once here
            dataTable.Columns.Add("Sıra No");
            dataTable.Columns.Add("Adet Takım");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("Ürün Adı");
            dataTable.Columns.Add("Malın Cinsi / Rengi");
            dataTable.Columns.Add("   ");
            dataTable.Columns.Add("Gön. Tipi");
            dataTable.Columns.Add("Parça Tutarı");
            dataTable.Columns.Add("Birim");
            dataTable.Columns.Add("Toplam Tutar");

            // Populate the DataTable with data from the DataGrid
            foreach (var item in dataGrid.Items)
            {
                var faturaItem = item as Cls_Fatura;
                if (faturaItem != null)
                {
                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_fatura to the DataTable columns
                    dataRow["Sıra No"] = faturaItem.SiraNo;
                    dataRow["Adet Takım"] = faturaItem.AdetTakim;
                    dataRow["Ürün Kodu"] = faturaItem.UrunKodu;
                    dataRow["Ürün Adı"] = faturaItem.UrunAdi;
                    dataRow["Malın Cinsi / Rengi"] = faturaItem.MalinCinsiRengi;
                    dataRow["   "] = "";
                    dataRow["Gön. Tipi"] = faturaItem.GonderimSekil;
                    dataRow["Parça Tutarı"] = faturaItem.BirimTutar;
                    dataRow["Birim"] = faturaItem.Birim;
                    dataRow["Toplam Tutar"] = faturaItem.ToplamTutar;


                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void btn_degisiklik_kaydet(object sender, RoutedEventArgs e)
        {

        }
        private void menu_item_clicked(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = sender as MenuItem;

            if (clickedItem != null)
            {
                string formName = clickedItem.Tag as string;

                if (!string.IsNullOrEmpty(formName))
                {
                    // Construct the form name using your naming convention
                    string fullFormName = "SablonCalismalari.View." + formName;

                    // Use reflection to create an instance of the form
                    Type formType = Type.GetType(fullFormName);
                    if (formType != null)
                    {
                        var form = Activator.CreateInstance(formType) as Window;
                        form?.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}

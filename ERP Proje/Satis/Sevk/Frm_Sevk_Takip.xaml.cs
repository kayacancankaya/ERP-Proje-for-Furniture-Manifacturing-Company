using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Sevk.Popups;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk
{
    /// <summary>
    /// Interaction logic for Frm_Sevk_Takip.xaml
    /// </summary>
    public partial class Frm_Sevk_Takip : Window
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
        public Frm_Sevk_Takip()
        {
            InitializeComponent(); Window_Loaded();
            dg_SevkRaporGenel.ItemsSource = cariReportCollection;
        }

        Variables variables = new();
        Cls_Sevk sevk = new();
        ObservableCollection<Cls_Sevk> cariReportCollection = new();
        ObservableCollection<Cls_Sevk> siparisReportCollection = new();
        ObservableCollection<Cls_Sevk> excelReportCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_sevk_no.Text))
                    restrictionPairs.Add("@sevkNo", txt_sevk_no.Text);

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);

                if (!string.IsNullOrWhiteSpace(txt_cari_adi.Text))
                    restrictionPairs.Add("@cariAdi", txt_cari_adi.Text);

                if (string.IsNullOrEmpty(txt_sevk_baslangic_tarih.SelectedDate?.ToString()) ||
                    string.IsNullOrEmpty(txt_sevk_bitis_tarih.SelectedDate?.ToString()))
                { CRUDmessages.GeneralFailureMessage("Sevk Tarihleri Alınırken"); return; }

                restrictionPairs.Add("@baslangicTarih", txt_sevk_baslangic_tarih.SelectedDate?.ToString("yyyy-MM-dd"));
                restrictionPairs.Add("@bitisTarih", txt_sevk_bitis_tarih.SelectedDate?.ToString("yyyy-MM-dd"));

                cariReportCollection = sevk.PopulateCariReportCollectionForShipment(restrictionPairs);

                if (!cariReportCollection.Any())
                { CRUDmessages.GeneralFailureMessage("Cari Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                dg_SevkRaporGenel.ItemsSource = cariReportCollection;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Cari Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }

        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Sevk item = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);
                siparisReportCollection = sevk.PopulateSiparisReportCollectionForShipment(restrictionPairs,
                                                item.SevkEmriNo);
                if (!siparisReportCollection.Any())
                { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                Popup_Sevk_Takip_Siparis _popup = new(siparisReportCollection, restrictionPairs);
                _popup.ShowDialog();
                cariReportCollection = sevk.PopulateCariReportCollectionForShipment(restrictionPairs);
                dg_SevkRaporGenel.ItemsSource = cariReportCollection;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }
        }

        private void excel_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Sevk item = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\sevk\\{0}_{1}", item.SevkEmriNo, DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Sevkiyat";

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

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 17);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 26);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 17);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 7);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 7);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:I2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:I3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Sevkiyat", "Calibri", 13, "#ffffff", true);

                string simdikiZaman = DateTime.Now.ToString("dd.MM.yyyy");
                excelReportCollection = sevk.GetExcelReportCollectionForUrunToplama(item.SevkEmriNo);
                var sevkEmriNo = excelReportCollection.Select(item => item.SevkEmriNo).FirstOrDefault();
                var teslimCariKodu = excelReportCollection.Select(item => item.CariKodu).FirstOrDefault();
                var teslimCariAdi = excelReportCollection.Select(item => item.CariAdi).FirstOrDefault();

                var sevkAciklama = excelReportCollection.Select(item => item.SevkAciklama).FirstOrDefault();

                string cariAciklama = string.Empty;
                if (string.IsNullOrEmpty(sevkAciklama))
                { cariAciklama = string.Format("{0}({1})", teslimCariAdi, teslimCariKodu); }
                else
                {
                    cariAciklama = string.Format("{0}({1})-{2}", teslimCariAdi, teslimCariKodu, sevkAciklama);
                }

                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F2:H2");
                excelWorks.MergeAndRightAlignCells(existingPackage, sheetName, "F3:H3");
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F2", simdikiZaman, "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "F3", sevkEmriNo, "Calibri", 13, "#ffffff", true);

                excelWorks.VerticalAlignBottom(existingPackage, sheetName, "B5");
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B5", cariAciklama, "Calibri", 13, "#000000", true);

                DataTable dataTable = GetDataFromCollection(excelReportCollection);
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

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:H6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "Sevk");

                cariReportCollection = sevk.PopulateCariReportCollectionForShipment(restrictionPairs);

                Mouse.OverrideCursor = null;

                MessageBox.Show("Sevk Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
            }
        }

        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Sevk> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Tarih");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("Ürün Adı");
            dataTable.Columns.Add("Paket Kodu");
            dataTable.Columns.Add("Paket Adı");
            dataTable.Columns.Add("İngilizce İsim");
            dataTable.Columns.Add("Miktar");

            foreach (Cls_Sevk item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Tarih"] = item.SevkTarihi;
                    dataRow["Ürün Kodu"] = item.UrunKodu;
                    dataRow["Ürün Adı"] = item.UrunAdi;
                    dataRow["Paket Kodu"] = item.PaketKodu;
                    dataRow["Paket Adı"] = item.PaketAdi;
                    dataRow["İngilizce İsim"] = item.IngilizceIsim;
                    dataRow["Miktar"] = item.SevkMiktar;

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

using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Rapor.xaml
    /// </summary>
    public partial class Frm_Siparis_Rapor : Window
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
        public Frm_Siparis_Rapor()
        {
            InitializeComponent(); Window_Loaded();
        }

        Variables variables = new();
        Cls_Siparis siparis = new();
        ObservableCollection<Cls_Siparis> siparisReportCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);
                if (!string.IsNullOrWhiteSpace(txt_siparis_sira.Text))
                    restrictionPairs.Add("@siparisSira", txt_siparis_sira.Text);
                if (!string.IsNullOrWhiteSpace(txt_cari_adi.Text))
                    restrictionPairs.Add("@cariAdi", txt_cari_adi.Text);

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);
                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);

                if (!string.IsNullOrWhiteSpace(txt_destinasyon.Text))
                    restrictionPairs.Add("@destinasyon", txt_destinasyon.Text);
                if (!string.IsNullOrWhiteSpace(txt_po_no.Text))
                    restrictionPairs.Add("@poNumarasi", txt_po_no.Text);
                queryRestrictions = string.Empty;
                if (cb_kapali_siparis.IsChecked == false)
                    queryRestrictions = queryRestrictions + " and [Sip Durum] <> 'K' ";
                if (cb_teslim_edilen_siparis.IsChecked == false)
                    queryRestrictions = queryRestrictions + " and [Sip Miktar] > [Yük Mik] ";
                siparisReportCollection = siparis.PopulateOrderReportCollection(restrictionPairs, queryRestrictions);
                if (siparisReportCollection == null)
                { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                if (siparisReportCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                dg_SiparisSecim.ItemsSource = siparisReportCollection;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }

        private void btn_export_to_excel_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\siparis\\{0}_{1}", "SiparisRapor", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "SiparisRapor";

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
                excelWorks.SetColumnWidth(existingPackage, sheetName, 14, 1);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 8);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 8);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 23);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 37);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 13, 5);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:M2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:M3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Sipariş Rapor", "Calibri", 13, "#ffffff", true);

                DataTable dataTable = GetDataFromCollection(siparisReportCollection);
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, sheetName, "B6:M" + rowCount + 6, true);

                int i = 7;
                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                    i++;
                }

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:M6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "Siparis");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Rapor Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Siparis> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Sipariş Numarası");
            dataTable.Columns.Add("Sip Sıra");
            dataTable.Columns.Add("Sip Durum");
            dataTable.Columns.Add("Teslim Cari");
            dataTable.Columns.Add("Destinasyon");
            dataTable.Columns.Add("PO No");
            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Sip Mik");
            dataTable.Columns.Add("Yük Mik");
            dataTable.Columns.Add("Ürs Mik");
            dataTable.Columns.Add("Depo Mik");

            foreach (Cls_Siparis item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    dataRow["Sipariş Numarası"] = item.Fisno;
                    dataRow["Sip Sıra"] = item.FisSira;
                    dataRow["Sip Durum"] = item.SiparisDurum;
                    dataRow["Teslim Cari"] = item.AssociatedCari.TeslimCariAdi;
                    dataRow["Destinasyon"] = item.Destinasyon;
                    dataRow["PO No"] = item.POnumarasi;
                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Sip Mik"] = item.SiparisMiktar;
                    dataRow["Yük Mik"] = item.SiparisTeslimMiktar;
                    dataRow["Ürs Mik"] = item.UretilenMiktar;
                    dataRow["Depo Mik"] = item.DepoMiktar;

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


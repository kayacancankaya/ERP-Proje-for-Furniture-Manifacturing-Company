using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Ahsap.Planlama
{
    /// <summary>
    /// Interaction logic for Frm_Urun_Agacina_Bagli_Olmayan_Siparisler.xaml
    /// </summary>
    public partial class Frm_Urun_Agacina_Bagli_Olmayan_Siparisler : Window
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
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        ObservableCollection<Cls_Siparis> urunAgacinaBagliOlmayanlarCollection = new();
        public Frm_Urun_Agacina_Bagli_Olmayan_Siparisler()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                InitializeComponent(); Window_Loaded();
                ObservableCollection<Cls_Siparis> firstColl = siparis.PopulateUrunAgaciOlmayanSiparisler(1);

                urunAgacinaBagliOlmayanlarCollection = firstColl;

                if (urunAgacinaBagliOlmayanlarCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (urunAgacinaBagliOlmayanlarCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                totalResult = siparis.CountUrunAgaciOlmayanSiparisler(1);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;
                Mouse.OverrideCursor = null;

                dg_Urun_Agaci_Olmayan_Siparisler.ItemsSource = urunAgacinaBagliOlmayanlarCollection;

                Mouse.OverrideCursor = null;

            }

            catch
            {
                CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken"); txt_result.Visibility = Visibility.Collapsed;

                Mouse.OverrideCursor = null;
            }
        }
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                lastOffset = e.NewValue;
                if (lastOffset > pageValueChanged + 100 && totalResult > urunAgacinaBagliOlmayanlarCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Siparis> moreSiparisCollection = new();
                    moreSiparisCollection = siparis.PopulateUrunAgaciOlmayanSiparisler(pageNumber);
                    if (moreSiparisCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Siparişler Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreSiparisCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Siparis> lastTalepCollection = new ObservableCollection<Cls_Siparis>
                                        (urunAgacinaBagliOlmayanlarCollection.Union(moreSiparisCollection).ToList());
                        dg_Urun_Agaci_Olmayan_Siparisler.ItemsSource = lastTalepCollection;
                        dg_Urun_Agaci_Olmayan_Siparisler.Items.Refresh();
                        urunAgacinaBagliOlmayanlarCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_Urun_Agaci_Olmayan_Siparisler, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }

                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                    lastOffset += e.Delta * -1 / 48;
                else
                    lastOffset -= e.Delta / 48;

                if (lastOffset > pageValueChanged + 100 && totalResult > urunAgacinaBagliOlmayanlarCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Siparis> moreTalepCollection = new();
                    moreTalepCollection = siparis.PopulateUrunAgaciOlmayanSiparisler(pageNumber);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Siparişler Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Siparis> lastTalepCollection = new ObservableCollection<Cls_Siparis>
                                        (urunAgacinaBagliOlmayanlarCollection.Union(moreTalepCollection).ToList());
                        dg_Urun_Agaci_Olmayan_Siparisler.ItemsSource = lastTalepCollection;
                        dg_Urun_Agaci_Olmayan_Siparisler.Items.Refresh();
                        urunAgacinaBagliOlmayanlarCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_Urun_Agaci_Olmayan_Siparisler, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void btn_execle_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\plan\\{0}_{1}", "UrunAgaciOlmayanlar", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Urun_Agaci_Olmayanlar";

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
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:H2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:H3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Sevkiyat", "Calibri", 13, "#ffffff", true);

                if (urunAgacinaBagliOlmayanlarCollection.Count == 0 ||
                    urunAgacinaBagliOlmayanlarCollection == null)
                { CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Ağacına Bağlı Olmayan Sipariş Bulunamadı."); Mouse.OverrideCursor = null; return; }

                DataTable dataTable = GetDataFromCollection(urunAgacinaBagliOlmayanlarCollection);
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

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:H6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "UrunAgacsiz");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Liste Excele Aktarıldı.");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
                Mouse.OverrideCursor = null;
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
            dataTable.Columns.Add("Sipariş Tarih");
            dataTable.Columns.Add("Teslim Tarih");

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
                    dataRow["Sipariş Tarih"] = item.SiparisTarih;
                    dataRow["Teslim Tarih"] = item.TerminTarih.ToString("yyyy-MM-dd");

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

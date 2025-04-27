using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Moduler.Yardimci
{
    /// <summary>
    /// Interaction logic for Frm_Urun_Agacina_Bagli_Olmayan.xaml
    /// </summary>
    public partial class Frm_Urun_Agacina_Bagli_Olmayan : Window
    {
        Cls_Arge arge = new();
        Dictionary<string, string> restrictionPairs = new();
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        ObservableCollection<Cls_Arge> bagliOlmayanlarCollection = new();
        LoginLogic login = new();
        Variables variables = new();
        public Frm_Urun_Agacina_Bagli_Olmayan()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod_1.ItemsSource = arge.GetDistinctKod1()?.ToList();
            cbx_kod_2.ItemsSource = arge.GetDistinctKod2()?.ToList();
            cbx_kod_3.ItemsSource = arge.GetDistinctKod3()?.ToList();
            cbx_kod_4.ItemsSource = arge.GetDistinctKod4()?.ToList();
            cbx_kod_5.ItemsSource = arge.GetDistinctKod5()?.ToList();
        }
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
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                    restrictionPairs.Add("stokKodu", txt_stok_kodu.Text);
                else
                    restrictionPairs.Add("stokKodu", null);

                if (cbx_kod_1.SelectedItem != null)
                    restrictionPairs.Add("kod1", cbx_kod_1.SelectedItem.ToString());
                else
                    restrictionPairs.Add("kod1", null);

                if (cbx_kod_2.SelectedItem != null)
                    restrictionPairs.Add("kod2", cbx_kod_2.SelectedItem.ToString());
                else
                    restrictionPairs.Add("kod2", null);

                if (cbx_kod_3.SelectedItem != null)
                    restrictionPairs.Add("kod3", cbx_kod_3.SelectedItem.ToString());
                else
                    restrictionPairs.Add("kod3", null);

                if (cbx_kod_4.SelectedItem != null)
                    restrictionPairs.Add("kod4", cbx_kod_4.SelectedItem.ToString());
                else
                    restrictionPairs.Add("kod4", null);

                if (cbx_kod_5.SelectedItem != null)
                    restrictionPairs.Add("kod5", cbx_kod_4.SelectedItem.ToString());
                else
                    restrictionPairs.Add("kod5", null);

                ObservableCollection<Cls_Arge> firstColl = arge.PopulateUrunAgacinaBagliOlmayanMalzemelerList(restrictionPairs, 1);

                bagliOlmayanlarCollection = firstColl;

                if (bagliOlmayanlarCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Stoklar Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (bagliOlmayanlarCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                totalResult = arge.CountUrunAgacinaBagliOlmayanMalzemelerList(restrictionPairs, 1);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;
                Mouse.OverrideCursor = null;

                dg_BagliOlmayan.ItemsSource = bagliOlmayanlarCollection;

                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stoklar Listelenirken"); txt_result.Visibility = Visibility.Collapsed;

                Mouse.OverrideCursor = null;
            }
        }
        private void btn_excele_aktar_clicked(object sender, EventArgs e)
        {
            try
            {
                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();

                if (dg_BagliOlmayan.Items.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aktarılacak Stok Bulunamadı.");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                Variables.FileName = string.Format("UrunAgacinaBagliOlmayanlar_{0}_{1}", login.GetUserName(), DateTime.Now.ToString("yyyyMMddHHmmss"));
                Variables.FilePath = "C:\\excel-c\\plan\\" + Variables.FileName;
                Variables.SheetName = "urun_agacina_bagli_olmayanlar";

                Variables.FilePath = excelWorks.CreateExcelFile(Variables.FilePath, Variables.SheetName);

                FileInfo fileInfo = new FileInfo(Variables.FilePath);

                var existingPackage = new ExcelPackage(fileInfo);

                //şablon header kısmı
                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 1, 6);
                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 2, 25);
                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 3, 19);
                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 4, 6);
                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 5, 50);

                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 1, 1);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 9, 1);

                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 2, 17);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 3, 26);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 4, 17);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 5, 17);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 6, 17);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 7, 17);
                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 8, 17);

                excelWorks.SetCellBackgroundColor(existingPackage, Variables.SheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, Variables.SheetName, "B2:H2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, Variables.SheetName, "B3:H3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, "B3", "Ürün Ağacına Bağlı Olmayanlar", "Calibri", 13, "#ffffff", true);

                DataTable dataTable = GetDataFromCollection(bagliOlmayanlarCollection);
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                excelWorks.ExportDataToExcel(dataTable, existingPackage, Variables.SheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, Variables.SheetName, string.Format("B6:H{0}", rowCount + 6), true);

                int i = 7;
                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, Variables.SheetName, i, 50);
                    i++;
                }

                excelWorks.CreateStyledTable(existingPackage, Variables.SheetName, "B6:H6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "Sevk");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Stok Bilgileri Excele Aktarıldı.");

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excele Aktarılırken");
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Arge> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Kod 1");
            dataTable.Columns.Add("Kod 2");
            dataTable.Columns.Add("Kod 3");
            dataTable.Columns.Add("Kod 4");
            dataTable.Columns.Add("Kod 5");

            foreach (Cls_Arge item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Kod 1"] = item.Kod1;
                    dataRow["Kod 2"] = item.Kod2;
                    dataRow["Kod 3"] = item.Kod3;
                    dataRow["Kod 4"] = item.Kod4;
                    dataRow["Kod 5"] = item.Kod5;

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
        private void btn_stok_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                string stokKodu = string.Empty;

                Frm_Stok_Karti_Rehberi frm = new();
                var result = frm.ShowDialog();
                if (result == true)
                {
                    stokKodu = frm.SelectedStokKodu;
                }


                if (clickedButton.Parent is StackPanel stackPanel)
                {
                    foreach (UIElement element in stackPanel.Children)
                    {
                        if (element is TextBox textBox)
                        {
                            textBox.Text = stokKodu;
                        }
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Stok Rehberi Açılırken");
            }
        }
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                lastOffset = e.NewValue;
                if (lastOffset > pageValueChanged + 100 && totalResult > bagliOlmayanlarCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Arge> moreSiparisCollection = new();
                    moreSiparisCollection = arge.PopulateUrunAgacinaBagliOlmayanMalzemelerList(restrictionPairs, pageNumber);
                    if (moreSiparisCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreSiparisCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Arge> lastTalepCollection = new ObservableCollection<Cls_Arge>
                                        (bagliOlmayanlarCollection.Union(moreSiparisCollection).ToList());
                        dg_BagliOlmayan.ItemsSource = lastTalepCollection;
                        dg_BagliOlmayan.Items.Refresh();
                        bagliOlmayanlarCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_BagliOlmayan, 0) as Decorator;
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

                if (lastOffset > pageValueChanged + 100 && totalResult > bagliOlmayanlarCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Arge> moreTalepCollection = new();
                    moreTalepCollection = arge.PopulateUrunAgacinaBagliOlmayanMalzemelerList(restrictionPairs, pageNumber);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Arge> lastTalepCollection = new ObservableCollection<Cls_Arge>
                                        (bagliOlmayanlarCollection.Union(moreTalepCollection).ToList());
                        dg_BagliOlmayan.ItemsSource = lastTalepCollection;
                        dg_BagliOlmayan.Items.Refresh();
                        bagliOlmayanlarCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_BagliOlmayan, 0) as Decorator;
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

        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

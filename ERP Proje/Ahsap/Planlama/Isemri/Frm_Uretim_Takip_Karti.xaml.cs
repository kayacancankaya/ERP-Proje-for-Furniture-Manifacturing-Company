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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Layer_UI.Ahsap.Planlama.Isemri
{
    /// <summary>
    /// Interaction logic for Frm_Uretim_Takip_Karti.xaml
    /// </summary>
    public partial class Frm_Uretim_Takip_Karti : Window
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
        public Frm_Uretim_Takip_Karti()
        {
            InitializeComponent(); Window_Loaded();
        }
        public ObservableCollection<Cls_Isemri> SiparisCollection { get; set; }
        public ObservableCollection<Cls_Isemri> IsemriCollection { get; set; }
        public ObservableCollection<Cls_Isemri> TakipKartiCollection { get; set; }
        public ObservableCollection<Cls_Isemri> HamMaddeCollection { get; set; }


        Variables variables = new();
        Cls_Isemri isemri = new();
        ObservableCollection<Cls_Isemri> isemriCollection = new();
        ObservableCollection<Cls_Isemri> bildirimCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_siparis_no.Text) &&
                    string.IsNullOrWhiteSpace(txt_isemrino.Text) &&
                    string.IsNullOrWhiteSpace(txt_stok_kodu.Text) &&
                    string.IsNullOrWhiteSpace(txt_stok_adi.Text) &&
                    string.IsNullOrWhiteSpace(txt_cari_adi.Text))
                { CRUDmessages.NoInput(); return; }


                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);

                if (!string.IsNullOrWhiteSpace(txt_isemrino.Text))
                    restrictionPairs.Add("@isemrino", txt_isemrino.Text);


                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);

                if (!string.IsNullOrWhiteSpace(txt_cari_adi.Text))
                    restrictionPairs.Add("@cariAdi", txt_cari_adi.Text);

                isemriCollection = isemri.PopulateIsemriBildirimList(restrictionPairs);

                if (isemriCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
                }
                if (!isemriCollection.Any())
                { CRUDmessages.GeneralFailureMessageCustomMessage("Listelenecek İşemri Bulunamadı."); Mouse.OverrideCursor = null; return; }

                dg_SiparisSecim.ItemsSource = isemriCollection;
                stack_panel_takip_karti.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }
        private async void btn_takip_karti_bas_clicked(object sender, RoutedEventArgs e)
        {
            variables.WarningMessage = "Takip Kartları Açılıyor.\n Lütfen Bekleyiniz.";
            Frm_Wait waitForm = new(variables.WarningMessage);
            try
            {

                IsemriCollection = new();
                variables.Counter = 0;
                foreach (Cls_Isemri item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        if (item.BildirilecekIsemriMiktar > item.KALAN_IE_MIKTAR)
                        { CRUDmessages.GeneralFailureMessageCustomMessage("Bildirilecek İşemri Miktarı Kalan İşemri Miktarından Büyük Olamaz."); return; }

                        if (item.BildirilecekIsemriMiktar == 0)
                        { CRUDmessages.GeneralFailureMessage("Gönderilecek Miktar 0 Olamaz."); return; }

                        IsemriCollection.Add(item);
                        variables.Counter++;
                    }

                }
                if (variables.Counter == 0)
                { CRUDmessages.NoInput(); return; }

                waitForm.Show();

                bildirimCollection = isemri.GetUretimTakipKartiCollection(IsemriCollection);

                if (bildirimCollection == null)
                { CRUDmessages.GeneralFailureMessage("Alt İşemirlerine Ulaşılırken"); waitForm.Close(); return; }
                if (bildirimCollection.Count == 0)
                { CRUDmessages.GeneralFailureMessageCustomMessage("Alt İşemirlerine Ulaşılamadı."); waitForm.Close(); return; }

                await Task.Run(() =>
                {
                    ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();

                    var distinctSiparisNo = IsemriCollection.Select(s => s.SIPARIS_NO).Distinct().ToList();
                    var distinctSiparisSatirStok = IsemriCollection.Select(s => new { SiparisNo = s.SIPARIS_NO, SiparisSira = s.SIPARIS_SIRA, UrunKodu = s.URUN_KODU }).Distinct().ToList();
                    ExcelPackage existingPackage = null;

                    //çalışma kitabını ve sayfalarını kaydet
                    foreach (var siparis in distinctSiparisNo)
                    {

                        Variables.FilePath = string.Format("C:\\excel-c\\plan\\{0}_{1}", "Uretim_Takip_Karti", siparis);

                        if (!File.Exists(Variables.FilePath + "_1.xlsx"))
                        {
                            variables.Counter = 0;
                            foreach (var item in distinctSiparisSatirStok.Where(s => s.SiparisNo == siparis))
                            {
                                if (variables.Counter == 0)
                                {
                                    Variables.SheetName = string.Format("{0}_{1}_{2}", item.SiparisNo, item.SiparisSira, item.UrunKodu);
                                    Variables.FilePath = excelWorks.CreateExcelFile(Variables.FilePath, Variables.SheetName);

                                    FileInfo fileInfo = new FileInfo(Variables.FilePath);
                                    existingPackage = new ExcelPackage(fileInfo);

                                    excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 1, 3);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 1, 15);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 2, 26);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 3, 10);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 4, 26);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 5, 9);
                                    excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 6, 9);

                                    variables.Counter++;
                                    continue;
                                }

                                Variables.SheetName = string.Format("{0}_{1}_{2}", item.SiparisNo, item.SiparisSira, item.UrunKodu);


                                variables.Result = excelWorks.CreateExcelSheetIfDoesntExists(existingPackage, Variables.FilePath, Variables.SheetName);

                                if (!variables.Result)
                                { return; }

                                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 1, 3);
                                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 1, 15);
                                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 2, 71);

                                variables.Counter++;
                            }

                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(Variables.FilePath + "_1.xlsx");
                            existingPackage = new ExcelPackage(fileInfo);


                            foreach (var item in distinctSiparisSatirStok.Where(s => s.SiparisNo == siparis))
                            {
                                Variables.SheetName = string.Format("{0}_{1}_{2}", item.SiparisNo, item.SiparisSira, item.UrunKodu);

                                variables.Result = excelWorks.CreateExcelSheetIfDoesntExists(existingPackage, Variables.FilePath + "_1.xlsx", Variables.SheetName);
                                if (!variables.Result)
                                { return; }

                                excelWorks.SetRowHeight(existingPackage, Variables.SheetName, 1, 3);
                                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 1, 15);
                                excelWorks.SetColumnWidth(existingPackage, Variables.SheetName, 2, 71);
                            }
                        }
                    }
                    //her sipariş satır için üretim takip kartı oluştur               
                    foreach (var isemri in IsemriCollection)
                    {
                        variables.Counter = 0;
                        //bildirilecek iş emri miktarı 50 den fazla ise sadece 1 adet değilse miktar kadar etiket oluştur
                        if (isemri.BildirilecekIsemriMiktar <= 50)
                        {
                            do
                            {
                                Variables.FilePath = string.Format("C:\\excel-c\\plan\\{0}_{1}", "Uretim_Takip_Karti", isemri.SIPARIS_NO);
                                Variables.SheetName = string.Format("{0}_{1}_{2}", isemri.SIPARIS_NO, isemri.SIPARIS_SIRA, isemri.URUN_KODU);
                                FileInfo fileInfo = new FileInfo(Variables.FilePath + "_1.xlsx");
                                existingPackage = new ExcelPackage(fileInfo);
                                ExcelWorksheet worksheet = existingPackage.Workbook.Worksheets[Variables.SheetName];
                                string firstBlankCell = excelWorks.GetFirstBlankCell(existingPackage, Variables.SheetName, "A2");
                                int currentRow = worksheet.Cells[firstBlankCell].Start.Row;

                                if (string.IsNullOrEmpty(firstBlankCell))
                                    return;

                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow), "Sipariş No:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 1), "Sipariş Sıra:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 2), "Cari Adı:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 3), "Açıklama:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 4), "Ürün Kodu:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 5), "Ürün Adı:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 6), "İşemrino:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 7), "İşemri Miktar:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 8), "Sıra:", "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}:A{1}", currentRow + 9, currentRow + 13), "Boş:", "Calibri", 11, "#ffffff", false);

                                excelWorks.RightAlignCells(existingPackage, Variables.SheetName, string.Format("A{0}:A{1}", currentRow, currentRow + 8));

                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow), isemri.SIPARIS_NO, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 1), isemri.SIPARIS_SIRA.ToString(), "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 2), isemri.CARI_ADI, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 3), isemri.SIPARIS_GENEL_ACIKLAMA, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 4), isemri.URUN_KODU, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 5), isemri.URUN_ADI, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 6), isemri.ISEMRINO, "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 7), isemri.BildirilecekIsemriMiktar.ToString(), "Calibri", 11, "#000000", true);
                                excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 8), isemri.BildirilecekIsemriMiktar.ToString() + "/" + isemri.BildirilecekIsemriMiktar.ToString(), "Calibri", 11, "#000000", true);

                                excelWorks.ShrinkToFit(existingPackage, Variables.SheetName, string.Format("B{0}:B{1}", currentRow, currentRow + 8), true);

                                variables.Counter++;
                            }
                            while (variables.Counter <= isemri.BildirilecekIsemriMiktar);
                        }

                        else
                        {
                            Variables.FilePath = string.Format("C:\\excel-c\\plan\\{0}_{1}", "Uretim_Takip_Karti", isemri.SIPARIS_NO);
                            Variables.SheetName = string.Format("{0}_{1}_{2}", isemri.SIPARIS_NO, isemri.SIPARIS_SIRA, isemri.URUN_KODU);
                            FileInfo fileInfo = new FileInfo(Variables.FilePath + "_1.xlsx");
                            existingPackage = new ExcelPackage(fileInfo);
                            ExcelWorksheet worksheet = existingPackage.Workbook.Worksheets[Variables.SheetName];
                            string firstBlankCell = excelWorks.GetFirstBlankCell(existingPackage, Variables.SheetName, "A2");
                            int currentRow = worksheet.Cells[firstBlankCell].Start.Row;

                            if (string.IsNullOrEmpty(firstBlankCell))
                                return;

                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow), "Sipariş No:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 1), "Sipariş Sıra:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 2), "Cari Adı:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 3), "Açıklama:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 4), "Ürün Kodu:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 5), "Ürün Adı:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 6), "İşemrino:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 7), "İşemri Miktar:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}", currentRow + 8), "Sıra:", "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("A{0}:A{1}", currentRow + 9, currentRow + 13), "Boş:", "Calibri", 11, "#ffffff", false);

                            excelWorks.RightAlignCells(existingPackage, Variables.SheetName, string.Format("A{0}:A{1}", currentRow, currentRow + 8));

                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow), isemri.SIPARIS_NO, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 1), isemri.SIPARIS_SIRA.ToString(), "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 2), isemri.CARI_ADI, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 3), isemri.SIPARIS_GENEL_ACIKLAMA, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 4), isemri.URUN_KODU, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 5), isemri.URUN_ADI, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 6), isemri.ISEMRINO, "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 7), isemri.BildirilecekIsemriMiktar.ToString(), "Calibri", 11, "#000000", true);
                            excelWorks.WriteTextToCell(existingPackage, Variables.SheetName, string.Format("B{0}", currentRow + 8), isemri.BildirilecekIsemriMiktar.ToString() + "/" + isemri.BildirilecekIsemriMiktar.ToString(), "Calibri", 11, "#000000", true);

                            excelWorks.ShrinkToFit(existingPackage, Variables.SheetName, string.Format("B{0}:B{1}", currentRow, currentRow + 8), true);

                        }

                    }
                });


                switch (variables.ResultInt)
                {
                    case 1:
                        CRUDmessages.GeneralSuccessMessage("Excele Aktarım ");
                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessage("Excel İşlemleri Yapılırken");
                        break;
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Veri Tabanına Kayıt Yapılırken");
                        break;
                }

                waitForm.Close();
                Frm_Uretim_Takip_Karti _frm = new();
                _frm.Show();
                this.Close();
                MessageBox.Show("Üretim Takip Kartı Excele Aktarıldı");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Kaydedilirken");
                waitForm.Close();
            }
        }

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Isemri item in dg_SiparisSecim.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private bool selectMiktarColumn = false;
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Gönderilecek İşemri Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
            }
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContex associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {
                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up
                }
            }

        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {

                DataGridCellInfo cellInfo = dg_SiparisSecim.CurrentCell;

                if (cellInfo.Column.DisplayIndex != null)
                {
                    if (cellInfo.Column.DisplayIndex == 6)
                    {

                        var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);


                        if (cellContent is TextBox textBox)
                        {
                            // Clear the text inside the cell
                            textBox.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
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

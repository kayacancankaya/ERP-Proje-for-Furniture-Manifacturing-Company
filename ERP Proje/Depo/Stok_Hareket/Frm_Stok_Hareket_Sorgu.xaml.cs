using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Satis.Evrak;
using Layer_UI.UserControls;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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

namespace Layer_UI.Depo.Stok_Hareket
{
    /// <summary>
    /// Interaction logic for Frm_Stok_Hareket_Sorgu.xaml
    /// </summary>
    public partial class Frm_Stok_Hareket_Sorgu : Window
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
        Cls_Depo depo = new();
        Variables variables = new();
        private ObservableCollection<Cls_Depo> depoNoCollection = new();
        List<int> depoKoduConst = new();
        private ObservableCollection<Cls_Depo> kod1Collection = new();
        private List<string> kod1Const = new();
        private ObservableCollection<Cls_Depo> harTipCollection = new();
        private List<string> harTipConst = new();
        private ObservableCollection<Cls_Depo> belTipCollection = new();
        private List<string> belTipConst = new();
        private ObservableCollection<Cls_Depo> fatTipCollection = new();
        private List<string> fatTipConst = new();
        private ObservableCollection<Cls_Depo> depoCollection = new();
        private ObservableCollection<Cls_Depo> stokHareketCollection = new();
        public Dictionary<string, string> filterDic = new();
        int pageNumber = 1;
        double lastOffset = 0;
        bool isPageUp = false;
        int totalResult = 0;
        LoginLogic login = new();
        string fabrika = string.Empty;
        string departman = string.Empty;

        public Frm_Stok_Hareket_Sorgu()
        {
            InitializeComponent(); Window_Loaded();
            Mouse.OverrideCursor = Cursors.Wait;
            depoNoCollection = depo.GetDepoKoduForListBox();
            kod1Collection = depo.GetKod1ForListView();
            FilterPopupLstDepo.ItemsSource = depoNoCollection;
            FilterPopupLstKod1.ItemsSource = kod1Collection;
            fabrika = login.GetFabrika();
            departman = login.GetDepartment();
            if(departman.Contains("Planlama") ||
                departman.Contains("Yonetim") ||
                departman.Contains("Bilgi"))
            {
                txt_fabrika.Visibility = Visibility.Visible;
                cbx_fabrika.Visibility = Visibility.Visible;
            }
            if(string.IsNullOrEmpty(fabrika))
            {
                CRUDmessages.GeneralFailureMessage("Fabrika Bulunamadı");
                Mouse.OverrideCursor = null;
                return;
            }
            if(fabrika == "Vita")
                cbx_fabrika.SelectedIndex = 0;
            else
                cbx_fabrika.SelectedIndex=1;
            InitializeTypes();
            stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika,depoKoduConst,kod1Const,harTipConst,belTipConst,fatTipConst);
            totalResult = depo.CountStokHareketList(filterDic,1,fabrika,depoKoduConst,kod1Const,harTipConst,belTipConst,fatTipConst);
            dg_stok_hareket.ItemsSource = stokHareketCollection;
            ShowResult(stokHareketCollection.Count,totalResult);

            lastOffset = 0;
            cbx_fabrika.SelectionChanged += cbx_departman_selection_changed;
            Mouse.OverrideCursor = null;
        }
        private void ShowResult(int currentResult,int totalResult)
        {
            try
            {
                if(currentResult == 0) 
                    txt_result.Text = string.Empty;
                else if(currentResult >= totalResult)
                    txt_result.Text = string.Format("Toplam {0} Sonuçtan {1} Adet Listeleniyor.",currentResult,currentResult);
                else if(currentResult < totalResult)
                    txt_result.Text = string.Format("Toplam {0} Sonuçtan {1} Adet Listeleniyor.",totalResult,currentResult);
                else
                    txt_result.Text = string.Empty;
                
            }
            catch 
            {
                txt_result.Text = string.Empty;
                return ;
            }
        }
        private void btn_excele_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\depo\\{0}_{1}", "Stok_Hareket", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Stok_Hareket";

                filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);

                //şablon header kısmı
                excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 25);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 19, 1);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 7);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 25);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 35);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 6);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 6);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 7);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 11);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 11);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 21);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 13, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 14, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 15, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 16, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 17, 5);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 18, 5);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:A22", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:S1", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "S1:S22", "#FFFFFF");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:R2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:R3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#FFFFFF", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Depo-Stok Hareket Rapor", "Calibri", 13, "#FFFFFF", true);

                excelWorks.InsertImage(existingPackage, sheetName, imagePath, "logo", 17, 2, 57, 57);

                DataTable dataTable = GetDataFromCollection(stokHareketCollection);
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, sheetName, "B6:R" + rowCount + 6, true);

                int i = 7;
                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);
                    i++;
                }

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:R6", "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#FFFFFF", "Stok_Hareket");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Stok Hareket Tablosu Excele Aktarıldı.");
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

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Stok Kodu");
            dataTable.Columns.Add("Stok Adı");
            dataTable.Columns.Add("Fiş No");
            dataTable.Columns.Add("G-C Mik");
            dataTable.Columns.Add("G-C Kod");
            dataTable.Columns.Add("Depo Kodu");
            dataTable.Columns.Add("Tarih");
            dataTable.Columns.Add("Açıklama");
            dataTable.Columns.Add("Ekalan");
            dataTable.Columns.Add("Takip No");
            dataTable.Columns.Add("Sipariş Numarası");
            dataTable.Columns.Add("Sip Sıra");
            dataTable.Columns.Add("Kod1");
            dataTable.Columns.Add("Har Tür");
            dataTable.Columns.Add("Bel Tip");
            dataTable.Columns.Add("Fat Tip");

            foreach (Cls_Depo item in stokHareketCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    // Map the properties of cls_Irsaliye to the DataTable columns
                    dataRow["ID"] = item.HareketID;
                    dataRow["Stok Kodu"] = item.StokKodu;
                    dataRow["Stok Adı"] = item.StokAdi;
                    dataRow["Fiş No"] = item.FisNo;
                    dataRow["G-C Mik"] = item.HareketMiktar;
                    dataRow["G-C Kod"] = item.GirisCikisKod;
                    dataRow["Depo Kodu"] = item.DepoKodu;
                    dataRow["Tarih"] = item.HareketTarih;
                    dataRow["Açıklama"] = item.HareketAciklama;
                    dataRow["Ekalan"] = item.Ekalan;
                    dataRow["Takip No"] = item.TakipNo;
                    dataRow["Sipariş Numarası"] = item.SiparisNumarasi;
                    dataRow["Sip Sıra"] = item.SiparisSira;
                    dataRow["Kod1"] = item.Kod1;
                    dataRow["Har Tür"] = item.HareketTipiKodu;
                    dataRow["Bel Tip"] = item.BelgeTipiKodu;
                    dataRow["Fat Tip"] = item.FaturaTipiKodu;

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

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button == null) 
                    return;
                if (string.IsNullOrEmpty(button.Tag.ToString()))
                    return;
                switch (button.Tag.ToString())
                {
                    case "Stok Kodu":
                        FilterPopupStokKodu.IsOpen = true;
                        txt_header_stok_kodu.Text = button.Tag.ToString();
                        break;
                    case "Stok Adı":
                        FilterPopupStokAdi.IsOpen = true;
                        txt_header_stok_adi.Text = button.Tag.ToString();
                        break;
                    case "Fiş No":
                        FilterPopupFisno.IsOpen = true;
                        txt_header_fisno.Text = button.Tag.ToString();
                        break;
                    case "G-C":
                        FilterPopupGC.IsOpen = true;
                        txt_header_gc.Text = button.Tag.ToString();
                        break;
                    case "Depo Kodu":
                        FilterPopupDepoKodu.IsOpen = true;
                        txt_header_depo_kodu.Text = button.Tag.ToString();
                        break;
                    case "Tarih":
                        FilterPopupTarih.IsOpen = true;
                        txt_header_tarih.Text = button.Tag.ToString();
                        break;
                    case "Açıklama":
                        FilterPopupAciklama.IsOpen = true;
                        txt_header_aciklama.Text = button.Tag.ToString();
                        break;
                    case "Ekalan":
                        FilterPopupEkalan.IsOpen = true;
                        txt_header_ekalan.Text = button.Tag.ToString();
                        break;
                    case "Takip No":
                        FilterPopupTakipNo.IsOpen = true;
                        txt_header_takip_no.Text = button.Tag.ToString();
                        break;
                    case "Sipariş Numarası":
                        FilterPopupSiparisNumarasi.IsOpen = true;
                        txt_header_siparis_numarasi.Text = button.Tag.ToString();
                        break;
                    case "Sipariş Sıra":
                        FilterPopupSiparisSira.IsOpen = true;
                        txt_header_siparis_sira.Text = button.Tag.ToString();
                        break;
                    case "Kod 1":
                        FilterPopupKod1.IsOpen = true;
                        txt_header_kod1.Text = button.Tag.ToString();
                        break;
                    case "Hareket Tipi":
                        FilterPopupLstHareketTipi.ItemsSource = harTipCollection;
                        FilterPopupHareketTipi.IsOpen = true;
                        txt_header_hareket_tipi.Text = button.Tag.ToString();

                        break;
                    case "Belge Tipi":
                        FilterPopupLstBelgeTipi.ItemsSource = belTipCollection;
                        FilterPopupBelgeTipi.IsOpen = true;
                        txt_header_belge_tipi.Text = button.Tag.ToString();

                        

                        break;
                    case "Fatura Tipi":

                        FilterPopupLstFaturaTipi.ItemsSource = fatTipCollection;
                        FilterPopupFaturaTipi.IsOpen = true;

                        txt_header_fatura_tipi.Text = button.Tag.ToString();
                        break;
                }
            }
            catch 
            {
                return;
            }
        }
        string filterText = string.Empty;
        string filterHeader = string.Empty;
        private void Filter(object sender)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    Mouse.OverrideCursor = null;
                    return;
                }

                switch (textBox.Name.ToString())
                {
                    case "FilterTextBox_stok_kodu":
                        filterText = FilterTextBox_stok_kodu.Text;
                        filterHeader = txt_header_stok_kodu.Text;
                        FilterPopupStokKodu.IsOpen = false;
                        break;
                    case "FilterTextBox_stok_adi":
                        filterText = FilterTextBox_stok_adi.Text;
                        filterHeader = txt_header_stok_adi.Text;
                        FilterPopupStokAdi.IsOpen = false;
                        break;
                    case "FilterTextBox_fisno":
                        filterText = FilterTextBox_fisno.Text;
                        filterHeader = txt_header_fisno.Text;
                        FilterPopupFisno.IsOpen = false;
                        break;
                    case "FilterTextBox_aciklama":
                        filterText = FilterTextBox_aciklama.Text;
                        filterHeader = txt_header_aciklama.Text;
                        FilterPopupFisno.IsOpen = false;
                        break;
                    case "FilterTextBox_ekalan":
                        filterText = FilterTextBox_ekalan.Text;
                        filterHeader = txt_header_ekalan.Text;
                        FilterPopupEkalan.IsOpen = false;
                        break;
                    case "FilterTextBox_takip_no":
                        filterText = FilterTextBox_takip_no.Text;
                        filterHeader = txt_header_takip_no.Text;
                        FilterPopupTakipNo.IsOpen = false;
                        break;
                    case "FilterTextBox_siparis_numarasi":
                        filterText = FilterTextBox_siparis_numarasi.Text;
                        filterHeader = txt_header_siparis_numarasi.Text;
                        FilterPopupSiparisNumarasi.IsOpen = false;
                        break;
                    case "FilterTextBox_siparis_sira":
                        filterText = FilterTextBox_siparis_sira.Text;
                        filterHeader = txt_header_siparis_sira.Text;
                        FilterPopupSiparisSira.IsOpen = false;
                        break;
                }
            }
            else
            {
                Mouse.OverrideCursor = null;
                return;
            }

            if (!string.IsNullOrEmpty(filterText))
            {
                //daha önceden filtre varsa filtreyi değiştir
                if (filterDic.ContainsKey(filterHeader))
                    filterDic[filterHeader] = filterText;
                //yoksa ekle
                else
                    filterDic.Add(filterHeader, filterText);

            }
            else
            {
                //eğer boş bir şekilde entera vurulmuşsa ve daha önceden filtre varsa filtreyi kaldır
                if (filterDic.ContainsKey(filterHeader))
                    filterDic.Remove(filterHeader);
                //daha önce bir şey yoksa sorgulamadan direk çık
                else
                {
                    Mouse.OverrideCursor = null;
                    return;
                }
            }
            pageNumber = 1;
            if (filterDic == null)
                filterDic = new();

            stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
            totalResult = depo.CountStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
            dg_stok_hareket.ItemsSource = stokHareketCollection;
            ShowResult(stokHareketCollection.Count, totalResult);

            Mouse.OverrideCursor = null;
        }
        private void btn_popup_filter_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if(btn == null) return;
                if(string.IsNullOrEmpty(btn.Tag.ToString())) return;
                Mouse.OverrideCursor = Cursors.Wait;
                filterText = string.Empty;
                filterHeader = string.Empty;
                switch (btn.Tag.ToString())
                {
                    case "FilterPopupGC":
                        ComboBoxItem comboBoxItem = FilterComboBox_gc.SelectedItem as ComboBoxItem;
                        if (comboBoxItem == null)
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Giriş Çıkış Kodu Seçiniz");
                            Mouse.OverrideCursor = null;
                            return;
                        }
                        filterText = comboBoxItem.Content.ToString();
                        filterHeader = txt_header_gc.Text;
                        FilterPopupGC.IsOpen = false;
                        break;
                    case "FilterPopupDepoKodu":
                        FilterPopupDepoKodu.IsOpen = false;
                        break;
                    case "FilterPopupkod1":
                        FilterPopupKod1.IsOpen = false;
                        break;


                }
                if (!string.IsNullOrEmpty(filterText))
                {
                    //daha önceden filtre varsa filtreyi değiştir
                    if (filterDic.ContainsKey(filterHeader))
                        filterDic[filterHeader] = filterText;
                    //yoksa ekle
                    else
                        filterDic.Add(filterHeader, filterText);

                }
                else
                {
                    //eğer boş bir şekilde entera vurulmuşsa ve daha önceden filtre varsa filtreyi kaldır
                    if (filterDic.ContainsKey(filterHeader))
                        filterDic.Remove(filterHeader);
                    //daha önce bir şey yoksa sorgulamadan direk çık
                    else
                    {
                        if(btn.Tag.ToString() != "FilterPopupDepoKodu" &&
                            btn.Tag.ToString() != "FilterPopupkod1")
                        { 
                        Mouse.OverrideCursor = null;
                        return;
                        }
                    }
                }
                pageNumber = 1;
                if (filterDic == null)
                    filterDic = new();

                stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                totalResult = depo.CountStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                dg_stok_hareket.ItemsSource = stokHareketCollection;
                ShowResult(stokHareketCollection.Count, totalResult);
                lastOffset = 0;

                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_popup_filter_tarih_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (filterDic == null)
                    filterDic = new();
                if (filterDic.ContainsKey("BaslangicTarih"))
                    filterDic.Remove("BaslangicTarih");
                if (filterDic.ContainsKey("BitisTarih"))
                    filterDic.Remove("BitisTarih");
                Variables.Counter_ = 0;
                if (dp_baslangic_tarih != null)
                {
                    if (dp_baslangic_tarih.SelectedDate != null)
                    {
                        if (dp_baslangic_tarih.SelectedDate != DateTime.MinValue)
                        {
                            if (DateTime.TryParse(dp_baslangic_tarih.SelectedDate.ToString(), out DateTime baslangicTarih))
                            {
                                if (filterDic == null)
                                    filterDic = new();
                                ComboBoxItem selectedItem = FilterComboBox_baslangic_tarih.SelectedItem as ComboBoxItem;
                                if (selectedItem != null)
                                {
                                    string date = string.Format("{0};{1}", selectedItem.Content.ToString(), dp_baslangic_tarih.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty);
                                    filterDic.Add("BaslangicTarih", date);
                                    Variables.Counter_++;
                                }
                            }
                        }
                    }
                }
                if (dp_bitis_tarih != null)
                {
                    if (dp_bitis_tarih.SelectedDate != null)
                    {
                        if (dp_bitis_tarih.SelectedDate != DateTime.MinValue)
                        {
                            if (DateTime.TryParse(dp_bitis_tarih.SelectedDate.ToString(), out DateTime bitisTarih))
                            {
                                if (filterDic == null)
                                    filterDic = new();
                                ComboBoxItem selectedItem = FilterComboBox_bitis_tarih.SelectedItem as ComboBoxItem;
                                if (selectedItem != null)
                                {
                                    string date = string.Format("{0};{1}", selectedItem.Content.ToString(), dp_bitis_tarih.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty);
                                    filterDic.Add("BitisTarih", date);
                                    Variables.Counter_++;
                                }
                            }
                        }
                    }
                }
                if(Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;
                pageNumber = 1;
                FilterPopupTarih.IsOpen = false;
                stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                totalResult = depo.CountStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                dg_stok_hareket.ItemsSource = stokHareketCollection;
                ShowResult(stokHareketCollection.Count, totalResult);

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Filtereleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter)
                    return;
                Filter(sender);
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    Filter(sender);

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_clear_filter_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FilterTextBox_stok_kodu.Clear();
                FilterTextBox_stok_adi.Clear();
                FilterTextBox_fisno.Clear();
                FilterTextBox_ekalan.Clear();
                FilterTextBox_aciklama.Clear();
                FilterTextBox_siparis_numarasi.Clear();
                FilterTextBox_siparis_sira.Clear();
                FilterTextBox_takip_no.Clear();
                foreach (Cls_Depo item in FilterPopupLstBelgeTipi.Items)
                    item.IsChecked = false;
                foreach (Cls_Depo item in FilterPopupLstHareketTipi.Items)
                    item.IsChecked = false;
                foreach (Cls_Depo item in FilterPopupLstFaturaTipi.Items)
                    item.IsChecked = false;
                foreach (Cls_Depo item in FilterPopupLstKod1.Items)
                    item.IsChecked = false;
                foreach (Cls_Depo item in FilterPopupLstDepo.Items)
                    item.IsChecked = false;

                dp_baslangic_tarih.SelectedDate = null;
                dp_bitis_tarih.SelectedDate = null;

                filterDic.Clear();
                depoKoduConst.Clear();
                harTipConst.Clear();
                belTipConst.Clear();
                fatTipConst.Clear();
                kod1Const.Clear();

                stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                totalResult = depo.CountStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                dg_stok_hareket.ItemsSource = stokHareketCollection;
                ShowResult(stokHareketCollection.Count, totalResult);
                lastOffset = 0;
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void cbx_departman_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboBoxItem = cbx_fabrika.SelectedItem as ComboBoxItem;
                if (comboBoxItem == null)
                    return;
                fabrika = comboBoxItem.Content.ToString();
                pageNumber = 1;
                if (filterDic == null)
                    filterDic = new();

                Mouse.OverrideCursor = Cursors.Wait;
                stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                totalResult = depo.CountStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                dg_stok_hareket.ItemsSource = null;
                dg_stok_hareket.ItemsSource = stokHareketCollection;
                ShowResult(stokHareketCollection.Count, totalResult);
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Fabrika Değiştirilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox == null)
                    return;

                Cls_Depo item = checkBox.DataContext as Cls_Depo; if (item == null) return;
                switch (checkBox.Name)
                {
                    case "cx_depo_kodu":
                        if (checkBox.IsChecked == true)
                        {
                            if (depoKoduConst != null)
                            {
                                if (!depoKoduConst.Contains(item.DepoKodu))
                                    depoKoduConst.Add(item.DepoKodu);
                            }
                        }
                        if (checkBox.IsChecked == false)
                        {
                            if (depoKoduConst != null)
                            {
                                if (depoKoduConst.Contains(item.DepoKodu))
                                    depoKoduConst.Remove(item.DepoKodu);
                            }
                        }
                        break;
                    case "cx_kod1":
                        if (checkBox.IsChecked == true)
                        {
                            if (kod1Const != null)
                            {
                                if (!kod1Const.Contains(item.Kod1))
                                    kod1Const.Add(item.Kod1);
                            }
                        }
                        if (checkBox.IsChecked == false)
                        {
                            if (kod1Const != null)
                            {
                                if (kod1Const.Contains(item.Kod1))
                                    kod1Const.Remove(item.Kod1);
                            }
                        }
                        break;
                    case "cx_hareket_tipi":
                        if (checkBox.IsChecked == true)
                        {
                            if (harTipConst != null)
                            {
                                if (!harTipConst.Contains(item.HareketTipiKodu))
                                    harTipConst.Add(item.HareketTipiKodu);
                            }
                        }
                        if (checkBox.IsChecked == false)
                        {
                            if (harTipConst != null)
                            {
                                if (harTipConst.Contains(item.HareketTipiKodu))
                                    harTipConst.Remove(item.HareketTipiKodu);
                            }
                        }
                        break;
                    case "cx_belge_tipi":
                        if (checkBox.IsChecked == true)
                        {
                            if (belTipConst != null)
                            {
                                if (!belTipConst.Contains(item.BelgeTipiKodu))
                                    belTipConst.Add(item.BelgeTipiKodu);
                            }
                        }
                        if (checkBox.IsChecked == false)
                        {
                            if (belTipConst != null)
                            {
                                if (belTipConst.Contains(item.BelgeTipiKodu))
                                    belTipConst.Remove(item.BelgeTipiKodu);
                            }
                        }
                        break;
                    case "cx_fatura_tipi":
                        if (checkBox.IsChecked == true)
                        {
                            if (fatTipConst != null)
                            {
                                if (!fatTipConst.Contains(item.FaturaTipiKodu))
                                    fatTipConst.Add(item.FaturaTipiKodu);
                            }
                        }
                        if (checkBox.IsChecked == false)
                        {
                            if (fatTipConst != null)
                            {
                                if (fatTipConst.Contains(item.FaturaTipiKodu))
                                    fatTipConst.Remove(item.FaturaTipiKodu);
                            }
                        }
                        break;
                }

            }
            catch
            {
                return;
            }
        }
        private void InitializeTypes()
        {
            try
            {
                
                harTipCollection = new();
                Cls_Depo depo = new();
                depo.HareketTipi = "Devir";
                depo.HareketTipiKodu = "A";
                depo.IsChecked = false;
                harTipCollection.Add(depo);

                Cls_Depo depo1 = new();
                depo1.HareketTipi = "Fatura";
                depo1.HareketTipiKodu = "J";
                depo1.IsChecked = false;
                harTipCollection.Add(depo1);

                Cls_Depo depo2 = new();
                depo2.HareketTipi = "İrsaliye";
                depo2.HareketTipiKodu = "H";
                depo2.IsChecked = false;
                harTipCollection.Add(depo2);

                Cls_Depo depo3 = new();
                depo3.HareketTipi = "DAT";
                depo3.HareketTipiKodu = "B";
                depo3.IsChecked = false;
                harTipCollection.Add(depo3);

                Cls_Depo depo4 = new();
                depo4.HareketTipi = "Üretim";
                depo4.HareketTipiKodu = "C";
                depo4.IsChecked = false;
                harTipCollection.Add(depo4);

                Cls_Depo depo5 = new();
                depo5.HareketTipi = "Faturalaşmış İrsaliye";
                depo5.HareketTipiKodu = "N";
                depo5.IsChecked = false;
                harTipCollection.Add(depo5);

                Cls_Depo depo19 = new();
                depo19.HareketTipi = "İade";
                depo19.HareketTipiKodu = "L";
                depo19.IsChecked = false;
                harTipCollection.Add(depo19);

                FilterPopupLstHareketTipi.ItemsSource = harTipCollection;

                belTipCollection = new();
                Cls_Depo depo6 = new();
                depo6.BelgeTipi = "Fatura";
                depo6.BelgeTipiKodu = "F";
                depo6.IsChecked = false;
                belTipCollection.Add(depo6);

                Cls_Depo depo7 = new();
                depo7.BelgeTipi = "İrsaliye";
                depo7.BelgeTipiKodu = "I";
                depo7.IsChecked = false;
                belTipCollection.Add(depo7);

                Cls_Depo depo8 = new();
                depo8.BelgeTipi = "Üretim Giriş";
                depo8.BelgeTipiKodu = "U";
                depo8.IsChecked = false;
                belTipCollection.Add(depo8);

                Cls_Depo depo9 = new();
                depo9.BelgeTipi = "Üretim Çıkış";
                depo9.BelgeTipiKodu = "V";
                depo9.IsChecked = false;
                belTipCollection.Add(depo9);

                FilterPopupLstBelgeTipi.ItemsSource = belTipCollection;
                fatTipCollection = new();

                Cls_Depo depo10 = new();
                depo10.FaturaTipi = "Satış Faturası";
                depo10.FaturaTipiKodu = "1";
                depo10.IsChecked = false;
                fatTipCollection.Add(depo10);

                Cls_Depo depo11 = new();
                depo11.FaturaTipi = "Alış Faturası";
                depo11.FaturaTipiKodu = "2";
                depo11.IsChecked = false;
                fatTipCollection.Add(depo11);

                Cls_Depo depo12 = new();
                depo12.FaturaTipi = "Satış İrsaliyesi";
                depo12.FaturaTipiKodu = "3";
                depo12.IsChecked = false;
                fatTipCollection.Add(depo12);

                Cls_Depo depo13 = new();
                depo13.FaturaTipi = "Alış İrsaliyesi";
                depo13.FaturaTipiKodu = "4";
                depo13.IsChecked = false;
                fatTipCollection.Add(depo13);

                Cls_Depo depo14 = new();
                depo14.FaturaTipi = "Müşteri Siparişi";
                depo14.FaturaTipiKodu = "6";
                depo14.IsChecked = false;
                fatTipCollection.Add(depo14);

                Cls_Depo depo15 = new();
                depo15.FaturaTipi = "Satıcı Siparişi";
                depo15.FaturaTipiKodu = "7";
                depo15.IsChecked = false;
                fatTipCollection.Add(depo15);

                Cls_Depo depo16 = new();
                depo16.FaturaTipi = "DAT Giriş";
                depo16.FaturaTipiKodu = "8";
                depo16.IsChecked = false;
                fatTipCollection.Add(depo16);

                Cls_Depo depo17 = new();
                depo17.FaturaTipi = "DAT Çıkış";
                depo17.FaturaTipiKodu = "9";
                depo17.IsChecked = false;
                fatTipCollection.Add(depo17);

                Cls_Depo depo18 = new();
                depo18.FaturaTipi = "Saklanmış İrsaliye";
                depo18.FaturaTipiKodu = "A";
                depo18.IsChecked = false;
                fatTipCollection.Add(depo18);
                FilterPopupLstFaturaTipi.ItemsSource = fatTipCollection;

            }
            catch 
            {
                return;
            }
        }
        private void btn_depo_kodu_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;
                
                foreach(Cls_Depo item in FilterPopupLstDepo.Items) 
                    item.IsChecked = false;
                depoKoduConst.Clear();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void btn_kod1_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;
                
                foreach(Cls_Depo item in FilterPopupLstKod1.Items) 
                    item.IsChecked = false;
                kod1Const.Clear();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void btn_hartip_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;
                
                foreach(Cls_Depo item in FilterPopupLstHareketTipi.Items) 
                    item.IsChecked = false;
                harTipConst.Clear();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void btn_beltip_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;
                
                foreach(Cls_Depo item in FilterPopupLstBelgeTipi.Items) 
                    item.IsChecked = false;
                belTipConst.Clear();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void btn_fattip_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;
                
                foreach(Cls_Depo item in FilterPopupLstFaturaTipi.Items) 
                    item.IsChecked = false;
                fatTipConst.Clear();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void btn_tarih_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                if (clickedButton == null) return;

                dp_baslangic_tarih.SelectedDate = null;
                dp_bitis_tarih.SelectedDate = null;
                if (filterDic.ContainsKey("BaslangicTarih"))
                    filterDic.Remove("BaslangicTarih");
                if (filterDic.ContainsKey("BitisTarih"))
                    filterDic.Remove("BitisTarih");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
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

        private void btn_popup_filter_collection_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if(button == null)
                {
                    CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Counter_ = 0;
                switch (button.Tag.ToString())
                {
                    case "FilterPopupHareketTipi":
                        if (harTipConst == null)
                            harTipConst = new();
                        else
                            harTipConst.Clear();

                        foreach (Cls_Depo item in FilterPopupLstHareketTipi.ItemsSource)
                        {
                            if (item.IsChecked == true)
                                harTipConst.Add(item.HareketTipiKodu);
                        }
                        FilterPopupHareketTipi.IsOpen = false;
                        break;
                    case "FilterPopupBelgeTipi":
                        if (belTipConst == null)
                            belTipConst = new();
                        else
                            belTipConst.Clear();

                        foreach (Cls_Depo item in FilterPopupLstBelgeTipi.ItemsSource)
                        {
                            if (item.IsChecked == true)
                            {
                                belTipConst.Add(item.BelgeTipiKodu);
                                Variables.Counter_++;
                            }
                        }
                        FilterPopupBelgeTipi.IsOpen = false;
                        break;

                    case "FilterPopupFaturaTipi":
                        if (fatTipConst == null)
                            fatTipConst = new();
                        else
                            fatTipConst.Clear();

                        foreach (Cls_Depo item in FilterPopupLstFaturaTipi.ItemsSource)
                        {
                            if (item.IsChecked == true)
                            { 
                                fatTipConst.Add(item.FaturaTipiKodu);
                                Variables.Counter_++;
                            }
                            FilterPopupFaturaTipi.IsOpen = false;
                        }
                        break;
                }

                pageNumber = 1;
                if (filterDic == null)
                    filterDic = new();
                stokHareketCollection = depo.PopulateStokHareketList(filterDic, 1, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                totalResult = depo.CountStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                dg_stok_hareket.ItemsSource = stokHareketCollection;
                ShowResult(stokHareketCollection.Count, totalResult);

                Mouse.OverrideCursor = null;

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Filtreler Uygulanırken");
            }
        }

        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_stok_hareket, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > stokHareketCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Depo> moreStokHareketCollection = new();
                    moreStokHareketCollection = depo.PopulateStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                    if (moreStokHareketCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stok Hareketleri Eklenirken");
                        Mouse.OverrideCursor = null;
                        if (isPageUp)
                            pageNumber--;
                        return;
                    }
                    if (moreStokHareketCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastMasterCollection = new ObservableCollection<Cls_Depo>
                                        (stokHareketCollection.Union(moreStokHareketCollection).ToList());
                        dg_stok_hareket.ItemsSource = lastMasterCollection;
                        dg_stok_hareket.Items.Refresh();
                        stokHareketCollection = lastMasterCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastMasterCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset = center;
                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {

                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_stok_hareket, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > stokHareketCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Depo> moreMasCollection = new();
                    moreMasCollection = depo.PopulateStokHareketList(filterDic, pageNumber, fabrika, depoKoduConst, kod1Const, harTipConst, belTipConst, fatTipConst);
                    if (moreMasCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stok Hareketleri Eklenirken");
                        Mouse.OverrideCursor = null;
                        if (isPageUp) pageNumber--;

                        return;
                    }
                    if (moreMasCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastMasCollection = new ObservableCollection<Cls_Depo>
                                        (stokHareketCollection.Union(moreMasCollection).ToList());
                        dg_stok_hareket.ItemsSource = lastMasCollection;
                        dg_stok_hareket.Items.Refresh();
                        stokHareketCollection = lastMasCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastMasCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset = center;

                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
            }

        }

        private void btn_open_Frm_Stok_Durum(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu &&
                    contextMenu.PlacementTarget is TextBox textBox)
                {
                    // Get the DataContext of the TextBox (the row data)
                    var rowData = textBox.DataContext;

                    // Assuming StokKodu is a property in your bound row object
                    string stokKodu = (rowData as Cls_Depo)?.StokKodu;

                    if (string.IsNullOrEmpty(stokKodu))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kodu Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    // Check if StokKodu exists
                    Cls_Arge arge = new();
                    Variables.Result_ = arge.IfStokKoduExists(stokKodu);

                    if (!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kodu Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    // Open the form with StokKodu
                    Frm_Stok_Durum frm = new(stokKodu,fabrika);
                    frm.ShowDialog();
                }
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Stok Durum Formu Açılırken");
                Mouse.OverrideCursor = null;
            }
        }
    }
}

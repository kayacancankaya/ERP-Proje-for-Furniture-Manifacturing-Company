using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Arge.Recete;
using Layer_UI.Depo.Stok_Hareket;
using Layer_UI.Methods;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Finans
{
    /// <summary>
    /// Interaction logic for Frm_Irsaliye_Aktarim.xaml
    /// </summary>
    public partial class Frm_Irsaliye_Aktarim : Window
    {
        ObservableCollection<IrsaliyeAktarimMasViewModel> masColl { get; set; }
        IrsaliyeAktarimMasViewModel mas = new();
        Dictionary<string, string> filterDic = new();
        Dictionary<string, string> cariDic = new();
        Cls_Cari cari = new();
        string filterText = string.Empty;
        string filterHeader = string.Empty;
        int totalResult = 0;
        public Frm_Irsaliye_Aktarim()
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                Mouse.OverrideCursor = Cursors.Wait;

                masColl = mas.GetTop20IrsaliyeAktarimList();
                tr_irsaliye.Items.Clear();
                tr_irsaliye.ItemsSource = masColl;
            
                totalResult = mas.CountAktarimList();
                if (totalResult > 20)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 20 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                cariDic = cari.GetTeslimCaris();
                cbx_cari_secim.ItemsSource = cariDic.Values;
                cbx_cari_secim_kod.ItemsSource = cariDic.Keys;

                lastOffset = 0;
                pageNumber = 1;
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Aktarım Formu Açılırken Hata İle Karşılaşıldı.");
            }

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
        private void btn_popup_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                IrsaliyeAktarimMasViewModel selectedMas = clickedButton.DataContext as IrsaliyeAktarimMasViewModel;
                if (clickedButton == null)
                {
                    CRUDmessages.GeneralFailureMessage("Aktarılacak İrsaliye Bulunumadı");
                    return;
                }
                txt_eski_fisno.Text = selectedMas.Fisno;
                IrsaliyeAktarimPopup.IsOpen = true;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Aktarım Formu Açılırken");
            }
        }
        private void btn_aktar_clicked(object sender, EventArgs e)
        {
            try
            {
                Variables.ErrorMessage_ = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txt_eski_fisno.Text))
                    Variables.ErrorMessage_ += "İrsaliye Numarası Boş Olamaz \n";
                if (string.IsNullOrEmpty(txt_yeni_fis_no.Text))
                    Variables.ErrorMessage_ += "Yeni Fiş No Boş Olamaz \n";
                if (txt_yeni_fis_no.Text.Length!=15)
                    Variables.ErrorMessage_ += "Yeni Fiş No 15 Karakter Olmalı \n";
                if (cbx_cari_secim.SelectedItem == null)
                    Variables.ErrorMessage_ += "Cari Seçiniz \n";
                ComboBoxItem selectedExportType = cbx_export_type.SelectedItem as ComboBoxItem;
                if (selectedExportType == null)
                    Variables.ErrorMessage_ += "İhracat Tipi Seçiniz \n";

                if(!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    CRUDmessages.GeneralFailureMessage(Variables.ErrorMessage_);
                    Mouse.OverrideCursor = null;
                    return;
                }

                int exportTypeID = Convert.ToInt32(selectedExportType.Tag);

                Variables.Result_ = mas.InsertAktarilacakIrsaliye(txt_eski_fisno.Text,txt_yeni_fis_no.Text,cbx_cari_secim_kod.SelectedItem.ToString(), exportTypeID);
                if (Variables.Result_)
                    CRUDmessages.InsertSuccessMessage("Fatura", 1);
                else
                {
                    CRUDmessages.GeneralFailureMessage("İrsaliye Kaydedilirken");
                    mas.AktarilacakIrsaliyeKayitGeriAl(txt_eski_fisno.Text, txt_yeni_fis_no.Text);
                }

                masColl = mas.GetTop20IrsaliyeAktarimList();
                tr_irsaliye.ItemsSource = masColl;

                totalResult = mas.CountAktarimList();
                if (totalResult > 20)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 20 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                lastOffset = 0;
                pageNumber = 1;
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Aktarma İşlemi Başarısız.");
                Mouse.OverrideCursor = null;
            }
        }
        private void cbx_cari_secim_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbx_cari_secim.SelectedItem == null)
                    cbx_cari_secim_kod.SelectedIndex = -1;
                else
                    cbx_cari_secim_kod.SelectedIndex = cbx_cari_secim.SelectedIndex;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Cari Seçim Esnasında");
            }
        }
        private void txt_yeni_fisno_text_changed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_yeni_fis_no.Text.Length != 15)
                    txt_yeni_fis_no.Foreground = new SolidColorBrush(Colors.Red);
                else
                    txt_yeni_fis_no.Foreground = new SolidColorBrush(Colors.Green);
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Fişno Yazımı Esnasında");
            }
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switch (button.Tag.ToString())
                {

                    case "Fisno":
                        txt_header_fis_no.Text = button.Tag.ToString();
                        FilterPopupFisno.IsOpen = true;
                        break;
                    case "Tarih":
                        txt_header_tarih.Text = button.Tag.ToString();
                        FilterPopupTarih.IsOpen = true;
                        break;
                    case "Cari Kodu":
                        txt_header_cari_kodu.Text = button.Tag.ToString();
                        FilterPopupCariKodu.IsOpen = true;
                        break;
                    case "Cari Adı":
                        txt_header_cari_adi.Text = button.Tag.ToString();
                        FilterPopupCariAdi.IsOpen = true;
                        break;
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
            }
        }
        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    TextBox textBox = sender as TextBox;
                    switch (textBox.Name.ToString())
                    {
                        case "FilterTextBox_fis_no":
                            filterText = FilterTextBox_fis_no.Text;
                            filterHeader = txt_header_fis_no.Text;
                            FilterPopupFisno.IsOpen = false;
                            break;
                        case "FilterTextBox_tarih":
                            filterText = FilterTextBox_tarih.Text;
                            filterHeader = txt_header_tarih.Text;
                            FilterPopupTarih.IsOpen = false;
                            break;
                        case "FilterTextBox_cari_kodu":
                            filterText = FilterTextBox_cari_kodu.Text;
                            filterHeader = txt_header_cari_kodu.Text;
                            FilterPopupCariKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_cari_adi":
                            filterText = FilterTextBox_cari_adi.Text;
                            filterHeader = txt_header_cari_adi.Text;
                            FilterPopupCariAdi.IsOpen = false;
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
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }

                    if (filterDic.Count > 0)
                        masColl = mas.GetIrsaliyeAktarimList(filterDic, 1);
                    else
                        masColl = mas.GetTop20IrsaliyeAktarimList();
                    tr_irsaliye.ItemsSource = masColl;

                    if (filterDic.Count > 0)
                        totalResult = mas.CountAktarimList(filterDic);
                    else
                        totalResult = mas.CountAktarimList();
                    
                    
                    if (totalResult > 20)
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 20 adet Listeleniyor.";
                    else
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";


                    lastOffset = 0;
                    pageNumber = 1;
                    Mouse.OverrideCursor = null;

                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_clear_filter_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FilterTextBox_cari_adi.Text = string.Empty;
                FilterTextBox_cari_kodu.Text = string.Empty;
                FilterTextBox_fis_no.Text = string.Empty;
                FilterTextBox_tarih.Text = string.Empty;

                if (filterDic != null)
                {
                    if (filterDic.Count > 0)
                        filterDic.Clear();
                }

                masColl = mas.GetTop20IrsaliyeAktarimList();
                tr_irsaliye.ItemsSource = masColl;

                totalResult = mas.CountAktarimList();
                if (totalResult > 20)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 20 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleri Kaldırırken");
                Mouse.OverrideCursor = null;
            }
        }

        double lastOffset = 0;
        int pageNumber = 1;
        bool isPageUp = false;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                 isPageUp = false;
                var border = VisualTreeHelper.GetChild(tr_irsaliye, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > masColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<IrsaliyeAktarimMasViewModel> moreMasterCollection = new();
                    moreMasterCollection = mas.GetIrsaliyeAktarimList(filterDic, pageNumber);
                    if (moreMasterCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İrsaliyeler Eklenirken");
                        Mouse.OverrideCursor = null;
                        if(isPageUp)
                            pageNumber--;
                        return;
                    }
                    if (moreMasterCollection.Count > 0)
                    {
                        ObservableCollection<IrsaliyeAktarimMasViewModel> lastMasterCollection = new ObservableCollection<IrsaliyeAktarimMasViewModel>
                                        (masColl.Union(moreMasterCollection).ToList());
                        tr_irsaliye.ItemsSource = lastMasterCollection;
                        tr_irsaliye.Items.Refresh();
                        masColl = lastMasterCollection;
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
                if(isPageUp) pageNumber--;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {

                isPageUp = false;
                var border = VisualTreeHelper.GetChild(tr_irsaliye, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > masColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<IrsaliyeAktarimMasViewModel> moreMasCollection = new();
                    moreMasCollection = mas.GetIrsaliyeAktarimList(filterDic, pageNumber);
                    if (moreMasCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İrsaliyeler Eklenirken");
                        Mouse.OverrideCursor = null;
                        if(isPageUp) pageNumber--;

                        return; 
                    }
                    if (moreMasCollection.Count > 0)
                    {
                        ObservableCollection<IrsaliyeAktarimMasViewModel> lastMasCollection = new ObservableCollection<IrsaliyeAktarimMasViewModel>
                                        (masColl.Union(moreMasCollection).ToList());
                        tr_irsaliye.ItemsSource = lastMasCollection;
                        tr_irsaliye.Items.Refresh();
                        masColl = lastMasCollection;
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
                if(isPageUp) pageNumber--;
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

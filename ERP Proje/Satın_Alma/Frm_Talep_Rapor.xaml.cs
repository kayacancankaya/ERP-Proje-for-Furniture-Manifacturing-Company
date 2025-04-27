using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Satis.Evrak;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Satın_Alma
{
    /// <summary>
    /// Interaction logic for Frm_Talep_Rapor.xaml
    /// </summary>
    public partial class Frm_Talep_Rapor : Window
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
        Cls_Arge arge = new();
        Cls_SatinAlma satinAlma = new();
        Variables variables = new();
        ObservableCollection<Cls_SatinAlma> SiparisVermeCollection = new();
        ObservableCollection<Cls_SatinAlma> DataGridCollection = new();
        ObservableCollection<Cls_SatinAlma> SiparisCollection = new();
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        bool kapaliTalepleriGosterme= false;
        bool siparislesenleriGosterme = false;
        bool teslimEdilenleriGosterme = false;
        Dictionary<string, string> constraints = new Dictionary<string, string>();

        public Frm_Talep_Rapor()
        {
            InitializeComponent();
            cbx_kod_1.ItemsSource = arge.GetDistinctKod1();
        }
        private void btn_talep_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_TalepSecim.ItemsSource = null;
                dg_TalepSecim.Items.Clear();

                Mouse.OverrideCursor = Cursors.Wait;

                constraints.Clear();

                if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    constraints.Add("stokKodu", txt_stok_kodu.Text);
                else
                    constraints.Add("stokKodu", null);

                if (!string.IsNullOrEmpty(txt_stok_adi.Text))
                    constraints.Add("stokAdi", txt_stok_adi.Text);
                else
                    constraints.Add("stokAdi", null);

                if (!string.IsNullOrEmpty(txt_talep_numarasi.Text))
                    constraints.Add("talepNumarasi", txt_talep_numarasi.Text);
                else
                    constraints.Add("talepNumarasi", null);

                if (!string.IsNullOrEmpty(txt_siparis_numarasi.Text))
                    constraints.Add("siparisNumarasi", txt_siparis_numarasi.Text);
                else
                    constraints.Add("siparisNumarasi", null);

                if (cbx_kod_1.SelectedItem != null)
                    constraints.Add("Kod1", cbx_kod_1.SelectedItem.ToString());
                else
                    constraints.Add("Kod1", null);

                if (cb_kapali_siparis.IsChecked == true)
                    kapaliTalepleriGosterme = true;
                else
                    kapaliTalepleriGosterme = false;
                if (cb_siparis_edilen.IsChecked == true)
                    siparislesenleriGosterme = true;
                else
                    siparislesenleriGosterme = false;
                if (cb_teslim_edilen.IsChecked == true)
                    teslimEdilenleriGosterme = true;
                else
                    teslimEdilenleriGosterme = false;

                ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateTalepReportList(constraints, 1, kapaliTalepleriGosterme, siparislesenleriGosterme, teslimEdilenleriGosterme);

                SiparisVermeCollection = firstColl;

                if (SiparisVermeCollection == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Talepler Listelenirken");
                    dg_TalepSecim.ItemsSource = null;
                    txt_result.Visibility = Visibility.Collapsed;
                    lastOffset = 0;
                    pageNumber = 1;
                    return;
                }
                if (SiparisVermeCollection.Count() == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    dg_TalepSecim.ItemsSource = SiparisVermeCollection;
                    txt_result.Visibility = Visibility.Collapsed;
                    lastOffset = 0;
                    pageNumber = 1;
                    return;
                }
                totalResult = satinAlma.CountTalepReportList(constraints, 1, kapaliTalepleriGosterme, siparislesenleriGosterme, teslimEdilenleriGosterme);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                dg_TalepSecim.ItemsSource = SiparisVermeCollection;
                txt_result.Visibility = Visibility.Visible;
                lastOffset = 0;
                pageNumber = 1;

                Mouse.OverrideCursor = null;

            }

            catch { CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken"); Mouse.OverrideCursor = null; txt_result.Visibility = Visibility.Collapsed; }
        }
        ScrollViewer scrollViewer;
        Decorator border;
        bool isPageUp = false;
        private void dg_scroll_down(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_TalepSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;

                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > SiparisCollection.Count())
                {
                        Mouse.OverrideCursor = Cursors.Wait;
                        pageNumber++;
                        ObservableCollection<Cls_SatinAlma> moreSiparisCollection = new();

                        if (cb_kapali_siparis.IsChecked == true)
                            kapaliTalepleriGosterme = true;
                        else
                            kapaliTalepleriGosterme = false;
                        if (cb_teslim_edilen.IsChecked == true)
                            siparislesenleriGosterme = true;
                        else
                            siparislesenleriGosterme = false;
                        moreSiparisCollection = satinAlma.PopulateTalepReportList(constraints, pageNumber,kapaliTalepleriGosterme,siparislesenleriGosterme,teslimEdilenleriGosterme);
                        if (moreSiparisCollection == null)
                        {
                            CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                            if (isPageUp)
                             pageNumber--;
                            Mouse.OverrideCursor = null;
                        }
                        if (moreSiparisCollection.Count > 0)
                        {
                            ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                            (SiparisVermeCollection.Union(moreSiparisCollection).ToList());
                            dg_TalepSecim.ItemsSource = lastTalepCollection;
                            dg_TalepSecim.Items.Refresh();
                            SiparisVermeCollection = lastTalepCollection;

                            txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                                double center = 0;
                                if (scrollViewer != null)
                                {
                                    center = scrollViewer.ScrollableHeight / 2.0;
                                    scrollViewer.ScrollToVerticalOffset(center);
                                }
                                lastOffset = center;
                            

                        }
                        Mouse.OverrideCursor = null;
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");

                if (isPageUp)
                    pageNumber--;
                Mouse.OverrideCursor = null;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_TalepSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;

                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > SiparisCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_SatinAlma> moreTalepCollection = new();

                    if (cb_kapali_siparis.IsChecked == true)
                        kapaliTalepleriGosterme = true;
                    else
                        kapaliTalepleriGosterme = false;
                    if (cb_teslim_edilen.IsChecked == true)
                        siparislesenleriGosterme = true;
                    else
                        siparislesenleriGosterme = false;
                    moreTalepCollection = satinAlma.PopulateTalepReportList(constraints, pageNumber, kapaliTalepleriGosterme, siparislesenleriGosterme,teslimEdilenleriGosterme);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                        if (isPageUp)
                            pageNumber--;
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                        (SiparisVermeCollection.Union(moreTalepCollection).ToList());
                        dg_TalepSecim.ItemsSource = lastTalepCollection;
                        dg_TalepSecim.Items.Refresh();
                        SiparisVermeCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        if (scrollViewer != null)
                        {
                            center = scrollViewer.ScrollableHeight / 2.0;
                            scrollViewer.ScrollToVerticalOffset(center);
                        }
                        lastOffset = center;
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                if (isPageUp)
                    pageNumber--;
                Mouse.OverrideCursor = null;
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private bool selectMiktarColumn = false;
    }
}

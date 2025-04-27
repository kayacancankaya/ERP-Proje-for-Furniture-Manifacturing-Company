using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Satın_Alma
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Ver.xaml
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
        Cls_Arge arge = new();
        Cls_SatinAlma satinAlma = new();
        Variables variables = new();
        ObservableCollection<Cls_SatinAlma> SiparisVermeCollection = new();
        ObservableCollection<Cls_SatinAlma> DataGridCollection = new();
        ObservableCollection<Cls_SatinAlma> SiparisCollection = new();
        public bool kapaliSiparisleriGosterme = false;
        public bool teslimEdilenleriGosterme = false;
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        Dictionary<string, string> constraints = new Dictionary<string, string>();
        public Frm_Siparis_Rapor()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod_1.ItemsSource = arge.GetDistinctKod1();
        }
        private void btn_siparis_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                dg_SiparisSecim.ItemsSource = null;
                dg_SiparisSecim.Items.Clear();

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
                    kapaliSiparisleriGosterme = true;
                else
                    kapaliSiparisleriGosterme = false;
                if (cb_teslim_edilen.IsChecked == true)
                    teslimEdilenleriGosterme = true;
                else
                    teslimEdilenleriGosterme = false;

                ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateSiparisReportList(constraints, 1,kapaliSiparisleriGosterme,teslimEdilenleriGosterme);

                SiparisVermeCollection = firstColl;

                if (SiparisVermeCollection == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                    dg_SiparisSecim.ItemsSource = null;
                    txt_result.Visibility = Visibility.Collapsed;
                    lastOffset = 0;
                    pageValueChanged = -50;
                    pageNumber = 1;
                    return;
                }
                if (SiparisVermeCollection.Count() == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    dg_SiparisSecim.ItemsSource = SiparisVermeCollection;
                    txt_result.Visibility = Visibility.Collapsed;
                    lastOffset = 0;
                    pageValueChanged = -50;
                    pageNumber = 1;
                    return;
                }
                totalResult = satinAlma.CountSiparisReportList(constraints, 1, kapaliSiparisleriGosterme, teslimEdilenleriGosterme);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                dg_SiparisSecim.ItemsSource = SiparisVermeCollection;
                txt_result.Visibility = Visibility.Visible;
                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;

            }

            catch { CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken"); Mouse.OverrideCursor = null; txt_result.Visibility = Visibility.Collapsed; }
        }

        private void dg_scroll_down(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                
                    if (e.VerticalChange != 0)
                    {
                        lastOffset = e.VerticalOffset;
                        if (lastOffset > pageValueChanged + 100 && totalResult > SiparisCollection.Count())
                        {
                            Mouse.OverrideCursor = Cursors.Wait;
                            pageNumber++;
                            ObservableCollection<Cls_SatinAlma> moreSiparisCollection = new();
                        if (cb_kapali_siparis.IsChecked == true)
                            kapaliSiparisleriGosterme = true;
                        else
                            kapaliSiparisleriGosterme = false;
                        if (cb_teslim_edilen.IsChecked == true)
                            teslimEdilenleriGosterme = true;
                        else
                            teslimEdilenleriGosterme = false;
                            moreSiparisCollection = satinAlma.PopulateSiparisReportList(constraints, pageNumber, kapaliSiparisleriGosterme, teslimEdilenleriGosterme);
                            if (moreSiparisCollection == null)
                            {
                                CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                                Mouse.OverrideCursor = null;
                            }
                            if (moreSiparisCollection.Count > 0)
                            {
                                ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                                (SiparisVermeCollection.Union(moreSiparisCollection).ToList());
                                dg_SiparisSecim.ItemsSource = lastTalepCollection;
                                dg_SiparisSecim.Items.Refresh();
                                SiparisVermeCollection = lastTalepCollection;
                                pageValueChanged += 100;
                                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                                var border = VisualTreeHelper.GetChild(dg_SiparisSecim, 0) as Decorator;
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

                if (lastOffset > pageValueChanged + 100 && totalResult > SiparisVermeCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_SatinAlma> moreTalepCollection = new();
                    if (cb_kapali_siparis.IsChecked == true)
                        kapaliSiparisleriGosterme = true;
                    else
                        kapaliSiparisleriGosterme = false;
                    if (cb_teslim_edilen.IsChecked == true)
                        teslimEdilenleriGosterme = true;
                    else
                        teslimEdilenleriGosterme = false;
                    moreTalepCollection = satinAlma.PopulateSiparisReportList(constraints, pageNumber,kapaliSiparisleriGosterme,teslimEdilenleriGosterme);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                        (SiparisVermeCollection.Union(moreTalepCollection).ToList());
                        dg_SiparisSecim.ItemsSource = lastTalepCollection;
                        dg_SiparisSecim.Items.Refresh();
                        SiparisVermeCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_SiparisSecim, 0) as Decorator;
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

using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Uretim
{
    /// <summary>
    /// Interaction logic for Frm_Uretim_Durum.xaml
    /// </summary>
    public partial class Frm_Uretim_Durum : Window
    {
        DateTime currentDate = DateTime.Now;
        int totalResult = 0;

        public Frm_Uretim_Durum()
        {
            InitializeComponent(); Window_Loaded();
            
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
        Variables variables = new();
        Cls_Isemri isemri = new();
        ObservableCollection<Cls_Isemri> isemriCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                {
                    //if (txt_siparis_no.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}

                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_siparis_sira.Text))
                    restrictionPairs.Add("@siparisSira", txt_siparis_sira.Text);

                if (!string.IsNullOrWhiteSpace(txt_takip_no.Text))
                {
                    //if (txt_takip_no.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Takip Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}
                    restrictionPairs.Add("@takipno", txt_takip_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_refisemrino.Text))
                {
                    //if (txt_refisemrino.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("İşemri Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}
                    restrictionPairs.Add("@refisemrino", txt_refisemrino.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_urun_kodu.Text))
                {

                    //if (txt_urun_kodu.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Koduna 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}

                    restrictionPairs.Add("@urunKodu", txt_urun_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_urun_adi.Text))
                {
                    //if (txt_urun_adi.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}
                    restrictionPairs.Add("@urunAdi", txt_urun_adi.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_isemrino.Text))
                {
                    //if (txt_isemrino.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("İşemri Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}
                    restrictionPairs.Add("@isemrino", txt_isemrino.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                {

                    //if (txt_stok_kodu.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Koduna 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}

                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                {
                    //if (txt_stok_adi.Text.Length < 3)
                    //{
                    //    CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                    //    return;
                    //}
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);
                }


                //if (restrictionPairs.Count == 1 &&
                //    restrictionPairs.ContainsKey("@siparisSira"))
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Yalnızca Sipariş Sıra ile Filtreleme Yapılamaz.");
                //    return;
                //}
                

                //if (restrictionPairs.Count == 0)
                //{
                //    CRUDmessages.NoInput();
                //    return;
                //}


                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Isemri> firstColl = isemri.PopulateUretimDurumList(restrictionPairs, 1);

                isemriCollection = firstColl;

                totalResult = isemri.CountUretimDurumList(restrictionPairs, 1);

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                if (isemriCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null; return;
                }

                if (!isemriCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null;
                }

                dg_IsemriSecim.ItemsSource = isemriCollection;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
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
                CRUDmessages.GeneralFailureMessage("Liste Sıfırlanırken");
            }
        }

        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;

        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(dg_IsemriSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;
                // Check if vertical scrolling occurred
                if (e.ScrollEventType == ScrollEventType.EndScroll && scrollViewer.VerticalOffset > lastOffset)
                {

                    lastOffset = scrollViewer.VerticalOffset;

                    if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > isemriCollection.Count())
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        pageNumber++;
                        ObservableCollection<Cls_Isemri> moreIsemriCollection = new();
                        moreIsemriCollection = isemri.PopulateUretimDurumList(restrictionPairs, pageNumber);
                        if (moreIsemriCollection == null)
                        {
                            CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                            Mouse.OverrideCursor = null;
                        }
                        if (moreIsemriCollection.Count > 0)
                        {
                            ObservableCollection<Cls_Isemri> lastIsemriCollection = new ObservableCollection<Cls_Isemri>
                                            (isemriCollection.Union(moreIsemriCollection).ToList());
                            dg_IsemriSecim.ItemsSource = lastIsemriCollection;
                            dg_IsemriSecim.Items.Refresh();
                            isemriCollection = lastIsemriCollection;
                            pageValueChanged += 100;
                            txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastIsemriCollection.Count() + " adet Listeleniyor.";


                            double center = 0;
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
                var border = VisualTreeHelper.GetChild(dg_IsemriSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;
                // Check if vertical scrolling occurred
                
                    lastOffset = scrollViewer.VerticalOffset;

                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > isemriCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Isemri> moreIsemriCollection = new();
                    moreIsemriCollection = isemri.PopulateUretimDurumList(restrictionPairs, pageNumber);
                    if (moreIsemriCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreIsemriCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Isemri> lastIsemriCollection = new ObservableCollection<Cls_Isemri>
                                        (isemriCollection.Union(moreIsemriCollection).ToList());
                        dg_IsemriSecim.ItemsSource = lastIsemriCollection;
                        dg_IsemriSecim.Items.Refresh();
                        isemriCollection = lastIsemriCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastIsemriCollection.Count() + " adet Listeleniyor.";


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
            }

        }

        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Mouse_Wheel_Pushed(object sender, MouseWheelEventArgs e)
        {

        }
    }
}

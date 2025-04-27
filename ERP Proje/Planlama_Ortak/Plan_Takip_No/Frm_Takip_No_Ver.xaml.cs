using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Evrak;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Ortak.Plan_Takip_No
{
    /// <summary>
    /// Interaction logic for Frm_Takip_No_Ver.xaml
    /// </summary>
    public partial class Frm_Takip_No_Ver : Window
    {
        LoginLogic login = new();
        string departman = string.Empty;
        Cls_Planlama plan = new();
        List<string> planNoList = new();
        Dictionary<int, string> planAdiDic = new();
        List<string> takipNoList = new();
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        Dictionary<string, string> restrictionPairs = new();
        public Frm_Takip_No_Ver()
        {
            try
            {

                InitializeComponent(); Window_Loaded();
                if (login.GetDepartment().Contains("Doseme"))
                    departman = "Doseme";
                else if (login.GetDepartment().Contains("Moduler"))
                    departman = "Moduler";
                else
                    departman = "Hepsi";

                planAdiDic = plan.GetDistinctPlanAdiString(departman);
                cbx_plan_adi.ItemsSource = planAdiDic.Values;
                planNoList = plan.GetDistinctPlanNo();
                cbx_plan_no.ItemsSource = planNoList;
                takipNoList = plan.GetDistinctTakipNoString(departman);
                cbx_takip_no.ItemsSource = takipNoList;
                if(departman == "Doseme" ||
                    departman == "Hepsi")
                    cbx_yeni_takip_no.SelectedIndex = 0;
                else
                    cbx_yeni_takip_no.SelectedIndex = 1;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Sayfa Açılırken");
            }
        }
        ObservableCollection<Cls_Planlama> planColl = new();
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                listele();
            }
            catch
            {
                Mouse.OverrideCursor = null;
            }
        }
        private void listele()
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;
                restrictionPairs.Clear();
                if (!string.IsNullOrEmpty(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNumarasi", txt_siparis_no.Text);
                if (cbx_plan_adi.SelectedItem != null)
                    restrictionPairs.Add("@planAdi", cbx_plan_adi.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    restrictionPairs.Add("@urunKodu", txt_stok_kodu.Text);
                if (cbx_plan_no.SelectedItem != null)
                    restrictionPairs.Add("@planNo", cbx_plan_no.SelectedItem.ToString());

                ObservableCollection<Cls_Planlama> siparisColl = plan.PlanNoVerilecekSiparisler(restrictionPairs, false, 1, true);
                if (siparisColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Takip Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (siparisColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                planColl = siparisColl;
                dg_SiparisSecim.ItemsSource = planColl;

                totalResult = plan.CountPlanNoVerilecekSiparisler(restrictionPairs, false, 1, true);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";


                dg_SiparisSecim.Visibility = Visibility.Visible;
                txt_result.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                takipNoList = plan.GetDistinctTakipNoString(departman);
                cbx_takip_no.ItemsSource = takipNoList;

                cbi_add.IsSelected = true;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        bool isPageUp = false;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_SiparisSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > planColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Planlama> moreSiparisCollection = new();
                    moreSiparisCollection = plan.PlanNoVerilecekSiparisler(restrictionPairs, false, pageNumber, true);
                    if (moreSiparisCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        if (isPageUp)
                            pageNumber--;
                        Mouse.OverrideCursor = null;
                    }
                    if (moreSiparisCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Planlama> lastTalepCollection = new ObservableCollection<Cls_Planlama>
                                        (planColl.Union(moreSiparisCollection).ToList());
                        dg_SiparisSecim.ItemsSource = lastTalepCollection;
                        dg_SiparisSecim.Items.Refresh();
                        planColl = lastTalepCollection;
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
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_SiparisSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > planColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_Planlama> moreTalepCollection = new();
                    moreTalepCollection = plan.PlanNoVerilecekSiparisler(restrictionPairs, false, pageNumber, true);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        if (isPageUp)
                            pageNumber--;
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Planlama> lastTalepCollection = new ObservableCollection<Cls_Planlama>
                                        (planColl.Union(moreTalepCollection).ToList());
                        dg_SiparisSecim.ItemsSource = lastTalepCollection;
                        dg_SiparisSecim.Items.Refresh();
                        planColl = lastTalepCollection;
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
        private void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Variables.Counter_ = 0;
                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Planlama> planNoVerilecekColl = new();
                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        if (!string.IsNullOrEmpty(item.TakipNo))
                        {
                            Mouse.OverrideCursor = null;
                            CRUDmessages.GeneralFailureMessageCustomMessage(item.SiparisNumarasi + "," + item.SiparisSira + " Takip Numarası Var, Yeni Takip Numarası Verilemez");
                            return;
                        }

                        Variables.Result_ = UserEntryControl.IsStringNullOrEmpty(item.SiparisNumarasi, item.Isemrino, null, null, null);
                        if (!Variables.Result_)
                        {
                            Mouse.OverrideCursor = null;
                            CRUDmessages.GeneralFailureMessageCustomMessage(item.PlanAdi + " Sipariş Numarası ve Referans İşemri Boş Olamaz");
                            return;
                        }

                        planNoVerilecekColl.Add(item);

                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                bool isExisting = false;
                bool isManuel = false;
                string? mevcutTakip = string.Empty;
                string? manuelGir = txt_manuel_gir.Text;
                if (!string.IsNullOrEmpty(manuelGir))
                {
                    if (Variables.Counter_ > 1)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Manuel Takip No Girildiğinden Birden Çok Seçim Yapılamaz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    int control = 0;
                    if (manuelGir.Length != 8)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Manuel Girilen Takip No 8 Karakterden Oluşmalı.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else if (manuelGir.Substring(0, 1) != "M" &&
                            manuelGir.Substring(0, 1) != "D")
                    {

                        CRUDmessages.GeneralFailureMessageCustomMessage("Manuel Girilen Takip No İlk Karakteri 'M' ya da 'D' Olmalı.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else if (!Int32.TryParse(manuelGir.Substring(1, 7), out control))
                    {

                        CRUDmessages.GeneralFailureMessageCustomMessage("Manuel Girilen Takip No Son 7 Karakteri Sayı Olmalı.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else
                    {
                        isManuel = true;
                        isExisting = true;
                        mevcutTakip = txt_manuel_gir.Text;
                    }

                    Variables.ResultInt_ = plan.CheckIfTakipNoExists(txt_manuel_gir.Text);
                    if (Variables.ResultInt_ > 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Manuel Girilen Takip No'ya Ait Sistemde Kayıt Mevcut.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (Variables.ResultInt_ == 1)
                    {
                        CRUDmessages.GeneralFailureMessage("Manuel Girilen Takip No Kontrol Edilirken");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                }
                else if (cb_mevcut_takip_noya_ekle.IsChecked == true)
                {
                    if (cb_mevcut_takip_noya_ekle == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Mevcut Takip No Seçimi Bulunamadı.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (cbx_takip_no.SelectedItem == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Mevcut Takip No Seçimi Bulunamadı.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    mevcutTakip = cbx_takip_no.SelectedItem.ToString();
                    isExisting = true;
                }
                else if (cb_yeni_takip_no.IsChecked == true)
                {
                    if(cbx_yeni_takip_no.SelectedItem == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Yeni Takip No İlk Karakterini Seçiniz.");
                        Mouse.OverrideCursor = null;
                    }
                    ComboBoxItem combo = cbx_yeni_takip_no.SelectedItem as ComboBoxItem;
                    
                    mevcutTakip = combo.Content.ToString();
                }

                Variables.Result_ = CRUDmessages.InsertOnayMessage();
                if(!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.Result_ = plan.InsertTakipNo(planNoVerilecekColl, isExisting, mevcutTakip,isManuel);


                if (!Variables.Result_) { CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında"); Mouse.OverrideCursor = null; return; }
                CRUDmessages.InsertSuccessMessage("İşemri", planNoVerilecekColl.Count());
                Mouse.OverrideCursor = null;
                listele();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sil_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Variables.Counter_ = 0;
                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Planlama> planNoSilinecekColl = new();
                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        if (string.IsNullOrEmpty(item.TakipNo))
                        {
                            Mouse.OverrideCursor = null;
                            CRUDmessages.GeneralFailureMessageCustomMessage(item.SiparisNumarasi + "sıra:" + item.SiparisSira + " Takip Numarası Yok, Silinemez.");
                            return;
                        }

                        Variables.Result_ = UserEntryControl.IsStringNullOrEmpty(item.SiparisNumarasi, item.Isemrino, null, null, null);
                        if (!Variables.Result_)
                        {
                            Mouse.OverrideCursor = null;
                            CRUDmessages.GeneralFailureMessageCustomMessage(item.PlanAdi + " Sipariş Numarası ve Referans İşemri Boş Olamaz.");
                            return;
                        }

                        planNoSilinecekColl.Add(item);

                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.ErrorMessage_ = string.Empty;
                Variables.Counter_ = 0;
                foreach (Cls_Planlama item in planNoSilinecekColl)
                {
                    Variables.Result_ = plan.TakipNoCallBack(item.ReferansIsemrino);
                    if (!Variables.Result_)
                        Variables.ErrorMessage_ += string.Format("Sipariş Numarası:{0},Sipariş Satır:{1} Silinemedi \n");
                    else
                        Variables.Counter_++;
                }

                if (!Variables.Result_) 
                { 
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    if(Variables.Counter_ == 0)
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }
                CRUDmessages.DeleteSuccessMessage("İşemri", Variables.Counter_);
                Mouse.OverrideCursor = null;
                listele();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Planlama item in dg_SiparisSecim.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private bool selectMiktarColumn = false;
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
        private void cb_mevcut_takip_noya_ekle_checked(object sender, EventArgs e)
        {
            cbx_takip_no.Visibility = Visibility.Visible;
            cb_yeni_takip_no.IsChecked = false;
        }
        private void cb_mevcut_takip_noya_ekle_unchecked(object sender, EventArgs e)
        {
            cbx_takip_no.Visibility = Visibility.Collapsed;
        }
        private void cbx_plan_adi_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNoList;
                    return;
                }

                List<string> relatedPlanNos = plan.GetPlanAdiRelatedPlanNos(combo.SelectedItem.ToString());
                cbx_plan_no.ItemsSource = relatedPlanNos;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
            }
        }
        private void cbx_plan_no_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNoList;
                    return;
                }

                List<string> relatedPlanAdis = plan.GetPlanNoRelatedPlanAdis(combo.SelectedItem.ToString());
                cbx_plan_adi.ItemsSource = relatedPlanAdis;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
            }
        }
        private void cbx_islem_tipi_selection_changed(object sender,SelectionChangedEventArgs e)
        {
            try
            {
                if (cbx_islem_tipi == null)
                    return;
                if (cbx_islem_tipi.SelectedItem == null)
                    return;
                if(cbx_islem_tipi.SelectedIndex  == 0)
                {
                    stc_add.Visibility = Visibility.Visible;
                    btn_sil.Visibility = Visibility.Collapsed;
                }
                if(cbx_islem_tipi.SelectedIndex  == 1)
                {
                    stc_add.Visibility = Visibility.Collapsed;
                    btn_sil.Visibility = Visibility.Visible;
                }

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("İşlem Tipi Değiştirilirken");
            }
        }
        private void txt_manuel_gir_text_changed(object obj, EventArgs e)
        {
            try
            {
                int originalSelectionStart = txt_manuel_gir.SelectionStart;
                string text = txt_manuel_gir.Text;
                txt_manuel_gir.Text = text.ToUpper();
                txt_manuel_gir.Text.ToUpper();
                if (txt_manuel_gir.Text.Length < 8)
                {
                    txt_manuel_gir.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (txt_manuel_gir.Text.Length > 8)
                {
                    txt_manuel_gir.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (txt_manuel_gir.Text.Length == 8 &&
                        (txt_manuel_gir.Text.Substring(0, 1) != "D" &&
                        txt_manuel_gir.Text.Substring(0, 1) != "M"))
                {
                    txt_manuel_gir.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (txt_manuel_gir.Text.Length == 8 && 
                         !UIinteractions.TryConvertToInt32(txt_manuel_gir.Text.Substring(1, 7)))
                {
                    txt_manuel_gir.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (txt_manuel_gir.Text.Length == 8)
                {
                    txt_manuel_gir.Foreground = new SolidColorBrush(Colors.Green);
                }
                txt_manuel_gir.SelectionStart = originalSelectionStart;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Takip No Yazımı Esnasında");
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void cb_yeni_takip_no_checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cbx_yeni_takip_no != null)
                    cbx_yeni_takip_no.Visibility = Visibility.Visible;
                cb_mevcut_takip_noya_ekle.IsChecked = false;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Yeni Takip No Seçilirken");
            }
        }

        private void cb_yeni_takip_no_unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbx_yeni_takip_no.Visibility = Visibility.Collapsed;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Yeni Takip No Seçilirken");
            }
        }
    }
}

using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
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

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Frm_Turemis_Kod_Guncelle.xaml
    /// </summary>
    public partial class Frm_Turemis_Kod_Guncelle : Window
    {
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> argeColl = new();
        ObservableCollection<Cls_Arge> secilenColl = new();
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
        public Frm_Turemis_Kod_Guncelle()
        {
            InitializeComponent();
            Window_Loaded();
        }
        private void btn_sablon_kod_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Sablon_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if(Variables.FormResult_ == true)
                {
                    txt_sablon_kod.Text = frm.SelectedStokKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Şablon Kod Rehberi Açılırken");
            }
        }
        private void cbx_guncelleme_tipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;
                if (combo == null)
                {
                    stc_button_list.Visibility = Visibility.Collapsed;
                    btn_guncelle.Visibility = Visibility.Collapsed;
                    btn_listele.Visibility = Visibility.Collapsed;
                    cbx_guncelleme_tipi.SelectedIndex = -1;
                    dg_varyant_liste.Visibility = Visibility.Collapsed;
                    stc_secilenleri_guncelle.Visibility = Visibility.Collapsed;
                    return;
                }
                ComboBoxItem item = combo.SelectedItem as ComboBoxItem;
                if (item == null)
                {
                    stc_button_list.Visibility = Visibility.Collapsed;
                    btn_guncelle.Visibility = Visibility.Collapsed;
                    btn_listele.Visibility = Visibility.Collapsed;
                    dg_varyant_liste.Visibility = Visibility.Collapsed;
                    stc_secilenleri_guncelle.Visibility = Visibility.Collapsed;
                    cbx_guncelleme_tipi.SelectedIndex = -1;

                    return;
                }
                if (item.Content.ToString() == "Tümünü Güncelle")
                {
                    stc_button_list.Visibility = Visibility.Visible;
                    btn_guncelle.Visibility = Visibility.Visible;
                    btn_listele.Visibility = Visibility.Collapsed;
                    dg_varyant_liste.Visibility = Visibility.Collapsed;
                    stc_secilenleri_guncelle.Visibility = Visibility.Collapsed;
                }
                if (item.Content.ToString() == "Seçilenleri Güncelle")
                {
                    stc_button_list.Visibility = Visibility.Visible;
                    btn_guncelle.Visibility = Visibility.Collapsed;
                    btn_listele.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme Tipi Seçilirken");
            }
        }
        private async void btn_guncelle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sablonKodu = txt_sablon_kod.Text;
                if (string.IsNullOrEmpty(sablonKodu))
                {
                    CRUDmessages.NoSelection();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.Result_ = await arge.IfStokKoduExistsAsync(sablonKodu);
                if(!Variables.Result_) { txt_please_wait.Visibility = Visibility.Collapsed; CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu Sistemde Bulunamadı");Mouse.OverrideCursor = null; return; }

                Variables.Result_ = await arge.UpdateTuremisKodTumuAsync(sablonKodu);
                txt_please_wait.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün");
                    return;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    return;
                }
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string sablonKodu = txt_sablon_kod.Text;
                if (string.IsNullOrEmpty(sablonKodu))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = arge.IfStokKoduExists(sablonKodu);
                if (!Variables.Result_) {CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu Sistemde Bulunamadı"); Mouse.OverrideCursor = null; return; }

                argeColl = arge.GetVaryantCodes(sablonKodu);
                if(argeColl == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Varyant Bilgileri Listelenirken");
                    return;
                }
                if (argeColl.Count == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                dg_varyant_liste.ItemsSource = argeColl;
                dg_varyant_liste.Items.Refresh();
                dg_varyant_liste.Visibility = Visibility.Visible;
                stc_secilenleri_guncelle.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;

            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
            }
        }
        //private void btn_show_detail_Clicked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch
        //    {
        //        CRUDmessages.GeneralFailureMessageCustomMessage("Detay Bilgileri Listelenirken");
        //    }
        //}
        private async void btn_secilenleri_guncelle_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string sablonKodu = txt_sablon_kod.Text;
                if (string.IsNullOrEmpty(sablonKodu))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.Result_ = await arge.IfStokKoduExistsAsync(sablonKodu);
                if (!Variables.Result_) { txt_please_wait.Visibility = Visibility.Collapsed; CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu Sistemde Bulunamadı"); Mouse.OverrideCursor = null; return; }
                
                if(secilenColl == null)
                { txt_please_wait.Visibility = Visibility.Collapsed; CRUDmessages.GeneralFailureMessageCustomMessage("Seçilen Kodlar Bulunamadı"); Mouse.OverrideCursor = null; return; }
                if (secilenColl.Count > 0)
                    secilenColl.Clear();

                foreach (Cls_Arge item in dg_varyant_liste.Items)
                {
                    if (item.IsChecked == true)
                        secilenColl.Add(item);
                }

                if (secilenColl == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessageCustomMessage("Seçilen Ürün Kodları Bulunamadı");
                    return;
                }
                if (secilenColl.Count == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.NoSelection();
                    return;
                }

                Variables.Result_ = await arge.UpdateTuremisKodTekAsync(sablonKodu,secilenColl);
                txt_please_wait.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün");
                    return;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    return;
                }
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
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
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Arge item in dg_varyant_liste.Items)
            {
                item.IsChecked = headerIsChecked;
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

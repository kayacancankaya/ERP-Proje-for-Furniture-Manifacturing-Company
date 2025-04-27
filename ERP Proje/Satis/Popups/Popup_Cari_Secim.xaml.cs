using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Layer_Business.Cls_Base;

namespace Layer_UI.Satis.Siparis.Popups
{

    public partial class Popup_Cari_Secim : Window
    {

        public string MyProperty { get; set; }
        private frm_musteri_secim mainFormInstance;

        frm_musteri_secim frm_Musteri_Secim = new frm_musteri_secim();
        Variables variables = new Variables();
        public Popup_Cari_Secim()
        {
            InitializeComponent();

            DataContext = Resources["cls_cari"];

            mainFormInstance = frm_Musteri_Secim;

            // Subscribe to the DataUpdated event in the main form
            mainFormInstance.DataUpdated += HandleDataUpdated;
        }

        private void btn_teslim_cari_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Cls_Cari cls_cari_teslim_cari = new Cls_Cari();

                if (string.IsNullOrEmpty(txt_teslim_cari_kodu.Text) && string.IsNullOrEmpty(txt_teslim_cari_adi.Text))
                {
                    MessageBox.Show("Lütfen Cari Bilgisi Giriniz.");
                    return;
                }


                dg_SipariseCariBaglaTeslimCari.ItemsSource = null;
                dg_SipariseCariBaglaTeslimCari.Items.Clear();

                cls_cari_teslim_cari.SipariseCariBaglaCollection = cls_cari_teslim_cari.PopulateSipariseCariBaglaTeslimCari(txt_teslim_cari_kodu.Text, txt_teslim_cari_adi.Text);
                dg_SipariseCariBaglaTeslimCari.ItemsSource = cls_cari_teslim_cari.SipariseCariBaglaCollection;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_satis_cari_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Cls_Cari cls_cari_satis_cari = new Cls_Cari();

                if (txt_satis_cari_kodu.Text == "" && txt_satis_cari_adi.Text == "") { MessageBox.Show("Lütfen Cari Bilgisi Giriniz."); return; }


                dg_SipariseCariBaglaSatisCari.ItemsSource = null;
                dg_SipariseCariBaglaSatisCari.Items.Clear();


                cls_cari_satis_cari.SipariseCariBaglaCollection = cls_cari_satis_cari.PopulateSipariseCariBaglaSatisCari(txt_satis_cari_kodu.Text, txt_satis_cari_adi.Text);
                dg_SipariseCariBaglaSatisCari.ItemsSource = cls_cari_satis_cari.SipariseCariBaglaCollection;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_musteri_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Cls_Cari cls_cari = new();
                variables.Counter = 0;


                // Assuming your DataGrid is bound to a collection of objects of type MyDataGridItem
                foreach (Cls_Cari item in dg_SipariseCariBaglaSatisCari.Items)
                {
                    // Assuming you have a property in MyDataGridItem that represents the checkbox state
                    if (item.IsChecked)
                    {
                        cls_cari.SatisCariKodu = item.SatisCariKodu;
                        cls_cari.SatisCariAdi = item.SatisCariAdi;
                        variables.Counter++;
                    }
                }

                variables.WarningMessage = string.Empty;

                if (variables.Counter == 0)
                {
                    cls_cari.SatisCariKodu = txt_satis_cari_kodu.Text;
                    cls_cari.SatisCariAdi = txt_satis_cari_adi.Text;
                    variables.WarningMessage = "Satış Cari Limited Seçildi.\n";
                }

                if (variables.Counter > 1)
                {
                    MessageBox.Show("Birden Fazla Satış Carisi Seçilemez.");
                    return;
                }

                variables.Counter = 0;
                foreach (Cls_Cari item in dg_SipariseCariBaglaTeslimCari.Items)
                {
                    // Assuming you have a property in MyDataGridItem that represents the checkbox state
                    if (item.IsChecked)
                    {
                        cls_cari.TeslimCariKodu = item.TeslimCariKodu;
                        cls_cari.TeslimCariAdi = item.TeslimCariAdi;
                        variables.Counter++;
                    }
                }

                if (variables.Counter == 0)
                {
                    MessageBox.Show("Teslim Carisi Seçiniz.");
                    return;

                }

                if (variables.Counter > 1)
                {
                    MessageBox.Show("Birden Fazla Teslim Carisi Seçilemez.");
                    return;

                }
                try
                {
                    if (cmb_doviz_tipi.SelectedItem == null)
                    {
                        cls_cari.DovizTipi = DovizTipi.USD;
                        variables.WarningMessage = variables.WarningMessage + "Döviz Tipi Seçilmediğinden USD Atandı.\n";
                    }
                    else cls_cari.DovizTipi = Cls_Cari.GetDovizTipi(((ComboBoxItem)cmb_doviz_tipi.SelectedItem).Content.ToString());

                    if (cmb_siparis_tipi.SelectedItem == null)
                    {
                        cls_cari.SiparisTipi = SiparisTipi.Yurtdisi;
                        variables.WarningMessage = variables.WarningMessage + "Satış Tipi Seçilmediğinden Yurt Dışı Atandı.\n";
                    }
                    else cls_cari.SiparisTipi = Cls_Cari.GetSiparisTipi(((ComboBoxItem)cmb_siparis_tipi.SelectedItem).Content.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Döviz Tipi ve Sipariş Tipi bilgileri kaydedilirken\n hata ile karşılaşıldı.");
                }

                if (string.IsNullOrEmpty(variables.WarningMessage) == false)
                {
                    variables.WarningMessage = variables.WarningMessage + "Devam Etmek İstiyor Musunuz?";
                    MessageBoxResult result = MessageBox.Show(variables.WarningMessage, "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Raise the DataUpdated event to pass the data
                        mainFormInstance.RaiseDataUpdated(cls_cari);

                        var openWindows = Application.Current.Windows.OfType<Window>().ToList();

                        openWindows[0].Close();//açık sipariş formunu kapatıp yeni instanceı aç.
                        frm_Musteri_Secim.Show();
                        //frm_Musteri_Secim.Show();
                        //openWindows[2].Close();//açık sipariş formunu kapatıp yeni instanceı aç.
                        //openWindows[4].Close();//açık sipariş formunu kapatıp yeni instanceı aç.
                        this.Close();
                    }

                    else return;

                }

                else
                {
                    mainFormInstance.RaiseDataUpdated(cls_cari);

                    var openWindows = Application.Current.Windows.OfType<Window>().ToList();
                    openWindows[0].Close();//açık sipariş formunu kapatıp yeni instanceı aç.
                    frm_Musteri_Secim.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HandleDataUpdated(Cls_Cari cls_Cari)
        {
            // Your event handling logic here
            // Update the main form with the data received from the popup
            frm_Musteri_Secim.txt_satis_cari_kodu.Text = cls_Cari.SatisCariKodu;
            frm_Musteri_Secim.txt_satis_cari_adi.Text = cls_Cari.SatisCariAdi;
            frm_Musteri_Secim.txt_teslim_cari_kodu.Text = cls_Cari.TeslimCariKodu;
            frm_Musteri_Secim.txt_teslim_cari_adi.Text = cls_Cari.TeslimCariAdi;
            frm_Musteri_Secim.txt_doviz_tipi.Text = cls_Cari.DovizTipi.ToString();
            frm_Musteri_Secim.txt_siparis_tipi.Text = cls_Cari.SiparisTipi.ToString();
        }
    }
}

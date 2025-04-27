using Layer_Business;
using System;
using System.Windows;

namespace Layer_UI.Satis.Popups
{

    public partial class Popup_Cari_Secim_Onay_Bekleyen_Siparis_Guncelle : Window
    {

        string satisOrTeslim = string.Empty;

        private Popup_Onay_Bekleyen_Siparis_Guncelle callingForm;

        public Popup_Cari_Secim_Onay_Bekleyen_Siparis_Guncelle(string satis_teslim, Popup_Onay_Bekleyen_Siparis_Guncelle callingForm)
        {
            InitializeComponent();

            satisOrTeslim = satis_teslim;
            this.callingForm = callingForm;
        }

        Cls_Cari cls_Cari = new();

        private void btn_cari_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txt_cari_kodu.Text) && string.IsNullOrEmpty(txt_cari_adi.Text))
                {
                    MessageBox.Show("Lütfen Cari Bilgisi Giriniz.");
                    return;
                }

                dg_SipariseCariBagla.ItemsSource = null;
                dg_SipariseCariBagla.Items.Clear();

                cls_Cari.SipariseCariBaglaCollection = cls_Cari.PopulateSipariseCariBaglaSatisCari(txt_cari_kodu.Text, txt_cari_adi.Text);
                dg_SipariseCariBagla.ItemsSource = cls_Cari.SipariseCariBaglaCollection;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                int counter = 0;
                foreach (Cls_Cari item in dg_SipariseCariBagla.Items)
                {
                    if (item.IsChecked) counter++;

                }

                if (counter > 1)
                {
                    MessageBox.Show("Birden Fazla Satış Carisi Seçilemez.");
                    return;
                }

                if (counter == 0)
                {
                    MessageBox.Show("Hiç Seçim Yapmadınız.");
                    return;
                }

                foreach (Cls_Cari item in dg_SipariseCariBagla.Items)
                {

                    if (item.IsChecked && satisOrTeslim == "satis")
                    {

                        callingForm.UpdateCariTextBoxes("satis", item.SatisCariKodu, item.SatisCariAdi);
                        counter++;
                        break;
                    }
                    if (item.IsChecked && satisOrTeslim == "teslim")
                    {

                        callingForm.UpdateCariTextBoxes("teslim", item.SatisCariKodu, item.SatisCariAdi);
                        counter++;
                        break;

                    }
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

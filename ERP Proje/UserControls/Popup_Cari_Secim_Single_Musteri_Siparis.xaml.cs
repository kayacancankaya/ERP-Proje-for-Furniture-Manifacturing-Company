using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Windows;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Popup_Cari_Secim_Single_Musteri_Siparis.xaml
    /// </summary>
    public partial class Popup_Cari_Secim_Single_Musteri_Siparis : Window
    {
        public Popup_Cari_Secim_Single_Musteri_Siparis()
        {
            InitializeComponent();
        }
        Variables variables = new();
        Cls_Cari cari = new();

        public string CariKodu { get; set; } = string.Empty;


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

                cari.SipariseCariBaglaCollection = cari.PopulateSipariseCariBaglaTeslimCari(txt_cari_kodu.Text, txt_cari_adi.Text);
                dg_SipariseCariBagla.ItemsSource = cari.SipariseCariBaglaCollection;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cari_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                variables.Counter = 0;

                foreach (Cls_Cari item in dg_SipariseCariBagla.Items)
                {
                    if (item.IsChecked)
                    {
                        cari.TeslimCariKodu = item.TeslimCariKodu;
                        variables.Counter++;
                    }
                }

                variables.WarningMessage = string.Empty;

                if (variables.Counter == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (variables.Counter > 1)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Birden Fazla Satış Carisi Seçilemez.");
                    return;
                }


                if (!string.IsNullOrEmpty(variables.WarningMessage))
                {
                    variables.WarningMessage = variables.WarningMessage + "Devam Etmek İstiyor Musunuz?";
                    MessageBoxResult result = MessageBox.Show(variables.WarningMessage, "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.No)
                        return;
                }

                CariKodu = cari.TeslimCariKodu;

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

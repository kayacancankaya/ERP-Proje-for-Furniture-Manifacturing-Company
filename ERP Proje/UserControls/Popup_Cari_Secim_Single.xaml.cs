using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Popup_Cari_Secim_Single.xaml
    /// </summary>
    public partial class Popup_Cari_Secim_Single : Window
    {

        public Popup_Cari_Secim_Single()
        {
            InitializeComponent();
        }
        Variables variables = new();
        Cls_Cari cari = new();

        public string CariKodu { get; set; } = string.Empty;
        public string CariAdi { get; set; } = string.Empty;

        public string DovizTipi { get; set; } = string.Empty;

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
                        cari.TeslimCariAdi = item.TeslimCariAdi;
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

                try
                {
                    if (cmb_doviz_tipi.SelectedItem == null)
                    {
                        DovizTipi = "TRY";
                        variables.WarningMessage = variables.WarningMessage + "Döviz Tipi Seçilmediğinden Tl Atandı.\n";
                    }
                    else
                    {
                        ComboBoxItem selectedItem = cmb_doviz_tipi.SelectedItem as ComboBoxItem;
                        DovizTipi = selectedItem.Content.ToString();
                    }

                }
                catch (Exception ex)
                {
                    CRUDmessages.GeneralFailureMessage("Döviz Tipi Bilgileri Alınırken");
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
                CariAdi = cari.TeslimCariAdi;

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

using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Frm_Cari_Stok_Rehberi.xaml
    /// </summary>
    public partial class Frm_Cari_Stok_Rehberi : Window
    {
        public Frm_Cari_Stok_Rehberi()
        {
            InitializeComponent();
        }

        ObservableCollection<Cls_Depo> depoCollection = new();
        Cls_Depo depo = new();
        Cls_Etiket etiket = new();
        Variables variables = new();
        public string SelectedStokKodu { get; private set; }
        private void btn_stok_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txt_stok_kodu.Text) &&
                    string.IsNullOrEmpty(txt_stok_adi.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kodu veya Stok Adı Hanelerinden Birini Doldurunuz.");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Dictionary<string, string> constraintPairs = new();

                if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    constraintPairs.Add("stokKodu", txt_stok_kodu.Text);
                else constraintPairs.Add("stokKodu", null);
                if (!string.IsNullOrEmpty(txt_stok_adi.Text))
                    constraintPairs.Add("stokAdi", txt_stok_adi.Text);
                else constraintPairs.Add("stokAdi", null);

                //eğer bir tane seçim yapıldıysa karakter 3 den küçük olamaz
                if (constraintPairs.Any(v => v.Value == null))
                {
                    if (!string.IsNullOrEmpty(txt_stok_kodu.Text) &&
                        txt_stok_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Eğer Sadece Stok Kodu Filtrelenecek ise 3 Karakterden Az Giriş Yapılamaz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (!string.IsNullOrEmpty(txt_stok_adi.Text) &&
                        txt_stok_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Eğer Sadece Stok Adı Filtrelenecek ise 3 Karakterden Az Giriş Yapılamaz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }

                if (depoCollection.Count > 0) depoCollection.Clear();
                depoCollection = depo.GetStokKarti(constraintPairs);

                if (depoCollection.Any(a => a.StokAdi == "Stok Kodu, Stok Adı Seçimleri Bulunamadı"))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kodu, Stok Adı Seçimleri Bulunamadı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (depoCollection.Any(a => a.StokAdi == "Sorgu Boş Sonuç Döndürdü"))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sorgu Boş Sonuç Döndürdü");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (depoCollection.Any(a => a.StokAdi == "Veri Tabanına Bağlanırken Hata İle Karşılaşıldı."))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Veri Tabanına Bağlanırken Hata İle Karşılaşıldı.");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_StokListe.ItemsSource = depoCollection;
                Mouse.OverrideCursor = null;
                dg_StokListe.Visibility = Visibility.Visible;
                stc_listele.Visibility = Visibility.Visible;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Kartı Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_cari_stok_listele_clicked(object sender, EventArgs e)
        {
            try
            {
                if (depoCollection.Where(c => c.IsChecked == true).Count() != 1)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("1 Adet Seçim Yapılmalıdır.");
                    return;
                }

                string aktarilacakStok = depoCollection.Select(s => s.StokKodu).FirstOrDefault();

                if (string.IsNullOrEmpty(aktarilacakStok))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sorgulanacak Stok Kodu Bulunamadı.");
                    return;
                }

                ObservableCollection<Cls_Etiket> etiketColl = etiket.GetEtiketInfo(aktarilacakStok, false);
                if (etiketColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Etiket Bilgileri Alınırken");
                    return;
                }

                if (etiketColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                dg_CariStokListe.ItemsSource = etiketColl;
                dg_CariStokListe.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Cari Stok Bilgileri Listelenirken");
                return;
            }
        }

        private void btn_stok_bilgisi_aktar_clicked(object sender, EventArgs e)
        {
            try
            {
                variables.Counter = 0;
                foreach (Cls_Etiket item in dg_CariStokListe.Items)
                {
                    if (item.IsChecked == true)
                    {
                        SelectedStokKodu = item.CariStokKodu;
                        variables.Counter++;
                    }
                }

                if (variables.Counter == 0)
                {
                    CRUDmessages.GeneralFailureMessageNoInput();
                    return;
                }
                if (variables.Counter > 1)
                {
                    CRUDmessages.GeneralFailureMessageMoreSelectionThanExpected();
                    return;
                }
                DialogResult = true;
                Close();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Bilgisi Aktarılırken");

                Mouse.OverrideCursor = null;
            }
        }
    }
}

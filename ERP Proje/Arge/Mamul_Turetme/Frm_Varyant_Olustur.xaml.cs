using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Frm_Varyant_Olustur.xaml
    /// </summary>
    public partial class Frm_Varyant_Olustur : Window
    {
        Cls_Urun urun = new();
        bool didAnythingChanged = false;
        public string UrunGrubuKodu { get; set; }
        public string ModelKodu { get; set; }
        public string SatisSekilKodu { get; set; }
        public Frm_Varyant_Olustur()
        {
            InitializeComponent(); Window_Loaded();
            List<string> urunGrupColl = urun.GetUrunGrupIsim().Select(i => i.UrunGrubuIsim).ToList();
            List<string> modelColl = urun.GetModelIsim().Select(i => i.ModelIsim).ToList();
            List<string> satisColl = urun.GetSatisSekilIsim().Select(i => i.SatisSekilIsim).ToList();
            cbx_urun_grup.ItemsSource = urunGrupColl;
            cbx_model.ItemsSource = modelColl;
            cbx_satis_sekil.ItemsSource = satisColl;
            cbx_yeni_urun_grup.ItemsSource = urunGrupColl;
            cbx_yeni_model.ItemsSource = modelColl;
            cbx_yeni_satis_sekil.ItemsSource = satisColl;

            btn_ilerle.Content = "İlerle";
            btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);

        }
        private void cbx_guncelleme_tipi_selection_changed(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme Tipi Seçimi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                ComboBoxItem selectedItem = cbx.SelectedItem as ComboBoxItem;
                if (selectedItem == null)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }
                stc_opsiyon_olustur.Visibility = Visibility.Visible;
                stc_comboboxes.Visibility = Visibility.Visible;
                dg_model_secim.Visibility = Visibility.Visible;

                if (selectedItem.Content.ToString() == "Varolan")
                {
                    rd_changable.Height = rd_changable.Height = new GridLength(80);
                    txt_varolan_urun_grup.Text = "Güncellenecek Ürün Grubu";
                    txt_varolan_model.Text = "Güncellenecek Model";
                    txt_varolan_satis_sekil.Text = "Güncellenecek Satış Şekil";
                    stc_yeni_opsiyon_text.Visibility = Visibility.Collapsed;
                    stc_yeni_opsiyon_combo.Visibility = Visibility.Collapsed;
                    cbx_yeni_urun_grup.SelectedItem = null;
                    cbx_yeni_model.SelectedItem = null;
                    cbx_yeni_satis_sekil.SelectedItem = null;
                }
                if (selectedItem.Content.ToString() == "Yeni")
                {
                    rd_changable.Height = rd_changable.Height = new GridLength(160);
                    txt_varolan_urun_grup.Text = "Kopyalanacak Ürün Grubu";
                    txt_varolan_model.Text = "Kopyalanacak Model";
                    txt_varolan_satis_sekil.Text = "Kopyalanacak Satış Şekil";
                    stc_yeni_opsiyon_text.Visibility = Visibility.Visible;
                    stc_yeni_opsiyon_combo.Visibility = Visibility.Visible;
                    cbx_urun_grup.SelectedItem = null;
                    cbx_model.SelectedItem = null;
                    cbx_satis_sekil.SelectedItem = null;
                }

                dg_model_secim.ItemsSource = urun.PopulateModelSecimList(UrunGrubuKodu, ModelKodu, SatisSekilKodu);
                dg_model_secim.Items.Refresh();

                txt_urun_grubu_kodu.Text = string.Empty;
                txt_model_kodu.Text = string.Empty;
                txt_satis_sekil_kodu.Text = string.Empty;
                txt_urun_grubu_ismi.Text = string.Empty;
                txt_model_ismi.Text = string.Empty;
                txt_satis_sekil_ismi.Text = string.Empty;
                txt_ingilizce_isim_anahtar.Text = string.Empty;
                txt_isim_anahtar.Text = string.Empty;
                txt_kod1.Text = string.Empty;
                txt_kod2.Text = string.Empty;
                txt_kod3.Text = string.Empty;
                txt_kod4.Text = string.Empty;
                txt_sablon_kodu.Text = string.Empty;
                txt_opsiyon_adedi.Text = string.Empty;
                didAnythingChanged = false;
                btn_ilerle.Content = "İlerle";
                btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Seçilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void cbx_yeni_urun_grup_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    UrunGrubuKodu = urun.GetUrunGrubuKodu(cbx.SelectedItem.ToString()).Select(k => k.UrunGrubuKodu).FirstOrDefault().ToString();
                else
                    UrunGrubuKodu = string.Empty;
                txt_yeni_urun_grup.Text = UrunGrubuKodu;

                if (cbx.SelectedItem == null)
                    txt_yeni_urun_grubu_ismi.Text = string.Empty;
                else
                    txt_yeni_urun_grubu_ismi.Text = cbx.SelectedItem.ToString();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçilirken");
            }
        }
        private void cbx_yeni_model_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Model Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    ModelKodu = urun.GetModelKodu(cbx.SelectedItem.ToString()).Select(k => k.ModelKodu).FirstOrDefault().ToString();
                else
                    ModelKodu = string.Empty;
                txt_yeni_model.Text = ModelKodu;
                if (cbx.SelectedItem == null)
                    txt_yeni_model_ismi.Text = string.Empty;
                else
                    txt_yeni_model_ismi.Text = cbx.SelectedItem.ToString();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Seçilirken");
            }
        }
        private void cbx_yeni_satis_sekil_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    SatisSekilKodu = urun.GetSatisSekilKodu(cbx.SelectedItem.ToString()).Select(k => k.SatisSekilKodu).FirstOrDefault().ToString();
                else
                    SatisSekilKodu = string.Empty;
                txt_yeni_satis_sekil.Text = SatisSekilKodu;

                if (cbx.SelectedItem == null)
                    txt_yeni_satis_sekil_ismi.Text = string.Empty;
                else
                    txt_yeni_satis_sekil_ismi.Text = cbx.SelectedItem.ToString();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçilirken");
            }
        }
        private void cbx_urun_grup_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    UrunGrubuKodu = urun.GetUrunGrubuKodu(cbx.SelectedItem.ToString()).Select(k => k.UrunGrubuKodu).FirstOrDefault().ToString();
                else
                    UrunGrubuKodu = string.Empty;
                txt_urun_grup.Text = UrunGrubuKodu;
                if (!string.IsNullOrEmpty(txt_model.Text))
                    ModelKodu = txt_model.Text;
                else
                    ModelKodu = string.Empty;
                if (!string.IsNullOrEmpty(txt_satis_sekil_kodu.Text))
                    SatisSekilKodu = txt_satis_sekil_kodu.Text;
                else
                    SatisSekilKodu = string.Empty;

                dg_model_secim.ItemsSource = urun.PopulateModelSecimList(UrunGrubuKodu, ModelKodu, SatisSekilKodu);
                dg_model_secim.Items.Refresh();


                txt_isim_anahtar.Text = string.Empty;
                txt_kod1.Text = string.Empty;
                txt_kod2.Text = string.Empty;
                txt_kod3.Text = string.Empty;
                txt_kod4.Text = string.Empty;
                txt_sablon_kodu.Text = string.Empty;
                txt_opsiyon_adedi.Text = string.Empty;
                didAnythingChanged = false;
                btn_ilerle.Content = "İlerle";
                btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçilirken");
            }
        }
        private void cbx_model_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Model Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    ModelKodu = urun.GetModelKodu(cbx.SelectedItem.ToString()).Select(k => k.ModelKodu).FirstOrDefault().ToString();
                else
                    ModelKodu = string.Empty;
                txt_model.Text = ModelKodu;
                if (!string.IsNullOrEmpty(txt_urun_grup.Text))
                    UrunGrubuKodu = txt_urun_grup.Text;
                else
                    UrunGrubuKodu = string.Empty;
                if (!string.IsNullOrEmpty(txt_satis_sekil_kodu.Text))
                    SatisSekilKodu = txt_satis_sekil_kodu.Text;
                else
                    SatisSekilKodu = string.Empty;

                dg_model_secim.ItemsSource = urun.PopulateModelSecimList(UrunGrubuKodu, ModelKodu, SatisSekilKodu);
                dg_model_secim.Items.Refresh();

                txt_ingilizce_isim_anahtar.Text = string.Empty;
                txt_isim_anahtar.Text = string.Empty;
                txt_kod1.Text = string.Empty;
                txt_kod2.Text = string.Empty;
                txt_kod3.Text = string.Empty;
                txt_kod4.Text = string.Empty;
                txt_sablon_kodu.Text = string.Empty;
                txt_opsiyon_adedi.Text = string.Empty;
                didAnythingChanged = false;
                btn_ilerle.Content = "İlerle";
                btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Seçilirken");
            }
        }
        private void cbx_satis_sekil_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Satış Şekli Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    SatisSekilKodu = urun.GetSatisSekilKodu(cbx.SelectedItem.ToString()).Select(k => k.SatisSekilKodu).FirstOrDefault().ToString();
                else
                    SatisSekilKodu = string.Empty;
                txt_satis_sekil_kodu.Text = SatisSekilKodu;
                if (!string.IsNullOrEmpty(txt_urun_grup.Text))
                    UrunGrubuKodu = txt_urun_grup.Text;
                else
                    UrunGrubuKodu = string.Empty;
                if (!string.IsNullOrEmpty(txt_model.Text))
                    ModelKodu = txt_model.Text;
                else
                    ModelKodu = string.Empty;

                dg_model_secim.ItemsSource = urun.PopulateModelSecimList(UrunGrubuKodu, ModelKodu, SatisSekilKodu);
                dg_model_secim.Items.Refresh();

                txt_ingilizce_isim_anahtar.Text = string.Empty;
                txt_isim_anahtar.Text = string.Empty;
                txt_kod1.Text = string.Empty;
                txt_kod2.Text = string.Empty;
                txt_kod3.Text = string.Empty;
                txt_kod4.Text = string.Empty;
                txt_sablon_kodu.Text = string.Empty;
                txt_opsiyon_adedi.Text = string.Empty;
                didAnythingChanged = false;
                btn_ilerle.Content = "İlerle";
                btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Satış Şekli Seçilirken");
            }
        }
        private void btn_urun_grup_rehberi(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Rehberi Açılırken");
            }
        }
        private void btn_model_rehberi(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Rehberi Açılırken");
            }
        }
        private void btn_satis_sekil_rehberi(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Satış Şekil Rehberi Açılırken");
            }
        }
        private void btn_ozellik_ekle(object sender, EventArgs e)
        {
            try
            {
                Cls_Urun secilenModel = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if (secilenModel == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Seçilen Model Bulunamadı.");
                    return;
                }

                Cls_Urun opsiyonOlusacak = urun.GetOpsiyonOlusacakUrun(secilenModel);

                if (opsiyonOlusacak == null)
                {
                    CRUDmessages.GeneralFailureMessage("Özellik Eklenirken");
                    return;
                }

                txt_opsiyon_adedi.Text = opsiyonOlusacak.OzellikSayisi.ToString();
                txt_sablon_kodu.Text = opsiyonOlusacak.SablonKod;
                txt_ingilizce_isim_anahtar.Text = opsiyonOlusacak.IngilizceIsimAnahtar;
                txt_isim_anahtar.Text = opsiyonOlusacak.IsimAnahtar;
                txt_kod1.Text = opsiyonOlusacak.Kod1;
                txt_kod2.Text = opsiyonOlusacak.Kod2;
                txt_kod3.Text = opsiyonOlusacak.Kod3;
                txt_kod4.Text = opsiyonOlusacak.Kod4;
                txt_urun_grubu_kodu.Text = secilenModel.UrunGrubuKodu;
                txt_model_kodu.Text = secilenModel.ModelKodu;
                txt_satis_sekil_kodu.Text = secilenModel.SatisSekilKodu;
                txt_urun_grubu_ismi.Text = secilenModel.UrunGrubuIsim;
                txt_model_ismi.Text = secilenModel.ModelIsim;
                txt_satis_sekil_ismi.Text = secilenModel.SatisSekilIsim;

                stc_opsiyon_olustur.Visibility = Visibility.Visible;
                didAnythingChanged = false;
                ComboBoxItem selectedItem = cbx_guncelleme_tipi.SelectedItem as ComboBoxItem;
                if (selectedItem.Content.ToString() == "Yeni")
                {
                    btn_ilerle.Content = "Oluştur ve İlerle";
                    btn_ilerle.Background = new SolidColorBrush(Colors.Lime);
                }
                else
                {
                    btn_ilerle.Content = "İlerle";
                    btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Özellik Eklenirken");
            }
        }
        private void text_changed(object sender, EventArgs e)
        {
            try
            {
                didAnythingChanged = true;
                if (cbx_guncelleme_tipi.SelectedItem != null)
                {
                    ComboBoxItem selectedItem = cbx_guncelleme_tipi.SelectedItem as ComboBoxItem;
                    if (selectedItem.Content.ToString() == "Yeni")
                        btn_ilerle.Content = "Oluştur ve İlerle";
                    if (selectedItem.Content.ToString() == "Varolan")
                        btn_ilerle.Content = "Güncelle ve İlerle";
                    btn_ilerle.Background = new SolidColorBrush(Colors.Lime);
                }

            }
            catch
            {

            }
        }
        private void btn_sablon_kod_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Sablon_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    this.txt_sablon_kodu.Text = frm.SelectedStokKodu;
                    frm.Close();
                    didAnythingChanged = true;
                    if (cbx_guncelleme_tipi.SelectedItem != null)
                    {
                        ComboBoxItem selectedItem = cbx_guncelleme_tipi.SelectedItem as ComboBoxItem;
                        if (selectedItem.Content.ToString() == "Yeni")
                            btn_ilerle.Content = "Oluştur ve İlerle";
                        if (selectedItem.Content.ToString() == "Varolan")
                            btn_ilerle.Content = "Güncelle ve İlerle";
                        btn_ilerle.Background = new SolidColorBrush(Colors.Lime);
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Şablon Rehberi Açılırken");
            }
        }
        private void btn_ilerle_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ComboBoxItem selectedItem = cbx_guncelleme_tipi.SelectedItem as ComboBoxItem;

                if (selectedItem == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Güncelleme Tipi Seçiniz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (selectedItem.Content.ToString() == "Varolan")
                {
                    Cls_Urun olusturulacakVaryant = new();

                    olusturulacakVaryant.UrunGrubuKodu = txt_urun_grubu_kodu.Text;
                    olusturulacakVaryant.ModelKodu = txt_model_kodu.Text;
                    olusturulacakVaryant.SatisSekilKodu = txt_satis_sekil_kodu.Text;
                    olusturulacakVaryant.UrunGrubuIsim = txt_urun_grubu_ismi.Text;
                    olusturulacakVaryant.ModelIsim = txt_model_ismi.Text;
                    olusturulacakVaryant.SatisSekilIsim = txt_satis_sekil_ismi.Text;
                    olusturulacakVaryant.IngilizceIsimAnahtar = txt_ingilizce_isim_anahtar.Text;
                    olusturulacakVaryant.IsimAnahtar = txt_isim_anahtar.Text;
                    olusturulacakVaryant.Kod1 = txt_kod1.Text;
                    olusturulacakVaryant.Kod2 = txt_kod2.Text;
                    olusturulacakVaryant.Kod3 = txt_kod3.Text;
                    olusturulacakVaryant.Kod4 = txt_kod4.Text;
                    olusturulacakVaryant.SablonKod = txt_sablon_kodu.Text;
                    olusturulacakVaryant.OzellikSayisi = Convert.ToInt32(txt_opsiyon_adedi.Text);

                    if (didAnythingChanged)
                    {
                        Variables.Result_ = urun.UpdateVaryantOlustur(olusturulacakVaryant);
                        if (Variables.Result_)
                            CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                        else
                        {
                            CRUDmessages.UpdateFailureMessage("Ürün");
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }

                    ObservableCollection<Cls_Urun> opsiyonColl = urun.GetOpsiyonlar(olusturulacakVaryant);
                    Popup_Varyant_Ozellik_Ekle frm = new(olusturulacakVaryant, opsiyonColl, false);
                    Variables.FormResult_ = frm.ShowDialog();
                    if (Variables.FormResult_ == true)
                    {
                        txt_urun_grubu_kodu.Text = string.Empty;
                        txt_model_kodu.Text = string.Empty;
                        txt_satis_sekil_kodu.Text = string.Empty;
                        txt_urun_grubu_ismi.Text = string.Empty;
                        txt_model_ismi.Text = string.Empty;
                        txt_satis_sekil_ismi.Text = string.Empty;
                        txt_ingilizce_isim_anahtar.Text = string.Empty;
                        txt_isim_anahtar.Text = string.Empty;
                        txt_kod1.Text = string.Empty;
                        txt_kod2.Text = string.Empty;
                        txt_kod3.Text = string.Empty;
                        txt_kod4.Text = string.Empty;
                        txt_sablon_kodu.Text = string.Empty;
                        txt_opsiyon_adedi.Text = string.Empty;
                        didAnythingChanged = false;
                        btn_ilerle.Content = "İlerle";
                        btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);

                        stc_opsiyon_olustur.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        txt_urun_grubu_kodu.Text = string.Empty;
                        txt_model_kodu.Text = string.Empty;
                        txt_satis_sekil_kodu.Text = string.Empty;
                        txt_urun_grubu_ismi.Text = string.Empty;
                        txt_model_ismi.Text = string.Empty;
                        txt_satis_sekil_ismi.Text = string.Empty;
                        txt_ingilizce_isim_anahtar.Text = string.Empty;
                        txt_isim_anahtar.Text = string.Empty;
                        txt_kod1.Text = string.Empty;
                        txt_kod2.Text = string.Empty;
                        txt_kod3.Text = string.Empty;
                        txt_kod4.Text = string.Empty;
                        txt_sablon_kodu.Text = string.Empty;
                        txt_opsiyon_adedi.Text = string.Empty;
                        didAnythingChanged = false;
                        btn_ilerle.Content = "İlerle";
                        btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);

                        stc_opsiyon_olustur.Visibility = Visibility.Collapsed;

                    }
                }
                if (selectedItem.Content.ToString() == "Yeni")
                {
                    Cls_Urun olusturulacakVaryant = new();
                    if (string.IsNullOrEmpty(txt_opsiyon_adedi.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Opsiyon Adedi Boş Olamaz");
                        Mouse.OverrideCursor = null;
                        return;
                    }


                    if (string.IsNullOrEmpty(txt_sablon_kodu.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu Boş Olamaz");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (txt_sablon_kodu.Text.Length != 11)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu 11 Karakter Olmalı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (cbx_yeni_urun_grup.SelectedItem == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Yeni Ürün Grubu Seçiniz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (cbx_yeni_model.SelectedItem == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Yeni Model Seçiniz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (cbx_yeni_satis_sekil.SelectedItem == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Yeni Satış Şekli Seçiniz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    Variables.ResultInt_ = urun.CheckIfSablonKodExists(txt_sablon_kodu.Text);
                    if (Variables.ResultInt_ > 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Şablon Kodu Sistemde Kayıtlı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else if (Variables.ResultInt_ < 0)
                    {
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    Variables.ErrorMessage_ = string.Empty;
                    if (string.IsNullOrEmpty(txt_yeni_urun_grup.Text))
                        Variables.ErrorMessage_ = "Yeni Ürün Grubunu Tekrar Seçiniz. ";
                    if (string.IsNullOrEmpty(txt_yeni_model.Text))
                        Variables.ErrorMessage_ = "Yeni Modeli Tekrar Seçiniz. ";
                    if (string.IsNullOrEmpty(txt_yeni_satis_sekil.Text))
                        Variables.ErrorMessage_ = "Yeni Satış Şeklini Tekrar Seçiniz. ";

                    if (!string.IsNullOrEmpty(Variables.ErrorMessage_))
                    {
                        CRUDmessages.GeneralFailureMessage(Variables.ErrorMessage_);
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    olusturulacakVaryant.UrunGrubuKodu = txt_yeni_urun_grup.Text;
                    olusturulacakVaryant.ModelKodu = txt_yeni_model.Text;
                    olusturulacakVaryant.SatisSekilKodu = txt_yeni_satis_sekil.Text;
                    olusturulacakVaryant.UrunGrubuIsim = txt_yeni_urun_grubu_ismi.Text;
                    olusturulacakVaryant.ModelIsim = txt_yeni_model_ismi.Text;
                    olusturulacakVaryant.SatisSekilIsim = txt_yeni_satis_sekil_ismi.Text;
                    olusturulacakVaryant.IngilizceIsimAnahtar = txt_ingilizce_isim_anahtar.Text;
                    olusturulacakVaryant.IsimAnahtar = txt_isim_anahtar.Text;
                    olusturulacakVaryant.Kod1 = txt_kod1.Text;
                    olusturulacakVaryant.Kod2 = txt_kod2.Text;
                    olusturulacakVaryant.Kod3 = txt_kod3.Text;
                    olusturulacakVaryant.Kod4 = txt_kod4.Text;
                    olusturulacakVaryant.SablonKod = txt_sablon_kodu.Text;
                    olusturulacakVaryant.OzellikSayisi = Convert.ToInt32(txt_opsiyon_adedi.Text);

                    Variables.Result_ = urun.InsertVaryantOlustur(olusturulacakVaryant);
                    if (Variables.Result_)
                        CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                    else
                    {
                        CRUDmessages.UpdateFailureMessage("Ürün");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    Cls_Urun kopyalanacakUrun = new();
                    if (string.IsNullOrEmpty(txt_urun_grubu_kodu.Text) &&
                        string.IsNullOrEmpty(txt_model_kodu.Text) &&
                        string.IsNullOrEmpty(txt_satis_sekil_kodu.Text))
                    {
                        kopyalanacakUrun.UrunGrubuKodu = txt_urun_grubu_kodu.Text;
                        kopyalanacakUrun.ModelKodu = txt_model_kodu.Text;
                        kopyalanacakUrun.SatisSekilKodu = txt_satis_sekil_kodu.Text;
                    }


                    ObservableCollection<Cls_Urun> opsiyonColl = urun.GetOpsiyonlarYeniUrun(olusturulacakVaryant, kopyalanacakUrun);
                    Popup_Varyant_Ozellik_Ekle frm = new(olusturulacakVaryant, opsiyonColl, true);
                    Variables.FormResult_ = frm.ShowDialog();
                    if (Variables.FormResult_ == true)
                    {
                        txt_urun_grubu_kodu.Text = string.Empty;
                        txt_model_kodu.Text = string.Empty;
                        txt_satis_sekil_kodu.Text = string.Empty;
                        txt_urun_grubu_ismi.Text = string.Empty;
                        txt_model_ismi.Text = string.Empty;
                        txt_satis_sekil_ismi.Text = string.Empty;
                        txt_ingilizce_isim_anahtar.Text = string.Empty;
                        txt_isim_anahtar.Text = string.Empty;
                        txt_kod1.Text = string.Empty;
                        txt_kod2.Text = string.Empty;
                        txt_kod3.Text = string.Empty;
                        txt_kod4.Text = string.Empty;
                        txt_sablon_kodu.Text = string.Empty;
                        txt_opsiyon_adedi.Text = string.Empty;
                        cbx_yeni_urun_grup.SelectedItem = null;
                        cbx_yeni_model.SelectedItem = null;
                        cbx_yeni_satis_sekil.SelectedItem = null;
                        cbx_urun_grup.SelectedItem = null;
                        cbx_model.SelectedItem = null;
                        cbx_urun_grup.SelectedItem = null;
                        didAnythingChanged = false;
                        btn_ilerle.Content = "İlerle";
                        btn_ilerle.Background = new SolidColorBrush(Colors.LightGray);

                    }
                    else
                    {
                        txt_urun_grubu_kodu.Text = string.Empty;
                        txt_model_kodu.Text = string.Empty;
                        txt_satis_sekil_kodu.Text = string.Empty;
                        txt_urun_grubu_ismi.Text = string.Empty;
                        txt_model_ismi.Text = string.Empty;
                        txt_satis_sekil_ismi.Text = string.Empty;
                        txt_ingilizce_isim_anahtar.Text = string.Empty;
                        txt_isim_anahtar.Text = string.Empty;
                        txt_kod1.Text = string.Empty;
                        txt_kod2.Text = string.Empty;
                        txt_kod3.Text = string.Empty;
                        txt_kod4.Text = string.Empty;
                        txt_sablon_kodu.Text = string.Empty;
                        txt_opsiyon_adedi.Text = string.Empty;
                        cbx_yeni_urun_grup.SelectedItem = null;
                        cbx_yeni_model.SelectedItem = null;
                        cbx_yeni_satis_sekil.SelectedItem = null;
                        cbx_urun_grup.SelectedItem = null;
                        cbx_model.SelectedItem = null;
                        cbx_urun_grup.SelectedItem = null;
                        didAnythingChanged = false;
                        btn_ilerle.Content = "İlerle";

                    }
                }
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Varyant Bilgileri Açılırken");
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
        private bool selectMiktarColumn = false;
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Gönderilecek Mik")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        // Select the "Miktar" column of the row
                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
            }
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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
        private void btn_minimize_click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }
        private void btn_close_click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void btn_level_down_click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen();
        }
        public bool isFullScreen { get; set; } = true;
        private Rect originalWindowRect;
        private void ToggleFullScreen()
        {
            Window parentWindow = Window.GetWindow(this);
            var workArea = SystemParameters.WorkArea;
            if (parentWindow != null && isFullScreen == false)
            {

                parentWindow.Left = workArea.Left;
                parentWindow.Top = workArea.Top;
                parentWindow.Width = workArea.Width;
                parentWindow.Height = workArea.Height;
                parentWindow.Topmost = true;
                parentWindow.Topmost = false;
                isFullScreen = true;
                return;
            }
            if (parentWindow != null && isFullScreen == true)
            {
                originalWindowRect = new Rect(parentWindow.Left, parentWindow.Top, parentWindow.Width, parentWindow.Height);

                // Set the window to fullscreen
                double newWidth = parentWindow.MinWidth == 0 ? 500 : parentWindow.MinWidth;
                double newHeight = parentWindow.MinHeight == 0 ? 500 : parentWindow.MinHeight;

                parentWindow.Width = newWidth;
                parentWindow.Height = newHeight;
                parentWindow.Left = workArea.Left + (workArea.Width - newWidth) / 2;
                parentWindow.Top = workArea.Top + (workArea.Height - newHeight) / 2;
                parentWindow.Topmost = false;
                isFullScreen = false;
                return;
            }

        }
    }
}

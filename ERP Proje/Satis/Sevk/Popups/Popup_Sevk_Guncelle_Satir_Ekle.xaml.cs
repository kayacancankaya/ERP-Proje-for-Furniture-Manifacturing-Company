using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using static Layer_Business.Cls_Base;

namespace Layer_UI.Satis.Sevk.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Guncelle_Satir_Ekle.xaml
    /// </summary>
    public partial class Popup_Sevk_Guncelle_Satir_Ekle : Window
    {
        public ObservableCollection<Cls_Sevk> OrdersCollection { get; set; }
        public ObservableCollection<Cls_Sevk> ShipmentsCollection { get; set; }
        public ObservableCollection<Cls_Sevk> WeightCollection { get; set; }


        Variables variables = new();
        Cls_Sevk sevk = new();

        string siparisNo = string.Empty, sevkEmriNo = string.Empty;
        int siparisSira = 0;
        public Popup_Sevk_Guncelle_Satir_Ekle(string sevkEmriNo)
        {
            InitializeComponent();

            Mouse.OverrideCursor = Cursors.Wait;

            OrdersCollection = new ObservableCollection<Cls_Sevk>();
            ShipmentsCollection = new ObservableCollection<Cls_Sevk>();
            WeightCollection = new ObservableCollection<Cls_Sevk>();
            dg_SiparisSecim.ItemsSource = OrdersCollection;
            dg_SevkEkle.ItemsSource = ShipmentsCollection;

            ShipmentsCollection.Clear();


            var existingShipment = sevk.PopulatePopupToBeUpdatedSevkEmri(sevkEmriNo);
            foreach (var shipment in existingShipment)
            {

                ShipmentsCollection.Add(shipment);
            }

            if (!ShipmentsCollection.Any())
            {
                CRUDmessages.QueryIsEmpty("Sevk");
                Mouse.OverrideCursor = null;
                return;
            }

            CultureInfo culture = CultureInfo.CurrentCulture;
            txt_sevk_tarihi.Language = XmlLanguage.GetLanguage(culture.Name);

            txt_plaka.Text = ShipmentsCollection.Select(x => x.PlakaNo).FirstOrDefault();
            txt_siparis_aciklama.Text = ShipmentsCollection.Select(x => x.SevkAciklama).FirstOrDefault();
            txt_sofor_isim.Text = ShipmentsCollection.Select(x => x.SoforIsim).FirstOrDefault();
            txt_toplam_agirlik.Text = ShipmentsCollection.Select(x => x.SevkAgirlik.ToString()).FirstOrDefault();
            txt_toplam_hacim.Text = ShipmentsCollection.Select(x => x.SevkHacim.ToString()).FirstOrDefault();

            Mouse.OverrideCursor = null;
        }

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;

                variables.ErrorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(txt_siparis_no.Text) &&
                   string.IsNullOrWhiteSpace(txt_cari_adi.Text) &&
                   string.IsNullOrWhiteSpace(txt_stok_kodu.Text) &&
                   string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                {
                    CRUDmessages.GeneralFailureMessageNoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                Dictionary<string, string> kisitPairs = new Dictionary<string, string>();

                kisitPairs.Add("siparisNo", txt_siparis_no.Text);
                kisitPairs.Add("stokKodu", txt_stok_kodu.Text);
                kisitPairs.Add("stokAdi", txt_stok_adi.Text);
                kisitPairs.Add("cariAdi", txt_cari_adi.Text);

                OrdersCollection.Clear();

                var newOrders = sevk.PopulateSevkEmriOlusturulacakList(kisitPairs);
                foreach (var order in newOrders)
                {
                    OrdersCollection.Add(order);
                }

                if (!OrdersCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty("Sipariş");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_SiparisSecim.ItemsSource = OrdersCollection;

                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_Sevk> productVolumeAndWeight = new();
                variables.ErrorMessage = string.Empty;
                variables.WarningMessage = string.Empty;

                int miktar = 0, depoMiktar = 0, siparisMiktar = 0, teslimMiktar = 0, acikSevkMiktar = 0;
                string uretimDurum = string.Empty;

                Cls_Sevk dataItem = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);

                miktar = dataItem.SevkMiktar;
                depoMiktar = dataItem.DepoMiktar;
                siparisMiktar = dataItem.SiparisMiktar;
                teslimMiktar = dataItem.TeslimMiktar;
                acikSevkMiktar = dataItem.AcikSevkMiktar;
                uretimDurum = Enum.GetName(typeof(UretimDurum), dataItem.UretimDurum);

                variables.ErrorMessage = miktar == 0 ? variables.ErrorMessage + "Miktar 0 Olamaz.\n" : variables.ErrorMessage;
                variables.WarningMessage = uretimDurum != "Uretildi" ? variables.WarningMessage + "Seçilen Ürünün Üretimi Tamamlanmadı.\n" : variables.WarningMessage;
                variables.ErrorMessage = depoMiktar < (miktar + acikSevkMiktar) ? variables.ErrorMessage + "Depo Bakiyesi Yeterli Değil.\n" : variables.ErrorMessage;
                variables.ErrorMessage = siparisMiktar - (teslimMiktar + acikSevkMiktar) < miktar ? variables.ErrorMessage + "Sevk Miktarı Sipariş Miktarından Fazla.\n" : variables.ErrorMessage;

                if (!string.IsNullOrEmpty(variables.ErrorMessage)) { CRUDmessages.GeneralFailureMessageCustomMessage(variables.ErrorMessage); return; };
                if (!string.IsNullOrEmpty(variables.WarningMessage))
                {
                    variables.Result = CRUDmessages.DoYouWishToContinue(variables.WarningMessage);
                    if (!variables.Result) { return; };

                };

                int sevkSira = ShipmentsCollection.Any() ? ShipmentsCollection.Select(item => item.SevkSira).Max() + 1 : 1;

                Cls_Sevk? siparisItem = new Cls_Sevk
                {
                    SevkSira = sevkSira,
                    CariKodu = dataItem.CariKodu,
                    CariAdi = dataItem.CariAdi,
                    SiparisKodu = dataItem.SiparisKodu,
                    SiparisSira = dataItem.SiparisSira,
                    UrunAdi = dataItem.UrunAdi,
                    UrunKodu = dataItem.UrunKodu,
                    SevkMiktar = dataItem.SevkMiktar,
                };

                if (sevkSira > 1)
                {
                    variables.ResultInt = EklemeKontrol(siparisItem, ShipmentsCollection);

                    if (variables.ResultInt == 1)
                    {
                        productVolumeAndWeight = sevk.GetProductVolumeAndWeight(siparisItem.UrunKodu);
                        siparisItem.UrunHacim = productVolumeAndWeight[0].UrunHacim;
                        siparisItem.UrunAgirlik = productVolumeAndWeight[0].UrunAgirlik;
                        ShipmentsCollection.Add(siparisItem);
                    }
                    if (variables.ResultInt == 2)
                    { CRUDmessages.GeneralFailureMessageCustomMessage("Birden Fazla Aynı Ürün Eklenemez."); return; }
                    if (variables.ResultInt == 3)
                    { CRUDmessages.GeneralFailureMessageCustomMessage("Birden Fazla Farklı Cari Eklenemez."); return; }
                    if (variables.ResultInt == 4)
                    { CRUDmessages.GeneralFailureMessage("Sipariş Eklenmek Üzere Kontrol Edilirken"); return; }

                }

                if (sevkSira == 1)
                {
                    productVolumeAndWeight = sevk.GetProductVolumeAndWeight(siparisItem.UrunKodu);
                    siparisItem.UrunHacim = productVolumeAndWeight[0].UrunHacim;
                    siparisItem.UrunAgirlik = productVolumeAndWeight[0].UrunAgirlik;
                    ShipmentsCollection.Add(siparisItem);

                }

                CalculateAllTotalValues(ShipmentsCollection);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        private void btn_siparis_sil(object sender, RoutedEventArgs e)
        {
            variables.Result = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (variables.Result)
                {
                    variables.ErrorMessage = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                    Cls_Sevk dataItem = row.Item as Cls_Sevk;

                    if (ShipmentsCollection.Count == 1)
                    { CRUDmessages.GeneralFailureMessageCustomMessage("En Az Bir Sevk Satırı Olmalı."); Mouse.OverrideCursor = null; return; }

                    if (!string.IsNullOrEmpty(dataItem.SevkEmriNo))
                        variables.Result = sevk.DeleteYuklenmemisSevkSatir(dataItem.SevkEmriNo, dataItem.SiparisKodu, dataItem.SiparisSira);

                    if (!variables.Result)
                    { CRUDmessages.DeleteFailureMessage("Sevk"); Mouse.OverrideCursor = null; return; }


                    ShipmentsCollection.Remove(dataItem);

                    dg_SevkEkle.ItemsSource = ShipmentsCollection;

                    dg_SevkEkle.Items.Refresh();

                    CalculateAllTotalValues(ShipmentsCollection);

                    string sevkEmriTarihiText = txt_sevk_tarihi.Text;
                    DateTime sevkEmriTarihi;
                    DateTime now;
                    string formattedTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime.TryParse(formattedTimeNow, out now);

                    foreach (var item in ShipmentsCollection)
                    {


                        if (DateTime.TryParse(sevkEmriTarihiText, out sevkEmriTarihi))
                            item.SevkEmriTarihi = sevkEmriTarihi;
                        else
                            item.SevkEmriTarihi = DateTime.Now;

                        item.SevkHacim = Convert.ToSingle(txt_toplam_hacim.Text);
                        item.SevkAgirlik = Convert.ToSingle(txt_toplam_agirlik.Text);
                        item.SoforIsim = txt_sofor_isim.Text;
                        item.PlakaNo = txt_plaka.Text;
                        item.SevkAciklama = txt_siparis_aciklama.Text;
                    }

                    variables.IsTrue = sevk.UpdateSevkMas(ShipmentsCollection);
                    if (!variables.IsTrue)
                    {
                        CRUDmessages.GeneralFailureMessage("Sevk Satırı Silindi Ancak Sevk Genel Bilgileri Güncellenirken");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    CRUDmessages.DeleteSuccessMessage("Sevk");

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_siparis_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;
                variables.WarningMessage = string.Empty;
                variables.Continue = true;

                if (!ShipmentsCollection.Any())
                    variables.ErrorMessage += "Eklenecek Sipariş Bulunamadı.\n";

                if (!string.IsNullOrEmpty(variables.WarningMessage))
                {
                    variables.Result = CRUDmessages.DoYouWishToContinue("Sevk Emri Güncellenecek");

                    if (!variables.Result) return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                CalculateAllTotalValues(ShipmentsCollection);

                string formattedDate = txt_sevk_tarihi.Text.Length >= 10 ? txt_sevk_tarihi.Text.Substring(0, 10) : formattedDate = txt_sevk_tarihi.Text;

                DateTime sevkEmriTarihi;

                string formattedTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime now;
                DateTime.TryParse(formattedTimeNow, out now);

                foreach (var item in ShipmentsCollection)
                {

                    if (DateTime.TryParse(formattedDate, out sevkEmriTarihi))
                        item.SevkEmriTarihi = sevkEmriTarihi;
                    else
                        item.SevkEmriTarihi = now;

                    item.SevkHacim = Convert.ToSingle(txt_toplam_hacim.Text);
                    item.SevkAgirlik = Convert.ToSingle(txt_toplam_agirlik.Text);
                    item.SoforIsim = txt_sofor_isim.Text;
                    item.PlakaNo = txt_plaka.Text;
                    item.SevkAciklama = txt_siparis_aciklama.Text;
                }

                variables.Result = sevk.InsertShipmentsToExistingOrder(ShipmentsCollection);
                if (variables.Result)
                    CRUDmessages.InsertSuccessMessage("Sevk");
                else
                { CRUDmessages.InsertFailureMessage("Sevk"); Mouse.OverrideCursor = null; return; }

                sevkEmriNo = ShipmentsCollection.Select(item => item.SevkEmriNo).FirstOrDefault();
                Popup_Sevk_Guncelle_Satir_Ekle _popup = new Popup_Sevk_Guncelle_Satir_Ekle(sevkEmriNo);
                _popup.ShowDialog();
                Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sevk Bilgileri Kaydedilirken"); Mouse.OverrideCursor = null; return;
            }


        }
        private int EklemeKontrol(Cls_Sevk sevkItem, ObservableCollection<Cls_Sevk> addedCollection)
        {
            try
            {
                if (addedCollection.Any())
                {
                    foreach (Cls_Sevk item in addedCollection)
                    {
                        if (item.SiparisKodu == sevkItem.SiparisKodu &&
                            item.SiparisSira == sevkItem.SiparisSira)
                            return 2;

                    }

                    if (addedCollection.Select(item => item.CariAdi).FirstOrDefault() != sevkItem.CariAdi)
                        return 3;

                }

                return 1;
            }
            catch
            {
                return 4;
            }
        }

        float totalWeight = 0;
        float totalVolume = 0;

        private void CalculateAllTotalValues(ObservableCollection<Cls_Sevk> addedCollection)
        {
            totalWeight = 0;
            totalVolume = 0;

            foreach (Cls_Sevk item in addedCollection)
            {
                totalWeight += (item.UrunAgirlik * item.SevkMiktar);
                totalVolume += (item.UrunHacim * item.SevkMiktar);

            }
            txt_toplam_agirlik.Text = totalWeight.ToString("0.######", CultureInfo.InvariantCulture);
            txt_toplam_hacim.Text = totalVolume.ToString("0.######", CultureInfo.InvariantCulture);
        }
        private void Volume_column_lost_focus(object sender, RoutedEventArgs e)
        {
            CalculateTotalVolume(ShipmentsCollection);
        }
        private void CalculateTotalVolume(ObservableCollection<Cls_Sevk> addedCollection)
        {
            totalVolume = 0;

            foreach (Cls_Sevk item in addedCollection)
            {
                totalVolume += item.UrunHacim * item.SevkMiktar;
            }
            txt_toplam_hacim.Text = totalVolume.ToString();
        }

        private void Weight_column_lost_focus(object sender, RoutedEventArgs e)
        {
            CalculateTotalWeight(ShipmentsCollection);
        }
        private void CalculateTotalWeight(ObservableCollection<Cls_Sevk> addedCollection)
        {
            totalWeight = 0;

            foreach (Cls_Sevk item in addedCollection)
            {
                totalWeight += item.UrunAgirlik * item.SevkMiktar;

            }
            txt_toplam_agirlik.Text = totalWeight.ToString();
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContex associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {
                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up
                }
            }

        }
    }
}

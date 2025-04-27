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

namespace Layer_UI.Satis.Sevk
{
    /// <summary>
    /// Interaction logic for Frm_SSH_MT_Sevk_Et.xaml
    /// </summary>
    public partial class Frm_SSH_MT_Sevk_Et : Window
    {
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
        public ObservableCollection<Cls_Sevk> OrdersCollection { get; set; }
        public ObservableCollection<Cls_Sevk> ShipmentsCollection { get; set; }
        public Frm_SSH_MT_Sevk_Et()
        {
            InitializeComponent(); Window_Loaded();
            OrdersCollection = new ObservableCollection<Cls_Sevk>();
            ShipmentsCollection = new ObservableCollection<Cls_Sevk>();
            dg_SiparisSecim.ItemsSource = OrdersCollection;
            dg_SevkEkle.ItemsSource = ShipmentsCollection;


            // Set the Language property for the DatePicker
            CultureInfo culture = CultureInfo.CurrentCulture;
            txt_sevk_tarihi.Language = XmlLanguage.GetLanguage(culture.Name);
        }

        Variables variables = new();
        Cls_Sevk sevk = new();
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
                    variables.ErrorMessage = "Hiç Değer Girmediniz.";
                    CRUDmessages.GeneralFailureMessageCustomMessage(variables.ErrorMessage);
                    Mouse.OverrideCursor = null;
                    return;
                }


                Dictionary<string, string> kisitPairs = new Dictionary<string, string>();

                kisitPairs.Add("siparisNo", txt_siparis_no.Text);
                kisitPairs.Add("stokKodu", txt_stok_kodu.Text);
                kisitPairs.Add("stokAdi", txt_stok_adi.Text);
                kisitPairs.Add("cariAdi", txt_cari_adi.Text);

                OrdersCollection.Clear();

                var newOrders = sevk.PopulateSevkEmriOlusturulacakList_SSH(kisitPairs);
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
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Yapılırken");
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

                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Sipariş Eklerken Hata İle Karşılaşıldı."); return; }

                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                variables.ErrorMessage = row == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;

                // Get the data item associated with the row
                Cls_Sevk? dataItem = row.Item as Cls_Sevk;
                int miktar = 0, depoMiktar = 0, siparisMiktar = 0, teslimMiktar = 0, acikSevkMiktar = 0;
                string uretimDurum = string.Empty;

                variables.ErrorMessage = dataItem == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;
                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; };


                miktar = dataItem.SevkMiktar;
                depoMiktar = dataItem.DepoMiktar;
                siparisMiktar = dataItem.SiparisMiktar;
                teslimMiktar = dataItem.TeslimMiktar;
                acikSevkMiktar = dataItem.AcikSevkMiktar;
                uretimDurum = Enum.GetName(typeof(UretimDurum), dataItem.UretimDurum);

                variables.ErrorMessage = miktar == 0 ? variables.ErrorMessage + "Miktar 0 Olamaz.\n" : variables.ErrorMessage;
                variables.WarningMessage = uretimDurum != "Uretildi" ? variables.WarningMessage + "Seçilen Ürünün Üretimi Tamamlanmadı." : variables.WarningMessage;
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
                        if (productVolumeAndWeight.Count > 0)
                        {
                            siparisItem.UrunHacim = productVolumeAndWeight[0].UrunHacim;
                            siparisItem.UrunAgirlik = productVolumeAndWeight[0].UrunAgirlik;
                        }

                        else
                        {
                            siparisItem.UrunHacim = 0; ;
                            siparisItem.UrunAgirlik = 0;
                        }

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
                    if (productVolumeAndWeight.Any())
                    {
                        siparisItem.UrunHacim = productVolumeAndWeight[0].UrunHacim;
                        siparisItem.UrunAgirlik = productVolumeAndWeight[0].UrunAgirlik;
                    }
                    else
                    {
                        siparisItem.UrunHacim = 0;
                        siparisItem.UrunAgirlik = 0;
                    }

                    ShipmentsCollection.Add(siparisItem);
                    stack_panel_sevk_kaydet.Visibility = Visibility.Visible;
                    dg_SevkEkle.Visibility = Visibility.Visible;
                    stack_panel_sevk_ilave_bilgiler.Visibility = Visibility.Visible;
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
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                    Cls_Sevk dataItem = row.Item as Cls_Sevk;

                    ShipmentsCollection.Remove(dataItem);

                    dg_SevkEkle.ItemsSource = ShipmentsCollection;

                    dg_SevkEkle.Items.Refresh();

                    CalculateAllTotalValues(ShipmentsCollection);

                    Mouse.OverrideCursor = null;
                }
                else
                {
                    Mouse.OverrideCursor = null; return;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }

        }
        private void btn_siparis_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                variables.ErrorMessage = string.Empty;
                variables.WarningMessage = string.Empty;
                variables.Continue = true;
                Mouse.OverrideCursor = Cursors.Wait;

                if (!ShipmentsCollection.Any())
                    variables.ErrorMessage += "Eklenecek Sipariş Bulunamadı.\n";

                if (string.IsNullOrWhiteSpace(txt_siparis_aciklama.Text))
                    variables.WarningMessage += "Açıklama Hanesi Boş.\n";

                if (string.IsNullOrWhiteSpace(txt_toplam_hacim.Text) || txt_toplam_hacim.Text == "0")
                    variables.WarningMessage += "Hacim Hanesi Boş.\n";

                if (string.IsNullOrWhiteSpace(txt_toplam_agirlik.Text) || txt_toplam_agirlik.Text == "0")
                    variables.WarningMessage += "Ağırlık Hanesi Boş.\n";

                if (string.IsNullOrWhiteSpace(txt_sofor_isim.Text) || string.IsNullOrWhiteSpace(txt_plaka.Text))
                    variables.WarningMessage += "Nakliye Bilgileri Boş.\n";


                if (!string.IsNullOrEmpty(variables.ErrorMessage))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(variables.ErrorMessage);
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (!string.IsNullOrEmpty(variables.WarningMessage))
                {
                    variables.Result = CRUDmessages.DoYouWishToContinue(variables.WarningMessage);

                    if (!variables.Result)
                    {
                        Mouse.OverrideCursor = null; return;
                    }
                }

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

                string result = sevk.InsertShipmentsSSH(ShipmentsCollection);
                if (result == "Başarı")
                    CRUDmessages.InsertSuccessMessage("Sevk");
                else
                {
                    variables.Result = sevk.DeleteYuklemeKaydiGeriAl(ShipmentsCollection, result);
                    CRUDmessages.InsertFailureMessage("Sevk");
                    if (!variables.Result)
                        CRUDmessages.GeneralFailureMessageCustomMessage("Kayıtlar Geri Alınırken Problem İle Karşılaşıldı.\n" +
                                                                        "Veri Bütünlüğü Bozulmuş Olabilir.\n " +
                                                                        "Bilgi İşlem Personeline Haber Veriniz.");

                }
                Mouse.OverrideCursor = null;
                Frm_SSH_MT_Sevk_Et _frm = new();
                _frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sevk Bilgileri Kaydedilirken");
                Mouse.OverrideCursor = null;

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
                totalVolume += (item.UrunHacim * item.SevkMiktar);
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
                totalWeight += (item.UrunAgirlik * item.SevkMiktar);

            }
            txt_toplam_agirlik.Text = totalWeight.ToString();
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
                            if (dataGrid.Columns[i].Header.ToString() == "Sevk Miktar" ||
                                dataGrid.Columns[i].Header.ToString() == "Hacim" ||
                                dataGrid.Columns[i].Header.ToString() == "Ağırlık")
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

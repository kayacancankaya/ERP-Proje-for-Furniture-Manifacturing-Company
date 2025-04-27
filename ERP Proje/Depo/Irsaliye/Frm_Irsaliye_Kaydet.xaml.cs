using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Depo.Irsaliye
{
    /// <summary>
    /// Interaction logic for Frm_Irsaliye_Kaydet.xaml
    /// </summary>
    public partial class Frm_Irsaliye_Kaydet : Window
    {
        private Cls_Siparis cls_siparis = new();
        private Cls_Cari cari = new();
        private Cls_Depo depo = new();
        private ObservableCollection<Cls_Siparis> siparisCollection = new();
        private ObservableCollection<Cls_Siparis> irsaliyeCollection = new();
        public Frm_Irsaliye_Kaydet()
        {
            InitializeComponent(); Window_Loaded();

        }
        public Frm_Irsaliye_Kaydet(string irsaliyeNumarasi, string cariKodu, string cariAdi)
        {
            InitializeComponent(); Window_Loaded();

            txt_irsaliye_numarasi.Text = irsaliyeNumarasi;
            txt_tedarikci_cari_kodu.Text = cariKodu;
            txt_tedarikci_cari_adi.Text = cariAdi;
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
        private void btn_numara_guncelle_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                string result = cari.GetIrsaliyeNoFromCari(txt_tedarikci_cari_kodu.Text);
                if (result == null)
                {
                    CRUDmessages.GeneralFailureMessage("Numara Getirilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                txt_irsaliye_numarasi.Text = result;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Numara Getirilirken");
            }
        }
        private void Show_Tedarikci_Secim_Clicked(object sender, RoutedEventArgs e)
        {
            var cariSecimWindow = new Popup_Irsaliye_Cari_Secim(txt_irsaliye_numarasi.Text);
            // Show the popup window
            cariSecimWindow.ShowDialog();
        }
        private void btn_siparis_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Variables.ErrorMessage_ = string.Empty;

                if (string.IsNullOrEmpty(txt_tedarikci_cari_kodu.Text) || txt_tedarikci_cari_kodu.Text == "Kayıt Yok") Variables.ErrorMessage_ += "Teslim Cari Kodu Eksik.\n";
                if (string.IsNullOrEmpty(txt_tedarikci_cari_adi.Text) || txt_tedarikci_cari_adi.Text == "Kayıt Yok") Variables.ErrorMessage_ += "Teslim Cari Adı Eksik.\n";


                if (string.IsNullOrEmpty(Variables.ErrorMessage_) == false) { MessageBox.Show(Variables.ErrorMessage_); Variables.ErrorMessage_ = string.Empty; Mouse.OverrideCursor = null; return; }

                dg_siparis_secim.ItemsSource = null;
                dg_siparis_secim.Items.Clear();
                Mouse.OverrideCursor = Cursors.Wait;

                Dictionary<string, string> constraints = new Dictionary<string, string>();

                constraints.Add("cariKodu", txt_tedarikci_cari_kodu.Text);
                constraints.Add("siparisNumarasi", txt_siparis_numarasi.Text);
                constraints.Add("siparisSira", txt_siparis_sira.Text);
                constraints.Add("stokAdi", txt_stok_adi.Text);
                constraints.Add("stokKodu", txt_stok_kodu.Text);

                siparisCollection = cls_siparis.PopulateTedarikciSiparisListele(constraints);
                if(siparisCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Listelenirken"); Mouse.OverrideCursor = null;
                    return;
                }
                if(siparisCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_siparis_secim.ItemsSource = cls_siparis.SiparisCollection;
                
                Mouse.OverrideCursor = null;
            }

            catch { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Listelenirken"); Mouse.OverrideCursor = null; }
        }

        private void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Variables.ErrorMessage_ = string.Empty;

                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Sipariş Eklerken Hata İle Karşılaşıldı.\n"); return; }

                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                Variables.ErrorMessage_ += row == null ? "Hata ile Karşılaşıldı.\n" : Variables.ErrorMessage_;

                // Get the data item associated with the row
                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                Variables.ErrorMessage_ += dataItem == null ? "Hata ile Karşılaşıldı.\n" : Variables.ErrorMessage_;
                if (string.IsNullOrEmpty(Variables.ErrorMessage_) == false) { MessageBox.Show(Variables.ErrorMessage_); return; };

                Variables.ErrorMessage_ += dataItem.TedarikGirisMiktar == 0 ? "Miktar 0 Olamaz.\n" : Variables.ErrorMessage_;

                if (dataItem.TedarikGirisMiktar > dataItem.TedarikSiparisBakiye)
                {
                    Variables.Result_ = cls_siparis.CheckIfFazTesExceed(dataItem.TedarikSiparisBakiye, dataItem.TedarikGirisMiktar, dataItem.StokKodu,dataItem.Kod1,dataItem.Birim);
                    if (!Variables.Result_)
                        Variables.ErrorMessage_ += "Giriş Miktarı Fazla Teslim Oranı ile Sipariş Bakiyesinin Toplamından Büyük Olamaz.\n";
                }
                Variables.Result_ = depo.CheckIfDepoKoduExists(dataItem.DepoKodu);
                if (!Variables.Result_)
                    Variables.ErrorMessage_ += "Depo Kodu Bulunamadı.\n";


                if (string.IsNullOrEmpty(Variables.ErrorMessage_) == false) { MessageBox.Show(Variables.ErrorMessage_); return; };

                Cls_Siparis? siparis = new Cls_Siparis
                {
                    StokKodu = dataItem.StokKodu,
                    StokAdi = dataItem.StokAdi,
                    Fisno = dataItem.Fisno,
                    FisSira = dataItem.FisSira,
                    TedarikGirisMiktar = dataItem.TedarikGirisMiktar,
                    DepoKodu = dataItem.DepoKodu,
                    SiparisTarih = dataItem.SiparisTarih.Substring(0, 11),
                    SiparisFiyat = dataItem.SiparisFiyat,

                };
                siparis.AssociatedCari.TedarikciCariKodu = dataItem.AssociatedCari.TedarikciCariKodu;
                Variables.ResultInt_ = EklemeKontrol(siparis, irsaliyeCollection);
                if (Variables.ResultInt_ == -1)
                { CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Kontrol Edilirken"); Mouse.OverrideCursor = null; return; }
                if (Variables.ResultInt_ == 2)
                { CRUDmessages.GeneralFailureMessageCustomMessage("Farklı Sipariş Numarasından Kayıt Yapılamaz."); Mouse.OverrideCursor = null; return; }
                if (Variables.ResultInt_ == 3)
                { CRUDmessages.GeneralFailureMessage("Farklı Cari İle Kayıt Yapılamaz."); Mouse.OverrideCursor = null; return; }
                if (Variables.ResultInt_ == 4)
                { CRUDmessages.GeneralFailureMessage("Aynı Sipariş Birden Fazla Eklenemez."); Mouse.OverrideCursor = null; return; }

                if (Variables.ResultInt_ == 1)
                    irsaliyeCollection.Add(siparis);

                dg_SiparisEkle.ItemsSource = irsaliyeCollection;

                dg_SiparisEkle.Visibility = Visibility.Visible;
                stack_panel_irsaliye_kaydet.Visibility = Visibility.Visible;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

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
                            if (dataGrid.Columns[i].Header.ToString() == "Miktar")
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
        private void btn_irsaliye_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            var result = CRUDmessages.InsertOnayMessage();
            if (!result)
                return;

            if (dp_irsaliye_tarih.SelectedDate == null)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("İrsaliye Tarihi Giriniz.");
                return;

            }

            Variables.ErrorMessage_ = string.Empty;
            Variables.WarningMessage_ = string.Empty;


            if (string.IsNullOrEmpty(txt_irsaliye_numarasi.Text)) Variables.ErrorMessage_ += "İrsaliye Numarası Eksik.\n";
            if (string.IsNullOrEmpty(txt_tedarikci_cari_kodu.Text)) Variables.ErrorMessage_ += "Cari Kodu Eksik.\n";

            Mouse.OverrideCursor = Cursors.Wait;

            if (!string.IsNullOrEmpty(txt_irsaliye_numarasi.Text))
            {
                Variables.ResultInt_ = cls_siparis.CheckIfIrsaliyeNoExists(txt_irsaliye_numarasi.Text, "Vita");
                if (Variables.ResultInt_ == -1)
                { CRUDmessages.GeneralFailureMessage("İrsaliye Numarası Kontrolü Yapılırken"); Mouse.OverrideCursor = null; return; }
                if (Variables.ResultInt_ > 0)
                { Variables.ErrorMessage_ = Variables.ErrorMessage_ + "İrsaliye Numarası İle Kayıt Mevcut.\n"; Mouse.OverrideCursor = null; }
            }

            if (string.IsNullOrEmpty(Variables.ErrorMessage_) == false)
            { MessageBox.Show(Variables.ErrorMessage_); Variables.ErrorMessage_ = string.Empty; Mouse.OverrideCursor = null; return; }

            Variables.QumulativeSum_ = 0;
            Variables.Counter_ = 0;
            double kdv = 0;
            double qumulativeKdv = 0;

            foreach (Cls_Siparis item in dg_SiparisEkle.Items)
            {
                try
                {
                    if (string.IsNullOrEmpty(item.StokKodu)) Variables.ErrorMessage_ = Variables.ErrorMessage_ + string.Format("{0}. Satır Stok Kodu Boş Olamaz.\n", Variables.Counter_ + 1);
                    if (string.IsNullOrEmpty(item.Fisno)) Variables.ErrorMessage_ = Variables.ErrorMessage_ + string.Format("{0}. Satır Fiş Numarası Boş Olamaz.\n", Variables.Counter_ + 1);
                    if (item.FisSira is 0) Variables.ErrorMessage_ = Variables.ErrorMessage_ + string.Format("{0}. Satır Sipariş Sıra 0 olamaz.\n", Variables.Counter_ + 1);
                    if (string.IsNullOrEmpty(item.StokKodu)) Variables.ErrorMessage_ = Variables.ErrorMessage_ + string.Format("{0}. Satır Stok Kodu Eksik.\n", Variables.Counter_ + 1);
                    Variables.Result_ = depo.CheckIfDepoKoduExists(item.DepoKodu);
                    if (!Variables.Result_)
                        Variables.ErrorMessage_ += "Depo Kodu Bulunamadı.\n";

                    if (item.TedarikGirisMiktar is 0) Variables.ErrorMessage_ = Variables.ErrorMessage_ + string.Format("{0}. Satır Miktar 0 Olamaz.\n", Variables.Counter_ + 1);

                    if (string.IsNullOrEmpty(Variables.ErrorMessage_) == false) { MessageBox.Show(Variables.ErrorMessage_); Variables.ErrorMessage_ = string.Empty; Mouse.OverrideCursor = null; return; }


                    Variables.QumulativeSum_ += Convert.ToSingle((item.SiparisFiyat * item.TedarikGirisMiktar));
                    kdv = cls_siparis.GetKdvOrani(item.Fisno, item.StokKodu);
                    qumulativeKdv = qumulativeKdv + (item.SiparisFiyat * (kdv / 100) * item.TedarikGirisMiktar);
                    Variables.Counter_++;

                }

                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; return; }

            }

            Variables.Result_ = cls_siparis.InsertTedarikSiparisGenel(txt_irsaliye_numarasi.Text, txt_tedarikci_cari_kodu.Text, Variables.QumulativeSum_,
                                                                     qumulativeKdv, Variables.Counter_, irsaliyeCollection.OrderBy(x => x.Fisno).Select(x => x.Fisno).FirstOrDefault(),
                                                                     dp_irsaliye_tarih.SelectedDate.Value);
            if (!Variables.Result_)
            {
                KayitGeriAl(txt_irsaliye_numarasi.Text.PadLeft(15, '0'));
                CRUDmessages.GeneralFailureMessage("İrsaliye Genel Bilgileri Kaydedilirken");
                Mouse.OverrideCursor = null;
                return;
            }

            Variables.SuccessMessage_ = "İrsaliye Genel Bilgileri Kaydedildi.\n";
            Variables.Counter_ = 0;

            foreach (Cls_Siparis item in dg_SiparisEkle.Items)
            {
                try
                {

                    Variables.Counter_++;

                    if (Variables.Counter_ == 1)
                    {
                        Variables.Result_ = cls_siparis.InsertTedarikSiparisSatir(item.StokKodu, item.TedarikGirisMiktar, item.SiparisFiyat,
                                                                             txt_tedarikci_cari_kodu.Text, Variables.Counter_, txt_irsaliye_numarasi.Text,
                                                                             item.Fisno, item.FisSira, item.DepoKodu,
                                                                             dp_irsaliye_tarih.SelectedDate.Value);
                        if (Variables.Result_ == false)
                        {
                            KayitGeriAl(txt_irsaliye_numarasi.Text);
                            CRUDmessages.GeneralFailureMessage("İrsaliye Genel Bilgileri Kaydedilirken");
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }
                    else
                    {
                        Variables.Result_ = cls_siparis.InsertTedarikSiparisSatir(item.StokKodu, item.TedarikGirisMiktar, item.SiparisFiyat,
                                                                             txt_tedarikci_cari_kodu.Text, Variables.Counter_, txt_irsaliye_numarasi.Text,
                                                                             item.Fisno, item.FisSira, item.DepoKodu, dp_irsaliye_tarih.SelectedDate.Value);
                        if (!Variables.Result_)
                        {
                            KayitGeriAl(txt_irsaliye_numarasi.Text);
                            CRUDmessages.GeneralFailureMessage("İrsaliye Genel Bilgileri Kaydedilirken");
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }
                }
                catch
                {
                    KayitGeriAl(txt_irsaliye_numarasi.Text);
                    CRUDmessages.GeneralFailureMessage("İrsaliye Genel Bilgileri Kaydedilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

            }

            if (Variables.ResultInt_ != -1)
            {
                Variables.SuccessMessage_ += "İrsaliye Satırları Kaydedildi.";
                MessageBox.Show(Variables.SuccessMessage_, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = null;
                Frm_Irsaliye_Kaydet frm = new Frm_Irsaliye_Kaydet();
                frm.Show();
                this.Close();
            }

            else
            {
                KayitGeriAl(txt_irsaliye_numarasi.Text);
                CRUDmessages.GeneralFailureMessage("İrsaliye Genel Bilgileri Kaydedilirken");
                Mouse.OverrideCursor = null;
                return;
            }

        }
        private void btn_siparis_sil(object sender, RoutedEventArgs e)
        {
            Variables.Result_ = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (Variables.Result_)
                {
                    Variables.ErrorMessage_ = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                    Cls_Siparis dataItem = row.Item as Cls_Siparis;

                    irsaliyeCollection.Remove(dataItem);

                    dg_SiparisEkle.ItemsSource = irsaliyeCollection;

                    dg_SiparisEkle.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private int EklemeKontrol(Cls_Siparis siparisItem, ObservableCollection<Cls_Siparis> siparisCollection)
        {
            try
            {

                int toplamSiparis = siparisCollection.Count();
                if (toplamSiparis > 0)
                {

                    if (siparisItem.AssociatedCari.TedarikciCariKodu != siparisCollection.Select(x => x.AssociatedCari.TedarikciCariKodu).FirstOrDefault())
                    {
                        return 3;
                    };
                }

                foreach (Cls_Siparis item in siparisCollection)
                {
                    if (item.StokKodu == siparisItem.StokKodu &&
                        item.StokAdi == siparisItem.StokAdi &&
                        item.Fisno == siparisItem.Fisno &&
                        item.FisSira == siparisItem.FisSira)
                        return 4;

                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        private void KayitGeriAl(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return;

                Variables.Result_ = cls_siparis.IrsaliyeGeriAl(fisno);
                if (Variables.Result_)
                { CRUDmessages.GeneralFailureMessageCustomMessage("İrsaliye İle Alakalı Kayıtlar Geri Alındı."); return; };
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("İrsaliye İle Alakalı Kayıtlar Geri Alınırken Problem İle Karşılaşıldı.\n" +
                                                                   "Veri Bütünlüğü Bozulmuş Olabilir.\n Bilgi İşlem Personeline Bilgi Veriniz.");
                    return;
                };
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(ex.ToString());
            }
        }

    }
}

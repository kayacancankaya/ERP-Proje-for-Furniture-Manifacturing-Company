using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Siparis.Popups;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Layer_Business.Cls_Base;

namespace Layer_UI.Satis.Siparis
{

    public partial class frm_musteri_secim : Window
    {
        int counterTuretilen = 0;
        bool varyantTureyecekMi = false;
        public delegate void DataUpdatedEventHandler(Cls_Cari cls_Cari);
        public event DataUpdatedEventHandler? DataUpdated;//cari popup kapandığında bilgileri aktarmak maksatlı
        public static frm_musteri_secim? CurrentInstance { get; private set; }

        Cls_Urun cls_urun = new Cls_Urun();
        Cls_Arge arge = new();
        private Cls_Siparis cls_siparis = new();
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
        public frm_musteri_secim()
        {
            InitializeComponent(); Window_Loaded();
            DataContext = Resources["cls_cari"];
            CurrentInstance = this;//popup kapandığında bilgileri aktarmak maksatlı

            cls_siparis = new Cls_Siparis();
            cls_siparis.SiparisCollection = new ObservableCollection<Cls_Siparis>();

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Show_Musteri_Secim_Clicked(object sender, RoutedEventArgs e)
        {
            var cariSecimWindow = new Popup_Cari_Secim();
            // Show the popup window
            cariSecimWindow.ShowDialog();
        }
        public void HandleDataUpdated(Cls_Cari cls_Cari)
        {
            txt_satis_cari_kodu.Text = cls_Cari.SatisCariKodu;
            txt_satis_cari_adi.Text = cls_Cari.SatisCariAdi;
            txt_teslim_cari_kodu.Text = cls_Cari.TeslimCariKodu;
            txt_teslim_cari_adi.Text = cls_Cari.TeslimCariAdi;
            txt_doviz_tipi.Text = cls_Cari.DovizTipi.ToString();
            txt_siparis_tipi.Text = cls_Cari.SiparisTipi.ToString();
        }
        public void RaiseDataUpdated(Cls_Cari cls_Cari)
        {

            DataUpdated?.Invoke(cls_Cari);
        }
        private void btn_urun_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

                if (string.IsNullOrEmpty(txt_satis_cari_kodu.Text)) variables.ErrorMessage = variables.ErrorMessage + "Satış Cari Kodu Eksik.\n";
                if (string.IsNullOrEmpty(txt_satis_cari_adi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Satış Cari Bilgileri Eksik.\n";
                if (string.IsNullOrEmpty(txt_teslim_cari_adi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Teslim Cari Adı Eksik.\n";
                if (string.IsNullOrEmpty(txt_teslim_cari_kodu.Text)) variables.ErrorMessage = variables.ErrorMessage + "Teslim Cari Kodu Eksik.\n";
                if (string.IsNullOrEmpty(txt_doviz_tipi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Döviz Tipi Eksik.\n";
                if (string.IsNullOrEmpty(txt_siparis_tipi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Sipariş Tipi Eksik.";

                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); variables.ErrorMessage = string.Empty; Mouse.OverrideCursor = null; return; }


                dg_UrunSecim.ItemsSource = null;
                dg_UrunSecim.Items.Clear();
                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrWhiteSpace(txt_urun_tipi.Text) && string.IsNullOrWhiteSpace(txt_model.Text) && string.IsNullOrWhiteSpace(txt_satis_sekil.Text) &&
                    string.IsNullOrEmpty(txt_urun_adi.Text) && string.IsNullOrEmpty(txt_sablon_kod.Text))
                {
                    CRUDmessages.GeneralFailureMessageNoInput(); Mouse.OverrideCursor = null; return;
                }


                Dictionary<string, string> constraints = new Dictionary<string, string>();

                constraints.Add("sablonKod", txt_sablon_kod.Text);
                constraints.Add("urunAdi", txt_urun_adi.Text);
                constraints.Add("urunTipi", txt_urun_tipi.Text);
                constraints.Add("model", txt_model.Text);
                constraints.Add("satisSekil", txt_satis_sekil.Text);

                cls_urun.UrunCollection = cls_urun.PopulateUrunListele(constraints);

                if (cls_urun.UrunCollection.Any())
                    dg_UrunSecim.ItemsSource = cls_urun.UrunCollection;
                else
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null;
                    return;
                }


                Mouse.OverrideCursor = null;

            }

            catch { CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null; }
        }

        Variables variables = new Variables();
        private async void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;
                Cls_Siparis siparis = new();
                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Ürün Eklerken Hata İle Karşılaşıldı."); return; }

                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                variables.ErrorMessage = row == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;


                // Get the data item associated with the row
                Cls_Urun? dataItem = row.Item as Cls_Urun;
                int miktar = 0;
                string stok_kodu = string.Empty;
                string stok_adi = string.Empty;
                string varyant_mi = string.Empty;

                variables.ErrorMessage = dataItem == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;
                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; };


                miktar = dataItem.UrunMiktar;
                varyant_mi = dataItem.VaryantVarMi;
                stok_kodu = dataItem.UrunKodu;
                stok_adi = dataItem.UrunAdi;

                variables.ErrorMessage = miktar == 0 ? "Miktar 0 Olamaz." : variables.ErrorMessage;
                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; };

                bool result = false;

                if (varyant_mi == "H")
                {
                    varyantTureyecekMi = false;
                    siparis.StokKodu = stok_kodu;
                    siparis.StokAdi = stok_adi;
                    siparis.SiparisMiktar = miktar;
                 
                    cls_siparis.SiparisCollection.Add(siparis);

                };

                string urunGrubuKodu = dataItem.UrunGrubuKodu;
                string modelKodu = dataItem.ModelKodu;
                string satisSekilKodu = dataItem.SatisSekilKodu;
                string urunGrubu = dataItem.UrunTipi;
                string model = dataItem.Model;
                string satisSekil = dataItem.SatisSekil;

                if (varyant_mi == "E")
                {
                    var varyantSecimWindow = new Popup_Varyant_Secim(urunGrubuKodu, modelKodu, satisSekilKodu, urunGrubu, model, satisSekil);
                    
                    var result_ = varyantSecimWindow.ShowDialog();

                    if (result_ == true && varyantSecimWindow.ValuesToTransfer != null)
                    {
                        // Access the transferred values
                        var (varyantKodu, varyantAdi) = varyantSecimWindow.ValuesToTransfer;


                        siparis.StokKodu = varyantKodu;
                        siparis.StokAdi = varyantAdi;
                            siparis.SiparisMiktar = miktar;
                        Cls_Arge arge = new();
                        if(varyantSecimWindow.KodTuretilecekMi)
                            varyantTureyecekMi = true;

                        cls_siparis.SiparisCollection.Add(siparis);

                    }
                }

                stack_panel_siparis_kaydet.Visibility = Visibility.Visible;
                dg_SiparisEkle.Visibility = Visibility.Visible;
                stack_panel_siparis_ilave_bilgiler.Visibility = Visibility.Visible;

                dg_SiparisEkle.ItemsSource = cls_siparis.SiparisCollection;
                if (varyantTureyecekMi)
                    await arge.UpdateTuremisKodTekAsync(siparis.StokKodu);
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
        private void btn_siparis_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            variables.ErrorMessage = string.Empty;
            variables.WarningMessage = string.Empty;
            variables.SuccessMessage = string.Empty;

            if (string.IsNullOrEmpty(txt_satis_cari_kodu.Text)) variables.ErrorMessage = variables.ErrorMessage + "Satış Cari Kodu Eksik.\n";
            if (string.IsNullOrEmpty(txt_teslim_cari_kodu.Text)) variables.ErrorMessage = variables.ErrorMessage + "Teslim Cari Kodu Eksik.\n";
            if (string.IsNullOrEmpty(txt_doviz_tipi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Döviz Tipi Eksik.\n";
            if (string.IsNullOrEmpty(txt_siparis_tipi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Sipariş Tipi Eksik.";
            if (string.IsNullOrEmpty(txt_POno.Text)) variables.ErrorMessage = variables.ErrorMessage + "PO Numarası Eksik.";
            if (string.IsNullOrEmpty(txt_destinasyon.Text)) variables.ErrorMessage = variables.ErrorMessage + "Destinasyon Bilgisi Eksik.";

            if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); variables.ErrorMessage = string.Empty; Mouse.OverrideCursor = null; return; }


            variables.QumulativeSum = 0;
            variables.Counter = 0;
            double kdv = 0;
            double qumulativeKdv = 0;

            foreach (Cls_Siparis item in dg_SiparisEkle.Items)
            {
                try
                {
                    if (string.IsNullOrEmpty(item.StokKodu)) variables.ErrorMessage = variables.ErrorMessage + string.Format("{0}. Satır Stok Kodu Eksik.\n", variables.Counter + 1);
                    if (item.SiparisMiktar is 0) variables.ErrorMessage = variables.ErrorMessage + string.Format("{0}. Satır Sipariş Miktarı 0 Olamaz.\n", variables.Counter + 1);
                    if (item.SiparisFiyat is 0) variables.WarningMessage = variables.WarningMessage + string.Format("{0}. Satır Fiyat Bilgisi Sıfır.\n", variables.Counter + 1);

                    if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); variables.ErrorMessage = string.Empty; Mouse.OverrideCursor = null; return; }


                    variables.QumulativeSum = variables.QumulativeSum + (item.SiparisFiyat * item.SiparisMiktar);
                    kdv = cls_siparis.GetKdvOrani(item.Fisno, item.StokKodu);
                    qumulativeKdv = qumulativeKdv + (item.SiparisFiyat * (kdv / 100) * item.TedarikGirisMiktar);
                    variables.Counter++;

                }

                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; return; }

            }

            cls_siparis.Fisno = cls_siparis.SatisSiparisiIcinFisNoUret();
            LoginLogic login = new();
            string userName = login.GetUserName();

            if (string.IsNullOrEmpty(variables.WarningMessage) == false)
                variables.Result = CRUDmessages.DoYouWishToContinue(variables.WarningMessage);
            else
                variables.Result = CRUDmessages.InsertOnayMessage();

            if (!variables.Result)
            {
                Mouse.OverrideCursor = null; return;
            }

            variables.IsTrue = cls_siparis.InsertSiparisGenel(txt_satis_cari_kodu.Text, (int)Enum.Parse(typeof(DovizTipi), txt_doviz_tipi.Text), txt_teslim_cari_kodu.Text,
                                                              txt_siparis_aciklama.Text, (int)Enum.Parse(typeof(SiparisTipi), txt_siparis_tipi.Text), variables.QumulativeSum,
                                                              qumulativeKdv, txt_destinasyon.Text, variables.Counter, userName, txt_POno.Text, cls_siparis.Fisno);
            if (variables.IsTrue)
            {

                variables.SuccessMessage = "Siparişin Genel Bilgileri Kaydedildi.\n";
                variables.Counter = 0;

                foreach (Cls_Siparis items in dg_SiparisEkle.Items)
                {
                    try
                    {

                        variables.Counter++;

                        bool satirIsTrue = cls_siparis.InsertSiparisSatir(items.StokKodu, items.SiparisMiktar, items.SiparisFiyat, string.Format(items.TerminTarih.ToString(), "yyyy-MM-dd"),
                                                                     txt_satis_cari_kodu.Text, variables.Counter, txt_teslim_cari_kodu.Text,
                                                                     (int)Enum.Parse(typeof(DovizTipi), txt_doviz_tipi.Text), cls_siparis.Fisno);

                        if (satirIsTrue == false) { variables.ErrorMessage = variables.ErrorMessage + variables.Counter + ". Satır Kaydedilemedi.\n"; variables.IsTrue = false; }

                    }

                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; return; }

                }

                if (variables.IsTrue)
                {
                    variables.SuccessMessage = variables.SuccessMessage + "Sipariş Satırları Kaydedildi.";
                    MessageBox.Show(variables.SuccessMessage, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
                    Mouse.OverrideCursor = null;
                    frm_musteri_secim frm_Musteri_Secim = new frm_musteri_secim();
                    frm_Musteri_Secim.Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show(variables.ErrorMessage, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    Mouse.OverrideCursor = null;
                    return;
                }

            }

            else
            {
                MessageBox.Show("Siparişin Genel Bilgileri Kaydedilirken Hata İle Karşılaşıldı.");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void btn_siparis_sil(object sender, RoutedEventArgs e)
        {
            variables.IsTrue = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (variables.IsTrue)
                {
                    variables.ErrorMessage = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                    Cls_Siparis dataItem = row.Item as Cls_Siparis;

                    cls_siparis.SiparisCollection.Remove(dataItem);

                    dg_SiparisEkle.ItemsSource = cls_siparis.SiparisCollection;

                    dg_SiparisEkle.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {

                // Get the new value of the cell
                if (e.Column is DataGridTextColumn column)
                {
                    if (e.Column.Header.ToString() == "Br Fiyat")
                    {
                        UpdateTotalMiktar();
                    }
                }
            }
        }

        private void UpdateTotalMiktar()
        {
            float total = 0;
            foreach (Cls_Siparis item in dg_SiparisEkle.Items)
            {
                total += Convert.ToSingle(item.SiparisToplamFiyat);
                txt_total_miktar.Text = $"Toplam Miktar: {total}";
            }
        }

        private bool EklemeKontrol(Cls_Siparis siparisItem, ObservableCollection<Cls_Siparis> siparisCollection)
        {
            try
            {
                foreach (Cls_Siparis item in siparisCollection)
                {
                    if (item.StokKodu == siparisItem.StokKodu)
                        return false;

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
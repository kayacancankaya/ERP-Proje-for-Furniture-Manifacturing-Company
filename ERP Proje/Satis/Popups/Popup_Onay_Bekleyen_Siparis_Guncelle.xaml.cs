using ERP_Proje.Satis.Popups;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Siparis;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Onay_Bekleyen_Siparis_Guncelle.xaml
    /// </summary>
    public partial class Popup_Onay_Bekleyen_Siparis_Guncelle : Window
    {


        public delegate void DataUpdatedEventHandler(Cls_Cari cls_Cari);
        public event DataUpdatedEventHandler? DataUpdated;//cari popup kapandığında bilgileri aktarmak maksatlı


        ObservableCollection<Cls_Siparis> siparisMasGuncelle = new();
        ObservableCollection<Cls_Siparis> siparisSatirGuncelle = new();
        Cls_Siparis siparis = new();
        string fisno = string.Empty;
        string departman = string.Empty;
        public Popup_Onay_Bekleyen_Siparis_Guncelle(ObservableCollection<Cls_Siparis> siparisDuzenle,
                                                    ObservableCollection<Cls_Siparis> siparisDuzenleSatir,
                                                    string Departman)
        {
            InitializeComponent();
            departman = Departman;
            siparisMasGuncelle = siparisDuzenle;

            fisno = siparisDuzenle.First().Fisno;

            txt_satis_cari_kodu.Text = siparisDuzenle.First().AssociatedCari.SatisCariKodu;
            txt_satis_cari_adi.Text = siparisDuzenle.First().AssociatedCari.SatisCariAdi;
            txt_teslim_cari_kodu.Text = siparisDuzenle.First().AssociatedCari.TeslimCariKodu;
            txt_teslim_cari_adi.Text = siparisDuzenle.First().AssociatedCari.TeslimCariAdi;
            txt_doviz_tipi_selected.Text = siparisDuzenle.First().AssociatedCari.DovizTipi.ToString();
            txt_siparis_tipi_selected.Text = siparisDuzenle.First().AssociatedCari.SiparisTipi.ToString();

            dg_Siparis_Guncelle.ItemsSource = siparisDuzenleSatir;

            if (string.IsNullOrEmpty(siparisDuzenle.First().Destinasyon))
                txt_destinasyon.Text = "";
            else txt_destinasyon.Text = siparisDuzenle.First().Destinasyon;

            if (string.IsNullOrEmpty(siparisDuzenle.First().POnumarasi))
                txt_POno.Text = "";
            else txt_POno.Text = siparisDuzenle.First().POnumarasi;

            if (string.IsNullOrEmpty(siparisDuzenle.First().SiparisAciklama))
                txt_siparis_aciklama.Text = "";
            else txt_siparis_aciklama.Text = siparisDuzenle.First().SiparisAciklama;


            txt_POno.LostFocus += txt_siparis_genel_bilgiler_changed;
            txt_destinasyon.LostFocus += txt_siparis_genel_bilgiler_changed;
            txt_siparis_aciklama.LostFocus += txt_siparis_genel_bilgiler_changed;

            variables.UpdateMessage = string.Empty;

            Mouse.OverrideCursor = null;

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


        Variables variables = new Variables();
        Cls_Cari cari = new Cls_Cari();

        private void btn_cari_select_clicked(object sender, RoutedEventArgs e)
        {
            string satis_teslim = string.Empty;

            if (sender is Button clickedButton)
            {
                if (clickedButton.Name == "btn_teslim_cari_select")
                {
                    satis_teslim = "teslim";
                    variables.UpdateMessage = variables.UpdateMessage + "Teslim Cari Bilgileri Değişecek. \n";
                }

                if (clickedButton.Name == "btn_satis_cari_select")
                {
                    satis_teslim = "satis";
                    variables.UpdateMessage = variables.UpdateMessage + "Satış Cari Bilgileri Değişecek. \n";
                }
            }

            Popup_Cari_Secim_Onay_Bekleyen_Siparis_Guncelle popup_ = new Popup_Cari_Secim_Onay_Bekleyen_Siparis_Guncelle(satis_teslim, this);
            popup_.ShowDialog();

        }

        public void UpdateCariTextBoxes(string satis_teslim, string cari_kodu, string cari_adi)
        {
            if (satis_teslim == "satis")
            {
                txt_satis_cari_kodu.Text = cari_kodu;
                txt_satis_cari_adi.Text = cari_adi;
            }

            if (satis_teslim == "teslim")
            {
                txt_teslim_cari_kodu.Text = cari_kodu;
                txt_teslim_cari_adi.Text = cari_adi;
            }
        }

        private void cmb_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = new();

            if (sender is ComboBox comboBox)
            {

                comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;

                if (comboBox.Name == "cmb_doviz_tipi")
                {
                    txt_doviz_tipi_selected.Text = comboBoxItem.Content.ToString();
                    variables.UpdateMessage = variables.UpdateMessage + "Döviz Tipi Bilgisi Değişecek. \n";
                }
                if (comboBox.Name == "cmb_satis_tipi")
                {
                    txt_siparis_tipi_selected.Text = comboBoxItem.Content.ToString();
                    variables.UpdateMessage = variables.UpdateMessage + "Sipariş Tipi Bilgisi Değişecek. \n";
                }
            }

        }

        private void txt_siparis_genel_bilgiler_changed(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Name == "txt_POno")
            {
                // This code will now be executed when the TextBox loses focus
                variables.UpdateMessage = variables.UpdateMessage + "PO Numarası Değişecek. \n";
            }
            else if (textBox.Name == "txt_destinasyon")
            {
                variables.UpdateMessage = variables.UpdateMessage + "Destinasyon Değişecek. \n";
            }
            else if (textBox.Name == "txt_siparis_aciklama")
            {
                variables.UpdateMessage = variables.UpdateMessage + "Sipariş Açıklaması Değişecek. \n";
            }
        }

        private void btn_siparis_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(variables.UpdateMessage))
            {

                var answer = MessageBox.Show("Sipariş Genel Bilgilerinde Değişiklik Yapılmadı.\n Devam Etmek İstiyor Musunuz?"
                                                                                 , "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.No)
                { variables.UpdateMessage = string.Empty; return; }

            }
            else
            {
                variables.UpdateMessage += "Onaylıyor Musunuz?\n";
                var answer = MessageBox.Show(variables.UpdateMessage, "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.No)
                { variables.UpdateMessage = string.Empty; return; }
            }

            siparisMasGuncelle.First().AssociatedCari.TeslimCariKodu = txt_teslim_cari_kodu.Text;
            siparisMasGuncelle.First().AssociatedCari.TeslimCariAdi = txt_teslim_cari_adi.Text;
            siparisMasGuncelle.First().AssociatedCari.SatisCariKodu = txt_satis_cari_kodu.Text;
            siparisMasGuncelle.First().AssociatedCari.SatisCariAdi = txt_satis_cari_adi.Text;
            siparisMasGuncelle.First().AssociatedCari.DovizTipi = Cls_Base.GetEnumDovizTipi(txt_doviz_tipi_selected.Text);
            siparisMasGuncelle.First().AssociatedCari.SiparisTipi = Cls_Base.GetEnumSiparisTipi(txt_siparis_tipi_selected.Text);
            siparisMasGuncelle.First().POnumarasi = txt_POno.Text;
            siparisMasGuncelle.First().Destinasyon = txt_destinasyon.Text;
            siparisMasGuncelle.First().SiparisAciklama = txt_siparis_aciklama.Text;

            siparisSatirGuncelle.Clear();

            string fisNo = siparisMasGuncelle.Select(item => item.Fisno).First();

            if (string.IsNullOrEmpty(fisNo))
            { CRUDmessages.GeneralFailureMessage("Güncelleme İçin Sipariş Numarası Alınırken"); variables.UpdateMessage = string.Empty; return; }
            variables.Counter = 1;
            foreach (Cls_Siparis item in dg_Siparis_Guncelle.Items)
            {
                Cls_Siparis siparisItem = new Cls_Siparis
                {
                    Fisno = fisNo,
                    FisSira = variables.Counter,
                    StokKodu = item.StokKodu,
                    StokAdi = item.StokAdi,
                    SiparisFiyat = item.SiparisFiyat,
                    SiparisMiktar = item.SiparisMiktar,
                    TerminTarih = item.TerminTarih,
                };

                siparisSatirGuncelle.Add(siparisItem);
                variables.Counter++;
            }

            variables.UpdateMessage = string.Empty;

            variables.Result = siparis.UpdateOnayBekleyenSiparis(siparisMasGuncelle, siparisSatirGuncelle);

            if (!variables.Result)
            { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Güncellenirken"); variables.UpdateMessage = string.Empty; return; }

            if (variables.Result)
            {
                CRUDmessages.UpdateSuccessMessage("Sipariş");

                variables.UpdateMessage = string.Empty;

                Frm_Siparis_Onay_Durum openWindow = UIinteractions.FindSpecificForm<Frm_Siparis_Onay_Durum>();

                if (openWindow != null)
                {
                    openWindow.Close();
                }
                if (departman == "Satış")
                {
                    Frm_Siparis_Onay_Durum onay_Durum = new();
                    onay_Durum.Show();
                    this.Close();
                }
            }
        }

        private void btn_siparis_satir_sil(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            // Get the corresponding data item from the row
            Cls_Siparis item = (Cls_Siparis)button.DataContext;

            // Get the DataGrid's item source collection
            ObservableCollection<Cls_Siparis> items = dg_Siparis_Guncelle.ItemsSource as ObservableCollection<Cls_Siparis>;

            // Remove the item from the collection
            if (items != null)
            {
                items.Remove(item);

                // Refresh the DataGrid to reflect the changes
                dg_Siparis_Guncelle.Items.Refresh();
            }
        }

        private void btn_AddRow_Click(object sender, RoutedEventArgs e)
        {
            Popup_Onay_Bekleyen_Siparis_Satir_Ekle _ekle = new();

            var result = _ekle.ShowDialog();

            if (result == true)
            {
                Cls_Siparis addedSiparis = new();
                addedSiparis = _ekle.toBeTransferredSiparis;
                ObservableCollection<Cls_Siparis> items = dg_Siparis_Guncelle.ItemsSource as ObservableCollection<Cls_Siparis>;

                // Add the new item to the collection
                items.Add(addedSiparis);
            }

            dg_Siparis_Guncelle.Items.Refresh();
        }
    }
}


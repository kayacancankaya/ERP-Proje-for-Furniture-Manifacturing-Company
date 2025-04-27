using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Siparis.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ERP_Proje.Satis.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Onay_Bekleyen_Siparis_Satir_Ekle.xaml
    /// </summary>
    public partial class Popup_Onay_Bekleyen_Siparis_Satir_Ekle : Window
    {
        public Popup_Onay_Bekleyen_Siparis_Satir_Ekle()
        {
            InitializeComponent();
        }


        Cls_Urun urun = new();
        Variables variables = new();
        Cls_Siparis cls_siparis = new();

        public Cls_Siparis toBeTransferredSiparis { get; set; }
        private void btn_urun_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
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


                urun.UrunCollection = urun.PopulateUrunListele(constraints);

                if (!urun.UrunCollection.Any())
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null;
                    return;
                }

                dg_UrunSecim.ItemsSource = urun.UrunCollection;

                Mouse.OverrideCursor = null;

            }

            catch { CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null; }
        }

        private void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

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
                    Cls_Siparis? siparis = new Cls_Siparis
                    {
                        StokKodu = stok_kodu,
                        StokAdi = stok_adi,
                        SiparisMiktar = miktar
                    };

                    result = EklemeKontrol(siparis, cls_siparis.SiparisCollection);

                    if (result)
                        toBeTransferredSiparis = siparis;
                    this.DialogResult = true;
                    this.Close();

                    if (!result)
                    { MessageBox.Show("Birden Fazla Aynı Ürün Eklenemez."); return; }

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

                        Cls_Siparis? siparis = new Cls_Siparis
                        {
                            StokKodu = varyantKodu,
                            StokAdi = varyantAdi,
                            SiparisMiktar = miktar,
                        };

                        result = EklemeKontrol(siparis, cls_siparis.SiparisCollection);

                        if (result)
                            cls_siparis.SiparisCollection.Add(siparis);
                        toBeTransferredSiparis = siparis;
                        this.DialogResult = true;
                        this.Close();

                        if (!result)
                        { MessageBox.Show("Birden Fazla Aynı Ürün Eklenemez."); return; }

                    }
                }



            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

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


    }

}

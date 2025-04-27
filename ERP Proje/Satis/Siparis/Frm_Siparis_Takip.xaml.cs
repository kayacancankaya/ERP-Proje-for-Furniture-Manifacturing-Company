using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Sevk.Popups;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Takip.xaml
    /// </summary>
    public partial class Frm_Siparis_Takip : Window
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
        public Frm_Siparis_Takip()
        {
            InitializeComponent(); Window_Loaded();
        }
        Variables variables = new();
        Cls_Sevk sevk = new();
        ObservableCollection<Cls_Sevk> cariReportCollection = new();
        ObservableCollection<Cls_Sevk> siparisReportCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);


                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);

                if (!string.IsNullOrWhiteSpace(txt_cari_adi.Text))
                    restrictionPairs.Add("@cariAdi", txt_cari_adi.Text);

                queryRestrictions = string.Empty;
                if (cb_kapali_siparis.IsChecked == false)
                    queryRestrictions = queryRestrictions + " and siparisDurum <> 'K' ";
                if (cb_acilmamis_isemri.IsChecked == false)
                    queryRestrictions = queryRestrictions + " and (isemrino <>'') ";
                if (cb_teslim_edilen_siparis.IsChecked == false)
                    queryRestrictions = queryRestrictions + " and siparisMiktar > teslimMiktar ";

                cariReportCollection = sevk.PopulateCariReportCollection(restrictionPairs, queryRestrictions);



                if (cariReportCollection == null)
                { CRUDmessages.GeneralFailureMessage("Cari Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                if (cariReportCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                dg_SiparisSecim.ItemsSource = cariReportCollection;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Cari Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }

        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Sevk item = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);
                siparisReportCollection = sevk.PopulateSiparisReportCollection(restrictionPairs, queryRestrictions,
                                                item.SatisCariKodu, item.SatisCariAdi, item.CariKodu, item.CariAdi);
                if (!siparisReportCollection.Any())
                { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                Popup_Siparis_Takip_Siparis _popup = new(siparisReportCollection, restrictionPairs, queryRestrictions);
                _popup.ShowDialog();
                cariReportCollection = sevk.PopulateCariReportCollection(restrictionPairs, queryRestrictions);
                dg_SiparisSecim.ItemsSource = cariReportCollection;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
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

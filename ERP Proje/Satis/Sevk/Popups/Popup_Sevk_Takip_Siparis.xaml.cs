using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Takip_Siparis.xaml
    /// </summary>
    public partial class Popup_Sevk_Takip_Siparis : Window
    {

        public Popup_Sevk_Takip_Siparis(ObservableCollection<Cls_Sevk> siparisReportCollection, Dictionary<string, string> restrictionDictionary)
        {
            InitializeComponent();
            dg_Rapor_Sevk_Siparis_Detay.ItemsSource = siparisReportCollection;
            restrictionDict = restrictionDictionary;

            Mouse.OverrideCursor = null;
        }
        Cls_Sevk sevk = new();
        ObservableCollection<Cls_Sevk> yuklemeEmriReportCollection = new();
        Dictionary<string, string> restrictionDict = new Dictionary<string, string>();

        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Sevk item = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);

                yuklemeEmriReportCollection = sevk.PopulateYuklemeReportCollectionForShipment(restrictionDict, item.SiparisKodu, item.SiparisSira);

                if (!yuklemeEmriReportCollection.Any())
                { CRUDmessages.GeneralFailureMessage("Rapor Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                Popup_Sevk_Takip_Yukleme _popup = new(yuklemeEmriReportCollection);
                _popup.ShowDialog();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Yükleme Bilgileri Alınırken"); Mouse.OverrideCursor = null;
            }
        }
    }
}

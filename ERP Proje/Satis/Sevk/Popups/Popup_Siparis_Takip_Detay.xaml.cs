using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Takip_Detay.xaml
    /// </summary>
    public partial class Popup_Siparis_Takip_Detay : Window
    {

        public Popup_Siparis_Takip_Detay(ObservableCollection<Cls_Sevk> wholeReport)
        {
            InitializeComponent();

            dg_Detayli_Rapor.ItemsSource = wholeReport;

            Mouse.OverrideCursor = null;
        }

    }
}

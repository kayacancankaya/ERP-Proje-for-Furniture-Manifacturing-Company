using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Takip_Yukleme.xaml
    /// </summary>
    public partial class Popup_Sevk_Takip_Yukleme : Window
    {

        public Popup_Sevk_Takip_Yukleme(ObservableCollection<Cls_Sevk> yuklemeEmriReportCollection)
        {
            InitializeComponent();

            dg_Sevk_Yukleme_Rapor.ItemsSource = yuklemeEmriReportCollection;

            Mouse.OverrideCursor = null;
        }
    }
}

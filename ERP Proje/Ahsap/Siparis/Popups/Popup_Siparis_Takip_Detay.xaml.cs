using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Ahsap.Siparis.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Takip_Detay.xaml
    /// </summary>
    public partial class Popup_Siparis_Takip_Detay : Window
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
        public Popup_Siparis_Takip_Detay(ObservableCollection<Cls_Sevk> wholeReport)
        {
            InitializeComponent(); Window_Loaded();

            dg_Detayli_Rapor.ItemsSource = wholeReport;

            Mouse.OverrideCursor = null;
        }

    }
}

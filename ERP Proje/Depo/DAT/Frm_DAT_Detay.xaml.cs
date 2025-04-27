using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Depo.DAT
{
    /// <summary>
    /// Interaction logic for Frm_DAT_Detay.xaml
    /// </summary>
    public partial class Frm_DAT_Detay : Window
    {
        public Frm_DAT_Detay(ObservableCollection<Cls_Depo> datDetay)
        {
            InitializeComponent(); Window_Loaded();
            dg_DAT_Detay.ItemsSource = datDetay;
            Mouse.OverrideCursor = null;
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
    }
}

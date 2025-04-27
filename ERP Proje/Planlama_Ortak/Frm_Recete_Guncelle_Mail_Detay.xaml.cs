using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Recete_Guncelle_Mail_Detay.xaml
    /// </summary>
    public partial class Frm_Recete_Guncelle_Mail_Detay : Window
    {
        public Frm_Recete_Guncelle_Mail_Detay(ObservableCollection<Cls_Isemri> detayCollection)
        {
            InitializeComponent(); Window_Loaded();
            dg_recete_Detay.ItemsSource = detayCollection;
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

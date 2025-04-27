using System.Windows;

namespace ERP_Proje.Ahsap.Depo.Stok_Hareket
{
    /// <summary>
    /// Interaction logic for Frm_Stok_Hareket_Sorgu.xaml
    /// </summary>
    public partial class Frm_Stok_Hareket_Sorgu : Window
    {
        public Frm_Stok_Hareket_Sorgu()
        {
            InitializeComponent(); Window_Loaded();
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

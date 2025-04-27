using System.Windows;

namespace ERP_Proje.Depo.Stok_Hareket
{
    /// <summary>
    /// Interaction logic for Frm_Virman_Kaydet.xaml
    /// </summary>
    public partial class Frm_Virman_Kaydet : Window
    {
        public Frm_Virman_Kaydet()
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

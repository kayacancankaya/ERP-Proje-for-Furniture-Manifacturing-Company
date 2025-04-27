using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Bilgi_Islem
{
    /// <summary>
    /// Interaction logic for Frm_Bilgi_Islem.xaml
    /// </summary>
    public partial class Frm_Bilgi_Islem : Window
    {
        public Frm_Bilgi_Islem()
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

    }
}

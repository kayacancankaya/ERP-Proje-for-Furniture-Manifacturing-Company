using System.Windows;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Frm_Wait.xaml
    /// </summary>
    public partial class Frm_Wait : Window
    {
        public Frm_Wait(string whatIsWaitingFor)
        {
            InitializeComponent();
            txt_whatIsWaitingFor.Text = whatIsWaitingFor;
        }
    }
}

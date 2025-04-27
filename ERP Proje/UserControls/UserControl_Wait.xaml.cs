using System.Windows.Controls;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl_Wait.xaml
    /// </summary>
    public partial class UserControl_Wait : UserControl
    {
        public UserControl_Wait()
        {
            InitializeComponent();

        }
        public UserControl_Wait(string whatIsWaitingFor)
        {
            InitializeComponent();
            txt_wait.Text = whatIsWaitingFor;
        }
        public string WaitText
        {
            get { return txt_wait.Text; }
            set { txt_wait.Text = value; }
        }
    }
}

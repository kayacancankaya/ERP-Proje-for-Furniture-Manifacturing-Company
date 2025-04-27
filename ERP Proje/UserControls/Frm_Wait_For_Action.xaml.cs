using Layer_2_Common.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Frm_Wait_For_Action.xaml
    /// </summary>
    public partial class Frm_Wait_For_Action : Window
    {
        private readonly Action _actionToExecute;
        public Frm_Wait_For_Action(string whatIsWaitingFor, Action actionToExecute)
        {
            try
            {

                InitializeComponent();
                if (string.IsNullOrEmpty(whatIsWaitingFor))
                    whatIsWaitingFor = string.Empty;

                txt_whatIsWaitingFor.Text = whatIsWaitingFor;
                if (actionToExecute != null)
                {
                    _actionToExecute = actionToExecute;
                    _actionToExecute?.Invoke();
                }
                this.Close();
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage(string.Format("{0} İşlenirken",whatIsWaitingFor));
                this.Close();
            }
        }
    }
}

using Layer_2_Common.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace Layer_UI.Ayarlar
{
    /// <summary>
    /// Interaction logic for Frm_Yil_Degistir.xaml
    /// </summary>
    public partial class Frm_Yil_Degistir : Window
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
        public Frm_Yil_Degistir()
        {
            InitializeComponent(); Window_Loaded();
            txt_yil.Text = Variables.Yil_.ToString();
        }

        private void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Int32.TryParse(txt_yil.Text, out int yil))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(txt_yil.Text);
                    return;
                }
                if (txt_yil.Text.Length != 4)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Yıl 4 Haneli Olmalı");
                    return;
                }
                if (txt_yil.Text.Substring(0,2) != "20")
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Yıl 20 ile Başlamalı.");
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = Variables.UpdateYil(yil);
                if(Variables.Result_)
                    CRUDmessages.UpdateSuccessMessage("Ayarlar");
                else
                    CRUDmessages.UpdateFailureMessage("Ayarlar");
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Yıl Değiştirilemedi.");
                Mouse.OverrideCursor = null;
            }
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

using Layer_2_Common.Type;
using Layer_Business;
using System.Windows;

namespace Layer_UI.Planlama_Ortak.Plan_Takip_No
{
    /// <summary>
    /// Interaction logic for Frm_Talep_Hesaplat.xaml
    /// </summary>
    public partial class Frm_Talep_Hesaplat : Window
    {
        Cls_Planlama plan = new();
        public Frm_Talep_Hesaplat()
        {
            InitializeComponent();
        }

        private async void btn_talep_hesaplat_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;

                Variables.Result_ = await plan.TalepHesaplatAsync();
                if (Variables.Result_)
                    CRUDmessages.GeneralSuccessMessage("Talep Hesaplaması Tamamlandı");
                else
                    CRUDmessages.GeneralFailureMessage("Hesaplama Esnasında");

                txt_please_wait.Visibility = Visibility.Collapsed;
                this.Close();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Talepler Hesaplanırken");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
    }
}

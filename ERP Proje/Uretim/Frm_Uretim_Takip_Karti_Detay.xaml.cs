using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Uretim
{
    /// <summary>
    /// Interaction logic for Frm_Uretim_Takip_Karti_Detay.xaml
    /// </summary>
    public partial class Frm_Uretim_Takip_Karti_Detay : Window
    {
        public Frm_Uretim_Takip_Karti_Detay(ObservableCollection<Cls_Uretim> detay)
        {
            try
            {
                InitializeComponent();

                if (detay == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Detay Formu Açılırken");
                    this.Close();
                }

                this.Title = string.Format("{0}_{1}_{2}_{3}", detay.Select(s => s.SiparisNumarasi).FirstOrDefault(),
                                                        detay.Select(s => s.SiparisSira).FirstOrDefault().ToString(),
                                                        detay.Select(s => s.ReferansIsemri).FirstOrDefault().ToString(),
                                                        detay.Select(s => s.UrunKodu).FirstOrDefault());
                dg_UretimTakipDetay.ItemsSource = detay;

                Mouse.OverrideCursor = null;

            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Detay Formu Açılırken");
                this.Close();
            }
        }
    }
}

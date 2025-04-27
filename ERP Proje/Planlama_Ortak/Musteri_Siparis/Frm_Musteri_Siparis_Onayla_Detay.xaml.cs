using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Ortak.Musteri_Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Musteri_Siparis_Onayla_Detay.xaml
    /// </summary>
    public partial class Frm_Musteri_Siparis_Onayla_Detay : Window
    {
        public Frm_Musteri_Siparis_Onayla_Detay(ObservableCollection<Cls_Siparis> siparisDetay)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                InitializeComponent();

                dg_siparis_detay.ItemsSource = siparisDetay;
                Mouse.OverrideCursor = null;
            }
            catch (System.Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                Mouse.OverrideCursor = null;
                if (Application.Current.MainWindow == this)
                {
                    Frm_Musteri_Siparis_Onayla frm = new();
                    frm.Show();
                    this.Close();
                }
                else
                    this.Close();

            }
        }

        private void dg_siparis_onay_durum_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var item = e.Row.Item as Cls_Siparis; 

            if (item != null)
            {
                if (!item.DoesUrunAgaciExists)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                    e.Row.Foreground = new SolidColorBrush(Colors.White);
                    e.Row.FontWeight = FontWeights.Bold;
                    e.Row.FontStyle = FontStyles.Normal;
                }
            }

        }
    }
}

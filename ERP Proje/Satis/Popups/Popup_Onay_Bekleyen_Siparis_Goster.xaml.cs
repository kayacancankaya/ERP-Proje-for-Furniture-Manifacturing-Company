using Layer_Business;
using System.Windows;

namespace Layer_UI.Satis.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Onay_Bekleyen_Siparis_Goster.xaml
    /// </summary>
    public partial class Popup_Onay_Bekleyen_Siparis_Goster : Window
    {

        Cls_Siparis siparis = new();
        public Popup_Onay_Bekleyen_Siparis_Goster(string fisno)
        {
            InitializeComponent();
            siparis.SiparisDetayCollection = siparis.GetSiparisSatirInfo(fisno, "Onay");
            dg_Onay_Bekleyen_Siparis_Detay.ItemsSource = siparis.SiparisDetayCollection;
        }

        private void btn_AddRow_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_siparis_satir_sil(object sender, RoutedEventArgs e)
        {


        }
    }
}

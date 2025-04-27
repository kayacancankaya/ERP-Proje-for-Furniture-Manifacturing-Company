using Layer_Business;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Planlama_Moduler.Simulasyon.Popups
{
    /// <summary>
    /// Popup_Plan_Adi_Detay.xaml etkileşim mantığı
    /// </summary>
    public partial class Popup_Plan_Adi_Detay : Window
    {
        public Popup_Plan_Adi_Detay(ObservableCollection<Cls_Planlama> planDetay)
        {
            InitializeComponent();

            dg_Plan_Adlari.ItemsSource = planDetay;
            Mouse.OverrideCursor = null;
        }

    }
}

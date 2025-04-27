using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak.Plan_Takip_No
{
    /// <summary>
    /// Interaction logic for Popup_Plana_Bagli_Hammadde_Detay.xaml
    /// </summary>
    public partial class Popup_Plana_Bagli_Hammadde_Detay : Window
    {
        public Popup_Plana_Bagli_Hammadde_Detay(ObservableCollection<Cls_Planlama> hammaddeDetayColl)
        {
            try
            {
                InitializeComponent();
                if (hammaddeDetayColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Hammadde Bilgileri Listelenirken");
                    Mouse.OverrideCursor = null;
                    this.Close();
                }
                if (hammaddeDetayColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    this.Close();
                }

                this.Title = hammaddeDetayColl.Select(h => h.HamKodu).FirstOrDefault();

                dg_PlanaBagliTalep_Detay.ItemsSource = hammaddeDetayColl;
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Hammadde Bilgileri Listelenirken");
            }

        }
    }
}

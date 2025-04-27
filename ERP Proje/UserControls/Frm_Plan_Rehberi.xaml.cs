using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Frm_Plan_Rehberi.xaml
    /// </summary>
    public partial class Frm_Plan_Rehberi : Window
    {
        public Frm_Plan_Rehberi()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                InitializeComponent();
                dg_PlanListe.ItemsSource = plan.GetDistinctPlanAdi();
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Rehberi Açılırken");
            }
        }
        ObservableCollection<Cls_Planlama> Planlar = new();
        public ObservableCollection<Cls_Planlama> AktarilacakPlanlar = new();
        Cls_Planlama plan = new();
        Variables variables = new();
        private void btn_filtrele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txt_plan_adi.Text))
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                Dictionary<string, string> constraintPairs = new();

                if (!string.IsNullOrEmpty(txt_plan_adi.Text))
                    constraintPairs.Add("planAdi", txt_plan_adi.Text);
                ObservableCollection<Cls_Planlama> planListesi = new();

                planListesi = plan.GetDistinctPlanAdi(constraintPairs);

                if (planListesi == null)
                {
                    CRUDmessages.GeneralFailureMessage("Plan Adları Filtrelenirken");
                    Mouse.OverrideCursor = null ;
                    return;
                }
                if (planListesi.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null ;
                    return;
                }

                dg_PlanListe.ItemsSource = planListesi;
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Plan Adları Filtrelenirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_plan_aktar_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                variables.Counter = 0;
                AktarilacakPlanlar.Clear(); 
                foreach (Cls_Planlama item in dg_PlanListe.Items)
                {
                    if (item.IsChecked == true)
                    {
                        AktarilacakPlanlar.Add(item);
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.GeneralFailureMessageNoInput();
                    return;
                }
                DialogResult = true;
                Close();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Bilgisi Aktarılırken");

                Mouse.OverrideCursor = null;
            }
        }
    }
}

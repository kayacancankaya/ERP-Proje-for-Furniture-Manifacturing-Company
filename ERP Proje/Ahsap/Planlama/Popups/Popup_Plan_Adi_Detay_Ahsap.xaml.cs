using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Ahsap.Planlama.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Plan_Adi_Detay_Ahsap.xaml
    /// </summary>
    public partial class Popup_Plan_Adi_Detay_Ahsap : Window
    {
        Variables variables = new();
        Cls_Planlama plan = new();
        public Popup_Plan_Adi_Detay_Ahsap(ObservableCollection<Cls_Planlama> planDetay)
        {
            InitializeComponent(); Window_Loaded();
            dg_Plan_Adi_Detay.ItemsSource = planDetay;
            Mouse.OverrideCursor = null;
        }

        private void btn_plan_detay_sil_clicked(object sender, EventArgs e)
        {
            try
            {
                variables.Result = CRUDmessages.DeleteOnayMessage();
                if (!variables.Result)
                    return;

                variables.ErrorMessage = string.Empty;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Plan Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Plan Bilgileri Alınırken"); return; }

                // Get the data item associated with the row
                Cls_Planlama? dataItem = row.Item as Cls_Planlama;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Plan Bilgileri Alınırken"); return; }
                variables.ResultInt = plan.DeletePlanAdiDetay("Ahsap Plan", dataItem);

                switch (variables.ResultInt)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessage("Plan Detayı Silinirken");
                        break;
                    case 1:
                        CRUDmessages.DeleteSuccessMessage("Plan", 1);
                        break;
                }

                ObservableCollection<Cls_Planlama> updatedPlanDetay = plan.GetPlanAdiDetay(dataItem, "Ahsap Plan");
                dg_Plan_Adi_Detay.ItemsSource = updatedPlanDetay;
                dg_Plan_Adi_Detay.Items.Refresh();
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleşirken");
            }
        }


        private async void mamul_detay_clicked(object sender, EventArgs e)
        {
            try
            {
                Cls_Planlama mamul = UIinteractions.GetDataItemFromButton<Cls_Planlama>(sender);
                if (mamul == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Kodu Bulunamadı");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Planlama> hammaddeCollection = await plan.GetMamulHammaddeIhtiyacDurum(mamul.UrunKodu);

                if (hammaddeCollection == null ||
                    hammaddeCollection.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Mamule Bağlı Hammadde Bulunamadı");
                    Mouse.OverrideCursor = null;
                    return;
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Hammadde İhtiyaç Durumu");
            }
        }
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
    }

}

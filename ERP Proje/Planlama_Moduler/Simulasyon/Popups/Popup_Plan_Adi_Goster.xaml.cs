using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Moduler.Simulasyon.Popups
{
    /// <summary>
    /// Popup_Plan_Adi_Goster.xaml etkileşim mantığı
    /// </summary>
    public partial class Popup_Plan_Adi_Goster : Window
    {

        Cls_Planlama plan = new();
        Variables variables = new();
        ObservableCollection<Cls_Planlama> planAdiCollection = new();
        string simulasyonTipi = string.Empty;
        public Popup_Plan_Adi_Goster(string simulasyonTip)
        {
            InitializeComponent();
            simulasyonTipi = simulasyonTip;
            ObservableCollection<Cls_Planlama> planAdiCollection = new();
            planAdiCollection = plan.GetDistinctPlanAdi(simulasyonTip);
            if (planAdiCollection == null)
            {
                CRUDmessages.QueryIsEmpty("Plan Adı");
                Mouse.OverrideCursor = null;
                return;
            }

            dg_Plan_Adlari.ItemsSource = planAdiCollection;
            Mouse.OverrideCursor = null;

        }

        private bool isDragging = false;
        private Point startPoint;
        private DataGridRow draggedItem;
        private void plan_adi_detay_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Planlama dataItem = UIinteractions.GetDataItemFromButton<Cls_Planlama>(sender);

                Cls_Planlama? planItem = new Cls_Planlama
                {
                    PlanAdiSira = dataItem.PlanAdiSira,
                    PlanAdi = dataItem.PlanAdi,
                };

                ObservableCollection<Cls_Planlama> planAdiDetayCollection = plan.GetPlanAdiDetay(planItem, simulasyonTipi);

                Popup_Plan_Adi_Detay _frm = new(planAdiDetayCollection);
                var result = _frm.ShowDialog();
                if (result == false)
                {
                    planAdiCollection = plan.GetDistinctPlanAdi(simulasyonTipi);
                    dg_Plan_Adlari.ItemsSource = planAdiCollection;
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Plan Adı Detayları Getirilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_duzenle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_Plan_Adlari.Items.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessage("Plan Bulunamadı.");
                    return;
                }

                ObservableCollection<Cls_Planlama> reOrderedSira = new();

                variables.Counter = 1;

                foreach (Cls_Planlama item in dg_Plan_Adlari.Items)
                {
                    item.PlanAdiSira = variables.Counter;
                    item.EskiPlanAdiSira = planAdiCollection.Where(x => x.PlanAdi == item.PlanAdi).Select(s => s.PlanAdiSira).FirstOrDefault();
                    reOrderedSira.Add(item);
                    variables.Counter++;
                }

                variables.ResultInt = plan.ReOrderPlanSira(reOrderedSira, simulasyonTipi);

                Mouse.OverrideCursor = null;
                switch (variables.ResultInt)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        break;
                    case 1:
                        CRUDmessages.UpdateSuccessMessage("Plan", variables.Counter - 1);
                        dg_Plan_Adlari.ItemsSource = reOrderedSira;

                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Kaydedilecek Plan Bulunamadı.");
                        break;
                    case 3:
                        CRUDmessages.GeneralFailureMessage("Plan Kaydedilirken");
                        break;
                }
                dg_Plan_Adlari.Items.Refresh();
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Plan Sıraları Yeniden Düzenlenirken");
            }

        }
        private void btn_plan_sil_clicked(object sender, EventArgs e)
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

                variables.ResultInt = plan.DeletePlanAdi(simulasyonTipi, dataItem.PlanAdi);

                switch (variables.ResultInt)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessage("Plan Silinirken");
                        break;
                    case 1:
                        CRUDmessages.DeleteSuccessMessage("Plan", 1);
                        break;
                    case 6:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Bildirim Var");
                        break;
                    case 7:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Var");
                        break;

                }

                planAdiCollection = plan.GetDistinctPlanAdi(simulasyonTipi);
                if (planAdiCollection == null)
                {
                    CRUDmessages.QueryIsEmpty("Plan Adı");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_Plan_Adlari.ItemsSource = planAdiCollection;
                dg_Plan_Adlari.Items.Refresh();
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleşirken");
            }
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
            draggedItem = UIinteractions.FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource);

            if (draggedItem != null)
            {
                isDragging = true;
            }
        }
        private void DataGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Released ||
                    (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance &&
                     Math.Abs(diff.Y) < SystemParameters.MinimumVerticalDragDistance))
                {
                    return;
                }

                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                isDragging = false;
            }
        }
        private void DataGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Cls_Planlama)))
            {
                Cls_Planlama droppedData = (Cls_Planlama)e.Data.GetData(typeof(Cls_Planlama));
                ObservableCollection<Cls_Planlama> dataGridSource = (ObservableCollection<Cls_Planlama>)dg_Plan_Adlari.ItemsSource;

                int index = dataGridSource.IndexOf(droppedData);
                int newIndex = GetIndexUnderMouse(e.GetPosition(dg_Plan_Adlari), dg_Plan_Adlari);

                // Ensure the index and newIndex are within valid range
                if (index != -1 && newIndex != -1 && index < dataGridSource.Count && newIndex < dataGridSource.Count)
                {
                    dataGridSource.Move(index, newIndex);
                }
            }
        }


        private int GetIndexUnderMouse(Point position, DataGrid grid)
        {
            var hitTestResult = VisualTreeHelper.HitTest(grid, position);
            var visual = hitTestResult.VisualHit;

            while (visual != null && !(visual is DataGridRow))
            {
                visual = VisualTreeHelper.GetParent(visual);
            }

            if (visual == null)
            {
                return -1;
            }

            return grid.ItemContainerGenerator.IndexFromContainer(visual as DataGridRow);
        }



    }
}

using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak.Plan_Takip_No
{
    /// <summary>
    /// Interaction logic for Frm_Plan_Ihtiyac.xaml
    /// </summary>
    public partial class Frm_Plan_Ihtiyac : Window
    {
        Cls_Planlama plan = new();
        LoginLogic login = new();
        string departman = string.Empty;
        Dictionary<int, string> planAdiDic = new();
        ObservableCollection<Cls_Planlama> talepColl = new();
        Dictionary<string, string> restrictionPairs = new();
        List<string> planNoList = new();
        public Frm_Plan_Ihtiyac()
        {
            try
            {

                InitializeComponent(); Window_Loaded();

                if (login.GetDepartment().Contains("Doseme"))
                    departman = "Doseme";
                else if (login.GetDepartment().Contains("Moduler"))
                    departman = "Moduler";
                else
                    departman = "Hepsi";

                planAdiDic = plan.GetDistinctPlanAdiString(departman);
                cbx_plan_adi.ItemsSource = planAdiDic.Values;

                planNoList = plan.GetDistinctPlanNo();
                cbx_plan_no.ItemsSource = planNoList;
                dp_termin_tarih_genel.SelectedDate = DateTime.Now.AddMonths(1);
            }
            catch (Exception)
            {
                InitializeComponent(); Window_Loaded();
                CRUDmessages.GeneralFailureMessage("Sayfa Yüklenirken");
            }
        }

        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Planlama item = UIinteractions.GetDataItemFromButton<Cls_Planlama>(sender);
                ObservableCollection<Cls_Planlama> hamDetayCollection = plan.PopulatePlanaBagliHammaddeDetay(item.PlanAdi, item.HamKodu);
                if (hamDetayCollection == null)

                { CRUDmessages.GeneralFailureMessage("Hammadde Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                if (hamDetayCollection.Count() == 0)
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                Popup_Plana_Bagli_Hammadde_Detay _popup = new(hamDetayCollection);
                Variables.FormResult_ = _popup.ShowDialog();
                if (!Variables.Result_)
                {
                    listele();
                    Mouse.OverrideCursor = null;
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }
        }
        private void cbx_plan_adi_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNoList;
                    return;
                }

                List<string> relatedPlanNos = plan.GetPlanAdiRelatedPlanNos(combo.SelectedItem.ToString());
                cbx_plan_no.ItemsSource = relatedPlanNos;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
            }
        }
        private void cbx_plan_no_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNoList;
                    return;
                }

                List<string> relatedPlanAdis = plan.GetPlanNoRelatedPlanAdis(combo.SelectedItem.ToString());
                cbx_plan_adi.ItemsSource = relatedPlanAdis;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                listele();
            }
            catch
            {
                Mouse.OverrideCursor = null;
            }
        }
        private void listele()
        {
            try
            {

                if (cbx_plan_no.SelectedItem == null &&
                    cbx_plan_adi.SelectedItem == null)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;

                restrictionPairs.Clear();

                if (cbx_plan_no.SelectedItem != null)
                    restrictionPairs.Add("@planNo", cbx_plan_no.SelectedItem.ToString());
                if (cbx_plan_adi.SelectedItem != null)
                    restrictionPairs.Add("@planAdi", cbx_plan_adi.SelectedItem.ToString());

                ObservableCollection<Cls_Planlama> firstColl = plan.PopulatePlanIhtiyacListe(restrictionPairs);
                if (firstColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Talep Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (firstColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                talepColl = firstColl;
                dg_PlanaBagliTalep.ItemsSource = talepColl;

                dg_PlanaBagliTalep.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private async void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_Planlama> talepAcilacakColl = new();

                txt_please_wait.Visibility = Visibility.Visible;
                Variables.Counter_ = 0;

                if (dg_PlanaBagliTalep == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Talep Listesi Bulunamadı");
                    return;
                }

                if (dg_PlanaBagliTalep.Items.Count < 1)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                foreach (Cls_Planlama item in dg_PlanaBagliTalep.Items)
                {
                    if (item.TedarikTalepMiktar > 0 &&
                        !string.IsNullOrEmpty(item.PlanAdi) &&
                        !string.IsNullOrEmpty(item.HamKodu))
                    {
                        Variables.Counter_++;
                        item.TalepSira = Variables.Counter_;
                        item.TalepGenelAciklama = txt_genel_aciklama.Text;
                        item.TerminTarih = dp_termin_tarih_genel.SelectedDate == null ? item.TerminTarih : Convert.ToDateTime(dp_termin_tarih_genel.SelectedDate);
                        talepAcilacakColl.Add(item);

                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }


                btn_talep_ac.IsEnabled = false;

                Variables.ResultInt_ = await plan.InsertTalepPlanaBagliAsync(talepAcilacakColl);

                switch (Variables.ResultInt_)
                {
                    case 1:
                        {
                            CRUDmessages.InsertSuccessMessage("Talep", talepAcilacakColl.Count); txt_please_wait.Visibility = Visibility.Collapsed; break;
                        }
                    case -1:
                        {
                            CRUDmessages.GeneralFailureMessage("Veri Tabanında İşlem Yaparken"); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 2:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Talep Bulunamadı."); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 3:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Talep Giriniz."); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 4:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Genel Bilgileri Kaydedilirken"); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 5:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Bilgileri Kaydedilirken"); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 6:
                        {
                            CRUDmessages.GeneralFailureMessage("Fiş Numarası Alınırken"); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                    case 7:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Hesaplaması Güncellenirken"); txt_please_wait.Visibility = Visibility.Collapsed; return;
                        }
                }

            }

            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private bool selectMiktarColumn = false;
        private void btn_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;


                if (clickedButton.Parent is StackPanel stackPanel)
                {
                    foreach (UIElement element in stackPanel.Children)
                    {
                        if (element is ComboBox comboBox)
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        if (element is DatePicker datePicker)
                        {
                            datePicker.SelectedDate = null;
                        }
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Talep Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
            }
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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

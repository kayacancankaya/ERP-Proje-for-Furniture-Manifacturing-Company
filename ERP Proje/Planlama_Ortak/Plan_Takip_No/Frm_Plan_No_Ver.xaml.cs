
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
    /// Interaction logic for Frm_Plan_No_Ver.xaml
    /// </summary>
    public partial class Frm_Plan_No_Ver : Window
    {
        LoginLogic login = new();
        string departman = string.Empty;
        Cls_Planlama plan = new();
        List<string> planNoList = new();
        public Frm_Plan_No_Ver()
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
                Dictionary<int, string> planAdiDic = new();
                planAdiDic = plan.GetDistinctPlanAdiString(departman);
                if (planAdiDic != null)
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;

                planNoList = plan.GetDistinctPlanNo();
                if (planNoList != null)
                    cbx_mevcut_planlar.ItemsSource = planNoList;
                Window_Loaded();
            }
            catch (Exception)
            {
                InitializeComponent(); Window_Loaded();
                CRUDmessages.GeneralFailureMessage("Sayfa Yüklenirken");
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
                if (string.IsNullOrEmpty(txt_siparis_no.Text) && cbx_plan_adi.SelectedItem == null)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                Dictionary<string, string> restrictionPairs = new();
                if (!string.IsNullOrEmpty(txt_siparis_no.Text))
                    restrictionPairs.Add("@siparisNumarasi", txt_siparis_no.Text);
                if (cbx_plan_adi.SelectedItem != null)
                    restrictionPairs.Add("@planAdi", cbx_plan_adi.SelectedItem.ToString());

                bool planNoVerilenleriGosterme = cb_plan_no_verilenleri_gosterme.IsChecked ?? false;

                ObservableCollection<Cls_Planlama> siparisColl = plan.PlanNoVerilecekSiparisler(restrictionPairs, planNoVerilenleriGosterme, 1, false);
                if (siparisColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (siparisColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_SiparisSecim.ItemsSource = siparisColl;

                dg_SiparisSecim.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;

                planNoList = plan.GetDistinctPlanNo();
                cbx_mevcut_planlar.ItemsSource = planNoList;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Variables.Counter_ = 0;
                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Planlama> planNoVerilecekColl = new();
                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        if (!string.IsNullOrEmpty(item.PlanNo))
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage(item.SiparisNumarasi + "," + item.SiparisSira + " Plan Numarası Var, Yeni Plan Numarası Verilemez");
                            return;
                        }

                        Variables.Result_ = UserEntryControl.IsStringNullOrEmpty(item.SiparisNumarasi, item.Isemrino, null, null, null);
                        if (!Variables.Result_)
                        {
                            Mouse.OverrideCursor = null;
                            return;
                        }

                        planNoVerilecekColl.Add(item);

                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                bool isExisting = false;
                if (cb_mevcut_plana_ekle.IsChecked == true)
                {
                    isExisting = true;
                    foreach (var item in planNoVerilecekColl)
                        item.PlanNo = cbx_mevcut_planlar.SelectedItem.ToString();
                }

                Variables.Result_ = plan.InsertPlanNo(planNoVerilecekColl, isExisting);

                if (!Variables.Result_) { CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında"); Mouse.OverrideCursor = null; return; }
                CRUDmessages.InsertSuccessMessage("İşemri", Variables.Counter_);
                Mouse.OverrideCursor = null;
                listele();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Planlama item in dg_SiparisSecim.Items)
            {
                item.IsChecked = headerIsChecked;
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

        private void check_box_mevcut_plana_ekle_checked(object sender, EventArgs e)
        {
            cbx_mevcut_planlar.Visibility = Visibility.Visible;
            btn_sifirla_mevcut_planlar.Visibility = Visibility.Visible;
        }
        private void check_box_mevcut_plana_ekle_unchecked(object sender, EventArgs e)
        {
            cbx_mevcut_planlar.Visibility = Visibility.Collapsed;
            btn_sifirla_mevcut_planlar.Visibility = Visibility.Collapsed;
        }

        private async void btn_toplu_sil_clicked(object sender, EventArgs e)
        {
            try
            {
                if (dg_SiparisSecim.ItemsSource == null)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (dg_SiparisSecim.Items.Count < 1)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                ObservableCollection<Cls_Planlama> silinecekPlanColl = new();
                Variables.Counter_++;

                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (!string.IsNullOrEmpty(item.PlanNo) && item.MevcutSiparisAdedi == 0)
                    {
                        silinecekPlanColl.Add(item);
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }
                txt_please_wait.Visibility = Visibility.Visible;

                Variables.Result_ = await plan.DeletePlanNoTopluAsync(silinecekPlanColl);

                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }
                CRUDmessages.DeleteSuccessMessage("Plan", silinecekPlanColl.Count());
                txt_please_wait.Visibility = Visibility.Collapsed;
                listele();

                return;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Toplu Silme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private async void btn_plan_sil(object sender, EventArgs e)
        {
            try
            {
                Cls_Planlama item = UIinteractions.GetDataItemFromButton<Cls_Planlama>(sender);
                if (item == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken");
                    return;
                }
                if (item.MevcutSiparisAdedi > 0)
                {
                    CRUDmessages.GeneralFailureMessage("Siparişe Ait Hammadde Siparişi Bulunduğundan Plan Numarası Silinemez");
                    return;
                }

                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;

                Variables.Result_ = await plan.DeletePlanNoAsync(item.PlanNo, item.SiparisNumarasi, item.SiparisSira);
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }

                CRUDmessages.DeleteSuccessMessage("Plan", 1);
                listele();
                Mouse.OverrideCursor = null;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
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

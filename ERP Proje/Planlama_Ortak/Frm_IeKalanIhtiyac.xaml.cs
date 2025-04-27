using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_IeKalanIhtiyac.xaml
    /// </summary>
    public partial class Frm_IeKalanIhtiyac : Window
    {
        Cls_Arge arge = new();
        public Frm_IeKalanIhtiyac()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod_1.ItemsSource = arge.GetDistinctKod1();
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
        Cls_Planlama plan = new();
        private async void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, string> constraintPairs = new();
                string restrictionQuery = string.Empty;

                if (!string.IsNullOrEmpty(txt_siparis_no.Text))
                    constraintPairs.Add("@siparisNumarasi", txt_siparis_no.Text);
                if (!string.IsNullOrEmpty(txt_siparis_sira.Text))
                    constraintPairs.Add("@siparisSira", txt_siparis_sira.Text);
                if (!string.IsNullOrEmpty(txt_urun_kodu.Text))
                    constraintPairs.Add("@urunKodu", txt_urun_kodu.Text);
                if (!string.IsNullOrEmpty(txt_urun_adi.Text))
                    constraintPairs.Add("@urunAdi", txt_urun_adi.Text);

                if (!string.IsNullOrEmpty(txt_refisemri_no.Text))
                    constraintPairs.Add("@referansIsemrino", txt_refisemri_no.Text);
                if (!string.IsNullOrEmpty(txt_isemrino.Text))
                    constraintPairs.Add("@isemrino", txt_isemrino.Text);
                if (!string.IsNullOrEmpty(txt_mamul_kodu.Text))
                    constraintPairs.Add("@mamulKodu", txt_mamul_kodu.Text);
                if (!string.IsNullOrEmpty(txt_mamul_adi.Text))
                    constraintPairs.Add("@mamulAdi", txt_mamul_adi.Text);

                if (!string.IsNullOrEmpty(txt_ham_kodu.Text))
                    constraintPairs.Add("@hamKodu", txt_ham_kodu.Text);
                if (!string.IsNullOrEmpty(txt_ham_adi.Text))
                    constraintPairs.Add("@hamAdi", txt_ham_adi.Text);
                if (cbx_kod_1.SelectedItem != null)
                    constraintPairs.Add("@kod1", cbx_kod_1.SelectedItem.ToString());

                if (cb_kapali_siparis.IsChecked == true)
                    restrictionQuery += " and SiparisDurum <>'K'";
                if (cb_isemri_olmayan_siparis.IsChecked == true)
                    restrictionQuery += " and ReferansIsemri is not null";
                if (cb_teslim_edilen_siparis.IsChecked == true)
                    restrictionQuery += " and KalanSiparis > 0";

                if (constraintPairs.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                txt_please_wait.Visibility = Visibility.Visible;



                ObservableCollection<Cls_Planlama> IsemriCollection = await plan.GetIsemriKalanIhtiyacAsync(constraintPairs, restrictionQuery);

                if (IsemriCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("İşemirleri Listelenirken");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }
                if (IsemriCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                Frm_IeKalanIhtiyacResult frm = new(IsemriCollection);

                frm.Show();

                txt_please_wait.Visibility = Visibility.Collapsed;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Listelenirken");
            }
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

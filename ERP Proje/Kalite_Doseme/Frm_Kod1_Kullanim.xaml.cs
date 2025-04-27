using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Kalite_Doseme
{
    /// <summary>
    /// Interaction logic for Frm_Kod1_Kullanim.xaml
    /// </summary>
    public partial class Frm_Kod1_Kullanim : Window
    {
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> ihtiyacColl = new();
        ObservableCollection<Cls_Arge> excelCollection = new();
        public Frm_Kod1_Kullanim()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod1.ItemsSource = arge.GetDistinctKod1();
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
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_urun_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (txt_urun_kodu.Text.Substring(0, 1) != "M" &&
                    txt_urun_kodu.Text.Substring(0, 1) != "S")
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Ürün Sorgulanabilir.");
                    return;
                }


                Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
                restrictionPairs.Add("UrunKodu", txt_urun_kodu.Text);

                ComboBox combo = cbx_kod1;
                string selectedItem = cbx_kod1.SelectedItem.ToString();

                if (selectedItem != null)
                    restrictionPairs.Add("Kod1", selectedItem);
                if (ihtiyacColl != null)
                {
                    if (ihtiyacColl.Count > 0)
                        ihtiyacColl.Clear();
                }
                Mouse.OverrideCursor = Cursors.Wait;
                ihtiyacColl = arge.GetUrunHMIhtiyac(restrictionPairs);
                dg_MalzemeListe.ItemsSource = ihtiyacColl;
                dg_MalzemeListe.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Malzeme İhtiyaç Bilgileri Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_stok_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Stok_Karti_Rehberi frm = new();
                var Result = frm.ShowDialog();
                if (Result == true)
                    txt_urun_kodu.Text = frm.SelectedStokKodu;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Stok Kodu Listelenirken");
                Mouse.OverrideCursor = null;
                return;
            }
        }
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

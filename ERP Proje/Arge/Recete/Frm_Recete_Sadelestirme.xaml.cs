using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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

namespace Layer_UI.Arge.Recete
{
    /// <summary>
    /// Interaction logic for Frm_Recete_Sadelestirme.xaml
    /// </summary>
    public partial class Frm_Recete_Sadelestirme : Window
    {
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> argeColl = new();
        public Frm_Recete_Sadelestirme()
        {
            InitializeComponent();
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
        private void cbx_guncelleme_tipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;
                if (combo == null)
                {
                    stc_mamul_sadelestir.Visibility = Visibility.Collapsed;
                    stc_mamul_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_tumunu_sadelestir.Visibility = Visibility.Collapsed;

                    return;
                }
                ComboBoxItem item = combo.SelectedItem as ComboBoxItem;
                if (item == null)
                {
                    stc_mamul_sadelestir.Visibility = Visibility.Collapsed;
                    stc_mamul_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_tumunu_sadelestir.Visibility = Visibility.Collapsed;

                    return;
                }
                if (item.Content.ToString() == "Tümünü Sadeleştir")
                {
                    stc_mamul_sadelestir.Visibility = Visibility.Collapsed;
                    stc_mamul_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_tumunu_sadelestir.Visibility = Visibility.Visible;
                }
                if (item.Content.ToString() == "Şablon Sadeleştir")
                {
                    stc_mamul_sadelestir.Visibility = Visibility.Collapsed;
                    stc_mamul_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir.Visibility = Visibility.Visible;
                    stc_sablon_sadelestir_button.Visibility = Visibility.Visible;
                    stc_tumunu_sadelestir.Visibility = Visibility.Collapsed;
                }
                if (item.Content.ToString() == "Mamul Sadeleştir")
                {
                    stc_mamul_sadelestir.Visibility = Visibility.Visible;
                    stc_mamul_sadelestir_button.Visibility = Visibility.Visible;
                    stc_sablon_sadelestir.Visibility = Visibility.Collapsed;
                    stc_sablon_sadelestir_button.Visibility = Visibility.Collapsed;
                    stc_tumunu_sadelestir.Visibility = Visibility.Collapsed;
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme Tipi Seçilirken");
            }
        }
        private void btn_mamul_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Stok_Karti_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    txt_mamul_kodu.Text = frm.SelectedStokKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Şablon Kod Rehberi Açılırken");
            }
        }
        private void btn_sablon_kod_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Sablon_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    txt_sablon_kod.Text = frm.SelectedStokKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Şablon Kod Rehberi Açılırken");
            }
        }
        private async void btn_tumunu_sadelestir_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.NumberOfAsyncExecutions ++;
                btn_mamul_sadelestir.IsEnabled = false;
                btn_sablon_sadelestir.IsEnabled = false ;
                btn_tumunu_sadelestir.IsEnabled = false;
                Variables.Result_ = await arge.ReceteSadelestirme();
               Variables.NumberOfAsyncExecutions --;
                btn_mamul_sadelestir.IsEnabled = true;
                btn_sablon_sadelestir.IsEnabled = true;
                btn_tumunu_sadelestir.IsEnabled = true;

                txt_please_wait.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün");
                    return;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    return;
                }
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private async void btn_sablon_sadelestir_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_sablon_kod.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                btn_mamul_sadelestir.IsEnabled = false;
                btn_sablon_sadelestir.IsEnabled = false ;
                btn_tumunu_sadelestir.IsEnabled = false;
                ObservableCollection<Cls_Arge> urunList = arge.GetVaryantCodes(txt_sablon_kod.Text);
                if(urunList == null)
                {
                    CRUDmessages.GeneralFailureMessage("Varyant Kodlara Ulaşırken");
                    return;
                }
                if(urunList.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.NumberOfAsyncExecutions++;
                foreach (Cls_Arge urun in urunList)
                {
                    Variables.Result_ = await arge.ReceteSadelestirme(urun.UrunKodu);
                }
               Variables.NumberOfAsyncExecutions --;
                btn_mamul_sadelestir.IsEnabled = true;
                btn_sablon_sadelestir.IsEnabled = true;
                btn_tumunu_sadelestir.IsEnabled = true;

                txt_please_wait.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün");
                    return;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    return;
                }
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private async void btn_mamul_sadelestir_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_mamul_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                btn_mamul_sadelestir.IsEnabled = false;
                btn_sablon_sadelestir.IsEnabled = false ;
                btn_tumunu_sadelestir.IsEnabled = false;
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.NumberOfAsyncExecutions++;
                
                    Variables.Result_ = await arge.ReceteSadelestirme(txt_mamul_kodu.Text);
                
               Variables.NumberOfAsyncExecutions --;
                btn_mamul_sadelestir.IsEnabled = true;
                btn_sablon_sadelestir.IsEnabled = true;
                btn_tumunu_sadelestir.IsEnabled = true;

                txt_please_wait.Visibility = Visibility.Collapsed;
                Mouse.OverrideCursor = null;
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün");
                    return;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    return;
                }
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                txt_please_wait.Visibility = Visibility.Collapsed;
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

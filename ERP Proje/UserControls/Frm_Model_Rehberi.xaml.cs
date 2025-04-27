using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Frm_Model_Rehberi.xaml
    /// </summary>
    public partial class Frm_Model_Rehberi : Window
    {
        Cls_Urun urun = new();
        public string SelectedModelKodu { get; set; } = string.Empty;
        public string SelectedModelIsmi { get; set; } = string.Empty;
        public Frm_Model_Rehberi()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                InitializeComponent();
                dg_Rehber.ItemsSource = urun.GetModelList();
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Model Rehberi Açılırken");
                Mouse.OverrideCursor = null;
                DialogResult = false;
                this.Close();
            }
        }

        private void btn_filtrele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(txt_model_kodu.Text))
                    dict.Add("Kod", txt_model_kodu.Text);
                if (!string.IsNullOrEmpty(txt_model_adi.Text))
                    dict.Add("Ad", txt_model_adi.Text);
                if (dict.Count == 0)
                {
                    CRUDmessages.NoSelection();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                dg_Rehber.ItemsSource = urun.GetModelList(dict);
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_aktar_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Counter_ = 0;
                foreach (Cls_Urun item in dg_Rehber.Items)
                {
                    if (item.IsChecked == true)
                    {
                        SelectedModelKodu = item.ModelKodu;
                        SelectedModelIsmi = item.ModelIsim;
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.GeneralFailureMessageNoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.Counter_ > 1)
                {
                    CRUDmessages.GeneralFailureMessageMoreSelectionThanExpected();
                    Mouse.OverrideCursor = null;
                    return;
                }
                DialogResult = true;
                Close();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kod Bilgisi Aktarılırken");

                Mouse.OverrideCursor = null;
            }
        }
    }
}


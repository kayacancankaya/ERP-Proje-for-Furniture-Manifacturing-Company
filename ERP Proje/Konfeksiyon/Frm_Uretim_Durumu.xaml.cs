using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Konfeksiyon
{
    public partial class Frm_Uretim_Durumu : Window
    {
        public Frm_Uretim_Durumu()
        {
            InitializeComponent(); Window_Loaded();
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
        Variables variables = new Variables();
        public void listele_click(object sender, RoutedEventArgs e)
        {


            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Variables.Query_ = "select * from vbvKonfeksiyonUretimDurum where 1=1";

                if (txt_stok_adi.Text != "")
                {
                    Variables.Query_ = Variables.Query_ + " and mamulAdi like '%" + txt_stok_adi.Text + "%'";
                }

                if (txt_kumas_kod.Text != "")
                {
                    Variables.Query_ = Variables.Query_ + " and kumasKodu like '%" + txt_kumas_kod.Text + "%'";
                }

                DataTable dataTable = SelectStatement.GetDataTable(Variables.Query_, Variables.Yil_);

                dg_genel_durum.ItemsSource = dataTable.DefaultView;

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                MessageBox.Show(ex.Message);
                // Close the modal dialog in case of an error

                Mouse.OverrideCursor = null;
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

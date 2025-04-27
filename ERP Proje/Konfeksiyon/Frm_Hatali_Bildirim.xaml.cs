using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Konfeksiyon
{
    /// <summary>
    /// Interaction logic for Frm_Hatali_Bildirim.xaml
    /// </summary>
    public partial class Frm_Hatali_Bildirim : Window
    {
        public Frm_Hatali_Bildirim()
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

        Variables variables = new();

        public void listele_click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Variables.Query_ = "select * from vbvKumasHatali";

                DataTable dataTable = SelectStatement.GetDataTable(Variables.Query_, Variables.Yil_);
                if (dataTable.Rows.Count == 0)
                {
                    lbl_uyari.Visibility = Visibility.Visible;
                }
                else
                {
                    dg_genel_durum.ItemsSource = dataTable.DefaultView;
                }

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

using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Kalite_Doseme
{
    /// <summary>
    /// Interaction logic for Frm_Ahsaptan_Gelen.xaml
    /// </summary>
    public partial class Frm_Ahsaptan_Gelen : Window
    {
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
        public Frm_Ahsaptan_Gelen()
        {
            InitializeComponent(); Window_Loaded();
        }
        Variables variables = new();
        public void listele_click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Variables.Query_ = "select * from vbvAhsaptanGelen where 1=1 ";

                if (string.IsNullOrEmpty(txt_urun_kodu.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + $"and Urun_Kodu like '%{txt_urun_kodu.Text}%' ";
                }

                if (string.IsNullOrEmpty(txt_urun_adi.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + $"and Urun_Adi like '%{txt_urun_adi.Text}%' ";
                }

                if (string.IsNullOrEmpty(txt_ham_kodu.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + $"and Ham_Kodu like '%{txt_ham_kodu.Text}%' ";
                }

                if (string.IsNullOrEmpty(txt_ham_adi.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + $"and Ham_Adi like '%{txt_ham_adi.Text}%' ";
                }

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
                MessageBox.Show(ex.Message);

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

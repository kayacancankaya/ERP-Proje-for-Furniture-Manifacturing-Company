using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Referans_Isemri_Sil_Toplu.xaml
    /// </summary>
    public partial class Frm_Referans_Isemri_Sil_Toplu : Window
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
        public Frm_Referans_Isemri_Sil_Toplu()
        {
            InitializeComponent(); Window_Loaded();
        }

        ExcelMethodsEPP excel = new();
        Cls_Planlama plan = new();
        ObservableCollection<Cls_Planlama> excelCollection = new();
        private void btn_excel_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                DataTable dataTable = new DataTable();
                if (openFileDialog.ShowDialog() == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    string filePath = openFileDialog.FileName;
                    dataTable = excel.ReadExcelFile(filePath, "referans_isemri_sil", 1, 1, 1);
                }
                else return;

                if (excelCollection != null)
                    excelCollection.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Cls_Planlama plan = new Cls_Planlama
                    {
                        ReferansIsemrino = row["ReferansIsemrino"].ToString()
                    };

                    excelCollection.Add(plan);
                }

                dg_IE_Ekle.ItemsSource = excelCollection;

                txt_pageResult.Text = "Toplam " + dg_IE_Ekle.Items.Count + " adet stok listeleniyor.";
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Excel Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }
        private async void btn_sil_clicked(object sender, EventArgs e)
        {
            try
            {
                if (excelCollection == null)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                if (excelCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }

                txt_please_wait.Visibility = Visibility.Visible;
                Variables.Result_ = await plan.DeleteRefisemriAsync(excelCollection);

                if (Variables.Result_ == false)
                {
                    CRUDmessages.GeneralFailureMessage("İşemirleri Silinirken");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                CRUDmessages.GeneralSuccessMessage("Silme İşlemi");
                txt_please_wait.Visibility = Visibility.Collapsed;
                Frm_Referans_Isemri_Sil_Toplu frm = new();
                frm.Show();
                Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private static DataTable GetDataFromCollection(ObservableCollection<Cls_Planlama> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("ReferansIsemri");


            foreach (Cls_Planlama item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    dataRow["ReferansIsemri"] = item.ReferansIsemrino;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void btn_ie_cikar(object sender, RoutedEventArgs e)
        {
            Variables.Result_ = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (Variables.Result_)
                {
                    Variables.ResultString_ = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("İşemri Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("İşemri Bilgileri Alınırken"); return; }

                    Cls_Planlama dataItem = row.Item as Cls_Planlama;

                    excelCollection.Remove(dataItem);

                    dg_IE_Ekle.ItemsSource = excelCollection;

                    dg_IE_Ekle.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        bool selectMiktarColumn = false;

    }
}

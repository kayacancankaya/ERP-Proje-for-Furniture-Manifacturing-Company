using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Guncelle.xaml
    /// </summary>
    public partial class Frm_Siparis_Guncelle : Window
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
        ObservableCollection<Cls_Siparis> siparisCollection = new();
        Cls_Siparis siparis = new();
        public Frm_Siparis_Guncelle()
        {
            InitializeComponent(); Window_Loaded();
        }

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txt_siparis_no.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                siparisCollection = siparis.GetCustomerOrdersToBeUpdated(txt_siparis_no.Text);
                if (siparisCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (siparisCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_SiparisSecim.ItemsSource = siparisCollection;

                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Listelenirken");
                Mouse.OverrideCursor = null;
            }

        }
        private void btn_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Siparis> ordersToUpdate = new();
                Variables.Counter_ = 0;
                foreach (Cls_Siparis item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked == true)
                    {
                        ordersToUpdate.Add(item);
                        Variables.Counter_++;
                    }

                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txt_po_no.Text))
                {
                    foreach (Cls_Siparis item in ordersToUpdate)
                    {
                        item.POnumarasi = txt_po_no.Text;
                    }
                }
                if (!string.IsNullOrWhiteSpace(txt_destinasyon.Text))
                {
                    foreach (Cls_Siparis item in ordersToUpdate)
                    {
                        item.Destinasyon = txt_destinasyon.Text;
                    }

                }
                if (dp_termin_tarih.SelectedDate != null)
                {
                    foreach (Cls_Siparis item in ordersToUpdate)
                    {
                        item.TerminTarih = dp_termin_tarih.SelectedDate.Value;
                    }
                }

                Variables.Result_ = siparis.UpdatePO_DestinasyonTermin(ordersToUpdate);

                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Güncellenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                CRUDmessages.UpdateSuccessMessage("Sipariş", ordersToUpdate.Count);
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Güncellenirken");
            }

        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Siparis item in dg_SiparisSecim.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }

        bool selectMiktarColumn = false;
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
                            if (dataGrid.Columns[i].Header.ToString() == "Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        // Select the "Miktar" column of the row
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
    }
}

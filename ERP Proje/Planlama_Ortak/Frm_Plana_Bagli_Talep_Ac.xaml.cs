using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Plana_Bagli_Talep_Ac.xaml
    /// </summary>
    public partial class Frm_Plana_Bagli_Talep_Ac : Window
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
        Cls_Planlama plan = new();
        ObservableCollection<Cls_Planlama> talepColl = new();
        Dictionary<string, string> constraintPairs = new();
        public Frm_Plana_Bagli_Talep_Ac()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod_1.ItemsSource = plan.GetKod1();
            cbx_plan_no.ItemsSource = plan.GetPlanNo();
            cbx_plan_adi.ItemsSource = plan.GetPlanAdiForPlanNo();
        }
        private async void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string restrictionQuery = string.Empty;

                if (!string.IsNullOrEmpty(txt_siparis_no.Text))
                    constraintPairs.Add("@siparisNumarasi", txt_siparis_no.Text);
                if (!string.IsNullOrEmpty(txt_siparis_sira.Text))
                    constraintPairs.Add("@siparisSira", txt_siparis_sira.Text);
                if (!string.IsNullOrEmpty(txt_urun_kodu.Text))
                    constraintPairs.Add("@urunKodu", txt_urun_kodu.Text);
                if (!string.IsNullOrEmpty(txt_urun_adi.Text))
                    constraintPairs.Add("@urunAdi", txt_urun_adi.Text);

                if (cbx_plan_adi.SelectedItem != null)
                    constraintPairs.Add("@planAdi", cbx_plan_adi.SelectedItem.ToString());
                if (cbx_plan_no.SelectedItem != null)
                    constraintPairs.Add("@planNo", cbx_plan_no.SelectedItem.ToString());

                if (!string.IsNullOrEmpty(txt_ham_kodu.Text))
                    constraintPairs.Add("@hamKodu", txt_ham_kodu.Text);
                if (!string.IsNullOrEmpty(txt_ham_adi.Text))
                    constraintPairs.Add("@hamAdi", txt_ham_adi.Text);
                if (cbx_kod_1.SelectedItem != null)
                    constraintPairs.Add("@kod1", cbx_kod_1.SelectedItem.ToString());


                Mouse.OverrideCursor = Cursors.Wait;


                ObservableCollection<Cls_Planlama> firstColl = plan.GetPlanaBagliTalepListesi(constraintPairs, 1);

                if (firstColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (firstColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                talepColl = firstColl;
                totalResult = plan.CountPlanaBagliTalep(constraintPairs, 1);
                dg_plana_bagli_talep.ItemsSource = firstColl;

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                dg_plana_bagli_talep.ItemsSource = talepColl;
                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                txt_result.Visibility = Visibility.Collapsed;
            }
        }
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                lastOffset = e.NewValue;
                if (lastOffset > pageValueChanged + 100 && totalResult > talepColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Planlama> moreSiparisCollection = new();
                    moreSiparisCollection = plan.GetPlanaBagliTalepListesi(constraintPairs, pageNumber);
                    if (moreSiparisCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Siparişler Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreSiparisCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Planlama> lastTalepCollection = new ObservableCollection<Cls_Planlama>
                                        (talepColl.Union(moreSiparisCollection).ToList());
                        dg_plana_bagli_talep.ItemsSource = lastTalepCollection;
                        dg_plana_bagli_talep.Items.Refresh();
                        talepColl = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_plana_bagli_talep, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }

                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                    lastOffset += e.Delta * -1 / 48;
                else
                    lastOffset -= e.Delta / 48;

                if (lastOffset > pageValueChanged + 100 && totalResult > talepColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Planlama> moreTalepCollection = new();
                    moreTalepCollection = plan.GetPlanaBagliTalepListesi(constraintPairs, pageNumber);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Planlama> lastTalepCollection = new ObservableCollection<Cls_Planlama>
                                        (talepColl.Union(moreTalepCollection).ToList());
                        dg_plana_bagli_talep.ItemsSource = lastTalepCollection;
                        dg_plana_bagli_talep.Items.Refresh();
                        talepColl = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_plana_bagli_talep, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Planlama item in dg_plana_bagli_talep.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private bool selectMiktarColumn = false;
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
                            if (dataGrid.Columns[i].Header.ToString() == "Gönderilecek İşemri Miktar" ||
                                dataGrid.Columns[i].Header.ToString() == "İşemri Açıklama")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

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
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContex associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {
                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up
                }
            }

        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {

                DataGridCellInfo cellInfo = dg_plana_bagli_talep.CurrentCell;

                if (cellInfo.Column.DisplayIndex != null)
                {
                    if (cellInfo.Column.DisplayIndex == 6)
                    {

                        var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);


                        if (cellContent is TextBox textBox)
                        {
                            // Clear the text inside the cell
                            textBox.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
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

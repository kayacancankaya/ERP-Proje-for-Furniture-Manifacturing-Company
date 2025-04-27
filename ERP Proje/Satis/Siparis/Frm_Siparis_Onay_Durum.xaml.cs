using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Popups;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Layer_UI.Satis.Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Onay_Durum.xaml
    /// </summary>
    public partial class Frm_Siparis_Onay_Durum : Window
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
        Cls_Siparis siparis = new();
        public Frm_Siparis_Onay_Durum()
        {
            InitializeComponent(); Window_Loaded();
            PopulateOnayBekleyenSiparislerDataGrid();
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Siparis item in dg_siparis_onay_durum.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }

        private void PopulateOnayBekleyenSiparislerDataGrid()
        {
            try
            {
                dg_siparis_onay_durum.ItemsSource = null;
                dg_siparis_onay_durum.Items.Clear();

                siparis.SiparisCollection = siparis.GetOnayBekleyenSiparisler();
                dg_siparis_onay_durum.ItemsSource = siparis.SiparisCollection;

                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Mouse.OverrideCursor = null;
                return;
            }
        }

        Variables variables = new();

        private void btn_detay_goster(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Sipariş Detayı Gösterilirken Problem İle Karşılaşıldı."); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                variables.ErrorMessage = row == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;

                // Get the data item associated with the row
                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                string fisno = string.Empty;

                variables.ErrorMessage = dataItem == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;
                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; };


                fisno = dataItem.Fisno;

                Popup_Onay_Bekleyen_Siparis_Goster _popUp = new Popup_Onay_Bekleyen_Siparis_Goster(fisno);

                _popUp.ShowDialog();


            }

            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }

        Cls_Siparis siparisDuzenleGenel = new();
        Cls_Siparis siparisDuzenleSatir = new();
        private void btn_siparis_duzenle(object sender, RoutedEventArgs e)
        {
            variables.ErrorMessage = string.Empty;
            Mouse.OverrideCursor = Cursors.Wait;

            Button? button = sender as Button;
            if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
            DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

            if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

            // Get the data item associated with the row
            Cls_Siparis? dataItem = row.Item as Cls_Siparis;

            if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

            try
            {
                siparisDuzenleGenel.SiparisCollection = siparisDuzenleGenel.GetDuzenleOnayBekleyenSiparisler(dataItem.Fisno);
                siparisDuzenleSatir.SiparisCollection = siparisDuzenleSatir.GetSiparisSatirInfo(dataItem.Fisno, "Onay");

                Popup_Onay_Bekleyen_Siparis_Guncelle _Guncelle = new Popup_Onay_Bekleyen_Siparis_Guncelle(siparisDuzenleGenel.SiparisCollection,
                                                                                                          siparisDuzenleSatir.SiparisCollection,
                                                                                                          "Satış");

                _Guncelle.ShowDialog();
            }
            catch (Exception ex) { CRUDmessages.GeneralFailureMessage("Sipariş Güncelleme Ekranı Açılırken "); Mouse.OverrideCursor = null; };

        }
        private void btn_siparis_sil(object sender, RoutedEventArgs e)
        {

            variables.IsTrue = CRUDmessages.DeleteOnayMessage();


            if (variables.IsTrue)
            {
                variables.ErrorMessage = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                // Get the data item associated with the row
                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                try
                {
                    variables.IsTrue = siparis.DeleteOnayBekleyenSiparis(dataItem.Fisno);

                    if (variables.IsTrue) { CRUDmessages.DeleteSuccessMessage("Sipariş", 1); Mouse.OverrideCursor = null; }
                    else { CRUDmessages.InsertFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }

                    PopulateOnayBekleyenSiparislerDataGrid();
                }

                catch { CRUDmessages.InsertFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }
            }
            else return;

        }
    }
}

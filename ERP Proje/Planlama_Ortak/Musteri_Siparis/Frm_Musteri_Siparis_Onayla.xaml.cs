using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satın_Alma;
using Layer_UI.Satis.Popups;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Ortak.Musteri_Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Musteri_Siparis_Onayla.xaml
    /// </summary>
    public partial class Frm_Musteri_Siparis_Onayla : Window
    {
        Cls_Siparis siparis = new();
        ObservableCollection<Cls_Siparis> onayBekleyenSiparisMasColl = new();
        ObservableCollection<Cls_Siparis> clonedColl = new();
        public Frm_Musteri_Siparis_Onayla()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                InitializeComponent(); Window_Loaded();
                onayBekleyenSiparisMasColl = siparis.GetOnayBekleyenSiparisler();
                dg_siparis_onay_durum.ItemsSource = onayBekleyenSiparisMasColl;
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                Mouse.OverrideCursor = null;
                if (Application.Current.MainWindow == this)
                {
                    Frm_Talep_Siparislestir frm = new();
                    frm.Show();
                    this.Close();
                }
                else
                    this.Close();
            }
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
        private void btn_detay_goster(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;
                clonedColl = new ObservableCollection<Cls_Siparis>(onayBekleyenSiparisMasColl);
                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                ObservableCollection<Cls_Siparis> siparisDetayColl = siparis.GetDuzenleSiparisSatirInfo(dataItem.Fisno);

                if (siparisDetayColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Detayı Listelenirken");
                    dg_siparis_onay_durum.ItemsSource = clonedColl;
                    dg_siparis_onay_durum.Items.Refresh();
                    onayBekleyenSiparisMasColl = clonedColl;
                    Mouse.OverrideCursor = null;
                    return;

                }
                if (siparisDetayColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    dg_siparis_onay_durum.ItemsSource = clonedColl;
                    dg_siparis_onay_durum.Items.Refresh();
                    onayBekleyenSiparisMasColl = clonedColl;
                    Mouse.OverrideCursor = null;
                    return;
                }

                Frm_Musteri_Siparis_Onayla_Detay frm = new(new ObservableCollection<Cls_Siparis>(siparisDetayColl)); 
                var formResult = frm.ShowDialog();
                if (formResult == false)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    dg_siparis_onay_durum.ItemsSource = clonedColl;
                    dg_siparis_onay_durum.Items.Refresh();
                    onayBekleyenSiparisMasColl = clonedColl;
                    Mouse.OverrideCursor = null;
                }

            }

            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }

        }
        private void btn_update_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                try
                {
                    Cls_Siparis siparisDuzenleGenel = new();
                    Cls_Siparis siparisDuzenleSatir = new();

                    siparisDuzenleGenel.SiparisCollection = siparisDuzenleGenel.GetDuzenleOnayBekleyenSiparisler(dataItem.Fisno);
                    siparisDuzenleSatir.SiparisCollection = siparisDuzenleSatir.GetSiparisSatirInfo(dataItem.Fisno, "Onay");

                    Popup_Onay_Bekleyen_Siparis_Guncelle _Guncelle = new Popup_Onay_Bekleyen_Siparis_Guncelle(siparisDuzenleGenel.SiparisCollection,
                                                                                                              siparisDuzenleSatir.SiparisCollection,
                                                                                                              "Planlama");

                    var result = _Guncelle.ShowDialog();
                    if (result == false)
                    {
                        onayBekleyenSiparisMasColl = siparis.GetOnayBekleyenSiparisler();
                        dg_siparis_onay_durum.ItemsSource = onayBekleyenSiparisMasColl;
                        dg_siparis_onay_durum.Items.Refresh();

                    }

                }
                catch (Exception ex) { CRUDmessages.GeneralFailureMessage("Sipariş Güncelleme Ekranı Açılırken "); Mouse.OverrideCursor = null; };
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme  İşlemi Gerçekleşirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_delete_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }

                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }


                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = siparis.DeleteOnayBekleyenSiparis(dataItem.Fisno);
                if (Variables.Result_)
                    CRUDmessages.DeleteSuccessMessage("Sipariş");
                else
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleştirilirken");

                onayBekleyenSiparisMasColl = siparis.GetOnayBekleyenSiparisler();
                dg_siparis_onay_durum.ItemsSource = onayBekleyenSiparisMasColl;
                dg_siparis_onay_durum.Items.Refresh();

                Mouse.OverrideCursor = null;


            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleşirken");
                Mouse.OverrideCursor = null;
            }


        }

        private void siparislestir(object sender, RoutedEventArgs e)
        {
            try
            {

                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;

                Image? image = sender as Image;
                if (image == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(image);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                try
                {

                    Variables.Result_ = siparis.OnayBekleyenSiparislestir(dataItem.Fisno);

                    if (Variables.Result_)
                        CRUDmessages.UpdateSuccessMessage("Sipariş");
                    else
                    { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }

                    onayBekleyenSiparisMasColl = siparis.GetOnayBekleyenSiparisler();
                    dg_siparis_onay_durum.ItemsSource = onayBekleyenSiparisMasColl;
                    dg_siparis_onay_durum.Items.Refresh();

                    Mouse.OverrideCursor = null;

                }

                catch { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; return;
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void dg_siparis_onay_durum_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            var item = e.Row.Item as Cls_Siparis; 

            if (item != null)
            {
                if(!item.DoesUrunAgaciExists)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                    e.Row.Foreground = new SolidColorBrush(Colors.White);
                    e.Row.FontWeight = FontWeights.Bold;
                    e.Row.FontStyle = FontStyles.Normal;
                }
            }
          
        }

    }
}

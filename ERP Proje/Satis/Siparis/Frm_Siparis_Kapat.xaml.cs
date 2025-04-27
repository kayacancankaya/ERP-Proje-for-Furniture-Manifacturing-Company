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
    public partial class Frm_Siparis_Kapat : Window
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
        Variables variables = new();
        Cls_Siparis siparis = new();
        public Frm_Siparis_Kapat()
        {
            InitializeComponent(); Window_Loaded();
        }
        public Frm_Siparis_Kapat(string siparisNo)
        {
            InitializeComponent(); Window_Loaded();
            txt_siparis_no.Text = siparisNo;
            SiparisDurumGoster(siparisNo);
        }
        private void SiparisDurumGoster(string siparisNo)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                dg_siparis_onay_durum.ItemsSource = null;
                siparis.SiparisCollection.Clear();

                siparis.SiparisCollection = siparis.GetSiparisGenelInfo(txt_siparis_no.Text);

                if (siparis.SiparisCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Bilgisi Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_siparis_onay_durum.ItemsSource = siparis.SiparisCollection;
                Mouse.OverrideCursor = null;
            }
            catch
            {

                Mouse.OverrideCursor = null; CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_siparis_no.Text))
            {
                CRUDmessages.NoInput();
                return;
            }
            SiparisDurumGoster(txt_siparis_no.Text);
        }
        private void btn_detay_goster(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;
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

                Popup_Kapanacak_Siparis_Detay_Goster _popUp = new Popup_Kapanacak_Siparis_Detay_Goster(fisno);

                Mouse.OverrideCursor = null;

                _popUp.ShowDialog();

                if (_popUp.IsClosedSuccessfully)
                    SiparisDurumGoster(txt_siparis_no.Text);

            }

            catch (Exception ex)
            {
                Mouse.OverrideCursor = null; MessageBox.Show(ex.Message.ToString());
            }

        }
        private void siparis_kapat_ac(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.IsTrue = CRUDmessages.UpdateOnayMessage();


                if (variables.IsTrue)
                {
                    variables.ErrorMessage = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Image? image = sender as Image;
                    if (image == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(image);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                    // Get the data item associated with the row
                    Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                    if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                    try
                    {
                        bool kapat_ = true;

                        if (dataItem.SiparisDurum == "K")
                        {
                            kapat_ = false;
                        }

                        variables.IsTrue = siparis.SiparisKapatAc(dataItem.Fisno, kapat_);

                        if (variables.IsTrue)
                            CRUDmessages.UpdateSuccessMessage("Sipariş");
                        else
                        { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; return; }

                        siparis.SiparisCollection = siparis.GetSiparisGenelInfo(txt_siparis_no.Text);

                        if (siparis.SiparisCollection == null)
                        {
                            CRUDmessages.GeneralFailureMessage("Sayfa Yenilenirken");
                            Mouse.OverrideCursor = null;
                            return;
                        }

                        Frm_Siparis_Kapat _frm = new Frm_Siparis_Kapat(txt_siparis_no.Text);
                        _frm.Show();
                        this.Close();

                    }

                    catch { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }
                }
                else return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); return;
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
    }
}

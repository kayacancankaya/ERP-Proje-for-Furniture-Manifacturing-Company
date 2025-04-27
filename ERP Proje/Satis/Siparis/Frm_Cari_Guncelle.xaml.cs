using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Cari_Guncelle.xaml
    /// </summary>
    public partial class Frm_Cari_Guncelle : Window
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
        public Frm_Cari_Guncelle()
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

                siparisCollection = siparis.GetCustomerOrdersToBeUpdatedCari(txt_siparis_no.Text);
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
                Variables.Counter_ = 0;
                if (!string.IsNullOrWhiteSpace(txt_satis_cari_kodu.Text))
                {
                    int checkNumber;

                    if (!int.TryParse(txt_satis_cari_kodu.Text.Substring(0, 3), out checkNumber))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Cari Kodunun İlk 3 Harfi Rakam Olmalı");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    if (txt_satis_cari_kodu.Text.Length < 10)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Cari Kodu 10 Haneden Küçük Olamaz");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    Variables.Result_ = siparis.CheckIfCariExists(txt_satis_cari_kodu.Text);
                    if (!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sistemde Cari Kodu Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    foreach (Cls_Siparis item in ordersToUpdate)
                    {
                        item.AssociatedCari.SatisCariKodu = txt_satis_cari_kodu.Text;
                    }
                    Variables.Counter_++;
                }
                if (!string.IsNullOrWhiteSpace(txt_teslim_cari_kodu.Text))
                {
                    int checkNumber;

                    if (!int.TryParse(txt_teslim_cari_kodu.Text.Substring(0, 3), out checkNumber))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Cari Kodunun İlk 3 Harfi Rakam Olmalı");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    if (txt_teslim_cari_kodu.Text.Length < 10)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Cari Kodu 10 Haneden Küçük Olamaz");
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    Variables.Result_ = siparis.CheckIfCariExists(txt_teslim_cari_kodu.Text);
                    if (!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sistemde Cari Kodu Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    foreach (Cls_Siparis item in ordersToUpdate)
                    {
                        item.AssociatedCari.TeslimCariKodu = txt_teslim_cari_kodu.Text;
                    }
                    Variables.Counter_++;
                }


                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }

                var formresult = CRUDmessages.UpdateOnayMessage();
                if (formresult == false)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.Result_ = siparis.UpdateMusteriSiparisCari(ordersToUpdate);

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

        private void btn_cari_kodu_getir_teslim_cari_clicked(object sender, RoutedEventArgs e)
        {

            var cariSecimWindow = new Popup_Cari_Secim_Single_Musteri_Siparis();
            // Show the popup window
            cariSecimWindow.ShowDialog();

            var result = cariSecimWindow.DialogResult;
            if (result == true)
            {
                if (string.IsNullOrEmpty(cariSecimWindow.CariKodu))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Cari Bilgileri Getirilemedi. Tekrar Deneyiniz.");
                    return;
                }

                txt_teslim_cari_kodu.Text = cariSecimWindow.CariKodu;
            }

        }
        private void btn_cari_kodu_getir_satis_cari_clicked(object sender, RoutedEventArgs e)
        {

            var cariSecimWindow = new Popup_Cari_Secim_Single_Musteri_Siparis();
            // Show the popup window
            cariSecimWindow.ShowDialog();

            var result = cariSecimWindow.DialogResult;
            if (result == true)
            {
                if (string.IsNullOrEmpty(cariSecimWindow.CariKodu))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Cari Bilgileri Getirilemedi. Tekrar Deneyiniz.");
                    return;
                }

                txt_satis_cari_kodu.Text = cariSecimWindow.CariKodu;
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

        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

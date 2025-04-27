using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Recete_Guncelle_Mail.xaml
    /// </summary>
    public partial class Frm_Recete_Guncelle_Mail : Window
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
        Cls_Arge arge = new();
        Variables variables = new();
        ObservableCollection<Cls_Arge> mailCollection = new();
        ObservableCollection<Cls_Isemri> receteCollection = new();
        ObservableCollection<Cls_Isemri> receteDetayCollection = new();
        bool tamamlananIsemirleriniGosterme = false;
        public Frm_Recete_Guncelle_Mail()
        {
            try
            {

                InitializeComponent(); Window_Loaded();

                Mouse.OverrideCursor = Cursors.Wait;

                mailCollection = arge.PopulateIsemriReceteMailGuncellenecekList();
                dg_GuncellenenReceteler.ItemsSource = mailCollection;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Sayfa Yüklenirken Hata İle Karşılaşıldı. Tekrar Yüklemeyi Deneyiniz.");
                Mouse.OverrideCursor = null;
            }
        }

        public async void btn_degisiklik_uygula(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                if (mailCollection.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (receteCollection != null) receteCollection.Clear();

                if (cb_tamamlananlari_listeleme.IsChecked == true)
                    tamamlananIsemirleriniGosterme = true;
                else
                    tamamlananIsemirleriniGosterme = false;

                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                {
                    txt_please_wait.Visibility = Visibility.Collapsed;
                    return;
                }

                Variables.ResultString_ = await arge.MailGelenIsemriRecetesiGuncelle(mailCollection, tamamlananIsemirleriniGosterme);

                if (Variables.ResultString_ == "Başarı")
                {
                    CRUDmessages.GeneralSuccessMessage("Reçeteler Güncelleme İşlemi");
                    txt_please_wait.Visibility = Visibility.Collapsed;
                }
                else
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ResultString_);
                    txt_please_wait.Visibility = Visibility.Collapsed;
                }

                mailCollection = arge.PopulateIsemriReceteMailGuncellenecekList();
                dg_GuncellenenReceteler.ItemsSource = mailCollection;
                dg_GuncellenenReceteler.Items.Refresh();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Reçete Güncellenirken");
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private void btn_recete_sil(object sender, RoutedEventArgs e)
        {

            try
            {
                variables.ErrorMessage = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }

                Cls_Arge dataItem = row.Item as Cls_Arge;

                mailCollection.Remove(dataItem);

                dg_GuncellenenReceteler.ItemsSource = mailCollection;

                dg_GuncellenenReceteler.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_guncellenmis_yap_clicked(object sender, RoutedEventArgs e)
        {

            try
            {
                variables.ErrorMessage = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); return; }

                Cls_Arge dataItem = row.Item as Cls_Arge;

                Variables.Result_ = arge.UpdateReceteGuncellenmisGoster(dataItem);

                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Reçete Güncelleme Durumu Değiştirilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                CRUDmessages.GeneralSuccessMessage("Günceleme Durumu Değiştirme");

                mailCollection.Remove(dataItem);

                dg_GuncellenenReceteler.ItemsSource = mailCollection;

                dg_GuncellenenReceteler.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch { CRUDmessages.GeneralFailureMessage("Reçete Güncelleme Durumu Değiştirilirken"); }

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Arge item = UIinteractions.GetDataItemFromButton<Cls_Arge>(sender);

                if (item == null)
                { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                if (cb_tamamlananlari_listeleme.IsChecked == true)
                    tamamlananIsemirleriniGosterme = true;
                else
                    tamamlananIsemirleriniGosterme = false;

                receteDetayCollection = arge.PopulateReceteDetayCollection(item, tamamlananIsemirleriniGosterme);
                if (receteDetayCollection == null)
                { CRUDmessages.GeneralFailureMessage("Reçete Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                if (!receteDetayCollection.Any())
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                Frm_Recete_Guncelle_Mail_Detay _popup = new(receteDetayCollection);
                _popup.ShowDialog();


            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Reçeteleri Listelenirken"); Mouse.OverrideCursor = null;
            }
        }
    }
}

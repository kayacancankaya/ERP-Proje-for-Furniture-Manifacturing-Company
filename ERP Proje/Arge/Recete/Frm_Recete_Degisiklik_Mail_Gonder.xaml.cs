using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Recete
{
    /// <summary>
    /// Interaction logic for Frm_Recete_Degisiklik_Mail_Gonder.xaml
    /// </summary>
    public partial class Frm_Recete_Degisiklik_Mail_Gonder : Window
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
        ObservableCollection<Cls_Arge> argeCollection = new();
        public Frm_Recete_Degisiklik_Mail_Gonder()
        {
            try
            {
                InitializeComponent(); Window_Loaded();

                Mouse.OverrideCursor = Cursors.Wait;

                argeCollection = arge.PopulateReceteMailGonderilecekList();
                dg_GuncellenenReceteler.ItemsSource = argeCollection;

                Mouse.OverrideCursor = null;

            }

            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Sayfa Yüklenirken Hata İle Karşılaşıldı. Tekrar Yüklemeyi Deneyiniz.");
                Mouse.OverrideCursor = null;
            }
        }
        public async void btn_mail_gonder(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_please_wait.Visibility = Visibility.Visible;
                if (argeCollection.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                Variables.ResultInt_ = await arge.GuncellenenReceteMailGonder(argeCollection);

                switch (Variables.ResultInt_)
                {
                    case 1:
                        CRUDmessages.GeneralSuccessMessage("Mail Gönderme İşlemi");
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                    case -1:
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                    case -2:
                        CRUDmessages.GeneralFailureMessage("Mail Tablosuna Kayıt Atılırken");
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                    case -3:
                        CRUDmessages.GeneralFailureMessage("Departman İsmi Alınırken");
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                    case -4:
                        CRUDmessages.GeneralFailureMessage("Mail Gönderilirken");
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                }

                argeCollection = arge.PopulateReceteMailGonderilecekList();
                dg_GuncellenenReceteler.ItemsSource = argeCollection;
                dg_GuncellenenReceteler.Items.Refresh();


            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Mail Gönderilirken");
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

                argeCollection.Remove(dataItem);

                dg_GuncellenenReceteler.ItemsSource = argeCollection;

                dg_GuncellenenReceteler.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_gonderilmis_yap_clicked(object sender, RoutedEventArgs e)
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

                Variables.Result_ = arge.UpdateReceteGuncelleMailStatus(dataItem);

                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Mail Gönderim Durumu Değiştirilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                CRUDmessages.GeneralSuccessMessage("Mail Listesinden Kaldırma İşlemi");

                argeCollection.Remove(dataItem);

                dg_GuncellenenReceteler.ItemsSource = argeCollection;

                dg_GuncellenenReceteler.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch { CRUDmessages.GeneralFailureMessage("Mail Gönderim Durumu Değiştirilirken"); }

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

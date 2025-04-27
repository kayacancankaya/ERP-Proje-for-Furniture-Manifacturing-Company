using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Satis.Sevk.Popups;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk
{
    /// <summary>
    /// Interaction logic for Frm_Sevk_Guncelle.xaml
    /// </summary>
    public partial class Frm_Sevk_Guncelle : Window
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
        Cls_Sevk sevk = new();
        Variables variables = new();
        public Frm_Sevk_Guncelle()
        {
            InitializeComponent(); Window_Loaded();

            sevk.SevkCollection = sevk.PopulateSevkGuncelleList();
            if (sevk.SevkCollection.Any())
                dg_sevk_guncelle.ItemsSource = sevk.SevkCollection;
            else
            { CRUDmessages.QueryIsEmpty("Açık Sevk Emri"); return; }

        }

        string sevkEmrino = string.Empty;
        private void btn_detay_goster(object sender, RoutedEventArgs e)
        {
            try
            {

                Cls_Sevk dataItem = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);

                sevkEmrino = dataItem.SevkEmriNo;

                Popup_Sevk_Guncelle_Satir _popUp = new Popup_Sevk_Guncelle_Satir(sevkEmrino);

                _popUp.ShowDialog();


            }

            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_add_row(object sender, RoutedEventArgs e)
        {
            try
            {
                Cls_Sevk dataItem = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);
                sevkEmrino = dataItem.SevkEmriNo;
                Popup_Sevk_Guncelle_Satir_Ekle _popup = new Popup_Sevk_Guncelle_Satir_Ekle(sevkEmrino);
                _popup.ShowDialog();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Gerçekleşirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sevk_sil(object sender, RoutedEventArgs e)
        {
            try
            {
                variables.Result = CRUDmessages.DeleteOnayMessage();

                if (!variables.Result)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Sevk dataItem = UIinteractions.GetDataItemFromButton<Cls_Sevk>(sender);

                sevkEmrino = dataItem.SevkEmriNo;
                if (string.IsNullOrWhiteSpace(sevkEmrino))
                { CRUDmessages.GeneralFailureMessage("Sevk Emri Numarası Alınırken"); Mouse.OverrideCursor = null; return; }

                variables.Result = sevk.DeleteYuklenmemisSevkEmriMas(sevkEmrino);
                if (!variables.Result)
                { CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleşirken"); Mouse.OverrideCursor = null; return; }

                CRUDmessages.DeleteSuccessMessage("Sevk");
                Frm_Sevk_Guncelle frm_ = new();
                Mouse.OverrideCursor = null;
                frm_.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Gerçekleşirken");
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

using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Depo.Stok_Hareket
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Frm_Hareket_Gormeyen_Stok : Window
    {
        public DateTime SelectedDate { get; set; } = DateTime.MinValue;
        public Frm_Hareket_Gormeyen_Stok()
        {
            InitializeComponent(); Window_Loaded();
            SelectedDate = DateTime.Today.AddMonths(-6);
            txt_baslangic_tarih.SelectedDate = SelectedDate;
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

        Variables variables = new();
        Cls_Depo depo = new();


        private void btn_mamul_paket_clicked(object sender, RoutedEventArgs e)
        {

            try
            {

                if (txt_baslangic_tarih.SelectedDate == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Giriniz.");
                }

                SelectedDate = Convert.ToDateTime(txt_baslangic_tarih.SelectedDate, CultureInfo.InvariantCulture);

                hareket_gormeyen_sorgu(SelectedDate, 1);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Hareket etmeyen stoklar listelenirken"); Mouse.OverrideCursor = null;
            }

        }


        private void btn_hammadde_clicked(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txt_baslangic_tarih.SelectedDate == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Giriniz.");
                }

                SelectedDate = Convert.ToDateTime(txt_baslangic_tarih.SelectedDate, CultureInfo.InvariantCulture);

                hareket_gormeyen_sorgu(SelectedDate, 2);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Hareket etmeyen stoklar listelenirken"); Mouse.OverrideCursor = null;
            }

        }
        private void btn_hepsi_clicked(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txt_baslangic_tarih.SelectedDate == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Giriniz.");
                }

                SelectedDate = Convert.ToDateTime(txt_baslangic_tarih.SelectedDate, CultureInfo.InvariantCulture);

                hareket_gormeyen_sorgu(SelectedDate, 3);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Hareket etmeyen stoklar listelenirken"); Mouse.OverrideCursor = null;
            }

        }

        private void hareket_gormeyen_sorgu(DateTime selectedDate, int sorguTipi)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Depo> hareketGormeyenColl = depo.PopulateHareketsizStokList(SelectedDate, sorguTipi);

                if (hareketGormeyenColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Hareket Görmeyen Stoklar Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (!hareketGormeyenColl.Any())
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                dg_HareketGormeyenRapor.ItemsSource = hareketGormeyenColl;

                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {

                CRUDmessages.GeneralFailureMessage("Hareket etmeyen stoklar listelenirken"); Mouse.OverrideCursor = null; ;
            }
        }

        private void detail_button_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {

                throw;
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

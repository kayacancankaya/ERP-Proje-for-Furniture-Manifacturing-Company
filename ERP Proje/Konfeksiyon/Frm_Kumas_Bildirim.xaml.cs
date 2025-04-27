using Layer_2_Common.Type;
using Layer_Business;
using Layer_Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Layer_UI.Konfeksiyon
{

    public partial class Frm_Kumas_Bildirim : Window
    {
        public Frm_Kumas_Bildirim()
        {
            InitializeComponent(); Window_Loaded();
            lbl_bos_liste.Visibility = Visibility.Collapsed;
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

        Cls_Isemri cls_isemri = new Cls_Isemri();
        Variables variables = new Variables();

        private void listele_click(object sender, RoutedEventArgs e)
        {

            lbl_bos_liste.Visibility = Visibility.Collapsed;

            Mouse.OverrideCursor = Cursors.Wait;

            dp_tarih.SelectedDate = DateTime.Now;

            try
            {
                Variables.Query_ = "select * from vbvKumasUretimDurum where 1=1";


                if ((string.IsNullOrEmpty(txt_ham_adi.Text) || string.IsNullOrEmpty(txt_takip_no.Text)))
                {
                    MessageBox.Show("Ham Adı ve Takip No Giriniz.");
                    Mouse.OverrideCursor = null;
                    return;
                }


                if (string.IsNullOrEmpty(txt_ham_adi.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + " and HAM_ADI like '%" + txt_ham_adi.Text + "%'";
                }

                if (string.IsNullOrEmpty(txt_takip_no.Text) == false)
                {
                    Variables.Query_ = Variables.Query_ + " and TAKIP_NO like '%" + txt_takip_no.Text + "%'";
                }

                gv_bildirim.ItemsSource = null;
                gv_bildirim.Items.Clear();

                cls_isemri.KumulatifIhtiyac = cls_isemri.IsemirleriToplamIhtiyacHesapla(Variables.Query_, Variables.Yil_);

                cls_isemri.IsemirleriCollection = cls_isemri.PopulateGridViewWithIsemirleriCollection(Variables.Query_, Variables.Yil_);

                gv_bildirim.ItemsSource = cls_isemri.IsemirleriCollection;

                Mouse.OverrideCursor = null;

                if (gv_bildirim.Items.Count > 0)
                {
                    btn_bildir.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl_bos_liste.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                MessageBox.Show(ex.Message);
                // Close the modal dialog in case of an error

                Mouse.OverrideCursor = null;

            }
        }
        private void btn_bildir_click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            DateTime? selectedDate = dp_tarih.SelectedDate;


            int kullanici_kodu = 78;
            string tarih = selectedDate.Value.ToString("yyyy-MM-dd");
            DateTime simdikiZaman = DateTime.Now;
            string kayitTarih = simdikiZaman.ToString("yyyy-MM-dd");

            if (string.IsNullOrEmpty(txt_ham_adi.Text)) { MessageBox.Show("Lütfen Ham Adı Giriniz."); Mouse.OverrideCursor = null; return; }
            if (string.IsNullOrEmpty(txt_takip_no.Text)) { MessageBox.Show("Lütfen Takip No Giriniz."); Mouse.OverrideCursor = null; return; }

            if (selectedDate.HasValue == false || selectedDate >= DateTime.Now) { MessageBox.Show("Lütfen Geçerli Bir Tarih Seçiniz."); Mouse.OverrideCursor = null; return; }

            if (string.IsNullOrEmpty(txt_metre.Text)) { MessageBox.Show("Metre Bilgisi Giriniz."); Mouse.OverrideCursor = null; return; }

            if (!EntryControls.IsValidDecimal(txt_metre.Text)) { MessageBox.Show("Metre Formatı Hatalı."); Mouse.OverrideCursor = null; return; }

            Variables.Query_ = string.Empty;

            string Query_2 = "insert into sbptUretimSonu (isEmriNo,stokKodu,uretimMiktari,uretimSonuFisNo,fireMiktari,girisDepo,cikisDepo,seriNo,aktarildiMi,hataliMi,tarih,kayitTarihi,subeKodu,userID) values ";

            decimal kumulatifToplamIhtiyac = 0;

            foreach (Cls_Isemri item in gv_bildirim.Items)
            {

                if (item.IsChecked == true)
                {
                    decimal birimToplamIhtiyac = item.BildirilecekIsemriMiktar * item.BIRIM_HAM_MIKTAR;
                    kumulatifToplamIhtiyac = kumulatifToplamIhtiyac + birimToplamIhtiyac;
                }

            }

            string metre = txt_metre.Text.Replace(".", ",");

            decimal katSayi = Convert.ToDecimal(metre) / kumulatifToplamIhtiyac;


            variables.Counter = 0;
            foreach (Cls_Isemri item in gv_bildirim.Items)
            {

                if (item.IsChecked == true)
                {
                    if (item.BildirilecekIsemriMiktar > item.KALAN_IE_MIKTAR) { MessageBox.Show("Bildirilecek Miktar Kalan Miktardan Büyük Olamaz."); return; }
                    if (item.BildirilecekIsemriMiktar == 0) { MessageBox.Show("Bildirilecek Miktar 0 Olamaz."); return; }


                    decimal receteDuzeltilmisMiktar = katSayi * item.BIRIM_HAM_MIKTAR;
                    int decimalPlaces = 5; // Number of decimal places you want

                    receteDuzeltilmisMiktar = Math.Round(receteDuzeltilmisMiktar, decimalPlaces);
                    string receteDuzeltilmisMiktarString = receteDuzeltilmisMiktar.ToString();
                    receteDuzeltilmisMiktarString = receteDuzeltilmisMiktarString.Replace(",", ".");

                    Variables.Query_ = Variables.Query_ + $"UPDATE TBLISEMRIREC SET MIKTAR='{receteDuzeltilmisMiktarString}' where inckeyno='{item.ID}' ";

                    Query_2 = Query_2 + $"('{item.ISEMRINO}', '{item.STOK_KODU}', '{item.BildirilecekIsemriMiktar}','','0', '40', '15','UST', '0', '0', '{tarih}', '{kayitTarih}', '0', '{kullanici_kodu}'), ";
                    variables.Counter++;
                }
            }

            Query_2 = Query_2.Substring(0, Query_2.Length - 2);

            DataLayer dataLayer = new DataLayer();
            dataLayer.Update_Statement(Variables.Query_, Variables.Yil_, "Isemri Recetesi", variables.Counter);
            dataLayer.Insert_Statement(Query_2, Variables.Yil_, "Isemri", variables.Counter);
            Mouse.OverrideCursor = null;
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Isemri item in gv_bildirim.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;
                var a = checkBox.Parent;
                var b = checkBox.Height;
                var c = checkBox.Width;
                var d = checkBox.SnapsToDevicePixels;
                var g = checkBox.HorizontalAlignment;
                var f = checkBox.VerticalAlignment;
                var l = checkBox.ActualWidth; var r = checkBox.ActualHeight;
                var k = checkBox.GetHashCode();
                var p = checkBox.GetLocalValueEnumerator();
                var y = checkBox.GetType().Name;

                // Get the DataContext (Cls_Isemri) associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {

                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up

                }
            }

        }
    }
}

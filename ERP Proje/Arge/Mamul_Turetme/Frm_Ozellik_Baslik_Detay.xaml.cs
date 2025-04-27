using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Frm_Ozellik_Baslik_Detay.xaml
    /// </summary>
    public partial class Frm_Ozellik_Baslik_Detay : Window
    {
        Cls_Urun urun = new();
        private ObservableCollection<Cls_Urun> detayCollection = new();
        string secilenUrunOzellikTipi = string.Empty;
        string secilenUrunOzellikTipiKod = string.Empty;
        Variables variables = new Variables();
        public Frm_Ozellik_Baslik_Detay()
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                Mouse.OverrideCursor = Cursors.Wait;
                cbx_ozellik_secim.ItemsSource = urun.GetOzmas().Select(i => i.OzellikIsmi).ToList();
                cbx_ozellik_secim_kod.ItemsSource = urun.GetOzmas().Select(i => i.OzellikTipi).ToList();
                Mouse.OverrideCursor = null;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        public Frm_Ozellik_Baslik_Detay(Cls_Urun ozellikDetay,int selectedIndex)
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                Mouse.OverrideCursor = Cursors.Wait;
                cbx_ozellik_secim.ItemsSource = urun.GetOzmas().Select(i => i.OzellikIsmi).ToList();
                cbx_ozellik_secim_kod.ItemsSource = urun.GetOzmas().Select(i => i.OzellikTipi).ToList();
                cbx_ozellik_secim.SelectedIndex = selectedIndex;
                cbx_ozellik_secim_kod.SelectedIndex = selectedIndex;

                detayCollection = urun.PopulateOzellikDetayListe(ozellikDetay.OzellikTipi);

                if (detayCollection == null)
                { CRUDmessages.GeneralFailureMessage("Özellik Detayları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (detayCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("Özellik Detayları"); Mouse.OverrideCursor = null; return; }

                dg_ozdetay_liste.ItemsSource = detayCollection;
                dg_ozdetay_liste.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Variables.ErrorMessage_ = string.Empty;

                if (cbx_ozellik_secim.SelectedItem == null)
                { CRUDmessages.NoInput(); return; }

                dg_ozdetay_liste.ItemsSource = null;
                dg_ozdetay_liste.Items.Clear();
                

                if(string.IsNullOrEmpty(secilenUrunOzellikTipiKod))
                {
                    CRUDmessages.GeneralFailureMessage("Özellik Seçiniz");
                    Mouse.OverrideCursor=null;
                    return;
                }

                detayCollection = urun.PopulateOzellikDetayListe(secilenUrunOzellikTipiKod);

                if (detayCollection == null)
                { CRUDmessages.GeneralFailureMessage("Özellik Detayları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (detayCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("Özellik Detayları"); Mouse.OverrideCursor = null; return; }

                dg_ozdetay_liste.ItemsSource = detayCollection;
                dg_ozdetay_liste.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }

            catch { CRUDmessages.GeneralFailureMessage("Özellik Kayıtları Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private void btn_yeni_ekle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(secilenUrunOzellikTipi))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Tip Seçiniz"); return; }

                Mouse.OverrideCursor = Cursors.Wait;

                Frm_Ozellik_Baslik_Detay_Ekle popup = new Frm_Ozellik_Baslik_Detay_Ekle(secilenUrunOzellikTipi, secilenUrunOzellikTipiKod);
                popup.ShowDialog();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
            }
        }
        private void btn_guncelle(object sender, RoutedEventArgs e)
        {
            try
            {
                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Urun ozellikDetay = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if (ozellikDetay == null)
                {
                    CRUDmessages.GeneralFailureMessage("Özellik Detay Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (string.IsNullOrEmpty(ozellikDetay.OzellikTipi))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Kodu Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                //kod detay kontrol
                if (string.IsNullOrEmpty(ozellikDetay.Koddetay))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Detay Kodu Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.GetKodDetayUzunluk(ozellikDetay.OzellikTipi);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Kodu Uzunluğu Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                else if (ozellikDetay.Koddetay.Length != Variables.ResultInt_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Detay Kodu uzunluğu {0} Olmalı.", Variables.ResultInt_));
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.ResultInt_ = urun.CheckIfDetayKoduExistsElseWhere(ozellikDetay.OzellikTipi, ozellikDetay.Koddetay,ozellikDetay.KisitSira);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Kodu Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Detay Kodu Başka Özellik Detayına Tanımlı");
                    Mouse.OverrideCursor = null;
                    return;
                }

                //koddetay isim kontrol
                if (string.IsNullOrEmpty(ozellikDetay.KoddetayIsim))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Detay Kodu İsmi Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.CheckIfDetayKoduIsimExistsElseWhere(ozellikDetay.OzellikTipi, ozellikDetay.KoddetayIsim, ozellikDetay.KisitSira);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Kodu İsmi Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Detay Kodu İsmi Başka Özellik Detayına Tanımlı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                //koddetay ingilizce isim kontrol
                Variables.ResultInt_ = urun.CheckIfDetayKoduIsimIngExistsElseWhere(ozellikDetay.OzellikTipi, ozellikDetay.KoddetayIsimIng, ozellikDetay.KisitSira);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Kodu İngilizce İsmi Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Detay Kodu İngilizce İsmi Başka Özellik Detayına Tanımlı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                //koddetay kilit kontrol
                if ((ozellikDetay.Kilit == "H" ||
                    ozellikDetay.Kilit == "E") == false)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kilit 'H' veya 'E' Olmalıdır");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.Result_ = urun.UpdateOzDetay(ozellikDetay);
                if (!Variables.Result_)
                { CRUDmessages.UpdateFailureMessage("Ürün"); Mouse.OverrideCursor = null; return; }

                CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                Frm_Ozellik_Baslik_Detay frm = new(ozellikDetay, cbx_ozellik_secim.SelectedIndex);
                frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.UpdateFailureMessage("Ürün"); Mouse.OverrideCursor = null;
            }
        }
        private void btn_ozellik_sil(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(secilenUrunOzellikTipi))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Seçiniz"); return; }

                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Urun silinecekUrunTipi = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender as Button);
                if (silinecekUrunTipi == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Ürün Tipi Seçilirken");
                    return;
                }

                Variables.Result_ = urun.DeleteOzDetay(silinecekUrunTipi);
                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    return;
                }
                Mouse.OverrideCursor = null;
                CRUDmessages.DeleteSuccessMessage("Ürün", 1);
                Frm_Ozellik_Baslik_Detay frm = new();
                frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Tipi Silinirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_koda_gore_sirala_clicked(object sender, EventArgs e)
        {
            try
            {
                if (cbx_ozellik_secim.SelectedIndex == -1)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if(!Variables.Result_)
                    return;
                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Urun> siraliColl = urun.GetUpdatedSiraliOzellikDetay(cbx_ozellik_secim_kod.SelectedItem.ToString(), 1);
                if (siraliColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sıralama İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (siraliColl.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessage("Sıralama İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_ozdetay_liste.ItemsSource = siraliColl;
                dg_ozdetay_liste.Items.Refresh();
                dg_ozdetay_liste.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sırala İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_ada_gore_sirala_clicked(object sender, EventArgs e)
        {
            try
            {
                if (cbx_ozellik_secim.SelectedIndex == -1)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                    return;
                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Urun> siraliColl = urun.GetUpdatedSiraliOzellikDetay(cbx_ozellik_secim_kod.SelectedItem.ToString(), 2);
                if (siraliColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sıralama İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (siraliColl.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessage("Sıralama İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_ozdetay_liste.ItemsSource = siraliColl;
                dg_ozdetay_liste.Items.Refresh();
                dg_ozdetay_liste.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sırala İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sifirla_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;


                if (clickedButton.Parent is StackPanel stackPanel)
                {
                    foreach (UIElement element in stackPanel.Children)
                    {
                        if (element is ComboBox comboBox)
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        if (element is DatePicker datePicker)
                        {
                            datePicker.SelectedDate = null;
                        }
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
            }
        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void NewOzellikSelected(object sender, RoutedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null)
            {
                if(combo.SelectedIndex != -1)
                {
                    secilenUrunOzellikTipi = combo.SelectedItem.ToString();
                    int index = combo.SelectedIndex;
                    cbx_ozellik_secim_kod.SelectedIndex = index;
                    secilenUrunOzellikTipiKod = cbx_ozellik_secim_kod.SelectedItem.ToString();
                }
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
    }
}

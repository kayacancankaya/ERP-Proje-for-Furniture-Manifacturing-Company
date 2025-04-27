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
    /// Interaction logic for Frm_Ozellik_Baslik.xaml
    /// </summary>
    public partial class Frm_Ozellik_Baslik : Window
    {
        Cls_Urun urun = new();
        
        public Frm_Ozellik_Baslik()
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_Urun> masColl = urun.GetOzmas();
                if(masColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Özellik Başlıkları Listelenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_ozellik_baslik_liste.ItemsSource = urun.GetOzmas();
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Özellik Başlıkları Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sec_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Urun ozellikMas = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if(ozellikMas == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Özellik Bilgileri Listelenirken");
                    return;
                }
                ozellikMas = urun.GetOzMasInfo(ozellikMas.OzellikTipi);
                txt_kod.Text = ozellikMas.OzellikTipi;
                txt_isim.Text = ozellikMas.OzellikIsmi;
                txt_uzunluk.Text = ozellikMas.KodUzunluk.ToString();
                txt_kod1.Text = ozellikMas.Kod1;
                txt_kod2.Text = ozellikMas.Kod2;
                txt_kod3.Text = ozellikMas.Kod3;
                txt_kod4.Text = ozellikMas.Kod4;
                txt_kod5.Text = ozellikMas.Kod5;
                btn_kaydet.Visibility= Visibility.Visible;
                btn_guncelle.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Özellik Bilgileri Listelenirken");
            }
        }
        private void btn_sil_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if(!Variables.Result_)
                    return;
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Urun ozellikMas = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if (ozellikMas == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Özellik Bilgileri Alınırken");
                    return;
                }
                Variables.Result_ = urun.DeleteOzellikMas(ozellikMas.OzellikTipi);

                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    return;
                }

                CRUDmessages.DeleteSuccessMessage("Ürün", 1);
                Frm_Ozellik_Baslik frm = new();
                frm.Show();
                Mouse.OverrideCursor = null;
                this.Close();
            }
            catch 
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
            }
        }
        private void btn_kaydet_clicked(object sender, EventArgs e)
        {
            try
            {
                Variables.Result_ = CRUDmessages.InsertOnayMessage();
                if (!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;
                if(string.IsNullOrEmpty(txt_kod.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kod Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (txt_kod.Text.Length != 4)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kod 4 karakterden oluşmalı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.ResultInt_ = urun.CheckIfOzMasExists(txt_kod.Text);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Kod Bilgileri Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                    
                }
                else if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aynı Kodda Kayıt Mevcut");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.CheckIfOzMasIsimExists(txt_isim.Text);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Kod İsim Bilgileri Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;

                }
                else if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aynı İsimde Kayıt Mevcut");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (string.IsNullOrEmpty(txt_isim.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("İsim Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (string.IsNullOrEmpty(txt_uzunluk.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Uzunluk Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Cls_Urun yeniKayit = new();
                yeniKayit.OzellikTipi = txt_kod.Text;
                yeniKayit.OzellikIsmi = txt_isim.Text;
                yeniKayit.KodUzunluk = Convert.ToInt32(txt_uzunluk.Text);
                yeniKayit.Kod1 = string.IsNullOrEmpty(txt_kod1.Text) ? "" : txt_kod1.Text;
                yeniKayit.Kod2 = string.IsNullOrEmpty(txt_kod2.Text) ? "" : txt_kod2.Text;
                yeniKayit.Kod3 = string.IsNullOrEmpty(txt_kod3.Text) ? "" : txt_kod3.Text;
                yeniKayit.Kod4 = string.IsNullOrEmpty(txt_kod4.Text) ? "" : txt_kod4.Text;
                yeniKayit.Kod5 = string.IsNullOrEmpty(txt_kod5.Text) ? "" : txt_kod5.Text;

                Variables.Result_ = urun.InsertOzMas(yeniKayit);
                if(!Variables.Result_)
                {
                    CRUDmessages.InsertFailureMessage("Ürün");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.InsertSuccessMessage("Ürün",1);
                Frm_Ozellik_Baslik frm = new();
                frm.Show();
                this.Close();
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.InsertFailureMessage("Ürün");
                Mouse.OverrideCursor = null ;
                return;
            }
        }
        private void btn_guncelle_clicked(object sender, EventArgs e)
        {
            try
            {
                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if(!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txt_kod.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kod Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (txt_kod.Text.Length != 4)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kod 4 karakterden oluşmalı");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.CheckIfOzMasExists(txt_kod.Text);
                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Kod Bilgileri Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;

                }
                else if (Variables.ResultInt_ > 1)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aynı Kodda Birden Fazla Kayıt Mevcut");
                    Mouse.OverrideCursor = null;
                    return;
                }
                else if (Variables.ResultInt_ == 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Koda Ait Kayıt Bulunamadı");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (string.IsNullOrEmpty(txt_isim.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("İsim Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.CheckIfOzMasIsimExistsElseWhere(txt_kod.Text,txt_isim.Text);

                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Kod İsim Bilgileri Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;

                }
                else if (Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aynı İsimde Başka Kayıt Mevcut");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (string.IsNullOrEmpty(txt_uzunluk.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Uzunluk Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Cls_Urun guncellenecekKayit = new();
                guncellenecekKayit.OzellikTipi = txt_kod.Text;
                guncellenecekKayit.OzellikIsmi = txt_isim.Text;
                guncellenecekKayit.KodUzunluk = Convert.ToInt32(txt_uzunluk.Text);
                guncellenecekKayit.Kod1 = string.IsNullOrEmpty(txt_kod1.Text) ? "" : txt_kod1.Text;
                guncellenecekKayit.Kod2 = string.IsNullOrEmpty(txt_kod2.Text) ? "" : txt_kod2.Text;
                guncellenecekKayit.Kod3 = string.IsNullOrEmpty(txt_kod3.Text) ? "" : txt_kod3.Text;
                guncellenecekKayit.Kod4 = string.IsNullOrEmpty(txt_kod4.Text) ? "" : txt_kod4.Text;
                guncellenecekKayit.Kod5 = string.IsNullOrEmpty(txt_kod5.Text) ? "" : txt_kod5.Text;

                Variables.Result_ = urun.UpdateOzMas(guncellenecekKayit);
                if (!Variables.Result_)
                {
                    CRUDmessages.UpdateFailureMessage("Ürün");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.UpdateSuccessMessage("Ürün",1);

                Frm_Ozellik_Baslik frm = new();
                frm.Show();
                this.Close();
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.UpdateFailureMessage("Ürün");
                Mouse.OverrideCursor = null;
                return;
            }
        }

        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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

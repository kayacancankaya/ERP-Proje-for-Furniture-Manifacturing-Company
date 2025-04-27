using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Frm_Ozellik_Baslik_Detay_Ekle.xaml
    /// </summary>
    public partial class Frm_Ozellik_Baslik_Detay_Ekle : Window
    {
        Cls_Urun urun = new();
        Variables variables = new();
        public string Maskod { get; set; }
        public string OzellikIsim { get; set; }
        public Frm_Ozellik_Baslik_Detay_Ekle(string ozisim,string maskod)
        {
            try
            {

                InitializeComponent();
                Window_Loaded();
                Mouse.OverrideCursor = Cursors.Wait;
                Maskod = maskod;
                OzellikIsim = ozisim;
                if (string.IsNullOrEmpty(maskod))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Kodu Bulunamadı."); Mouse.OverrideCursor = null; this.Close(); }

                cbx_kilit.SelectedIndex = 0;

                this.Title = string.Format("Maskod:{0}",maskod);

                txt_sira.Text = urun.GetNextOzdetaySira(maskod).ToString();

                if(txt_sira.Text == "-1")
                { CRUDmessages.GeneralFailureMessageCustomMessage("Sıra Numarası Alınamadı."); Mouse.OverrideCursor = null; this.Close(); }

                Mouse.OverrideCursor = null;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Kodu Bulunamadı."); Mouse.OverrideCursor = null; this.Close();
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
        private void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_kod.Text) ||
                    string.IsNullOrEmpty(txt_isim.Text))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Kod ve İsim Boş Olamaz"); return; }


                Mouse.OverrideCursor = Cursors.Wait;

                ComboBoxItem selectedItem = new ComboBoxItem();
                selectedItem = cbx_kilit.SelectedItem as ComboBoxItem;
                
                int uzunluk = urun.GetKodDetayUzunluk(Maskod);

                if (txt_kod.Text.Length != uzunluk)
                { CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Kod Uzunluğu {0} olmalı", uzunluk)); return; }

                Variables.ResultInt_ = urun.CheckIfDetayKoduExistsElseWhere(Maskod, txt_kod.Text, Convert.ToInt32(txt_sira.Text));
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

                Variables.ResultInt_ = urun.CheckIfDetayKoduIsimExistsElseWhere(Maskod, txt_isim.Text, Convert.ToInt32(txt_sira.Text));
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
                Variables.ResultInt_ = urun.CheckIfDetayKoduIsimIngExistsElseWhere(Maskod, txt_ing_isim.Text, Convert.ToInt32(txt_sira.Text));
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
                if ((selectedItem.Content.ToString() == "H" ||
                    selectedItem.Content.ToString() == "E") == false)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kilit 'H' veya 'E' Olmalıdır");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if(string.IsNullOrEmpty(txt_sira.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sıra Numarası Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;

                }

                Cls_Urun urunDetay = new Cls_Urun
                {
                    OzellikTipi = Maskod,
                    KoddetayIsim = txt_isim.Text,
                    KoddetayIsimIng = txt_ing_isim.Text,
                    Koddetay = txt_kod.Text,
                    KoddetaySira = string.IsNullOrEmpty(txt_sira.Text) ? -1 : Convert.ToInt32(txt_sira.Text),
                    Kilit = selectedItem.Content.ToString(),
                    Kod1 = txt_kod1.Text,
                    Kod2 = txt_kod2.Text,
                    Kod3 = txt_kod3.Text,
                    Kod4 = txt_kod4.Text,
                    Kod5 = txt_kod5.Text,
                    
                };
                Variables.Result_ = urun.InsertOzellikBaslikDetay(urunDetay);
                
                
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Özellik Kaydedilirken"); Mouse.OverrideCursor = null; return;
                }

                CRUDmessages.InsertSuccessMessage("Ürün", 1);
                Mouse.OverrideCursor = null;
                Frm_Ozellik_Baslik_Detay_Ekle frm_ = new Frm_Ozellik_Baslik_Detay_Ekle(OzellikIsim,Maskod);
                frm_.Show();
                this.Close();
            }

            catch
            {
                CRUDmessages.GeneralFailureMessage("Özellik Kaydedilirken"); Mouse.OverrideCursor = null;
            }
        }
    }
}

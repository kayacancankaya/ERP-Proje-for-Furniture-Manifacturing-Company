using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Yardimci
{

    public partial class Frm_Varyant_Ad_Guncelle : Window
    {
        Cls_Urun cls_urun = new();
        Variables variables = new Variables();


        public Frm_Varyant_Ad_Guncelle()
        {
            InitializeComponent(); Window_Loaded();

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
        private void btn_urun_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                variables.ErrorMessage = string.Empty;

                if (cls_urun.UrunCollection != null)
                    cls_urun.UrunCollection.Clear();

                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrWhiteSpace(txt_urun_tipi.Text) && string.IsNullOrWhiteSpace(txt_model.Text) && string.IsNullOrWhiteSpace(txt_satis_sekil.Text) &&
                    string.IsNullOrEmpty(txt_urun_adi.Text) && string.IsNullOrEmpty(txt_urun_kodu.Text))
                {
                    CRUDmessages.NoInput(); Mouse.OverrideCursor = null; return;
                }


                Dictionary<string, string> constraints = new Dictionary<string, string>();

                constraints.Add("urunKod", txt_urun_kodu.Text);
                constraints.Add("urunAdi", txt_urun_adi.Text);
                constraints.Add("urunTipi", txt_urun_tipi.Text);
                constraints.Add("model", txt_model.Text);
                constraints.Add("satisSekil", txt_satis_sekil.Text);

                cls_urun.UrunCollection = cls_urun.PopulateUrunAdiGuncellenecekListele(constraints);

                if (cls_urun.UrunCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null;
                    return;
                }
                if (cls_urun.UrunCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null;
                    return;
                }

                dg_UrunSecim.ItemsSource = cls_urun.UrunCollection;
                dg_UrunSecim.Visibility = Visibility.Visible;
                stack_panel_urun_guncelle.Visibility = Visibility.Visible;



                Mouse.OverrideCursor = null;
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Ürünler Listelenirken");
            }
        }
        private async void btn_urun_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_Urun> urunColl = new();
                ObservableCollection<Cls_Urun> sablonColl = new();
                variables.ErrorMessage = string.Empty;


                Variables.Counter_ = 0;

                foreach (Cls_Urun urun in dg_UrunSecim.Items)
                {
                    if (urun.IsChecked)
                    {
                        if (!sablonColl.Where(s => s.UrunKodu.Substring(0, 11) == urun.UrunKodu.Substring(0, 11)).Any())
                        {
                            sablonColl.Add(urun);
                        }
                        urunColl.Add(urun);
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                bool digerVaryantlariDaBildir_ = false;
                txt_urun_listele.Visibility = Visibility.Visible;
                if (cb_tum_turemisleri_guncelle.IsChecked == true)
                {
                    digerVaryantlariDaBildir_ = true;
                    Variables.ResultInt_ = await cls_urun.UpdateUrunAdi(sablonColl, digerVaryantlariDaBildir_);

                }
                else
                    Variables.ResultInt_ = await cls_urun.UpdateUrunAdi(sablonColl, digerVaryantlariDaBildir_);


                txt_urun_listele.Visibility = Visibility.Collapsed;


                if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken");
                    return;
                }
                if (Variables.ResultInt_ == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }

                CRUDmessages.UpdateSuccessMessage("Stok Kartı");

                Frm_Varyant_Ad_Guncelle frm = new();
                frm.Show();
                this.Close();


            }

            catch { CRUDmessages.GeneralFailureMessage("Ürün Bilgileri Listelenirken"); Mouse.OverrideCursor = null; }
        }

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Urun item in dg_UrunSecim.Items)
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
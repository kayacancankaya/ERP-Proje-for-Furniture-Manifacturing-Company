using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Popup_Varyant_Ozellik_Ekle.xaml
    /// </summary>
    public partial class Popup_Varyant_Ozellik_Ekle : Window
    {
        Cls_Urun urun = new();
        Cls_Urun rowItem = new();
        public OpsiyonKaydetViewModel model { get; set; }
        Cls_Urun EklenecekUrun = new();
        public int OzellikSira { get; set; }
        public bool IsNew { get; set; } = false;
        public Popup_Varyant_Ozellik_Ekle(Cls_Urun eklenecekUrun, ObservableCollection<Cls_Urun> opsiyonColl, bool isNew)
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                EklenecekUrun = eklenecekUrun;
                txt_title.Text = string.Format("{0}-{1}-{2}", eklenecekUrun.UrunGrubuIsim, eklenecekUrun.ModelIsim, eklenecekUrun.SatisSekilIsim);
                this.Name = "Popup_Varyant_Ekle";
                IsNew = isNew;

                model = new(eklenecekUrun, opsiyonColl);
                txt_isim_anahtar.Text = EklenecekUrun.IsimAnahtar;
                txt_ingilizce_isim_anahtar.Text = EklenecekUrun.IngilizceIsimAnahtar;
                dg_OpsiyonListe.ItemsSource = model.UrunColl;
                dg_OpsiyonListe.DataContext = model;
                cbx_opsiyon_secim.ItemsSource = model.UrunColl.OrderBy(s=>s.BaslangicSira).Select(v => v.VaryantIsmi).ToList();
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                Mouse.OverrideCursor = null;
                this.DialogResult = true;
                this.Close();
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
        private void cbx_ozellik_isim_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(comboBox);
                if (row is null)
                {
                    dg_Opsiyon_Guncelle.ItemsSource = null;
                    return;
                 };
                Cls_Urun? dataItem = row.Item as Cls_Urun;
                if (dataItem is null)
                {
                    dg_Opsiyon_Guncelle.ItemsSource = null;
                    return;
                }
                string ozellikKodu = urun.GetOzellikKoduFromOzellikIsmi(comboBox.SelectedItem.ToString());
                dataItem.OzellikTipi = ozellikKodu;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Özellik İsmi Değişirken");
            }
        }
        private void btn_anahtar_guncelle_clicked(object sender,RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (txt_isim_anahtar.Text == null)
                    txt_isim_anahtar.Text = string.Empty;
                if (txt_ingilizce_isim_anahtar.Text == null)
                    txt_ingilizce_isim_anahtar.Text = string.Empty;

                Cls_Urun item = new();
                item.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                item.ModelKodu = EklenecekUrun.ModelKodu;
                item.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;
                item.IsimAnahtar = txt_isim_anahtar.Text;
                item.IngilizceIsimAnahtar = txt_ingilizce_isim_anahtar.Text;

                Variables.Result_ = urun.UpdateIsimAnahtar(item);
                if(!Variables.Result_)
                {
                    CRUDmessages.UpdateFailureMessage("Ürün");
                    Mouse.OverrideCursor = null;
                    return ;
                }
                CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_add_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Urun optionToAdd = new();
                optionToAdd.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                optionToAdd.ModelKodu = EklenecekUrun.ModelKodu;
                optionToAdd.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;
                optionToAdd.OzellikSayisi = model.UrunColl.Max<Cls_Urun>(s => s.OzellikSayisi) + 1;
                optionToAdd.OzellikTipi = "-";
                optionToAdd.OzellikIsmi = "<-Seçim Yapınız->";
                optionToAdd.ReceteDegeri = string.Format("@o{0}", optionToAdd.OzellikSayisi);
                //Variables.Result_ = urun.UpdateOpsiyonAdediBy1(optionToAdd, true);
                //if (!Variables.Result_)
                //{
                //    CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
                //    Mouse.OverrideCursor = null;
                //    return;
                //}

                model.UrunColl.Add(optionToAdd);

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void btn_remove_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Urun item = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if (item == null)
                {
                    CRUDmessages.GeneralFailureMessage("Opsiyon Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                item.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                item.ModelKodu = EklenecekUrun.ModelKodu;
                item.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;

                    model.UrunColl.Remove(item);
                    Variables.Counter_ = 1;
                    foreach (Cls_Urun dgItem in model.UrunColl.OrderBy(s => s.OzellikSayisi))
                    {
                        if (Variables.Counter_ != dgItem.OzellikSayisi)
                        {
                            dgItem.OzellikSayisi = Variables.Counter_;
                            dgItem.ReceteDegeri = string.Format("@o{0}", Variables.Counter_);
                        }
                        Variables.Counter_++;
                    }

                    dg_OpsiyonListe.Items.Refresh();
                    Mouse.OverrideCursor = null;
                return;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_delete_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Urun item = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);
                if (item == null)
                {
                    CRUDmessages.GeneralFailureMessage("Opsiyon Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_)
                    return;

                item.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                item.ModelKodu = EklenecekUrun.ModelKodu;
                item.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;

                Variables.ResultInt_ = urun.CheckIfOptionExists(item);
                if (Variables.ResultInt_ == 0)
                {
                    model.UrunColl.Remove(item);
                    Variables.Counter_ = 1;
                    foreach (Cls_Urun dgItem in model.UrunColl.OrderBy(s => s.OzellikSayisi))
                    {
                        if (Variables.Counter_ != dgItem.OzellikSayisi)
                        {
                            dgItem.OzellikSayisi = Variables.Counter_;
                            dgItem.ReceteDegeri = string.Format("@o{0}", Variables.Counter_);
                        }
                        Variables.Counter_++;
                    }
                    CRUDmessages.DeleteSuccessMessage("Ürün", 1);
                    dg_OpsiyonListe.Items.Refresh();

                    Mouse.OverrideCursor = null;
                    return;
                }
                else if (Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.Result_ = urun.DeleteOpsiyon(item, model.UrunColl);
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }


                CRUDmessages.DeleteSuccessMessage("Ürün", 1);

                ObservableCollection<Cls_Urun> opsiyonColl = urun.GetOpsiyonlar(item);

                Variables.Counter_ = 1;
                foreach (Cls_Urun collItem in opsiyonColl.OrderBy(s => s.OzellikSayisi))
                {
                    if (Variables.Counter_ != collItem.OzellikSayisi)
                    {
                        Variables.Result_ = urun.UpdateOpsiyonSiraRecDegRearranged(item, collItem.OzellikSayisi);
                        if (!Variables.Result_)
                            CRUDmessages.GeneralFailureMessage(string.Format("Sıra {0}: Yeniden Sıra Verilirken", collItem.OzellikSayisi));
                    }
                    Variables.Counter_++;
                }

                Variables.Result_ = urun.UpdateOpsiyonAdediBy1(item, false);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Name == "Popup_Varyant_Ekle")
                    {
                        window.Close();
                    }
                }
                EklenecekUrun.OzellikSayisi = opsiyonColl.Count;
                Popup_Varyant_Ozellik_Ekle frm = new(EklenecekUrun, opsiyonColl, IsNew);
                frm.Show();

                dg_OpsiyonListe.ItemsSource = model.UrunColl;
                dg_OpsiyonListe.DataContext = model;
                cbx_opsiyon_secim.ItemsSource = model.UrunColl.Select(v => v.VaryantIsmi).ToList();

                Mouse.OverrideCursor = null;
                return;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        //private bool cikisKontrol()
        //{
        //    Variables.Counter_ = 1;
        //    bool anyError = false;
        //    foreach (Cls_Urun item in model.UrunColl.OrderBy(s => s.OzellikSayisi))
        //    {
        //        if (item.OzellikSayisi != 1 && item.OzellikTipi == "1000")
        //        {
        //            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Model Kodu ilk sırada olmalı.Sıra:{0}", Variables.Counter_));
        //            anyError = true;
        //        }
        //        if (item.ReceteDegeri.Length < 3)
        //        {
        //            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Reçete Değeri 3 Karakterden Küçük Olamaz. Sıra:{0}", Variables.Counter_));
        //            anyError = true;
        //            break;
        //        }
        //        if (item.ReceteDegeri.Substring(0, 2) != "@o")
        //        {
        //            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Reçete Değeri @o ile Başlamalı.Sıra:{0}", Variables.Counter_));
        //            anyError = true;
        //        }
        //        if (item.ReceteDegeri != string.Format("@o{0}", Variables.Counter_))
        //        {
        //            Variables.Result_ = CRUDmessages.DoYouWishToContinue(string.Format("Reçete Değeri @o{0} Değil Devam Etmek İstiyor Musunuz?.Sıra:{1}", Variables.Counter_, Variables.Counter_));
        //            if (!Variables.Result_)
        //                return false;
        //        }
        //        if (item.OzellikIsmi == "<-Seçim Yapınız->")
        //        {
        //            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Sıra:{0}, güncelleme yapmadan çıkış yapılamaz. Seçim Yapınız ya da Siliniz...", Variables.Counter_));
        //            anyError = true;
        //        }
        //        Variables.Counter_++;
        //    }
        //    if (anyError)
        //        return false;
        //    else
        //        return true;

        //}
        private void btn_opsiyon_guncelle_clicked(object sender,RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Counter_ = 1;

                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                    return;

                foreach (Cls_Urun item in dg_OpsiyonListe.Items)
                {
                    if (item.OzellikIsmi == "<-Seçim Yapınız->")
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Sıra {0} Özellik İsmini güncelleyiniz.", item.OzellikSayisi));
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if(item.ReceteDegeri.Substring(0,2) != "@o")
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("Sıra {0} Reçete Değeri @o ile başlamalı.", item.OzellikSayisi));
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    item.OzellikSayisi = Variables.Counter_;
                    item.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                    item.ModelKodu = EklenecekUrun.ModelKodu;
                    item.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;
                    Variables.ResultInt_ = urun.CheckIfOptionExists(item);
                    if (Variables.ResultInt_ == 0)
                    {
                        Variables.Result_ = urun.InsertOption(item);
                        if (!Variables.Result_)
                        {
                            CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                            Mouse.OverrideCursor = null;
                            return;
                        }

                    }
                    else if (Variables.ResultInt_ == -1)
                    {
                        CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else if (Variables.ResultInt_ > 1)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Aynı Sırada Birden Fazla Kayıt Tespit Edildi. Bilgi İşlem Personeline Haber Veriniz.");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    else
                    {

                        Variables.Result_ = urun.UpdateOpsiyon(item);

                        if (!Variables.Result_)
                        {
                            CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                            Mouse.OverrideCursor = null;
                            return;
                        }

                        Variables.Result_ = urun.UpdateBaslangicUzunluk(item);

                        if (!Variables.Result_)
                        {
                            CRUDmessages.UpdateFailureMessage("Ürün");
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }
                    Variables.Counter_++;
                }
                EklenecekUrun.OzellikSayisi = dg_OpsiyonListe.Items.Count;
                Variables.Result_ = urun.UpdateOpsiyonAdedi(EklenecekUrun);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Opsiyon Adedi Güncellenirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.InsertSuccessMessage("Ürün", 1);
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
            }
        }
        private void cbx_opsiyon_secim_selection_changed(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ComboBox comboBox = sender as ComboBox;
                if (comboBox is null)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }
                int selectedIndex = comboBox.SelectedIndex;
                if (selectedIndex < 0)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }

				Cls_Urun secilenOpsiyon = dg_OpsiyonListe.Items[selectedIndex] as Cls_Urun;


                OzellikSira = secilenOpsiyon.OzellikSayisi;
                if (secilenOpsiyon is null)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }
                model.KisitColl = urun.GetOzellikInfoFromOzellikKodu(secilenOpsiyon.OzellikTipi);


                ObservableCollection<Cls_Urun> existingRestrictions = urun.GetKisits(EklenecekUrun.UrunGrubuKodu, EklenecekUrun.ModelKodu, EklenecekUrun.SatisSekilKodu, OzellikSira);

                if (existingRestrictions != null)
                {
                    if (existingRestrictions.Count > 0)
                    {
                        foreach (Cls_Urun item in existingRestrictions)
                        {
                            Cls_Urun toBeChecked = model.KisitColl.FirstOrDefault(i => i.Koddetay == item.KisitKod);
                            if (toBeChecked != null)
                            {
                                toBeChecked.IsChecked = true;
                            }
                        }
                    }
                }

                dg_Opsiyon_Guncelle.ItemsSource = model.KisitColl;
                dg_Opsiyon_Guncelle.DataContext = model;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Özellik Seçimi Değişirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_update_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
               

                ObservableCollection<Cls_Urun> kisitCollection = new();

                foreach (Cls_Urun item in dg_Opsiyon_Guncelle.Items)
                {
                    if (item.IsChecked == true)
                    {
                        item.UrunGrubuKodu = EklenecekUrun.UrunGrubuKodu;
                        item.ModelKodu = EklenecekUrun.ModelKodu;
                        item.SatisSekilKodu = EklenecekUrun.SatisSekilKodu;
                        item.OpsiyonAciklama = cbx_opsiyon_secim.SelectedItem.ToString();
                        item.OzellikSayisi = OzellikSira;
                        kisitCollection.Add(item);
                    }
                }
                if (EklenecekUrun == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (OzellikSira == 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Sırası Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                    return;

                Variables.Result_ = urun.UpdateUrunOzellikKisit(kisitCollection, EklenecekUrun.UrunGrubuKodu, EklenecekUrun.ModelKodu, EklenecekUrun.SatisSekilKodu, OzellikSira);
                if (Variables.Result_)
                {
                    CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                    Mouse.OverrideCursor = null;

                    ObservableCollection<Cls_Urun> opsiyonColl = urun.GetOpsiyonlar(EklenecekUrun);

                    Popup_Varyant_Ozellik_Ekle frm = new(EklenecekUrun, opsiyonColl, IsNew);
                    frm.Show();

                    this.Close();

                    dg_OpsiyonListe.ItemsSource = model.UrunColl;
                    dg_OpsiyonListe.DataContext = model;
                    cbx_opsiyon_secim.ItemsSource = model.UrunColl.Select(v => v.VaryantIsmi).ToList();
                }
                else
                {
                    CRUDmessages.UpdateFailureMessage("Ürün");
                    Mouse.OverrideCursor = null;
                    return;
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
            }
        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Urun item in dg_Opsiyon_Guncelle.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private void btn_sifirla_clicked_ops(object sender, RoutedEventArgs e)
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
                dg_Opsiyon_Guncelle.ItemsSource = null;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Liste Sıfırlanırken");
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
        private void btn_exit_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                //if (!cikisKontrol())
                //{
                //    Mouse.OverrideCursor = null;
                //    return;
                //}
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Name == "Popup_Varyant_Ekle")
                    {
                        window.Close();
                    }
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessage("Form Çıkışı Yapılırken: " + ex.Message);
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_minimize_click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }
        private void btn_close_click(object sender, RoutedEventArgs e)
        {
            //if (!cikisKontrol())
            //{
            //    Mouse.OverrideCursor = null;
            //    return;
            //}
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "Popup_Varyant_Ekle")
                {
                    window.Close();
                }
            }
            this.Close();
        }
        private void btn_level_down_click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen();
        }
        public bool isFullScreen { get; set; } = true;
        private Rect originalWindowRect;
        private void ToggleFullScreen()
        {
            Window parentWindow = Window.GetWindow(this);
            var workArea = SystemParameters.WorkArea;
            if (parentWindow != null && isFullScreen == false)
            {

                parentWindow.Left = workArea.Left;
                parentWindow.Top = workArea.Top;
                parentWindow.Width = workArea.Width;
                parentWindow.Height = workArea.Height;
                parentWindow.Topmost = true;
                parentWindow.Topmost = false;
                isFullScreen = true;
                return;
            }
            if (parentWindow != null && isFullScreen == true)
            {
                originalWindowRect = new Rect(parentWindow.Left, parentWindow.Top, parentWindow.Width, parentWindow.Height);

                // Set the window to fullscreen
                double newWidth = parentWindow.MinWidth == 0 ? 500 : parentWindow.MinWidth;
                double newHeight = parentWindow.MinHeight == 0 ? 500 : parentWindow.MinHeight;

                parentWindow.Width = newWidth;
                parentWindow.Height = newHeight;
                parentWindow.Left = workArea.Left + (workArea.Width - newWidth) / 2;
                parentWindow.Top = workArea.Top + (workArea.Height - newHeight) / 2;
                parentWindow.Topmost = false;
                isFullScreen = false;
                return;
            }

        }

		private void dg_OpsiyonListe_ColumnReordered(object sender, DataGridColumnEventArgs e)
		{
            try
            {
                if (dg_OpsiyonListe.ItemsSource == null)
                    return;
                
                if (cbx_opsiyon_secim.ItemsSource != null)
                    cbx_opsiyon_secim.ItemsSource = null;

                ObservableCollection<string> comboColl = new();
				ICollectionView view = CollectionViewSource.GetDefaultView(dg_OpsiyonListe.ItemsSource);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (Cls_Urun item in view)
                        comboColl.Add(item.VaryantIsmi);
                }));
                cbx_opsiyon_secim.ItemsSource = comboColl;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
		}
	}
}

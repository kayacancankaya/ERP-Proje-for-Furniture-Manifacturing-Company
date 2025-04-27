using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Recete
{
    /// <summary>
    /// Interaction logic for Frm_Urun_Agaci_Guncelle.xaml
    /// </summary>
    public partial class Frm_Urun_Agaci_Guncelle : Window
    {
        Variables variables = new();
        Cls_Arge arge = new();
        ObservableCollection<Cls_Arge> receteCollection = new();
        ObservableCollection<Cls_Arge> guncellemeCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        ObservableCollection<Cls_Arge> mamulDetayCollection = new();
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
        public Frm_Urun_Agaci_Guncelle()
        {
            InitializeComponent(); Window_Loaded();

        }
        private void cbx_bildirim_tipi_selection_changed(object sender, EventArgs e)
        {
            try
            {
                stc_constraints.Visibility = Visibility.Visible;
                btn_listele.Visibility = Visibility.Visible;
                stc_recete_ham_degistir.Visibility = Visibility.Collapsed;
                stc_recete_miktar_degistir.Visibility = Visibility.Collapsed;
                stc_recete_ozellik_ekle.Visibility = Visibility.Collapsed;
                if (dg_ReceteSecim.Visibility == Visibility.Collapsed)
                    stc_recete_guncelle.Visibility = Visibility.Collapsed;


                ComboBox combo = new ComboBox();
                combo = sender as ComboBox;

                ComboBoxItem cItem = new();
                cItem = combo.SelectedItem as ComboBoxItem;

                if (cItem == null)
                {
                    btn_listele.Visibility = Visibility.Collapsed;
                    return;
                }

                if (cItem.Tag != null)
                    ChangeVisibility(cItem.Tag.ToString());
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Bildirim Tipi Seçilirken");
            }
        }
        private void ChangeVisibility(string comboBoxItemTagName)
        {
            switch (comboBoxItemTagName)
            {
                case "hamkodu":
                    stc_recete_ham_degistir.Visibility = Visibility.Visible;
                    dg_MamulDetayListe.Visibility = Visibility.Collapsed;
                    break;
                case "miktar":
                    stc_recete_miktar_degistir.Visibility = Visibility.Visible;
                    dg_MamulDetayListe.Visibility = Visibility.Collapsed;
                    break;
                case "hammiktar":
                    stc_recete_miktar_degistir.Visibility = Visibility.Visible;
                    stc_recete_ham_degistir.Visibility = Visibility.Visible;
                    dg_MamulDetayListe.Visibility = Visibility.Collapsed;
                    break;
                case "ekle":
                    stc_constraints.Visibility = Visibility.Collapsed;
                    stc_recete_ozellik_degistir.Visibility = Visibility.Collapsed;
                    stc_recete_ozellik_ekle.Visibility = Visibility.Visible;
                    stc_recete_guncelle.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void txt_eklenecek_mamul_kodu_changed(object sender, EventArgs e)
        {
            try
            {
                Variables.Result_ = arge.IfStokKoduExists(txt_eklenecek_mamul_kodu.Text);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Kodu Sistemde Bulunamadı.");
                    return;
                }

                string opno = arge.GetLastOpNo(txt_eklenecek_mamul_kodu.Text);

                txt_opno.Text = opno;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Opno Alınırken");
            }
        }
        private void btn_mamul_detay_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_ReceteSecim.Visibility = Visibility.Collapsed;

                if (string.IsNullOrWhiteSpace(txt_eklenecek_mamul_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (txt_eklenecek_mamul_kodu.Text.Length < 11)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Kodu 11 Karakterden Az Olamaz.");
                    return;
                }
                if (txt_eklenecek_mamul_kodu.Text.Length > 35)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Kodu 35 Karakterden Büyük Olamaz.");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                mamulDetayCollection = arge.PopulateMamulReceteList(txt_eklenecek_mamul_kodu.Text);

                if (mamulDetayCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Mamulün Reçetesi Listelenirken"); Mouse.OverrideCursor = null; return;
                }

                if (!mamulDetayCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null;
                }

                dg_MamulDetayListe.ItemsSource = mamulDetayCollection;
                dg_MamulDetayListe.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Yapılırken");
            }
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_MamulDetayListe.Visibility = Visibility.Collapsed;
                stc_recete_guncelle.Visibility = Visibility.Visible;
                if (restrictionPairs != null) restrictionPairs.Clear();
                if (receteCollection != null) receteCollection.Clear();

                if (!string.IsNullOrWhiteSpace(txt_mamul_kodu.Text))
                {
                    if (txt_mamul_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@mamulKodu", txt_mamul_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_mamul_adi.Text))
                {
                    if (txt_mamul_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@mamulAdi", txt_mamul_adi.Text);
                }



                if (!string.IsNullOrWhiteSpace(txt_ham_kodu.Text) && txt_ham_kodu.Visibility == Visibility.Visible)
                {

                    if (txt_ham_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@hamKodu", txt_ham_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_ham_adi.Text) && txt_ham_adi.Visibility == Visibility.Visible)
                {
                    if (txt_ham_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@hamAdi", txt_ham_adi.Text);
                }
                if (!string.IsNullOrWhiteSpace(txt_urun_kodu.Text) && txt_urun_kodu.Visibility == Visibility.Visible)
                {

                    if (txt_urun_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@urunKodu", txt_urun_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_urun_adi.Text) && txt_urun_adi.Visibility == Visibility.Visible)
                {
                    if (txt_urun_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@urunAdi", txt_urun_adi.Text);
                }

                if (restrictionPairs.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }



                Mouse.OverrideCursor = Cursors.Wait;

                receteCollection = arge.PopulateReceteGuncelleList(restrictionPairs);

                if (receteCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Reçete Güncelleme Listesi Oluşturulurken"); Mouse.OverrideCursor = null; return;
                }

                if (!receteCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return;
                }

                dg_ReceteSecim.ItemsSource = receteCollection;
                dg_ReceteSecim.Visibility = Visibility.Visible;
                stc_recete_ozellik_degistir.Visibility = Visibility.Visible;
                stc_recete_guncelle.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Yapılırken");
            }
        }
        private async void btn_recete_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                ComboBoxItem comboBoxItem = cbx_guncelleme_tipi.SelectedItem as ComboBoxItem;

                string comboBoxItemTag = string.Empty;

                comboBoxItemTag = comboBoxItem.Tag as string;

                if (comboBoxItem == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Güncelleme Tipi Boş Olamaz");
                    return;
                }

                if (comboBoxItemTag == "hamkodu" ||
                    comboBoxItemTag == "hammiktar")
                {
                    if (string.IsNullOrWhiteSpace(txt_yeni_ham_kodu.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Kodu Boş Olamaz");
                        return;
                    }
                    if (txt_yeni_ham_kodu.Text.Length < 11)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Kodu 11 Karakterden Küçük Olamaz");
                        return;
                    }
                    if (txt_yeni_ham_kodu.Text.Length > 35)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Kodu 35 Karakterden Büyük Olamaz");
                        return;
                    }

                }
                if (comboBoxItemTag == "miktar" ||
                    comboBoxItemTag == "hammiktar")
                {
                    if (string.IsNullOrEmpty(txt_miktar.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Miktar Boş Olamaz");
                        return;
                    }
                    if (Convert.ToDecimal(txt_miktar.Text) == 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Miktar 0 Olamaz");
                        return;
                    }

                }


                if (comboBoxItemTag == "ekle")
                {
                    if (string.IsNullOrWhiteSpace(txt_eklenecek_ham_kodu.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ham Kodu Boş Olamaz");
                        return;
                    }
                    Variables.Result_ = arge.IfStokKoduExists(txt_eklenecek_ham_kodu.Text);
                    if (!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessage("Eklenecek Ham Kodunun Stok Kartı Sistemde Bulunmamaktadır.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txt_eklenecek_mamul_kodu.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Mamul Kodu Boş Olamaz");
                        return;
                    }

                    Variables.Result_ = arge.IfStokKoduExists(txt_eklenecek_mamul_kodu.Text);
                    if(!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessage("Eklenecek Mamul Kodunun Stok Kartı Sistemde Bulunmamaktadır.");
                        return;
                    }

                    if (string.IsNullOrEmpty(txt_eklenecek_miktar.Text))
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Miktar Boş Olamaz");
                        return;
                    }
                    if (Convert.ToDecimal(txt_eklenecek_miktar.Text) == 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Miktar 0 Olamaz");
                        return;
                    }

                    Variables.Result_ = await arge.CheckIfOpnoExists(txt_opno.Text, txt_eklenecek_mamul_kodu.Text);
                    if (Variables.Result_ == false)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Opno Mevcut.");
                        return;
                    }

                }

                if (guncellemeCollection != null) guncellemeCollection.Clear();
                if (comboBoxItemTag != "ekle")
                {
                    Variables.Counter_ = 0;
                    foreach (Cls_Arge arge in dg_ReceteSecim.Items)
                    {
                        if (arge.IsChecked == true)
                        {

                            if (comboBoxItemTag == "hamkodu" ||
                                comboBoxItemTag == "hammiktar")
                            {
                                arge.EskiHamKodu = arge.HamKodu;
                                arge.HamKodu = txt_yeni_ham_kodu.Text;
                            }
                            if (comboBoxItemTag == "miktar" ||
                                comboBoxItemTag == "hammiktar")
                            {
                                arge.EskiMiktar = arge.ReceteTuketimMiktar;
                                arge.ReceteTuketimMiktar = Convert.ToDecimal(txt_miktar.Text, CultureInfo.InvariantCulture);
                            }

                            guncellemeCollection.Add(arge);
                            Variables.Counter_++;
                        }
                    }

                    if (Variables.Counter_ == 0)
                    { CRUDmessages.NoInput(); return; }
                }


                if (comboBoxItemTag == "ekle")
                {
                    Cls_Arge eklenecek = new Cls_Arge();
                    eklenecek.EskiMiktar = 0;
                    eklenecek.ReceteTuketimMiktar = Convert.ToDecimal(txt_eklenecek_miktar.Text, CultureInfo.InvariantCulture);
                    eklenecek.EskiHamKodu = string.Empty;
                    eklenecek.HamKodu = txt_eklenecek_ham_kodu.Text;
                    eklenecek.MamulKodu = txt_eklenecek_mamul_kodu.Text;
                    eklenecek.Opno = txt_opno.Text;
                    guncellemeCollection.Add(eklenecek);
                }

                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_)
                    return;

                txt_please_wait_eklenecek.Visibility = Visibility.Visible;
                txt_please_wait.Visibility = Visibility.Visible;
                Variables.ResultInt_ = await arge.UpdateUrunAgaciAsync(guncellemeCollection, comboBoxItemTag);

                switch (Variables.ResultInt_)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sistemsel Problem İle Karşılaşıldı.");
                        txt_please_wait_eklenecek.Visibility = Visibility.Collapsed;
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        return;
                    case -2:
                        CRUDmessages.GeneralFailureMessage("Reçete Güncellenirken");
                        txt_please_wait_eklenecek.Visibility = Visibility.Collapsed;
                        txt_please_wait.Visibility = Visibility.Collapsed;

                        return;
                    case 1:
                        CRUDmessages.UpdateSuccessMessage("Reçete", guncellemeCollection.Count);
                        txt_please_wait_eklenecek.Visibility = Visibility.Collapsed;
                        txt_please_wait.Visibility = Visibility.Collapsed;
                        break;
                }

                if (comboBoxItemTag != "ekle")
                {
                    if (restrictionPairs == null)
                        return;

                    if (restrictionPairs.Count > 0)
                    {
                        if (receteCollection != null) receteCollection.Clear();
                        receteCollection = arge.PopulateReceteGuncelleList(restrictionPairs);
                        dg_ReceteSecim.ItemsSource = receteCollection;
                        dg_ReceteSecim.Items.Refresh();
                    }
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txt_eklenecek_mamul_kodu.Text)) return;

                    if (mamulDetayCollection != null) mamulDetayCollection.Clear();

                    mamulDetayCollection = arge.PopulateMamulReceteList(txt_eklenecek_mamul_kodu.Text);

                    if (mamulDetayCollection == null) return;
                    if (mamulDetayCollection.Count > 0)
                    {
                        dg_MamulDetayListe.ItemsSource = mamulDetayCollection;
                        dg_MamulDetayListe.Items.Refresh();
                    }

                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Kaydedilirken");
                txt_please_wait_eklenecek.Visibility = Visibility.Collapsed;
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private void btn_stok_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;

                string stokKodu = string.Empty;

                Frm_Stok_Karti_Rehberi frm = new();
                var result = frm.ShowDialog();
                if (result == true)
                {
                    stokKodu = frm.SelectedStokKodu;
                }


                if (clickedButton.Parent is StackPanel stackPanel)
                {
                    foreach (UIElement element in stackPanel.Children)
                    {
                        if (element is TextBox textBox)
                        {
                            textBox.Text = stokKodu;
                        }
                    }
                }

            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Stok Rehberi Açılırken");
            }
        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Arge item in dg_ReceteSecim.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private bool selectMiktarColumn = false;
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
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


    }
}

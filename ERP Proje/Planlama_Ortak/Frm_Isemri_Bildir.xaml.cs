using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Isemri_Bildir.xaml
    /// </summary>
    public partial class Frm_Isemri_Bildir : Window
    {

        Variables variables = new();
        Cls_Isemri isemri = new();
        ObservableCollection<Cls_Isemri> isemriCollection = new();
        ObservableCollection<Cls_Isemri> bildirimCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;

        public Frm_Isemri_Bildir()
        {
            InitializeComponent(); Window_Loaded();
            cb_alt_isemri_bildir.IsChecked = true;
            dp_bildirim_tarih.SelectedDate = DateTime.Now;
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

        private void cbx_bildirim_tipi_selection_changed(object sender, EventArgs e)
        {
            try
            {
                stc_constraints.Visibility = Visibility.Visible;
                btn_listele.Visibility = Visibility.Visible;
                txt_refisemrino.Visibility = Visibility.Collapsed;
                txb_refisemrino.Visibility = Visibility.Collapsed;
                txt_siparis_no.Visibility = Visibility.Collapsed;
                txb_siparisno.Visibility = Visibility.Collapsed;
                txt_siparis_sira.Visibility = Visibility.Collapsed;
                txb_sipsira.Visibility = Visibility.Collapsed;
                txt_stok_adi.Visibility = Visibility.Collapsed;
                txb_urunadi.Visibility = Visibility.Collapsed;
                txt_stok_kodu.Visibility = Visibility.Collapsed;
                txb_urunkodu.Visibility = Visibility.Collapsed;
                txt_takip_no.Visibility = Visibility.Collapsed;
                txb_takipno.Visibility = Visibility.Collapsed;
                txb_stokadi.Visibility = Visibility.Collapsed;
                txb_stokkodu.Visibility = Visibility.Collapsed;
                txt_urun_adi.Visibility = Visibility.Collapsed;
                txt_urun_kodu.Visibility = Visibility.Collapsed;

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
                case "takipno":
                    txt_takip_no.Visibility = Visibility.Visible;
                    txb_takipno.Visibility = Visibility.Visible;
                    break;
                case "siparisno":
                    txt_siparis_no.Visibility = Visibility.Visible;
                    txb_siparisno.Visibility = Visibility.Visible;
                    txt_siparis_sira.Visibility = Visibility.Visible;
                    txb_sipsira.Visibility = Visibility.Visible;
                    break;
                case "referansisemri":
                    txt_refisemrino.Visibility = Visibility.Visible;
                    txb_refisemrino.Visibility = Visibility.Visible;
                    break;
                case "stokkodu":
                    txb_stokkodu.Visibility = Visibility.Visible;
                    txb_stokadi.Visibility = Visibility.Visible;
                    txt_stok_adi.Visibility = Visibility.Visible;
                    txt_stok_kodu.Visibility = Visibility.Visible;
                    break;
                case "urunkodu":
                    txb_urunkodu.Visibility = Visibility.Visible;
                    txb_urunadi.Visibility = Visibility.Visible;
                    txt_urun_adi.Visibility = Visibility.Visible;
                    txt_urun_kodu.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                restrictionPairs.Clear();


                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text) &&
                    txt_siparis_no.Visibility == Visibility.Visible)
                {
                    if (txt_siparis_no.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_siparis_sira.Text) &&
                    txt_siparis_sira.Visibility == Visibility.Visible)
                    restrictionPairs.Add("@siparisSira", txt_siparis_sira.Text);

                if (!string.IsNullOrWhiteSpace(txt_takip_no.Text) && txt_takip_no.Visibility == Visibility.Visible)
                {
                    if (txt_takip_no.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Takip Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@takipno", txt_takip_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_refisemrino.Text) && txt_refisemrino.Visibility == Visibility.Visible)
                {
                    if (txt_refisemrino.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Referans İşemri Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@refisemrino", txt_refisemrino.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text) && txt_stok_kodu.Visibility == Visibility.Visible)
                {

                    if (txt_stok_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Stok Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text) && txt_stok_adi.Visibility == Visibility.Visible)
                {
                    if (txt_stok_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);
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

                if (restrictionPairs.Count == 1 &&
                    restrictionPairs.ContainsKey("@siparisSira"))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Yalnızca Sipariş Sıra ile Filtreleme Yapılamaz.");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                isemriCollection = isemri.PopulateIsemriBildirimList(restrictionPairs, false);

                if (isemriCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null; return;
                }

                if (!isemriCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null;
                }

                dg_IsemriSecim.ItemsSource = isemriCollection;
                dg_IsemriSecim.Visibility = Visibility.Visible;
                stc_isemri_bildir.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Yapılırken");
            }
        }
        private async void btn_isemri_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                bildirimCollection.Clear();
                foreach (Cls_Isemri isemri in dg_IsemriSecim.Items)
                {
                    if (isemri.IsChecked == true)
                    {
                        if (isemri.KALAN_IE_MIKTAR > (isemri.IE_MIKTAR - isemri.BILDIRILEN_MIKTAR))
                        {
                            string message = string.Format("Bildirilecek Miktar Kalan Miktardan Büyük Olamaz.{0}", isemri.ISEMRINO);
                            CRUDmessages.GeneralFailureMessageCustomMessage(message);
                            return;
                        }

                        if (isemri.DEPO_KODU == 0)
                        {
                            string message = string.Format("Depo Kodu 0 Olamaz.{0}", isemri.ISEMRINO);
                            CRUDmessages.GeneralFailureMessageCustomMessage(message);
                            return;
                        }
                        if (isemri.CIKIS_DEPO_KODU == 0)
                        {
                            string message = string.Format("Çıkış Depo Kodu 0 Olamaz.{0}", isemri.ISEMRINO);
                            CRUDmessages.GeneralFailureMessageCustomMessage(message);
                            return;
                        }



                        isemri.BildirimTarih = Convert.ToDateTime(dp_bildirim_tarih.SelectedDate);

                        bildirimCollection.Add(isemri);
                    }
                }

                Variables.Result_ = CRUDmessages.InsertOnayMessage();
                if (!Variables.Result_)
                    return;

                txt_please_wait.Visibility = Visibility.Visible;

                if (cb_alt_isemri_bildir.IsChecked == true)
                    variables.ResultInt = await isemri.InsertIsemriAsync(bildirimCollection, true);
                else
                    variables.ResultInt = await isemri.InsertIsemriAsync(bildirimCollection, false);

                switch (variables.ResultInt)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sistemsel Problem İle Karşılaşıldı.");
                        return;
                    case 2:
                        CRUDmessages.GeneralFailureMessageCustomMessage("İşemri Kaydedilirken");
                        return;
                    case 1:
                        CRUDmessages.InsertSuccessMessage("İşemri", bildirimCollection.Count);
                        break;
                }
                Frm_Isemri_Bildir _frm = new();
                _frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Kaydedilirken");
            }
        }

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Isemri item in dg_IsemriSecim.Items)
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
                            if (dataGrid.Columns[i].Header.ToString() == "Bildirilecek Miktar")
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

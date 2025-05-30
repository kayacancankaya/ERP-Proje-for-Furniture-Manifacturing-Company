﻿using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Uretim_Kaydi_Geri_Al.xaml
    /// </summary>
    public partial class Frm_Isemri_Sil : Window
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
        public Frm_Isemri_Sil()
        {
            InitializeComponent(); Window_Loaded();
            cb_alt_isemri_geri_al.IsChecked = true;
        }
        Variables variables = new();
        Cls_Isemri isemri = new();
        ObservableCollection<Cls_Isemri> isemriCollection = new();
        ObservableCollection<Cls_Isemri> gerialCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                {
                    if (txt_siparis_no.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_siparis_sira.Text))
                    restrictionPairs.Add("@siparisSira", txt_siparis_sira.Text);

                if (!string.IsNullOrWhiteSpace(txt_takip_no.Text))
                {
                    if (txt_takip_no.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Takip Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@takipno", txt_takip_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_refisemrino.Text))
                {
                    if (txt_refisemrino.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Referans İşemri Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@refisemrino", txt_refisemrino.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_urun_kodu.Text))
                {

                    if (txt_urun_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Stok Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@urunKodu", txt_urun_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_urun_adi.Text))
                {
                    if (txt_urun_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@urunAdi", txt_urun_adi.Text);
                }
                if (!string.IsNullOrWhiteSpace(txt_isemrino.Text))
                {
                    if (txt_isemrino.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("İşemri Numarasına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@isemrino", txt_isemrino.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_kodu.Text))
                {

                    if (txt_stok_kodu.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Stok Koduna 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }

                    restrictionPairs.Add("@stokKodu", txt_stok_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_stok_adi.Text))
                {
                    if (txt_stok_adi.Text.Length < 3)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Ürün Adına 3 Karakterden Az Giriş Yapılamaz.");
                        return;
                    }
                    restrictionPairs.Add("@stokAdi", txt_stok_adi.Text);
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

                isemriCollection = isemri.PopulateIsemriGeriAlList(restrictionPairs, false);

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
                stc_isemri_geri_al.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemri Bildirim Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }
        private async void btn_isemri_geri_al_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                gerialCollection.Clear();
                Variables.Counter_ = 0;
                foreach (Cls_Isemri isemri in dg_IsemriSecim.Items)
                {
                    if (isemri.IsChecked == true)
                    {

                        gerialCollection.Add(isemri);
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_)
                    return;

                txt_please_wait.Visibility = Visibility.Visible;

                variables.ResultInt = await isemri.IsemriGeriAlAsync(gerialCollection, false);

                switch (variables.ResultInt)
                {
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Sistemsel Problem İle Karşılaşıldı.");
                        return;
                    case 2:
                        CRUDmessages.GeneralFailureMessageCustomMessage("İşemri Geri Alınırken");
                        return;
                    case 1:
                        CRUDmessages.DeleteSuccessMessage("İşemri", gerialCollection.Count);
                        break;
                }

                txt_please_wait.Visibility = Visibility.Collapsed;

                Frm_Isemri_Sil _frm = new();
                _frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Kaydedilirken");
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

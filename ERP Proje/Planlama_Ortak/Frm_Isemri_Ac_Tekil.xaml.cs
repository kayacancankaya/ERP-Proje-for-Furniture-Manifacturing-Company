using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Layer_UI.Planlama_Ortak
{
    /// <summary>
    /// Interaction logic for Frm_Isemri_Tekten.xaml
    /// </summary>
    public partial class Frm_Isemri_Ac_Tekil : Window
    {
        Cls_Depo depo = new();
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
        public Frm_Isemri_Ac_Tekil()
        {
            InitializeComponent(); Window_Loaded();
            cbx_depo_kodu.ItemsSource = depo.GetDistinctDepoKodu();
            cbx_giris_depo.ItemsSource = depo.GetDistinctDepoKodu();
            cbx_cikis_depo.ItemsSource = depo.GetDistinctDepoKodu();
            dp_bildirim_tarih.SelectedDate = DateTime.Now;
        }
        public ObservableCollection<Cls_Planlama> SiparisCollection { get; set; }
        public ObservableCollection<Cls_Planlama> IsemriCollection { get; set; }

        Variables variables = new();
        Cls_Planlama plan = new();
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                variables.ErrorMessage = string.Empty;

                if (string.IsNullOrEmpty(txt_siparis_no.Text) &&
                    string.IsNullOrEmpty(txt_siparis_sira.Text) &&
                    string.IsNullOrEmpty(txt_stok_kodu.Text) &&
                    string.IsNullOrEmpty(txt_stok_adi.Text) &&
                    string.IsNullOrEmpty(txt_urun_kodu.Text) &&
                    string.IsNullOrEmpty(txt_urun_adi.Text))
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.NoInput();
                    return;
                }

                if (string.IsNullOrEmpty(txt_siparis_no.Text) &&
                    string.IsNullOrEmpty(txt_stok_kodu.Text) &&
                    string.IsNullOrEmpty(txt_stok_adi.Text) &&
                    string.IsNullOrEmpty(txt_urun_kodu.Text) &&
                    string.IsNullOrEmpty(txt_urun_adi.Text))
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Sipariş Sıra İle Filtreleme Yapılamaz.");
                    return;
                }

                Dictionary<string, string> kisitPairs = new Dictionary<string, string>();

                kisitPairs.Add("siparisNo", txt_siparis_no.Text);
                kisitPairs.Add("siparisSira", txt_siparis_sira.Text);
                kisitPairs.Add("stokKodu", txt_stok_kodu.Text);
                kisitPairs.Add("stokAdi", txt_stok_adi.Text);
                kisitPairs.Add("urunKodu", txt_urun_kodu.Text);
                kisitPairs.Add("urunAdi", txt_urun_adi.Text);

                SiparisCollection = new();

                SiparisCollection = plan.PopulateTekIsemriAcList(kisitPairs, false);
                if (!SiparisCollection.Any())
                { CRUDmessages.QueryIsEmpty("Sipariş"); Mouse.OverrideCursor = null; return; }

                dg_SiparisSecim.ItemsSource = SiparisCollection;

                dg_SiparisSecim.Visibility = Visibility.Visible;
                stack_panel_isemri_kaydet.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Yapılırken");
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_eksi_bakiye(object sender, EventArgs e)
        {
            try
            {

                if (cbx_depo_kodu.SelectedItem == null)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;

                variables.ErrorMessage = string.Empty;

                Dictionary<string, string> kisitPairs = new Dictionary<string, string>();

                kisitPairs.Add("depoKodu", cbx_depo_kodu.SelectedItem.ToString());

                SiparisCollection = new();

                SiparisCollection = plan.PopulateTekIsemriAcList(kisitPairs, true);
                if (!SiparisCollection.Any())
                { CRUDmessages.QueryIsEmpty("Sipariş"); Mouse.OverrideCursor = null; return; }

                dg_SiparisSecim.ItemsSource = SiparisCollection;

                dg_SiparisSecim.Visibility = Visibility.Visible;
                stack_panel_isemri_kaydet.Visibility = Visibility.Visible;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Eksi Bakiye Stokları Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }


        private async void btn_isemri_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            variables.WarningMessage = "İşemirleri Açılıyor.\n Lütfen Bekleyiniz.";
            Frm_Wait waitForm = new(variables.WarningMessage);
            try
            {

                if (cbx_giris_depo.SelectedItem == null)
                {
                    CRUDmessages.GeneralFailureMessage("Giriş Depo Seçiniz"); return;
                }
                if (cbx_cikis_depo.SelectedItem == null)
                {
                    CRUDmessages.GeneralFailureMessage("Çıkış Depo Seçiniz"); return;
                }

                IsemriCollection = new();
                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        item.IsemriAciklama = txt_aciklama.Text;
                        item.CikisDepoKodu = Convert.ToInt32(cbx_cikis_depo.SelectedItem);
                        item.DepoKodu = Convert.ToInt32(cbx_giris_depo.SelectedItem);
                        item.UretimBildir_ = false;
                        item.BildirimTarih = Convert.ToDateTime(dp_bildirim_tarih.SelectedDate);
                        if (item.IsemriMiktar == 0)
                        { CRUDmessages.GeneralFailureMessage("İşemri Miktarı 0 Olamaz."); return; }

                        IsemriCollection.Add(item);
                    }

                }


                btn_isemri.IsEnabled = false;
                btn_usk.IsEnabled = false;

                waitForm.Show();

                variables.ResultInt = await plan.InsertIsemriTekil(IsemriCollection, false);

                switch (variables.ResultInt)
                {
                    case 1:
                        CRUDmessages.InsertSuccessMessage("İşemri", IsemriCollection.Count);
                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessage("İş Emirleri Kaydedilirken");
                        break;
                    case 3:
                        CRUDmessages.GeneralFailureMessage("İş Emirleri Okunurken");
                        break;
                    case 4:
                        CRUDmessages.GeneralFailureMessageCustomMessage("İş Emirleri Bulunamadı");
                        break;
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Veri Tabanına Kayıt Yapılırken");
                        break;
                }


                waitForm.Close();
                Frm_Isemri_Ac_Tekil _frm = new();
                _frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Kaydedilirken");
                waitForm.Close();
            }
        }
        private async void btn_uretim_bildir_clicked(object sender, RoutedEventArgs e)
        {
            variables.WarningMessage = "İşemirleri Açılıyor.\n Lütfen Bekleyiniz.";
            Frm_Wait waitForm = new(variables.WarningMessage);
            try
            {

                if (cbx_giris_depo.SelectedItem == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Giriş Depo Seçiniz"); return;
                }
                if (cbx_cikis_depo.SelectedItem == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Çıkış Depo Seçiniz"); return;
                }

                IsemriCollection = new();
                foreach (Cls_Planlama item in dg_SiparisSecim.Items)
                {
                    if (item.IsChecked)
                    {
                        item.IsemriAciklama = txt_aciklama.Text;
                        item.CikisDepoKodu = Convert.ToInt32(cbx_cikis_depo.SelectedItem);
                        item.DepoKodu = Convert.ToInt32(cbx_giris_depo.SelectedItem);
                        item.UretimBildir_ = true;
                        item.BildirimTarih = Convert.ToDateTime(dp_bildirim_tarih.SelectedDate);
                        if (item.IsemriMiktar == 0)
                        { CRUDmessages.GeneralFailureMessage("İşemri Miktarı 0 Olamaz."); return; }

                        IsemriCollection.Add(item);
                    }

                }


                btn_isemri.IsEnabled = false;
                btn_usk.IsEnabled = false;

                waitForm.Show();

                variables.ResultInt = await plan.InsertIsemriTekil(IsemriCollection, true);

                switch (variables.ResultInt)
                {
                    case 1:
                        CRUDmessages.InsertSuccessMessage("İşemri", IsemriCollection.Count);
                        break;
                    case 2:
                        CRUDmessages.GeneralFailureMessage("İş Emirleri Kaydedilirken");
                        break;
                    case 3:
                        CRUDmessages.GeneralFailureMessage("İş Emirleri Okunurken");
                        break;
                    case 4:
                        CRUDmessages.GeneralFailureMessageCustomMessage("İş Emirleri Bulunamadı");
                        break;
                    case -1:
                        CRUDmessages.GeneralFailureMessageCustomMessage("Veri Tabanına Kayıt Yapılırken");
                        break;
                }


                waitForm.Close();
                Frm_Isemri_Ac_Tekil _frm = new();
                _frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("İşemirleri Kaydedilirken");
                waitForm.Close();
            }
        }

        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Planlama item in dg_SiparisSecim.Items)
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
                            if (dataGrid.Columns[i].Header.ToString() == "Gönderilecek İşemri Miktar" ||
                                dataGrid.Columns[i].Header.ToString() == "İşemri Açıklama")
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
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContex associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {
                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up
                }
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

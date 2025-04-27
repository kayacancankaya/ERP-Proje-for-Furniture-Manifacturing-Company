using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Satın_Alma
{
    /// <summary>
    /// Interaction logic for Frm_Siparis_Guncelle.xaml
    /// </summary>
    public partial class Frm_Siparis_Guncelle : Window
    {
        Cls_Planlama plan = new();
        Cls_Arge arge = new();
        Cls_SatinAlma satinAlma = new();
        Cls_Siparis siparis = new();
        LoginLogic login = new();
        string departman = string.Empty;
        Dictionary<int, string> planAdiDic = new();
        ObservableCollection<Cls_Planlama> talepColl = new();
        ObservableCollection<Cls_SatinAlma> SiparisVermeCollection = new();
        ObservableCollection<Cls_Planlama> planlar = new();
        Dictionary<string, string> restrictionPairs = new();
        List<string> planNoList = new();
        Dictionary<string, string> constraints = new();
        bool kapaliTalepleriGosterme = false;
        bool siradanPlanBagla = false;
        bool planSecerekBagla = false;
        bool teslimEdilenleriGosterme = false;
        string cariKodu = string.Empty;
        int dovizTipi = 0;
        public Frm_Siparis_Guncelle()
        {
            InitializeComponent(); Window_Loaded();
            cbx_kod_1.ItemsSource = arge.GetKod1();
        }
        private void btn_siparis_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_SiparisGuncelle.ItemsSource = null;
                dg_SiparisGuncelle.Items.Clear();

                Mouse.OverrideCursor = Cursors.Wait;

                constraints.Clear();

                if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    constraints.Add("stokKodu", txt_stok_kodu.Text);
                else
                    constraints.Add("stokKodu", null);

                if (!string.IsNullOrEmpty(txt_stok_adi.Text))
                    constraints.Add("stokAdi", txt_stok_adi.Text);
                else
                    constraints.Add("stokAdi", null);

                if (!string.IsNullOrEmpty(txt_talep_numarasi.Text))
                    constraints.Add("talepNumarasi", txt_talep_numarasi.Text);
                else
                    constraints.Add("talepNumarasi", null);

                if (!string.IsNullOrEmpty(txt_siparis_numarasi.Text))
                    constraints.Add("siparisNumarasi", txt_siparis_numarasi.Text);
                else
                    constraints.Add("siparisNumarasi", null);

                if (cbx_kod_1.SelectedItem != null)
                    constraints.Add("Kod1", cbx_kod_1.SelectedItem.ToString());
                else
                    constraints.Add("Kod1", null);
                
                if (cb_kapali_siparis.IsChecked == true)
                    kapaliTalepleriGosterme = true;
                else
                    kapaliTalepleriGosterme = false;
                if (cb_teslim_edilen.IsChecked == true)
                    teslimEdilenleriGosterme = true;
                else
                    teslimEdilenleriGosterme = false;

                listele();

            }

            catch { CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private void listele()
        {
            ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateSiparisReportList(constraints, 1, kapaliTalepleriGosterme, teslimEdilenleriGosterme);

            SiparisVermeCollection = firstColl;

            if (SiparisVermeCollection == null)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                dg_SiparisGuncelle.ItemsSource = null;
                dg_SiparisGuncelle.Visibility = Visibility.Collapsed;
                stc_buttons.Visibility = Visibility.Collapsed;
                return;
            }
            if (SiparisVermeCollection.Count() == 0)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.QueryIsEmpty();
                dg_SiparisGuncelle.ItemsSource = SiparisVermeCollection;
                txt_result.Visibility = Visibility.Collapsed;
                dg_SiparisGuncelle.Visibility = Visibility.Collapsed;
                stc_buttons.Visibility = Visibility.Collapsed;
                return;
            }
            dg_SiparisGuncelle.ItemsSource = SiparisVermeCollection;

            dg_SiparisGuncelle.Visibility = Visibility.Visible;
            stc_buttons.Visibility = Visibility.Visible;
            dg_SiparisGuncelle.Items.Refresh();

            Mouse.OverrideCursor = null;
        }
        private void btn_secilenleri_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_SatinAlma> guncellenecekColl = new();
                Mouse.OverrideCursor = Cursors.Wait;
                foreach (Cls_SatinAlma item in dg_SiparisGuncelle.Items)
                {
                    if (item.IsChecked)
                        guncellenecekColl.Add(item);
                }
                if (guncellenecekColl.Count < 1)
                {
                    CRUDmessages.NoSelection();
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.ResultInt_ = satinAlma.UpdateSiparis(guncellenecekColl, planlar, false, false);

                switch (Variables.ResultInt_)
                {
                    case 1:
                        {
                            CRUDmessages.UpdateSuccessMessage("Sipariş", guncellenecekColl.Count); break;
                        }
                    case -1:
                        {
                            CRUDmessages.GeneralFailureMessage("Veri Tabanında İşlem Yaparken"); Mouse.OverrideCursor = null; return;
                        }
                    case 2:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Bulunamadı."); Mouse.OverrideCursor = null; return;
                        }
                    case 3:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Giriniz."); Mouse.OverrideCursor = null; return;
                        }
                    case 4:
                        {
                            CRUDmessages.GeneralFailureMessage("Sipariş Genel Bilgileri Kaydedilirken"); Mouse.OverrideCursor = null; return;
                        }
                    case 5:
                        {
                            CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Kaydedilirken"); Mouse.OverrideCursor = null; return;
                        }
                    case 6:
                        {
                            CRUDmessages.GeneralFailureMessage("Fiş Numarası Alınırken"); Mouse.OverrideCursor = null; return;
                        }
                    case 7:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Hesaplama Tablosu Güncellenirken"); Mouse.OverrideCursor = null; return;
                        }
                }

                listele();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Güncelleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sil_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_SiparisGuncelle.Items.Count < 1)
                {
                    CRUDmessages.GeneralFailureMessage("Talep Listesi Bulunamadı.");
                    return;
                }
                Variables.Counter_ = 0;
                Variables.ErrorMessage_ = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;
                foreach (Cls_SatinAlma item in dg_SiparisGuncelle.Items)
                {
                    if (item.IsChecked)
                    {
                        Variables.Result_ = arge.IfStokKoduExists(item.StokKodu);
                        if (!Variables.Result_)
                        {
                            Variables.ErrorMessage_ += string.Format("Stok Kartı Bulunamadı.({0}) \n ", item.StokKodu);
                            continue;
                        }
                        if(item.SiparislestirilmisTalepMiktar>= Convert.ToSingle(item.TedarikSiparisMiktar))
                        {
                            Variables.ErrorMessage_ += string.Format("Siparişe Teslim Edilmiş.({0}) \n ", item.SiparisNumarasi,item.SiparisSira);
                            continue;
                        } 
                        Variables.Result_ = satinAlma.DeleteSiparisSatir(item.SiparisNumarasi, item.SiparisSira);
                        if (!Variables.Result_)
                        {
                            Variables.ErrorMessage_ += string.Format("Sipariş Kapama İşlemi Esnasında Hata İle Karşılaşıldı.({0},{1}) \n ", item.SiparisNumarasi, item.SiparisSira);
                            continue;
                        }
                        Variables.Counter_++;
                    }
                }
                if (!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.Counter_ < 1)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kapatılacak Sipariş Bulunamadı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                else
                    CRUDmessages.UpdateSuccessMessage("Sipariş", Variables.Counter_);


                listele();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Kapama İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_plan_kaldir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.ErrorMessage_ = string.Empty;
                Variables.Counter_ = 0;
                foreach (Cls_SatinAlma item in dg_SiparisGuncelle.Items)
                {
                    if (item.IsChecked)
                    {
                        if(string.IsNullOrEmpty(item.TalepNumarasi))
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}-{1} talep numarası yok.", item.SiparisNumarasi,item.SiparisSira));
                            continue;
                        }
                        if(item.TalepSira == 0)
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}-{1} talep sıra yok.", item.SiparisNumarasi, item.SiparisSira));
                            continue;
                        }
                        Variables.Result_ = satinAlma.UpdateTaleptenPlanKaldir(item.TalepNumarasi, item.TalepSira);
                        if (!Variables.Result_)
                            Variables.ErrorMessage_ += string.Format("Plan Bilgileri Kaldırılırken Hata İle Karşılaşıldı {0}-{1} \n", item.TalepNumarasi, item.TalepSira);
                        Variables.Counter_++;
                    }
                }
                if (Variables.Counter_ == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.NoSelection();
                    return;
                }
                if (!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    return;
                }

                //yeniden listele
                ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateSiparisReportList(constraints, 1, kapaliTalepleriGosterme, teslimEdilenleriGosterme);

                SiparisVermeCollection = firstColl;

                if (SiparisVermeCollection == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Siparişler Listelenirken");
                    dg_SiparisGuncelle.ItemsSource = null;
                    dg_SiparisGuncelle.Visibility = Visibility.Collapsed;
                    stc_buttons.Visibility = Visibility.Collapsed;
                    return;
                }
                if (SiparisVermeCollection.Count() == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    dg_SiparisGuncelle.ItemsSource = SiparisVermeCollection;
                    txt_result.Visibility = Visibility.Collapsed;
                    dg_SiparisGuncelle.Visibility = Visibility.Collapsed;
                    stc_buttons.Visibility = Visibility.Collapsed;
                    return;
                }
                dg_SiparisGuncelle.ItemsSource = SiparisVermeCollection;

                dg_SiparisGuncelle.Visibility = Visibility.Visible;
                stc_buttons.Visibility = Visibility.Visible;
                dg_SiparisGuncelle.Items.Refresh();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş(ler)e Ait Planlar Kaldırılırken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_cari_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button == null) return;
                Cls_SatinAlma cariGuncelle = button.DataContext as Cls_SatinAlma;
                if (cariGuncelle == null) return;
                if (cariGuncelle.SiparislestirilmisTalepMiktar > 0 )
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("İrsaliye Mevcut Cari Güncellenemez.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                Popup_Cari_Secim_Single _popup = new();
                Variables.FormResult_ = _popup.ShowDialog();
                {
                    if (Variables.FormResult_ == true)
                    {
                        if (string.IsNullOrEmpty(_popup.CariKodu))
                        {
                            CRUDmessages.GeneralFailureMessage("Cari Bilgisi Alınırken");
                            return;
                        }
                        if (string.IsNullOrEmpty(_popup.DovizTipi))
                        {
                            CRUDmessages.GeneralFailureMessage("Döviz Bilgisi Alınırken");
                            return;
                        }

                        Mouse.OverrideCursor = Cursors.Wait;
                        cariGuncelle.CariKodu = _popup.CariKodu;
                        cariGuncelle.CariAdi = _popup.CariAdi;
                        cariGuncelle.DovizTipi = _popup.DovizTipi;
                        switch (_popup.DovizTipi)
                        {
                            case "TRY":
                                dovizTipi = 0;
                                break;
                            case "USD":
                                dovizTipi = 1;
                                break;
                            case "EUR":
                                dovizTipi = 2;
                                break;
                        }
                    }
                }

                dg_SiparisGuncelle.Items.Refresh();
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Cari Formu Açılırken");
                Mouse.OverrideCursor = null;
            }
        }
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_SatinAlma item in dg_SiparisGuncelle.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private bool selectMiktarColumn = false;
        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                ContextMenu contextMenu = dataGrid.Resources["dgr_talep"] as ContextMenu;
                dataGrid.ContextMenu = contextMenu;
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
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {

                DataGridCellInfo cellInfo = dg_SiparisGuncelle.CurrentCell;

                if (cellInfo.Column.DisplayIndex != null)
                {
                    if (cellInfo.Column.DisplayIndex == 6)
                    {

                        var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);


                        if (cellContent is TextBox textBox)
                        {
                            // Clear the text inside the cell
                            textBox.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception)
            {
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

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;
                TextBox text = sender as TextBox;
                if (sender == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sipariş Bulunamadı");

                    Mouse.OverrideCursor = null;
                    return;
                }
                var cell = UIinteractions.FindVisualParent<DataGridCell>(text);

                // Retrieve the DataGridColumn
                var column = cell?.Column;

                Cls_SatinAlma siparis = text.DataContext as Cls_SatinAlma;
                if(siparis.SiparislestirilmisTalepMiktar > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Girişi Bulunan Sipariş Güncellenemez.");
                    Mouse.OverrideCursor = null;
                    switch (column?.Header?.ToString())
                    {
                        case "Depo Kodu":
                            siparis.DepoKodu = satinAlma.GetDepoKodu(siparis.SiparisNumarasi,siparis.SiparisSira);

                            break;
                        case "Stok Kodu":
                            siparis.StokKodu = satinAlma.GetStokKodu(siparis.SiparisNumarasi, siparis.SiparisSira);
                            siparis.StokAdi = arge.GetStokAdi(siparis.StokKodu);

                            break;
                        case "Sipariş Miktar":
                            siparis.SiparisMiktar = satinAlma.GetSiparisMiktar(siparis.SiparisNumarasi, siparis.SiparisSira);

                            break;

                    }

                    return;
                }
            
                switch (column?.Header?.ToString())
                {
                    case "Depo Kodu":
                        Cls_Depo depo = new();
                        Variables.Result_ = depo.CheckIfDepoKoduExists(Convert.ToInt32(text.Text));
                        if(!Variables.Result_)
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Depo Kodu Bulunamadı");
                            siparis.DepoKodu = satinAlma.GetDepoKodu(siparis.SiparisNumarasi, siparis.SiparisSira);
                            Mouse.OverrideCursor = null;
                            return;
                        }
                        siparis.DepoKodu = Convert.ToInt32(text.Text);
                        break;
                    case "Stok Kodu":
                        Cls_Arge arge = new();
                        Variables.Result_ = arge.IfStokKoduExists(text.Text.ToString());
                        if (!Variables.Result_)
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Stok Kodu Bulunamadı");
                            Mouse.OverrideCursor = null;
                            siparis.StokKodu = satinAlma.GetStokKodu(siparis.SiparisNumarasi, siparis.SiparisSira);
                            siparis.StokAdi = arge.GetStokAdi(siparis.StokKodu);
                            return;
                        }
                        siparis.StokKodu = text.Text.ToString();
                        siparis.StokAdi = arge.GetStokAdi(siparis.StokKodu);
                        break;
                    case "Sipariş Miktar":
                        siparis.SiparisMiktar = Convert.ToInt32(text.Text);
                        break;

                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Değer Değiştirilirken");
                Mouse.OverrideCursor = null;
            }
        }
    }
}

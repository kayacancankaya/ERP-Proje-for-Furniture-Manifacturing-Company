using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Satın_Alma
{
    /// <summary>
    /// Interaction logic for Frm_Talep_Siparislestir.xaml
    /// </summary>
    public partial class Frm_Talep_Siparislestir : Window
    {
        Cls_Arge arge = new();
        Cls_SatinAlma satinAlma = new();
        Cls_Planlama plan = new();
        Variables variables = new();
        LoginLogic login = new();
        ObservableCollection<Cls_SatinAlma> TalepSiparislestirmeCollection = new();
        ObservableCollection<Cls_SatinAlma> DataGridCollection = new();
        ObservableCollection<Cls_SatinAlma> SiparisCollection = new();
        string departman = string.Empty;
        Dictionary<int, string> planAdiDic = new();
        List<string> planNos = new();
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        bool pageUp = false;
        Dictionary<string, string> constraints = new Dictionary<string, string>();
        public Frm_Talep_Siparislestir()
        {
            try
            {


                InitializeComponent(); Window_Loaded();
                if (login.GetDepartment().Contains("Doseme"))
                    departman = "Doseme";
                else if (login.GetDepartment().Contains("Moduler"))
                    departman = "Moduler";
                else
                    departman = "Hepsi";

                if (plan.GetDistinctPlanAdiString(departman) != null)
                    planAdiDic = plan.GetDistinctPlanAdiString(departman);
                if (arge.GetDistinctKod1() != null)
                    cbx_kod_1.ItemsSource = arge.GetDistinctKod1();
                if (planAdiDic.Values != null)
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                if (plan.GetDistinctPlanNo() != null)
                    planNos = plan.GetDistinctPlanNo();
                if (planNos != null)
                    cbx_plan_no.ItemsSource = planNos;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
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
        private void cbx_plan_adi_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNos;
                    return;
                }

                List<string> relatedPlanNos = plan.GetPlanAdiRelatedPlanNos(combo.SelectedItem.ToString());
                cbx_plan_no.ItemsSource = relatedPlanNos;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
            }
        }
        private void cbx_plan_no_selected_item_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;

                if (combo.SelectedItem == null)
                {
                    cbx_plan_adi.ItemsSource = planAdiDic.Values;
                    cbx_plan_no.ItemsSource = planNos;
                    return;
                }

                List<string> relatedPlanAdis = plan.GetPlanNoRelatedPlanAdis(combo.SelectedItem.ToString());
                cbx_plan_adi.ItemsSource = relatedPlanAdis;

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Plan Adı Değiştirilirken");
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
        private void btn_talep_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_TalepSecim.ItemsSource = null;
                dg_TalepSecim.Items.Clear();

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

                if (cbx_kod_1.SelectedItem != null)
                    constraints.Add("Kod1", cbx_kod_1.SelectedItem.ToString());
                else
                    constraints.Add("Kod1", null);

                if (cbx_plan_no.SelectedItem != null)
                    constraints.Add("PlanNo", cbx_plan_no.SelectedItem.ToString());
                else
                    constraints.Add("PlanNo", null);

                if (cbx_plan_adi.SelectedItem != null)
                    constraints.Add("PlanAdi", cbx_plan_adi.SelectedItem.ToString());
                else
                    constraints.Add("PlanAdi", null);

                ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateTalepSiparislestirmeList(constraints, 1);

                TalepSiparislestirmeCollection = firstColl;

                if (TalepSiparislestirmeCollection == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Talepler Listelenirken");
                    return;
                }
                if (TalepSiparislestirmeCollection.Count() == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    return;
                }

                totalResult = satinAlma.CountTalepSiparislestirmeList(constraints, 1);
                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                dg_TalepSecim.ItemsSource = TalepSiparislestirmeCollection;
                txt_result.Visibility = Visibility.Visible;
                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;
                Mouse.OverrideCursor = null;

            }

            catch { CRUDmessages.GeneralFailureMessage("Talepler Listelenirken"); Mouse.OverrideCursor = null; txt_result.Visibility = Visibility.Collapsed; }
        }
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(dg_TalepSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;
                // Check if vertical scrolling occurred
                if (e.ScrollEventType == ScrollEventType.EndScroll && scrollViewer.VerticalOffset > lastOffset)
                {

                    lastOffset = scrollViewer.VerticalOffset;

                    if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > SiparisCollection.Count())
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        pageNumber++;
                        ObservableCollection<Cls_SatinAlma> moreSiparisCollection = new();
                        moreSiparisCollection = satinAlma.PopulateTalepSiparislestirmeList(constraints, pageNumber);
                        if (moreSiparisCollection == null)
                        {
                            CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                            Mouse.OverrideCursor = null;
                        }
                        if (moreSiparisCollection.Count > 0)
                        {
                            ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                            (TalepSiparislestirmeCollection.Union(moreSiparisCollection).ToList());
                            dg_TalepSecim.ItemsSource = lastTalepCollection;
                            dg_TalepSecim.Items.Refresh();
                            TalepSiparislestirmeCollection = lastTalepCollection;

                            txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                                double center = 0;
                                    center = scrollViewer.ScrollableHeight / 2.0;
                                    scrollViewer.ScrollToVerticalOffset(center);
                                lastOffset = center;
                            
                        }
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(dg_TalepSecim, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;
                lastOffset = scrollViewer.VerticalOffset;
                    if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > TalepSiparislestirmeCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_SatinAlma> moreTalepCollection = new();
                    moreTalepCollection = satinAlma.PopulateTalepSiparislestirmeList(constraints, pageNumber);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Talepler Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_SatinAlma> lastTalepCollection = new ObservableCollection<Cls_SatinAlma>
                                        (TalepSiparislestirmeCollection.Union(moreTalepCollection).ToList());
                        dg_TalepSecim.ItemsSource = lastTalepCollection;
                        dg_TalepSecim.Items.Refresh();
                        TalepSiparislestirmeCollection = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                            double center = 0;
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            lastOffset = center;
                        
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
            }

        }
        private void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Ürün Eklerken Hata İle Karşılaşıldı."); return; }

                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                variables.ErrorMessage = row == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;


                // Get the data item associated with the row
                Cls_SatinAlma? dataItem = row.Item as Cls_SatinAlma;

                variables.ErrorMessage = dataItem == null ? "Hata ile Karşılaşıldı" : variables.ErrorMessage;
                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; };

                if(dataItem.KalanTalepMiktar>(dataItem.TalepMiktar-dataItem.SiparislestirilmisTalepMiktar))
                {
                    MessageBox.Show("Siparişleştirilecek Talep Miktarı Kalan Bakiyeden Büyük."); return;
                }

                Variables.Result_ = EklemeKontrol(dataItem, DataGridCollection);

                if (Variables.Result_)
                    DataGridCollection.Add(dataItem);
                else
                    return; 

                stack_panel_talep_siparislestir.Visibility = Visibility.Visible;
                dg_SiparisEkle.Visibility = Visibility.Visible;
                wrap_panel_cari.Visibility = Visibility.Visible;

                dg_SiparisEkle.ItemsSource = DataGridCollection;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_siparis_sil(object sender, RoutedEventArgs e)
        {

            try
            {

                variables.ErrorMessage = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); return; }
                Cls_SatinAlma dataItem = row.Item as Cls_SatinAlma;
                DataGridCollection.Remove(dataItem);

                dg_SiparisEkle.ItemsSource = DataGridCollection;

                dg_SiparisEkle.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private bool EklemeKontrol(Cls_SatinAlma siparisItem, ObservableCollection<Cls_SatinAlma> dataGridCollection)
        {
            try
            {
                Variables.Result_ = arge.CheckIfDepoKoduExists(siparisItem.DepoKodu);
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Sistemde Kayıtlı Depo Kodu Bulunamadı.");
                    return false;
                }
                foreach (Cls_SatinAlma item in dataGridCollection)
                {
                    if (item.StokKodu == siparisItem.StokKodu &&
                        item.TalepNumarasi == siparisItem.TalepNumarasi &&
                        item.TalepSira == siparisItem.TalepSira)
                     {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Birden Fazla Aynı Talep Satır Numarası Eklenemez.");
                        return false;
                    }
                    
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void Show_Cari_Rehberi_Clicked(object sender, RoutedEventArgs e)
        {

            var cariSecimWindow = new Popup_Cari_Secim_Single();
            // Show the popup window
            cariSecimWindow.ShowDialog();

            var result = cariSecimWindow.DialogResult;
            if (result == true)
            {
                if (string.IsNullOrEmpty(cariSecimWindow.CariKodu) ||
                   string.IsNullOrEmpty(cariSecimWindow.CariAdi) ||
                   string.IsNullOrEmpty(cariSecimWindow.DovizTipi))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Cari Bilgileri Getirilemedi. Tekrar Deneyiniz.");
                    return;
                }

                txt_cari_kodu.Text = cariSecimWindow.CariKodu;
                txt_doviz_tipi.Text = cariSecimWindow.DovizTipi;
                txt_cari_adi.Text = cariSecimWindow.CariAdi;
            }

        }
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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
                            if (dataGrid.Columns[i].Header.ToString() == "Kalan Miktar" ||
                                dataGrid.Columns[i].Header.ToString() == "Stok Fiyat" ||
                                dataGrid.Columns[i].Header.ToString() == "Miktar" ||
                                dataGrid.Columns[i].Header.ToString() == "Fiyat")
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
        private void btn_talep_siparislestir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                variables.ErrorMessage = string.Empty;
                variables.WarningMessage = string.Empty;
                variables.SuccessMessage = string.Empty;

                if (string.IsNullOrEmpty(txt_cari_kodu.Text)) variables.ErrorMessage = variables.ErrorMessage + "Cari Kodu Eksik.\n";
                if (string.IsNullOrEmpty(txt_doviz_tipi.Text)) variables.ErrorMessage = variables.ErrorMessage + "Döviz Tipi Eksik.\n";

                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); variables.ErrorMessage = string.Empty; Mouse.OverrideCursor = null; return; }

                variables.QumulativeSum = 0;
                variables.Counter = 0;
                decimal qumulativeKdv = 0;
                if (SiparisCollection.Count > 0)
                    SiparisCollection.Clear();
                foreach (Cls_SatinAlma item in dg_SiparisEkle.Items)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.StokKodu)) variables.ErrorMessage = variables.ErrorMessage + string.Format("{0}. Satır Stok Kodu Eksik.\n", variables.Counter + 1);
                        if (item.KalanTalepMiktar is 0) variables.ErrorMessage = variables.ErrorMessage + string.Format("{0}. Satır Sipariş Miktarı 0 Olamaz.\n", variables.Counter + 1);
                        if (item.StokFiyat is 0) variables.WarningMessage = variables.WarningMessage + string.Format("{0}. Satır Fiyat Bilgisi Sıfır.\n", variables.Counter + 1);

                        if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); variables.ErrorMessage = string.Empty; Mouse.OverrideCursor = null; return; }

                        item.StokFiyat = UserEntryControl.TruncateToFourDecimalPlaces(item.StokFiyat);
                        item.KalanTalepMiktar = UserEntryControl.TruncateToFourDecimalPlaces(item.KalanTalepMiktar);

                        variables.QumulativeSum = variables.QumulativeSum + Convert.ToSingle(item.StokFiyat * item.KalanTalepMiktar);
                        item.StokKDV = arge.GetKdvOrani(item.StokKodu);

                        qumulativeKdv = qumulativeKdv + (Convert.ToDecimal(item.StokFiyat) * (item.StokKDV / 100) * Convert.ToDecimal(item.KalanTalepMiktar));

                        variables.Counter++;
                        item.SiparisSira = variables.Counter;
                        SiparisCollection.Add(item);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; return; }

                }

                string siparisNumarasi = satinAlma.GetTedarikciSiparisNumarasi();

                if (string.IsNullOrEmpty(siparisNumarasi) ||
                    siparisNumarasi == "STRING HATA")
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Sipariş İçin Fiş Numarası Alınırken");
                    return;
                }
                foreach (Cls_SatinAlma item in SiparisCollection)
                {
                    item.SiparisNumarasi = siparisNumarasi;
                    item.SiparisToplamTutar = Convert.ToDouble(variables.QumulativeSum);
                    item.SiparisToplamKDV = Convert.ToDouble(qumulativeKdv);
                    item.SiparisAciklama = string.IsNullOrEmpty(txt_vade.Text) ? string.Empty : txt_siparis_aciklama.Text;
                    item.Vade = string.IsNullOrEmpty(txt_vade.Text) ? 0 : Convert.ToInt32(txt_vade.Text);
                    item.CariKodu = txt_cari_kodu.Text;
                    item.FaturaKalemAdedi = variables.Counter;

                    item.TeslimTarih = dp_teslim_tarih.SelectedDate.HasValue ? Convert.ToDateTime(dp_teslim_tarih.SelectedDate)
                                                                                 : item.TeslimTarih = DateTime.Now.AddDays(14);

                    switch (txt_doviz_tipi.Text)
                    {
                        case ("TRY"):
                            item.DovizTipiInt = 0;
                            break;
                        case ("USD"):
                            item.DovizTipiInt = 1;
                            break;
                        case ("EUR"):
                            item.DovizTipiInt = 2;
                            break;
                        default:
                            item.DovizTipiInt = 0;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(variables.WarningMessage) == false)
                    variables.Result = CRUDmessages.DoYouWishToContinue(variables.WarningMessage);
                else
                    variables.Result = CRUDmessages.InsertOnayMessage();
                if (!variables.Result)
                {
                    Mouse.OverrideCursor = null; return;
                }

                variables.IsTrue = satinAlma.InsertTedarikciSiparisGenel(SiparisCollection);
                if (!variables.IsTrue)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Siparişin Genel Bilgileri Kaydedilirken Hata İle Karşılaşıldı.");
                    satinAlma.KayitGeriAlTedarikciSiparis(siparisNumarasi);
                    Mouse.OverrideCursor = null;
                    return;
                }

                variables.IsTrue = satinAlma.InsertTedarikciSiparisSatir(SiparisCollection);
                if (!variables.IsTrue)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Siparişin Satır Bilgileri Kaydedilirken Hata İle Karşılaşıldı.");
                    satinAlma.KayitGeriAlTedarikciSiparis(siparisNumarasi);
                    Mouse.OverrideCursor = null;
                    return;
                }

                CRUDmessages.InsertSuccessMessage("Sipariş", SiparisCollection.Count());
                Mouse.OverrideCursor = null;
                Frm_Talep_Siparislestir frm = new();
                frm.Show();
                this.Close();
                return;

            }

            catch
            {
                CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Kaydedilirken");
                Mouse.OverrideCursor = null;
            }
        }
    }
}

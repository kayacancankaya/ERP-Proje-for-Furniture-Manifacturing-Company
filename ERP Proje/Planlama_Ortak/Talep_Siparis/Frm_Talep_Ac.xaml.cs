using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using PdfSharp.BigGustave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Planlama_Ortak.Talep_Siparis
{
    /// <summary>
    /// Interaction logic for Frm_Talep_Ac.xaml
    /// </summary>
    public partial class Frm_Talep_Ac : Window
    {
        Cls_Siparis siparis = new();
        Cls_Arge arge = new();
        Cls_Planlama plan = new();
        ObservableCollection<Cls_Siparis> talepColl = new();
        public ObservableCollection<Cls_Planlama> planlar { get; set; } = new();

        ObservableCollection<Cls_Siparis> siradanPlanaBaglanan = new();
        bool siradanPlanBagla = false;
        bool planSecerekBagla = false;
        Dictionary<string, string> constraintPairs = new();
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
        public Frm_Talep_Ac()
        {
            InitializeComponent(); Window_Loaded();
            //dg_StokSecim.Items.Clear();
            cbx_kod1.ItemsSource = arge.GetDistinctKod1();
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                constraintPairs.Clear();
                if (!string.IsNullOrEmpty(txt_stok_kodu.Text))
                    constraintPairs.Add("@stokKodu", txt_stok_kodu.Text);
                if (cbx_kod1.SelectedItem != null)
                    constraintPairs.Add("@kod1", cbx_kod1.SelectedItem.ToString());

                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Siparis> firstColl = siparis.GetTalepCollection(constraintPairs, 1);
                if (firstColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Veri Tabanına Bağlanırken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (firstColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }
                talepColl = firstColl;

                totalResult = siparis.CountTalepAcList(constraintPairs, 1);

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;


                dg_StokSecim.ItemsSource = talepColl;
                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Listeleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void detailed_button_clicked(object sender,RoutedEventArgs e)
        {
            try
            {

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Detay Listelenirken");
                return;
            }
        }
        private void add_product_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                Button? button = sender as Button;
                if (button == null) { MessageBox.Show("Talep Eklerken Hata İle Karşılaşıldı."); return; }

                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);
                if (row == null) { MessageBox.Show("Talep Eklerken Hata İle Karşılaşıldı."); return; }

                Cls_Siparis? dataItem = row.Item as Cls_Siparis;
                if (dataItem == null) { MessageBox.Show("Talep Eklerken Hata İle Karşılaşıldı."); return; }

                if (UserEntryControl.StringUzunlukKontrol(dataItem.StokKodu, 35, true, "Stok Kodu") == false) return;

                if (UserEntryControl.Miktar0Kontrol(Convert.ToDecimal(dataItem.GonderilecekMiktar), "Talep Miktar") == false) return;

                if (UserEntryControl.DepoKoduKontrol(dataItem.DepoKodu) == false) return;

                //if (UserEntryControl.MiktarKiyasKontrol(Convert.ToDecimal(dataItem.MinimumSiparisMiktar), Convert.ToDecimal(dataItem.TedarikGirisMiktar), true, true, "Minimum Sipariş Miktarı,Talep Miktarından ") == false) return;

                if (EklemeKontrol(dataItem.StokKodu, siparis.SiparisCollection) == false) return;

                Cls_Siparis? eklenecekSiparis = new Cls_Siparis
                {
                    StokKodu = dataItem.StokKodu,
                    StokAdi = dataItem.StokAdi,
                    DepoKodu = dataItem.DepoKodu,
                    StokKDV = dataItem.StokKDV,
                    GonderilecekMiktar = dataItem.GonderilecekMiktar,
                    TerminTarih = dataItem.TerminTarih,
                    TalepAciklama = dataItem.TalepAciklama,

                };

                siparis.SiparisCollection.Add(eklenecekSiparis);
                dg_TalepEkle.ItemsSource = siparis.SiparisCollection;
                dg_TalepEkle.Items.Refresh();
                dg_TalepEkle.Visibility = Visibility.Visible;
                stack_panel_talep_kaydet.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private bool EklemeKontrol(string stokKodu, ObservableCollection<Cls_Siparis> siparisCollection)
        {
            try
            {
                foreach (Cls_Siparis item in siparisCollection)
                {
                    if (item.StokKodu == stokKodu)
                    {
                        MessageBox.Show("Birden Fazla Aynı Ürün Eklenemez.");
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
        private void btn_talep_kaydet_clicked(object sender, EventArgs e)
        {
            try
            {
                if (siparis.SiparisCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Talep Bulunamadı.");
                    return;
                }
                if (siparis.SiparisCollection.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                string onayMessage = string.Empty;

                if (cb_plana_bagla.IsChecked == true)
                    onayMessage = "Sıradan Plana Bağlanarak Kaydedilecek. Onaylıyor Musunuz?";
                else if (cb_plan_secerek_bagla.IsChecked == true)
                    onayMessage = "Seçili Planlara Kayıt Yapılacak. Onaylıyor Musunuz?";
                else
                    onayMessage = "Serbest Kayıt Yapılacak. Onaylıyor Musunuz?";

                Variables.Result_ = CRUDmessages.InsertOnayMessage(onayMessage);
                if (!Variables.Result_)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Counter_ = 0;
                Variables.QumulativeSum_ = 0;
                float QumulativeKDV = 0;
                foreach (Cls_Siparis item in dg_TalepEkle.Items)
                {
                    if (UserEntryControl.DepoKoduKontrol(item.DepoKodu) == false)
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    if (UserEntryControl.Miktar0Kontrol(Convert.ToDecimal(item.GonderilecekMiktar), "Miktar") == false)
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    Variables.Result_ = arge.IfStokKoduExists(item.StokKodu);
                    if(!Variables.Result_)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0} Stok Kartı Bulunamadı"));
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    Variables.Counter_++;
                    item.TalepSira = Variables.Counter_;
                    item.TalepGenelAciklama = string.IsNullOrWhiteSpace(txt_talep_aciklama.Text) ? string.Empty : txt_talep_aciklama.Text;
                    item.TalepAciklama = item.TalepAciklama == "Lütfen Açıklama Giriniz..." ? string.Empty : item.TalepAciklama;
                    item.GonderilecekMiktar = UserEntryControl.TruncateToFourDecimalPlaces(Convert.ToSingle(item.GonderilecekMiktar, CultureInfo.InvariantCulture));
                    item.StokKDV = Convert.ToSingle(item.StokKDV, CultureInfo.InvariantCulture);
                }
                siradanPlanBagla = false;
                planSecerekBagla = false;
                if (cb_plana_bagla.IsChecked == true)
                {
                    siradanPlanBagla = true;
                }
                else if (cb_plan_secerek_bagla.IsChecked == true)
                {
                    planSecerekBagla = true;
                    if(planlar == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Plan Seçerek Bağla Seçili Olmasına Rağmen Seçilen Plan Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if(planlar.Count == 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Plan Seçerek Bağla Seçili Olmasına Rağmen Seçilen Plan Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }


                Variables.ResultInt_ = siparis.InsertTalep(siparis.SiparisCollection, Variables.QumulativeSum_, QumulativeKDV,planlar, siradanPlanBagla, planSecerekBagla);

                switch (Variables.ResultInt_)
                {
                    case 1:
                        {
                            CRUDmessages.InsertSuccessMessage("Talep", siparis.SiparisCollection.Count); break;
                        }
                    case -1:
                        {
                            CRUDmessages.GeneralFailureMessage("Veri Tabanında İşlem Yaparken"); Mouse.OverrideCursor = null; return;
                        }
                    case 2:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Talep Bulunamadı."); Mouse.OverrideCursor = null; return;
                        }
                    case 3:
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage("Talep Giriniz."); Mouse.OverrideCursor = null; return;
                        }
                    case 4:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Genel Bilgileri Kaydedilirken"); Mouse.OverrideCursor = null; return;
                        }
                    case 5:
                        {
                            CRUDmessages.GeneralFailureMessage("Talep Bilgileri Kaydedilirken"); Mouse.OverrideCursor = null; return;
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

                Mouse.OverrideCursor = null;
                Frm_Talep_Ac frm = new();
                frm.Show();
                this.Close();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border checkBox)
            {
                if (checkBox.Name == "DGR_Border") return;
                if (checkBox.Child is SelectiveScrollingGrid) return;

                // Get the DataContext (Cls_Isemri) associated with the clicked checkbox
                if (checkBox.DataContext is Cls_Isemri item && checkBox.Child is ContentPresenter && checkBox.ActualHeight == 15.098340034484863 && checkBox.ActualWidth == 15.974980354309082)
                {

                    item.IsChecked = !item.IsChecked; // Toggle the IsChecked property
                    e.Handled = true; // Prevent the checkbox click event from bubbling up

                }
            }

        }
        private void btn_talep_sil(object sender, RoutedEventArgs e)
        {

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Talep Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Talep Bilgileri Alınırken"); return; }

                Cls_Siparis dataItem = row.Item as Cls_Siparis;

                siparis.SiparisCollection.Remove(dataItem);

                //dg_TalepEkle.ItemsSource = siparis.SiparisCollection;

                //dg_TalepEkle.Items.Refresh();

                Mouse.OverrideCursor = null;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_plan_sec_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Button clickedButton = (Button)sender;
                string stokKodu = string.Empty;
                Frm_Plan_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    foreach(Cls_Planlama item in frm.AktarilacakPlanlar)
                    {
                        if (!planlar.Where(p => p.PlanAdi == item.PlanAdi).Any())
                            planlar.Add(item);
                    }
                    btn_secilen_planlari_goster.Visibility = Visibility.Visible;
                }
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Stok Rehberi Açılırken");
                Mouse.OverrideCursor = null;
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
        private void btn_secilen_planlari_goster_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                if (planlar == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Seçilen Planlar Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (planlar.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Hiç Plan Seçilmedi.");
                    btn_secilen_planlari_goster.Visibility = Visibility.Collapsed;
                    Mouse.OverrideCursor = null;
                    return;
                }
                dg_SecilenPlanlar.ItemsSource = planlar;
                popupSecilenPlanlar.IsOpen = true;
                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Seçilen Planlar Gösterilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_secilen_plan_sil(object sender, RoutedEventArgs e)
        {
            try
            {
                Button? button = sender as Button;
                if (button == null) { CRUDmessages.GeneralFailureMessage("Plan Bilgileri Alınırken"); return; }
                DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                if (row == null) { CRUDmessages.GeneralFailureMessage("Plan Bilgileri Alınırken"); return; }

                Cls_Planlama dataItem = row.Item as Cls_Planlama;

                planlar.Remove(dataItem);
                if (planlar.Count == 0)
                    btn_secilen_planlari_goster.Visibility = Visibility.Collapsed;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Seçilen Plan Silinirken");
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
        private void CheckBox_SelectionChanged(object sender,RoutedEventArgs e)
        {
            try
            {
                CheckBox check = sender as CheckBox;
                if (check == null)
                    return;
                if (check.Name == "cb_plana_bagla" &&
                    check.IsChecked == true)
                    cb_plan_secerek_bagla.IsChecked = false;
                if (check.Name == "cb_plan_secerek_bagla" &&
                    check.IsChecked == true)
                    cb_plana_bagla.IsChecked = false;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Check Box İşlemi Esnasında");
            }
        }
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        ScrollViewer scrollViewer;
        Decorator border;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                border = VisualTreeHelper.GetChild(dg_StokSecim, 0) as Decorator;
                if (border != null)
                    scrollViewer = border.Child as ScrollViewer;

                lastOffset = scrollViewer.VerticalOffset;
                if (lastOffset > scrollViewer.Height - (scrollViewer.Height/10) && totalResult > talepColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Siparis> moreSiparisCollection = new();
                    moreSiparisCollection = siparis.GetTalepCollection(constraintPairs, pageNumber);
                    if (moreSiparisCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreSiparisCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Siparis> lastTalepCollection = new ObservableCollection<Cls_Siparis>
                                        (talepColl.Union(moreSiparisCollection).ToList());
                        //dg_StokSecim.ItemsSource = lastTalepCollection;
                        //dg_StokSecim.Items.Refresh();
                        talepColl = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        if (scrollViewer != null)
                        {
                            center = scrollViewer.ScrollableHeight / 2.0;
                            scrollViewer.ScrollToVerticalOffset(center);
                        }
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
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                border = VisualTreeHelper.GetChild(dg_StokSecim, 0) as Decorator;
                if (border != null)
                    scrollViewer = border.Child as ScrollViewer;

                lastOffset = scrollViewer.VerticalOffset;

                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > talepColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Siparis> moreTalepCollection = new();
                    moreTalepCollection = siparis.GetTalepCollection(constraintPairs, pageNumber);
                    if (moreTalepCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Stoklar Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreTalepCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Siparis> lastTalepCollection = new ObservableCollection<Cls_Siparis>
                                        (talepColl.Union(moreTalepCollection).ToList());
                        dg_StokSecim.ItemsSource = lastTalepCollection;
                        dg_StokSecim.Items.Refresh();
                        talepColl = lastTalepCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastTalepCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                          if (scrollViewer != null)
                          {
                              center = scrollViewer.ScrollableHeight / 2.0;
                              scrollViewer.ScrollToVerticalOffset(center);
                          }
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
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only digits, one dot, and handling backspace.
            Regex regex = new Regex("[^0-9.-]+"); // Regex for allowed input
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

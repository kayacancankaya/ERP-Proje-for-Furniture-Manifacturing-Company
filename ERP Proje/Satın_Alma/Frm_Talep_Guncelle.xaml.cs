using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Planlama_Ortak.Talep_Siparis;
using Layer_UI.UserControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for Frm_Talep_Guncelle.xaml
    /// </summary>
    public partial class Frm_Talep_Guncelle : Window
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
        bool siparislesenleriGosterme = false;
        bool siradanPlanBagla = false;
        bool planSecerekBagla = false;
        bool teslimEdilenleriGosterme = false;
        public Frm_Talep_Guncelle()
        {
            InitializeComponent();Window_Loaded();
            cbx_kod_1.ItemsSource = arge.GetKod1();
        }
        private void btn_talep_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dg_TalepGuncelle.ItemsSource = null;
                dg_TalepGuncelle.Items.Clear();

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
                if (cb_siparis_edilen.IsChecked == true)
                    siparislesenleriGosterme = true;
                else
                    siparislesenleriGosterme = false;
                if (cb_teslim_edilen.IsChecked == true)
                    teslimEdilenleriGosterme = true;
                else
                    teslimEdilenleriGosterme = false;

                listele();

            }

            catch { CRUDmessages.GeneralFailureMessage("Talepler Listelenirken"); Mouse.OverrideCursor = null;}
        }
        private void listele()
        {
            ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateTalepReportList(constraints, 1, kapaliTalepleriGosterme, siparislesenleriGosterme, teslimEdilenleriGosterme);

            SiparisVermeCollection = firstColl;

            if (SiparisVermeCollection == null)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Talepler Listelenirken");
                dg_TalepGuncelle.ItemsSource = null;
                dg_TalepGuncelle.Visibility = Visibility.Collapsed;
                stc_buttons.Visibility = Visibility.Collapsed;
                return;
            }
            if (SiparisVermeCollection.Count() == 0)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.QueryIsEmpty();
                dg_TalepGuncelle.ItemsSource = SiparisVermeCollection;
                txt_result.Visibility = Visibility.Collapsed;
                dg_TalepGuncelle.Visibility = Visibility.Collapsed;
                stc_buttons.Visibility = Visibility.Collapsed;
                return;
            }
            dg_TalepGuncelle.ItemsSource = SiparisVermeCollection;

            dg_TalepGuncelle.Visibility = Visibility.Visible;
            stc_buttons.Visibility = Visibility.Visible;
            dg_TalepGuncelle.Items.Refresh();

            Mouse.OverrideCursor = null;
        }
        private void btn_plana_bagla_clicked(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if(button.Tag as string == "Siradan Bagla")
                {
                    siradanPlanBagla = true;
                    planSecerekBagla = false;
                    Variables.ErrorMessage_ = "Sıradan Plana Bağlanarak Kaydedilecek. Onaylıyor Musunuz?";
                }
                else if(button.Tag as string == "Secerek Bagla")
                {
                    siradanPlanBagla = false;
                    planSecerekBagla = true;
                    Variables.ErrorMessage_ = "Seçtiğiniz Planlara Bağlanacak. Onaylıyor Musunuz?";
                }
                else
                {
                    siradanPlanBagla = false;
                    planSecerekBagla = false;
                    Variables.ErrorMessage_ = "Güncelleme İşlemini Onaylıyor Musunuz?";
                }

                Variables.Result_ = CRUDmessages.InsertOnayMessage(Variables.ErrorMessage_);
                if (!Variables.Result_)
                    return;

                Mouse.OverrideCursor = Cursors.Wait;
                ObservableCollection<Cls_SatinAlma> planabaglaColl = new();
                foreach (Cls_SatinAlma item in dg_TalepGuncelle.Items)
                {
                    if (item.IsChecked == true)
                    {
                        Variables.Result_ = arge.IfStokKoduExists(item.StokKodu);
                        if (!Variables.Result_)
                        {
                            CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0} Stok Kartı Bulunamadı"));
                            Mouse.OverrideCursor = null;
                            return;
                        }
                        planabaglaColl.Add(item);
                    }
                }
                if (planSecerekBagla)
                {
                    if (planlar == null)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Plan Seçerek Bağla Seçili Olmasına Rağmen Seçilen Plan Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (planlar.Count == 0)
                    {
                        CRUDmessages.GeneralFailureMessageCustomMessage("Plan Seçerek Bağla Seçili Olmasına Rağmen Seçilen Plan Bulunamadı");
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }

                Variables.ResultInt_ = satinAlma.UpdateTalep(planabaglaColl, planlar, siradanPlanBagla, planSecerekBagla);

                switch (Variables.ResultInt_)
                {
                    case 1:
                        {
                            CRUDmessages.UpdateSuccessMessage("Talep", planabaglaColl.Count); break;
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

                listele();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Kayıt İşlemi Esnasında");
                Mouse.OverrideCursor = null;
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
        private void btn_secilenleri_guncelle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_SatinAlma> guncellenecekColl = new();
                Mouse.OverrideCursor = Cursors.Wait;
                foreach(Cls_SatinAlma item in dg_TalepGuncelle.Items)
                {
                    if (item.IsChecked)
                        guncellenecekColl.Add(item);
                }
                if(guncellenecekColl.Count<1)
                {
                    CRUDmessages.NoSelection();
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.ResultInt_ = satinAlma.UpdateTalep(guncellenecekColl, planlar, false, false);
                
                switch (Variables.ResultInt_)
                {
                    case 1:
                        {
                            CRUDmessages.UpdateSuccessMessage("Talep", guncellenecekColl.Count); break;
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

                listele();
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Güncelleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_sil_clicked (object sender, RoutedEventArgs e)
        {
            try
            {
                if(dg_TalepGuncelle.Items.Count<1)
                {
                    CRUDmessages.GeneralFailureMessage("Talep Listesi Bulunamadı.");
                    return;
                }
                Variables.Counter_ = 0;
                Variables.ErrorMessage_ = string.Empty;
                Mouse.OverrideCursor = Cursors.Wait;
                foreach(Cls_SatinAlma item in dg_TalepGuncelle.Items)
                {
                    if(item.IsChecked)
                    {
                        Variables.Result_ = arge.IfStokKoduExists(item.StokKodu);
                        if(!Variables.Result_)
                        {
                            Variables.ErrorMessage_ += string.Format("Stok Kartı Bulunamadı.({0}) \n ", item.StokKodu);
                            continue;
                        }

                        Variables.Result_ = satinAlma.DeleteTalepSatir(item.TalepNumarasi,item.TalepSira);
                        if(!Variables.Result_)
                        {
                            Variables.ErrorMessage_ += string.Format("Talep Kapama İşlemi Esnasında Hata İle Karşılaşıldı.({0},{1}) \n ", item.TalepNumarasi, item.TalepSira);
                            continue;
                        }
                        Variables.Counter_++;
                    }
                }
                if(!string.IsNullOrEmpty(Variables.ErrorMessage_))
                { 
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (Variables.Counter_ < 1)
                { 
                    CRUDmessages.GeneralFailureMessageCustomMessage("Kapatılacak Talep Bulunamadı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                else 
                    CRUDmessages.UpdateSuccessMessage("Talep", Variables.Counter_);


                listele();

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Talep Kapama İşlemi Esnasında");
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
                foreach(Cls_SatinAlma item in dg_TalepGuncelle.Items)
                {
                    if(item.IsChecked)
                    {
                        Variables.Result_ = satinAlma.UpdateTaleptenPlanKaldir(item.TalepNumarasi,item.TalepSira);
                        if (!Variables.Result_)
                            Variables.ErrorMessage_ += string.Format("Plan Bilgileri Kaldırılırken Hata İle Karşılaşıldı {0}-{1} \n", item.TalepNumarasi, item.TalepSira);
                        Variables.Counter_++;
                    }
                }
                if(Variables.Counter_==0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.NoSelection();
                    return;
                }
               if(!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    return;
                }

                //yeniden listele
                ObservableCollection<Cls_SatinAlma> firstColl = satinAlma.PopulateTalepReportList(constraints, 1, kapaliTalepleriGosterme, siparislesenleriGosterme, teslimEdilenleriGosterme);

                SiparisVermeCollection = firstColl;

                if (SiparisVermeCollection == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Talepler Listelenirken");
                    dg_TalepGuncelle.ItemsSource = null;
                    dg_TalepGuncelle.Visibility = Visibility.Collapsed;
                    stc_buttons.Visibility = Visibility.Collapsed;
                    return;
                }
                if (SiparisVermeCollection.Count() == 0)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.QueryIsEmpty();
                    dg_TalepGuncelle.ItemsSource = SiparisVermeCollection;
                    txt_result.Visibility = Visibility.Collapsed;
                    dg_TalepGuncelle.Visibility = Visibility.Collapsed;
                    stc_buttons.Visibility = Visibility.Collapsed;
                    return;
                }
                dg_TalepGuncelle.ItemsSource = SiparisVermeCollection;

                dg_TalepGuncelle.Visibility = Visibility.Visible;
                stc_buttons.Visibility = Visibility.Visible;
                dg_TalepGuncelle.Items.Refresh();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Talep(ler) Kaldırılırken");
                Mouse.OverrideCursor = null;
            }
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
                    foreach (Cls_Planlama item in frm.AktarilacakPlanlar)
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
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_SatinAlma item in dg_TalepGuncelle.Items)
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

                DataGridCellInfo cellInfo = dg_TalepGuncelle.CurrentCell;

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
    }
}

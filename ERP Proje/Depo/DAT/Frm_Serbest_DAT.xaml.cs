using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
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

namespace Layer_UI.Depo.DAT
{
    /// <summary>
    /// Interaction logic for Frm_Serbest_DAT.xaml
    /// </summary>
    public partial class Frm_Serbest_DAT : Window
    {
        
        Cls_Depo depo= new Cls_Depo();
        ObservableCollection<Cls_Depo> depoColl = new();
        string kod1 = string.Empty;
        public Frm_Serbest_DAT()
        {
            InitializeComponent();
            cbx_kod1.ItemsSource = depo.GetDistinctKod1();
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
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Mouse.OverrideCursor = Cursors.Wait;
                if(cbx_kod1.SelectedItem == null)
                    kod1 = string.Empty;
                else
                    kod1 = cbx_kod1.SelectedItem.ToString();

                DATKaydetViewModelSerbest dat = new(txt_ham_kodu.Text,kod1, 1);

                if (dat.DepoColl == null)
                { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (dat.DepoColl.Count == 0)
                { CRUDmessages.QueryIsEmpty("DAT Kayıtları"); Mouse.OverrideCursor = null; return; }
                depoColl = dat.DepoColl;
                dg_dat_liste.ItemsSource = depoColl;
                dg_dat_liste.DataContext = dat;

                totalResult = depo.CountSerbestDATList(txt_ham_kodu.Text,kod1, 1);

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("DAT Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
            }

        }
        private void btn_stok_karti_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Stok_Karti_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    txt_ham_kodu.Text = frm.SelectedStokKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Kartı Rehberi Açılırken");
            }
        }

        private void btn_dat_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Variables.ErrorMessage_ = string.Empty;
            ObservableCollection<Cls_Depo> datCollection = new();
            Variables.Counter_ = 0;
            Variables.QumulativeSum_ = 0;
            foreach (Cls_Depo d in dg_dat_liste.Items)
            {
                if (d.IsChecked)
                {
                    Variables.QumulativeSum_ += Convert.ToSingle(d.GonderilecekDATMiktar);
                    if (string.IsNullOrEmpty(d.StokKodu))
                        Variables.ErrorMessage_ += 
                             " Stok Kodu Boş Olamaz.\n";

                    if (d.CikisDepoKodu == d.GirisDepoKodu)
                    {
                        Variables.ErrorMessage_ +=  
                             d.StokKodu + " Giriş ve Çıkış Depo Kodları Aynı Olamaz.\n";
                    }

                    if (d.CikisDepoBakiye - d.GonderilecekDATMiktar < 0)
                    {
                        Variables.ErrorMessage_ +=  
                            d.StokKodu + " Gönderilecek Miktar Depo Bakiyesinden Büyük Olamaz.\n";
                    }
                    Variables.Counter_++;
                    datCollection.Add(d);
                }
            }
            if (!string.IsNullOrEmpty(Variables.ErrorMessage_))
            {
                datCollection.Clear();
                CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                Mouse.OverrideCursor = null;
                return;
            }
            Variables.FormResult_ = CRUDmessages.InsertOnayMessage();
            if (Variables.FormResult_ == false)
            {
                Mouse.OverrideCursor = null;
                return;
            }
            string fisno = depo.GetFisnoForDAT();
            Variables.ResultInt_ = depo.InsertDATSerbest(datCollection, fisno);
            if (Variables.ResultInt_ == 3 ||
                Variables.ResultInt_ == 2 ||
                Variables.ResultInt_ == -1)
            {
                KayitGeriAl(fisno);
                CRUDmessages.GeneralFailureMessage("DAT Kaydedilirken");
                Mouse.OverrideCursor = null;
                return;
            }

            CRUDmessages.InsertSuccessMessage("Depo", datCollection.Count);
            Mouse.OverrideCursor = null;
            Frm_Serbest_DAT frm = new Frm_Serbest_DAT();
            frm.Show();
            this.Close();

        }
        private void KayitGeriAl(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return;

                Variables.Result_ = depo.DATGeriAl(fisno);
                if (Variables.Result_)
                { CRUDmessages.GeneralFailureMessageCustomMessage("DAT İle Alakalı Kayıtlar Geri Alındı."); return; };
                if (!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("DAT İle Alakalı Kayıtlar Geri Alınırken Problem İle Karşılaşıldı.\n" +
                                                                   "Veri Bütünlüğü Bozulmuş Olabilir.\n Bilgi İşlem Personeline Bilgi Veriniz.");
                    return;
                };
            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("DAT İle Alakalı Kayıtlar Geri Alınırken Problem İle Karşılaşıldı.\n" +
                                                                   "Veri Bütünlüğü Bozulmuş Olabilir.\n Bilgi İşlem Personeline Bilgi Veriniz.");
            }
        }
        int totalResult = 0;
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                lastOffset = e.NewValue;
                if (lastOffset > pageValueChanged + 100 && totalResult > depoColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Depo> moreDATCollection = new();
                    if (cbx_kod1.SelectedItem == null)
                        kod1 = string.Empty;
                    else
                        kod1 = cbx_kod1.SelectedItem.ToString();
                    moreDATCollection = depo.PopulateSerbestDATList(txt_ham_kodu.Text.ToString(),kod1, pageNumber);
                    if (moreDATCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreDATCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastDepoCollection = new ObservableCollection<Cls_Depo>
                                        (depoColl.Union(moreDATCollection).ToList());
                        dg_dat_liste.ItemsSource = lastDepoCollection;
                        dg_dat_liste.Items.Refresh();
                        depoColl = lastDepoCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastDepoCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_dat_liste, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }

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
                if (e.Delta < 0)
                    lastOffset += e.Delta * -1 / 48;
                else
                    lastOffset -= e.Delta / 48;

                if (lastOffset > pageValueChanged + 100 && totalResult > depoColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Depo> moreDepoCollection = new();
                    if (cbx_kod1.SelectedItem == null)
                        kod1 = string.Empty;
                    else
                        kod1 = cbx_kod1.SelectedItem.ToString();
                    moreDepoCollection = depo.PopulateSerbestDATList(txt_ham_kodu.Text.ToString(),kod1, pageNumber);
                    if (moreDepoCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreDepoCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Depo> lastDepoCollection = new ObservableCollection<Cls_Depo>
                                        (depoColl.Union(moreDepoCollection).ToList());
                        dg_dat_liste.ItemsSource = lastDepoCollection;
                        dg_dat_liste.Items.Refresh();
                        depoColl = lastDepoCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastDepoCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_dat_liste, 0) as Decorator;
                        if (border != null)
                        {
                            double center = 0;
                            var scrollViewer = border.Child as ScrollViewer;
                            if (scrollViewer != null)
                            {
                                center = scrollViewer.ScrollableHeight / 2.0;
                                scrollViewer.ScrollToVerticalOffset(center);
                            }
                            lastOffset = center;
                        }
                    }

                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
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
                            if (dataGrid.Columns[i].Header.ToString() == "Gönd Mik")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        // Select the "Miktar" column of the row
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
        private void ToggleHeaderCheckBox(object sender, RoutedEventArgs e)
        {
            bool headerIsChecked = ((CheckBox)sender).IsChecked ?? false;
            foreach (Cls_Depo item in dg_dat_liste.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
    }
}

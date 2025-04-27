using Layer_2_Common.Type;
using Layer_Business;
using Layer_Business.ViewModels;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Depo.DAT
{
    /// <summary>
    /// Interaction logic for Frm_DAT_Kaydet.xaml
    /// </summary>
    public partial class Frm_DAT_Kaydet : Window
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
        public Frm_DAT_Kaydet()
        {
            InitializeComponent(); Window_Loaded();

            cbx_kod1.ItemsSource = depo.GetDistinctKod1();
            cbx_kod5.ItemsSource = depo.GetDistinctKod5();
        }
        Cls_Depo depo = new();
        private ObservableCollection<Cls_Depo> depoCollection = new();
        private void btn_dat_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                listele();
            }

            catch { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private void listele()
        {
            variables.ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(txt_takip_no.Text) &&
                string.IsNullOrEmpty(txt_ham_kodu.Text) &&
                string.IsNullOrEmpty(txt_ham_adi.Text))
            { CRUDmessages.NoInput(); return; }

            dg_dat_liste.ItemsSource = null;
            dg_dat_liste.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            Dictionary<string, string> constraints = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(txt_takip_no.Text))
                constraints.Add("takipNo", txt_takip_no.Text);
            else
                constraints.Add("takipNo", null);

            if (!string.IsNullOrWhiteSpace(txt_ham_kodu.Text))
                constraints.Add("hamKodu", txt_ham_kodu.Text);
            else
                constraints.Add("hamKodu", null);

            if (!string.IsNullOrWhiteSpace(txt_ham_adi.Text))
                constraints.Add("hamAdi", txt_ham_adi.Text);
            else
                constraints.Add("hamAdi", null);

            if (cbx_kod1.SelectedItem != null)
                constraints.Add("kod1", cbx_kod1.SelectedItem.ToString());
            else
                constraints.Add("kod1", null);

            if (cbx_kod5.SelectedItem != null)
                constraints.Add("kod5", cbx_kod5.SelectedItem.ToString());
            else
                constraints.Add("kod5", null);

            DATKaydetViewModel model = new(constraints);
            if (model.DepoColl == null)
            { CRUDmessages.GeneralFailureMessage("DAT Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
            if (model.DepoColl.Count == 0)
            { CRUDmessages.QueryIsEmpty("DAT Kayıtları"); Mouse.OverrideCursor = null; return; }

            dg_dat_liste.ItemsSource = model.DepoColl;
            dg_dat_liste.DataContext = model;
            Mouse.OverrideCursor = null;
        }
        Variables variables = new Variables();
        private void btn_dat_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            var result = CRUDmessages.InsertOnayMessage();
            if (!result)
                return;

            Mouse.OverrideCursor = Cursors.Wait;
            variables.ErrorMessage = string.Empty;
            variables.WarningMessage = string.Empty;
            variables.SuccessMessage = string.Empty;

            ObservableCollection<Cls_Depo> datCollection = new();
            variables.Counter = 0;
            variables.QumulativeSum = 0;

            foreach (Cls_Depo d in dg_dat_liste.Items)
            {
                if (d.IsChecked)
                {
                    variables.QumulativeSum += Convert.ToSingle(d.GonderilecekDATMiktar);
                    if (string.IsNullOrEmpty(d.TakipNo))
                        variables.ErrorMessage = variables.ErrorMessage +
                             d.StokKodu + "Takip No Boş Olamaz.";
                    if (string.IsNullOrEmpty(d.StokKodu))
                        variables.ErrorMessage = variables.ErrorMessage +
                             d.TakipNo + "Stok Kodu Boş Olamaz.";

                    if (d.CikisDepoKodu == d.GirisDepoKodu)
                    {
                        variables.ErrorMessage = variables.ErrorMessage +
                             d.StokKodu + "Giriş ve Çıkış Depo Kodları Aynı Olamaz.";
                    }

                    if (d.CikisDepoBakiye - d.GonderilecekDATMiktar < 0)
                    {
                        variables.ErrorMessage = variables.ErrorMessage +
                            d.StokKodu + "Gönderilecek Miktar Depo Bakiyesinden Büyük Olamaz.";
                    }
                    if (d.GonderilecekDATMiktar > d.KalanDATMiktar * 1.1)
                        variables.WarningMessage += $"{d.StokKodu} DAT Yapılacak Miktar Kalan Miktarın %10'undan fazla.\n";
                    
                    variables.Counter++;
                    datCollection.Add(d);
                }
            }

            if (!string.IsNullOrEmpty(variables.ErrorMessage))
            {
                datCollection.Clear();
                CRUDmessages.GeneralFailureMessageCustomMessage(variables.ErrorMessage);
                Mouse.OverrideCursor = null;
                return;
            }
            if (!string.IsNullOrEmpty(variables.WarningMessage))
            {
                Variables.Result_ = CRUDmessages.DoYouWishToContinue(Variables.WarningMessage_);
                if(!Variables.Result_)
                    return;
            }

            string fisno = depo.GetFisnoForDAT();
            if(string.IsNullOrEmpty(fisno))
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessageCustomMessage("Fişno Alınamadı");
                return;
            }
            variables.ResultInt = depo.InsertDAT(datCollection, fisno);
            if (variables.ResultInt == 3 ||
                variables.ResultInt == 2 ||
                variables.ResultInt == -1)
            {
                KayitGeriAl(fisno);
                CRUDmessages.GeneralFailureMessage("DAT Kaydedilirken");
                Mouse.OverrideCursor = null;
                return;
            }

            CRUDmessages.InsertSuccessMessage("Depo", variables.Counter);
            Mouse.OverrideCursor = null;
            Frm_DAT_Kaydet frm = new Frm_DAT_Kaydet();
            frm.Show();
            this.Close();

        }
        private void KayitGeriAl(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return;

                variables.Result = depo.DATGeriAl(fisno);
                if (variables.Result)
                { CRUDmessages.GeneralFailureMessageCustomMessage("DAT İle Alakalı Kayıtlar Geri Alındı."); return; };
                if (!variables.Result)
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
        public void btn_detailed_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Cls_Depo item = UIinteractions.GetDataItemFromButton<Cls_Depo>(sender);
                ObservableCollection<Cls_Depo> datDetayCollection = depo.PopulateDatDetay(item.TakipNo, item.StokKodu);
                if (datDetayCollection == null)

                { CRUDmessages.GeneralFailureMessage("Hammadde Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                if (datDetayCollection.Count() == 0)
                { CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null; return; }

                Frm_DAT_Detay _popup = new(datDetayCollection);
                Variables.FormResult_ = _popup.ShowDialog();
                if (!Variables.Result_)
                {
                    listele();
                    Mouse.OverrideCursor = null;
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Detay Bilgileri Listelenirken");
            }
        }
        private void datagrid_stok_kodu_clicked(object sender, MouseButtonEventArgs e)
        {
            try
            {

                TextBlock text = sender as TextBlock;
                if (text == null)
                    return;
                Cls_Depo depo = text.DataContext as Cls_Depo;
                if (depo == null) return;

                Frm_Stok_Karti_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    depo.StokKodu = frm.SelectedStokKodu;
                    Cls_Arge arge = new();
                    depo.StokAdi = arge.GetStokAdi(frm.SelectedStokKodu);
                    depo.Kod1 = arge.GetKod1(frm.SelectedStokKodu);
                    depo.Kod5 = arge.GetKod5(frm.SelectedStokKodu);
                    depo.CikisDepoKodu = depo.GetDepoKodu(frm.SelectedStokKodu);
                    depo.GirisDepoKodu = depo.CikisDepoKodu + 5;
                    depo.CikisDepoBakiye = Convert.ToSingle(depo.GetDepoBakiye(frm.SelectedStokKodu, depo.CikisDepoKodu));
                    depo.GirisDepoBakiye = Convert.ToSingle(depo.GetDepoBakiye(frm.SelectedStokKodu, depo.GirisDepoKodu));
                    depo.GonderilecekDATMiktar = depo.ToplamDATIhtiyacMiktar;
                    depo.KalanDATMiktar = 0;
                    Mouse.OverrideCursor = null;
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Kodu Değiştirilirken");
                Mouse.OverrideCursor = null;
            }
        }
        private void CikisDepo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;
                if (combo == null) return;

                if (combo.SelectedItem == null) return;
                Cls_Depo depo = combo.DataContext as Cls_Depo;
                if (depo == null) return;

                depo.CikisDepoBakiye = Convert.ToSingle(depo.GetDepoBakiye(depo.StokKodu, Convert.ToInt32(combo.SelectedItem)));




            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Depo Kodu Değiştirilirken");
            }
        }
        private void GirisDepo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox combo = sender as ComboBox;
                if (combo == null) return;

                if (combo.SelectedItem == null) return;
                Cls_Depo depo = combo.DataContext as Cls_Depo;
                if (depo == null) return;

                depo.GirisDepoBakiye = Convert.ToSingle(depo.GetDepoBakiye(depo.StokKodu, Convert.ToInt32(combo.SelectedItem)));


            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Depo Kodu Değiştirilirken");
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
                            if (dataGrid.Columns[i].Header.ToString() == "Gönderilecek Mik")
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

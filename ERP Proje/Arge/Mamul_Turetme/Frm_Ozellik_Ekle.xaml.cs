using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Frm_Ozellik_Ekle.xaml
    /// </summary>
    public partial class Frm_Ozellik_Ekle : Window
    {

        Cls_Urun urun = new();
        private ObservableCollection<Cls_Urun> urunCollection = new();
        string secilenUrunOzellikTipi = string.Empty;
        Variables variables = new Variables();
        public Frm_Ozellik_Ekle()
        {
            InitializeComponent(); Window_Loaded();
        }
        public Frm_Ozellik_Ekle(string eskiFormdanGelenSecilenOzellikTipi)
        {
            InitializeComponent(); Window_Loaded();

            dg_urun_grubu_liste.ItemsSource = null;
            dg_urun_grubu_liste.Items.Clear();
            dg_model_liste.ItemsSource = null;
            dg_model_liste.Items.Clear();
            dg_satis_sekil_liste.ItemsSource = null;
            dg_satis_sekil_liste.Items.Clear();

            secilenUrunOzellikTipi = eskiFormdanGelenSecilenOzellikTipi;

            Mouse.OverrideCursor = Cursors.Wait;

            foreach (ComboBoxItem item in cbx_ozellik_secim.Items)
            {
                if (item.Content.ToString() == secilenUrunOzellikTipi.ToString())
                { cbx_ozellik_secim.SelectedItem = item; break; }
            }

            urunCollection = urun.PopulateOzellikListe(secilenUrunOzellikTipi);

            if (urunCollection == null)
            { CRUDmessages.GeneralFailureMessage("Ürün Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
            if (urunCollection.Count == 0)
            { CRUDmessages.QueryIsEmpty("Ürün Kayıtları"); Mouse.OverrideCursor = null; return; }

            if (secilenUrunOzellikTipi == "Ürün Grup")
            {
                dg_urun_grubu_liste.ItemsSource = urunCollection;
                dg_urun_grubu_liste.Visibility = Visibility.Visible;
                dg_model_liste.Visibility = Visibility.Collapsed;
                dg_satis_sekil_liste.Visibility = Visibility.Collapsed;

            }
            if (secilenUrunOzellikTipi == "Model")
            {
                dg_model_liste.ItemsSource = urunCollection;
                dg_model_liste.Visibility = Visibility.Visible;
                dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                dg_satis_sekil_liste.Visibility = Visibility.Collapsed;
            }
            if (secilenUrunOzellikTipi == "Satış Şekil")
            {
                dg_satis_sekil_liste.ItemsSource = urunCollection;
                dg_satis_sekil_liste.Visibility = Visibility.Visible;
                dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                dg_model_liste.Visibility = Visibility.Collapsed;
            }

            Mouse.OverrideCursor = null;
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.ErrorMessage = string.Empty;

                if (cbx_ozellik_secim.SelectedItem == null)
                { CRUDmessages.NoInput(); return; }

                dg_urun_grubu_liste.ItemsSource = null;
                dg_urun_grubu_liste.Items.Clear();
                dg_model_liste.ItemsSource = null;
                dg_model_liste.Items.Clear();
                dg_satis_sekil_liste.ItemsSource = null;
                dg_satis_sekil_liste.Items.Clear();

                Mouse.OverrideCursor = Cursors.Wait;

                ComboBoxItem selectedItem = cbx_ozellik_secim.SelectedItem as ComboBoxItem;

                secilenUrunOzellikTipi = selectedItem.Content.ToString();

                urunCollection = urun.PopulateOzellikListe(secilenUrunOzellikTipi);

                if (urunCollection == null)
                { CRUDmessages.GeneralFailureMessage("Ürün Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (urunCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("Ürün Kayıtları"); Mouse.OverrideCursor = null; return; }

                if (secilenUrunOzellikTipi == "Ürün Grup")
                {
                    dg_urun_grubu_liste.ItemsSource = urunCollection;
                    dg_urun_grubu_liste.Visibility = Visibility.Visible;
                    dg_model_liste.Visibility = Visibility.Collapsed;
                    dg_satis_sekil_liste.Visibility = Visibility.Collapsed;

                }
                if (secilenUrunOzellikTipi == "Model")
                {
                    dg_model_liste.ItemsSource = urunCollection;
                    dg_model_liste.Visibility = Visibility.Visible;
                    dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                    dg_satis_sekil_liste.Visibility = Visibility.Collapsed;
                }
                if (secilenUrunOzellikTipi == "Satış Şekil")
                {
                    dg_satis_sekil_liste.ItemsSource = urunCollection;
                    dg_satis_sekil_liste.Visibility = Visibility.Visible;
                    dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                    dg_model_liste.Visibility = Visibility.Collapsed;
                }

                stc_filter.Visibility = Visibility.Visible;
                Mouse.OverrideCursor = null;
            }

            catch { CRUDmessages.GeneralFailureMessage("Özellik Kayıtları Listelenirken"); Mouse.OverrideCursor = null; }
        }
        private void btn_yeni_ekle_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(secilenUrunOzellikTipi))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Tip Seçiniz"); return; }

                Mouse.OverrideCursor = Cursors.Wait;

                Popup_Ozellik_Ekle popup = new Popup_Ozellik_Ekle(secilenUrunOzellikTipi);
                popup.ShowDialog();


            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
            }
        }
        private void btn_guncelle(object sender, RoutedEventArgs e)
        {
            try
            {
                Cls_Urun dataItem = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender);

                if (dataItem == null)
                { CRUDmessages.GeneralFailureMessage("Ürün Özelliği Seçilirken"); return; }

                if (secilenUrunOzellikTipi == "Ürün Grup")
                {
                    if (string.IsNullOrEmpty(dataItem.UrunGrubuIsim))
                    { CRUDmessages.GeneralFailureMessage("İsim Boş Olamaz"); return; }

                }
                if (secilenUrunOzellikTipi == "Model")
                {
                    if (string.IsNullOrEmpty(dataItem.ModelIsim))
                    { CRUDmessages.GeneralFailureMessage("İsim Boş Olamaz"); return; }

                }
                if (secilenUrunOzellikTipi == "Satış Şekil")
                {
                    if (string.IsNullOrEmpty(dataItem.SatisSekilIsim))
                    { CRUDmessages.GeneralFailureMessage("İsim Boş Olamaz"); return; }

                }

                Mouse.OverrideCursor = Cursors.Wait;

                variables.Result = urun.UpdateUrunOzellik(dataItem, secilenUrunOzellikTipi);
                if (!variables.Result)
                { CRUDmessages.GeneralFailureMessage("Güncelleme Yapılırken"); Mouse.OverrideCursor = null; return; }

                CRUDmessages.UpdateSuccessMessage("Ürün", 1);
                Frm_Ozellik_Ekle frm = new(secilenUrunOzellikTipi);
                frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme Yapılırken"); Mouse.OverrideCursor = null;
            }
        }
        private void btn_ozellik_sil(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(secilenUrunOzellikTipi))
                { CRUDmessages.GeneralFailureMessageCustomMessage("Tip Seçiniz"); return; }

                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Urun silinecekUrunTipi = UIinteractions.GetDataItemFromButton<Cls_Urun>(sender as Button);
                if (silinecekUrunTipi == null)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Ürün Tipi Seçilirken");
                    return;
                }

                Variables.Result_ = urun.DeleteOzellik(silinecekUrunTipi, secilenUrunOzellikTipi);
                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    return;
                }
                Mouse.OverrideCursor = null;
                CRUDmessages.DeleteSuccessMessage("Ürün", 1);
                Frm_Ozellik_Ekle frm = new();
                frm.Show();
                this.Close();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Tipi Silinirken");
                Mouse.OverrideCursor = null;
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

        private void NewOzellikSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox comboBoxSelectionHasChanged = sender as ComboBox;
                if (comboBoxSelectionHasChanged == null)
                    return;
                ComboBoxItem selectedItem = comboBoxSelectionHasChanged.SelectedItem as ComboBoxItem;
                if (selectedItem == null)
                    return;
                secilenUrunOzellikTipi = selectedItem.Content.ToString();
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Seçim Yapılırken");
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

        private void btn_filter_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(secilenUrunOzellikTipi))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Özellik Tipi Seçiniz");
                    return;
                }
                if (string.IsNullOrEmpty(txt_filter.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                dg_urun_grubu_liste.ItemsSource = null;
                dg_urun_grubu_liste.Items.Clear();
                dg_model_liste.ItemsSource = null;
                dg_model_liste.Items.Clear();
                dg_satis_sekil_liste.ItemsSource = null;
                dg_satis_sekil_liste.Items.Clear();

                Mouse.OverrideCursor = Cursors.Wait;


                urunCollection = urun.PopulateOzellikListe(secilenUrunOzellikTipi,txt_filter.Text);

                if (urunCollection == null)
                { CRUDmessages.GeneralFailureMessage("Ürün Kayıtları Listelenirken"); Mouse.OverrideCursor = null; return; }
                if (urunCollection.Count == 0)
                { CRUDmessages.QueryIsEmpty("Ürün Kayıtları"); Mouse.OverrideCursor = null; return; }

                if (secilenUrunOzellikTipi == "Ürün Grup")
                {
                    dg_urun_grubu_liste.ItemsSource = urunCollection;
                    dg_urun_grubu_liste.Visibility = Visibility.Visible;
                    dg_model_liste.Visibility = Visibility.Collapsed;
                    dg_satis_sekil_liste.Visibility = Visibility.Collapsed;

                }
                if (secilenUrunOzellikTipi == "Model")
                {
                    dg_model_liste.ItemsSource = urunCollection;
                    dg_model_liste.Visibility = Visibility.Visible;
                    dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                    dg_satis_sekil_liste.Visibility = Visibility.Collapsed;
                }
                if (secilenUrunOzellikTipi == "Satış Şekil")
                {
                    dg_satis_sekil_liste.ItemsSource = urunCollection;
                    dg_satis_sekil_liste.Visibility = Visibility.Visible;
                    dg_urun_grubu_liste.Visibility = Visibility.Collapsed;
                    dg_model_liste.Visibility = Visibility.Collapsed;
                }

                Mouse.OverrideCursor = null;
            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
    }
}

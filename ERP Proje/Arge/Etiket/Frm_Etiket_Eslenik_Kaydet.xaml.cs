using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Arge.Etiket
{
    /// <summary>
    /// Interaction logic for Frm_Etiket_Eslenik_Kaydet.xaml
    /// </summary>
    public partial class Frm_Etiket_Eslenik_Kaydet : Window
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
        public Frm_Etiket_Eslenik_Kaydet()
        {
            InitializeComponent(); Window_Loaded();
            txt_cari_kodu.Text = "120AA10054";
        }

        ExcelMethodsEPP excel = new();
        ObservableCollection<Cls_Etiket> excelCollection = new();
        Cls_Etiket etiket = new();
        private void btn_cari_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Popup_Cari_Secim_Single_Musteri_Siparis frm = new();
                var formResult = frm.ShowDialog();
                if (formResult == true)
                    txt_cari_kodu.Text = frm.CariKodu;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Cari Bilgileri Alınırken");
            }
        }

        private void btn_excel_getir(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Dosyaları|*.xlsm;*.xlsx",
                    Title = "Excel Seç"
                };

                DataTable dataTable = new DataTable();
                if (openFileDialog.ShowDialog() == true)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    string filePath = openFileDialog.FileName;
                    dataTable = excel.ReadExcelFile(filePath, 0, 3, 16, 1);
                }
                else return;

                if (excelCollection != null)
                    excelCollection.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Cls_Etiket etiket = new Cls_Etiket
                    {
                        CariStokKodu = Convert.IsDBNull(row["Component Code (Article Description)"]) ? null : row["Component Code (Article Description)"].ToString(),
                        CariStokIsim = (Convert.IsDBNull(row["Description"]) ? "" : row["Description"].ToString()) + " " + (Convert.IsDBNull(row["Pack"]) ? "" : row["Pack"].ToString()),
                        SetCode = Convert.IsDBNull(row["Set Code"]) ? null : row["Set Code"].ToString(),
                        Model = Convert.IsDBNull(row["Model"]) ? null : row["Model"].ToString(),
                        Renk = Convert.IsDBNull(row["Color"]) ? null : row["Color"].ToString(),
                        EANcode = Convert.IsDBNull(row["EAN Code"]) ? null : row["EAN Code"].ToString(),
                        PaketKodu = Convert.IsDBNull(row["Pack"]) ? null : row["Pack"].ToString()
                    };

                    if (!Convert.IsDBNull(row["Carton Qty"]) && int.TryParse(Convert.ToString(row["Carton Qty"]), out int koliMiktarValue))
                    {
                        etiket.KoliMiktar = koliMiktarValue;
                    }
                    else
                    {
                        // Set default value or handle error as needed
                        etiket.KoliMiktar = 0;
                    }
                    if (!Convert.IsDBNull(row["GW"]) && float.TryParse(Convert.ToString(row["GW"]), out float gw))
                    {
                        etiket.BrutAgirlik = gw;
                    }
                    else
                    {
                        // Set default value or handle error as needed
                        etiket.BrutAgirlik = 0;
                    }
                    if (!Convert.IsDBNull(row["Stack"]) && float.TryParse(Convert.ToString(row["Stack"]), out float st))
                    {
                        etiket.Stack = st;
                    }
                    else
                    {
                        // Set default value or handle error as needed
                        etiket.Stack = 0;
                    }




                    excelCollection.Add(etiket);
                }

                dg_EtiketEslenik.ItemsSource = excelCollection;

                txt_pageResult.Text = "Toplam " + dg_EtiketEslenik.Items.Count + " adet kod listeleniyor.";
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralSuccessMessage("Aktarım İşlemi");

            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(ex.Message.ToString());
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_stok_kodu_bul_clicked(object sender, EventArgs e)
        {
            try
            {


                Cls_Etiket item = UIinteractions.GetDataItemFromButton<Cls_Etiket>(sender);
                Frm_Stok_Karti_Rehberi frm = new();
                var result = frm.ShowDialog();
                if (result == true)
                    item.StokKodu = frm.SelectedStokKodu;


                dg_EtiketEslenik.Items.Refresh();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Kodu Bilgileri Alınırken");
            }
        }
        private void btn_eslenik_sil(object sender, RoutedEventArgs e)
        {
            Variables.Result_ = CRUDmessages.DeleteOnayMessage();

            try
            {
                if (Variables.Result_)
                {
                    Variables.ErrorMessage_ = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Button? button = sender as Button;
                    if (button == null) { CRUDmessages.GeneralFailureMessage("Excel Bilgileri Alınırken"); return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(button);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Excel Bilgileri Alınırken"); return; }

                    Cls_Etiket dataItem = row.Item as Cls_Etiket;

                    excelCollection.Remove(dataItem);

                    dg_EtiketEslenik.ItemsSource = excelCollection;

                    dg_EtiketEslenik.Items.Refresh();

                    Mouse.OverrideCursor = null;
                }
                else return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }
        private void btn_eslenik_kaydet_clicked(object sender, EventArgs e)
        {
            try
            {
                if (dg_EtiketEslenik.ItemsSource == null)
                {
                    CRUDmessages.GeneralFailureMessage("Tablo Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if (dg_EtiketEslenik.Items.Count == 0)
                {
                    CRUDmessages.GeneralFailureMessage("Tablo Boş Olamaz");
                    Mouse.OverrideCursor = null;
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;



                foreach (Cls_Etiket item in dg_EtiketEslenik.Items)
                {

                    if (string.IsNullOrEmpty(item.StokKodu))
                    {
                        Variables.ResultString_ = "Stok Kodu Boş Olamaz...";
                        item.InsertStatus = Variables.ResultString_;
                        dg_EtiketEslenik.Items.Refresh();
                        continue;
                    }
                    if (string.IsNullOrEmpty(item.CariStokKodu))
                    {
                        Variables.ResultString_ = "Cari Stok Kodu Boş Olamaz...";
                        item.InsertStatus = Variables.ResultString_;
                        dg_EtiketEslenik.Items.Refresh();
                        continue;
                    }

                    Variables.ResultString_ = etiket.InsertEslenikKaydet(item, txt_cari_kodu.Text);
                    item.InsertStatus = Variables.ResultString_;
                    dg_EtiketEslenik.Items.Refresh();
                }

                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralMessage("İşlem Tamamlandı");
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Bilgileri Kaydedilirken");
            }
        }
        private void btn_refresh(object sender, EventArgs e)
        {
            if (dg_EtiketEslenik.ItemsSource != null)
                dg_EtiketEslenik.ItemsSource = null;
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

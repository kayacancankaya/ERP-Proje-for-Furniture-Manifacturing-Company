using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Satis
{
    /// <summary>
    /// Interaction logic for frm_main_window.xaml
    /// </summary>
    public partial class frm_main_window : Window
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

        PlanDurum planDurum = new();

        DateValidation dateValidation = new();

        Variables variables = new();

        LoginLogic login = new();

        Dictionary<string, string> filterDic = new();
        string filterText = string.Empty;
        string filterHeader = string.Empty;

        string updateQuery = string.Empty, strMsg = string.Empty;
        int counter = 0, SevkMiktar = 0;
        bool dateCheck;
        MessageBoxResult resultMsg;

        string queryCount = string.Empty, query = string.Empty;
        public frm_main_window()
        {
            InitializeComponent(); Window_Loaded();

            DataContext = planDurum;

            login.Departman = login.GetDepartment();

            try
            {

                dg_Plan_Durum.ItemsSource = null;
                dg_Plan_Durum.Items.Clear();

                Mouse.OverrideCursor = Cursors.Wait;
                if (cbx_siparis_durum.IsChecked == true)
                {
                    query = "select * from vbvPlanDurum where SiparisDurum<>'KAPALI' order by CONVERT(DATE, SiparisTarih, 104) DESC offset 0 rows fetch next 25 rows only";
                    queryCount = "select count(*) from vbvPlanDurum where SiparisDurum<>'KAPALI'";
                }
                else
                {
                    query = "select * from vbvPlanDurum  order by CONVERT(DATE, SiparisTarih, 104) DESC offset 0 rows fetch next 25 rows only";
                    queryCount = "select count(*) from vbvPlanDurum";

                }


                planDurum.PopulateMainWindow(query);
                dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;

                planDurum.UpdatePageNumber(queryCount);
                grd_page_numbers.DataContext = planDurum;
                grd_selected_number.DataContext = planDurum;
                grd_Total_Numbers_Count.DataContext = planDurum;
                Mouse.OverrideCursor = null;

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
            }
        }

        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //    if (sender is DataGrid dataGrid)
        //    {
        //        ContextMenu contextMenu = dataGrid.Resources["dgr_satis"] as ContextMenu;
        //        dataGrid.ContextMenu = contextMenu;
        //        if (contextMenu != null &&
        //            (login.Departman == "Satis" ||
        //             login.Departman == "Bilgi Islem" ||
        //             login.Departman.Contains("Planlama")))
        //        {
        //            //cCari.Visibility=Visibility.Visible;
        //            //cPO.Visibility = Visibility.Visible;
        //            //cDes.Visibility = Visibility.Visible;
        //            //cSip.Visibility = Visibility.Visible;
        //            //cTarih.Visibility = Visibility.Visible;
        //        }
        //    }
        //}
        private void cg_dg_header_checked(object sender, RoutedEventArgs e)
        {
            bool isChecked = ((CheckBox)sender).IsChecked ?? false;

            foreach (var item in dg_Plan_Durum.Items)
            {
                var row = dg_Plan_Durum.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null)
                    {
                        checkBox.IsChecked = isChecked;
                    }
                }
            }

        }
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                    return (T)child;

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }
        private void cmb_selected_record_changed(object sender, SelectionChangedEventArgs e)
        {

            Mouse.OverrideCursor = Cursors.Wait;
            dg_Plan_Durum.ItemsSource = null;
            dg_Plan_Durum.Items.Clear();


            if (((ComboBox)sender).SelectedItem is ComboBoxItem selectedItem && selectedItem.Content != null)
            {
                planDurum.SelectedRecord = Convert.ToInt32(selectedItem.Content);
            }
            planDurum.DisplayedRowSelectionChanged(cbx_siparis_durum.IsChecked);
            dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;


            Mouse.OverrideCursor = null;
        }
        private void btn_paging_clicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string buttonName = button.Name;

            Mouse.OverrideCursor = Cursors.Wait;
            dg_Plan_Durum.ItemsSource = null;
            dg_Plan_Durum.Items.Clear();

            planDurum.PlanDurumCollection = planDurum.PagingButtonsClicked(buttonName, cbx_siparis_durum.IsChecked);

            grd_page_numbers.DataContext = planDurum;
            grd_next_buttons.DataContext = planDurum;
            grd_prev_buttons.DataContext = planDurum;

            dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;


            Mouse.OverrideCursor = null;

        }
        private void btn_search_clicked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            dg_Plan_Durum.ItemsSource = null;
            dg_Plan_Durum.Items.Clear();

            string searchBox1 = txt_search_box1.Text;
            string searchBox2 = txt_search_box2.Text;
            string searchBox3 = txt_search_box3.Text;
            string searchBox4 = txt_search_box4.Text;

            planDurum.SearchBoxChanged(filterDic, searchBox1, searchBox2, searchBox3, searchBox4, cbx_siparis_durum.IsChecked); 
            dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;
            grd_page_numbers.DataContext = planDurum;
            grd_next_buttons.DataContext = planDurum;
            grd_prev_buttons.DataContext = planDurum;

            Mouse.OverrideCursor = null;
        }
        private void btn_cari_guncelle_click(object sender, RoutedEventArgs e)
        {

            updateQuery = string.Empty;
            strMsg = string.Empty;
            counter = 0;
            SevkMiktar = 0;
            MessageBoxResult resultMsg;


            foreach (PlanDurum item in dg_Plan_Durum.Items)
            {

                if (item.IsChecked)
                {
                    if (Convert.ToInt32(item.SevkMiktar) > 0)
                    {
                        strMsg = strMsg + item.SiparisNo + item.SiparisSira + item.UrunKodu + "sevk gözüküyor.\n";
                    }
                    else
                    {

                        updateQuery = updateQuery + $" update tblsipatra set sthar_carikod='{item.CariKod}' where fisno='{item.SiparisNo}' update tblsipamas set cari_kod2='{item.CariKod}' where fatirs_no='{item.SiparisNo}' ";
                    }

                    counter++;
                }

            }

            if (strMsg != string.Empty)
            {
                MessageBox.Show(strMsg + "Güncelleme iptal edildi.");
            }
            else
            {
                resultMsg = MessageBox.Show(counter + " Adet Güncelleme Yapılacak Onaylıyor Musunuz?", "Onay", MessageBoxButton.YesNo);

                if (resultMsg == MessageBoxResult.Yes)
                {
                    planDurum.DataGridContextMenuUpdate(updateQuery, "Sipariş", counter);
                }
                else
                {
                    MessageBox.Show("Güncelleme İptal Edildi.");
                }
            }

        }
        private void btn_po_guncelle_click(object sender, RoutedEventArgs e)
        {

            updateQuery = string.Empty;
            strMsg = string.Empty;
            counter = 0;
            MessageBoxResult resultMsg;

            foreach (PlanDurum item in dg_Plan_Durum.Items)
            {

                if (item.IsChecked)
                {
                    updateQuery = updateQuery + $" update tblfatuek set acik12='{item.PO}' where fatirsno='{item.SiparisNo}'";

                    counter++;
                }

            }

            if (strMsg != string.Empty)
            {
                MessageBox.Show(strMsg + "Güncelleme iptal edildi.");
            }
            else
            {
                resultMsg = MessageBox.Show(counter + " Adet Güncelleme Yapılacak Onaylıyor Musunuz?", "Onay", MessageBoxButton.YesNo);

                if (resultMsg == MessageBoxResult.Yes)
                {
                    planDurum.DataGridContextMenuUpdate(updateQuery, "Fatura", counter);
                }
                else
                {
                    MessageBox.Show("Güncelleme İptal Edildi.");
                }
            }

        }
        private void btn_download_excel(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\plan\\{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Plan";

                filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);

                //şablon header kısmı
                excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 14);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 39, 1);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 18);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 14);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 7, 14);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 8, 14);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 9, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 10, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 11, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 12, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 13, 9);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 14, 18);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 15, 18);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 17, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 18, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 19, 10);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:AL2", "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:AL3", "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Plan", "Calibri", 13, "#ffffff", true);

                int rowCount = dg_Plan_Durum.Items.Count;
                int columnCount = dg_Plan_Durum.Columns.Count;
                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 44);


                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 44);
                int i = 6;
                while (i < rowCount + 6)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 24);
                    i++;
                }
                excelWorks.TextWrap(existingPackage, sheetName, "B5:AL5", true);
                DataTable dataToExport = GetDataFromDataGrid(dg_Plan_Durum);

                excelWorks.ExportDataToExcel(dataToExport, existingPackage, sheetName, 5, 2);
                excelWorks.TextWrap(existingPackage, sheetName, "B5:AL5", true);

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B5:AL5", "#333F4F", rowCount, 5, columnCount, 2, "#D9D9D9", "#ffffff", "Plan");

                Mouse.OverrideCursor = null;

                MessageBox.Show("Plan Excele Aktarıldı.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null;
            }

        }
        private static DataTable GetDataFromDataGrid(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Sip No");
            dataTable.Columns.Add("Sip Sıra");
            dataTable.Columns.Add("Sip Tarih");
            dataTable.Columns.Add("Sip Durum");
            dataTable.Columns.Add("Sat C Kod");
            dataTable.Columns.Add("Sat C İsim");
            dataTable.Columns.Add("Tes C Kod");
            dataTable.Columns.Add("Tes C İsim");
            dataTable.Columns.Add("Destinasyon");
            dataTable.Columns.Add("PO");
            dataTable.Columns.Add("Plan No");
            dataTable.Columns.Add("Takip No");
            dataTable.Columns.Add("Ref İe");
            dataTable.Columns.Add("Ürün Kodu");
            dataTable.Columns.Add("Durum");
            dataTable.Columns.Add("Termin Tarih");
            dataTable.Columns.Add("Planlanan Tarih");
            dataTable.Columns.Add("Aciklama");
            dataTable.Columns.Add("Urun Miktar");
            dataTable.Columns.Add("Paket Adet");
            dataTable.Columns.Add("Toplam Paket");
            dataTable.Columns.Add("Kalan Paket");
            dataTable.Columns.Add("Mamul Stok");
            dataTable.Columns.Add("Sevk Miktar");
            dataTable.Columns.Add("Paket Stok");
            dataTable.Columns.Add("Doseme Ie Mik");
            dataTable.Columns.Add("Doseme Urs Mik");
            dataTable.Columns.Add("Konfeksiyon Ie Mik");
            dataTable.Columns.Add("Konfeksiyon Urs Mik");
            dataTable.Columns.Add("Kumas Ie Mik");
            dataTable.Columns.Add("Kumas Urs Mik");
            dataTable.Columns.Add("Plaka Ie Mik");
            dataTable.Columns.Add("Plaka Urs Mik");
            dataTable.Columns.Add("Iskelet Ie Mik");
            dataTable.Columns.Add("Iskelet Urs Mik");
            dataTable.Columns.Add("KBandi Ie Mik");
            dataTable.Columns.Add("KBandi Urs Mik");

            foreach (var item in dataGrid.Items)
            {
                var planItem = item as PlanDurum;
                if (planItem != null)
                {
                    var dataRow = dataTable.NewRow();

                    dataRow["Sip No"] = planItem.SiparisNo;
                    dataRow["Sip Sıra"] = planItem.SiparisSira;
                    dataRow["Sip Tarih"] = planItem.SiparisTarih;
                    dataRow["Sip Durum"] = planItem.SiparisDurum;
                    dataRow["Sat C Kod"] = planItem.SatisCariKod;
                    dataRow["Sat C İsim"] = planItem.SatisCariIsim;
                    dataRow["Tes C Kod"] = planItem.CariKod;
                    dataRow["Tes C İsim"] = planItem.CariIsim;
                    dataRow["Destinasyon"] = planItem.Destinasyon;
                    dataRow["PO"] = planItem.PO;
                    dataRow["Plan No"] = planItem.PlanNo;
                    dataRow["Takip No"] = planItem.TakipNo;
                    dataRow["Ref İe"] = planItem.ReferansIsemri;
                    dataRow["Ürün Kodu"] = planItem.UrunKodu;
                    dataRow["Durum"] = planItem.Durum;
                    dataRow["Termin Tarih"] = planItem.IhtiyacDuyulanTarih;
                    dataRow["Planlanan Tarih"] = planItem.PlanlananTarih;
                    dataRow["Aciklama"] = planItem.PlanlamaAciklama;
                    dataRow["Urun Miktar"] = planItem.UrunMiktar;
                    dataRow["Paket Adet"] = planItem.PaketAdet;
                    dataRow["Toplam Paket"] = planItem.ToplamPaket;
                    dataRow["Kalan Paket"] = planItem.KalanPaket;
                    dataRow["Mamul Stok"] = planItem.MamulStok;
                    dataRow["Sevk Miktar"] = planItem.SevkMiktar;
                    dataRow["Paket Stok"] = planItem.PaketStok;
                    dataRow["Doseme Ie Mik"] = planItem.DosemeIeMik;
                    dataRow["Doseme Urs Mik"] = planItem.DosemeUrsMik;
                    dataRow["Konfeksiyon Ie Mik"] = planItem.KonfeksiyonIeMik;
                    dataRow["Konfeksiyon Urs Mik"] = planItem.KonfeksiyonUrsMik;
                    dataRow["Kumas Ie Mik"] = planItem.KumasIeMik;
                    dataRow["Kumas Urs Mik"] = planItem.KumasUrsMik;
                    dataRow["Plaka Ie Mik"] = planItem.PlakaIeMik;
                    dataRow["Plaka Urs Mik"] = planItem.PlakaUrsMik;
                    dataRow["Iskelet Ie Mik"] = planItem.IskeletIeMik;
                    dataRow["Iskelet Urs Mik"] = planItem.IskeletUrsMik;
                    dataRow["KBandi Ie Mik"] = planItem.KBandiIeMik;
                    dataRow["KBandi Urs Mik"] = planItem.KBandiUrsMik;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void btn_destinasyon_guncelle_click(object sender, RoutedEventArgs e)
        {

            updateQuery = string.Empty;
            strMsg = string.Empty;
            counter = 0;
            MessageBoxResult resultMsg;

            foreach (PlanDurum item in dg_Plan_Durum.Items)
            {

                if (item.IsChecked)
                {
                    updateQuery = updateQuery + $" update tblsipamas set aciklama='{item.Destinasyon}' where FATIRS_NO='{item.SiparisNo}'";

                    counter++;
                }

            }

            if (strMsg != string.Empty)
            {
                MessageBox.Show(strMsg + "Güncelleme iptal edildi.");
            }
            else
            {
                resultMsg = MessageBox.Show(counter + " Adet Güncelleme Yapılacak Onaylıyor Musunuz?", "Onay", MessageBoxButton.YesNo);

                if (resultMsg == MessageBoxResult.Yes)
                {
                    planDurum.DataGridContextMenuUpdate(updateQuery, "Sipariş", counter);
                }
                else
                {
                    MessageBox.Show("Güncelleme İptal Edildi.");
                }
            }

        }
        private void btn_siparis_durum_degistir_click(object sender, RoutedEventArgs e)
        {

            updateQuery = string.Empty;
            strMsg = string.Empty;
            counter = 0;
            MessageBoxResult resultMsg;

            foreach (PlanDurum item in dg_Plan_Durum.Items)
            {

                if (item.IsChecked)
                {
                    if (item.SiparisDurum == "K" || item.SiparisDurum == "H")
                    {
                        updateQuery = updateQuery + $" update tblsipatra set sthar_htur='{item.SiparisDurum}' where fisno='{item.SiparisNo}' and stra_sipkont='{item.SiparisSira}'";

                        counter++;
                    }
                    else
                    {
                        strMsg = strMsg + item.SiparisNo + item.SiparisDurum + " Sipariş Durumu 'K' veya 'H' olabilir.";
                    }

                }
            }

            if (strMsg != string.Empty)
            {
                MessageBox.Show(strMsg + "Güncelleme iptal edildi.");
            }
            else
            {
                resultMsg = MessageBox.Show(counter + " Adet Güncelleme Yapılacak Onaylıyor Musunuz?", "Onay", MessageBoxButton.YesNo);

                if (resultMsg == MessageBoxResult.Yes)
                {
                    planDurum.DataGridContextMenuUpdate(updateQuery, "Sipariş", counter);
                }
                else
                {
                    MessageBox.Show("Güncelleme İptal Edildi.");
                }
            }
        }
        private void btn_ihtiyac_duyulan_tarih_degistir_click(object sender, RoutedEventArgs e)
        {

            updateQuery = string.Empty;
            strMsg = string.Empty;
            counter = 0;
            MessageBoxResult resultMsg;

            string terminTarih = string.Empty;

            foreach (PlanDurum item in dg_Plan_Durum.Items)
            {

                if (item.IsChecked)
                {

                    terminTarih = item.IhtiyacDuyulanTarih.ToString();
                    dateCheck = dateValidation.CheckDate(terminTarih);

                    if (dateCheck)
                    {

                        terminTarih = dateValidation.ConverttoDateString(terminTarih);
                        updateQuery = updateQuery + $" update kktTerminTarih set IhtiyacDuyulanTarih='{item.IhtiyacDuyulanTarih}' where SiparisNo='{item.SiparisNo}' and SiparisSira='{item.SiparisSira}'";

                        counter++;

                    }

                }
            }

            if (strMsg != string.Empty)
            {
                System.Windows.MessageBox.Show(strMsg + "Güncelleme iptal edildi.");
            }
            else
            {
                resultMsg = System.Windows.MessageBox.Show(counter + " Adet Güncelleme Yapılacak Onaylıyor Musunuz?", "Onay", MessageBoxButton.YesNo);

                if (resultMsg == MessageBoxResult.Yes)
                {
                    planDurum.DataGridContextMenuUpdate(updateQuery, "Sipariş", counter);
                }
                else
                {
                    System.Windows.MessageBox.Show("Güncelleme İptal Edildi.");
                }
            }
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switch(button.Tag.ToString())
                {

                    case "Sipariş No":
                        txt_header_sip_no.Text = button.Tag.ToString();
                        FilterPopupSipNo.IsOpen = true;
                        break;
                    case "Sip S.":
                        txt_header_sip_sira.Text = button.Tag.ToString();
                        FilterPopupSipSira.IsOpen = true;
                        break;
                    case "Sat C Kod":
                        txt_header_sat_cari.Text = button.Tag.ToString();
                        FilterPopupSatCari.IsOpen = true;
                        break;
                    case "Sat C Isim":
                        txt_header_sat_cari_isim.Text = button.Tag.ToString();
                        FilterPopupSatCariIsim.IsOpen = true;
                        break;
                    case "Tes C Kod":
                        txt_header_tes_cari.Text = button.Tag.ToString();
                        FilterPopupTesCari.IsOpen = true;
                        break;
                    case "Tes C Isim":
                        txt_header_tes_cari_isim.Text = button.Tag.ToString();
                        FilterPopupTesCariIsim.IsOpen = true;
                        break;
                    case "Destinasyon":
                        txt_header_destinasyon.Text = button.Tag.ToString();
                        FilterPopupDestinasyon.IsOpen = true;
                        break;
                    case "PO":
                        txt_header_PO.Text = button.Tag.ToString();
                        FilterPopupPO.IsOpen = true;
                        break;
                    case "Plan Adi":
                        txt_header_plan_adi.Text = button.Tag.ToString();
                        FilterPopupPlanAdi.IsOpen = true;
                        break;
                    case "Plan No":
                        txt_header_plan_no.Text = button.Tag.ToString();
                        FilterPopupPlanNo.IsOpen = true;
                        break;
                    case "Takip No":
                        txt_header_takip_no.Text = button.Tag.ToString();
                        FilterPopupTakipNo.IsOpen = true;
                        break;
                    case "Ref Ie":
                        txt_header_ref_ie.Text = button.Tag.ToString();
                        FilterPopupRefIe.IsOpen = true;
                        break;
                    case "Ürün Kodu":
                        txt_header_urun_kodu.Text = button.Tag.ToString();
                        FilterPopupUrunKodu.IsOpen = true;
                        break;
                    case "Ürün Adı":
                        txt_header_urun_adi.Text = button.Tag.ToString();
                        FilterPopupUrunAdi.IsOpen = true;
                        break;
                    case "Durum":
                        txt_header_durum.Text = button.Tag.ToString();
                        FilterPopupDurum.IsOpen = true;
                        break;
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
            }
        }
        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    TextBox textBox = sender as TextBox;
                    switch (textBox.Name.ToString())
                    {

                        case "FilterTextBox_sip_no":
                            filterText = FilterTextBox_sip_no.Text;
                            filterHeader = txt_header_sip_no.Text;
                            FilterPopupSipNo.IsOpen = false;
                            break;
                        case "FilterTextBox_sip_sira":
                            filterText = FilterTextBox_sip_sira.Text;
                            filterHeader = txt_header_sip_sira.Text;
                            FilterPopupSipSira.IsOpen = false;
                            break;
                        case "FilterTextBox_sat_cari":
                            filterText = FilterTextBox_sat_cari.Text;
                            filterHeader = txt_header_sat_cari.Text;
                            FilterPopupSatCari.IsOpen = false;
                            break;
                        case "FilterTextBox_sat_cari_isim":
                            filterText = FilterTextBox_sat_cari_isim.Text;
                            filterHeader = txt_header_sat_cari_isim.Text;
                            FilterPopupSatCariIsim.IsOpen = false;
                            break;
                        case "FilterTextBox_tes_cari":
                            filterText = FilterTextBox_tes_cari.Text;
                            filterHeader = txt_header_tes_cari.Text;
                            FilterPopupTesCari.IsOpen = false;
                            break;
                        case "FilterTextBox_tes_cari_isim":
                            filterText = FilterTextBox_tes_cari_isim.Text;
                            filterHeader = txt_header_tes_cari_isim.Text;
                            FilterPopupTesCariIsim.IsOpen = false;
                            break;
                        case "FilterTextBox_destinasyon":
                            filterText = FilterTextBox_destinasyon.Text;
                            filterHeader = txt_header_destinasyon.Text;
                            FilterPopupDestinasyon.IsOpen = false;
                            break;
                        case "FilterTextBox_PO":
                            filterText = FilterTextBox_PO.Text;
                            filterHeader = txt_header_PO.Text;
                            FilterPopupPO.IsOpen = false;
                            break;
                        case "FilterTextBox_plan_adi":
                            filterText = FilterTextBox_plan_adi.Text;
                            filterHeader = txt_header_plan_adi.Text;
                            FilterPopupPlanAdi.IsOpen = false;
                            break;
                        case "FilterTextBox_plan_no":
                            filterText = FilterTextBox_plan_no.Text;
                            filterHeader = txt_header_plan_no.Text;
                            FilterPopupPlanNo.IsOpen = false;
                            break;
                        case "FilterTextBox_takip_no":
                            filterText = FilterTextBox_takip_no.Text;
                            filterHeader = txt_header_takip_no.Text;
                            FilterPopupTakipNo.IsOpen = false;
                            break;
                        case "FilterTextBox_ref_ie":
                            filterText = FilterTextBox_ref_ie.Text;
                            filterHeader = txt_header_ref_ie.Text;
                            FilterPopupRefIe.IsOpen = false;
                            break;
                        case "FilterTextBox_urun_kodu":
                            filterText = FilterTextBox_urun_kodu.Text;
                            filterHeader = txt_header_urun_kodu.Text;
                            FilterPopupUrunKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_urun_adi":
                            filterText = FilterTextBox_urun_adi.Text;
                            filterHeader = txt_header_urun_adi.Text;
                            FilterPopupUrunAdi.IsOpen = false;
                            break;
                        case "FilterTextBox_durum":
                            filterText = FilterTextBox_durum.Text;
                            filterHeader = txt_header_durum.Text;
                            FilterPopupDurum.IsOpen = false;
                            break;
                        ;
                    }


                    if (!string.IsNullOrEmpty(filterText))
                    {
                        //daha önceden filtre varsa filtreyi değiştir
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic[filterHeader] = filterText;
                        //yoksa ekle
                        else
                            filterDic.Add(filterHeader, filterText);

                    }
                    else
                    {
                        //eğer boş bir şekilde entera vurulmuşsa ve daha önceden filtre varsa filtreyi kaldır
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic.Remove(filterHeader);
                        //daha önce bir şey yoksa sorgulamadan direk çık
                        else
                            return;
                    }
                    dg_Plan_Durum.ItemsSource = null;
                    dg_Plan_Durum.Items.Clear();

                    string searchBox1 = txt_search_box1.Text;
                    string searchBox2 = txt_search_box2.Text;
                    string searchBox3 = txt_search_box3.Text;
                    string searchBox4 = txt_search_box4.Text;

                    planDurum.SearchBoxChanged(filterDic, searchBox1, searchBox2, searchBox3, searchBox4, cbx_siparis_durum.IsChecked);
                    dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;
                    grd_page_numbers.DataContext = planDurum;
                    grd_next_buttons.DataContext = planDurum;
                    grd_prev_buttons.DataContext = planDurum;

                    Mouse.OverrideCursor = null;

                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_clear_filter_clicked(object sender, EventArgs e)
        {
            try
            {
                FilterTextBox_durum.Text = string.Empty;
                FilterTextBox_destinasyon.Text = string.Empty;
                FilterTextBox_plan_adi.Text = string.Empty;
                FilterTextBox_plan_no.Text = string.Empty;
                FilterTextBox_PO.Text = string.Empty;
                FilterTextBox_ref_ie.Text = string.Empty;
                FilterTextBox_sat_cari.Text = string.Empty;
                FilterTextBox_sat_cari_isim.Text = string.Empty;
                FilterTextBox_sip_no.Text = string.Empty;
                FilterTextBox_sip_sira.Text = string.Empty;
                FilterTextBox_takip_no.Text = string.Empty;
                FilterTextBox_tes_cari.Text = string.Empty;
                FilterTextBox_tes_cari_isim.Text = string.Empty;
                FilterTextBox_urun_adi.Text= string.Empty;
                FilterTextBox_urun_kodu.Text = string.Empty;
                if (filterDic != null)
                {
                    if (filterDic.Count > 0)
                        filterDic.Clear();

                    dg_Plan_Durum.ItemsSource = null;
                    dg_Plan_Durum.Items.Clear();

                    string searchBox1 = txt_search_box1.Text;
                    string searchBox2 = txt_search_box2.Text;
                    string searchBox3 = txt_search_box3.Text;
                    string searchBox4 = txt_search_box4.Text;

                    planDurum.SearchBoxChanged(filterDic, searchBox1, searchBox2, searchBox3, searchBox4, cbx_siparis_durum.IsChecked);
                    dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;
                    grd_page_numbers.DataContext = planDurum;
                    grd_next_buttons.DataContext = planDurum;
                    grd_prev_buttons.DataContext = planDurum;

                    Mouse.OverrideCursor = null;
                }
                else
                {
                    Dictionary<string, string> emptyFilterDic = new();
                    dg_Plan_Durum.ItemsSource = null;
                    dg_Plan_Durum.Items.Clear();

                    string searchBox1 = txt_search_box1.Text;
                    string searchBox2 = txt_search_box2.Text;
                    string searchBox3 = txt_search_box3.Text;
                    string searchBox4 = txt_search_box4.Text;

                    planDurum.SearchBoxChanged(emptyFilterDic, searchBox1, searchBox2, searchBox3, searchBox4, cbx_siparis_durum.IsChecked);
                    dg_Plan_Durum.ItemsSource = planDurum.PlanDurumCollection;
                    grd_page_numbers.DataContext = planDurum;
                    grd_next_buttons.DataContext = planDurum;
                    grd_prev_buttons.DataContext = planDurum;

                }

            }
            catch (Exception)
            {

                CRUDmessages.GeneralFailureMessage("Filtreleri Kaldırırken"); 
            }
        }
        

    }
}

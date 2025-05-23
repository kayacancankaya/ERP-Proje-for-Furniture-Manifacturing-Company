﻿using Layer_2_Common.Excels;
using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.Planlama_Moduler.Simulasyon.Popups;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.Ahsap.Planlama.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Simulasyon_Ahsap_Plan.xaml
    /// </summary>
    public partial class Popup_Simulasyon_Ahsap_Plan : Window
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
        public ObservableCollection<Cls_Planlama> planRenkDurumCollection { get; set; }
        public ObservableCollection<Cls_Planlama> planAdiCollection { get; set; }
        public Popup_Simulasyon_Ahsap_Plan()
        {
            try
            {
                InitializeComponent(); Window_Loaded();

                ObservableCollection<Cls_Planlama> planAdiCollection = new();
                ObservableCollection<Cls_Planlama> planRenkDurumCollection = new();
                planAdiCollection = plan.GetDistinctPlanAdiForSimulation("Ahsap Plan");
                if (planAdiCollection == null)
                {
                    CRUDmessages.QueryIsEmpty("Plan Adı");
                    return;
                }
                planRenkDurumCollection = plan.GetPlanRenkDurum(planAdiCollection);
                if (planRenkDurumCollection == null)
                {
                    CRUDmessages.QueryIsEmpty("Plan Adı");
                    return;
                }
                dg_Plan_Sunta.ItemsSource = planRenkDurumCollection;
                dg_Plan_Sunta.Items.Refresh();
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Simülasyon Sonuç Formu Açılırken");
            }

        }
        Cls_Planlama plan = new();
        private void plan_adi_detay_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Planlama dataItem = UIinteractions.GetDataItemFromButton<Cls_Planlama>(sender);

                Cls_Planlama? planItem = new Cls_Planlama
                {
                    PlanAdi = dataItem.PlanAdi,
                };

                ObservableCollection<Cls_Planlama> planAdiDetayCollection = plan.GetPlanAdiDetayWithOnlyPlanAdi(planItem, "Ahsap Plan");

                Popup_Plan_Adi_Detay _frm = new(planAdiDetayCollection);
                var result = _frm.ShowDialog();
                if (result == false)
                {
                    ObservableCollection<Cls_Planlama> planAdiCollection = new();
                    ObservableCollection<Cls_Planlama> planRenkDurumCollection = new();
                    planAdiCollection = plan.GetDistinctPlanAdiForSimulation("Ahsap Plan");
                    if (planAdiCollection == null)
                    {
                        CRUDmessages.QueryIsEmpty("Plan Adı");
                        return;
                    }
                    planRenkDurumCollection = plan.GetPlanRenkDurum(planAdiCollection);
                    if (planRenkDurumCollection == null)
                    {
                        CRUDmessages.QueryIsEmpty("Plan Adı");
                        return;
                    }
                    dg_Plan_Sunta.ItemsSource = planRenkDurumCollection;
                    dg_Plan_Sunta.Items.Refresh();
                }



            }
            catch
            {
                CRUDmessages.GeneralFailureMessageCustomMessage("Plan Adı Detayları Getirilirken");
                Mouse.OverrideCursor = null;
            }
        }
        LoginLogic login = new();
        private async void btn_excele_aktar_clicked(object sender, RoutedEventArgs e)
        {


            await Task.Run(() =>
            {

                DataTable dataTable = plan.GetDataForExcelFromSimulatedTable("Ahsap Plan");
                int rowCount = dataTable.Rows.Count;
                int columnCount = dataTable.Columns.Count;

                if (rowCount == 0 || dataTable == null)
                { CRUDmessages.GeneralFailureMessage("Aktarılacak Veri Bulunamadı."); Mouse.OverrideCursor = null; return; }

                ExcelMethodsEPP excelWorks = new ExcelMethodsEPP();
                string filePath = string.Format("C:\\excel-c\\plan\\{0}_{1}_simulasyon_plan", login.GetUserName(), DateTime.Now.ToString("yyyyMMddHHmmss"));
                string imagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\vb.png";
                string sheetName = "Simulasyon";

                filePath = excelWorks.CreateExcelFile(filePath, sheetName);

                FileInfo fileInfo = new FileInfo(filePath);

                var existingPackage = new ExcelPackage(fileInfo);

                //şablon header kısmı
                excelWorks.SetRowHeight(existingPackage, sheetName, 1, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 2, 25);
                excelWorks.SetRowHeight(existingPackage, sheetName, 3, 19);
                excelWorks.SetRowHeight(existingPackage, sheetName, 4, 6);
                excelWorks.SetRowHeight(existingPackage, sheetName, 5, 50);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 1, 1);

                excelWorks.SetColumnWidth(existingPackage, sheetName, 2, 16);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 3, 21);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 4, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 5, 10);
                excelWorks.SetColumnWidth(existingPackage, sheetName, 6, 10);

                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "A1:XFD1000", "#E6E6E7");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B2:" + excelWorks.GetExcelColumnLetter(columnCount + 1) + 2, "#333F4F");
                excelWorks.SetCellBackgroundColor(existingPackage, sheetName, "B3:" + excelWorks.GetExcelColumnLetter(columnCount + 1) + 3, "#3B495B");

                excelWorks.WriteTextToCell(existingPackage, sheetName, "B2", "VitaBianca Ahşap", "Calibri", 13, "#ffffff", true);
                excelWorks.WriteTextToCell(existingPackage, sheetName, "B3", "Planlama-Simülasyon", "Calibri", 13, "#ffffff", true);

                excelWorks.InsertImage(existingPackage, sheetName, imagePath, "VitaBianca", columnCount + 1, 2, 54, 54);

                excelWorks.ExportDataToExcel(dataTable, existingPackage, sheetName, 6, 2);

                excelWorks.SetRowHeight(existingPackage, sheetName, 6, 38);
                excelWorks.TextWrap(existingPackage, sheetName, "B6:I" + rowCount + 6, true);

                excelWorks.CreateStyledTable(existingPackage, sheetName, "B6:" + excelWorks.GetExcelColumnLetter(columnCount + 1) + 6, "#333F4F", rowCount + 1, 6, columnCount + 1, 2, "#D9D9D9", "#ffffff", "Sunta_Simulasyon");

                int i = 7;
                decimal depoMiktar = 0, siparisMiktar = 0, talepMiktar = 0, hamIhtiyacMiktar = 0;

                while (i < rowCount + 7)
                {
                    excelWorks.SetRowHeight(existingPackage, sheetName, i, 50);

                    DataRow row = dataTable.Rows[i - 7];
                    depoMiktar = Convert.ToDecimal(row[2]);
                    siparisMiktar = Convert.ToDecimal(row[3]);
                    talepMiktar = Convert.ToDecimal(row[4]);

                    int j = 5;
                    string columnLetter = string.Empty;
                    while (j < columnCount)
                    {

                        if (row[j] == DBNull.Value || row[j] == null)
                            hamIhtiyacMiktar = 0;

                        else
                            hamIhtiyacMiktar = Convert.ToDecimal(row[j]);

                        if (depoMiktar > 0)
                        {
                            depoMiktar = depoMiktar - hamIhtiyacMiktar;
                            if (depoMiktar < 0)
                            {
                                siparisMiktar = siparisMiktar - Math.Abs(depoMiktar);
                            }
                        }
                        if (siparisMiktar > 0 && depoMiktar < 0)
                            siparisMiktar = siparisMiktar - hamIhtiyacMiktar;
                        if (siparisMiktar < 0)
                        {
                            talepMiktar = talepMiktar - Math.Abs(siparisMiktar);
                        }
                        if (talepMiktar > 0 && depoMiktar < 0 && siparisMiktar < 0)
                            talepMiktar = talepMiktar - hamIhtiyacMiktar;

                        columnLetter = excelWorks.GetExcelColumnLetter(j + 2);

                        if (depoMiktar > 0)
                        {
                            excelWorks.SetCellBackgroundColor(existingPackage, sheetName, columnLetter + i, "#00B050");
                            j++;
                            continue;
                        }
                        if (siparisMiktar > 0)
                        {
                            excelWorks.SetCellBackgroundColor(existingPackage, sheetName, columnLetter + i, "#FFFF00");
                            j++;
                            continue;
                        }
                        if (talepMiktar > 0)
                        {
                            excelWorks.SetCellBackgroundColor(existingPackage, sheetName, columnLetter + i, "#FFA500");
                            j++;
                            continue;
                        }
                        excelWorks.SetCellBackgroundColor(existingPackage, sheetName, columnLetter + i, "#FF0000");

                        j++;
                    }

                    i++;
                }

                excelWorks.ChangeNumberFormat(existingPackage, sheetName, "D" + 7 + ":" + excelWorks.GetExcelColumnLetter(columnCount + 1) + rowCount, "0,00");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Simülasyon Excele Aktarıldı.");
                });
            });
        }

    }
}

using Layer_2_Common.Type;
using Layer_Business;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PdfSharp;
using Layer_2_Common.PDF;
using System.IO;

namespace Layer_UI.InsanKaynaklari
{
    /// <summary>
    /// Interaction logic for Frm_Dogum_Gunu.xaml
    /// </summary>
    public partial class Frm_Dogum_Gunu : Window
    {
        string filterText = string.Empty;
        string filterHeader = string.Empty;
        int pageNumber = 0;
        double lastOffset = 0;
        int totalResult = 0;
        bool isPageUp = false;
        Cls_InsanKaynaklari ik = new();
        ObservableCollection<Cls_InsanKaynaklari> ikColl = new();
        Dictionary<string,string> filterDic = new Dictionary<string,string>();
        public Frm_Dogum_Gunu()
        {
            try
            {
                InitializeComponent(); Window_Loaded();
                filterDic = new();
                listele();

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Form Açılırken");
                return;
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
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switch (button.Tag.ToString())
                {

                    case "Adı":
                        txt_header_adi.Text = button.Tag.ToString();
                        FilterPopupAdi.IsOpen = true;
                        break;
                    case "Doğum Günü":
                        txt_header_tarih.Text = button.Tag.ToString();
                        FilterPopupTarih.IsOpen = true;
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
                    Filter(sender);
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {  
                Filter(sender);
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void FilterComboBox_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Filter(sender);
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
                return;
            }
        }
        private void Filter(object sender)
        {
            try
            {

                Mouse.OverrideCursor = Cursors.Wait;
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    filterText = FilterTextBox_adi.Text;
                    filterHeader = txt_header_adi.Text;
                    FilterPopupAdi.IsOpen = false;
                }
                else
                {
                    Button button = sender as Button;
                    if (button == null)
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    ComboBoxItem selectedItem = FilterComboBox_tarih.SelectedItem as ComboBoxItem;
                    if (selectedItem == null)
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    filterText = selectedItem.Tag.ToString();
                    filterHeader = txt_header_tarih.Text;
                    FilterPopupAdi.IsOpen = false;
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
                    {
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }
                dg_dg.ItemsSource = null;
                dg_dg.Items.Clear();

                listele();

                Mouse.OverrideCursor = null;

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
                Mouse.OverrideCursor = Cursors.Wait;
                FilterTextBox_adi.Text = string.Empty;
                FilterComboBox_tarih.SelectedItem = -1;

                if (filterDic != null)
                {
                    if (filterDic.Count > 0)
                        filterDic.Clear();
                }
                filterDic = new();
                listele();

                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleri Kaldırırken");
                Mouse.OverrideCursor = null;
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
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_dg, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > ikColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_InsanKaynaklari> moreMasterCollection = new();
                    moreMasterCollection = ik.PopulateDogumGunuList(filterDic, pageNumber);
                    if (moreMasterCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Kişiler Eklenirken");
                        Mouse.OverrideCursor = null;
                        if(isPageUp)
                            pageNumber--;
                        return;
                    }
                    if (moreMasterCollection.Count > 0)
                    {
                        ObservableCollection<Cls_InsanKaynaklari> lastMasterCollection = new ObservableCollection<Cls_InsanKaynaklari>
                                        (ikColl.Union(moreMasterCollection).ToList());
                        dg_dg.ItemsSource = lastMasterCollection;
                        dg_dg.Items.Refresh();
                        ikColl = lastMasterCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastMasterCollection.Count() + " adet Listeleniyor.";

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
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {

                isPageUp = false;
                var border = VisualTreeHelper.GetChild(dg_dg, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > ikColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    isPageUp = true;
                    ObservableCollection<Cls_InsanKaynaklari> moreikCollection = new();
                    moreikCollection = ik.PopulateDogumGunuList(filterDic, pageNumber);
                    if (moreikCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İrsaliyeler Eklenirken");
                        Mouse.OverrideCursor = null;
                        if(isPageUp) pageNumber--;
                        return;
                    }
                    if (moreikCollection.Count > 0)
                    {
                        ObservableCollection<Cls_InsanKaynaklari> lastikCollection = new ObservableCollection<Cls_InsanKaynaklari>
                                        (ikColl.Union(moreikCollection).ToList());
                        dg_dg.ItemsSource = lastikCollection;
                        dg_dg.Items.Refresh();
                        ikColl = lastikCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastikCollection.Count() + " adet Listeleniyor.";

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
                Mouse.OverrideCursor = null;
                if (isPageUp) pageNumber--;
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
            foreach (Cls_InsanKaynaklari item in dg_dg.Items)
            {
                item.IsChecked = headerIsChecked;
            }
        }
        private async void btn_export_to_pdf_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new PDF document
                btn_pdf.IsEnabled = false;

                if (dg_dg.Items == null)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (dg_dg.Items.Count == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }

                txt_result.Text = "PDF'e Aktarılıyor, Lütfen Bekleyiniz...";
                await Task.Run( () =>
                {


                        XBrush headerColor = new XSolidBrush(XColor.FromArgb(92, 158, 185));
                        XImage image = XImage.FromFile(Variables.ImagePath + "dogumgunu.png");
                        DateTime currentDate = DateTime.Now;
                        string filePath = System.IO.Path.Combine(Variables.AppDir, "PDFexport", "ik", currentDate.ToString("yyyyMMdd_HHmmss") + ".pdf");
                        ObservableCollection<Cls_InsanKaynaklari> ikColl = new();
                        foreach(Cls_InsanKaynaklari item in dg_dg.Items)
                        {
                            if (item.IsChecked)
                                ikColl.Add(item);
                        } // Create the PDF document
                        if(ikColl.Count == 0)
                    {
                        CRUDmessages.NoInput();
                        if (totalResult > 30)
                            txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                        else
                            txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                        return;
                    }    
                        PdfDocument pdfDoc = new PdfDocument();

                        pdfDoc.Info.Title = string.Format("{0}_{1}", "Doğum Günü Listesi", DateTime.Now);

                        foreach (Cls_InsanKaynaklari item in ikColl)
                        {

                            if (string.IsNullOrEmpty(item.Adi))
                                continue;
                            DateTime control;
                            if (!DateTime.TryParse(item.DogumGunuTarih.ToString(),out control))
                                continue;

                                PdfPage page = pdfDoc.AddPage();
                                page.Orientation = PageOrientation.Landscape;
                                XGraphics gfx = XGraphics.FromPdfPage(page);


                            gfx.DrawImage(image, 0, 0, page.Width, page.Height);
                            PDFMethods.WriteBorderedTextShrinkToFit(gfx, 421, 245, 421, 20, XColors.Transparent, 0, XBrushes.Transparent, item.Adi, "Arial", 18, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 0);

                        }

                        pdfDoc.Save(filePath);


                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Mouse.OverrideCursor = null;
                            CRUDmessages.GeneralSuccessMessage("PDF Başarılı Bir Şekilde Aktarıldı.");
                        });


                        Process.Start("explorer.exe", filePath);
                });

                btn_pdf.IsEnabled = true;
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessage("PDF'e Aktarılırken");
                btn_pdf.IsEnabled = true;
                if (totalResult > 30)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

            }
        }
        private void btn_add_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txt_ekle_adi.Text))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("İsim Boş Olamaz.");
                    return;
                }
                if(dp_ekle_tarih.SelectedDate == null)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Boş Olamaz.");
                    return;
                }
                DateTime selectedDateTime = DateTime.MinValue;
                if(!DateTime.TryParse(dp_ekle_tarih.SelectedDate.ToString(), out selectedDateTime))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Format Hatası");
                    return;
                }
                if(selectedDateTime == DateTime.MinValue)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Format Hatası");
                    return;
                }

                Variables.Result_ = CRUDmessages.InsertOnayMessage();
                if(!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = ik.InsertDogumGunu(txt_ekle_adi.Text,selectedDateTime);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.InsertSuccessMessage("Doğum Günü",1);
                Mouse.OverrideCursor = null;

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Ekleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_update_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_InsanKaynaklari> updateColl = new();
                Variables.Counter_ = 0;
                foreach (Cls_InsanKaynaklari item in dg_dg.Items)
                {
                    if(item.IsChecked)
                    {
                        updateColl.Add(item);
                        Variables.Counter_++;
                    } 

                }
                if(Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return ;
                }

                Variables.Result_ = CRUDmessages.UpdateOnayMessage();
                if (!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = ik.UpdateDogumGunu(updateColl);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.UpdateSuccessMessage("Doğum Günü",updateColl.Count);
                Mouse.OverrideCursor = null;

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Güncelleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void btn_delete_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Cls_InsanKaynaklari> deleteColl = new();
                Variables.Counter_ = 0;
                foreach (Cls_InsanKaynaklari item in dg_dg.Items)
                {
                    if(item.IsChecked)
                    {
                        deleteColl.Add(item);
                        Variables.Counter_++;
                    } 

                }
                if(Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return ;
                }

                Variables.Result_ = CRUDmessages.DeleteOnayMessage();
                if (!Variables.Result_) return;

                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = ik.DeleteDogumGunu(deleteColl);
                if(!Variables.Result_)
                {
                    CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                    Mouse.OverrideCursor = null;
                    return;
                }
                CRUDmessages.DeleteSuccessMessage("Doğum Günü",deleteColl.Count);
                Mouse.OverrideCursor = null;

            }
            catch 
            {
                CRUDmessages.GeneralFailureMessage("Silme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }

        private void listele()
        {
            ikColl = ik.PopulateDogumGunuList(filterDic, 1);
            dg_dg.ItemsSource = ikColl;
            totalResult = ik.CountDogumGunuList(filterDic);
            if (totalResult > 30)
                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
            else
                txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

            pageNumber = 1;
            lastOffset = 0;
        }

        private void btn_show_add_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                PopupEkle.IsOpen = true;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ekleme Formu Açılırken");
            }
        }
    }
}

using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Uretim
{
    /// <summary>
    /// Interaction logic for Frm_Uretim_Takip_Karti.xaml
    /// </summary>
    public partial class Frm_Uretim_Takip_Karti : Window
    {
        DateTime currentDate = DateTime.Now;
        int totalResult = 0;
        Cls_Uretim uretim = new();
        public string filePath { get; set; }
        public Frm_Uretim_Takip_Karti()
        {
            InitializeComponent(); Window_Loaded();
        }

        ObservableCollection<Cls_Uretim> uretimCollection = new();
        Dictionary<string, string> restrictionPairs = new Dictionary<string, string>();
        string queryRestrictions = string.Empty;

        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                restrictionPairs.Clear();

                if (!string.IsNullOrWhiteSpace(txt_siparis_no.Text))
                {
                    restrictionPairs.Add("@siparisNo", txt_siparis_no.Text);
                }
                if (!string.IsNullOrWhiteSpace(txt_urun_kodu.Text))
                {
                    restrictionPairs.Add("@urunKodu", txt_urun_kodu.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_takip_no.Text))
                {
                    restrictionPairs.Add("@takipno", txt_takip_no.Text);
                }

                if (!string.IsNullOrWhiteSpace(txt_plan_no.Text))
                {
                    restrictionPairs.Add("@planno", txt_plan_no.Text);
                }


                Mouse.OverrideCursor = Cursors.Wait;

                ObservableCollection<Cls_Uretim> firstColl = uretim.PopulateTakipKartiList(restrictionPairs, 1);

                uretimCollection = firstColl;

                totalResult = uretim.CountUretimTakipKartiList(restrictionPairs, 1);

                if (totalResult > 100)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 100 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                txt_result.Visibility = Visibility.Visible;

                if (uretimCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Üretim Takip Kartı Listesi Oluşturulurken"); Mouse.OverrideCursor = null; return;
                }

                if (!uretimCollection.Any())
                {
                    CRUDmessages.QueryIsEmpty(); Mouse.OverrideCursor = null;
                }

                dg_UretimTakip.ItemsSource = uretimCollection;

                lastOffset = 0;
                pageValueChanged = -50;
                pageNumber = 1;

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Üretim Takip Kartı Listesi Oluşturulurken"); Mouse.OverrideCursor = null;
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
        double lastOffset = 0;
        double pageValueChanged = -50;
        int pageNumber = 1;

        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {

                lastOffset = e.NewValue;
                if (lastOffset > pageValueChanged + 100 && totalResult > uretimCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Uretim> moreUretimCollection = new();
                    moreUretimCollection = uretim.PopulateTakipKartiList(restrictionPairs, pageNumber);
                    if (moreUretimCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreUretimCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Uretim> lastUretimCollection = new ObservableCollection<Cls_Uretim>
                                        (uretimCollection.Union(moreUretimCollection).ToList());
                        dg_UretimTakip.ItemsSource = lastUretimCollection;
                        dg_UretimTakip.Items.Refresh();
                        uretimCollection = lastUretimCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastUretimCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_UretimTakip, 0) as Decorator;
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

                if (lastOffset > pageValueChanged + 100 && totalResult > uretimCollection.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<Cls_Uretim> moreUretimCollection = new();
                    moreUretimCollection = uretim.PopulateTakipKartiList(restrictionPairs, pageNumber);
                    if (moreUretimCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave İşemirleri Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreUretimCollection.Count > 0)
                    {
                        ObservableCollection<Cls_Uretim> lastUretimCollection = new ObservableCollection<Cls_Uretim>
                                        (uretimCollection.Union(moreUretimCollection).ToList());
                        dg_UretimTakip.ItemsSource = lastUretimCollection;
                        dg_UretimTakip.Items.Refresh();
                        uretimCollection = lastUretimCollection;
                        pageValueChanged += 100;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastUretimCollection.Count() + " adet Listeleniyor.";

                        var border = VisualTreeHelper.GetChild(dg_UretimTakip, 0) as Decorator;
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
        private void btn_stok_karti_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Stok_Karti_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    txt_urun_kodu.Text = frm.SelectedStokKodu;
                    frm.Close();
                }

            }
            catch
            {

                CRUDmessages.GeneralFailureMessage("Stok Kartı Rehberi Açılırken");
            }
        }
        private async void export_to_excel_clicked(object sender, EventArgs e)
        {
            try
            {
                //await Task.Run(async () =>
                //{
                Button button = sender as Button;
                button.IsEnabled = false;
                button.Background = new SolidColorBrush(Colors.DarkGray);
                Cls_Uretim aktarilacakSiparis = UIinteractions.GetDataItemFromButton<Cls_Uretim>(sender);

                ObservableCollection<Cls_Uretim> pdfCollection = await uretim.GetUretimTakipKartiPdfCollection(aktarilacakSiparis);

                txt_result.Visibility = Visibility.Collapsed;
                txt_please_wait.Visibility = Visibility.Visible;
                filePath = string.Format("C:\\excel-c\\uretim\\{0}_{1}.pdf", "UretimTakipKarti", DateTime.Now.ToString("yyyyMMddHHmmss"));

                await CreateUretimTakipKartiPdfAsync(filePath, aktarilacakSiparis, pdfCollection);

                button.IsEnabled = true;
                button.Background = new SolidColorBrush(Colors.DarkGreen);
                txt_please_wait.Visibility = Visibility.Collapsed;
                txt_result.Visibility = Visibility.Visible;
                //});
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessage("Pdfe Aktarılırken");
                txt_result.Visibility = Visibility.Visible;
                txt_please_wait.Visibility = Visibility.Collapsed;
            }
        }
        private async Task CreateUretimTakipKartiPdfAsync(string filename, Cls_Uretim aktarilacakSiparis, ObservableCollection<Cls_Uretim> DataGridCollection)
        {
            await Task.Run(async () =>
            {

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = string.Format("ÜretimTakipKartı_{0}_{1}_{2}", aktarilacakSiparis.SiparisNumarasi, aktarilacakSiparis.SiparisSira.ToString(), aktarilacakSiparis.UrunKodu);
                string filePathProductImage = "\\\\192.168.1.11\\VitaPaketResim\\" + DataGridCollection.Select(u => u.UrunKodu).FirstOrDefault() + ".jpg";
                string filePathPartImage = string.Empty;
                Variables.Counter_ = 1;

                foreach (Cls_Uretim item in DataGridCollection)
                {

                    ObservableCollection<Cls_Uretim> hamCollection = await uretim.GetHamKoduInfoAsync(item.Isemrino);
                    // Create an empty page
                    PdfPage page = document.AddPage();

                    // Get an XGraphics object for drawing
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Create a font
                    XFont fontBold = new XFont("Arial", 11, XFontStyleEx.Bold);
                    // Create a font
                    XFont fontNormal = new XFont("Arial", 11, XFontStyleEx.Regular);

                    //headline
                    gfx.DrawString("VitaBianca", fontBold, XBrushes.Black,
                        new XRect(15, 15, page.Width, page.Height),
                        XStringFormats.TopLeft);
                    gfx.DrawString("Üretim Takip Kartı", fontBold, XBrushes.Black,
                    new XRect(-15, 15, page.Width, page.Height),
                        XStringFormats.TopRight);

                    //1.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 27, 97.5, 13, XColors.Black, 0.5, XBrushes.Gray, "PARÇA İŞEMRİ NO", "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 27, 97.5, 13, XColors.Black, 0.5, XBrushes.Gray, item.Isemrino, "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 27, 65, 13, XColors.Black, 0.5, XBrushes.Gray, "TAKİP NO", "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 27, 65, 13, XColors.Black, 0.5, XBrushes.Gray, item.Takipno, "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 340, 27, 100, 13, XColors.Black, 0.5, XBrushes.Gray, "", "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 440, 27, 140, 13, XColors.Black, 0.5, XBrushes.Gray, string.Format("REF NO {0}/{1}", Variables.Counter_, DataGridCollection.Count()), "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);

                    //2.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 41, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "ÜRÜN KODU", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 41, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, item.UrunKodu, "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 41, 65, 13, XColors.Black, 0.5, XBrushes.Transparent, "ÜRÜN ADI", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 41, 305, 13, XColors.Black, 0.5, XBrushes.Transparent, item.UrunAdi, "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);

                    //3.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 55, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "PAKET KODU", "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 55, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, item.PaketKodu, "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 55, 65, 13, XColors.Black, 0.5, XBrushes.Transparent, "PAKET ADI", "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 55, 305, 13, XColors.Black, 0.5, XBrushes.Transparent, item.PaketAdi, "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);

                    //4.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 69, 97.5, 13, XColors.Black, 0.5, XBrushes.Gray, "PARÇA KODU", "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 69, 97.5, 13, XColors.Black, 0.5, XBrushes.Gray, item.StokKodu, "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 69, 65, 13, XColors.Black, 0.5, XBrushes.Gray, "PARÇA ADI", "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 69, 305, 13, XColors.Black, 0.5, XBrushes.Gray, item.StokAdi, "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);

                    //5.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 83, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "BİRİM PAR MİK.", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 83, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, item.BirimParcaMiktar.ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 83, 65, 13, XColors.Black, 0.5, XBrushes.Transparent, "KABA BOY", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 83, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "KABA EN", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 336, 83, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "NET BOY", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 397, 83, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "NET EN", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 458, 83, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "BİTMİŞ BOY", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 519, 83, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "BİTMİŞ EN", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);

                    //6.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 97, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "İŞ EMRİ MİKTARI", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 97, 97.5, 13, XColors.Black, 0.5, XBrushes.Gray, item.IsemriMiktar.ToString(), "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 97, 65, 13, XColors.Black, 0.5, XBrushes.Transparent, item.KabaBoy.ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 97, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, item.KabaEn.ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 336, 97, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, item.NetBoy.ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 397, 97, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, item.NetEn.ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 458, 97, 61, 13, XColors.Black, 0.5, XBrushes.Gray, item.BitmisBoy.ToString(), "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 519, 97, 61, 13, XColors.Black, 0.5, XBrushes.Gray, item.BitmisEn.ToString(), "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);

                    //7.satır
                    WriteBorderedTextShrinkToFit(gfx, 15, 111, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "İŞ İSTASYONU", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 112.5, 111, 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, "OPERASYON", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 210, 111, 65, 13, XColors.Black, 0.5, XBrushes.Transparent, "OPERATÖR", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 275, 111, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "SAĞLAM ADET", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 336, 111, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "HURDA/FİRE", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 397, 111, 61, 13, XColors.Black, 0.5, XBrushes.Transparent, "KONTROL EDEN", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 2);
                    WriteBorderedTextShrinkToFit(gfx, 458, 111, 122, 13, XColors.Black, 0.5, XBrushes.Transparent, "AÇIKLAMA", "Arial", 10, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);

                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 122, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 122, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 122, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 122, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 122, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 122, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 122, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 122, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 133, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 133, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 133, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 133, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 133, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 133, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 133, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 133, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 144, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 144, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 144, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 144, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 144, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 144, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 144, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 144, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 155, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 155, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 155, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 155, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 155, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 155, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 155, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 155, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 166, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 166, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 166, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 166, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 166, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 166, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 166, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 166, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 177, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 177, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 177, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 177, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 177, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 177, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 177, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 177, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 188, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 188, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 188, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 188, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 188, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 188, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 188, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 188, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    //boş satırlar
                    CreateBorderAsync(gfx, 15, 199, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 112.5, 199, 97.5, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 210, 199, 65, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 275, 199, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 336, 199, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 397, 199, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 458, 199, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 519, 199, 61, 11, XColors.Black, 0.5, XBrushes.Transparent);

                    //reçete
                    WriteBorderedTextShrinkToFit(gfx, 15, 210, 565, 13, XColors.Black, 0.5, XBrushes.Transparent, "İŞ EMRİ REÇETE", "Arial", 11, XFontStyleEx.Bold, XParagraphAlignment.Center, XBrushes.Black, 2);
                    int satir_sayisi = 1;

                    if (hamCollection != null)
                    {
                        if (hamCollection.Count > 0)
                        {
                            DataTable dataTable = GetDataFromStokKodu(hamCollection);
                            int rowCount = dataTable.Rows.Count;
                            int columnCount = dataTable.Columns.Count;
                            for (int i = 0; i < rowCount; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            WriteBorderedTextShrinkToFit(gfx, 15, 210 + (satir_sayisi * 13), 97.5, 13, XColors.Black, 0.5, XBrushes.Transparent, dataTable.Rows[i][j].ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                                            break;
                                        case 1:
                                            WriteBorderedTextShrinkToFit(gfx, 112.5, 210 + (satir_sayisi * 13), 345.5, 13, XColors.Black, 0.5, XBrushes.Transparent, dataTable.Rows[i][j].ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                                            break;
                                        case 2:
                                            WriteBorderedTextShrinkToFit(gfx, 458, 210 + (satir_sayisi * 13), 61, 13, XColors.Black, 0.5, XBrushes.Transparent, dataTable.Rows[i][j].ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                                            break;
                                        case 3:
                                            WriteBorderedTextShrinkToFit(gfx, 519, 210 + (satir_sayisi * 13), 61, 13, XColors.Black, 0.5, XBrushes.Transparent, dataTable.Rows[i][j].ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Left, XBrushes.Black, 2);
                                            break;
                                    };
                                }
                                satir_sayisi++;
                            }
                        }
                    }
                    //kenar bandı section
                    CreateBorderAsync(gfx, 15, 631.5, 56.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 71.5, 631.5, 169.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 241, 631.5, 56.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 15, 670.6, 56.5, 117.3, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 71.5, 670.6, 169.5, 117.3, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 241, 670.6, 56.5, 117.3, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 15, 787.9, 56.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 71.5, 787.9, 169.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);
                    CreateBorderAsync(gfx, 241, 787.9, 56.5, 39.1, XColors.Black, 0.5, XBrushes.Transparent);

                    WriteBorderedTextWrap(gfx, 71.5, 631.5, 169.5, 9.775, XColors.Black, 0.5, XBrushes.Transparent, "BOY 1", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                    WriteBorderedTextWrap(gfx, 15, 670.6, 14.125, 117.3, XColors.Black, 0.5, XBrushes.Transparent, "EN 1", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                    WriteBorderedTextWrap(gfx, 283.375, 670.6, 14.125, 117.3, XColors.Black, 0.5, XBrushes.Transparent, "EN 2", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                    WriteBorderedTextWrap(gfx, 71.5, 817.225, 169.5, 9.775, XColors.Black, 0.5, XBrushes.Transparent, "BOY 2", "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);

                    ObservableCollection<Cls_Uretim> kbandColl = uretim.GetKenarBandiInfo(item.StokKodu);
                    if (kbandColl != null)
                    {
                        WriteBorderedTextWrap(gfx, 71.5, 641.275, 169.5, 29.325, XColors.Black, 0.5, XBrushes.Transparent, kbandColl.Select(k => k.BoyKenarBandi1).FirstOrDefault().ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                        WriteBorderedTextWrap(gfx, 29.125, 670.6, 42.375, 117.3, XColors.Black, 0.5, XBrushes.Transparent, kbandColl.Select(k => k.EnKenarBandi1).FirstOrDefault().ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                        WriteBorderedTextWrap(gfx, 241, 670.6, 42.375, 117.3, XColors.Black, 0.5, XBrushes.Transparent, kbandColl.Select(k => k.EnKenarBandi2).FirstOrDefault().ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                        WriteBorderedTextWrap(gfx, 71.5, 787.9, 169.5, 29.325, XColors.Black, 0.5, XBrushes.Transparent, kbandColl.Select(k => k.BoyKenarBandi2).FirstOrDefault().ToString(), "Arial", 10, XFontStyleEx.Regular, XParagraphAlignment.Center, XBrushes.Black, 0);
                    }

                    //ürün ekle
                    CreateBorderAsync(gfx, 297.5, 631.5, 282.5, 195.5, XColors.Black, 0.5, XBrushes.Transparent);
                    if (File.Exists(filePathProductImage))
                    {
                        XImage image = XImage.FromFile(filePathProductImage);
                        gfx.DrawImage(image, 300, 634, 280, 190);
                    }

                    PdfPage photoPage = document.AddPage();

                    filePathPartImage = "\\\\192.168.1.11\\VitaPaketResim\\Parca Resimleri\\" + item.StokKodu + ".pdf";

                    XGraphics gfxPhoto = XGraphics.FromPdfPage(photoPage);
                    if (File.Exists(filePathPartImage))
                    {
                        XImage image = XImage.FromFile(filePathPartImage);
                        gfxPhoto.DrawImage(image, 15, 15, 565, 812);
                    }
                }

                // Save the document
                document.Save(filename);
                Process.Start(new ProcessStartInfo
                {
                    FileName = filename,
                    UseShellExecute = true
                });
            });
        }
        private async Task CreateBorderAsync(XGraphics gfx,
                                             double left, double top, double width, double height,
                                             XColor borderColor, double borderWidth,
                                             XBrush bgColor)
        {
            XRect rect = new XRect(left, top, width, height);


            // Create a pen for the border
            XPen borderPen = new XPen(borderColor, borderWidth);

            // Draw the rectangle with bg and a border
            gfx.DrawRectangle(borderPen, bgColor, rect);

        }
        private async Task WriteBorderedTextWrap(XGraphics gfx,
                                             double left, double top, double width, double height,
                                             XColor borderColor, double borderWidth,
                                             XBrush bgColor,
                                             string content, string fontType, double fontSize, XFontStyleEx fontStyle,
                                             XParagraphAlignment textAlign, XBrush fontColor, double leftPadding)
        {
            XRect rect = new XRect(left, top, width, height);


            // Create a pen for the border
            XPen borderPen = new XPen(borderColor, borderWidth);

            // Draw the rectangle with bg and a border
            gfx.DrawRectangle(borderPen, bgColor, rect);

            rect = new XRect(rect.Left + leftPadding, rect.Top, rect.Width - leftPadding, rect.Height);

            string text = content;
            XFont font = new XFont(fontType, fontSize, fontStyle);

            // Draw the text inside the rectangle
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = textAlign;
            tf.DrawString(text, font, fontColor, rect);
        }
        private async Task WriteBorderedTextShrinkToFit(XGraphics gfx,
                                             double left, double top, double width, double height,
                                             XColor borderColor, double borderWidth,
                                             XBrush bgColor,
                                             string content, string fontType, double fontSize, XFontStyleEx fontStyle,
                                             XParagraphAlignment textAlign, XBrush fontColor, double leftPadding)
        {
            XRect rect = new XRect(left, top, width, height);


            // Create a pen for the border
            XPen borderPen = new XPen(borderColor, borderWidth);

            // Draw the rectangle with bg and a border
            gfx.DrawRectangle(borderPen, bgColor, rect);

            rect = new XRect(rect.Left + leftPadding, rect.Top, rect.Width - leftPadding, rect.Height);

            string text = content;
            XFont font = new XFont(fontType, fontSize, fontStyle);

            // Calculate the appropriate font size to fit the text within the rectangle
            AdjustFontSizeToFit(gfx, text, rect, ref font);

            // Draw the text inside the rectangle
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = textAlign;
            tf.DrawString(text, font, fontColor, rect);
        }
        private void AdjustFontSizeToFit(XGraphics gfx, string text, XRect rect, ref XFont font)
        {
            double fontSize = font.Size;
            XSize textSize = gfx.MeasureString(text, font);

            while ((textSize.Width > rect.Width || textSize.Height > rect.Height) && fontSize > 1)
            {
                fontSize -= 0.5;
                font = new XFont("Arial", fontSize, font.Style);
                textSize = gfx.MeasureString(text, font);
            }
        }
        private static DataTable GetDataFromStokKodu(ObservableCollection<Cls_Uretim> excelCollection)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Hamkodu");
            dataTable.Columns.Add("HamAdi");
            dataTable.Columns.Add("Miktar");
            dataTable.Columns.Add("Birim");

            foreach (Cls_Uretim item in excelCollection)
            {

                if (item != null)
                {

                    var dataRow = dataTable.NewRow();

                    dataRow["Hamkodu"] = item.HamKodu;
                    dataRow["HamAdi"] = item.HamAdi;
                    dataRow["Miktar"] = item.HamMiktar;
                    dataRow["Birim"] = item.Birim;

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        private void btn_detailed_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Cls_Uretim urun = UIinteractions.GetDataItemFromButton<Cls_Uretim>(sender);
                if (urun == null)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Bilgisi Listelenirken");
                    Mouse.OverrideCursor = null;
                }
                ObservableCollection<Cls_Uretim> detayColl = uretim.GetUretimTakipKartiDetayCollection(urun);
                if (detayColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Detay Bilgisi Listelenirken");
                    Mouse.OverrideCursor = null;
                }
                Frm_Uretim_Takip_Karti_Detay frm = new(detayColl);
                Variables.FormResult_ = frm.ShowDialog();

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Detay Bilgisi Listelenirken");
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
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

    }
}

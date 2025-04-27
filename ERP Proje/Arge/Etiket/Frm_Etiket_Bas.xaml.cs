using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using Layer_UI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Layer_UI.Arge.Etiket
{
    /// <summary>
    /// Interaction logic for Frm_Etiket_Bas.xaml
    /// </summary>
    public partial class Frm_Etiket_Bas : Window
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

        Cls_Etiket etiket = new();
        Cls_Etiket basilacakEtiket = new();
        public Frm_Etiket_Bas()
        {
            InitializeComponent(); Window_Loaded();
        }
        private void btn_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_stok_kodu.Text))
                {
                    CRUDmessages.NoInput();
                    return;
                }
                Variables.Result_ = UserEntryControl.StringUzunlukKontrol(txt_stok_kodu.Text, 35, true, "Stok Kodu");
                if (Variables.Result_ == false) return;
                Variables.Result_ = UserEntryControl.StringUzunlukKontrol(txt_stok_kodu.Text, 11, false, "Stok Kodu");
                if (Variables.Result_ == false) return;


                ObservableCollection<Cls_Etiket> etiketColl = etiket.GetEtiketInfo(txt_stok_kodu.Text, true);
                if (etiketColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Etiket Bilgileri Alınırken");
                    return;
                }

                if (etiketColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    return;
                }
                if (etiketColl.Count > 1)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Aratılan Stok Kodunun Birden Fazla Etiket Sonucu Var. Uygun Etiket Seçimi İçin Bilgi İşlem ile Görüşünüz.");
                    return;
                }

                basilacakEtiket = etiketColl.FirstOrDefault();

                txt_sap_code.Text = string.IsNullOrEmpty(basilacakEtiket.CariStokKodu) == true ? "" : basilacakEtiket.CariStokKodu;
                txt_sap_part.Text = string.IsNullOrEmpty(basilacakEtiket.SetCode) == true ? "" : basilacakEtiket.SetCode;
                txt_quantity.Text = string.IsNullOrEmpty(basilacakEtiket.KoliMiktar.ToString()) == true ? "" : basilacakEtiket.KoliMiktar.ToString();
                txt_stack.Text = string.IsNullOrEmpty(basilacakEtiket.Stack.ToString()) == true ? "" : basilacakEtiket.Stack.ToString();
                txt_color.Text = string.IsNullOrEmpty(basilacakEtiket.Renk) == true ? "" : basilacakEtiket.Renk.ToString();
                txt_set_code.Text = string.IsNullOrEmpty(basilacakEtiket.SetCode) == true ? "" : basilacakEtiket.SetCode;
                txt_size.Text = string.IsNullOrEmpty(basilacakEtiket.Dimensions) == true ? "" : basilacakEtiket.Dimensions;
                txt_gw.Text = string.IsNullOrEmpty(basilacakEtiket.BrutAgirlik.ToString()) == true ? "" : basilacakEtiket.BrutAgirlik.ToString();
                txt_pack_1.Text = string.IsNullOrEmpty(basilacakEtiket.PaketKodu) == true ? "" : basilacakEtiket.PaketKodu;
                txt_pack_2.Text = string.IsNullOrEmpty(basilacakEtiket.PaketKodu) == true ? "" : basilacakEtiket.PaketKodu;
                txt_po.Text = "0";

                //barkod imajını ui'a bas
                if (string.IsNullOrEmpty(basilacakEtiket.EANcode))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Barkod Kodu Boş Olduğu için Etiket Basılamıyor.");
                    return;
                }
                Variables.Result_ = UserEntryControl.StringEsitlikKontrol(basilacakEtiket.EANcode, 13, "Barkod");
                if (Variables.Result_ == false) return;

                // Generate QR code image
                Bitmap barcodeBitmap_horizontal = GenerateBarcode(basilacakEtiket.EANcode, 900, 150);
                img_horizontal.Source = ConvertBitmapToBitmapSource(barcodeBitmap_horizontal, false);

                Bitmap barcodeBitmap_vertical = GenerateBarcode(basilacakEtiket.EANcode, 900, 150);
                img_vertical.Source = ConvertBitmapToBitmapSource(barcodeBitmap_vertical, true);

                //qrCodeBitmap_vertical.Save(@"C:\excel-c\barcode_vertical.png", System.Drawing.Imaging.ImageFormat.Png);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static Bitmap GenerateBarcode(string content, int width, int height)
        {
            BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = height, // Adjust the height of the QR code as needed
                    Width = width,   // Adjust the width of the QR code as needed
                },
                Renderer = new BitmapRenderer()
            };

            // Generate QR code image
            Bitmap qrCodeBitmap = barcodeWriter.Write(content);

            return qrCodeBitmap;
        }

        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap, bool rotate_)
        {
            if (rotate_)
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);

            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        private void btn_print_clicked(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Create a visual representation of the grid
                Visual visual = CreateVisual(gridToPrint);

                // Print the visual representation
                printDialog.PrintVisual(visual, "Safat Etiket");
            }
            Frm_Etiket_Bas frm = new();
            frm.Show();
            this.Close();
        }

        private Visual CreateVisual(UIElement element)
        {
            // Measure and arrange the element to determine its size
            element.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));

            // Create a RenderTargetBitmap to render the element
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)element.RenderSize.Width,
                                                               (int)element.RenderSize.Height,
                                                               96, 96, PixelFormats.Default);
            bitmap.Render(element);

            // Create an image brush with the RenderTargetBitmap as its source
            ImageBrush imageBrush = new ImageBrush(bitmap);

            // Create a rectangle with the dimensions of the grid and apply the image brush as its background
            System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle
            {
                Width = element.RenderSize.Width,
                Height = element.RenderSize.Height,
                Fill = imageBrush
            };

            // Return the rectangle as the visual representation of the grid
            return rectangle;
        }

        private void txt_po_got_lost_focus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Set BorderThickness to 0 to make the border transparent
                textBox.BorderThickness = new Thickness(0);
            }
        }
        private void btn_stok_kodu_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_Cari_Stok_Rehberi frm = new();
                var Result = frm.ShowDialog();
                if (Result == true)
                    txt_stok_kodu.Text = frm.SelectedStokKodu;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Stok Kodu Listelenirken");
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
    }

}

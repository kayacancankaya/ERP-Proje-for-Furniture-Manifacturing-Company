using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Layer_UI.Arge.Etiket
{
    /// <summary>
    /// Interaction logic for Frm_Etiket_Bas_PO.xaml
    /// </summary>
    public partial class Frm_Etiket_Bas_PO : Window
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

        public Frm_Etiket_Bas_PO()
        {
            InitializeComponent(); Window_Loaded();
        }

        private void txt_po_numarasi_text_changed(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_po_no1.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no2.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no3.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no4.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no5.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no6.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no7.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no8.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no9.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
                txt_po_no10.Text = string.Format("PO.NO-{0}", txt_po_no.Text);
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("PO Bilgileri Aktarılırken");
            }
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
            Frm_Etiket_Bas_PO frm = new();
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
        private void btn_po_no_getir_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Frm_PO_No_Rehberi frm = new();
                var Result = frm.ShowDialog();
                if (Result == true)
                    txt_po_no.Text = frm.SelectedPoNo;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("PO No Listelenirken");
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

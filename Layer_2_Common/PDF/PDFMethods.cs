using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_2_Common.PDF
{

    public class PDFMethods
    {
        public static async Task CreateBorderAsync(XGraphics gfx,
                                     double left, double top, double width, double height,
                                     XColor borderColor, double borderWidth,
                                     XBrush bgColor)
        {
            XRect rect = new XRect(left, top, width, height);

            // Create a pen for the border
            XPen borderPen = new XPen(borderColor, borderWidth);

            // Draw the rectangle with the background color and a border
            gfx.DrawRectangle(borderPen, bgColor, rect);
        }
        public static async Task WriteBorderedTextWrap(XGraphics gfx,
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

            XFont font = new XFont(fontType, fontSize, fontStyle);

            // Draw the text inside the rectangle
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = textAlign;
            tf.DrawString(content, font, fontColor, rect);
        }
        public static async Task WriteBorderedTextShrinkToFit(XGraphics gfx,
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

            XFont font = new XFont(fontType, fontSize, fontStyle);

            // Calculate the appropriate font size to fit the text within the rectangle
            AdjustFontSizeToFit(gfx, content, rect, ref font);
            XSize textSize = gfx.MeasureString(content, font);

            // Calculate vertical offset to center the text vertically
            double verticalOffset = (rect.Height - textSize.Height) / 2;

            // Adjust the top of the rectangle to center the text vertically
            XRect adjustedRect = new XRect(rect.Left, rect.Top + verticalOffset, rect.Width, rect.Height);

            // Draw the text inside the rectangle
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = textAlign;
            tf.DrawString(content, font, fontColor, adjustedRect);
        }
        public static void AdjustFontSizeToFit(XGraphics gfx, string text, XRect rect, ref XFont font)
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
    }
}

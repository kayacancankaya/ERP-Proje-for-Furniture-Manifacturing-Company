using Layer_2_Common.Type;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Layer_UI.UserControls
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            try
            {

                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.CanResize;
                this.WindowState = WindowState.Maximized;
                this.MinWidth = 800;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                this.Background = (SolidColorBrush)Application.Current.Resources["b_r_one_color"];
                this.AllowsTransparency = true;


                var outerBorder = new Border
                {
                    CornerRadius = new CornerRadius(50),
                    BorderThickness = new Thickness(2),
                    Opacity = 0.95,
                    Background = Brushes.Transparent, // Ensure outer border background is transparent
                    Child = new Border
                    {
                        CornerRadius = new CornerRadius(50),
                        Background = new LinearGradientBrush(
                        new GradientStopCollection
                        {
                            new GradientStop((Color)ColorConverter.ConvertFromString("#00FF00"), 0),
                            new GradientStop((Color)ColorConverter.ConvertFromString("#0000FF"), 0.75)
                        }
                    ),
                        Child = new ContentPresenter
                        {
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            Content = this.Content
                        }
                    }
                };
                this.Content = outerBorder;
            }
            catch (Exception)
            {

                CRUDmessages.GeneralFailureMessage("Sayfa Yüklenirken");
            }
        }
    }
}

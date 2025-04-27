using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl_Menu.xaml
    /// </summary>
    public partial class UserControl_Menu : UserControl
    {
        public bool isFullScreen { get; set; } = false;
        private Rect originalWindowRect;
        public string Title
        {
            get { return txt_title.Text; }
            set { txt_title.Text = value; }
        }
        public Visibility IsSatisMenuVisible
        {
            get { return (Visibility)GetValue(IsSatisMenuVisibleProperty); }
            set { SetValue(IsSatisMenuVisibleProperty, value); }
        }
        public Visibility IsOptimizasyonMenuVisible
        {
            get { return (Visibility)GetValue(IsOptimizasyonMenuVisibleProperty); }
            set { SetValue(IsOptimizasyonMenuVisibleProperty, value); }
        }
        public Visibility IsPlanlamaModulerMenuVisible
        {
            get { return (Visibility)GetValue(IsPlanlamaModulerMenuVisibleProperty); }
            set { SetValue(IsPlanlamaModulerMenuVisibleProperty, value); }
        }
        public Visibility IsPlanlamaDosemeMenuVisible
        {
            get { return (Visibility)GetValue(IsPlanlamaDosemeMenuVisibleProperty); }
            set { SetValue(IsPlanlamaDosemeMenuVisibleProperty, value); }
        }
        public Visibility IsKonfeksiyonMenuVisible
        {
            get { return (Visibility)GetValue(IsKonfeksiyonMenuVisibleProperty); }
            set { SetValue(IsKonfeksiyonMenuVisibleProperty, value); }
        }
        public Visibility IsBilgiIslemMenuVisible
        {
            get { return (Visibility)GetValue(IsBilgiIslemMenuVisibleProperty); }
            set { SetValue(IsBilgiIslemMenuVisibleProperty, value); }
        }
        public Visibility IsKaliteDosemeMenuVisible
        {
            get { return (Visibility)GetValue(IsKaliteDosemeMenuVisibleProperty); }
            set { SetValue(IsKaliteDosemeMenuVisibleProperty, value); }
        }
        public Visibility IsLojistikMenuVisible
        {
            get { return (Visibility)GetValue(IsLojistikMenuVisibleProperty); }
            set { SetValue(IsLojistikMenuVisibleProperty, value); }
        }
        public Visibility IsDepoMenuVisible
        {
            get { return (Visibility)GetValue(IsDepoMenuVisibleProperty); }
            set { SetValue(IsDepoMenuVisibleProperty, value); }
        }
        public Visibility IsArgeMenuVisible
        {
            get { return (Visibility)GetValue(IsArgeMenuVisibleProperty); }
            set { SetValue(IsArgeMenuVisibleProperty, value); }
        }
        public Visibility IsAhsapMenuVisible
        {
            get { return (Visibility)GetValue(IsAhsapMenuVisibleProperty); }
            set { SetValue(IsAhsapMenuVisibleProperty, value); }
        }
        public Visibility IsSatinAlmaMenuVisible
        {
            get { return (Visibility)GetValue(IsSatinAlmaMenuVisibleProperty); }
            set { SetValue(IsSatinAlmaMenuVisibleProperty, value); }
        }
        public Visibility IsUretimPaketMenuVisible
        {
            get { return (Visibility)GetValue(IsUretimPaketMenuVisibleProperty); }
            set { SetValue(IsUretimPaketMenuVisibleProperty, value); }
        }
        public Visibility IsFinansMenuVisible
        {
            get { return (Visibility)GetValue(IsFinansMenuVisibleProperty); }
            set { SetValue(IsFinansMenuVisibleProperty, value); }
        }
        public Visibility IsInsanKaynaklariMenuVisible
        {
            get { return (Visibility)GetValue(IsInsanKaynaklariMenuVisibleProperty); }
            set { SetValue(IsInsanKaynaklariMenuVisibleProperty, value); }
        }


        public static readonly DependencyProperty IsSatisMenuVisibleProperty =
           DependencyProperty.Register("IsSatisMenuVisible", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsOptimizasyonMenuVisibleProperty =
           DependencyProperty.Register("IsOptimizasyonMenuVisible", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsPlanlamaModulerMenuVisibleProperty =
           DependencyProperty.Register("IsPlanlamaModulerMenuVisible", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsPlanlamaDosemeMenuVisibleProperty =
           DependencyProperty.Register("IsPlanlamaDosemeMenuVisible", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsKonfeksiyonMenuVisibleProperty =
           DependencyProperty.Register("IsKonfeksiyonMenuVisible", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsBilgiIslemMenuVisibleProperty =
           DependencyProperty.Register("IsBilgiIslemMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsKaliteDosemeMenuVisibleProperty =
           DependencyProperty.Register("IsKaliteDosemeMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsLojistikMenuVisibleProperty =
           DependencyProperty.Register("IsLojistikMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsDepoMenuVisibleProperty =
           DependencyProperty.Register("IsDepoMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsArgeMenuVisibleProperty =
           DependencyProperty.Register("IsArgeMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsAhsapMenuVisibleProperty =
           DependencyProperty.Register("IsAhsapMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsSatinAlmaMenuVisibleProperty =
           DependencyProperty.Register("IsSatinAlmaMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsUretimPaketMenuVisibleProperty =
           DependencyProperty.Register("IsUretimPaketMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
         public static readonly DependencyProperty IsFinansMenuVisibleProperty =
           DependencyProperty.Register("IsFinansMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty IsInsanKaynaklariMenuVisibleProperty =
           DependencyProperty.Register("IsInsanKaynaklariMenuVisibleProperty", typeof(Visibility), typeof(UserControl_Menu), new PropertyMetadata(Visibility.Visible));

        public UserControl_Menu()
        {
            InitializeComponent();
            IsSatisMenuVisible = Visibility.Collapsed;
            IsOptimizasyonMenuVisible = Visibility.Collapsed;
            IsPlanlamaModulerMenuVisible = Visibility.Collapsed;
            IsPlanlamaDosemeMenuVisible = Visibility.Collapsed;
            IsKonfeksiyonMenuVisible = Visibility.Collapsed;
            IsBilgiIslemMenuVisible = Visibility.Collapsed;
            IsKaliteDosemeMenuVisible = Visibility.Collapsed;
            IsLojistikMenuVisible = Visibility.Collapsed;
            IsDepoMenuVisible = Visibility.Collapsed;
            IsArgeMenuVisible = Visibility.Collapsed;
            IsAhsapMenuVisible = Visibility.Collapsed;
            IsSatinAlmaMenuVisible = Visibility.Collapsed;
            IsUretimPaketMenuVisible = Visibility.Collapsed;
            IsFinansMenuVisible = Visibility.Collapsed;
            IsInsanKaynaklariMenuVisible = Visibility.Collapsed;
            DisplayRelatedMenuItems();
            ToggleFullScreen();
        }

        private void DisplayRelatedMenuItems()
        {
            LoginLogic login = new();
            login.Departman = login.GetDepartment();

            switch (login.Departman)
            {
                case ("Bilgi Islem"):
                    {
                        IsBilgiIslemMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Konfeksiyon"):
                    {
                        IsKonfeksiyonMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Satis"):
                    {
                        IsSatisMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Doseme Kalite"):
                    {
                        IsKaliteDosemeMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Moduler Planlama"):
                    {
                        IsPlanlamaModulerMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Doseme Planlama"):
                    {
                        IsPlanlamaDosemeMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Lojistik"):
                    {
                        IsLojistikMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Doseme Depo"):
                    {
                        IsDepoMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Moduler Depo"):
                    {
                        IsDepoMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Konfeksiyon Depo"):
                    {
                        IsDepoMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Ar-Ge Moduler"):
                    {
                        IsArgeMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Ar-Ge Doseme"):
                    {
                        IsArgeMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Ahsap Planlama"):
                    {
                        IsAhsapMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Ahsap Kalite"):
                    {
                        IsAhsapMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Satin Alma Dosemeli"):
                    {
                        IsSatinAlmaMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Satin Alma Moduler"):
                    {
                        IsSatinAlmaMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Moduler Paketleme"):
                    {
                        IsUretimPaketMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Doseme Paketleme"):
                    {
                        IsUretimPaketMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Optimizasyon"):
                    {
                        IsOptimizasyonMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Finans"):
                    {
                        IsFinansMenuVisible = Visibility.Visible;
                        break;
                    }
                case ("Insan Kaynaklari"):
                    {
                        IsInsanKaynaklariMenuVisible = Visibility.Visible;
                        break;
                    }


                    // private Window DepartmanSelection(string departman) cls_login ui


                    //< ComboBoxItem Content = "Ahsap Planlama" />
                    //< ComboBoxItem Content = "Ahsap Kalite" />
                    //< ComboBoxItem Content = "Ar-Ge" />
                    //< ComboBoxItem Content = "Bilgi Islem" />
                    //< ComboBoxItem Content = "Finans" />
                    //< ComboBoxItem Content = "Insan Kaynaklari" />
                    //< ComboBoxItem Content = "Konfeksiyon" />
                    //< ComboBoxItem Content = "Moduler Planlama" />
                    //< ComboBoxItem Content = "Doseme Planlama" />
                    //< ComboBoxItem Content = "Satin Alma" />
                    //< ComboBoxItem Content = "Satis" />
                    //< ComboBoxItem Content = "Yonetim" />
                    //< ComboBoxItem Content = "Doseme Kalite" />
                    //< ComboBoxItem Content = "Lojistik" />
                    //< ComboBoxItem Content = "Depo" />
                    //< ComboBoxItem Content = "Optimizasyon" />

            }

        }
        private void btn_minimize_click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }
        private async void btn_close_click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the current window
                Window currentWindow = Window.GetWindow(this);
                int openWindowsCount = Application.Current.Windows.Cast<Window>()
                .Count(w => w.GetType().Namespace != "Microsoft.VisualStudio.DesignTools.WpfTap.WpfVisualTreeService.Adorners");
                // Check if this is the last open window
                if (openWindowsCount == 1)
                {
                    // Check if there are any ongoing async operations (custom logic)
                    if (Variables.NumberOfAsyncExecutions > 0)
                    {
                        // Ask the user if they want to terminate the operation
                        MessageBoxResult result = MessageBox.Show(
                            "Arka Planda Devam Eden İşlemler Mevcut. Bunları Durdurmak ve Uygulamayı Kapatmak İstiyor Musunuz?",
                            "Uyarı",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.No)
                        {
                            // Do not close the window or app
                            return;
                        }
                    }

                    // Close the entire application if it's the last window
                    Application.Current.Shutdown();
                }
                else
                {
                    // Close just the current window if it's not the last window
                    currentWindow?.Close();
                }
            }
            catch (Exception ex)
            {
                CRUDmessages.GeneralFailureMessage("Error during window close operation: " + ex.Message);
            }
        }
        private void btn_level_down_click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen();
        }
        private void menu_item_clicked(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = sender as MenuItem;

            if (clickedItem != null)
            {
                string formName = clickedItem.Tag as string;

                if (!string.IsNullOrEmpty(formName))
                {
                    // Construct the form name using your naming convention
                    string fullFormName = "Layer_UI." + formName;

                    // Use reflection to create an instance of the form
                    Type formType = Type.GetType(fullFormName);
                    if (formType != null)
                    {
                        var form = Activator.CreateInstance(formType) as Window;
                        form?.Show();

                        Window.GetWindow(this)?.Close();
                    }
                }
            }
        }
        private void menu_item_clicked_show_dialog(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = sender as MenuItem;

            if (clickedItem != null)
            {
                string formName = clickedItem.Tag as string;

                if (!string.IsNullOrEmpty(formName))
                {
                    // Construct the form name using your naming convention
                    string fullFormName = "Layer_UI." + formName;

                    // Use reflection to create an instance of the form
                    Type formType = Type.GetType(fullFormName);
                    if (formType != null)
                    {
                        var form = Activator.CreateInstance(formType) as Window;
                        form?.ShowDialog();

                    }
                }
            }
        }
        private void context_menu_item_clicked_show_second_form(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = (sender as MenuItem)?.Parent as ContextMenu;
            var originalMenuItem = clickedMenuItem?.PlacementTarget as MenuItem;

            if (originalMenuItem != null)
            {
                string formName = originalMenuItem.Tag as string;

                if (!string.IsNullOrEmpty(formName))
                {
                    // Construct the form name using your naming convention
                    string fullFormName = "Layer_UI." + formName;

                    // Use reflection to create an instance of the form
                    Type formType = Type.GetType(fullFormName);
                    if (formType != null)
                    {
                        var form = Activator.CreateInstance(formType) as Window;
                        form?.Show();
                    }
                }
            }
        }
        private void menu_item_clicked_show_second_form(object sender, RoutedEventArgs e)
        {
            MenuItem clickedItem = sender as MenuItem;

            if (clickedItem != null)
            {
                string formName = clickedItem.Tag as string;

                if (!string.IsNullOrEmpty(formName))
                {
                    // Construct the form name using your naming convention
                    string fullFormName = "Layer_UI." + formName;

                    // Use reflection to create an instance of the form
                    Type formType = Type.GetType(fullFormName);
                    if (formType != null)
                    {
                        var form = Activator.CreateInstance(formType) as Window;
                        form?.Show();

                    }
                }
            }
        }
        private void btn_log_out_clicked(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null) { parentWindow.Close(); }

            LoginLogic login = new LoginLogic();
            login.UpdatePCAutoLoginStatustoZero();

            Frm_Login frm_ = new Frm_Login();
            frm_.Show();
        }
        private void ToggleFullScreen()
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow == null) return;

            var workArea = SystemParameters.WorkArea;
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            // Determine if the window is on the second monitor
            bool isSecondMonitor = parentWindow.Left >= screenWidth || parentWindow.Top >= screenHeight;

            if (!isFullScreen)
            {
                // Store original position and size
                originalWindowRect = new Rect(parentWindow.Left, parentWindow.Top, parentWindow.Width, parentWindow.Height);

                // Adjust target screen dimensions for secondary monitor
                if (isSecondMonitor)
                {
                    screenWidth = SystemParameters.VirtualScreenWidth - SystemParameters.PrimaryScreenWidth;
                    screenHeight = SystemParameters.VirtualScreenHeight - SystemParameters.PrimaryScreenHeight;
                }

                // Set to full-screen but keep taskbar visible
                parentWindow.Left = isSecondMonitor ? SystemParameters.PrimaryScreenWidth : workArea.Left;
                parentWindow.Top = workArea.Top;
                parentWindow.Width = workArea.Width;
                parentWindow.Height = workArea.Height;  // Limits the height to exclude the taskbar area

                parentWindow.Topmost = true;
                isFullScreen = true;
            }
            else
            {
                originalWindowRect = new Rect(parentWindow.Left, parentWindow.Top, parentWindow.Width, parentWindow.Height);
                // Restore original window size and position
                double newWidth = parentWindow.MinWidth == 0 ? 500 : parentWindow.MinWidth;
                double newHeight = parentWindow.MinHeight == 0 ? 500 : parentWindow.MinHeight;

                parentWindow.Width = newWidth;
                parentWindow.Height = newHeight;
                parentWindow.Left = workArea.Left + (workArea.Width - newWidth) / 2;
                parentWindow.Top = workArea.Top + (workArea.Height - newHeight) / 2;
                parentWindow.Topmost = false;
                isFullScreen = false;
                return;
            }
        }



    }
}

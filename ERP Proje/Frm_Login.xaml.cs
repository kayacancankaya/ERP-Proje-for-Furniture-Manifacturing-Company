using Layer_Business;
using Layer_UI.Login;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI
{

    public partial class Frm_Login : Window
    {

        Cls_Login_Ui login_ui = new Cls_Login_Ui();
        LoginLogic login = new LoginLogic();

        public Frm_Login()
        {
            int autoStatus = login.IfStatusAutoLogin();
            if (autoStatus == 1)
            {
                Window window = login_ui.IfStatusAutoLoginSuccedGetDepartment();

                this.Close();
                window.Show();
            }

            InitializeComponent(); Window_Loaded();

            if (autoStatus == 0)
            {
                txt_user_name.Text = login.GetUserName();
            }


            if (autoStatus != 0 && autoStatus != 1)
            {
                MessageBox.Show("Yeni Kullanıcı veya Otomatik Giriş Parametreleri Hatalı.");
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_close_click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void btn_login_click(object sender, RoutedEventArgs e)
        {
            string user = txt_user_name.Text.ToString().Trim().ToLower();
            string password = txt_password.Password.ToString().Trim();

            login.IsCheckedAutoLogin = cb_auto_login.IsChecked ?? true;

            if (login.IsCheckedAutoLogin == true)
            {
                bool checkAutoLoginAvaiable = login.AutoLoginAttemp();
                if (checkAutoLoginAvaiable == false) { MessageBox.Show("Bu Cihazdan Otomatik Giriş Yapamazsınız."); }
            };


            int numberOfRows = login.CheckLoginAttemp(user, password);

            if (numberOfRows > 0)
            {
                login.UpdateAutoLoginStatus(user, password);

                password = txt_password.Password.ToString();
                Window window = new Window();
                window = login_ui.GetDepartmentForMainPage(txt_user_name.Text, password);
                this.Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş Yaptınız.");
            }
        }

        private void btn_sifremi_unuttum_clicked(object sender, RoutedEventArgs e)
        {
            Frm_Sifre_Degistir _frm = new Frm_Sifre_Degistir();
            this.Close();
            _frm.Show();
        }

        private void btn_ilk_kayit_clicked(object sender, RoutedEventArgs e)
        {
            Frm_Ilk_Kayit _frm = new Frm_Ilk_Kayit();
            this.Close();
            _frm.Show();
        }
    }
}

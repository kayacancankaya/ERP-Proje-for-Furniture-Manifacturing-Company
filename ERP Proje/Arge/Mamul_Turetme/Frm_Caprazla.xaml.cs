using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Arge.Mamul_Turetme
{
    /// <summary>
    /// Interaction logic for Frm_Caprazla.xaml
    /// </summary>
    public partial class Frm_Caprazla : Window
    {
        Cls_Urun urun = new Cls_Urun();
        public string UrunGrubuKodu { get; set; }
        public string ModelKodu { get; set; }
        public string SatisSekilKodu { get; set; }

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
        public Frm_Caprazla()
        {
            InitializeComponent(); Window_Loaded();
            List<string> urunGrupColl = urun.GetUrunGrupIsim().Select(i => i.UrunGrubuIsim).ToList();
            List<string> modelColl = urun.GetModelIsim().Select(i => i.ModelIsim).ToList();
            List<string> satisColl = urun.GetSatisSekilIsim().Select(i => i.SatisSekilIsim).ToList();
            cbx_yeni_urun_grup.ItemsSource = urunGrupColl;
            cbx_yeni_model.ItemsSource = modelColl;
            cbx_yeni_satis_sekil.ItemsSource = satisColl;

        }
        private void cbx_yeni_urun_grup_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    UrunGrubuKodu = urun.GetUrunGrubuKodu(cbx.SelectedItem.ToString()).Select(k => k.UrunGrubuKodu).FirstOrDefault().ToString();
                else
                    UrunGrubuKodu = string.Empty;
                txt_yeni_urun_grup.Text = UrunGrubuKodu;


            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçilirken");
            }
        }
        private void cbx_yeni_model_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Model Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    ModelKodu = urun.GetModelKodu(cbx.SelectedItem.ToString()).Select(k => k.ModelKodu).FirstOrDefault().ToString();
                else
                    ModelKodu = string.Empty;
                txt_yeni_model.Text = ModelKodu;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Seçilirken");
            }
        }
        private void cbx_yeni_satis_sekil_selection_changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx == null)
                {
                    CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçimi Esnasında");
                    return;
                }
                if (cbx.SelectedItem != null)
                    SatisSekilKodu = urun.GetSatisSekilKodu(cbx.SelectedItem.ToString()).Select(k => k.SatisSekilKodu).FirstOrDefault().ToString();
                else
                    SatisSekilKodu = string.Empty;
                txt_yeni_satis_sekil.Text = SatisSekilKodu;

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Seçilirken");
            }
        }
        private void btn_urun_grup_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Urun_Grubu_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if(Variables.FormResult_ == true)
                {
                    cbx_yeni_urun_grup.SelectedItem = frm.SelectedUrunGrubuIsmi;
                    txt_yeni_urun_grup.Text = frm.SelectedUrunGrubuKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Ürün Grubu Rehberi Açılırken");
            }
        }
        private void btn_model_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Model_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    cbx_yeni_model.SelectedItem = frm.SelectedModelIsmi;
                    txt_yeni_model.Text = frm.SelectedModelKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Model Rehberi Açılırken");
            }
        }
        private void btn_satis_sekil_rehberi_clicked(object sender, EventArgs e)
        {
            try
            {
                Frm_Satis_Sekil_Rehberi frm = new();
                Variables.FormResult_ = frm.ShowDialog();
                if (Variables.FormResult_ == true)
                {
                    cbx_yeni_satis_sekil.SelectedItem = frm.SelectedSatisSekilIsmi;
                    txt_yeni_satis_sekil.Text = frm.SelectedSatisSekilKodu;
                    frm.Close();
                }
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Satış Şekil Rehberi Açılırken");
            }
        }
        private void btn_kaydet_clicked(object sender, EventArgs e)
        {
            try
            {
                Variables.ErrorMessage_  =string.Empty;
                if (string.IsNullOrEmpty(txt_yeni_urun_grup.Text))
                    Variables.ErrorMessage_ += "Ürün Grubu Kodu Boş Olamaz \n";
                if (string.IsNullOrEmpty(txt_yeni_model.Text))
                    Variables.ErrorMessage_ += "Model Kodu Boş Olamaz \n";
                if (string.IsNullOrEmpty(txt_yeni_satis_sekil.Text))
                    Variables.ErrorMessage_ += "Satış Şekil Kodu Boş Olamaz \n";
                if(!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    return;
                }
                Mouse.OverrideCursor = Cursors.Wait;
                Variables.Result_ = urun.CheckIfUrunGrubuExists(txt_yeni_urun_grup.Text);
                if(!Variables.Result_)
                    Variables.ErrorMessage_ += "Girilen Ürün Grubu Kodu Sistemde Bulunamadı \n";
                Variables.Result_ = urun.CheckIfModelExists(txt_yeni_model.Text);
                if(!Variables.Result_)
                    Variables.ErrorMessage_ += "Girilen Model Kodu Sistemde Bulunamadı \n";
                Variables.Result_ = urun.CheckIfSatisSekliExists(txt_yeni_satis_sekil.Text);
                if(!Variables.Result_)
                    Variables.ErrorMessage_ += "Girilen Satış Şekil Kodu Sistemde Bulunamadı \n";
                if (!string.IsNullOrEmpty(Variables.ErrorMessage_))
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(Variables.ErrorMessage_);
                    Mouse.OverrideCursor = null;
                    return;
                }

                Variables.ResultInt_ = urun.CheckIfCaprazExists(txt_yeni_urun_grup.Text, txt_yeni_model.Text, txt_yeni_satis_sekil.Text);
                if(Variables.ResultInt_ == -1)
                {
                    CRUDmessages.GeneralFailureMessage("Çaprazlama Kontrol Edilirken");
                    Mouse.OverrideCursor = null;
                    return;
                }
                if(Variables.ResultInt_ > 0)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Çaprazlama Sistemde Kayıtlı");
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.Result_ = CRUDmessages.InsertOnayMessage();
                if (!Variables.Result_)
                    return;

                Variables.Result_ = urun.InsertCapraz(txt_yeni_urun_grup.Text,txt_yeni_model.Text,txt_yeni_satis_sekil.Text);
                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    CRUDmessages.GeneralFailureMessage("Kayıt Yapılırken");
                    return;
                }
                
                CRUDmessages.InsertSuccessMessage("Varyant", 1);
                Mouse.OverrideCursor = null;
                cbx_yeni_urun_grup.SelectedIndex = -1;
                cbx_yeni_model.SelectedIndex = -1;
                cbx_yeni_satis_sekil.SelectedIndex = -1;
            }
            catch
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Kayıt Yapılırken");
            }
        }
        public bool isFullScreen { get; set; } = true;
        private Rect originalWindowRect;
        private void ToggleFullScreen()
        {
            Window parentWindow = Window.GetWindow(this);
            var workArea = SystemParameters.WorkArea;
            if (parentWindow != null && isFullScreen == false)
            {

                parentWindow.Left = workArea.Left;
                parentWindow.Top = workArea.Top;
                parentWindow.Width = workArea.Width;
                parentWindow.Height = workArea.Height;
                parentWindow.Topmost = true;
                parentWindow.Topmost = false;
                isFullScreen = true;
                return;
            }
            if (parentWindow != null && isFullScreen == true)
            {
                originalWindowRect = new Rect(parentWindow.Left, parentWindow.Top, parentWindow.Width, parentWindow.Height);

                // Set the window to fullscreen
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
        private void mousedown_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
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
    }
}

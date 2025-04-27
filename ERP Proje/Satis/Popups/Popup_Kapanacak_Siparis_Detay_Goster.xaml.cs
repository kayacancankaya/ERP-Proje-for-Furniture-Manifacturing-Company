using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Kapanacak_Siparis_Detay_Goster.xaml
    /// </summary>
    public partial class Popup_Kapanacak_Siparis_Detay_Goster : Window
    {
        Cls_Siparis siparis = new();
        public bool IsClosedSuccessfully { get; private set; }

        public Popup_Kapanacak_Siparis_Detay_Goster(string fisno)
        {
            InitializeComponent();

            siparis.SiparisDetayCollection = siparis.GetSiparisSatirInfo(fisno, "Asil");
            dg_Siparis_Detay.ItemsSource = siparis.SiparisDetayCollection;

            Closed += PopupClosed;
        }

        Variables variables = new();
        private void siparis_kapat_ac(object sender, RoutedEventArgs e)
        {
            try
            {

                variables.IsTrue = CRUDmessages.UpdateOnayMessage();


                if (variables.IsTrue)
                {
                    variables.ErrorMessage = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;

                    Image? image = sender as Image;
                    if (image == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }
                    DataGridRow? row = UIinteractions.FindVisualParent<DataGridRow>(image);

                    if (row == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                    // Get the data item associated with the row
                    Cls_Siparis? dataItem = row.Item as Cls_Siparis;

                    if (dataItem == null) { CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken"); Mouse.OverrideCursor = null; return; }

                    try
                    {
                        bool kapat_ = true;

                        if (dataItem.SiparisDurum == "K")
                        {
                            kapat_ = false;
                        }

                        variables.IsTrue = siparis.SiparisKapatAcSatir(dataItem.Fisno, dataItem.FisSira, kapat_);

                        if (variables.IsTrue)
                            CRUDmessages.UpdateSuccessMessage("Sipariş");
                        else
                        { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }

                        siparis.SiparisDetayCollection = siparis.GetSiparisSatirInfo(dataItem.Fisno, "Asil");
                        dg_Siparis_Detay.ItemsSource = siparis.SiparisDetayCollection;

                        if (siparis.SiparisDetayCollection == null)
                        {
                            CRUDmessages.GeneralFailureMessage("Sayfa Yenilenirken");
                            Mouse.OverrideCursor = null;
                            return;
                        }


                        Mouse.OverrideCursor = null;
                    }

                    catch { CRUDmessages.UpdateFailureMessage("Sipariş"); Mouse.OverrideCursor = null; }
                }
                else return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); return;
            }

        }

        private void PopupClosed(object sender, EventArgs e)
        {
            IsClosedSuccessfully = true;

        }
    }

}

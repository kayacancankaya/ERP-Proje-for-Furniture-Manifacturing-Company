using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.Methods;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Frm_PO_No_Rehberi.xaml
    /// </summary>
    public partial class Frm_PO_No_Rehberi : Window
    {
        public Frm_PO_No_Rehberi()
        {
            InitializeComponent();
        }
        ObservableCollection<Cls_Siparis> sipCollection = new();
        Cls_Siparis siparis = new();

        public string SelectedPoNo { get; private set; }
        private void btn_siparis_listele_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                if (string.IsNullOrEmpty(txt_siparis_numarasi.Text))

                {
                    CRUDmessages.NoInput();
                    Mouse.OverrideCursor = null;
                    return;
                }
                Variables.Result_ = UserEntryControl.StringUzunlukKontrol(txt_siparis_numarasi.Text, 3, false, "Sipariş Numarası");
                if (!Variables.Result_)
                {
                    Mouse.OverrideCursor = null;
                    return;
                }

                sipCollection = siparis.GetPOno(txt_siparis_numarasi.Text);

                if (sipCollection == null)
                {
                    CRUDmessages.GeneralFailureMessage("Sipariş Bilgileri Alınırken");
                    Mouse.OverrideCursor = null;
                    return;
                }

                if (sipCollection.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    return;
                }

                dg_SiparisListe.ItemsSource = sipCollection;
                Mouse.OverrideCursor = null;
                dg_SiparisListe.Visibility = Visibility.Visible;
                stc_kaydet.Visibility = Visibility.Visible;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Stok Kartı Listelenirken");
                Mouse.OverrideCursor = null;
            }
        }

        private void btn_po_bilgisi_aktar_clicked(object sender, EventArgs e)
        {
            try
            {
                Variables.Counter_ = 0;
                foreach (Cls_Siparis item in dg_SiparisListe.Items)
                {
                    if (item.IsChecked == true)
                    {
                        SelectedPoNo = item.POnumarasi;
                        Variables.Counter_++;
                    }
                }

                if (Variables.Counter_ == 0)
                {
                    CRUDmessages.NoInput();
                    return;
                }
                if (Variables.Counter_ > 1)
                {
                    CRUDmessages.GeneralFailureMessageMoreSelectionThanExpected();
                    return;
                }
                DialogResult = true;
                Close();

                Mouse.OverrideCursor = null;
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Po Bilgisi Aktarılırken");

                Mouse.OverrideCursor = null;
            }
        }
    }
}

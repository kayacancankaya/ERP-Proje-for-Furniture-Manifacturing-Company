using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Layer_UI.UserControls
{
    /// <summary>
    /// Interaction logic for Frm_Stok_Durum.xaml
    /// </summary>
    public partial class Frm_Stok_Durum : Window
    {
        Cls_Depo depo = new();
        ObservableCollection<Cls_Depo> depoColl = new();
        Dictionary<string, string> kisitPairs = new Dictionary<string, string>();
        public Frm_Stok_Durum(string stok_kodu,string fabrika)
        {
            try
            {

                InitializeComponent();
                if(string.IsNullOrEmpty(stok_kodu))
                {
                    CRUDmessages.GeneralFailureMessage("Stok Kodu Bulunamadı.");
                    Mouse.OverrideCursor = null;
                    this.Close();
                }
                kisitPairs.Clear();
                kisitPairs.Add("stokKodu", stok_kodu);
                depoColl = depo.PopulateDepoDurumList(kisitPairs, fabrika);
                if (depoColl == null)
                {
                    CRUDmessages.GeneralFailureMessage("Stok Kodu Bilgileri Listelenirken");
                    Mouse.OverrideCursor = null;
                    this.Close();
                }
                if (depoColl.Count == 0)
                {
                    CRUDmessages.QueryIsEmpty();
                    Mouse.OverrideCursor = null;
                    this.Close();
                }
                dg_depo_stok_durum.ItemsSource= depoColl;
                Mouse.OverrideCursor = null;

            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                this.Close();   
            }
        }
    }
}

using Layer_2_Common.Type;
using Layer_Business;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Layer_UI.Satis.Sevk.Popups
{
    /// <summary>
    /// Interaction logic for Popup_Sevk_Guncelle_Satir.xaml
    /// </summary>
    public partial class Popup_Sevk_Guncelle_Satir : Window
    {
        Cls_Sevk sevk = new();
        Variables variables = new();
        public Popup_Sevk_Guncelle_Satir(string sevkEmriNoFormConstructor)
        {
            InitializeComponent();

            sevk.SevkCollection = sevk.PopulateSevkGuncelleListSatir(sevkEmriNoFormConstructor);
            if (sevk.SevkCollection.Any())
                dg_Sevk_Detay.ItemsSource = sevk.SevkCollection;
            else
            { CRUDmessages.GeneralFailureMessage("Sevk Satır Bilgileri Alınırken"); return; }

        }
        int sevkMiktar = 0, siparisSira = 0, depoMiktar = 0, siparisMiktar = 0, acikSevkMiktar = 0, teslimMiktar = 0;
        string sevkEmrino = string.Empty, siparisNo = string.Empty;

        Cls_Sevk ReOrderSevkSira = new();
        ObservableCollection<Cls_Sevk> ReOrderedSevkSiraCollection = new();


        private bool selectMiktarColumn = false;
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow? row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Sevk Miktar")
                            {
                                miktarColumnIndex = i;
                                break;
                            }
                        }

                        // Select the "Miktar" column of the row
                        if (miktarColumnIndex >= 0)
                        {
                            dataGrid.SelectedCells.Clear();
                            DataGridCellInfo cellInfo = new DataGridCellInfo(row.Item, dataGrid.Columns[miktarColumnIndex]);
                            dataGrid.SelectedCells.Add(cellInfo);
                        }
                    }
                }
                selectMiktarColumn = false; // Reset the flag
            }
        }


    }
}

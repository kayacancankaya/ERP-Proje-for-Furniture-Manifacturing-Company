using Layer_2_Common.Type;
using Layer_Business.ViewModels;
using Layer_UI.Depo.DAT;
using Layer_UI.Methods;
using Layer_UI.Satis.Evrak;
using Layer_UI.Satis.Siparis;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layer_UI.Arge.Recete
{
    /// <summary>
    /// Interaction logic for Frm_Urun_Agac.xaml
    /// </summary>
    public partial class Frm_Urun_Agac : Window
    {
        UrunAgacViewModel agac = new();
        ObservableCollection<UrunAgacViewModel> agacColl = new();
        Dictionary<string, string> filterDic = new();
        int expandState = 0;
        public Frm_Urun_Agac()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                InitializeComponent();
                agacColl = agac.GetTop30UrunAgac();
                tr_urun_agac.ItemsSource = agacColl;
                totalResult = agac.CountAgac();
                if (totalResult > 30)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = null;
                CRUDmessages.GeneralFailureMessage("Ürün Ağaçları Listelenirken");
            }
        }

        string filterText = string.Empty;
        string filterHeader = string.Empty;
        int totalResult = 0;
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switch (button.Tag.ToString())
                {
                    case "Mamul Kodu":
                        txt_header_mamul_kodu.Text = button.Tag.ToString();
                        FilterPopupMamulKodu.IsOpen = true;
                        break;
                    case "Mamul Adı":
                        txt_header_mamul_adi.Text = button.Tag.ToString();
                        FilterPopupMamulAdi.IsOpen = true;
                        break;
                    case "Ham Kodu":
                        txt_header_ham_kodu.Text = button.Tag.ToString();
                        FilterPopupHamKodu.IsOpen = true;
                        break;
                    case "Ham Adı":
                        txt_header_ham_adi.Text = button.Tag.ToString();
                        FilterPopupHamAdi.IsOpen = true;
                        break;
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
            }
        }
        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    TextBox textBox = sender as TextBox;
                    switch (textBox.Name.ToString())
                    {
                        case "FilterTextBox_mamul_kodu":
                            filterText = FilterTextBox_mamul_kodu.Text;
                            filterHeader = txt_header_mamul_kodu.Text;
                            FilterPopupMamulKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_mamul_adi":
                            filterText = FilterTextBox_mamul_adi.Text;
                            filterHeader = txt_header_mamul_adi.Text;
                            FilterPopupMamulAdi.IsOpen = false;
                            break;
                        case "FilterTextBox_cari_kodu":
                            filterText = FilterTextBox_ham_kodu.Text;
                            filterHeader = txt_header_ham_kodu.Text;
                            FilterPopupHamKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_ham_adi":
                            filterText = FilterTextBox_ham_adi.Text;
                            filterHeader = txt_header_ham_adi.Text;
                            FilterPopupHamAdi.IsOpen = false;
                            break;
                    }

                    if (!string.IsNullOrEmpty(filterText))
                    {
                        //daha önceden filtre varsa filtreyi değiştir
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic[filterHeader] = filterText;
                        //yoksa ekle
                        else
                            filterDic.Add(filterHeader, filterText);

                    }
                    else
                    {
                        //eğer boş bir şekilde entera vurulmuşsa ve daha önceden filtre varsa filtreyi kaldır
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic.Remove(filterHeader);
                        //daha önce bir şey yoksa sorgulamadan direk çık
                        else
                        {
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }

                    if (filterDic.Count > 0)
                        agacColl = agac.GetUrunAgac(filterDic, 1);
                    else
                        agacColl = agac.GetTop30UrunAgac();
                    tr_urun_agac.ItemsSource = agacColl;

                    if (filterDic.Count > 0)
                        totalResult = agac.CountAgac(filterDic);
                    else
                        totalResult = agac.CountAgac();


                    if (totalResult > 30)
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                    else
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                    lastOffset = 0;
                    pageNumber = 1;
                    Mouse.OverrideCursor = null;

                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }
        }
        private void FilterTextLostFocus(object sender, EventArgs e)
        {
            try
            {   
                Mouse.OverrideCursor = Cursors.Wait;
                    TextBox textBox = sender as TextBox;
                    switch (textBox.Name.ToString())
                    {
                        case "FilterTextBox_mamul_kodu":
                            filterText = FilterTextBox_mamul_kodu.Text;
                            filterHeader = txt_header_mamul_kodu.Text;
                            FilterPopupMamulKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_mamul_adi":
                            filterText = FilterTextBox_mamul_adi.Text;
                            filterHeader = txt_header_mamul_adi.Text;
                            FilterPopupMamulAdi.IsOpen = false;
                            break;
                        case "FilterTextBox_cari_kodu":
                            filterText = FilterTextBox_ham_kodu.Text;
                            filterHeader = txt_header_ham_kodu.Text;
                            FilterPopupHamKodu.IsOpen = false;
                            break;
                        case "FilterTextBox_ham_adi":
                            filterText = FilterTextBox_ham_adi.Text;
                            filterHeader = txt_header_ham_adi.Text;
                            FilterPopupHamAdi.IsOpen = false;
                            break;
                    }

                    if (!string.IsNullOrEmpty(filterText))
                    {
                        //daha önceden filtre varsa filtreyi değiştir
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic[filterHeader] = filterText;
                        //yoksa ekle
                        else
                            filterDic.Add(filterHeader, filterText);

                    }
                    else
                    {
                        //eğer boş bir şekilde entera vurulmuşsa ve daha önceden filtre varsa filtreyi kaldır
                        if (filterDic.ContainsKey(filterHeader))
                            filterDic.Remove(filterHeader);
                        //daha önce bir şey yoksa sorgulamadan direk çık
                        else
                        {
                            Mouse.OverrideCursor = null;
                            return;
                        }
                    }

                    if (filterDic.Count > 0)
                        agacColl = agac.GetUrunAgac(filterDic, 1);
                    else
                        agacColl = agac.GetTop30UrunAgac();
                    tr_urun_agac.ItemsSource = agacColl;

                    if (filterDic.Count > 0)
                        totalResult = agac.CountAgac(filterDic);
                    else
                        totalResult = agac.CountAgac();


                    if (totalResult > 30)
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                    else
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                    lastOffset = 0;
                    pageNumber = 1;
                    Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleme İşlemi Esnasında");
                Mouse.OverrideCursor = null;
            }

        }
        private void btn_clear_filter_clicked(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FilterTextBox_ham_adi.Text = string.Empty;
                FilterTextBox_ham_kodu.Text = string.Empty;
                FilterTextBox_mamul_adi.Text = string.Empty;
                FilterTextBox_mamul_kodu.Text = string.Empty;

                if (filterDic != null)
                {
                    if (filterDic.Count > 0)
                        filterDic.Clear();
                }

                agacColl = agac.GetTop30UrunAgac();
                tr_urun_agac.ItemsSource = agacColl;

                totalResult = agac.CountAgac();
                if (totalResult > 30)
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan 30 adet Listeleniyor.";
                else
                    txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + totalResult.ToString() + " adet Listeleniyor.";

                lastOffset = 0;
                pageNumber = 1;
                Mouse.OverrideCursor = null;
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Filtreleri Kaldırırken");
                Mouse.OverrideCursor = null;
            }
        }

        double lastOffset = 0;
        int pageNumber = 1;
        Dictionary<TreeViewItem,SolidColorBrush> coloredTreeViewDic = new();
        private void tree_view_expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                var treeViewItem = e.OriginalSource as TreeViewItem;

                if (treeViewItem == null)
                    return;

                SolidColorBrush solidColorBrush = treeViewItem.Background as SolidColorBrush;
                coloredTreeViewDic.Add(treeViewItem, solidColorBrush);
                treeViewItem.Background = new SolidColorBrush(Color.FromRgb(243,169,130));
                UrunAgacViewModel seviyeItem = treeViewItem.DataContext as UrunAgacViewModel;

                treeViewItem.ItemContainerGenerator.StatusChanged += (s, ev) =>
                {
                    if (treeViewItem.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        // Now the child items are generated, so we can access them
                        ApplyStripeToChildItems(treeViewItem, seviyeItem);
                    }
                };
                
            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Mamul Detayları Açılırken");
            }

        }
        private void ApplyStripeToChildItems(TreeViewItem treeViewItem, UrunAgacViewModel seviyeItem)
        {
            int childIndex = 0;
            foreach (var item in treeViewItem.Items)
            {
                var childItem = treeViewItem.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (childItem != null)
                {
                    // Alternating colors for stripe effect
                    if (seviyeItem.Seviye % 2 == 1)
                    {
                        childItem.Background = (childIndex % 2 == 0)
                            ? new SolidColorBrush(Color.FromRgb(235, 245, 255)) // Stripe color 1
                            : new SolidColorBrush(Color.FromRgb(200, 230, 240)); // Stripe color 2
                    }
                    else
                    {
                        childItem.Background = (childIndex % 2 == 1)
                            ? new SolidColorBrush(Color.FromRgb(245, 240, 255)) // Stripe color 1
                            : new SolidColorBrush(Color.FromRgb(235, 223, 255)); // Stripe color 2
                    }
                    childIndex++;
                }
            }
        }
        private void tree_view_collapsed(object sender, RoutedEventArgs e)
        {
            try
            {
                var treeViewItem = e.OriginalSource as TreeViewItem;

                if (treeViewItem == null)
                    return;

                SolidColorBrush solidColorBrush = new();
                if (coloredTreeViewDic.ContainsKey(treeViewItem))
                {
                    treeViewItem.Background = coloredTreeViewDic[treeViewItem];
                    coloredTreeViewDic.Remove(treeViewItem);
                }
                

            }
            catch
            {
                CRUDmessages.GeneralFailureMessage("Mamul Detayları Kapatılırken");
            }

        }
        private void treeScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            ScrollViewer item = UIinteractions.FindVisualChild<ScrollViewer>(tree);

            headerScrollViewer.ScrollToHorizontalOffset(item.HorizontalOffset);
        }
        private void dg_scroll_down(object sender, ScrollEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(tr_urun_agac, 0) as Decorator;
                if (border == null)
                    return;
                var scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer == null)
                    return;

                lastOffset = scrollViewer.VerticalOffset;
                // Load more items if we're near the bottom
                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight / 10) && totalResult > agacColl.Count())
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    pageNumber++;
                    ObservableCollection<UrunAgacViewModel> moreAgacCollection = new();
                    if (filterDic == null)
                        moreAgacCollection = agac.GetUrunAgac(pageNumber);
                    else if (filterDic.Count() == 0)
                        moreAgacCollection = agac.GetUrunAgac(pageNumber);
                    else
                        moreAgacCollection = agac.GetUrunAgac(filterDic, pageNumber);
                    if (moreAgacCollection == null)
                    {
                        CRUDmessages.GeneralFailureMessage("İlave Ürün Ağaçları Eklenirken");
                        Mouse.OverrideCursor = null;
                    }
                    if (moreAgacCollection.Count > 0)
                    {
                        ObservableCollection<UrunAgacViewModel> lastAgacCollection = new ObservableCollection<UrunAgacViewModel>
                                        (agacColl.Union(moreAgacCollection).ToList());
                        tr_urun_agac.ItemsSource = lastAgacCollection;
                        tr_urun_agac.Items.Refresh();
                        agacColl = lastAgacCollection;
                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastAgacCollection.Count() + " adet Listeleniyor.";

                        double center = 0;
                        center = scrollViewer.ScrollableHeight / 2.0;
                        scrollViewer.ScrollToVerticalOffset(center);
                        lastOffset = center;

                    }
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
            }

        }
        private void mouse_wheel_pushed(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var border = VisualTreeHelper.GetChild(tr_urun_agac, 0) as Decorator;
                if (border != null)
                {
                    var scrollViewer = border.Child as ScrollViewer;
                    if (scrollViewer != null)
                    {
                        // Adjust the scroll offset based on the delta
                        if (e.Delta < 0) // Scrolling down
                        {
                            lastOffset = scrollViewer.VerticalOffset ;
                               // Load more items if we're near the bottom
                                if (lastOffset > scrollViewer.ScrollableHeight - (scrollViewer.ScrollableHeight/10) && totalResult > agacColl.Count())
                                {
                                    Mouse.OverrideCursor = Cursors.Wait;
                                    pageNumber++;

                                    ObservableCollection<UrunAgacViewModel> moreAgacCollection = new();
                                    if (filterDic == null)
                                        moreAgacCollection = agac.GetUrunAgac(pageNumber);
                                    else if (filterDic.Count() == 0)
                                        moreAgacCollection = agac.GetUrunAgac(pageNumber);
                                    else
                                        moreAgacCollection = agac.GetUrunAgac(filterDic, pageNumber);

                                    if (moreAgacCollection == null)
                                    {
                                        CRUDmessages.GeneralFailureMessage("İlave Ürün Ağaçları Eklenirken");
                                        Mouse.OverrideCursor = null;
                                    }
                                    else if (moreAgacCollection.Count > 0)
                                    {
                                        // Combine current and new items
                                        ObservableCollection<UrunAgacViewModel> lastAgacCollection =
                                            new ObservableCollection<UrunAgacViewModel>(agacColl.Union(moreAgacCollection).ToList());

                                        tr_urun_agac.ItemsSource = lastAgacCollection;
                                        tr_urun_agac.Items.Refresh();
                                        agacColl = lastAgacCollection;
                                        txt_result.Text = "Toplam " + totalResult.ToString() + " Adet Sonuçtan " + lastAgacCollection.Count() + " adet Listeleniyor.";

                                     border = VisualTreeHelper.GetChild(tr_urun_agac, 0) as Decorator;
                                    if (border != null)
                                    {
                                        double center = 0;
                                         scrollViewer = border.Child as ScrollViewer;
                                        if (scrollViewer != null)
                                        {
                                            center = scrollViewer.ScrollableHeight / 2.0;
                                            scrollViewer.ScrollToVerticalOffset(center);
                                        }
                                        lastOffset = center;
                                    }

                                }

                                Mouse.OverrideCursor = null;
                                }
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                CRUDmessages.GeneralFailureMessage("Scroll Bar Yüksekliği Hesaplanırken");
                Mouse.OverrideCursor = null;
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

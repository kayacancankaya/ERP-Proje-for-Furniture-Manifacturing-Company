using Layer_2_Common.Type;
using Layer_Business;
using Layer_UI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Layer_UI.Satis.Siparis.Popups
{

    public partial class Popup_Varyant_Secim : Window
    {
        public bool KodTuretilecekMi { get; set; } = false;
        Cls_Urun cls_urun = new Cls_Urun();
        ObservableCollection<Cls_Urun> urunCollection = new ObservableCollection<Cls_Urun>();

        public Popup_Varyant_Secim(string urunGrubuKodu, string modelKodu, string satisSekilKodu, string urunGrubu, string model, string satisSekil)
        {
            InitializeComponent();

            urunCollection.Clear();

            DataContext = Resources["cls_urun"];

            txt_Urun_Tipi.Text = urunGrubu;
            txt_model.Text = model;
            txt_satis_sekil.Text = satisSekil;

            cls_urun.Query = string.Format("select ozisim,ozkod from stbmodelopsiyon where grupkod='{0}' and modelkod='{1}' and ssekli='{2}' order by sira", urunGrubuKodu, modelKodu, satisSekilKodu);
            urunCollection = cls_urun.PopulatePopUpVaryantSecimWindow();

            int sira = 1;
            foreach (Cls_Urun cls_urun in urunCollection)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = String.Format("{0}:", cls_urun.VaryantIsmi),
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 10, 0, 0),
                    Style = (Style)Resources["TextBlockStyle_18_Bold4"],

                };

                ComboBox comboBox = new ComboBox
                {
                    Width = 200,
                    Name = String.Format("ComboBox{0}", sira),
                    Margin = new Thickness(10, 10, 0, 0),
                    FontWeight = FontWeights.Bold,
                };

                cls_urun.Query = string.Format("select sira,isim from vbvOzellikKisitListele a where a.ugrupkod='{0}' and a.modelkod='{1}' and a.ssekli='{2}' and a.sira = {3} order by sira", urunGrubuKodu, modelKodu, satisSekilKodu, sira);
                ObservableCollection<Cls_Urun> varyantCollection = new ObservableCollection<Cls_Urun>();
                varyantCollection = cls_urun.VaryantOzKisitVarIseListele();

                if (varyantCollection.Count > 0)
                {

                    foreach (Cls_Urun urun in varyantCollection)
                    {

                        ComboBoxItem comboBoxItem = new ComboBoxItem
                        {
                            Content = urun.VaryantTipi,
                            Foreground = Brushes.Black,
                            Background = Brushes.White,
                            FontWeight = FontWeights.Bold,
                        };
                        comboBox.Items.Add(comboBoxItem);
                    }
                }
                else
                {
                    cls_urun.Query = string.Format("select isim from stbozdetay where maskod='{0}' and kilit='H' order by isim asc", cls_urun.VaryantKodu);

                    varyantCollection = cls_urun.VaryantListele();

                    foreach (Cls_Urun urun in varyantCollection)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem
                        {
                            Content = urun.VaryantTipi,
                            Foreground = Brushes.Black,
                            Background = Brushes.White,
                            FontWeight = FontWeights.Bold,
                        };
                        comboBox.Items.Add(comboBoxItem);
                    }

                }
                varyantCollection.Clear();

                stc_dynamic_texts.Children.Add(textBlock);
                stc_dynamic_combo_boxes.Children.Add(comboBox);
                sira++;
            }

            stc_button_panel.Visibility = Visibility.Visible;

            cls_urun.UrunGrubuKodu = urunGrubuKodu;
            cls_urun.ModelKodu = modelKodu;
            cls_urun.SatisSekilKodu = satisSekilKodu;
        }

        public Tuple<string, string> ValuesToTransfer { get; private set; } //frm_musteri_secime gondermek maksatlı

        private async void btn_kaydet_clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                
                string isimAnahtar = string.Empty, ingilizceIsimAnahtar = string.Empty, anahtar = string.Empty, anahtarDegeri = string.Empty, ingilizceAnahtarDegeri = string.Empty,
                       sablonKod = string.Empty, ozellikKodu = string.Empty, mamulKodu = string.Empty, ozellikKoduDuzeltme = string.Empty, ozellikKoduMas = string.Empty, ozellikKoduDuzeltilmemis = string.Empty;

                int ozellikKoduRakamKisiti;

                cls_urun.Query = string.Format("select isimanahtar,isimanahtaring,kod5 from [stbmodelcapraz] where grupkod='{0}' and modelkod='{1}' and ssekli='{2}'", cls_urun.UrunGrubuKodu, cls_urun.ModelKodu, cls_urun.SatisSekilKodu);
                urunCollection = cls_urun.GetNameKeys();

                isimAnahtar = urunCollection.FirstOrDefault()?.IsimAnahtar;
                ingilizceIsimAnahtar = urunCollection.FirstOrDefault()?.IngilizceIsimAnahtar;
                sablonKod = urunCollection.FirstOrDefault()?.SablonKod;

                cls_urun.Query = string.Format("select max(sira) from stbmodelopsiyon where grupkod='{0}' and modelkod='{1}' and ssekli='{2}'", cls_urun.UrunGrubuKodu, cls_urun.ModelKodu, cls_urun.SatisSekilKodu);
                int maxSira = cls_urun.GetMaxSira();
                

                for (int i = 1; i <= maxSira; i++)
                {
                    ComboBox comboBox = stc_dynamic_combo_boxes.Children[i - 1] as ComboBox;

                    foreach (Cls_Urun item in urunCollection)
                    {
                        anahtar = $"@o{i}";

                        if (comboBox != null)
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
                            anahtarDegeri = comboBoxItem.Content.ToString();
                        }

                        cls_urun.Query = string.Format("select c.ingisim from stbmodelopsiyon b left join stbozdetay c on b.ozkod = c.maskod where b.grupkod = '{0}' and b.modelkod = '{1}' and b.ssekli = '{2}'and b.sira = '{3}' and c.isim='{4}'", cls_urun.UrunGrubuKodu, cls_urun.ModelKodu, cls_urun.SatisSekilKodu, i, anahtarDegeri);
                        ingilizceAnahtarDegeri = cls_urun.GetIngilizceAnahtarDegeri();

                        cls_urun.Query = string.Format("select c.kod from stbmodelopsiyon b left join stbozdetay c on b.ozkod = c.maskod where b.grupkod = '{0}' and b.modelkod = '{1}' and b.ssekli = '{2}'and b.sira = '{3}' and c.isim='{4}'", cls_urun.UrunGrubuKodu, cls_urun.ModelKodu, cls_urun.SatisSekilKodu, i, anahtarDegeri);
                        ozellikKoduDuzeltilmemis = cls_urun.GetVaryantKodu();

                        cls_urun.Query = string.Format("select b.ozkod from stbmodelopsiyon b left join stbozdetay c on b.ozkod = c.maskod where b.grupkod = '{0}' and b.modelkod = '{1}' and b.ssekli = '{2}'and b.sira = '{3}' and c.isim='{4}'", cls_urun.UrunGrubuKodu, cls_urun.ModelKodu, cls_urun.SatisSekilKodu, i, anahtarDegeri);
                        ozellikKoduMas = cls_urun.GetVaryantKodu();

                        cls_urun.Query = string.Format("select uzunluk from stbozmas where kod = '{0}'", ozellikKoduMas);
                        ozellikKoduRakamKisiti = Convert.ToInt32(cls_urun.GetVaryantKodu());

                        if (ozellikKoduDuzeltilmemis.Length > ozellikKoduRakamKisiti) { MessageBox.Show("Varyant belirtilen uzunluk kısıtından büyük"); return; }
                        if (ozellikKoduDuzeltilmemis.Length == ozellikKoduRakamKisiti) { ozellikKoduDuzeltme = ozellikKoduDuzeltilmemis; }
                        if (ozellikKoduDuzeltilmemis.Length < ozellikKoduRakamKisiti)
                        {
                            int zerosToAdd = ozellikKoduRakamKisiti - ozellikKoduDuzeltme.Length;
                            ozellikKoduDuzeltme = ozellikKoduDuzeltme.PadLeft(zerosToAdd, '0');
                        }

                        ozellikKodu = ozellikKodu + ozellikKoduDuzeltme;

                        if (!string.IsNullOrEmpty(isimAnahtar)) isimAnahtar = isimAnahtar.Replace(anahtar, anahtarDegeri);
                        if (!string.IsNullOrEmpty(ingilizceIsimAnahtar)) ingilizceIsimAnahtar = ingilizceIsimAnahtar.Replace(anahtar, ingilizceAnahtarDegeri);
                    }
                }
                mamulKodu = sablonKod + ozellikKodu;

                Variables variables = new();

                variables.ErrorMessage = string.Empty;

                if (mamulKodu.Length > 35) variables.ErrorMessage = variables.ErrorMessage + "Ürün Kodunun Uzunluğu 35'den Büyük Olamaz.\n";
                if (sablonKod.Length > 11) variables.ErrorMessage = variables.ErrorMessage + "Şablon Kodunun Uzunluğu 11'den Büyük Olamaz.\n";
                if (ingilizceIsimAnahtar.Length > 100) variables.ErrorMessage = variables.ErrorMessage + "İngilizce İsim uzunluğu 100'den Büyük Olamaz.\n";
                if (isimAnahtar.Length > 100) variables.ErrorMessage = variables.ErrorMessage + "İngilizce İsim uzunluğu 100'den Büyük Olamaz.";

                if (string.IsNullOrEmpty(variables.ErrorMessage) == false) { MessageBox.Show(variables.ErrorMessage); return; }

                LoginLogic login = new();

                variables.IsTrue = cls_urun.UpdateStokAdiIfVaryantExists(mamulKodu, isimAnahtar, ingilizceIsimAnahtar);
                if (!variables.IsTrue) //stok kodu mevcut değil giriş yapılacak
                {
                    Guid newGuid = Guid.NewGuid();
                    string identifier = "{" + newGuid.ToString().ToUpper() + "}";

                    ValuesToTransfer = new Tuple<string, string>(mamulKodu, isimAnahtar);
                    this.DialogResult = true;
                    this.Close();
                    Mouse.OverrideCursor = null;
                    Variables.NumberOfAsyncExecutions ++;
                    await cls_urun.InsertVaryantAsync(sablonKod, mamulKodu, isimAnahtar, identifier, ingilizceIsimAnahtar);
                    Variables.NumberOfAsyncExecutions--;


                }

                if (variables.IsTrue)//stok kodu mevcut ve stok adına guncelleme yapılmış
                {
                    KodTuretilecekMi = true;
                    ValuesToTransfer = new Tuple<string, string>(mamulKodu, isimAnahtar);
                    MessageBox.Show("Ürün Adı Güncellendi");
                    this.DialogResult = true;
                    this.Close();
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); Mouse.OverrideCursor = null; }
        }
        private bool selectMiktarColumn = false;

        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectMiktarColumn)
            {
                if (sender is DataGrid dataGrid)
                {
                    DataGridRow row = ItemsControl.ContainerFromElement(dataGrid, e.OriginalSource as DependencyObject) as DataGridRow;
                    if (row != null)
                    {
                        // Find the index of the "Miktar" column
                        int miktarColumnIndex = -1;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Header.ToString() == "Miktar")
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



using Layer_2_Common.Type;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business.ViewModels
{
    public class OpsiyonKaydetViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Cls_Urun> _urunColl;
        public ObservableCollection<Cls_Urun> UrunColl
        {
            get => _urunColl;
            set
            {
                _urunColl = value;
                OnPropertyChanged("UrunColl");
            }
        }
        private ObservableCollection<Cls_Urun> _kisitColl;
        public ObservableCollection<Cls_Urun> KisitColl
        {
            get => _kisitColl;
            set
            {
                _kisitColl = value;
                OnPropertyChanged("KisitColl");
            }
        }

        private ObservableCollection<string> _ozellikKodlari;
        public ObservableCollection<string> OzellikKodlari
        {
            get => _ozellikKodlari;
            set
            {
                _ozellikKodlari = value;
                OnPropertyChanged("OzellikKodlari");
            }
        }
        private ObservableCollection<string> _ozellikIsimleri;
        public ObservableCollection<string> OzellikIsimleri
        {
            get => _ozellikIsimleri;
            set
            {
                _ozellikIsimleri = value;
                OnPropertyChanged("OzellikIsimleri");
            }
        }
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public OpsiyonKaydetViewModel(Cls_Urun eklenecekUrun,ObservableCollection<Cls_Urun> opsiyonColl)
        {

            Cls_Urun urun = new();
            if (UrunColl != null)
                UrunColl.Clear();
            UrunColl = new();
            int collOzellikSayisi = 0;
            if (opsiyonColl!= null)
            {
                if (opsiyonColl.Count > 0)
                    collOzellikSayisi = opsiyonColl.Max<Cls_Urun>(n => n.OzellikSayisi);

            }
            if (eklenecekUrun.OzellikSayisi <= collOzellikSayisi)
            {
                for (int i = 0; i < eklenecekUrun.OzellikSayisi; i++)
                {
                    Cls_Urun siradakiOzellik = opsiyonColl[i];
                    UrunColl.Add(siradakiOzellik);
                }
            }
            else
            {
                Variables.Counter_ = 0;
                for (int i = 0; i < collOzellikSayisi; i++)
                {
                    Cls_Urun siradakiOzellik = opsiyonColl[i];
                    UrunColl.Add(siradakiOzellik);
                    Variables.Counter_++;
                }
                for (int j = Variables.Counter_ + 1; j <= eklenecekUrun.OzellikSayisi; j++)
                {
                    Cls_Urun opsiyonToAdd = new();
                    opsiyonToAdd.OzellikSayisi = j;
                    opsiyonToAdd.OzellikTipi = "-";
                    opsiyonToAdd.OzellikIsmi = "<-Seçim Yapınız->";
                    opsiyonToAdd.ReceteDegeri = string.Format("@o{0}",j);
                    UrunColl.Add(opsiyonToAdd);

                }
            }
            OzellikIsimleri = urun.GetOzellikIsimleri();
            OzellikKodlari = urun.GetOzellikKodlari();
            if (KisitColl != null)
                UrunColl.Clear();
            KisitColl = new();
        }
        public OpsiyonKaydetViewModel()
        {
            Cls_Urun urun = new();
            OzellikIsimleri = urun.GetOzellikIsimleri();
            OzellikKodlari = urun.GetOzellikKodlari();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

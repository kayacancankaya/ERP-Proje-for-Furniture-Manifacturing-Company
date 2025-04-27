using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business.ViewModels
{
    public class DATKaydetViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Cls_Depo> _depoColl;
        public ObservableCollection<Cls_Depo> DepoColl
        {
            get => _depoColl;
            set
            {
                _depoColl = value;
                OnPropertyChanged("DepoColl");
            }
        }

        private ObservableCollection<int> _girisDepoKodus;
        public ObservableCollection<int> GirisDepoKodus
        {
            get => _girisDepoKodus;
            set
            {
                _girisDepoKodus = value;
                OnPropertyChanged("GirisDepoKodus");
            }
        }
        private ObservableCollection<int> _cikisDepoKodus;
        public ObservableCollection<int> CikisDepoKodus
        {
            get => _cikisDepoKodus;
            set
            {
                _cikisDepoKodus = value;
                OnPropertyChanged("CikisDepoKodus");
            }
        }
       
        public DATKaydetViewModel(Dictionary<string,string> kisitPairs)
        {
            Cls_Depo depo = new Cls_Depo();
            CikisDepoKodus = depo.GetDistinctDepoKodu();
            GirisDepoKodus = depo.GetDistinctDepoKodu();
            DepoColl = depo.PopulateDATKaydedilecekListesi(kisitPairs,"Vita",0);
        }
        public DATKaydetViewModel()
        {
            Cls_Depo depo = new Cls_Depo();
            CikisDepoKodus = depo.GetDistinctDepoKodu();
            GirisDepoKodus = depo.GetDistinctDepoKodu();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

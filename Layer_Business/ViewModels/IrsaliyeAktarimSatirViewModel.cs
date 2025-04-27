using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business.ViewModels
{
    public class IrsaliyeAktarimSatirViewModel
    {
        public string Fisno { get; set; } = string.Empty;
        public int Sira { get; set; } = 0;
        public string StokKodu { get; set; } = string.Empty;
        public string StokAdi { get; set; } = string.Empty;
        public float Miktar { get; set; } = 0;
        public float DovizFiyat { get; set; } = 0;
        public int DovizTipi { get; set; } = 0;
        public float NetFiyat { get; set; } = 0;
        public float BrutFiyat { get; set; } = 0;
    }
}

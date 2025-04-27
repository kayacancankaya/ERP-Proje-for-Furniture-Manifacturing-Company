using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business.ViewModels
{
    public class VaryantFiyatGirisViewModel
    {
        public string FiyatGrubu { get; set; } = string.Empty;      
        public string StokKodu { get; set; } = string.Empty;
        public string StokAdi { get; set; } = string.Empty;

        public float A { get; set; } = 0;
        public float B { get; set; } = 0;
        public float C { get; set; } = 0;
        public float D { get; set; } = 0;
        public float E { get; set; } = 0;
        public float F { get; set; } = 0;
        public float G { get; set; } = 0;
        public float H { get; set; } = 0; 
        public float HPlus { get; set; }             // H+
        public float I { get; set; } = 0;
        public float IPlus { get; set; } = 0;           // I+
        public float J { get; set; } = 0;
        public float JPlus { get; set; } = 0;           // J+
        public float K { get; set; } = 0;
        public float L { get; set; } = 0;
        public float DosemeSure { get; set; } = 0;

        public string KayitDurum { get; set; } = "Kaydedilmedi...";
    }
}

using Layer_2_Common.Type;
using Layer_Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Layer_UI.Methods
{
    internal class UserEntryControl
    {
        internal static bool Miktar0Kontrol(int miktar, string isim)
        {
            if (miktar <= 0)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(isim + " 0 olamaz");
                return false;
            }
            return true;
        }
        internal static bool Miktar0Kontrol(decimal miktar, string isim)
        {
            if (miktar <= 0)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(isim + " 0 olamaz");
                return false;
            }
            return true;
        }
        internal static bool StringUzunlukKontrol(string kontrolEdilecek, int uzunluk, bool buyukOlamaz, string isim)
        {
            if (buyukOlamaz)
            {
                if (kontrolEdilecek.Length > uzunluk)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0} {1} Karakterden Büyük Olamaz.", isim, uzunluk.ToString()));
                    return false;
                }
            }
            else
            {
                if (kontrolEdilecek.Length < uzunluk)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0} {1} Karakterden Küçük Olamaz.", isim, uzunluk.ToString()));
                    return false;
                }
            }
            return true;
        }
        internal static bool StringEsitlikKontrol(string kontrolEdilecek, int uzunluk, string isim)
        {

            if (kontrolEdilecek.Length != uzunluk)
            {
                CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0} {1} Karakter Olmalı. Mevcut Karakter {3}", isim, uzunluk.ToString(), kontrolEdilecek.Length.ToString()));
                return false;
            }

            return true;
        }
        internal static bool IsStringNullOrEmpty(string kontrolEdilecek, string? kontrolEdilecek2, string? kontrolEdilecek3, string? kontrolEdilecek4, string? kontrolEdilecek5)
        {
            if (string.IsNullOrEmpty(kontrolEdilecek) &&
                string.IsNullOrEmpty(kontrolEdilecek2) &&
                string.IsNullOrEmpty(kontrolEdilecek3) &&
                string.IsNullOrEmpty(kontrolEdilecek4) &&
                string.IsNullOrEmpty(kontrolEdilecek5))
            {
                CRUDmessages.NoInput();
                return false;
            }
            return true;
        }
        internal static bool MiktarKiyasKontrol(int kontrolEdilecek, int referans, bool buyukOlamaz, bool esitOlur, string neyNeyden)
        {
            if (buyukOlamaz && !esitOlur)
            {
                if (kontrolEdilecek > referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Büyük Olamaz.", neyNeyden));
                    return false;
                }
            }
            else if (buyukOlamaz && esitOlur)
            {
                if (kontrolEdilecek >= referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Büyük ve Eşit Olamaz.", neyNeyden));
                    return false;
                }
            }
            else if (!buyukOlamaz && !esitOlur)
            {
                if (kontrolEdilecek < referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Küçük Olamaz.", neyNeyden));
                    return false;
                }
            }
            else
            {
                if (kontrolEdilecek <= referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Küçük ve Eşit Olamaz.", neyNeyden));
                    return false;
                }
            }
            return true;
        }
        internal static bool MiktarKiyasKontrol(decimal kontrolEdilecek, decimal referans, bool buyukOlamaz, bool esitOlur, string neyNeyden)
        {
            if (buyukOlamaz && !esitOlur)
            {
                if (kontrolEdilecek > referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Büyük Olamaz.", neyNeyden));
                    return false;
                }
            }
            else if (buyukOlamaz && esitOlur)
            {
                if (kontrolEdilecek >= referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Büyük ve Eşit Olamaz.", neyNeyden));
                    return false;
                }
            }
            else if (!buyukOlamaz && !esitOlur)
            {
                if (kontrolEdilecek < referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Küçük Olamaz.", neyNeyden));
                    return false;
                }
            }
            else
            {
                if (kontrolEdilecek <= referans)
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage(string.Format("{0}'dan Küçük ve Eşit Olamaz.", neyNeyden));
                    return false;
                }
            }
            return true;
        }
        internal static bool DepoKoduKontrol(int depoKodu)
        {
            try
            {
                Cls_Depo distinctDepos = new();
                ObservableCollection<int> distinctDepoColl = distinctDepos.GetDistinctDepoKodu();

                foreach (int d in distinctDepoColl)
                {
                    if (d == depoKodu)
                    {
                        return true;
                    }
                }

                CRUDmessages.GeneralFailureMessageCustomMessage("Girilen Depo Kodu Sistemde Bulunamadı.");
                return false;
            }

            catch
            {
                CRUDmessages.GeneralFailureMessage("Depo Kodu Kontrol Edilirken");
                return false;
            }
        }
        public static float TruncateToFourDecimalPlaces(float number)
        {
            return Convert.ToSingle(Math.Truncate(number * 10000) / 10000);
        }


    }
}

using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;

namespace Layer_Business
{
    public class Cls_Cari : Cls_Base
    {
        private string _satisCariKodu = "120A100104";

        public string SatisCariKodu 
        {
            get { return _satisCariKodu; }
            set
            {
                _satisCariKodu = value;
                OnPropertyChanged(nameof(SatisCariKodu));
            }
        }

        private string _satisCariAdi = "VITA BIANCA MOB.TEKS.INS.ITH.IHR.PAZ.SAN.VE TIC.LTD.STI.";

        public string SatisCariAdi
        {
            get { return _satisCariAdi; }
            set
            {
                _satisCariAdi = value;
                OnPropertyChanged(nameof(SatisCariAdi));
            }
        }

        private string _teslimCariKodu = string.Empty;

        public string TeslimCariKodu
        {
            get => _teslimCariKodu;
            set
            {
                _teslimCariKodu = value;
                OnPropertyChanged(nameof(TeslimCariKodu));
            }
        }

        private string _teslimCariAdi = string.Empty;
        public string TeslimCariAdi
        {
            get => _teslimCariAdi;
            set
            {
                _teslimCariAdi = value;
                OnPropertyChanged(nameof(TeslimCariAdi));
            }
        }

        private string _tedarikciCariKodu = string.Empty;

        public string TedarikciCariKodu
        {
            get => _tedarikciCariKodu;
            set
            {
                _tedarikciCariKodu = value;
                OnPropertyChanged(nameof(TedarikciCariKodu));
            }
        }

        private string _tedarikciCariAdi = string.Empty;
        public string TedarikciCariAdi
        {
            get => _tedarikciCariAdi;
            set
            {
                _tedarikciCariAdi = value;
                OnPropertyChanged(nameof(TedarikciCariAdi));
            }
        }

        public string IrsaliyeNumarasi { get; set; }

        private DovizTipi _dovizTipi = DovizTipi.USD;

        public new DovizTipi DovizTipi
        {
            get { return _dovizTipi; }
            set
            {
                _dovizTipi = value;
                OnPropertyChanged(nameof(DovizTipi));
            }
        }

        private SiparisTipi _siparisTipi = SiparisTipi.Yurtdisi;

        public new SiparisTipi SiparisTipi
        {
            get { return _siparisTipi; }
            set
            {
                _siparisTipi = value;
                OnPropertyChanged(nameof(SiparisTipi));
            }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private string _ulkeKodu;

        public string UlkeKodu
        {
            get { return _ulkeKodu; }
            set
            {
                _ulkeKodu = value;
                OnPropertyChanged(nameof(UlkeKodu));
            }
        }
        private string _ilIlce;

        public string IlIlce
        {
            get { return _ilIlce; }
            set
            {
                _ilIlce = value;
                OnPropertyChanged(nameof(IlIlce));
            }
        }

        private ObservableCollection<Cls_Cari> _sipariseCariBaglaCollection;
        public ObservableCollection<Cls_Cari> SipariseCariBaglaCollection
        {
            get { return _sipariseCariBaglaCollection; }
            set { _sipariseCariBaglaCollection = value; }
        }
        ObservableCollection<Cls_Cari> coll_cari = new ObservableCollection<Cls_Cari>();
        DataLayer dataLayer = new DataLayer();
        DataTable dataTable = new DataTable();
        
        Variables variables = new Variables();

        public ObservableCollection<Cls_Cari> PopulateSipariseCariBaglaTeslimCari(string teslimCariKodu, string teslimCariAdi, string fabrika)
        {
            try
            {
                TeslimCariKodu = teslimCariKodu;
                TeslimCariAdi = teslimCariAdi;

                Variables.Query_ = "select CARI_KOD,CARI_ISIM,ULKE_KODU FROM TBLCASABIT WHERE 1=1 ";

                if (string.IsNullOrEmpty(TeslimCariKodu) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_KOD like '%" + TeslimCariKodu + "%' "; }

                if (string.IsNullOrEmpty(TeslimCariAdi) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_ISIM like '%" + TeslimCariAdi + "%' "; }

                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_,fabrika);
             
                coll_cari.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Cari cls_cari= new Cls_Cari
                    {
                        IsChecked = false,
                        TeslimCariKodu = row["CARI_KOD"].ToString(),
                        TeslimCariAdi = row["CARI_ISIM"].ToString(),
                        UlkeKodu = row["ULKE_KODU"].ToString()
                    };
                    coll_cari.Add(cls_cari);
                }

                SipariseCariBaglaCollection = coll_cari;

                return SipariseCariBaglaCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Cari> PopulateSipariseCariBaglaTeslimCari(string teslimCariKodu, string teslimCariAdi)
        {
            try
            {
                TeslimCariKodu = teslimCariKodu;
                TeslimCariAdi = teslimCariAdi;

                Variables.Query_ = "select CARI_KOD,CARI_ISIM,ULKE_KODU FROM TBLCASABIT WHERE 1=1 ";

                if (string.IsNullOrEmpty(TeslimCariKodu) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_KOD like '%" + TeslimCariKodu + "%' "; }

                if (string.IsNullOrEmpty(TeslimCariAdi) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_ISIM like '%" + TeslimCariAdi + "%' "; }

                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);
             
                coll_cari.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Cari cls_cari= new Cls_Cari
                    {
                        IsChecked = false,
                        TeslimCariKodu = row["CARI_KOD"].ToString(),
                        TeslimCariAdi = EntryControls.FixTurkishCharacters(row["CARI_ISIM"].ToString()),
                        UlkeKodu = row["ULKE_KODU"].ToString()
                    };
                    coll_cari.Add(cls_cari);
                }

                SipariseCariBaglaCollection = coll_cari;

                return SipariseCariBaglaCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Cari> PopulateSipariseCariBaglaSatisCari(string satisCariKodu, string satisCariAdi)
        {
            try
            {
                SatisCariKodu = satisCariKodu;
                SatisCariAdi = satisCariAdi;

                Variables.Query_ = "select CARI_KOD,CARI_ISIM,ULKE_KODU FROM TBLCASABIT WHERE 1=1 ";

                if (string.IsNullOrEmpty(SatisCariKodu) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_KOD like '%" + SatisCariKodu + "%' "; }

                if (string.IsNullOrEmpty(SatisCariAdi) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_ISIM like '%" + SatisCariAdi + "%' "; }

                
                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);
              
                coll_cari.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Cari cls_cari= new Cls_Cari
                    {
                        IsChecked = false,
                        SatisCariKodu = row["CARI_KOD"].ToString(),
                        SatisCariAdi = row["CARI_ISIM"].ToString(),
                        UlkeKodu = row["ULKE_KODU"].ToString()
                    };
                    coll_cari.Add(cls_cari);
                }

                SipariseCariBaglaCollection = coll_cari;

                return SipariseCariBaglaCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Cari> PopulateCariBilgileriGuncelle()
        {
            try
            {

                Variables.Query_ = "select CARI_KOD,CARI_ISIM FROM TBLCASABIT WHERE 1=1 ";

                if (string.IsNullOrEmpty(SatisCariKodu) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_KOD like '%" + SatisCariKodu + "%' "; }

                if (string.IsNullOrEmpty(SatisCariAdi) == false) { Variables.Query_ = Variables.Query_ + "AND CARI_ISIM like '%" + SatisCariAdi + "%' "; }


                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);

                coll_cari.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Cari cls_cari = new Cls_Cari
                    {
                        IsChecked = false,
                        SatisCariKodu = row["CARI_KOD"].ToString(),
                        SatisCariAdi = row["CARI_ISIM"].ToString(),
                        TeslimCariKodu = row["CARI_KOD"].ToString(),
                        TeslimCariAdi = row["CARI_ISIM"].ToString(),
                    };
                    coll_cari.Add(cls_cari);
                }

                SipariseCariBaglaCollection = coll_cari;

                return SipariseCariBaglaCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Cari> GetTedarikciCariInfo(string irsaliyeNumarasi, string fabrika)
        {
            try
            {
                Variables.Query_ = "select top 1 a.cari_kodu,cari_isim,fatirs_no from tblfatuirs a (nolock) " +
                                  " left join tblcasabit b (nolock) on a.cari_kodu = b.cari_kod " +
                                  " where a.fatirs_no like '%' + @irsaliyeNumarasi + '%' AND FTIRSIP='4' " +
                                  " order by TARIH desc";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = irsaliyeNumarasi.Substring(0,3);
                ObservableCollection<Cls_Cari> coll_temp = new();
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, fabrika))
                {
                    if (reader == null)
                    {
                        TedarikciCariKodu = "Kayıt Yok";
                        TedarikciCariAdi = "Kayıt Yok";
                        IrsaliyeNumarasi = "";
                    }
                    while (reader.Read())
                    {
                        Cls_Cari cari = new Cls_Cari()
                        {
                            TedarikciCariKodu = reader[0].ToString(),
                            TedarikciCariAdi = reader[1].ToString(),
                            IrsaliyeNumarasi = reader[2].ToString(),
                        };
                        
                        coll_temp.Add(cari);
                    }
                }
                return coll_temp;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Cari> GetTedarikciCariInfo(string irsaliyeNumarasi)
        {
            try
            {
                Variables.Query_ = "select top 1 a.cari_kodu,cari_isim,fatirs_no from tblfatuirs a (nolock) " +
                                  " left join tblcasabit b (nolock) on a.cari_kodu = b.cari_kod " +
                                  " where a.fatirs_no like '%' + @irsaliyeNumarasi + '%' AND FTIRSIP='4' " +
                                  " order by TARIH desc";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = irsaliyeNumarasi.Substring(0,3);
                ObservableCollection<Cls_Cari> coll_temp = new();
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter))
                {
                    if (reader == null)
                    {
                        TedarikciCariKodu = "Kayıt Yok";
                        TedarikciCariAdi = "Kayıt Yok";
                        IrsaliyeNumarasi = "";
                    }
                    while (reader.Read())
                    {
                        Cls_Cari cari = new Cls_Cari()
                        {
                            TedarikciCariKodu = reader[0].ToString(),
                            TedarikciCariAdi = reader[1].ToString(),
                            IrsaliyeNumarasi = reader[2].ToString(),
                        };
                        char[] charArray = cari.IrsaliyeNumarasi.ToCharArray();
                        Array.Reverse(charArray);
                        string reverseIrsaliye = new string(charArray);
                        string irsaliyeNoReverse = string.Empty;
                        foreach (char c in reverseIrsaliye)
                        {
                            if (c != '0')
                                irsaliyeNoReverse += c;
                            else
                                break;
                        }
                        char[] charArrayNo = irsaliyeNoReverse.ToCharArray();
                        Array.Reverse(charArrayNo);
                        string irsaliyeNoString = new string(charArrayNo);
                        int irsaliyeNumber = Convert.ToInt32(irsaliyeNoString) + 1;
                        string irsaliyeNumberString = irsaliyeNumber.ToString();
                        int numberLength = irsaliyeNoString.Length;
                        int totalLength = cari.IrsaliyeNumarasi.Length;
                        cari.IrsaliyeNumarasi = cari.IrsaliyeNumarasi.Substring(0, totalLength - numberLength) + irsaliyeNumberString;
                        coll_temp.Add(cari);
                    }
                }
                return coll_temp;
            }
            catch 
            {
                return null;
            }
        }
        public Dictionary<string, string> GetTeslimCaris()
        {
            try
            {
                Dictionary<string, string> cariDic = new();
                Variables.Query_ = "Select CARI_KOD,CARI_ISIM from tblcasabit (nolock) WHERE CARI_TIP='A' order by cari_isim ";
                ObservableCollection<Cls_Cari> cariColl = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Cari model = new();
                    model.TeslimCariKodu = reader["CARI_KOD"] is DBNull ? "" : reader["CARI_KOD"].ToString();
                    model.TeslimCariAdi = reader["CARI_ISIM"] is DBNull ? "" : reader["CARI_ISIM"].ToString();
                    return model;
                });
                if(cariColl == null)
                {
                    Dictionary<string, string> emptyDic = new();
                    return emptyDic;
                }

                foreach (Cls_Cari item in cariColl)
                    cariDic.Add(item.TeslimCariKodu, EntryControls.FixTurkishCharacters(item.TeslimCariAdi));

                return cariDic;

            }
            catch (Exception)
            {
                Dictionary<string, string> emptyDic = new();
                return emptyDic;
            }
        }
        public static DovizTipi GetDovizTipi(string dovizTipi)
        {
            switch (dovizTipi) 
            {
                case "USD":
                    return DovizTipi.USD;
                case "TRY":
                    return DovizTipi.TL;
                case "EUR":
                    return DovizTipi.EUR;
                case "GBP":
                    return DovizTipi.GBP;
                default:
                    return DovizTipi.USD;

            }
        }
        public static SiparisTipi GetSiparisTipi(string siparisTipi)
        {
            switch (siparisTipi) 
            {
                case "Yurt Dışı":
                    return SiparisTipi.Yurtdisi;
                case "Yurt İçi":
                    return SiparisTipi.Yurtici;
                default:
                    return SiparisTipi.Yurtdisi;

            }
        }


        public string GetIrsaliyeNoFromCari(string cariKodu)
        {
            try
            {
                Variables.Query_ = "Select top 1 fatirs_no from tblfatuirs where cari_kodu=@cariKodu and FTIRSIP='4' order by tarih desc";

                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 15);
                parameter[0].Value = cariKodu;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_,Variables.Yil_,parameter)) 
                {
                    if (reader == null)
                        return null;
                    while (reader.Read()) 
                    {
                        IrsaliyeNumarasi = reader[0].ToString();
                    }
                }
                char[] charArray = IrsaliyeNumarasi.ToCharArray();
                Array.Reverse(charArray);
                string reverseIrsaliye = new string(charArray);
                string irsaliyeNoReverse = string.Empty;
                foreach (char c  in reverseIrsaliye)
                {
                    if(c != '0')
                        irsaliyeNoReverse += c;
                    else
                        break;
                }
                char[] charArrayNo = irsaliyeNoReverse.ToCharArray();
                Array.Reverse(charArrayNo);
                string irsaliyeNoString = new string(charArrayNo);
                int irsaliyeNumber = Convert.ToInt32(irsaliyeNoString) + 1;
                string irsaliyeNumberString = irsaliyeNumber.ToString();
                int numberLength = irsaliyeNoString.Length;
                int totalLength = IrsaliyeNumarasi.Length;
                string newIrsaliyeNo = IrsaliyeNumarasi.Substring(0,totalLength-numberLength) + irsaliyeNumberString;

                return newIrsaliyeNo;

            }
            catch 
            {
                return null;
            }
        }
        public string GetIrsaliyeNoFromCari(string cariKodu, string fabrika)
        {
            try
            {
                Variables.Query_ = "Select top 1 fatirs_no from tblfatuirs where cari_kodu=@cariKodu and FTIRSIP='4' order by tarih desc";

                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 15);
                parameter[0].Value = cariKodu;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_,Variables.Yil_,parameter,fabrika)) 
                {
                    if (reader == null)
                        return null;
                    while (reader.Read()) 
                    {
                        IrsaliyeNumarasi = reader[0].ToString();
                    }
                }
                return IrsaliyeNumarasi;

            }
            catch 
            {
                return null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string getStr)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(getStr));
        }
    }
}

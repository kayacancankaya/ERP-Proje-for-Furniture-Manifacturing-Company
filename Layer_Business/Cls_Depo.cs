using Layer_2_Common.Type;
using Layer_Business.ViewModels;
using Layer_Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Layer_Business
{
    public class Cls_Depo : Cls_Base, INotifyPropertyChanged
    {

        private string _stokKodu = string.Empty;
        public string StokKodu
        {
            get { return _stokKodu; }
            set
            {
                if (_stokKodu != value)
                {
                    _stokKodu = value;
                    OnPropertyChanged(nameof(StokKodu));
                }
            }
        }
        public string EskiStokKodu { get; set; }
        private string _stokAdi = string.Empty;
        public string StokAdi
        {
            get { return _stokAdi; }
            set
            {
                if (_stokAdi != value)
                {
                    _stokAdi = value;
                    OnPropertyChanged(nameof(StokAdi));
                }
            }
        }
        public string HamKodu { get; set; } = string.Empty;
        public string HamAdi { get; set; } = string.Empty;
        public float ReceteBirimMiktar { get; set; }
        public string MamulKodu { get; set; } = string.Empty;
        public string MamulAdi { get; set; } = string.Empty;
        public string UrunKodu { get; set; } = string.Empty;
        public string UrunAdi { get; set; } = string.Empty;
        public float StokMiktar { get; set; } = 0;
        public int DepoKodu { get; set; } = 0;
        public int SiparisMiktar { get; set; }
        public int HareketID { get; set; } = 0;
        public string Birim { get; set; } = string.Empty;
        public string FisNo { get; set; } = string.Empty;

        public float HareketMiktar { get; set; } = 0;

        public string GirisCikisKod { get; set; } = string.Empty;

        public DateTime HareketTarih { get; set; }


        public string HareketAciklama { get; set; } = string.Empty;
        public string SiparisNumarasi { get; set; } = string.Empty;
        public int SiparisSira { get; set; } = 0;
        public string Ekalan { get; set; } = string.Empty;

        public float NetFiyat {  get; set; } = 0;
        public float BrütFiyat { get; set; } = 0;
        public float IsemriMiktar { get; set; }
        public float DovizFiyat { get; set; } = 0;
        public string TakipNoGenel { get; set; }
        public string HareketTuru { get; set; } = string.Empty;
        public string BelgeTipi { get; set; } = string.Empty;
        public string BelgeTipiKodu { get; set; } = string.Empty;
        public string FaturaTipi { get; set; } = string.Empty;
        public string FaturaTipiKodu { get; set; } = string.Empty;
        public string HareketTipi { get; set; } = string.Empty;
        public string HareketTipiKodu { get; set; } = string.Empty;
        public string IsemriNo { get; set; } = string.Empty;
        public string ReferansIsemriNo { get; set; } = string.Empty;
        public string TakipNo { get; set; } = string.Empty;
        public string Kod2 { get; set; } = string.Empty;
        public string Kod3 { get; set; } = string.Empty;
        public string Kod4 { get; set; } = string.Empty;

        private string _kod1 = string.Empty;
        public string Kod1
        {
            get { return _kod1; }
            set
            {
                if (_kod1 != value)
                {
                    _kod1 = value;
                    OnPropertyChanged(nameof(Kod1));
                }
            }
        }
        private string _kod5 = string.Empty;
        public string Kod5
        {
            get { return _kod5; }
            set
            {
                if (_kod5 != value)
                {
                    _kod5 = value;
                    OnPropertyChanged(nameof(Kod5));
                }
            }
        }
        private int _girisDepoKodu = 0;
        public int GirisDepoKodu
        {
            get { return _girisDepoKodu; }
            set
            {
                if (_girisDepoKodu != value)
                {
                    _girisDepoKodu = value;
                    OnPropertyChanged(nameof(GirisDepoKodu));
                }
            }
        }
        private int _cikisDepoKodu = 0;
        public int CikisDepoKodu
        {
            get { return _cikisDepoKodu; }
            set
            {
                if (_cikisDepoKodu != value)
                {
                    _cikisDepoKodu = value;
                    OnPropertyChanged(nameof(CikisDepoKodu));
                }
            }
        }
        private float _cikisDepoBakiye = 0;
        public float CikisDepoBakiye
        {
            get { return _cikisDepoBakiye; }
            set
            {
                if (_cikisDepoBakiye != value)
                {
                    _cikisDepoBakiye = value;
                    OnPropertyChanged(nameof(CikisDepoBakiye));
                }
            }
        }

        private float _girisDepoBakiye = 0;
        public float GirisDepoBakiye
        {
            get { return _girisDepoBakiye; }
            set
            {
                if (_girisDepoBakiye != value)
                {
                    _girisDepoBakiye = value;
                    OnPropertyChanged(nameof(GirisDepoBakiye));
                }
            }
        }
        private float _toplamDATIhtiyacMiktar = 0;
        public float ToplamDATIhtiyacMiktar
        {
            get { return _toplamDATIhtiyacMiktar; }
            set
            {
                if (_toplamDATIhtiyacMiktar != value)
                {
                    _toplamDATIhtiyacMiktar = value;
                    OnPropertyChanged(nameof(ToplamDATIhtiyacMiktar));
                }
            }
        }
        private float _gonderilenDATMiktar = 0;
        public float GonderilenDATMiktar
        {
            get { return _gonderilenDATMiktar; }
            set
            {
                if (_gonderilenDATMiktar != value)
                {
                    _gonderilenDATMiktar = value;
                    OnPropertyChanged(nameof(GonderilenDATMiktar));
                }
            }
        }
        private float _kalanDATMiktar = 0;
        public float KalanDATMiktar
        {
            get { return _kalanDATMiktar; }
            set
            {
                if (_kalanDATMiktar != value)
                {
                    _kalanDATMiktar = value;
                    OnPropertyChanged(nameof(KalanDATMiktar));
                }
            }
        }
        private float _gonderilecekDATMiktar = 0;
        public float GonderilecekDATMiktar
        {
            get { return _gonderilecekDATMiktar; }
            set
            {
                if (_gonderilecekDATMiktar != value)
                {
                    _gonderilecekDATMiktar = value;
                    OnPropertyChanged(nameof(GonderilecekDATMiktar));
                }
            }
        }

        //eksi bakiye
        public float BakiyeAranan { get; set; }
        public float Bakiye10 { get; set; }
        public float Bakiye15 { get; set; }
        public float Bakiye30 { get; set; }
        public float Bakiye35 { get; set; }
        public float Bakiye40 { get; set; }
        public float Bakiye45 { get; set; }
        // eksi bakiye ends
        private bool _isChecked = true;
        public int Id { get; set; }
        public bool IsChecked 
        {
            get { return _isChecked; }
            set { _isChecked = value; 
                    OnPropertyChanged(nameof(IsChecked));
                }
        }


        Variables variables = new();
        DataLayer data = new();
        LoginLogic login = new();
        ObservableCollection<Cls_Depo> temp_depo_coll = new();
        ObservableCollection<Cls_Depo> temp_stok_hareket_coll = new();
        ObservableCollection<Cls_Depo> DepoCollection = new();
        ObservableCollection<Cls_Depo> StokHareketCollection = new();

        public Cls_Depo()
        {
            variables.Fabrika = login.GetFabrika();

            Variables.Fabrika_ = variables.Fabrika;
            Variables.Departman_ = login.GetDepartment();
        }
        public ObservableCollection<Cls_Depo> PopulateDepoDurumList(Dictionary<string,string> kisitPairs, string fabrika)
        {
            try
            {
                Variables.Query_ = "SELECT ph.STOK_KODU,sabit.STOK_ADI,ph.depo_kodu,cast(isnull(TOP_GIRIS_MIK,0)-isnull(TOP_CIKIS_MIK,0) as float) AS bakiye " +
                                    " FROM TBLSTOKPH ph (nolock) left join tblstsabit sabit (nolock) on ph.stok_kodu=sabit.stok_kodu where ph.DEPO_KODU<>0 " +
                                    " and ph.stok_kodu = @stokKodu";

                SqlParameter[] parameter = new SqlParameter[1];
        
                if (kisitPairs["stokKodu"] != null)
                {
                    parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameter[0].Value = kisitPairs["stokKodu"];
                }

                temp_depo_coll.Clear();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter,fabrika))
                {
                    if (reader != null )
                    {
                        while (reader.Read())
                        {
                            Cls_Depo depoItem = new Cls_Depo
                            {
                                StokKodu = reader["STOK_KODU"].ToString(),
                                StokAdi = reader["STOK_ADI"].ToString(),
                                DepoKodu = Convert.ToInt32(reader["depo_kodu"]),
                                StokMiktar = Convert.ToInt32(reader["bakiye"]),
                            };
                            temp_depo_coll.Add(depoItem);
                        }
                    }

                    if (reader == null ) 
                    {
                        Cls_Depo depoItem = new Cls_Depo
                        {
                            StokKodu = "BOS",
                        };
                        temp_depo_coll.Add(depoItem);
                    }
                }

                DepoCollection = temp_depo_coll;
                return DepoCollection;

            }
            catch { return null; }
        }
        public ObservableCollection<Cls_Depo> PopulateStokHareketList(Dictionary<string, string> filterDic,int pageNumber,string fabrika,List<int> depoKoduConst,List<string> kod1Const, List<string> harTipConst, List<string> belTipConst, List<string> fatTipConst)
        {
            try
            {

                Variables.Query_ = "SELECT a.inckeyno,a.STOK_KODU,FISNO,CAST(round(isnull(STHAR_GCMIK,0),4) AS FLOAT) AS GC_MIK, STHAR_GCKOD AS GCKOD, a.DEPO_KODU,STHAR_TARIH AS TARIH," +
                                  " STHAR_ACIKLAMA AS ACIK,STHAR_SIPNUM AS SIPNUM,STRA_SIPKONT,kod_1,stok_adi,ekalan,STHAR_NF AS NF,STHAR_BF AS BF,STHAR_IAF AS IAF," +
                                  "STHAR_HTUR,STHAR_BGTIP,STHAR_FTIRSIP,kt_takipnum FROM TBLSTHAR a (NOLOCK) " +
                                   "left join tblstsabit b (NOLOCK) on a.stok_kodu = b.stok_kodu left join tblisemriek c (NOLOCK) on a.sthar_sipnum = c.isemri WHERE 1 = 1 ";


                if (depoKoduConst != null)
                {
                    if(depoKoduConst.Count>0)
                    {
                        Variables.Query_ += " and (";
                        foreach (int item in depoKoduConst)
                            Variables.Query_ += $" a.depo_kodu={item} or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (kod1Const != null)
                {
                    if(kod1Const.Count>0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in kod1Const)
                            Variables.Query_ += $" kod_1='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (harTipConst != null)
                {
                    if(harTipConst.Count>0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in harTipConst)
                            Variables.Query_ += $" a.sthar_htur='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (belTipConst != null)
                {
                    if(belTipConst.Count>0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in belTipConst)
                            Variables.Query_ += $" a.STHAR_BGTIP='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (fatTipConst != null)
                {
                    if(fatTipConst.Count>0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in fatTipConst)
                            Variables.Query_ += $" a.STHAR_FTIRSIP='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                    Variables.Counter_ = 0;
                if (filterDic == null)
                    filterDic = new();
                
                SqlParameter[] parameters = new SqlParameter[1 + filterDic.Count];
                parameters[0] = new SqlParameter("@pageNumber",SqlDbType.Int);
                parameters[0].Value = (pageNumber - 1) * 25;
                Variables.Counter_ = 1;

                foreach(var item in filterDic)
                {
                    switch (item.Key.ToString())
                    {
                        case "Stok Kodu":
                            Variables.Query_ += " and a.stok_kodu like '%' + @stokKodu + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar,35);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "Stok Adı":
                            Variables.Query_ += " and stok_adi like '%' + @stokAdi + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokAdi", SqlDbType.NVarChar,100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "Fiş No":
                            Variables.Query_ += " and a.fisno like '%' + @fisno + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@fisno", SqlDbType.NVarChar,15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "BaslangicTarih":

                            string[] filterArray;
                            filterArray = filterDic["BaslangicTarih"].Split(';');
                            Variables.Query_ += string.Format(" and a.sthar_tarih {0} @baslangicTarih ", filterArray[0]);
                            parameters[Variables.Counter_] = new SqlParameter("@baslangicTarih", SqlDbType.DateTime);
                            parameters[Variables.Counter_].Value = Convert.ToDateTime(filterArray[1]);
                            Variables.Counter_ ++;
                            break;
                        case "BitisTarih":
                            string[] filterArrayB;
                            filterArrayB = filterDic["BitisTarih"].Split(';');
                            Variables.Query_ += string.Format(" and a.sthar_tarih {0} @bitisTarih ", filterArrayB[0]);
                            parameters[Variables.Counter_] = new SqlParameter("@bitisTarih", SqlDbType.DateTime);
                            parameters[Variables.Counter_].Value = Convert.ToDateTime(filterArrayB[1]);
                            Variables.Counter_ ++;
                            break;
                        case "Açıklama":
                            Variables.Query_ += " and a.sthar_aciklama like '%' + @aciklama + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@aciklama", SqlDbType.NVarChar,100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "Ekalan":
                            Variables.Query_ += " and a.ekalan like '%' + @ekalan + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@ekalan", SqlDbType.NVarChar,100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "Takip No":
                            Variables.Query_ += " and kt_takipnum like '%' + @takipNo + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@takipNo", SqlDbType.NVarChar,15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                        case "Sipariş Numarası":
                            Variables.Query_ += " and a.sthar_sipnum like '%' + @siparisNumarasi + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar,15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;
                            
                        case "Sipariş Sıra":
                            Variables.Query_ += " and a.stra_sipkont = @siparisSira ";
                            parameters[Variables.Counter_] = new SqlParameter("@siparisSira", SqlDbType.Int);
                            parameters[Variables.Counter_].Value = Convert.ToInt32(item.Value);
                            Variables.Counter_ ++;
                            break;
                            
                        case "G-C":
                            Variables.Query_ += " and a.sthar_gckod = @gckod ";
                            parameters[Variables.Counter_] = new SqlParameter("@gckod", SqlDbType.NVarChar,1);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_ ++;
                            break;

                    }
                }

                Variables.Query_ += $" order by sthar_tarih desc offset @pageNumber rows fetch next 25 rows only";

                temp_stok_hareket_coll.Clear();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,fabrika))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Cls_Depo depoItem = new Cls_Depo
                            {
                                HareketID = Convert.ToInt32(reader["inckeyno"]),
                                StokKodu = reader["STOK_KODU"].ToString(),
                                StokAdi = reader["stok_adi"].ToString(),
                                FisNo = reader["FISNO"].ToString(),
                                HareketMiktar = Convert.ToSingle(reader["GC_MIK"]),
                                GirisCikisKod = reader["GCKOD"].ToString(),
                                DepoKodu = Convert.ToInt32(reader["DEPO_KODU"]),
                                HareketTarih =Convert.ToDateTime(reader["TARIH"]),
                                HareketAciklama = reader["ACIK"].ToString(),
                                Ekalan = reader["ekalan"].ToString(),
                                TakipNo = reader["kt_takipnum"].ToString(),
                                SiparisNumarasi = reader["SIPNUM"].ToString(),
                                SiparisSira = Convert.ToInt32(reader["STRA_SIPKONT"]),
                                Kod1 = reader["kod_1"].ToString(),
                                HareketTipiKodu = reader["STHAR_HTUR"].ToString(),
                                BelgeTipiKodu = reader["STHAR_BGTIP"].ToString(),
                                FaturaTipiKodu = reader["STHAR_FTIRSIP"].ToString(),

                            };
                            switch(depoItem.HareketTipiKodu)
                            {
                                case "A":
                                    depoItem.HareketTipi = "Devir";
                                    break;
                                case "C":
                                    depoItem.HareketTipi = "Üretim";
                                    break;
                                case "J":
                                    depoItem.HareketTipi = "Fatura";
                                    break;
                                case "H":
                                    depoItem.HareketTipi = "İrsaliye";
                                    break;
                                case "B":
                                    depoItem.HareketTipi = "DAT";
                                    break;
                                case "N":
                                    depoItem.HareketTipi = "Faturalaşmış İrsaliye";
                                    break;
                                case "L":
                                    depoItem.HareketTipi = "İade";
                                    break;
                                default:
                                    depoItem.BelgeTipi = string.Empty;
                                    break;
                            }
                            switch(depoItem.BelgeTipiKodu)
                            {
                                case "F":
                                    depoItem.BelgeTipi = "Fatura";
                                    break;
                                case "I":
                                    depoItem.BelgeTipi = "Irsaliye";
                                    break;
                                case "U":
                                    depoItem.BelgeTipi = "Uretim Giris";
                                    break;
                                case "V":
                                    depoItem.BelgeTipi = "Uretim Cikis";
                                    break;
                                default:
                                    depoItem.BelgeTipi = string.Empty;
                                    break;
                            }
                            switch(depoItem.FaturaTipiKodu)
                            {
                                case "1":
                                    depoItem.FaturaTipi = "Satış Faturası";
                                    break;
                                case "2":
                                    depoItem.FaturaTipi = "Alış Faturası";
                                    break;
                                case "3":
                                    depoItem.FaturaTipi = "Satış İrsaliyesi";
                                    break;
                                case "4":
                                    depoItem.FaturaTipi = "Alış İrsaliyesi";
                                    break;
                                case "6":
                                    depoItem.FaturaTipi = "Müşteri Siparişi";
                                    break;
                                case "7":
                                    depoItem.FaturaTipi = "Satıcı Siparişi";
                                    break;
                                case "8":
                                    depoItem.FaturaTipi = "DAT Giriş";
                                    break;
                                case "9":
                                    depoItem.FaturaTipi = "DAT Çıkış";
                                    break;
                                case "A":
                                    depoItem.FaturaTipi = "Saklanmış İrsaliye";
                                    break;
                                default:
                                    depoItem.FaturaTipi = string.Empty;
                                    break;
                            }
                            temp_stok_hareket_coll.Add(depoItem);
                        }
                    }

                    else
                    {
                        Cls_Depo depoItem = new Cls_Depo
                        {
                            StokKodu = "BOS",

                        };
                        temp_stok_hareket_coll.Add(depoItem);
                    }
                }

                StokHareketCollection = temp_stok_hareket_coll;
                return StokHareketCollection;

            }
            catch { return null; }
        }
        public int CountStokHareketList(Dictionary<string, string> filterDic, int pageNumber,string fabrika, List<int> depoKoduConst, List<string> kod1Const, List<string> harTipConst, List<string> belTipConst, List<string> fatTipConst)
        {
            try
            {

                Variables.Query_ = "SELECT count(*) FROM TBLSTHAR a (NOLOCK) " +
                                   "left join tblstsabit b (NOLOCK) on a.stok_kodu = b.stok_kodu left join tblisemriek c (NOLOCK) on a.sthar_sipnum = c.isemri WHERE 1 = 1 ";


                if (depoKoduConst != null)
                {
                    if (depoKoduConst.Count > 0)
                    {
                        Variables.Query_ += " and (";
                        foreach (int item in depoKoduConst)
                            Variables.Query_ += $" a.depo_kodu={item} or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (kod1Const != null)
                {
                    if (kod1Const.Count > 0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in kod1Const)
                            Variables.Query_ += $" kod_1='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (harTipConst != null)
                {
                    if (harTipConst.Count > 0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in harTipConst)
                            Variables.Query_ += $" a.sthar_htur='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (belTipConst != null)
                {
                    if (belTipConst.Count > 0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in belTipConst)
                            Variables.Query_ += $" a.STHAR_BGTIP='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                if (fatTipConst != null)
                {
                    if (fatTipConst.Count > 0)
                    {
                        Variables.Query_ += " and (";
                        foreach (string item in fatTipConst)
                            Variables.Query_ += $" a.STHAR_FTIRSIP='{item}' or";
                        Variables.Query_ = Variables.Query_.Substring(0, Variables.Query_.Length - 3);
                        Variables.Query_ += ")";
                    }
                }
                Variables.Counter_ = 0;
                if (filterDic == null)
                    filterDic = new();

                SqlParameter[] parameters = new SqlParameter[filterDic.Count];
                Variables.Counter_ = 0;

                foreach (var item in filterDic)
                {
                    switch (item.Key.ToString())
                    {
                        case "Stok Kodu":
                            Variables.Query_ += " and a.stok_kodu like '%' + @stokKodu + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Stok Adı":
                            Variables.Query_ += " and stok_adi like '%' + @stokAdi + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Fiş No":
                            Variables.Query_ += " and a.fisno like '%' + @fisno + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "BaslangicTarih":
                            Variables.Query_ += " and a.sthar_tarih @baslangicTarih ";
                            parameters[Variables.Counter_] = new SqlParameter("@baslangicTarih", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "BitisTarih":
                            Variables.Query_ += " and a.sthar_tarih @bitisTarih ";
                            parameters[Variables.Counter_] = new SqlParameter("@bitisTarih", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Açıklama":
                            Variables.Query_ += " and a.sthar_aciklama like '%' + @aciklama + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Ekalan":
                            Variables.Query_ += " and a.ekalan like '%' + @ekalan + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@ekalan", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Takip No":
                            Variables.Query_ += " and kt_takipnum like '%' + @takipNo + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;
                        case "Sipariş Numarası":
                            Variables.Query_ += " and a.sthar_sipnum like '%' + @siparisNumarasi + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                            parameters[Variables.Counter_].Value = item.Value.ToString();
                            Variables.Counter_++;
                            break;

                        case "Sipariş Sıra":
                            Variables.Query_ += " and a.stra_sipkont = @siparisSira ";
                            parameters[Variables.Counter_] = new SqlParameter("@siparisSira", SqlDbType.Int);
                            parameters[Variables.Counter_].Value = Convert.ToInt32(item.Value);
                            Variables.Counter_++;
                            break;

                    }
                }


                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_,Variables.Yil_,parameters,fabrika);
               return Variables.ResultInt_ ;
            }
            catch { return -1; }
        }
        public ObservableCollection<Cls_Depo> PopulateSerbestDATList(string hamKodu, string kod1, int pageNumber)
        {
            try
            {
                Variables.Query_ = @"select sabit.stok_kodu,sabit.stok_adi,isnull(sabit.depo_kodu,10) as cikisDepo
                                    , isnull(sabit.depo_kodu,10) as girisDepo,kod_1
                                    , olcu_br1,round(isnull(ph.miktar,0),4) as girisDepoBakiye,round(isnull(ph.miktar,0),4) as cikisDepoBakiye 
                                      from tblstsabit sabit (nolock) 
                                     left join (select round(isnull(top_giris_mik,0),4) - round(isnull(top_cikis_mik,0),4) as miktar,stok_kodu,depo_kodu from tblstokph) ph on ph.stok_kodu = sabit.stok_kodu and sabit.depo_kodu=ph.depo_kodu 
                                      where sabit.stok_kodu like + '%' + @hamKodu + '%' and (sabit.stok_kodu like 'SR%' or sabit.stok_kodu like 'HM%' OR sabit.stok_kodu like '730%')
                                            and kod_1 like + '%' + @kod1 + '%'";
                if (hamKodu == null)
                    hamKodu = string.Empty;

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new("@hamKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = hamKodu;
                parameters[1] = new("@kod1", SqlDbType.NVarChar, 100);
                parameters[1].Value = kod1;
               
                Variables.Query_ += $" order by sabit.stok_kodu desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                ObservableCollection<Cls_Depo> tempColl = new();

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        Cls_Depo depo = new Cls_Depo
                        {
                            StokKodu = reader["stok_kodu"] is DBNull ? "" : reader["stok_kodu"].ToString(),
                            StokAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString(),
                            CikisDepoKodu = reader["cikisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["cikisDepo"]),
                            GirisDepoKodu = reader["girisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["girisDepo"]),
                            Kod1 = reader["kod_1"] is DBNull ? "" : reader["kod_1"].ToString(),
                            Birim = reader["olcu_br1"] is DBNull ? "" : reader["olcu_br1"].ToString(),
                            GirisDepoBakiye = reader["girisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["girisDepoBakiye"]),
                            CikisDepoBakiye = reader["cikisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["cikisDepoBakiye"]),
                            IsChecked = false,
                        };
                        tempColl.Add(depo);
                    }
                    reader.Close();
                }
                return tempColl;
            }
            catch
            {
                return null;
            }
        }
        public int CountSerbestDATList(string hamKodu,string kod1, int pageNumber)
        {
            try
            {
                Variables.Query_ = @"select count(*) from tblstsabit sabit (nolock) 
                                     left join (select round(isnull(top_giris_mik,0),4) - round(isnull(top_cikis_mik,0),4) as miktar,stok_kodu,depo_kodu from tblstokph) ph on ph.stok_kodu = sabit.stok_kodu and sabit.depo_kodu=ph.depo_kodu 
                                      where sabit.stok_kodu like + '%' + @hamKodu + '%' and (sabit.stok_kodu like 'SR%' or sabit.stok_kodu like 'HM%' OR sabit.stok_kodu like '730%')";

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new("@hamKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = hamKodu;
                parameters[1] = new("@kod1", SqlDbType.NVarChar, 100);
                parameters[1].Value = kod1;
                if (hamKodu == null)
                    hamKodu = string.Empty;
                if (!string.IsNullOrEmpty(kod1))
                    Variables.Query_ += " and kod_1 = @kod1 ";

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        Variables.ResultInt_ = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public ObservableCollection<Cls_Depo> GetTakipNos(bool? isCheckedDosemeli,bool? isCheckedModuler,string searchText)
        {
            Variables.Query_ = @"with cte as (select distinct substring(kt_takipnum,1,5) as TakipNo from tblisemriek (nolock)) 
                                   select top 30 * from cte where 1=1 ";

            if (isCheckedModuler == true)
                Variables.Query_ += " and TakipNo like 'M%' ";
            if (isCheckedDosemeli == true)
                Variables.Query_ += " and TakipNo like 'D%' ";
            if (!string.IsNullOrEmpty(searchText))
                Variables.Query_ += string.Format(" and TakipNo like '%{0}%'",searchText);

            Variables.Query_ += " order by TakipNo desc";

            ObservableCollection<Cls_Depo> depoColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
            {
                Cls_Depo model = new();
                model.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                model.IsChecked = false;
                return model;
            });
            return depoColl;

        }
        public int CountTakipNos(bool? isCheckedDosemeli, bool? isCheckedModuler, string searchText)
        {
            try
            {

                Variables.Query_ = @"with cte as (select distinct substring(kt_takipnum,1,5) as TakipNo from tblisemriek (nolock)) 
                                       select count(*) from cte where 1=1 ";

                if (isCheckedModuler == true)
                    Variables.Query_ += " and TakipNo like 'M%' ";
                if (isCheckedDosemeli == true)
                    Variables.Query_ += " and TakipNo like 'D%' ";
                if (!string.IsNullOrEmpty(searchText))
                    Variables.Query_ += string.Format(" and TakipNo like '%{0}%'", searchText);


                Variables.ResultInt_ = data.Get_One_Int_Result_Command(Variables.Query_, Variables.Yil_, Variables.Fabrika_);

                return Variables.ResultInt_;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public ObservableCollection<Cls_Depo> GetTakipNos(bool? isCheckedDosemeli, bool? isCheckedModuler, string searchText, int pageNumber)
        {
            Variables.Query_ = @"with cte as (select distinct substring(kt_takipnum,1,5) as TakipNo from tblisemriek (nolock)) 
                                   select TakipNo from cte where 1=1 ";

            if (isCheckedModuler == true)
                Variables.Query_ += " and TakipNo like 'M%' ";
            if (isCheckedDosemeli == true)
                Variables.Query_ += " and TakipNo like 'D%' ";
            if (!string.IsNullOrEmpty(searchText))
                Variables.Query_ += string.Format(" and TakipNo like '%{0}%'", searchText);

            Variables.Query_ += " order by TakipNo desc";
            Variables.Query_ += string.Format(" offset {0} rows fetch next 30 rows only", (pageNumber - 1) * 30);

            ObservableCollection<Cls_Depo> depoColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
            {
                Cls_Depo model = new();
                model.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                model.IsChecked = false;
                return model;
            });
            return depoColl;

        }
        public ObservableCollection<Cls_Depo> PopulateDATKaydedilecekListesi(Dictionary<string, string> kisitPairs, string fabrika, int collectionCount)
        {
            try
            {
                Variables.Query_ = "select * from vbvDATListe where hamKodu like 'hm%' and takipNo is not null and takipNo<>'' and kalanDATmiktar>0 ";

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and takipNo like '%' + @takipNo + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamKodu"] != null)
                {
                    Variables.Query_ = Variables.Query_ + "and hamKodu like '%' + @hamKodu + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and hamAdi like '%' + @hamAdi + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod1 = @kod1 ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod5 = @kod5 ";
                        Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_ + collectionCount];

                if (collectionCount > 0)
                {
                    Variables.Query_ += " and (";
                    for (int i = 0; i < collectionCount; i++)
                    {
                        if (kisitPairs.ContainsKey(string.Format("TakipNo{0}", i)))
                        {
                            if (i != 0)
                                Variables.Query_ += " or takipNo like '%' + @takipNo" + i + " + '%' ";
                            else
                                Variables.Query_ += " takipNo like '%' + @takipNo" + i + " + '%' ";
                            int parametersLine = Variables.Counter_ + i;

                            parameters[parametersLine] = new SqlParameter(string.Format("@takipNo{0}", i), SqlDbType.NVarChar, 8);
                            parameters[parametersLine].Value = kisitPairs[string.Format("TakipNo{0}", i)];
                        }
                    }
                    Variables.Query_ += ")";
                }
                

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["takipNo"];
                    Variables.Counter_++;
                }

                if (kisitPairs["hamKodu"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["hamKodu"];
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = kisitPairs["hamAdi"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod1"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod5", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod5"];
                    Variables.Counter_++;
                }

                temp_depo_coll.Clear();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Cls_Depo depoItem = new Cls_Depo
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                TakipNo = reader["takipNo"].ToString(),
                                EskiStokKodu = reader["hamKodu"].ToString(),
                                StokKodu = reader["hamKodu"].ToString(),
                                StokAdi = reader["hamAdi"].ToString(),
                                Kod5 = reader["kod5"].ToString(),
                                Kod1 = reader["kod1"].ToString(),
                                ToplamDATIhtiyacMiktar = Convert.ToSingle(reader["toplamDATIhtiyac"]),
                                GonderilenDATMiktar = Convert.ToSingle(reader["gonderilenDATMiktar"]),
                                KalanDATMiktar = Convert.ToSingle(reader["kalanDATMiktar"]),
                                GonderilecekDATMiktar = Convert.ToSingle(reader["kalanDATMiktar"]),
                                IsChecked = true,

                            };

                            if (Variables.Departman_.Contains("Doseme"))
                            {
                                depoItem.CikisDepoKodu = 10;
                                depoItem.GirisDepoKodu = 15;
                                depoItem.CikisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.CikisDepoKodu));
                                depoItem.GirisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.GirisDepoKodu));
                            }

                            else if (Variables.Departman_.Contains("Moduler"))
                            {
                                depoItem.CikisDepoKodu = 30;
                                depoItem.GirisDepoKodu = 35;
                                depoItem.CikisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.CikisDepoKodu));
                                depoItem.GirisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.GirisDepoKodu));

                            }
                            else
                            {

                                if (Convert.ToInt32(reader["girisDepo"]) != 0)
                                    depoItem.GirisDepoKodu = Convert.ToInt32(reader["girisDepo"]);

                                else
                                    depoItem.GirisDepoKodu = 15;

                                if (Convert.ToInt32(reader["cikisDepo"]) != 0)
                                    depoItem.CikisDepoKodu = Convert.ToInt32(reader["cikisDepo"]);

                                else
                                    depoItem.CikisDepoKodu = 10;


                                depoItem.CikisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.CikisDepoKodu));
                                depoItem.GirisDepoBakiye = Convert.ToSingle(GetDepoBakiye(depoItem.StokKodu, depoItem.GirisDepoKodu));

                            }

                            temp_depo_coll.Add(depoItem);
                        } 
                    }

                    else
                    {
                        Cls_Depo depoItem = new Cls_Depo
                        {
                            StokKodu = "BOS",
                        };
                        temp_depo_coll.Add(depoItem);
                    }
                }

                DepoCollection = temp_depo_coll;

                return DepoCollection;

            }
            catch 
            {
                return null;
            }
        }
        public int CountDATKaydedilecekListesi(Dictionary<string, string> kisitPairs, string fabrika, int collectionCount,int pageNumber)
        {
            try
            {
                Variables.Query_ = "select Count(*) from vbvDATListe where hamKodu like 'hm%' and takipNo is not null and takipNo<>'' and kalanDATmiktar>0 ";

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and takipNo like '%' + @takipNo + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamKodu"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and hamKodu like '%' + @hamKodu + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and hamAdi like '%' + @hamAdi + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod1 = @kod1 ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod5 = @kod5 ";
                        Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_ + collectionCount];

                if (collectionCount > 0)
                {
                    Variables.Query_ += " and (";
                    for (int i = 0; i < collectionCount; i++)
                    {
                        if (kisitPairs.ContainsKey(string.Format("TakipNo{0}", i)))
                        {
                            if (i != 0)
                                Variables.Query_ += " or takipNo like '%' + @takipNo" + i + " + '%' ";
                            else
                                Variables.Query_ += " takipNo like '%' + @takipNo" + i + " + '%' ";
                            int parametersLine = Variables.Counter_ + i;

                            parameters[parametersLine] = new SqlParameter(string.Format("@takipNo{0}", i), SqlDbType.NVarChar, 8);
                            parameters[parametersLine].Value = kisitPairs[string.Format("TakipNo{0}", i)];
                        }
                    }
                    Variables.Query_ += ")";
                }
                

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["takipNo"];
                    Variables.Counter_++;
                }

                if (kisitPairs["hamKodu"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["hamKodu"];
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = kisitPairs["hamAdi"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod1"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod5", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod5"];
                    Variables.Counter_++;
                }


                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;

            }
            catch 
            {
                return -1;
            }
        }
        public ObservableCollection<Cls_Depo> PopulateDATKaydedilecekListesi(Dictionary<string, string> kisitPairs, string fabrika, int collectionCount,int pageNumber)
        {
            try
            {
                Variables.Query_ = "select * from vbvDATListe where hamKodu like 'hm%' and takipNo is not null and takipNo<>'' and kalanDATmiktar>0 ";

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and takipNo like '%' + @takipNo + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamKodu"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and hamKodu like '%' + @hamKodu + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and hamAdi like '%' + @hamAdi + '%' ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod1 = @kod1 ";
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    Variables.Query_ = Variables.Query_ + " and kod5 = @kod5 ";
                        Variables.Counter_++;
                }
                SqlParameter[] parameters = new SqlParameter[Variables.Counter_ + collectionCount];
                
                
                    

                if (collectionCount > 0)
                {
                    Variables.Query_ += " AND (";
                    for (int i = 0; i < collectionCount; i++)
                    {
                        if (kisitPairs.ContainsKey(string.Format("TakipNo{0}", i)))
                        {
                            if (i != 0)
                                Variables.Query_ += " or takipNo like '%' + @takipNo" + i + " + '%' ";
                            else
                                Variables.Query_ += " takipNo like '%' + @takipNo" + i + " + '%' ";
                            int parametersLine = Variables.Counter_ + i;

                            parameters[parametersLine] = new SqlParameter(string.Format("@takipNo{0}", i), SqlDbType.NVarChar, 8);
                            parameters[parametersLine].Value = kisitPairs[string.Format("TakipNo{0}", i)];
                        }
                    }
                    Variables.Query_ += ")";
                }
                

                Variables.Counter_ = 0;

                if (kisitPairs["takipNo"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["takipNo"];
                    Variables.Counter_++;
                }

                if (kisitPairs["hamKodu"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["hamKodu"];
                    Variables.Counter_++;
                }
                if (kisitPairs["hamAdi"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@hamAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = kisitPairs["hamAdi"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod1"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod1"];
                    Variables.Counter_++;
                }
                if (kisitPairs["kod5"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod5", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["kod5"];
                    Variables.Counter_++;
                }
                Variables.Query_ += " order by takipNo desc,hamKodu desc ";
                Variables.Query_ += $"offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                temp_depo_coll.Clear();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Cls_Depo depoItem = new Cls_Depo
                            {
                                TakipNo = reader["takipNo"].ToString(),
                                StokKodu = reader["hamKodu"].ToString(),
                                StokAdi = reader["hamAdi"].ToString(),
                                Kod5 = reader["kod5"].ToString(),
                                Kod1 = reader["kod1"].ToString(),
                                CikisDepoBakiye = Convert.ToSingle(reader["cikisDepoBakiye"]),
                                GirisDepoBakiye = Convert.ToSingle(reader["girisDepoBakiye"]),
                                ToplamDATIhtiyacMiktar = Convert.ToSingle(reader["toplamDATIhtiyac"]),
                                GonderilenDATMiktar = Convert.ToSingle(reader["gonderilenDATMiktar"]),
                                KalanDATMiktar = Convert.ToSingle(reader["kalanDATMiktar"]),
                                GonderilecekDATMiktar = Convert.ToSingle(reader["kalanDATMiktar"]),
                                IsChecked = true,

                            };

                            if (Convert.ToInt32(reader["cikisDepo"]) != 0)
                                depoItem.CikisDepoKodu = Convert.ToInt32(reader["cikisDepo"]);
                            else
                            {
                                if (Variables.Departman_.Contains("Doseme"))
                                    depoItem.CikisDepoKodu = 10;

                                else
                                    depoItem.CikisDepoKodu = 30;
                            }

                            if (Convert.ToInt32(reader["girisDepo"]) != 0)
                                depoItem.GirisDepoKodu = Convert.ToInt32(reader["girisDepo"]);
                            else
                            {
                                if (Variables.Departman_.Contains("Doseme"))
                                    depoItem.GirisDepoKodu = 15;

                                else
                                    depoItem.GirisDepoKodu = 35;
                            }

                            temp_depo_coll.Add(depoItem);
                        } 
                    }

                    else
                    {
                        Cls_Depo depoItem = new Cls_Depo
                        {
                            StokKodu = "BOS",
                        };
                        temp_depo_coll.Add(depoItem);
                    }
                }

                DepoCollection = temp_depo_coll;
                return DepoCollection;

            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Depo> PopulateEksiBakiyeListesi(int depoKodu)
        {
            try
            {

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@depoKodu", SqlDbType.SmallInt);
                parameter[0].Value = depoKodu;

                temp_depo_coll.Clear();
                using (DataTable dataTable = data.Stored_Proc_With_Parameters_Returns_Table("vbpEksiBakiyeListe", Variables.Yil_, parameter)) 
                {
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {

                            Cls_Depo depoItem = new Cls_Depo
                            {
                                StokKodu = dr["STOK_KODU"].ToString(),
                                StokAdi = dr["STOK_ADI"].ToString(),
                                Kod1 = dr["KOD_1"].ToString(),
                                BakiyeAranan = Convert.ToSingle(dr["BAKIYE"]),
                                Bakiye10 = Convert.ToSingle(dr["BAKIYE10"]),
                                Bakiye15 = Convert.ToSingle(dr["BAKIYE15"]),
                                Bakiye30 = Convert.ToSingle(dr["BAKIYE30"]),
                                Bakiye35 = Convert.ToSingle(dr["BAKIYE35"]),
                                Bakiye40 = Convert.ToSingle(dr["BAKIYE40"]),
                                Bakiye45 = Convert.ToSingle(dr["BAKIYE45"]),
                            };
                            temp_depo_coll.Add(depoItem);
                        }
                    }
                }

                DepoCollection = temp_depo_coll;
                return DepoCollection;

            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Depo> PopulateDatDetay(string takipNo,string hamKodu)
        {
            try
            {
                Variables.Query_ = "select * from vbvDatDetay where Substring(TakipNo,1,5)=@takipNo and HamKodu=@hamKodu";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@takipNo", SqlDbType.NVarChar,15);
                parameters[0].Value = takipNo;
                parameters[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar,35);
                parameters[1].Value = hamKodu;

                ObservableCollection<Cls_Depo> depoColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Depo temp_ = new();
                    temp_.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    temp_.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    temp_.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                    temp_.ReferansIsemriNo = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
                    temp_.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    temp_.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                    temp_.IsemriNo = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                    temp_.SiparisMiktar = reader["SiparisMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisMiktar"]);
                    temp_.MamulKodu = reader["YariMamulKodu"] is DBNull ? "" : reader["YariMamulKodu"].ToString();
                    temp_.MamulAdi = reader["YariMamulAdi"] is DBNull ? "" : reader["YariMamulAdi"].ToString();
                    temp_.IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["IsemriMiktar"]);
                    temp_.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                    temp_.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
                    temp_.Kod5 = reader["Kod5"] is DBNull ? "" : reader["Kod5"].ToString();
                    temp_.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    temp_.CikisDepoKodu = reader["CikisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["CikisDepo"]);
                    temp_.GirisDepoKodu = reader["GirisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["GirisDepo"]);
                    temp_.CikisDepoBakiye = reader["CikisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["CikisDepoBakiye"]);
                    temp_.GirisDepoBakiye = reader["GirisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["GirisDepoBakiye"]);
                    temp_.ReceteBirimMiktar = reader["BirimMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["BirimMiktar"]);
                    temp_.ToplamDATIhtiyacMiktar = reader["IhtiyacMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["IhtiyacMiktar"]);
                    temp_.GonderilenDATMiktar = reader["TakipNoDatMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["TakipNoDatMiktar"]);

                    return temp_;
                });


                return depoColl;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Depo> GetDATDetay(Cls_Depo datDetay)
        {
            try
            {
                if (datDetay == null)
                    return null;

                SqlParameter[] sp = new SqlParameter[2];
                sp[0] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 15);
                sp[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                sp[0].Value = datDetay.TakipNoGenel;
                sp[1].Value = datDetay.StokKodu;

                Variables.Query_ = "select * from vbvDATDetay where hamKodu=@hamKodu and takipNo like @takipNo + '%'";
                ObservableCollection<Cls_Depo> depoColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, sp, reader =>
                {
                    Cls_Depo temp_ = new();
                    temp_.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    temp_.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    temp_.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                    temp_.ReferansIsemriNo = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
                    temp_.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    temp_.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                    temp_.IsemriNo = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                    temp_.SiparisMiktar = reader["SiparisMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisMiktar"]);
                    temp_.MamulKodu = reader["YariMamulKodu"] is DBNull ? "" : reader["YariMamulKodu"].ToString();
                    temp_.MamulAdi = reader["YariMamulAdi"] is DBNull ? "" : reader["YariMamulAdi"].ToString();
                    temp_.IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["IsemriMiktar"]);
                    temp_.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                    temp_.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
                    temp_.Kod5 = reader["Kod5"] is DBNull ? "" : reader["Kod5"].ToString();
                    temp_.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    temp_.DepoKodu = reader["CikisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["CikisDepo"]);
                    temp_.GirisDepoKodu = reader["GirisDepo"] is DBNull ? 0 : Convert.ToInt32(reader["GirisDepo"]);
                    temp_.CikisDepoBakiye = reader["CikisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["CikisDepoBakiye"]);
                    temp_.GirisDepoBakiye = reader["GirisDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["GirisDepoBakiye"]);
                    temp_.ReceteBirimMiktar = reader["BirimMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["BirimMiktar"]);
                    temp_.ToplamDATIhtiyacMiktar = reader["IhtiyacMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["IhtiyacMiktar"]);
                    temp_.GonderilenDATMiktar = reader["TakipNoDatMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["TakipNoDatMiktar"]);

                    return temp_;
                });
                return depoColl;

            }
            catch 
            {
                return null;
            }
        }
        public double GetDepoBakiye(string stokKodu,int depoKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(stokKodu))
                    return -1;

                Variables.Query_ = "select isnull((top_giris_mik - top_cikis_mik),0) as miktar from tblstokph where stok_kodu = @stokKodu and depo_kodu=@depoKodu";

                SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35) { Value = stokKodu },
                    new SqlParameter("@depoKodu", SqlDbType.Int) { Value = depoKodu }
                };

                double result = data.Get_One_Double_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, sp, Variables.Fabrika_);
                if (result == -1)
                    result = 0;
                return result;
            }
            catch 
            {
                return -1;
            }
        }
        public ObservableCollection<int> GetDistinctDepoKodu()
        {
            try
            {
                Variables.Query_ = "Select distinct depo_kodu from tblstsabit (nolock) where depo_kodu <> 0 order by depo_kodu asc";
                int depoKodu = 0;
                ObservableCollection<int> depoKoduCollection = new();
                if(DepoCollection != null)
                    DepoCollection.Clear();

                using(SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,Variables.Fabrika_))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read()) 
                    {

                        depoKodu = Convert.ToInt32(reader[0]);

                        depoKoduCollection.Add(depoKodu);
                    }
                }
                return depoKoduCollection;
            }
            catch 
            {
                return null;
            }
        }
        public int GetDepoKodu(string stokKodu)
        {
            try
            {
                Variables.Query_ = "Select isnull(depo_kodu,10) as depo_kodu from tblstsabit (nolock) where depo_kodu <> 0 and stok_kodu=@stokKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;
                int depoKodu = 0;

                using(SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter))
                {
                    if (reader == null)
                        return 10;

                    while (reader.Read()) 
                    {

                        depoKodu = Convert.ToInt32(reader[0]);

                    }
                }
                return depoKodu;
            }
            catch 
            {
                return 10;
            }
        }
        public ObservableCollection<Cls_Depo> GetDepoKoduForListBox()
        {
            try
            {
                Variables.Query_ = "Select distinct depo_kodu from tblstsabit (nolock) where depo_kodu <> 0 order by depo_kodu asc";
                int depoKodu = 0;
                ObservableCollection<Cls_Depo> depoKoduCollection = new();
                if(DepoCollection != null)
                    DepoCollection.Clear();

                using(SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,Variables.Fabrika_))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read()) 
                    {
                        Cls_Depo depo = new();
                        depo.DepoKodu = Convert.ToInt32(reader[0]);
                        depo.IsChecked = false;
                        depoKoduCollection.Add(depo);
                    }
                }
                return depoKoduCollection;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Depo> GetKod1ForListView()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_1 from tblstsabit (nolock) order by kod_1";
                string kod1 = string.Empty;
                ObservableCollection<Cls_Depo> kod1Collection = new();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,variables.Fabrika))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read())
                    {
                        Cls_Depo depo= new();
                        depo.Kod1 = reader[0].ToString();
                        depo.IsChecked = false;
                        kod1Collection.Add(depo);
                    }
                }
                return kod1Collection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<string> GetDistinctKod1()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_1 from tblstsabit (nolock) order by kod_1 asc";
                string kod1 = string.Empty;
                ObservableCollection<string> kod1Collection = new();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,variables.Fabrika))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read())
                    {
                        kod1 = reader[0].ToString();

                        kod1Collection.Add(kod1);
                    }
                }
                return kod1Collection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<string> GetDistinctKod5()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_5 from tblstsabit (nolock) order by kod_5 asc";
                string kod5 = string.Empty;
                ObservableCollection<string> kod5Collection = new();
                
                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read())
                    {
                        kod5 = reader[0].ToString();

                        kod5Collection.Add(kod5);
                    }
                }
                return kod5Collection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Depo> GetTakipNos()
        {
            try
            {
                Variables.Query_ = @"with cte as (Select distinct substring(kt_takipnum,1,5) as TakipNo from tblisemriek )
                                        select top 30 TakipNo from cte where 1=1 ";

                if (Variables.Departman_.Contains("Moduler"))
                    Variables.Query_ += "and TakipNo like 'M%'";
                if (Variables.Departman_.Contains("Doseme"))
                    Variables.Query_ += "and TakipNo like 'D%'"; 

                Variables.Query_ += " order by TakipNo desc";

                ObservableCollection<Cls_Depo> takipNoColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Depo model = new();
                    model.TakipNo = reader[0].ToString();
                    model.IsChecked = false;
                    return model;
                });
                
                return takipNoColl;
            }
            catch
            {
                return null;
            }
        }
        private string GetHareketTipiQuery(string hareketTipi)
        {
            try
            {

                string query = string.Empty;
                switch (hareketTipi)
                {
                    case "DAT":
                        query = "and (sthar_ftirsip ='9' or sthar_ftirsip='8') and sthar_htur='B' and sthar_bgtip='I' ";
                        break;
                    case "FATURA":
                        query = "and sthar_ftirsip ='2' and sthar_htur='J' and sthar_bgtip='F' ";
                        break;
                    case "İRSALİYE":
                        query = "and sthar_ftirsip ='4' and sthar_htur='H' and sthar_bgtip='I' ";
                        break;
                    case "SEVK":
                        query = "and sthar_ftirsip ='1' and sthar_htur='J' and sthar_bgtip='F' ";
                        break;
                    case "ÜRETİM":
                        query = "and sthar_htur='C' and (sthar_bgtip='U' or sthar_bgtip='V') ";
                        break;
                    case "VİRMAN":
                        query = "and (sthar_ftirsip ='9' or sthar_ftirsip='8') and sthar_htur='A' and sthar_bgtip='I' ";
                        break;
                    default:
                        query = string.Empty;
                        break;
            }
            return query;

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public ObservableCollection<Cls_Depo> GetStokKarti(Dictionary<string,string> constraintPairs)
        {
            ObservableCollection<Cls_Depo> stokKartiCollection = new();
            try
            {
                
                if(constraintPairs == null || constraintPairs.Count == 0)
                {
                    Cls_Depo depoHata = new Cls_Depo();
                    depoHata.StokAdi = "Stok Kodu, Stok Adı Seçimleri Bulunamadı";
                    stokKartiCollection.Add(depoHata);
                    return stokKartiCollection;
                }

                Variables.Query_ = "Select stok_kodu,stok_adi,isnull(kod_1,'') as kod1,isnull(kod_2,'') as kod2,isnull(kod_3,'') as kod3,isnull(kod_4,'') as kod4,isnull(kod_5,'') as kod5 from tblstsabit (nolock) where 1=1 ";

                Variables.Counter_ = 0;

                if (constraintPairs["stokKodu"] != null)
                {
                    Variables.Query_ = Variables.Query_ + "and stok_kodu like '%' + @stokKodu + '%' ";
                    Variables.Counter_++;
                }

                if (constraintPairs["stokAdi"] != null)
                {
                    Variables.Query_ = Variables.Query_ + "and stok_adi like '%' + @stokAdi + '%' ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;

                if (constraintPairs["stokKodu"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = constraintPairs["stokKodu"];
                    Variables.Counter_++;
                }

                if (constraintPairs["stokAdi"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = constraintPairs["stokAdi"];
                    Variables.Counter_++;
                }
                if(temp_depo_coll.Count > 0) 
                    temp_depo_coll.Clear();
                
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameters, variables.Fabrika))
                {
                    if (reader == null)
                    {
                        Cls_Depo depoHata = new Cls_Depo();
                        depoHata.StokAdi = "Sorgu Boş Sonuç Döndürdü";
                        stokKartiCollection.Add(depoHata);
                        return stokKartiCollection;
                    }
                    while (reader.Read())
                    {
                        Cls_Depo stokKarti = new Cls_Depo
                        {
                            StokKodu = reader[0].ToString(),
                            StokAdi = EntryControls.FixTurkishCharacters(reader[1].ToString()),
                            Kod1 = reader[2].ToString(),
                            Kod2 = reader[3].ToString(),
                            Kod3 = reader[4].ToString(),
                            Kod4 = reader[5].ToString(),
                            Kod5 = reader[6].ToString(),
                        };
                        temp_depo_coll.Add(stokKarti);
                    }

                    stokKartiCollection = temp_depo_coll;
                }
                return stokKartiCollection;
            }
            catch
            {
                Cls_Depo depoHata = new Cls_Depo();
                depoHata.StokAdi = "Veri Tabanına Bağlanırken Hata İle Karşılaşıldı.";
                stokKartiCollection.Add(depoHata);
                return stokKartiCollection;
            }
        }
        public ObservableCollection<Cls_Depo> GetSablonKod(Dictionary<string, string> constraintPairs)
        {
            ObservableCollection<Cls_Depo> stokKartiCollection = new();
            try
            {

                if (constraintPairs == null || constraintPairs.Count == 0)
                {
                    Cls_Depo depoHata = new Cls_Depo();
                    depoHata.StokAdi = "Stok Kodu, Stok Adı Seçimleri Bulunamadı";
                    stokKartiCollection.Add(depoHata);
                    return stokKartiCollection;
                }

                Variables.Query_ = "Select stok_kodu,stok_adi,isnull(kod_1,'') as kod1,isnull(kod_2,'') as kod2,isnull(kod_3,'') as kod3,isnull(kod_4,'') as kod4,isnull(kod_5,'') as kod5 from tblstsabit (nolock) where (stok_kodu like 'M%' OR STOK_KODU LIKE 'SSH%') AND LEN(STOK_KODU) = 11 ";

                Variables.Counter_ = 0;

                if (constraintPairs["stokKodu"] != null)
                {
                    Variables.Query_ = Variables.Query_ + "and stok_kodu like '%' + @stokKodu + '%' ";
                    Variables.Counter_++;
                }

                if (constraintPairs["stokAdi"] != null)
                {
                    Variables.Query_ = Variables.Query_ + "and stok_adi like '%' + @stokAdi + '%' ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;

                if (constraintPairs["stokKodu"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = constraintPairs["stokKodu"];
                    Variables.Counter_++;
                }

                if (constraintPairs["stokAdi"] != null)
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = constraintPairs["stokAdi"];
                    Variables.Counter_++;
                }
                if (temp_depo_coll.Count > 0)
                    temp_depo_coll.Clear();

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                {
                    if (reader == null)
                    {
                        Cls_Depo depoHata = new Cls_Depo();
                        depoHata.StokAdi = "Sorgu Boş Sonuç Döndürdü";
                        stokKartiCollection.Add(depoHata);
                        return stokKartiCollection;
                    }
                    while (reader.Read())
                    {
                        Cls_Depo stokKarti = new Cls_Depo
                        {
                            StokKodu = reader[0].ToString(),
                            StokAdi = reader[1].ToString(),
                            Kod1 = reader[2].ToString(),
                            Kod2 = reader[3].ToString(),
                            Kod3 = reader[4].ToString(),
                            Kod4 = reader[5].ToString(),
                            Kod5 = reader[6].ToString(),
                        };
                        temp_depo_coll.Add(stokKarti);
                    }

                    stokKartiCollection = temp_depo_coll;
                }
                return stokKartiCollection;
            }
            catch
            {
                Cls_Depo depoHata = new Cls_Depo();
                depoHata.StokAdi = "Veri Tabanına Bağlanırken Hata İle Karşılaşıldı.";
                stokKartiCollection.Add(depoHata);
                return stokKartiCollection;
            }
        }
        public string GetFisnoForDAT()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@prefix", SqlDbType.NVarChar, 4);
                parameters[0].Value = "DPIH";
                parameters[1] = new SqlParameter("@tableName", SqlDbType.NVarChar, 128);
                parameters[1].Value = "TBLSTHAR";
                parameters[2] = new SqlParameter("@columnName", SqlDbType.NVarChar, 128);
                parameters[2].Value = "FISNO";
                FisNo = data.Get_One_String_Result_Stored_Proc_With_Parameters("vbpGetFisno", Variables.Yil_, parameters);

                return FisNo;
            }
            catch 
            {
                return string.Empty;
            }

        }
        public bool CheckIfDepoKoduExists(int depoKodu)
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstokdp where depo_kodu=@depoKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("depoKodu",SqlDbType.Int) {Value = depoKodu};
                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_,Variables.Yil_, parameter,Variables.Fabrika_);
                if (Variables.ResultInt_ > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int InsertDAT(ObservableCollection<Cls_Depo> datCollection, string fisno)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@userName", SqlDbType.NVarChar,12);
                parameters[0].Value = login.GetUserName();
                parameters[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[1].Value = fisno;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertDATMas", Variables.Yil_, parameters);
                    if(!Variables.Result_)
                    return 2;

                Variables.Counter_ = 1;
                Variables.QumulativeSum_ = 0;
                foreach (Cls_Depo item in datCollection)
                {

                    SqlParameter[] parametersSatir = new SqlParameter[10];
                    parametersSatir[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar,35);
                    parametersSatir[0].Value = item.EskiStokKodu;
                    parametersSatir[1] = new SqlParameter("@yeniStokKodu", SqlDbType.NVarChar,35);
                    parametersSatir[1].Value = item.StokKodu;
                    parametersSatir[2] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parametersSatir[2].Value = fisno;
                    parametersSatir[3] = new SqlParameter("@miktar", SqlDbType.Float);
                    parametersSatir[3].Value = Convert.ToSingle(Math.Round(item.GonderilecekDATMiktar,4));
                    parametersSatir[4] = new SqlParameter("@kalanMiktar", SqlDbType.Float);
                    parametersSatir[4].Value = Convert.ToSingle(Math.Round(item.GonderilecekDATMiktar   ,4) - Math.Round(item.KalanDATMiktar, 4)) < 0 ? 0 : Convert.ToSingle(Math.Round(item.GonderilecekDATMiktar, 4) - Math.Round(item.KalanDATMiktar, 4));
                    parametersSatir[5] = new SqlParameter("@cikisDepoKodu", SqlDbType.Int);
                    parametersSatir[5].Value = item.CikisDepoKodu;
                    parametersSatir[6] = new SqlParameter("@girisDepoKodu", SqlDbType.Int);
                    parametersSatir[6].Value = item.GirisDepoKodu;
                    parametersSatir[7] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 15);
                    parametersSatir[7].Value = item.TakipNo;
                    parametersSatir[8] = new SqlParameter("@sira", SqlDbType.SmallInt);
                    parametersSatir[8].Value = Variables.Counter_;
                    parametersSatir[9] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                    parametersSatir[9].Value = login.GetUserName();

                    Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertDATSatir",Variables.Yil_, parametersSatir);

                    if (!Variables.Result_)
                        return 3;

                    Variables.Counter_++;
                }
                return 1;
            }
            catch (Exception ex)
            {
                var exm = ex.Message.ToString(); return -1;
            }
        }
        public int InsertDATSerbest(ObservableCollection<Cls_Depo> datCollection, string fisno)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@userName", SqlDbType.NVarChar, 12);
                parameters[0].Value = login.GetUserName();
                parameters[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[1].Value = fisno;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertDATMas", Variables.Yil_, parameters);
                if (!Variables.Result_)
                    return 2;

                Variables.Counter_ = 1;
                Variables.QumulativeSum_ = 0;
                foreach (Cls_Depo item in datCollection)
                {

                    SqlParameter[] parametersSatir = new SqlParameter[6];
                    parametersSatir[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parametersSatir[0].Value = item.StokKodu;
                    parametersSatir[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parametersSatir[1].Value = fisno;
                    parametersSatir[2] = new SqlParameter("@miktar", SqlDbType.Float);
                    parametersSatir[2].Value = Convert.ToSingle(Math.Round(item.GonderilecekDATMiktar, 4));
                    parametersSatir[3] = new SqlParameter("@cikisDepoKodu", SqlDbType.Int);
                    parametersSatir[3].Value = item.CikisDepoKodu;
                    parametersSatir[4] = new SqlParameter("@girisDepoKodu", SqlDbType.Int);
                    parametersSatir[4].Value = item.GirisDepoKodu;
                    parametersSatir[5] = new SqlParameter("@sira", SqlDbType.SmallInt);
                    parametersSatir[5].Value = Variables.Counter_;

                    Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertDATSatirSerbest", Variables.Yil_, parametersSatir);

                    if (!Variables.Result_)
                        return 3;

                    Variables.Counter_++;
                }
                return 1;
            }
            catch (Exception ex)
            {
                var exm = ex.Message.ToString(); return -1;
            }
        }
        public int InsertVirman(Cls_Depo eskiDepoKodu, Cls_Depo yeniDepoKodu)
        {
            try
            {
                if(eskiDepoKodu is null ||
                    yeniDepoKodu is null)
                { return 2; }

                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@eskiStokKodu", SqlDbType.NVarChar,35);
                parameters[0].Value = eskiDepoKodu.StokKodu;
                parameters[1] = new SqlParameter("@eskiMiktar", SqlDbType.Float);
                parameters[1].Value = Math.Round(eskiDepoKodu.HareketMiktar,4);
                parameters[2] = new SqlParameter("@eskiDepoKodu", SqlDbType.Int);
                parameters[2].Value = eskiDepoKodu.DepoKodu;
                parameters[3] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 200);
                parameters[3].Value = eskiDepoKodu.Ekalan;
                parameters[4] = new SqlParameter("@yeniStokKodu", SqlDbType.NVarChar, 35);
                parameters[4].Value = yeniDepoKodu.StokKodu;
                parameters[5] = new SqlParameter("@yeniMiktar", SqlDbType.Float);
                parameters[5].Value = Math.Round(yeniDepoKodu.HareketMiktar,4);
                parameters[6] = new SqlParameter("@yeniDepoKodu", SqlDbType.Int);
                parameters[6].Value = yeniDepoKodu.DepoKodu;
                parameters[7] = new SqlParameter("@tarih", SqlDbType.DateTime);
                parameters[7].Value = eskiDepoKodu.HareketTarih;
                parameters[8] = new SqlParameter("@userName", SqlDbType.NVarChar,12);
                parameters[8].Value = login.GetUserName();

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertVirman", Variables.Yil_, parameters,Variables.Fabrika_);
                if(!Variables.Result_)
                    return 3;

                return 1;
            }
            catch 
            {
                return -1;
            }
        }

        public bool DATGeriAl(string fisno)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpDATGeriAl",Variables.Yil_, parameter);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }

        public ObservableCollection<Cls_Depo> PopulateHareketsizStokList(DateTime tarih, int sorgutipi)
        {
            try
            {
                Variables.Query_ = "vbpHareketGormeyenRapor";

                SqlParameter[] parameters = new SqlParameter[2];

                    parameters[0] = new SqlParameter("@tarih", SqlDbType.DateTime);
                    parameters[0].Value = tarih;
                    parameters[1] = new SqlParameter("@sorguTipi", SqlDbType.Int);
                    parameters[1].Value = sorgutipi;
               

                temp_depo_coll.Clear();
                using (SqlDataReader reader = data.ExecuteStoredProcedureWithParametersReturnsReader(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Cls_Depo depoItem = new Cls_Depo
                            {
                                StokKodu = reader["StokKodu"].ToString(),
                                StokAdi = reader["StokAdi"].ToString(),
                                StokMiktar = Convert.ToInt32(reader["StokMiktar"]),
                            };
                            temp_depo_coll.Add(depoItem);
                        }
                    }

                    if (reader == null)
                    {
                        Cls_Depo depoItem = new Cls_Depo
                        {
                            StokKodu = "BOS",
                        };
                        temp_depo_coll.Add(depoItem);
                    }
                }



                return temp_depo_coll;

            }
            catch { return null; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string getStr)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(getStr));
        }
    }
}


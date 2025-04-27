using Layer_2_Common.Type;
using Layer_Business.ViewModels;
using Layer_Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static Layer_Business.Cls_Base;

namespace Layer_Business
{
    public class Cls_Arge : INotifyPropertyChanged
    {
        private float _kabaBoy = 0;
        private float _kabaEn = 0;
        private float _netBoy = 0;
        private float _netEn = 0;
        private float _bitmisBoy = 0;
        private float _bitmisEn = 0;
        private string _b1KB = "0";
        private string _ekb1 = "0";
        private string _ekb2 = "0";
        private string _b2KB = "0";
        private string _b1KBRecSiraNo = string.Empty;
        private string _ekb1RecSiraNo = string.Empty;
        private string _ekb2RecSiraNo = string.Empty;
        private string _b2KBRecSiraNo = string.Empty;
        private string _aciklama = string.Empty;
        private string _yuzeyDelik = "0";
        private string _cumbaDelik = "0";
        private string _cncKanalBoyu = "0";
        private string _montajSure = "0";
        private string _method = string.Empty;

        public float KabaBoy
        {
            get { return _kabaBoy; }
            set
            {
                if (_kabaBoy != value)
                {
                    _kabaBoy = value;
                    OnPropertyChanged(nameof(KabaBoy));
                }
            }
        }

        public float KabaEn
        {
            get { return _kabaEn; }
            set
            {
                if (_kabaEn != value)
                {
                    _kabaEn = value;
                    OnPropertyChanged(nameof(KabaEn));
                }
            }
        }

        public float NetBoy
        {
            get { return _netBoy; }
            set
            {
                if (_netBoy != value)
                {
                    _netBoy = value;
                    OnPropertyChanged(nameof(NetBoy));
                }
            }
        }

        public float NetEn
        {
            get { return _netEn; }
            set
            {
                if (_netEn != value)
                {
                    _netEn = value;
                    OnPropertyChanged(nameof(NetEn));
                }
            }
        }

        public float BitmisBoy
        {
            get { return _bitmisBoy; }
            set
            {
                if (_bitmisBoy != value)
                {
                    _bitmisBoy = value;
                    OnPropertyChanged(nameof(BitmisBoy));
                }
            }
        }

        public float BitmisEn
        {
            get { return _bitmisEn; }
            set
            {
                if (_bitmisEn != value)
                {
                    _bitmisEn = value;
                    OnPropertyChanged(nameof(BitmisEn));
                }
            }
        }

        public string B1KB
        {
            get { return _b1KB; }
            set
            {
                if (_b1KB != value)
                {
                    _b1KB = value;
                    OnPropertyChanged(nameof(B1KB));
                }
            }
        }

        public string EKB1
        {
            get { return _ekb1; }
            set
            {
                if (_ekb1 != value)
                {
                    _ekb1 = value;
                    OnPropertyChanged(nameof(EKB1));
                }
            }
        }

        public string EKB2
        {
            get { return _ekb2; }
            set
            {
                if (_ekb2 != value)
                {
                    _ekb2 = value;
                    OnPropertyChanged(nameof(EKB2));
                }
            }
        }

        public string B2KB
        {
            get { return _b2KB; }
            set
            {
                if (_b2KB != value)
                {
                    _b2KB = value;
                    OnPropertyChanged(nameof(B2KB));
                }
            }
        }

        public string B1KBRecSiraNo
        {
            get { return _b1KBRecSiraNo; }
            set
            {
                if (_b1KBRecSiraNo != value)
                {
                    _b1KBRecSiraNo = value;
                    OnPropertyChanged(nameof(B1KBRecSiraNo));
                }
            }
        }

        public string EKB1RecSiraNo
        {
            get { return _ekb1RecSiraNo; }
            set
            {
                if (_ekb1RecSiraNo != value)
                {
                    _ekb1RecSiraNo = value;
                    OnPropertyChanged(nameof(EKB1RecSiraNo));
                }
            }
        }

        public string EKB2RecSiraNo
        {
            get { return _ekb2RecSiraNo; }
            set
            {
                if (_ekb2RecSiraNo != value)
                {
                    _ekb2RecSiraNo = value;
                    OnPropertyChanged(nameof(EKB2RecSiraNo));
                }
            }
        }

        public string B2KBRecSiraNo
        {
            get { return _b2KBRecSiraNo; }
            set
            {
                if (_b2KBRecSiraNo != value)
                {
                    _b2KBRecSiraNo = value;
                    OnPropertyChanged(nameof(B2KBRecSiraNo));
                }
            }
        }

        public string Aciklama
        {
            get { return _aciklama; }
            set
            {
                if (_aciklama != value)
                {
                    _aciklama = value;
                    OnPropertyChanged(nameof(Aciklama));
                }
            }
        }

        public string YuzeyDelik
        {
            get { return _yuzeyDelik; }
            set
            {
                if (_yuzeyDelik != value)
                {
                    _yuzeyDelik = value;
                    OnPropertyChanged(nameof(YuzeyDelik));
                }
            }
        }

        public string CumbaDelik
        {
            get { return _cumbaDelik; }
            set
            {
                if (_cumbaDelik != value)
                {
                    _cumbaDelik = value;
                    OnPropertyChanged(nameof(CumbaDelik));
                }
            }
        }

        public string CncKanalBoyu
        {
            get { return _cncKanalBoyu; }
            set
            {
                if (_cncKanalBoyu != value)
                {
                    _cncKanalBoyu = value;
                    OnPropertyChanged(nameof(CncKanalBoyu));
                }
            }
        }

        public string MontajSure
        {
            get { return _montajSure; }
            set
            {
                if (_montajSure != value)
                {
                    _montajSure = value;
                    OnPropertyChanged(nameof(MontajSure));
                }
            }
        }

        public string Method
        {
            get { return _method; }
            set
            {
                if (_method != value)
                {
                    _method = value;
                    OnPropertyChanged(nameof(Method));
                }
            }
        }

        public string StokKartiStokKodu { get; set; } = string.Empty;
        public string StokAdi { get; set; } = string.Empty;
        public string StokKodu { get; set; } = string.Empty;
        public string YariMamulKodu { get; set; } = string.Empty;
        public string YariMamulAdi { get; set; } = string.Empty;
        public string MamulKodu { get; set; } = string.Empty;
        public string MamulAdi { get; set; } = string.Empty;
        public string HamKodu { get; set; } = string.Empty;
        public string HamAdi { get; set; } = string.Empty;
        public decimal ToplamFiyat { get; set; } = decimal.Zero;
        public int En { get; set; } = 0;
        public int Boy { get; set; } = 0;
        public int Yukseklik { get; set; } = 0;

        public string Kod1 { get; set; } = string.Empty;
        public string Kod2 { get; set; } = string.Empty;
        public string Kod3 { get; set; } = string.Empty;
        public string Kod4 { get; set; } = string.Empty;
        public string Kod5 { get; set; } = string.Empty;

        public string Opno { get; set; } = string.Empty;
        public string ReceteAciklama { get; set; } = string.Empty;

        private string _receteKayitDurum = "Kaydedilmedi...";

        public string ReceteKayitDurum
        {
            get { return _receteKayitDurum; }
            set { _receteKayitDurum = value;
                OnPropertyChanged(nameof(ReceteKayitDurum));
                }
        }

        public string EskiHamKodu { get; set; } = string.Empty;

        public decimal ReceteTuketimMiktar { get; set; } = decimal.Zero;

        public decimal M2 { get; set; } = decimal.Zero;
        public int ID { get; set; } = 0;
        public decimal StokKDV { get; set; } = decimal.Zero;
        public string UrunKodu { get; set; } = string.Empty;
        public string? UrunAdi { get; set; } = string.Empty;

        private bool isChecked_ = false;

        public bool IsChecked
        {
            get { return isChecked_; }
            set { isChecked_ = value;
                  OnPropertyChanged(nameof(IsChecked));
                }
        }

        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        public decimal EskiMiktar { get; set; } = decimal.Zero;

        public string GuncellemeTipi { get; set; } = string.Empty;

        public ObservableCollection<Cls_Arge> ArgeCollection = new();
        private ObservableCollection<Cls_Arge> tempCollArge = new();
        Variables variables = new();
        DataLayer data = new();
        LoginLogic login = new();

        public Cls_Arge()
        {
            variables.Fabrika = login.GetFabrika();
            Variables.Fabrika_ = variables.Fabrika;
            Variables.UserName = login.GetUserName();
        }
        public ObservableCollection<Cls_Arge> GetUrunHMIhtiyac(Dictionary<string, string> kisitPairs)
        {
            try
            {
                if (kisitPairs == null) return null;

                Variables.Query_ = "Select * from vbvUrunHMihtiyac where 1=1 "; 
                Variables.Counter_= 0;

                if (kisitPairs.ContainsKey("UrunKodu"))
                {
                    Variables.Query_ += " and UrunKodu like '%' + @UrunKodu + '%'";
                    Variables.Counter_++;
                }
                if (kisitPairs.ContainsKey("Kod1"))
                {
                    Variables.Query_ += " and Kod1 like '%' + @Kod1 + '%'";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                if (kisitPairs.ContainsKey("UrunKodu"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@UrunKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = kisitPairs["UrunKodu"];
                    Variables.Counter_++;
                }
                if (kisitPairs.ContainsKey("Kod1"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@Kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = kisitPairs["Kod1"];
                    Variables.Counter_++;
                }

                    ObservableCollection<Cls_Arge> malzemeColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Arge model = new Cls_Arge();
                    model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                    model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                    model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
                    model.ReceteTuketimMiktar = reader["ReceteTuketimMiktar"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["ReceteTuketimMiktar"]);
                    model.ToplamFiyat = reader["ToplamFiyat"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["ToplamFiyat"]);
                    return model;
                });
                return malzemeColl;
                
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateReceteIcinKoliDonusturListControl(int en, int boy,int yukseklik)
        { 
            try 
	        {	        
		       
                Variables.Query_ = " select sabit.stok_kodu,sabit.STOK_ADI from TBLSTSABIT (nolock) sabit " +
                                    " where sabit.STOK_KODU like 'YM8%' " +
                                    " and coalesce(try_cast(SUBSTRING(sabit.STOK_ADI, CHARINDEX('*', sabit.STOK_ADI) + 1, 4) as int), 0)  = @en " +
                                    " and coalesce(try_cast(SUBSTRING(sabit.STOK_ADI, CHARINDEX('*', sabit.STOK_ADI) - 4, 4) as int), 0)  = @boy " +
                                    " and coalesce(try_cast(SUBSTRING(sabit.STOK_ADI, CHARINDEX('*', sabit.STOK_ADI) + 6, 4) as int), 0)  = @yukseklik "; 

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@en", SqlDbType.Int);
                parameters[0].Value = en;
                parameters[1] = new SqlParameter("@boy", SqlDbType.Int);
                parameters[1].Value = boy;
                parameters[2] = new SqlParameter("@yukseklik", SqlDbType.Int);
                parameters[2].Value = yukseklik;

                if(tempCollArge != null )
                    tempCollArge.Clear();

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters))
                {
                    if (reader == null)
                        return null;

                    if(!reader.HasRows && reader != null) 
                    {
                        Cls_Arge item = new Cls_Arge
                        {
                            HamKodu = "Boş Sonuç",
                        };
                        tempCollArge.Add(item);
                    }
                    while (reader.Read()) 
                    {
                        Cls_Arge item = new Cls_Arge
                        {
                            YariMamulKodu = reader["stok_kodu"].ToString(),
                            YariMamulAdi = reader["stok_adi"].ToString(),
                        };
                        tempCollArge.Add(item);
                    }
                }
                ArgeCollection = tempCollArge;
                return ArgeCollection;
            }
	        catch 
	        {
                return null;
		    }
        }
        public ObservableCollection<Cls_Arge> PopulateUrunAgacinaBagliOlmayanMalzemelerList(Dictionary<string, string> kisitPairs,int pageNumber)
        {
            try
            {

                ObservableCollection<Cls_Arge> BagliOlmayanCollection = new();
                Variables.Query_ = "select * from vbvUrunAgacinaBagliOlmayanMalzemeler (nolock) where 1=1 ";
                variables.Counter = 0;


                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod1 = @kod1 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod2 = @kod2 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod3 = @kod3 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod4"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod4 = @kod4 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod5"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod5 = @kod5 ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = kisitPairs["stokKodu"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod1"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod2", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod2"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod3", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod3"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod4"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod4", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod4"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod5"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod5", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod5"];
                    variables.Counter++;
                }
                Variables.Query_ += $"order by stokKodu desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                if (tempCollArge.Count > 0) 
                    tempCollArge.Clear();

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cls_Arge arge = new Cls_Arge
                            {
                                StokKodu = reader[0].ToString(),
                                StokAdi = reader[1].ToString(),
                                Kod1 = reader[2].ToString(),
                                Kod2 = reader[3].ToString(),
                                Kod3 = reader[4].ToString(),
                                Kod4 = reader[5].ToString(),
                                Kod5 = reader[6].ToString(),
                            };
                            tempCollArge.Add(arge);
                        }
                    }
                }

                ArgeCollection = tempCollArge;
                return ArgeCollection;

            }
            catch { return null; }
        }
        public ObservableCollection<Cls_Arge> GetVaryantCodes(string sablonKodu)
        {
            try
            {
                Variables.Query_ = "select stok_kodu,stok_adi from tblstsabit (nolock) where stok_kodu like @sablonKodu + '%' and len(stok_kodu)>11";
                
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@sablonKodu", SqlDbType.NVarChar, 11);
                parameters[0].Value = sablonKodu;

                ObservableCollection<Cls_Arge> varyantColl = data.Select_Command_Data_With_Parameters<Cls_Arge>(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Arge model = new();
                    model.UrunKodu = reader["stok_kodu"] is DBNull ? "" : reader["stok_kodu"].ToString();
                    model.UrunAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString();
                    return model;
                });
                return varyantColl;

            }
            catch { return null; }
        }
        public int CountUrunAgacinaBagliOlmayanMalzemelerList(Dictionary<string, string> kisitPairs, int pageNumber)
        {
            try
            {
                ObservableCollection<Cls_Arge> BagliOlmayanCollection = new();
                Variables.Query_ = "select count(*) from vbvUrunAgacinaBagliOlmayanMalzemeler (nolock) where 1=1 ";
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod1 = @kod1 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod2 = @kod2 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod3 = @kod3 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod4"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod4 = @kod4 ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod5"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod5 = @kod5 ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = kisitPairs["stokKodu"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod1"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod2", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod2"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod3", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod3"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod4"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod4", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod4"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod5"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod5", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = kisitPairs["kod5"];
                    variables.Counter++;
                }

                int totalCount = 0;
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        totalCount = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }


                return totalCount;
            }
            catch
            {
                return 0;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateIsemriReceteMailGuncellenecekList()
        {
            try
            {
                Variables.Departman_ = login.GetDepartment();
                if (Variables.Departman_.Contains("Doseme"))
                    Variables.Departman_ = "Ar-Ge Doseme";
                if (Variables.Departman_.Contains("Moduler"))
                    Variables.Departman_ = "Ar-Ge Moduler";

                Variables.Query_ = " select rg.* from vbtReceteGuncellemeDurum rg " +
                                   " left join vbtUserInfo us on substring(rg.kullaniciadi,0,12)= substring(us.KullaniciAdi, 0, 12) " +
                                   " where MailGitti=1 and (IeReceteIslemYapildi = 0 or IeReceteIslemYapildi is null) and " + 
                                   " departman=@departman" +
                                   " order by KayitTarihi"; 

                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new("@departman", SqlDbType.NVarChar, 100);
                parameter[0].Value = Variables.Departman_;


                    ObservableCollection<Cls_Arge> temp_coll_arge = new();

                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_))
                    {
                    if (reader == null)
                        return null;

                        while (reader.Read())
                        {
                            Cls_Arge arge = new Cls_Arge
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MamulKodu = reader["MamulKodu"].ToString(),
                                EskiHamKodu = reader["EskiHamKodu"].ToString(),
                                HamKodu = reader["YeniHamKodu"].ToString(),
                                EskiMiktar = Convert.ToDecimal(reader["EskiMiktar"]),
                                ReceteTuketimMiktar = Convert.ToDecimal(reader["YeniMiktar"]),
                                GuncellemeTipi = reader["GuncellemeTipi"].ToString(),
                                Opno = reader["opno"].ToString(),
                                KayitTarihi = Convert.ToDateTime(reader["KayitTarihi"]),
                            };
                            temp_coll_arge.Add(arge);
                        }
                        
                        reader.Close();
                    }

                return temp_coll_arge;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateReceteMailGonderilecekList()
        {
            try
            {
                Variables.Query_ = "select * from vbtReceteGuncellemeDurum where KullaniciAdi=@user and MailGitti=0 order by KayitTarihi"; 
                                    

                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new("@user", SqlDbType.NVarChar, 12);
                parameter[0].Value = login.GetUserName();

                   
                    ObservableCollection<Cls_Arge> temp_coll_arge = new();

                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_))
                    {
                    if (reader == null)
                        return null;

                        while (reader.Read())
                        {
                            Cls_Arge arge = new Cls_Arge
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MamulKodu = reader["MamulKodu"].ToString(),
                                EskiHamKodu = reader["EskiHamKodu"].ToString(),
                                HamKodu = reader["YeniHamKodu"].ToString(),
                                EskiMiktar = Convert.ToDecimal(reader["EskiMiktar"]),
                                ReceteTuketimMiktar = Convert.ToDecimal(reader["YeniMiktar"]),
                                GuncellemeTipi = reader["GuncellemeTipi"].ToString(),
                                Opno = reader["opno"].ToString(),
                                KayitTarihi = Convert.ToDateTime(reader["KayitTarihi"]),
                            };
                            temp_coll_arge.Add(arge);
                        }
                        
                        reader.Close();
                    }

                return temp_coll_arge;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateMamulReceteList(string mamulKodu)
        {
            try
            {
                   variables.Counter = 0;

                Variables.Query_ = "select urm.inckeyno as ID, mamul_kodu,mamul_adi,ham_kodu,ham_adi,urm.miktar,opno from tblstokurm (nolock) urm " + 
                                    "left join (select stok_kodu,stok_adi as mamul_adi from tblstsabit(nolock)) sabit on urm.mamul_kodu = sabit.stok_kodu " +
                                    "left join (select stok_kodu,stok_adi as ham_adi from tblstsabit(nolock)) sabitHam on urm.ham_kodu = sabitHam.stok_kodu " +
                                    "where mamul_kodu=@mamulKodu order by opno";

                    
                    SqlParameter[] parameters = new SqlParameter[1];

                        parameters[0] = new("@mamulKodu", SqlDbType.NVarChar, 35);
                        parameters[0].Value = mamulKodu;

                   
                    ObservableCollection<Cls_Arge> temp_coll_arge = new();

                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                    {

                        while (reader.Read())
                        {
                            Cls_Arge arge = new Cls_Arge
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MamulKodu = reader["mamul_kodu"].ToString(),
                                MamulAdi = reader["mamul_adi"].ToString(),
                                HamKodu = reader["ham_kodu"].ToString(),
                                HamAdi = reader["ham_adi"].ToString(),
                                ReceteTuketimMiktar = Convert.ToDecimal(reader["miktar"]),
                                Opno = reader["opno"].ToString(),
                            };
                            temp_coll_arge.Add(arge);
                        }
                        
                        reader.Close();
                    }

                return temp_coll_arge;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateReceteGuncelleList(Dictionary<string, string> restrictionPairs)
        {
            try
            {
                   variables.Counter = 0;

                Variables.Query_ = "select * from vbvUrunAgaciListele where 1=1 ";

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and urunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and urunAdi like '%' + @urunAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@mamulKodu"))
                    {
                        Variables.Query_ += " and mamulKodu like '%' + @mamulKodu + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@mamulAdi"))
                    {
                        Variables.Query_ += " and mamulAdi like '%' + @mamulAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@hamKodu"))
                    {
                        Variables.Query_ += " and hamKodu like '%' + @hamKodu + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@hamAdi"))
                    {
                        Variables.Query_ += " and hamAdi like '%' + @hamAdi + '%'";
                        variables.Counter++;
                    }

                  

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];

                    variables.Counter = 0;
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@mamulKodu"))
                    {
                        parameters[variables.Counter] = new("@mamulKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@mamulKodu"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@hamKodu"))
                    {
                        parameters[variables.Counter] = new("@hamKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@hamKodu"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 100);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@mamulAdi"))
                    {
                        parameters[variables.Counter] = new("@mamulAdi", SqlDbType.NVarChar, 100);
                        parameters[variables.Counter].Value = restrictionPairs["@mamulAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@hamAdi"))
                    {
                        parameters[variables.Counter] = new("@hamAdi", SqlDbType.NVarChar, 100);
                        parameters[variables.Counter].Value = restrictionPairs["@hamAdi"];
                        variables.Counter++;
                    }

                Variables.Query_ += " order by mamulKodu,opno";

                    ObservableCollection<Cls_Arge> temp_coll_arge = new();

                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {

                        while (reader.Read())
                        {
                            Cls_Arge arge = new Cls_Arge
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                UrunKodu = reader["urunKodu"].ToString(),
                                UrunAdi = reader["urunAdi"].ToString(),
                                MamulKodu = reader["mamulKodu"].ToString(),
                                MamulAdi = reader["mamulAdi"].ToString(),
                                HamKodu = reader["hamKodu"].ToString(),
                                HamAdi = reader["hamAdi"].ToString(),
                                ReceteTuketimMiktar = Convert.ToDecimal(reader["miktar"]),
                                Opno = reader["opno"].ToString(),
                            };
                            tempCollArge.Add(arge);
                        }
                        reader.Close();
                    }

                return tempCollArge;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<VaryantFiyatGirisViewModel> PopulateVaryantFiyatGirList(string stokKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(stokKodu))
                    return null;

                Variables.Query_ = @"with cte as (select ROW_NUMBER() OVER(PARTITION BY STOK_KODU ORDER BY KAYITTARIHI DESC) AS LINE
,isnull(fiyatgrubu,'') as fiyatgrubu,
isnull(stok_kodu,'') as stokKodu, isnull(A,0) A, isnull(B,0) B, isnull(C,0) C, isnull(D,0) D, isnull(E,0) E, isnull(F,0) F, isnull(G,0) G,
 isnull(H,0) H,isnull([H+],0) [H+], isnull(I,0) I, isnull([I+],0) [I+], isnull(J,0) J, isnull([J+],0) [J+], isnull(K,0) K, isnull(L,0) L, isnull(DOSEME_SURE,0) AS DOSEME_SURE
from vbtMaliyetHesap)

select isnull(sabit.stok_adi,'') as stok_adi, isnull(sabit.stok_kodu,'') as stok_kodu, cte.* from tblstsabit sabit (nolock)
                                    left join (select * from cte where line=1) cte on sabit.stok_kodu=cte.stokKodu
                                    where stok_kodu = @stokKodu ";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                ObservableCollection<VaryantFiyatGirisViewModel> fiyatColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_,Variables.Fabrika_, parameter, reader =>
                {
                    VaryantFiyatGirisViewModel model = new();
                    model.StokAdi = reader["stok_adi"] is DBNull ? string.Empty : reader["stok_adi"].ToString();
                    model.StokKodu = reader["stok_kodu"] is DBNull ? string.Empty : reader["stok_kodu"].ToString();
                    model.FiyatGrubu = reader["fiyatgrubu"] is DBNull ? "-----" : reader["fiyatgrubu"].ToString();
                  
                    model.A = reader["A"] is DBNull ? 0 : Convert.ToSingle(reader["A"]);
                    model.B = reader["B"] is DBNull ? 0 : Convert.ToSingle(reader["B"]);
                    model.C = reader["C"] is DBNull ? 0 : Convert.ToSingle(reader["C"]);
                    model.D = reader["D"] is DBNull ? 0 : Convert.ToSingle(reader["D"]);
                    model.E = reader["E"] is DBNull ? 0 : Convert.ToSingle(reader["E"]);
                    model.F = reader["F"] is DBNull ? 0 : Convert.ToSingle(reader["F"]);
                    model.G = reader["G"] is DBNull ? 0 : Convert.ToSingle(reader["G"]);
                    model.H = reader["H"] is DBNull ? 0 : Convert.ToSingle(reader["H"]);
                    model.HPlus = reader["H+"] is DBNull ? 0 : Convert.ToSingle(reader["H+"]);
                    model.I = reader["I"] is DBNull ? 0 : Convert.ToSingle(reader["I"]);
                    model.IPlus = reader["I+"] is DBNull ? 0 : Convert.ToSingle(reader["I+"]);
                    model.J = reader["J"] is DBNull ? 0 : Convert.ToSingle(reader["J"]);
                    model.JPlus = reader["J+"] is DBNull ? 0 : Convert.ToSingle(reader["J+"]);
                    model.K = reader["K"] is DBNull ? 0 : Convert.ToSingle(reader["K"]);
                    model.L = reader["L"] is DBNull ? 0 : Convert.ToSingle(reader["L"]);
                    model.DosemeSure = reader["DOSEME_SURE"] is DBNull ? 0 : Convert.ToSingle(reader["DOSEME_SURE"]);

                    return model;
                });

                return fiyatColl;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> PopulateStokKartiEkBilgilerList(string stokKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(stokKodu))
                    return null;

                if (stokKodu.Substring(0, 1) == "M" ||
                    stokKodu.Substring(0, 1) == "S")
                {
                    Variables.Query_ = "sbppReceteSadelestirmeMamulBazli";
                    SqlParameter[] parameterM = new SqlParameter[1];
                    parameterM[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameterM[0].Value = stokKodu;
                    Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameterM);
                    if (!Variables.Result_)
                        return null;
                    Variables.Query_ = @"with cte as (select distinct mainCode, childCode from sbptReceteSadelestirme) SELECT b.stok_kodu as StokKartiStokKodu,b.stok_adi,c.* FROM cte a
                                             left join TBLSTSABIT b on a.childCode=b.stok_kodu
                                             left join sbptstokekbilgiler c on a.ChildCode=c.StokKodu
                                             left join TBLSTSABIT d on a.ChildCode=d.STOK_KODU
                                             where a.mainCode = @mamulKodu and ChildCode like 'y%'
                                             order by childcode asc";
                    SqlParameter[] parameterMa = new SqlParameter[1];
                    parameterMa[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parameterMa[0].Value = stokKodu;

                    ObservableCollection<Cls_Arge> ekBilgiCollMamul = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameterMa, reader =>
                    {
                        Cls_Arge model = new();
                        model.StokKartiStokKodu = reader["StokKartiStokKodu"] is DBNull ? string.Empty : reader["StokKartiStokKodu"].ToString();
                           model.StokAdi = reader["stok_adi"] is DBNull ?  string.Empty : reader["stok_adi"].ToString();
                        model.StokKodu = reader["StokKodu"] is DBNull ? reader["StokKartiStokKodu"].ToString() : reader["StokKodu"].ToString();
                        model.KabaBoy = reader["KabaBoy"] is DBNull ? 0 : Convert.ToSingle(reader["KabaBoy"]);
                           model.KabaEn = reader["KabaEn"] is DBNull ? 0 : Convert.ToSingle(reader["KabaEn"]);
                           model.NetBoy = reader["NetBoy"] is DBNull ? 0 : Convert.ToSingle(reader["NetBoy"]);
                        model.NetEn = reader["NetEn"] is DBNull ? 0 : Convert.ToSingle(reader["NetEn"]);
                        model.BitmisBoy = reader["BitmisBoy"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisBoy"]);
                           model.BitmisEn = reader["BitmisEn"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisEn"]);
                           model.B1KB = reader["B1KB"] is DBNull ? string.Empty : reader["B1KB"].ToString();
                           model.EKB1 = reader["EKB1"] is DBNull ? string.Empty : reader["EKB1"].ToString();
                           model.EKB2 = reader["EKB2"] is DBNull ? string.Empty : reader["EKB2"].ToString();
                        model.B2KB = reader["B2KB"] is DBNull ? string.Empty : reader["B2KB"].ToString();
                           model.B1KBRecSiraNo = reader["B1KBRecSiraNo"] is DBNull ? string.Empty : reader["B1KBRecSiraNo"].ToString();
                           model.EKB1RecSiraNo = reader["EKB1RecSiraNo"] is DBNull ? string.Empty : reader["EKB1RecSiraNo"].ToString();
                           model.EKB2RecSiraNo = reader["EKB2RecSiraNo"] is DBNull ? string.Empty : reader["EKB2RecSiraNo"].ToString();
                        model.B2KBRecSiraNo = reader["B2KBRecSiraNo"] is DBNull ? string.Empty : reader["B2KBRecSiraNo"].ToString();
                        model.Aciklama = reader["Aciklama"] is DBNull ? string.Empty : reader["Aciklama"].ToString();
                           model.YuzeyDelik = reader["YuzeyDelik"] is DBNull ? string.Empty : reader["YuzeyDelik"].ToString();
                        model.CumbaDelik = reader["CumbaDelik"] is DBNull ? string.Empty : reader["CumbaDelik"].ToString();
                        model.CncKanalBoyu = reader["CncKanalBoyu"] is DBNull ? string.Empty : reader["CncKanalBoyu"].ToString();
                        model.MontajSure = reader["MontajSure"].ToString() is DBNull ? string.Empty : reader["MontajSure"].ToString();
                        model.Method = reader["StokKodu"] is DBNull ? "INSERT" : "UPDATE";

                        return model;
                    });
                    return ekBilgiCollMamul;
                }

                Variables.Query_ = @"select isnull(sabit.stok_adi,'') as stok_adi ,ek.* from sbptstokekbilgiler (nolock) ek
                                    left join tblstsabit sabit on ek.stokKodu=sabit.stok_kodu where ek.stokKodu=@stokKodu";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                ObservableCollection<Cls_Arge> ekBilgiColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_,Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Arge model = new();
                    model.StokKartiStokKodu = reader["StokKodu"] is DBNull ? string.Empty : reader["StokKodu"].ToString();
                    model.StokAdi = reader["stok_adi"] is DBNull ? string.Empty : reader["stok_adi"].ToString();
                    model.StokKodu = reader["StokKodu"] is DBNull ? string.Empty : reader["StokKodu"].ToString();
                    model.KabaBoy = reader["KabaBoy"] is DBNull ? 0 : Convert.ToSingle(reader["KabaBoy"]);
                    model.KabaEn = reader["KabaEn"] is DBNull ? 0 : Convert.ToSingle(reader["KabaEn"]);
                    model.NetBoy = reader["NetBoy"] is DBNull ? 0 : Convert.ToSingle(reader["NetBoy"]);
                    model.NetEn = reader["NetEn"] is DBNull ? 0 : Convert.ToSingle(reader["NetEn"]);
                    model.BitmisBoy = reader["BitmisBoy"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisBoy"]);
                    model.BitmisEn = reader["BitmisEn"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisEn"]);
                    model.B1KB = reader["B1KB"] is DBNull ? string.Empty : reader["B1KB"].ToString();
                    model.EKB1 = reader["EKB1"] is DBNull ? string.Empty : reader["EKB1"].ToString();
                    model.EKB2 = reader["EKB2"] is DBNull ? string.Empty : reader["EKB2"].ToString();
                    model.B2KB = reader["B2KB"] is DBNull ? string.Empty : reader["B2KB"].ToString();
                    model.B1KBRecSiraNo = reader["B1KBRecSiraNo"] is DBNull ? string.Empty : reader["B1KBRecSiraNo"].ToString();
                    model.EKB1RecSiraNo = reader["EKB1RecSiraNo"] is DBNull ? string.Empty : reader["EKB1RecSiraNo"].ToString();
                    model.EKB2RecSiraNo = reader["EKB2RecSiraNo"] is DBNull ? string.Empty : reader["EKB2RecSiraNo"].ToString();
                    model.B2KBRecSiraNo = reader["B2KBRecSiraNo"] is DBNull ? string.Empty : reader["B2KBRecSiraNo"].ToString();
                    model.Aciklama = reader["Aciklama"] is DBNull ? string.Empty : reader["Aciklama"].ToString();
                    model.YuzeyDelik = reader["YuzeyDelik"] is DBNull ? string.Empty : reader["YuzeyDelik"].ToString();
                    model.CumbaDelik = reader["CumbaDelik"] is DBNull ? string.Empty : reader["CumbaDelik"].ToString();
                    model.CncKanalBoyu = reader["CncKanalBoyu"] is DBNull ? string.Empty : reader["CncKanalBoyu"].ToString();
                    model.MontajSure = reader["MontajSure"].ToString() is DBNull ? string.Empty : reader["MontajSure"].ToString();
                    model.Method = "UPDATE";

                    return model;
                });
                if (ekBilgiColl.Count == 0)
                {
                    Cls_Arge notExists = new();
                    notExists.StokKodu = stokKodu;
                    notExists.StokKartiStokKodu = stokKodu;
                    notExists.StokAdi = GetStokAdi(stokKodu, "Vita");
                    notExists.Method = "INSERT";
                    ekBilgiColl.Add(notExists);
                }

                return ekBilgiColl;

            }
            catch
            {
                return null;
            }
        }
        public string GetKoliYariMamulKodu()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@prefix", SqlDbType.NVarChar, 3);
                parameters[0].Value = "YM8";
                parameters[1] = new SqlParameter("@tableName", SqlDbType.NVarChar, 128);
                parameters[1].Value = "TBLSTSABIT";
                parameters[2] = new SqlParameter("@columnName", SqlDbType.NVarChar, 128);
                parameters[2].Value = "STOK_KODU";
                YariMamulKodu = data.Get_One_String_Result_Stored_Proc_With_Parameters("vbpGetFisno", Variables.Yil_, parameters);

                if (YariMamulKodu.Length >= 4)
                {
                    YariMamulKodu = YariMamulKodu.Remove(3,1);
                }

                if (YariMamulKodu == string.Empty)
                    YariMamulKodu = "YM8000000001";

                

                return YariMamulKodu;
            }
            catch 
            {
                return string.Empty;
            }
        }
        public string GetHamAdiFromHamKodu(string hamKodu)
        {
            try
            {
                Variables.Query_ = "SELECT STOK_ADI FROM TBLSTSABIT (NOLOCK) WHERE " +
                    "STOK_KODU =@hamKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@hamKodu",SqlDbType.NVarChar, 35);
                parameter[0].Value = hamKodu;

                HamAdi = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter);

                if (HamAdi == "STRING HATA")
                    return "HAMADI HATA";

                return HamAdi;

            }
            catch 
            {
                return "HAMADI HATA";
            }
        }
        public string GetStokAdi(string stokKodu,string fabrika)
        {
            try
            {
                Variables.Query_ = "select stok_adi from tblstsabit (nolock) where stok_kodu=@stokKodu";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                StokAdi = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, fabrika);

                return StokAdi;
                
            }
            catch 
            {
                return string.Empty;
            }
        }
        public string GetStokAdi(string stokKodu)
        {
            try
            {
                Variables.Query_ = "select stok_adi from tblstsabit (nolock) where stok_kodu=@stokKodu";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                StokAdi = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                return StokAdi;

            }
            catch
            {
                return string.Empty;
            }
        }
        public ObservableCollection<Cls_Arge> GetDistinctCodes()
        { 
            try 
	        {
                Variables.Query_ = "Select Distinct KOD_1 from tblstsabit (nolock)";

                if(tempCollArge.Count > 0)
                    tempCollArge.Clear();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null)
                        return null;

                    while (reader.Read())
                    {
                        Cls_Arge arge = new Cls_Arge
                        {
                            Kod1 = reader[0].ToString(),
                        };
                        tempCollArge.Add(arge);
                    }
                }
                ArgeCollection = tempCollArge;
                return ArgeCollection;
	        }
	        catch 
	        {
                return null;
	        }
        }
        public List<string> GetDistinctKod1()
        {
            try
            {
                Variables.Query_ = "Select Distinct KOD_1 from tblstsabit (nolock) order by kod_1 asc";
                List<string> result = new List<string>();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<string> GetDistinctKod2()
        {
            try
            {
                Variables.Query_ = "Select Distinct KOD_2 from tblstsabit (nolock) order by kod_2 asc";
                List<string> result = new List<string>();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<string> GetDistinctKod3()
        {
            try
            {
                Variables.Query_ = "Select Distinct KOD_3 from tblstsabit (nolock) order by kod_3 asc";
                List<string> result = new List<string>();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<string> GetDistinctKod4()
        {
            try
            {
                Variables.Query_ = "Select Distinct KOD_4 from tblstsabit (nolock) order by kod_4 asc";
                List<string> result = new List<string>();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<string> GetDistinctKod5()
        {
            try
            {
                Variables.Query_ = "Select Distinct KOD_5 from tblstsabit (nolock) order by kod_5 asc";
                List<string> result = new List<string>();

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string GetLastOpNo(string mamulKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(mamulKodu))
                    return string.Empty;

                Variables.Query_ = "Select top 1 cast(opno as int) from tblstokurm (nolock) where mamul_kodu=@mamulKodu order by opno desc";
                string result = string.Empty;
                int opno = 0;

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = mamulKodu;

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        opno=Convert.ToInt32(reader[0]);
                    }
                }

                if(opno == 0)
                return string.Empty;

                opno++;

                int uzunluk = opno.ToString().Length;
                int kalanUzunluk = 4 - uzunluk;
                result = string.Concat(Enumerable.Repeat("0", kalanUzunluk)) + opno.ToString();


                return result;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }
        public decimal GetKdvOrani(string stokKodu)
        {
            try
            {
                Variables.Query_ = $"select top 1 kdv_orani from tblstsabit (nolock) where stok_kodu=@stokKodu";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = stokKodu;

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        StokKDV = reader.GetDecimal(0);
                    }
                }
                return StokKDV;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return 0; }

        }
        public ObservableCollection<Cls_Arge> GetKod1()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_1 from tblstsabit (nolock)";
                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_1"] is DBNull ? "" : reader["kod_1"].ToString();
                    return model;
                });
                return kodCollection;

            }
            catch
            {
                return null;
            }
        }
        public string GetKod1(string stokKodu)
        {
            try
            {
                Variables.Query_ = "Select isnull(kod_1,'') as kod_1 from tblstsabit (nolock) where stok_kodu=@stokKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_1"] is DBNull ? "" : reader["kod_1"].ToString();
                    return model;
                });
                return kodCollection.Select(k=>k.Kod1).FirstOrDefault();

            }
            catch
            {
                return null;
            }
        }
        public string GetKod5(string stokKodu)
        {
            try
            {
                Variables.Query_ = "Select isnull(kod_5,'') as kod_5 from tblstsabit (nolock) where stok_kodu=@stokKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod5 = reader["kod_5"] is DBNull ? "" : reader["kod_5"].ToString();
                    return model;
                });
                return kodCollection.Select(k=>k.Kod5).FirstOrDefault();

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> GetKod2()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_2 from tblstsabit (nolock)";
                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_2"] is DBNull ? "" : reader["kod_2"].ToString();
                    return model;
                });
                return kodCollection;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> GetKod3()
        {
            try
            {

                Variables.Query_ = "Select distinct kod_3 from tblstsabit (nolock)";
                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_3"] is DBNull ? "" : reader["kod_3"].ToString();
                    return model;
                });
                return kodCollection;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> GetKod4()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_4 from tblstsabit (nolock)";
                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_4"] is DBNull ? "" : reader["kod_4"].ToString();
                    return model;
                });
                return kodCollection;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Arge> GetKod5()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_5 from tblstsabit (nolock)";
                ObservableCollection<Cls_Arge> kodCollection = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Arge model = new();
                    model.Kod1 = reader["kod_1"] is DBNull ? "" : reader["kod_1"].ToString();
                    return model;
                });
                return kodCollection;

            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> IfStokKoduExistsAsync(string stokKodu) 
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstsabit (nolock) where stok_kodu = @stokKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;
                Variables.ResultInt_ = await data.Get_One_Int_Result_Command_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (Variables.ResultInt_ != 1)
                    return false;
                
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool IfSiparisUrunAgaciExists(string fisno,bool onayDurum)
        {
            try
            {
                if (onayDurum)
                    Variables.Query_ = "select isnull(stok_kodu,'') as StokKodu from tblsipatra (nolock) where fisno=@fisno";
                else
                    Variables.Query_ = "select isnull(stok_kodu,'') as StokKodu from werpsipatra (nolock) where fisno=@fisno";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;
                ObservableCollection<Cls_Siparis> siparisColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Siparis model = new();
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    return model;
                });
                foreach(Cls_Siparis siparis in siparisColl)
                {
                    Variables.Query_ = "Select count(*) from tblstokurm (nolock) where mamul_kodu = @mamulKodu ";
                    SqlParameter[] parameterM = new SqlParameter[1];
                    parameterM[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parameterM[0].Value = siparis.StokKodu;
                    Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameterM, Variables.Fabrika_);
                    if (Variables.ResultInt_ < 1)
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool IfUrunAgaciExists(string mamulKodu)
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstokurm (nolock) where mamul_kodu = @mamulKodu ";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = mamulKodu;
                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (Variables.ResultInt_ < 1)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> IfUrunAgaciExistsAsync(string mamulKodu)
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstokurm (nolock) where mamul_kodu = @mamulKodu ";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = mamulKodu;
                Variables.ResultInt_ = await data.Get_One_Int_Result_Command_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (Variables.ResultInt_ < 1)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> IfUrunAgaciExistsAsync(string mamulKodu,string hamKodu, string opno)
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstokurm (nolock) where mamul_kodu = @mamulKodu and ham_kodu=@hamKodu and opno=@opno";
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = mamulKodu;
                parameter[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                parameter[1].Value = hamKodu;
                parameter[2] = new SqlParameter("@opno", SqlDbType.NVarChar, 4);
                parameter[2].Value = opno;
                Variables.ResultInt_ = await data.Get_One_Int_Result_Command_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (Variables.ResultInt_ != 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool CheckIfDepoKoduExists(int depoKodu) 
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstokdp (nolock) where depo_kodu = @depoKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@depoKodu", SqlDbType.Int);
                parameter[0].Value = depoKodu;
                variables.ResultInt = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (variables.ResultInt < 1)
                    return false;
                
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool IfStokKoduExists(string stokKodu) 
        {
            try
            {
                Variables.Query_ = "Select count(*) from tblstsabit (nolock) where stok_kodu = @stokKodu";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;
                variables.ResultInt = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if (variables.ResultInt != 1)
                    return false;
                
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool InsertStokKartiveReceteKoli(ObservableCollection<Cls_Arge> receteVeStokKartiCollection)
        {
            try
            {
                YariMamulKodu = receteVeStokKartiCollection.Select(y=>y.YariMamulKodu).FirstOrDefault();
                YariMamulAdi = receteVeStokKartiCollection.Select(y=>y.YariMamulAdi).FirstOrDefault();
                HamKodu = receteVeStokKartiCollection.Select(h=>h.HamKodu).FirstOrDefault();
                En = receteVeStokKartiCollection.Select(e => e.En).FirstOrDefault();
                Boy = receteVeStokKartiCollection.Select(b => b.Boy).FirstOrDefault();
                Yukseklik = receteVeStokKartiCollection.Select(g => g.Yukseklik).FirstOrDefault();
                M2 = receteVeStokKartiCollection.Select(m => m.M2).FirstOrDefault();

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter("@stokKodu",SqlDbType.NVarChar,35);
                parameters[0].Value = YariMamulKodu;
                parameters[1] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 100);
                parameters[1].Value = YariMamulAdi;
                parameters[2] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                parameters[2].Value = HamKodu;
                parameters[3] = new SqlParameter("@en", SqlDbType.Int);
                parameters[3].Value = En;
                parameters[4] = new SqlParameter("@boy", SqlDbType.Int);
                parameters[4].Value = Boy;
                parameters[5] = new SqlParameter("@genislik", SqlDbType.Int);
                parameters[5].Value = Yukseklik;
                parameters[6] = new SqlParameter("@miktar", SqlDbType.Float);
                parameters[6].Value = Convert.ToDouble(M2);
                parameters[7] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                parameters[7].Value = login.GetUserName();
                
                variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertStokKartiveUrunRecetesiKoli",Variables.Yil_,parameters);

                if (!variables.Result)
                    return false;

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public async static Task<int> OpnoControl(string opno)
        {
            try
            {
                if(opno.Length != 4)
                    return 2;

                bool isNumeric = Regex.IsMatch(opno, @"^\d+$");
                if (isNumeric == false)
                    return 3;

                return 1;
            }
            catch 
            {
                return -1;
            }
        }
        public async Task<ObservableCollection<Cls_Arge>> InsertUrunAgaciAsync(ObservableCollection<Cls_Arge> recete)
        {
            try
            {
                foreach(Cls_Arge arge in recete)
                { 
                    Variables.Query_ = "vbpInsertUrunAgaci";
                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parameters[0].Value = arge.MamulKodu;
                    parameters[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                    parameters[1].Value = arge.HamKodu;
                    parameters[2] = new SqlParameter("@miktar", SqlDbType.Int);
                    parameters[2].Value = arge.ReceteTuketimMiktar;
                    parameters[3] = new SqlParameter("@opno", SqlDbType.NVarChar,4);
                    parameters[3].Value = arge.Opno;
                    parameters[4] = new SqlParameter("@aciklama", SqlDbType.NVarChar,100);
                    parameters[4].Value = arge.ReceteAciklama;
                    parameters[5] = new SqlParameter("@userName", SqlDbType.NVarChar,12);
                    parameters[5].Value = login.GetUserName();


                    Variables.ResultInt_ = await data.ExecuteStoredProcedureWithParametersAsyncReturnsRecordsAffected(Variables.Query_,Variables.Yil_,parameters,Variables.Fabrika_);
                    if (Variables.ResultInt_ > 0)
                    {   arge.ReceteKayitDurum = "Kayıt Başarılı..."; continue; }
                    if(Variables.ResultInt_ == -1)
                        arge.ReceteKayitDurum = "Kayıt Yapılırken Sistemsel Bir Hata İle Karşılaşıldı.";

                }

                return recete;
            }
            catch 
            {
                return null;
            }
        }
        public async Task<ObservableCollection<VaryantFiyatGirisViewModel>> InsertUpdateVaryantFiyatAsync(ObservableCollection<VaryantFiyatGirisViewModel> fiyatColl)
        {
            try
            {
                if (fiyatColl == null)
                    return null;
                if (fiyatColl.Count == 0)
                    return null;

                foreach(VaryantFiyatGirisViewModel stokKarti in fiyatColl)
                { 
                    Variables.Query_ = "vbpInsertUpdateVaryantFiyat";

                    if (string.IsNullOrEmpty(stokKarti.StokKodu))
                        continue;

                    SqlParameter[] parameters = new SqlParameter[19];

                    parameters[0] = new SqlParameter("@FIYATGRUBU", SqlDbType.NVarChar, 64);
                    parameters[0].Value = stokKarti.FiyatGrubu;

                    parameters[1] = new SqlParameter("@STOK_KODU", SqlDbType.NVarChar, 64);
                    parameters[1].Value = stokKarti.StokKodu;

                    parameters[2] = new SqlParameter("@A", SqlDbType.Float);
                    parameters[2].Value = stokKarti.A;

                    parameters[3] = new SqlParameter("@B", SqlDbType.Float);
                    parameters[3].Value = stokKarti.B;

                    parameters[4] = new SqlParameter("@C", SqlDbType.Float);
                    parameters[4].Value = stokKarti.C;

                    parameters[5] = new SqlParameter("@D", SqlDbType.Float);
                    parameters[5].Value = stokKarti.D;

                    parameters[6] = new SqlParameter("@E", SqlDbType.Float);
                    parameters[6].Value = stokKarti.E;

                    parameters[7] = new SqlParameter("@F", SqlDbType.Float);
                    parameters[7].Value = stokKarti.F;

                    parameters[8] = new SqlParameter("@G", SqlDbType.Float);
                    parameters[8].Value = stokKarti.G;

                    parameters[9] = new SqlParameter("@H", SqlDbType.Float);
                    parameters[9].Value =   stokKarti.H;

                    parameters[10] = new SqlParameter("@HPLUS", SqlDbType.Float);
                    parameters[10].Value = stokKarti.HPlus;

                    parameters[11] = new SqlParameter("@I", SqlDbType.Float);
                    parameters[11].Value = stokKarti.I;

                    parameters[12] = new SqlParameter("@IPLUS", SqlDbType.Float);
                    parameters[12].Value = stokKarti.IPlus;

                    parameters[13] = new SqlParameter("@J", SqlDbType.Float);
                    parameters[13].Value = stokKarti.J;

                    parameters[14] = new SqlParameter("@JPLUS", SqlDbType.Float);
                    parameters[14].Value = stokKarti.JPlus;

                    parameters[15] = new SqlParameter("@K", SqlDbType.Float);
                    parameters[15].Value = stokKarti.K;

                    parameters[16] = new SqlParameter("@L", SqlDbType.Float);
                    parameters[16].Value = stokKarti.L;

                    parameters[17] = new SqlParameter("@DOSEME_SURE", SqlDbType.Float);
                    parameters[17].Value = stokKarti.DosemeSure;

                    parameters[18] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                    parameters[18].Value = Variables.UserName;



                    Variables.ResultInt_ = await data.ExecuteStoredProcedureWithParametersAsyncReturnsRecordsAffected(Variables.Query_,Variables.Yil_,parameters,Variables.Fabrika_);
                    if (Variables.ResultInt_ > 0)
                    { 
                        stokKarti.KayitDurum = "Kayıt Başarılı...";
                        continue;
                    }
                    if(Variables.ResultInt_ == -1)
                        stokKarti.KayitDurum = "Kayıt Yapılırken Sistemsel Bir Hata İle Karşılaşıldı.";

                }

                return fiyatColl;
            }
            catch 
            {
                return null;
            }
        }
        public async Task<ObservableCollection<Cls_Arge>> InsertUpdateStokEkBilgilerAsync(ObservableCollection<Cls_Arge> stokEkbilgiColl)
        {
            try
            {
                foreach(Cls_Arge stokKarti in stokEkbilgiColl)
                { 
                    Variables.Query_ = "vbpInsertUpdateStokEkBilgiler";

                    if(!string.IsNullOrEmpty(stokKarti.B1KBRecSiraNo))
                    {
                        if(stokKarti.B1KBRecSiraNo.Length<4)
                        {
                            stokKarti.B1KBRecSiraNo = stokKarti.B1KBRecSiraNo.PadLeft(4, '0');
                        }
                    }
                    if(!string.IsNullOrEmpty(stokKarti.EKB1RecSiraNo))
                    {
                        if(stokKarti.EKB1RecSiraNo.Length<4)
                        {
                            stokKarti.EKB1RecSiraNo = stokKarti.EKB1RecSiraNo.PadLeft(4, '0');
                        }
                    }
                    if(!string.IsNullOrEmpty(stokKarti.EKB2RecSiraNo))
                    {
                        if(stokKarti.EKB2RecSiraNo.Length<4)
                        {
                            stokKarti.EKB2RecSiraNo = stokKarti.EKB2RecSiraNo.PadLeft(4, '0');
                        }
                    }
                    if(!string.IsNullOrEmpty(stokKarti.B2KBRecSiraNo))
                    {
                        if(stokKarti.B2KBRecSiraNo.Length<4)
                        {
                            stokKarti.B2KBRecSiraNo = stokKarti.B2KBRecSiraNo.PadLeft(4, '0');
                        }
                    }
                    SqlParameter[] parameters = new SqlParameter[22];

                    parameters[0] = new SqlParameter("@user", SqlDbType.NVarChar, 20);
                    parameters[0].Value = login.GetUserName();

                    parameters[1] = new SqlParameter("@method", SqlDbType.NVarChar, 20);
                    parameters[1].Value = stokKarti.Method;

                    parameters[2] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[2].Value = stokKarti.StokKodu;

                    parameters[3] = new SqlParameter("@kabaBoy", SqlDbType.Float);
                    parameters[3].Value = stokKarti.KabaBoy;

                    parameters[4] = new SqlParameter("@kabaEn", SqlDbType.Float);
                    parameters[4].Value = stokKarti.KabaEn;

                    parameters[5] = new SqlParameter("@netBoy", SqlDbType.Float);
                    parameters[5].Value = stokKarti.NetBoy;

                    parameters[6] = new SqlParameter("@netEn", SqlDbType.Float);
                    parameters[6].Value = stokKarti.NetEn;

                    parameters[7] = new SqlParameter("@bitmisBoy", SqlDbType.Float);
                    parameters[7].Value = stokKarti.BitmisBoy;

                    parameters[8] = new SqlParameter("@bitmisEn", SqlDbType.Float);
                    parameters[8].Value = stokKarti.BitmisEn;

                    parameters[9] = new SqlParameter("@b1KB", SqlDbType.NVarChar,1);
                    parameters[9].Value = stokKarti.B1KB;

                    parameters[10] = new SqlParameter("@ekb1", SqlDbType.NVarChar, 1);
                    parameters[10].Value = stokKarti.EKB1;

                    parameters[11] = new SqlParameter("@ekb2", SqlDbType.NVarChar, 1);
                    parameters[11].Value = stokKarti.EKB2;

                    parameters[12] = new SqlParameter("@b2KB", SqlDbType.NVarChar, 1);
                    parameters[12].Value = stokKarti.B2KB;

                    parameters[13] = new SqlParameter("@b1KBRecSiraNo", SqlDbType.NVarChar, 4);
                    parameters[13].Value = stokKarti.B1KBRecSiraNo;

                    parameters[14] = new SqlParameter("@ekb1RecSiraNo", SqlDbType.NVarChar, 4);
                    parameters[14].Value = stokKarti.EKB1RecSiraNo;

                    parameters[15] = new SqlParameter("@ekb2RecSiraNo", SqlDbType.NVarChar, 4);
                    parameters[15].Value = stokKarti.EKB2RecSiraNo;
                    

                    parameters[16] = new SqlParameter("@b2KBRecSiraNo", SqlDbType.NVarChar, 4);
                    parameters[16].Value = stokKarti.B2KBRecSiraNo;


                    parameters[17] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 500);
                    parameters[17].Value = stokKarti.Aciklama;

                    parameters[18] = new SqlParameter("@yuzeyDelik", SqlDbType.NVarChar, 50);
                    parameters[18].Value = stokKarti.YuzeyDelik;

                    parameters[19] = new SqlParameter("@cumbaDelik", SqlDbType.NVarChar, 50);
                    parameters[19].Value = stokKarti.CumbaDelik;

                    parameters[20] = new SqlParameter("@cncKanalBoyu", SqlDbType.NVarChar, 50);
                    parameters[20].Value = stokKarti.CncKanalBoyu;

                    parameters[21] = new SqlParameter("@montajSure", SqlDbType.NVarChar, 50);
                    parameters[21].Value = stokKarti.MontajSure;


                    Variables.ResultInt_ = await data.ExecuteStoredProcedureWithParametersAsyncReturnsRecordsAffected(Variables.Query_,Variables.Yil_,parameters,Variables.Fabrika_);
                    if (Variables.ResultInt_ > 0)
                    { 
                        stokKarti.ReceteKayitDurum = "Kayıt Başarılı...";
                        stokKarti.Method = "UPDATE";
                        continue;
                    }
                    if(Variables.ResultInt_ == -1)
                        stokKarti.ReceteKayitDurum = "Kayıt Yapılırken Sistemsel Bir Hata İle Karşılaşıldı.";

                }

                return stokEkbilgiColl;
            }
            catch 
            {
                return null;
            }
        }
        public bool UpdateStokAdi(string stokKodu,string stokAdi, string fabrika)
        {
            try
            {
                Variables.Query_ = "update tblstsabit set stok_adi = @stokAdi where stok_kodu = @stokKodu";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 100);
                parameters[0].Value = stokAdi;
                parameters[1] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[1].Value = stokKodu;
                variables.Result = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_,parameters,fabrika);
                return variables.Result;
            }
            catch 
            {
                return false;
            }
        }
        public async  Task<bool> CheckIfOpnoExists(string opno,string mamulKodu)
        {
            if (string.IsNullOrEmpty(opno))
                return false;

            Variables.Query_ = "Select count(*) from tblstokurm (nolock) where opno = @opno and mamul_kodu=@mamulKodu";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@opno", SqlDbType.NVarChar, 4);
            parameter[0].Value = opno;
            parameter[1] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
            parameter[1].Value = mamulKodu;

            Variables.ResultInt_ = await data.Get_One_Int_Result_Command_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if(Variables.ResultInt_ == 0)
                    return true; return false;

        }
        public async Task<int> UpdateUrunAgaciAsync(ObservableCollection<Cls_Arge> guncellemeCollection, string guncellemeTipi)
        {
            try
            {
                Variables.Query_ = "vbpUpdateUrunAgaci";

                

                    foreach (Cls_Arge arge in guncellemeCollection)
                    {
                        SqlParameter[] parameters = new SqlParameter[9];
                        parameters[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                        parameters[1] = new SqlParameter("@eskiHamKodu", SqlDbType.NVarChar, 35);
                        parameters[2] = new SqlParameter("@yeniHamKodu", SqlDbType.NVarChar, 35);
                        parameters[3] = new SqlParameter("@eskiMiktar", SqlDbType.Decimal);
                        parameters[4] = new SqlParameter("@miktar", SqlDbType.Decimal);
                        parameters[5] = new SqlParameter("@guncellemeTipi", SqlDbType.NVarChar,100);
                        parameters[6] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                        parameters[7] = new SqlParameter("@id", SqlDbType.Int);
                        parameters[8] = new SqlParameter("@opno", SqlDbType.Int);
                        parameters[0].Value = arge.MamulKodu;
                        parameters[1].Value = arge.EskiHamKodu;
                        parameters[2].Value = arge.HamKodu;
                        parameters[3].Value = arge.EskiMiktar;
                        parameters[4].Value = arge.ReceteTuketimMiktar;
                        parameters[5].Value = guncellemeTipi;
                        parameters[6].Value = login.GetUserName();
                        parameters[7].Value = arge.ID;
                        parameters[8].Value = arge.Opno;

                    variables.Result = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika);

                        if (variables.Result == false)
                            return -2;
                    
                }

                return 1;

            }
            catch
            {
                return -1;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateReceteDetayCollection(Cls_Arge detayRecete,bool tamamlananlariListeleme)
        {
            try
            {
                if (detayRecete == null)
                    return null;

                ObservableCollection<Cls_Isemri> argeDetayCollection = new();

                SqlParameter[] parametersForEkle = new SqlParameter[1];
                SqlParameter[] parametersForOtherTypes = new SqlParameter[3];
                Variables.Query_ = "select isnull(kt_takipnum,'') as kt_takipnum,isnull(rec.isemrino,'') as isemrino,isnull(mamul_kodu,'') as mamul_kodu,isnull(ham_kodu,'') as ham_kodu,isnull(cast(rec.miktar as float),0) as miktar,isnull(usk_status,'') as usk_status,isnull(opno,'') as opno,isnull(sabitU.stok_adi,'') as urunAdi,isnull(sabitM.stok_adi,'') as mamulAdi,isnull(sabitH.stok_adi,'') as hamAdi,isnull(tepesipno,'') as tepesipno,isnull(cast(tepesipkont as int),0) as tepesipkont,isnull(tepemam,'') as tepemam,isnull(refisemrino,'') as refisemrino from tblisemrirec rec (nolock) " +
                                    " left join tblisemri (nolock) ie on rec.isemrino=ie.isemrino and rec.mamul_kodu=ie.stok_kodu" +
                                    " left join tblstsabit sabitU (nolock) on ie.tepemam = sabitU.stok_kodu" +
                                    " left join tblstsabit sabitM (nolock) on ie.stok_kodu = sabitM.stok_kodu" +
                                    " left join tblstsabit sabitH (nolock) on rec.ham_kodu = sabitH.stok_kodu" +
                                    " left join tblisemriek ek (nolock) on ie.isemrino = ek.isemri" +
                                    " where mamul_kodu=@mamulKodu ";

                if (tamamlananlariListeleme == true)
                    Variables.Query_ += " and usk_status<>'T' ";

                if (detayRecete.GuncellemeTipi == "hamkodu" ||
                    detayRecete.GuncellemeTipi == "hammiktar" ||
                    detayRecete.GuncellemeTipi == "miktar" ||
                    detayRecete.GuncellemeTipi == "sil")
                {
                    Variables.Query_ += " and ham_kodu=@hamKodu and rec.miktar=@miktar ";
                    
                    parametersForOtherTypes[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parametersForOtherTypes[0].Value = detayRecete.MamulKodu;
                    parametersForOtherTypes[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                    parametersForOtherTypes[1].Value = detayRecete.EskiHamKodu;
                    parametersForOtherTypes[2] = new SqlParameter("@miktar", SqlDbType.Decimal);
                    parametersForOtherTypes[2].Value = detayRecete.EskiMiktar;

                        argeDetayCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parametersForOtherTypes, reader =>
                    {
                        Cls_Isemri model = new();

                        model.SIPARIS_NO = reader.IsDBNull(reader.GetOrdinal("tepesipno")) ? string.Empty : reader["tepesipno"].ToString();
                        model.SIPARIS_SIRA = reader.IsDBNull(reader.GetOrdinal("tepesipkont")) ? 0 : Convert.ToInt32(reader["tepesipkont"]);
                        model.TAKIP_NO = reader.IsDBNull(reader.GetOrdinal("kt_takipnum")) ? string.Empty : reader["kt_takipnum"].ToString();
                        model.REFISEMRINO = reader.IsDBNull(reader.GetOrdinal("refisemrino")) ? string.Empty : reader["refisemrino"].ToString();
                        model.URUN_KODU = reader.IsDBNull(reader.GetOrdinal("tepemam")) ? string.Empty : reader["tepemam"].ToString();
                        model.URUN_ADI = reader.IsDBNull(reader.GetOrdinal("urunAdi")) ? string.Empty : reader["urunAdi"].ToString();
                        model.ISEMRINO = reader.IsDBNull(reader.GetOrdinal("isemrino")) ? string.Empty : reader["isemrino"].ToString();
                        model.MAMUL_KODU = reader.IsDBNull(reader.GetOrdinal("mamul_kodu")) ? string.Empty : reader["mamul_kodu"].ToString();
                        model.MAMUL_ADI = reader.IsDBNull(reader.GetOrdinal("mamulAdi")) ? string.Empty : reader["mamulAdi"].ToString();
                        model.HAM_KODU = reader.IsDBNull(reader.GetOrdinal("ham_kodu")) ? string.Empty : reader["ham_kodu"].ToString();
                        model.HAM_ADI = reader.IsDBNull(reader.GetOrdinal("hamAdi")) ? string.Empty : reader["hamAdi"].ToString();
                        model.RECETE_MIKTAR = reader.IsDBNull(reader.GetOrdinal("miktar")) ? 0 : Convert.ToDecimal(reader["miktar"]);

                        return model;

                    });
                }
                else {
                    parametersForEkle[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parametersForEkle[0].Value = detayRecete.MamulKodu;

                        argeDetayCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parametersForEkle, reader =>
                    {
                        Cls_Isemri model = new Cls_Isemri();

                        model.SIPARIS_NO = reader.IsDBNull(reader.GetOrdinal("tepesipno")) ? string.Empty : reader["tepesipno"].ToString();
                        model.SIPARIS_SIRA = reader.IsDBNull(reader.GetOrdinal("tepesipkont")) ? 0 : Convert.ToInt32(reader["tepesipkont"]);
                        model.TAKIP_NO = reader.IsDBNull(reader.GetOrdinal("kt_takipnum")) ? string.Empty : reader["kt_takipnum"].ToString();
                        model.REFISEMRINO = reader.IsDBNull(reader.GetOrdinal("refisemrino")) ? string.Empty : reader["refisemrino"].ToString();
                        model.URUN_KODU = reader.IsDBNull(reader.GetOrdinal("tepemam")) ? string.Empty : reader["tepemam"].ToString();
                        model.URUN_ADI = reader.IsDBNull(reader.GetOrdinal("urunAdi")) ? string.Empty : reader["urunAdi"].ToString();
                        model.ISEMRINO = reader.IsDBNull(reader.GetOrdinal("isemrino")) ? string.Empty : reader["isemrino"].ToString();
                        model.MAMUL_KODU = reader.IsDBNull(reader.GetOrdinal("mamul_kodu")) ? string.Empty : reader["mamul_kodu"].ToString();
                        model.MAMUL_ADI = reader.IsDBNull(reader.GetOrdinal("mamulAdi")) ? string.Empty : reader["mamulAdi"].ToString();
                        model.HAM_KODU = reader.IsDBNull(reader.GetOrdinal("ham_kodu")) ? string.Empty : reader["ham_kodu"].ToString();
                        model.HAM_ADI = reader.IsDBNull(reader.GetOrdinal("hamAdi")) ? string.Empty : reader["hamAdi"].ToString();
                        model.RECETE_MIKTAR = reader.IsDBNull(reader.GetOrdinal("miktar")) ? 0 : Convert.ToDecimal(reader["miktar"]);

                        return model;


                    });
                }

                return argeDetayCollection;


            }
            catch 
            {

                throw;
            }
        }
        public bool UpdateReceteGuncelleMailStatus(Cls_Arge item)
        {
            try
            {
                Variables.Query_ = "update vbtReceteGuncellemeDurum set MailGitti=1 where KayitTarihi = @kayittarihi";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kayittarihi", SqlDbType.DateTime);
                parameter[0].Value = item.KayitTarihi;

                Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_, parameter, Variables.Fabrika_);

                return Variables.Result_;

            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateReceteGuncellenmisGoster(Cls_Arge item)
        {
            try
            {
                Variables.Query_ = "update vbtReceteGuncellemeDurum set IeReceteIslemYapildi=1,IeReceteIslemZamani=getdate(),IeReceteIslemYapan=@user where KayitTarihi = @kayittarihi";

                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@kayittarihi", SqlDbType.DateTime);
                parameter[0].Value = item.KayitTarihi;
                parameter[1] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                parameter[1].Value = login.GetUserName();

                Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_, parameter, Variables.Fabrika_);

                return Variables.Result_;

            }
            catch 
            {
                return false;
            }
        }
        public async Task<bool> UpdateTuremisKodTumuAsync(string sablonKodu)
        {
            try
            {
                Variables.Query_ = "vbpTuremisKodGuncelleTumu";
                string userName = await login.GetUserNameAsync();
                
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parameters[0].Value = userName;
                parameters[1] = new SqlParameter("@sablonKodu", SqlDbType.NVarChar, 11);
                parameters[1].Value = sablonKodu;

                Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);

                return Variables.Result_;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateTuremisKodTekAsync(string sablonKodu,ObservableCollection<Cls_Arge> urunColl)
        {
            try
            {
                Variables.Query_ = "vbpTuremisKodGuncelleTek";
                string userName = await login.GetUserNameAsync();
                foreach(Cls_Arge item in  urunColl)
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                    parameters[0].Value = userName;
                    parameters[1] = new SqlParameter("@varyantKodu", SqlDbType.NVarChar, 35);
                    parameters[1].Value = item.UrunKodu;
                    Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                    if (!Variables.Result_)
                        return false;
                }


                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateTuremisKodTekAsync(string urunKodu)
        {
            try
            {
                Variables.Query_ = "vbpTuremisKodGuncelleTek";
                string userName = await login.GetUserNameAsync();
                

                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                    parameters[0].Value = userName;
                    parameters[1] = new SqlParameter("@varyantKodu", SqlDbType.NVarChar, 35);
                    parameters[1].Value = urunKodu;
                    Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                    if (!Variables.Result_)
                        return false;
                



                return true;

            }
            catch
            {
                return false;
            }
        }
        public bool UpdateTuremisKodTek(string varyantKodu,string user)
        {
            try
            {
                Variables.Query_ = "vbpTuremisKodGuncelleTek";

                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                    parameters[0].Value = user;
                    parameters[1] = new SqlParameter("@varyantKodu", SqlDbType.NVarChar, 35);
                    parameters[1].Value = varyantKodu;
                    Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                    if (!Variables.Result_)
                        return false;
                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ReceteSadelestirme()
        {
            try
            {
                Variables.Query_ = "sbppReceteSadelestirme";
                Variables.Result_ = await data.ExecuteStoredProcedureAsync(Variables.Query_, Variables.Yil_);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }

        }
        public async Task<bool> ReceteSadelestirme(string urunKodu)
        {
            try
            {
                Variables.Query_ = "sbppReceteSadelestirmeMamulBazli";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = urunKodu;
                Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameter,Variables.Fabrika_);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }

        }
        public async Task<int> GuncellenenReceteMailGonder(ObservableCollection<Cls_Arge> mailCollection)
        {
            //(ID, MamulKodu, EskiHamKodu, EskiMiktar, YeniHamKodu, YeniMiktar, GuncellemeTipi, KayitTarihi, Opno)
            try
            {
                Variables.Query_ = "truncate TABLE tempReceteDegisiklikMailGonder " +
                                    " insert into  tempReceteDegisiklikMailGonder values";
                foreach (Cls_Arge arge in mailCollection)
                {

                    Variables.Query_ += string.Format(CultureInfo.InvariantCulture,"({0},'{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}'),",
                        arge.ID, arge.MamulKodu, arge.EskiHamKodu, arge.EskiMiktar, arge.HamKodu, arge.ReceteTuketimMiktar, arge.GuncellemeTipi, arge.KayitTarihi,arge.Opno);

                }
                Variables.Query_ = Variables.Query_.Substring(0,Variables.Query_.Length - 1);
                Variables.Result_ = await data.ExecuteCommandAsync(Variables.Query_, Variables.Yil_, Variables.Fabrika_);

                    if (Variables.Result_ == false)
                        return -2;

                Variables.Query_ = "vbpReceteDegisiklikMailGonder";

                Variables.Departman_ = await login.GetDepartmentAsync();

                if (Variables.Departman_.Contains("Moduler"))
                    Variables.Departman_ = "Moduler";
                else if (Variables.Departman_.Contains("Doseme"))
                    Variables.Departman_ = "Doseme";
                else
                    return -3;

                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@departman", SqlDbType.NVarChar, 35);
                parameter[0].Value = Variables.Departman_;
                parameter[1] = new SqlParameter("@mailAddress", SqlDbType.NVarChar, 400);
                parameter[1].Value = await login.GetEmailAddressAsync();
                parameter[2] = new SqlParameter("@kullaniciAdi", SqlDbType.NVarChar, 12);
                parameter[2].Value = await login.GetUserNameAsync();

                Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                    if (Variables.Result_ == false)
                        return -4;

                return 1;

            }
            catch
            {
                return -1;
            }
        }
        public async Task<string> MailGelenIsemriRecetesiGuncelle(ObservableCollection<Cls_Arge> receteCollection,bool tamamlananlariGuncelleme)
        {
           
            try
            {
                string errorMessage = string.Empty;
                foreach (Cls_Arge ie in receteCollection)
                { 
                    Variables.Query_ = "vbpMailGelenIsemriRecetesiGuncelle";


                    SqlParameter[] parameter = new SqlParameter[3];
                    parameter[0] = new SqlParameter("@kayitTarihi", SqlDbType.DateTime);
                    parameter[0].Value = ie.KayitTarihi;
                    parameter[1] = new SqlParameter("@kullaniciAdi", SqlDbType.NVarChar, 12);
                    parameter[1].Value = await login.GetUserNameAsync();
                    parameter[2] = new SqlParameter("@tamamlananlariGuncelleme",SqlDbType.Bit);
                    parameter[2].Value = tamamlananlariGuncelleme;
                    try
                    {

                        Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                    }
                    catch 
                    {
                        errorMessage += string.Format("Mamul Kodu:{0},Ham Kodu:{0} Güncelleme Esnasında Hata İle Karşılaşıldı.", ie.MamulKodu, ie.HamKodu);
                    }
                    
                    if (!Variables.Result_ &&
                        !errorMessage.Contains(string.Format("Mamul Kodu:{0},Ham Kodu:{0} Güncelleme Esnasında Hata İle Karşılaşıldı.", ie.MamulKodu, ie.HamKodu)))
                        errorMessage += string.Format("Mamul Kodu:{0},Ham Kodu:{0} Güncelleme Esnasında Hata İle Karşılaşıldı.", ie.MamulKodu, ie.HamKodu);
                   
                }
                if(string.IsNullOrEmpty(errorMessage)) 
                    return "Başarı";
                else 
                    return errorMessage;

            }
            catch
            {
                return "Reçeteler Bildirilirken Sistemsel Bir Hata İle Karşılaşıldı.";
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string getStr)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(getStr));
        }
    }


}

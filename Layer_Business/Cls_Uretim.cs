using Layer_2_Common.Type;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layer_Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Diagnostics.Metrics;

namespace Layer_Business
{
    public class Cls_Uretim : INotifyPropertyChanged
    {
        public string SiparisNumarasi { get; set; }
        public int SiparisSira { get; set; }
        public string ReferansIsemri { get; set; }
        public string Isemrino { get; set; }
        public string Takipno { get; set; }
        public string Planno { get; set; }
        public int IsemriMiktar { get; set; }
        public DateTime IsemriTarih { get; set; }
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public string PaketKodu { get; set; }
        public string PaketAdi { get; set; }
        public string ParcaKodu { get; set; }
        public string ParcaAdi { get; set; }
        public double BirimParcaMiktar { get; set; }
        public double KabaBoy { get; set; }
        public double KabaEn { get; set; }
        public double NetBoy { get; set; }
        public double NetEn { get; set; }
        public double BitmisBoy { get; set; }
        public double BitmisEn { get; set; }
        public string HamKodu { get; set; }
        public string HamAdi { get; set; }
        public float HamMiktar { get; set; }
        public string Birim { get; set; }
        public string StokKodu { get; set; }
        public string StokAdi { get; set; }
        public string MamulKodu { get; set; }
        public string MamulAdi { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public decimal IskeletSure { get; set; }
        public decimal CilaSure { get; set; }
        public decimal MontajSure { get; set; }
        public decimal PaketSure { get; set; }
        public string UretimDurum { get; set; }
        public DateTime SonUretimTarih { get; set; }
        public int Seviye { get; set; }
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { 
                    _isChecked = value; 
                    OnPropertyChanged(nameof(IsChecked));
                }
        }
        public string BoyKenarBandi1 { get; set; }
        public string EnKenarBandi1 { get; set; }
        public string EnKenarBandi2 { get; set; }
        public string BoyKenarBandi2 { get; set; }

        private ObservableCollection<Cls_Uretim> temp_coll_uretim = new();
        public ObservableCollection<Cls_Uretim> UretimCollection = new();
        public List<string> Kod1Collection = new();
        public List<string> Kod2Collection = new();
        public List<string> Kod3Collection = new();
        Variables variables = new();
        LoginLogic login = new();
        DataLayer data = new();
        
        public Cls_Uretim()
        {
            variables.Fabrika = login.GetFabrika();

            Variables.Fabrika_ = variables.Fabrika;

        }
        public ObservableCollection<Cls_Uretim> PopulateSureEkle(Dictionary<string, string> kisitPairs)
        {
            try
            {

                Variables.Query_ = "select isnull(UrunKodu,'') as UrunKodu, isnull(UrunAdi,'') as UrunAdi, isnull(Kod1,'') as Kod1," +
                    " isnull(Kod2,'') as Kod2, isnull(Kod3,'') as Kod3, isnull(IskeletSure,0) as IskeletSure, isnull(CilaSure,0) as CilaSure, " +
                    " isnull(MontajSure,0) as MontajSure, isnull(PaketSure,0) as PaketSure from vatMamulSureHesap where 1=1 ";
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["urunKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and UrunKodu like '%' + @urunKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["urunAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and UrunAdi like '%' + @urunAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod2 like '%' + @kod2 + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                {
                    Variables.Query_ = Variables.Query_ + "and Kod3 like '%' + @kod3 + '%' ";
                    variables.Counter++;
                }

                if(variables.Counter == 0)
                {
                    temp_coll_uretim.Clear();
                    using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Cls_Uretim urun = new Cls_Uretim
                                {
                                    UrunKodu = reader["UrunKodu"].ToString(),
                                    UrunAdi = reader["UrunAdi"].ToString(),
                                    Kod1 = reader["Kod1"].ToString(),
                                    Kod2 = reader["Kod2"].ToString(),
                                    Kod3 = reader["Kod3"].ToString(),
                                    IskeletSure = Convert.ToDecimal(reader["IskeletSure"]),
                                    CilaSure = Convert.ToDecimal(reader["CilaSure"]),
                                    MontajSure = Convert.ToDecimal(reader["MontajSure"]),
                                    PaketSure = Convert.ToDecimal(reader["PaketSure"]),
                                };

                                temp_coll_uretim.Add(urun);
                            }
                        }
                    }
                }

                else
                { 

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];
                    variables.Counter = 0;

                    if (!string.IsNullOrEmpty(kisitPairs["urunKodu"]))
                    {
                        parameters[variables.Counter] = new SqlParameter("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = kisitPairs["urunKodu"];
                        variables.Counter++;
                    }
                    if (!string.IsNullOrEmpty(kisitPairs["urunAdi"]))
                    {
                        parameters[variables.Counter] = new SqlParameter("@urunAdi", SqlDbType.NVarChar, 500);
                        parameters[variables.Counter].Value = kisitPairs["urunAdi"];
                        variables.Counter++;
                    }
                    if (!string.IsNullOrEmpty(kisitPairs["kod1"]))
                    {
                        parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 500);
                        parameters[variables.Counter].Value = kisitPairs["kod1"];
                        variables.Counter++;
                    }
                    if (!string.IsNullOrEmpty(kisitPairs["kod2"]))
                    {
                        parameters[variables.Counter] = new SqlParameter("@kod2", SqlDbType.NVarChar, 500);
                        parameters[variables.Counter].Value = kisitPairs["kod2"];
                        variables.Counter++;
                    }
                    if (!string.IsNullOrEmpty(kisitPairs["kod3"]))
                    {
                        parameters[variables.Counter] = new SqlParameter("@kod3", SqlDbType.NVarChar, 500);
                        parameters[variables.Counter].Value = kisitPairs["kod3"];
                        variables.Counter++;
                    }

                    temp_coll_uretim.Clear();
                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Cls_Uretim urun = new Cls_Uretim
                                {
                                    UrunKodu = reader["UrunKodu"].ToString(),
                                    UrunAdi = reader["UrunAdi"].ToString(),
                                    IskeletSure = Convert.ToDecimal(reader["IskeletSure"]),
                                    CilaSure = Convert.ToDecimal(reader["CilaSure"]),
                                    MontajSure = Convert.ToDecimal(reader["MontajSure"]),
                                    PaketSure = Convert.ToDecimal(reader["PaketSure"]),
                                };

                                temp_coll_uretim.Add(urun);
                            }
                        }
                    }
                }
                UretimCollection = temp_coll_uretim;
                return UretimCollection;

            }
            catch { return null; }
        }
        public ObservableCollection<Cls_Uretim> PopulateTakipKartiList(Dictionary<string, string> restrictionPairs, int pageNumber)
        {
            try
            {
                if (restrictionPairs == null)
                    return null;

                Variables.Query_ = "select * from vbvUretimTakipKartiListele where 1=1 ";
                Variables.Counter_ = 0;

                if (restrictionPairs.ContainsKey("@siparisNo"))
                {
                    Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@takipno"))
                {
                    Variables.Query_ += " and Takipno like '%' + @takipno + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planno"))
                {
                    Variables.Query_ += " and planno like  '%' + @planno + '%'";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                if (restrictionPairs.ContainsKey("@siparisNo"))
                {
                    parameters[Variables.Counter_] = new("@siparisNo", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = restrictionPairs["@siparisNo"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    parameters[Variables.Counter_] = new("@urunKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = restrictionPairs["@urunKodu"];
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@takipno"))
                {
                    parameters[Variables.Counter_] = new("@takipno", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = restrictionPairs["@takipno"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planno"))
                {
                    parameters[Variables.Counter_] = new("@planno", SqlDbType.NVarChar, 435);
                    parameters[Variables.Counter_].Value = restrictionPairs["@planno"];
                    Variables.Counter_++;
                }
                Variables.Query_ += $"order by IsemriTarih desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                ObservableCollection<Cls_Uretim> tempColl = new();


                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        Cls_Uretim uretim = new Cls_Uretim
                        {
                            SiparisNumarasi = reader["SiparisNumarasi"].ToString(),
                            SiparisSira = Convert.ToInt32(reader["SiparisSira"]),
                            UrunKodu = reader["UrunKodu"].ToString(),
                            UrunAdi = reader["UrunAdi"].ToString(),
                            ReferansIsemri = reader["ReferansIsemrino"].ToString(),
                            IsemriMiktar = Convert.ToInt32(reader["Miktar"]),
                            IsemriTarih = Convert.ToDateTime(reader["IsemriTarih"]),
                        };
                        tempColl.Add(uretim);
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
        public int CountUretimTakipKartiList(Dictionary<string, string> restrictionPairs, int pageNumber)
        {
            try
            {
                if (restrictionPairs == null)
                    return 0;

                Variables.Query_ = $"select count(*) from vbvUretimTakipKartiListele where 1=1 ";
                Variables.Counter_ = 0;

                if (restrictionPairs.ContainsKey("@siparisNo"))
                {
                    Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@takipno"))
                {
                    Variables.Query_ += " and Takipno like '%' + @takipno + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planno"))
                {
                    Variables.Query_ += " and planno like  '%' + @planno + '%'";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                if (restrictionPairs.ContainsKey("@siparisNo"))
                {
                    parameters[Variables.Counter_] = new("@siparisNo", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = restrictionPairs["@siparisNo"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    parameters[Variables.Counter_] = new("@urunKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = restrictionPairs["@urunKodu"];
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@takipno"))
                {
                    parameters[Variables.Counter_] = new("@takipno", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = restrictionPairs["@takipno"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planno"))
                {
                    parameters[Variables.Counter_] = new("@planno", SqlDbType.NVarChar, 435);
                    parameters[Variables.Counter_].Value = restrictionPairs["@planno"];
                    Variables.Counter_++;
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
        public async Task<ObservableCollection<Cls_Uretim>> GetUretimTakipKartiPdfCollection(Cls_Uretim siparis)
        {
            try
            {
                SqlParameter[] sp = new SqlParameter[3];
                sp[0] = new SqlParameter("@refisemrino",SqlDbType.NVarChar,15);
                sp[1] = new SqlParameter("@urunKodu", SqlDbType.NVarChar,35);
                sp[2] = new SqlParameter("@urunAdi", SqlDbType.NVarChar,100);
                sp[0].Value = siparis.ReferansIsemri;
                sp[1].Value = siparis.UrunKodu;
                sp[2].Value = siparis.UrunAdi;
                
                ObservableCollection<Cls_Uretim> excelColl = await data.ExecuteStoredProcedureWithParametersAsyncReturnsCollection("vbpUretimTakipKarti", Variables.Yil_, sp, Variables.Fabrika_, reader =>
                {
                    Cls_Uretim model = new();
                    model.ReferansIsemri = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString();
                    model.Takipno = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                    model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                    model.PaketKodu = reader["PaketKodu"] is DBNull ? "" : reader["PaketKodu"].ToString();
                    model.PaketAdi = reader["PaketAdi"] is DBNull ? "" : reader["PaketAdi"].ToString();
                    model.Isemrino = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                    model.IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["IsemriMiktar"]);
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.Seviye = reader["Seviye"] is DBNull ? 0 : Convert.ToInt32(reader["Seviye"]);
                    model.KabaBoy = reader["KabaBoy"] is DBNull ? 0 : Convert.ToSingle(reader["KabaBoy"]);
                    model.KabaEn = reader["KabaEn"] is DBNull ? 0 : Convert.ToSingle(reader["KabaEn"]);
                    model.NetBoy = reader["NetBoy"] is DBNull ? 0 : Convert.ToSingle(reader["NetBoy"]);
                    model.NetEn = reader["NetEn"] is DBNull ? 0 : Convert.ToSingle(reader["NetEn"]);
                    model.BitmisBoy = reader["BitmisBoy"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisBoy"]);
                    model.BitmisEn = reader["BitmisEn"] is DBNull ? 0 : Convert.ToSingle(reader["BitmisEn"]);
                    model.BirimParcaMiktar = reader["ParcaBirimMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["ParcaBirimMiktar"]);

                    return model;
                });

                return excelColl;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Uretim> GetKenarBandiInfo(string stokKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(stokKodu))
                    return null;
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;
                Variables.Query_ = "vbpUretimTakipKartiKenarBandiBul";
                ObservableCollection<Cls_Uretim> kbColl = data.ExecuteStoredProcedureWithParametersReturnsCollection(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_, reader =>
                {
                    Cls_Uretim model = new();
                    model.BoyKenarBandi1 = reader["BoyKenarBandi1"] is DBNull ? "" : reader["BoyKenarBandi1"].ToString();
                    model.EnKenarBandi1 = reader["EnKenarBandi1"] is DBNull ? "" : reader["EnKenarBandi1"].ToString();
                    model.EnKenarBandi2 = reader["EnKenarBandi2"] is DBNull ? "" : reader["EnKenarBandi2"].ToString();
                    model.BoyKenarBandi2 = reader["BoyKenarBandi2"] is DBNull ? "" : reader["BoyKenarBandi2"].ToString();
                    return model;

                });
                return kbColl;

            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Uretim> GetUretimTakipKartiDetayCollection(Cls_Uretim urun)
        {
            try
            {
                if (urun == null)
                    return null;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[2] = new SqlParameter("@urunKodu", SqlDbType.NVarChar, 35);
                parameters[3] = new SqlParameter("@refisemrino", SqlDbType.NVarChar, 15);
                parameters[0].Value = urun.SiparisNumarasi;
                parameters[1].Value = urun.SiparisSira;
                parameters[2].Value = urun.UrunKodu;
                parameters[3].Value = urun.ReferansIsemri;

                Variables.Query_ =
                                    " select " +
                                    " ISNULL(ie.TEPESIPNO, '') as SiparisNumarasi, " +
                                    " ISNULL(ie.TEPESIPKONT, 0) as SiparisSira, " +
                                    " ISNULL(ie.TEPEMAM, '') as UrunKodu, " +
                                    " ISNULL(ie.REFISEMRINO, '') as ReferansIsemri, " +
                                    " ISNULL(ie.ISEMRINO, '') as Isemrino, " +
                                    " ISNULL(ie.STOK_KODU, '') as StokKodu, " +
                                    " ISNULL(sabit.stok_adi, '') as StokAdi, " +
                                    " ISNULL(ie.MIKTAR, 0) as Miktar " +
                                    " from TBLISEMRI ie (nolock) " +
                                    " left join tblstsabit sabit (nolock) on ie.stok_kodu=sabit.stok_kodu" +
                                    " where ie.tepesipno=@siparisNumarasi and ie.tepesipkont = @siparisSira" +
                                    " and ie.tepemam = @urunKodu and ie.refisemrino=@refisemrino";

                ObservableCollection<Cls_Uretim> detayColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Uretim model = new();
                    model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    model.ReferansIsemri = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
                    model.Isemrino = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.IsemriMiktar = reader["Miktar"] is DBNull ? 0 : Convert.ToInt32(reader["Miktar"]);
                    return model;
                });
                return detayColl;
            }
            catch 
            {
                return null;
            }
        }
        public List<string> GetKod1()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_1 from tblstsabit (nolock) where stok_kodu like 'm%'  order by kod_1 asc";
                using(SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika)) 
                {

                    if (reader == null || reader.HasRows == false)
                        return null;
                    
                    Kod1Collection.Clear();
                    while (reader.Read()) 
                    {
                        Kod1Collection.Add(reader[0].ToString());
                    }
        
                }

                return Kod1Collection;
            }
            catch 
            {
                return null;
            }
        }
        public List<string> GetKod2()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_2 from tblstsabit (nolock) where stok_kodu like 'm%'  order by kod_2 asc";
                using(SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika)) 
                {

                    if (reader == null || reader.HasRows == false)
                        return null;
                    
                    Kod2Collection.Clear();
                    while (reader.Read()) 
                    {
                        Kod2Collection.Add(reader[0].ToString());
                    }
        
                }

                return Kod2Collection;
            }
            catch 
            {
                return null;
            }
        }
        public List<string> GetKod3()
        {
            try
            {
                Variables.Query_ = "Select distinct kod_3 from tblstsabit (nolock) where stok_kodu like 'm%' order by kod_3 asc";
                using(SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika)) 
                {

                    if (reader == null || reader.HasRows == false)
                        return null;
                    
                    Kod3Collection.Clear();
                    while (reader.Read()) 
                    {
                        Kod3Collection.Add(reader[0].ToString());
                    }
        
                }

                return Kod3Collection;
            }
            catch 
            {
                return null;
            }
        }
        public  async Task<ObservableCollection<Cls_Uretim>> GetHamKoduInfoAsync(string isemrino)
        {
            try
            {
                Variables.Query_ = "Select ham_kodu,stok_adi,sum(rec.miktar) as miktar,sabit.OLCU_BR1 from tblisemrirec rec (nolock) left join tblstsabit sabit (nolock) on rec.ham_kodu = sabit.stok_kodu where rec.isemrino = @isemrino group by ham_kodu,stok_adi,olcu_br1";
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@isemrino",SqlDbType.NVarChar,15);
                sp[0].Value = isemrino;

                ObservableCollection<Cls_Uretim> hamColl = await data.Select_Command_Data_Async_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,sp,reader =>
                {
                    Cls_Uretim model = new();
                    model.HamKodu = reader["ham_kodu"] is DBNull == true ? "" : reader["ham_kodu"].ToString();
                    model.HamAdi = reader["stok_adi"] is DBNull == true ? "" : reader["stok_adi"].ToString(); 
                    model.HamMiktar = reader["miktar"] is DBNull == true ? 0 : Convert.ToSingle(reader["miktar"]);
                    model.Birim = reader["OLCU_BR1"] is DBNull == true ? "" : reader["OLCU_BR1"].ToString();
                    return model;
                });

                return hamColl;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ObservableCollection<Cls_Uretim>> GetKumasUretimDurumAsync(ObservableCollection<Cls_Uretim>excelCollection)
        {
            try
            {
                Variables.Query_ = "vbpKonfeksiyonUretimDurum";
                foreach (Cls_Uretim item in excelCollection)
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@stokadi", SqlDbType.NVarChar, 100);
                    parameters[0].Value = item.StokAdi;

                    ObservableCollection<Cls_Uretim> kumas = await data.ExecuteStoredProcedureWithParametersAsyncReturnsCollection(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_, reader =>
                    {
                        Cls_Uretim model = new();
                        model.UretimDurum = reader["uretimDurum"] is DBNull == true ? "" : reader["uretimDurum"].ToString();
                        model.SonUretimTarih = reader["sonUretimTarih"] is DBNull == true ? DateTime.MinValue : Convert.ToDateTime(reader["sonUretimTarih"]);
                        return model;
                    });
                    item.UretimDurum = kumas.Select(u => u.UretimDurum).FirstOrDefault();
                    item.SonUretimTarih = kumas.Select(t => t.SonUretimTarih).FirstOrDefault();
                }
                return excelCollection;
              
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

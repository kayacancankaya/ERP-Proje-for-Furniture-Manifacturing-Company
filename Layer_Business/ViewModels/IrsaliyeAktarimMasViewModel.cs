using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business.ViewModels
{
    public class IrsaliyeAktarimMasViewModel
    {
        public string Fisno { get; set; } = string.Empty;
        public DateTime Tarih { get; set; } = DateTime.MinValue;
        public string CariKodu { get; set; } = string.Empty;
        public string CariAdi {  get; set; } = string.Empty;
        public float ToplamDovizFiyat { get; set; } = 0;
        public int DovizTipi { get; set; } = 0;
        public float ToplamTutarTL { get; set; } = 0;
        public ObservableCollection<IrsaliyeAktarimSatirViewModel> IrsaliyeAktarimSatirlari { get; set; }
        DataLayer data = new();
        public ObservableCollection<IrsaliyeAktarimMasViewModel> GetTop20IrsaliyeAktarimList()
        {
            try
            {
                Variables.Query_ = "vbpAktarilacakIrsaliyeListele";

                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@totalResult", SqlDbType.Int);
                parameter[0].Value = 20;
                parameter[1] = new SqlParameter("@detayMi", SqlDbType.Bit);
                parameter[1].Value = false;
                parameter[2] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[2].Value = string.Empty;

                ObservableCollection<IrsaliyeAktarimMasViewModel> modelColl = data.ExecuteStoredProcedureWithParametersReturnsCollection(Variables.Query_, Variables.Yil_, parameter,Variables.Fabrika_, reader =>
                {
                    IrsaliyeAktarimMasViewModel model = new();
                    model.Fisno = reader["Fisno"] is DBNull ? "" : reader["Fisno"].ToString();
                    model.Tarih = reader["Tarih"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["Tarih"]);
                    model.CariKodu = reader["CariKodu"] is DBNull ? "" : reader["CariKodu"].ToString();
                    model.CariAdi = reader["CariAdi"] is DBNull ? "" : EntryControls.FixTurkishCharacters(reader["CariAdi"].ToString());
                    model.ToplamDovizFiyat = reader["ToplamDovizFiyat"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamDovizFiyat"]);
                    model.DovizTipi = reader["DovizTipi"] is DBNull ? 0 : Convert.ToInt32(reader["DovizTipi"]);
                    model.ToplamTutarTL = reader["ToplamTutarTL"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamTutarTL"]);
                    return model;
                });
                foreach (IrsaliyeAktarimMasViewModel model in modelColl)
                    model.IrsaliyeAktarimSatirlari = GetIrsaliyeAktarimSatirlari(model.Fisno);
                return modelColl;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<IrsaliyeAktarimMasViewModel> GetIrsaliyeAktarimList(Dictionary<string,string> filterDic,int pageNumber)
        {
            try
            {
                if (filterDic == null)
                    return null;


                Variables.Query_ = "vbpAktarilacakIrsaliyeTabloOlustur";

                Variables.Result_ = data.ExecuteStoredProcedure(Variables.Query_, Variables.Yil_);

                Variables.Query_ = @"with cte as (select distinct Fisno,Tarih,CariKodu,CariAdi,ToplamDovizFiyat,DovizTipi,ToplamTutarTl 
                                    from tempIrsaliyeAktarimList)
                                    select * from cte where 1=1 ";

                SqlParameter[] parameters = new SqlParameter[filterDic.Count];
                Variables.Counter_ = 0;
                if (filterDic.ContainsKey("Fisno"))
                {
                    Variables.Query_ += " and Fisno like + '%' + @fisno + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = filterDic["Fisno"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Tarih"))
                {
                    Variables.Query_ += " and Tarih like + '%' + @tarih + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@tarih", SqlDbType.DateTime);
                    parameters[Variables.Counter_].Value = Convert.ToDateTime(filterDic["Tarih"]);
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Cari Kodu"))
                {
                    Variables.Query_ += " and CariKodu like + '%' + @cariKodu + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = filterDic["Cari Kodu"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Cari Adı"))
                {
                    Variables.Query_ += " and CariAdi like + '%' + @cariAdi + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@cariAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = filterDic["Cari Adı"];
                    Variables.Counter_++;
                }

                Variables.Query_ += " order by fisno desc";
                Variables.Query_ += $" offset {(pageNumber - 1) * 20} rows fetch next 20 rows only ";
                ObservableCollection<IrsaliyeAktarimMasViewModel> modelColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameters, reader =>
                {
                    IrsaliyeAktarimMasViewModel model = new();
                    model.Fisno = reader["Fisno"] is DBNull ? "" : reader["Fisno"].ToString();
                    model.Tarih = reader["Tarih"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["Tarih"]);
                    model.CariKodu = reader["CariKodu"] is DBNull ? "" : reader["CariKodu"].ToString();
                    model.CariAdi = reader["CariAdi"] is DBNull ? "" : EntryControls.FixTurkishCharacters(reader["CariAdi"].ToString());
                    model.ToplamDovizFiyat = reader["ToplamDovizFiyat"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamDovizFiyat"]);
                    model.DovizTipi = reader["DovizTipi"] is DBNull ? 0 : Convert.ToInt32(reader["DovizTipi"]);
                    model.ToplamTutarTL = reader["ToplamTutarTL"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamTutarTL"]);
                    return model;
                });
                foreach (IrsaliyeAktarimMasViewModel model in modelColl)
                    model.IrsaliyeAktarimSatirlari = GetIrsaliyeAktarimSatirlari(model.Fisno);
                return modelColl;
            }
            catch
            {
                return null;
            }
        }
        public int CountAktarimList(Dictionary<string, string> filterDic)
        {
            try
            {
                if (filterDic == null)
                    return -1;
                if (filterDic.Count == 0)
                    return -1;

                Variables.Query_ = "vbpAktarilacakIrsaliyeTabloOlustur";

                Variables.Result_ = data.ExecuteStoredProcedure(Variables.Query_, Variables.Yil_);

                if (!Variables.Result_)
                    return -1;

                Variables.Query_ = @"with cte as (select distinct Fisno,Tarih,CariKodu,CariAdi,ToplamDovizFiyat,DovizTipi,ToplamTutarTl 
                                    from tempIrsaliyeAktarimList)
                                    select count(*) from cte where 1=1 ";

                SqlParameter[] parameters = new SqlParameter[filterDic.Count];
                Variables.Counter_ = 0;
                if (filterDic.ContainsKey("Fisno"))
                {
                    Variables.Query_ += " and Fisno like + '%' + @fisno + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = filterDic["Fisno"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Tarih"))
                {
                    Variables.Query_ += " and Tarih like + '%' + @tarih + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@tarih", SqlDbType.DateTime);
                    parameters[Variables.Counter_].Value = Convert.ToDateTime(filterDic["Tarih"]);
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Cari Kodu"))
                {
                    Variables.Query_ += " and CariKodu like + '%' + @cariKodu + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = filterDic["Cari Kodu"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Cari Adı"))
                {
                    Variables.Query_ += " and CariAdi like + '%' + @cariAdi + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@cariAdi", SqlDbType.NVarChar, 400);
                    parameters[Variables.Counter_].Value = filterDic["Cari Adı"];
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
        public int CountAktarimList()
        {
            try
            {
                Variables.Query_ = "vbpAktarilacakIrsaliyeTabloOlustur";

                Variables.Result_ = data.ExecuteStoredProcedure(Variables.Query_, Variables.Yil_);

                if(!Variables.Result_)
                    return -1;

                Variables.Query_ = @"with cte as (select distinct Fisno,Tarih,CariKodu,CariAdi,ToplamDovizFiyat,DovizTipi,ToplamTutarTl 
                                    from tempIrsaliyeAktarimList)
                                    select count(*) from cte where 1=1 ";

                
                Variables.ResultInt_ = data.Get_One_Int_Result_Command(Variables.Query_, Variables.Yil_, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public ObservableCollection<IrsaliyeAktarimSatirViewModel> GetIrsaliyeAktarimSatirlari(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return null;

                Variables.Query_ = "vbpAktarilacakIrsaliyeListele";

                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@totalResult", SqlDbType.Int);
                parameter[0].Value = 0;
                parameter[1] = new SqlParameter("@detayMi", SqlDbType.Bit);
                parameter[1].Value = true;
                parameter[2] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[2].Value = fisno;

                ObservableCollection<IrsaliyeAktarimSatirViewModel> modelColl = data.ExecuteStoredProcedureWithParametersReturnsCollection(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_, reader =>
                {
                    IrsaliyeAktarimSatirViewModel model = new();
                    model.Fisno = fisno;
                    model.Sira = reader["Sira"] is DBNull ? 0 : Convert.ToInt32(reader["Sira"]);
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.Miktar = reader["Miktar"] is DBNull ? 0 : Convert.ToSingle(reader["Miktar"]);
                    model.DovizFiyat = reader["DovizFiyat"] is DBNull ? 0 : Convert.ToSingle(reader["DovizFiyat"]);
                    model.DovizTipi = reader["DovizTipi"] is DBNull ? 0 : Convert.ToInt32(reader["DovizTipi"]);
                    model.NetFiyat = reader["NetFiyat"] is DBNull ? 0 : Convert.ToSingle(reader["NetFiyat"]);
                    model.BrutFiyat = reader["BrutFiyat"] is DBNull ? 0 : Convert.ToSingle(reader["BrutFiyat"]);
                    return model;
                });

                return modelColl;
            }
            catch
            {
                return null;
            }
        }
        public bool InsertAktarilacakIrsaliye(string eskiFisno,string yeniFisno,string cariKodu,int exportType)
        {
            try
            {
                if(string.IsNullOrEmpty(eskiFisno) ||
                    string.IsNullOrEmpty(yeniFisno) ||
                    string.IsNullOrEmpty(cariKodu) ||
                    exportType<1)
                    return false;

                Variables.Query_ = "vbpInsertIrsaliyeAktarim";
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@eskiFisno",SqlDbType.NVarChar,15);
                parameters[1] = new SqlParameter("@yeniFisno",SqlDbType.NVarChar,15);
                parameters[2] = new SqlParameter("@cariKodu",SqlDbType.NVarChar,35);
                parameters[3] = new SqlParameter("@exportType",SqlDbType.Int);
                parameters[0].Value = eskiFisno;
                parameters[1].Value = yeniFisno;
                parameters[2].Value = cariKodu;
                parameters[3].Value = exportType;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);

                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public void AktarilacakIrsaliyeKayitGeriAl(string eskiFisno,string yeniFisno)
        {
            try
            {
                if (string.IsNullOrEmpty(eskiFisno) &&
                    string.IsNullOrEmpty(yeniFisno))
                    return;
                if (string.IsNullOrEmpty(eskiFisno))
                    eskiFisno = string.Empty;
                if (string.IsNullOrEmpty(yeniFisno))
                    yeniFisno = string.Empty;

                Variables.Query_ = "vbpDeleteIrsaliyeAktarim";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@eskiFisno", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@yeniFisno", SqlDbType.NVarChar, 15);
                parameters[0].Value = eskiFisno;
                parameters[1].Value = yeniFisno;

                data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);

            }
            catch 
            {

            }
        }
    }
}

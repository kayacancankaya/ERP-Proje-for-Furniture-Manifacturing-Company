using Layer_2_Common.Type;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layer_Data;
using System.Reflection;
using System.Globalization;

namespace Layer_Business
{
    public class Cls_Fiyat
    {
        public string FiyatGrubu { get; set; } = string.Empty;
        public string StokKodu { get; set; } = string.Empty;
        public string StokAdi { get; set; } = string.Empty;
        public string AlisSatis { get; set; } = string.Empty;
        public float Fiyat { get; set; } = 0;
        public int DovizTipi { get; set; } = 0;
        public DateTime Tarih { get; set; } = DateTime.MinValue;
        public DateTime BaslangicTarih { get; set; } = DateTime.MinValue;
        public DateTime BitisTarih { get; set; } = DateTime.MinValue;
        public string KayitDurum { get; set; } = "Kaydedilmedi...";
 
        DataLayer data = new();
        LoginLogic login = new();

        public Cls_Fiyat()
        {
            Variables.Fabrika_ = login.GetFabrika();
            Variables.UserName = login.GetUserName();
            Variables.UserID = login.GetUserId();
            Variables.Departman_ = login.GetDepartment();
        }
        public ObservableCollection<Cls_Fiyat> GetFiyatList(Dictionary<string, string> dic, int pageNumber)
        {
            try
            {

                Variables.Query_ = @"select stokkodu,sabit.stok_adi,isnull(fiat.fiyat1,0) as fiyat1,fiyatdoviztipi,isnull(fiyatgrubu,'----') as fiyatgrubu, 
                                BASTAR,BITTAR
                                from tblstsabit sabit (nolock) 
                                left join tblstokfiat fiat (nolock) on fiat.stokkodu=sabit.stok_kodu
                                where 1=1 ";

                SqlParameter[] parameters = new SqlParameter[0];
                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        parameters = new SqlParameter[dic.Count];
                        Variables.Counter_ = 0;
                        if (dic.Keys.Contains("FIYATGRUBU"))
                        {
                            Variables.Query_ += " AND FIYATGRUBU = @fiyatgrubu ";
                            parameters[Variables.Counter_] = new SqlParameter("@fiyatgrubu", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = dic["FIYATGRUBU"];
                            Variables.Counter_++;
                        }
                        if (dic.Keys.Contains("STOKKODU"))
                        {
                            Variables.Query_ += " AND STOKKODU like '%' + @stokKodu + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[Variables.Counter_].Value = dic["STOKKODU"];
                            Variables.Counter_++;
                        }
                        if (dic.Keys.Contains("KOD1"))
                        {
                            Variables.Query_ += " AND kod_1 = @kod1 ";
                            parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = dic["KOD1"];
                            Variables.Counter_++;
                        }
                    }
                }
                Variables.Query_ += "order by fiyatgrubu,stokkodu desc ";
                Variables.Query_ += $"offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";
                ObservableCollection<Cls_Fiyat> fiyatColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Fiyat model = new();
                    model.FiyatGrubu = reader["fiyatgrubu"] is DBNull ? "----" : reader["fiyatgrubu"].ToString();
                    model.StokKodu = reader["stokkodu"] is DBNull ? string.Empty : reader["stokkodu"].ToString();
                    model.StokAdi = reader["stok_adi"] is DBNull ? string.Empty : reader["stok_adi"].ToString();
                    model.Fiyat = reader["FIYAT1"] is DBNull ? 0 : Convert.ToSingle(reader["FIYAT1"]);
                    model.DovizTipi = reader["FIYATDOVIZTIPI"] is DBNull ? 0 : Convert.ToInt32(reader["FIYATDOVIZTIPI"]);
                    model.BaslangicTarih = reader["BASTAR"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["BASTAR"]);
                    model.BitisTarih = reader["BITTAR"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["BITTAR"]);
                    model.KayitDurum = "Kaydedilmedi...";

                    return model;
                });
                return fiyatColl;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public int CountFiyatList(Dictionary<string, string> dic, int pageNumber)
        {
            try
            {

                Variables.Query_ = @"select count (*)
                                from tblstsabit sabit (nolock) 
                                left join tblstokfiat fiat (nolock) on fiat.stokkodu=sabit.stok_kodu
                                where 1=1 ";

                SqlParameter[] parameters = new SqlParameter[0];
                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        parameters = new SqlParameter[dic.Count];
                        Variables.Counter_ = 0;
                        if (dic.Keys.Contains("FIYATGRUBU"))
                        {
                            Variables.Query_ += " AND FIYATGRUBU = @fiyatgrubu ";
                            parameters[Variables.Counter_] = new SqlParameter("@fiyatgrubu", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = dic["FIYATGRUBU"];
                            Variables.Counter_++;
                        }
                        if (dic.Keys.Contains("STOKKODU"))
                        {
                            Variables.Query_ += " AND STOK_KODU like '%' + @stokKodu + '%' ";
                            parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[Variables.Counter_].Value = dic["STOKKODU"];
                            Variables.Counter_++;
                        }
                        if (dic.Keys.Contains("KOD1"))
                        {
                            Variables.Query_ += " AND kod_1 = @kod1 ";
                            parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                            parameters[Variables.Counter_].Value = dic["KOD1"];
                            Variables.Counter_++;
                        }
                    }
                }
                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                
                return Variables.ResultInt_;

            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<ObservableCollection<Cls_Fiyat>> InsertUpdateFiyatAsync(ObservableCollection<Cls_Fiyat> excelCollection)
        {
            try
            {
                if (excelCollection == null) return null;
                if (excelCollection.Count == 0) return null;
                foreach (Cls_Fiyat item in excelCollection)
                {
                    Variables.Query_ = "vbpInsertUpdateFiyat";
                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[0].Value = item.StokKodu;
                    parameters[1] = new SqlParameter("@fiyatgrubu", SqlDbType.NVarChar, 100);
                    parameters[1].Value = item.FiyatGrubu;
                    parameters[2] = new SqlParameter("@fiyatdoviztipi", SqlDbType.Int);
                    parameters[2].Value = item.DovizTipi;
                    parameters[3] = new SqlParameter("@alis_satis", SqlDbType.NVarChar, 1);
                    parameters[3].Value = item.AlisSatis;
                    parameters[4] = new SqlParameter("@user", SqlDbType.Int);
                    parameters[4].Value = Variables.UserID;
                    parameters[5] = new SqlParameter("@fiyat", SqlDbType.Float);
                    parameters[5].Value = item.Fiyat;
                    Variables.ResultInt_ = await data.ExecuteStoredProcedureWithParametersAsyncReturnsRecordsAffected(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                    if (Variables.ResultInt_ > 0)
                    {
                        item.KayitDurum = "Kayıt Başarılı...";
                        continue;
                    }
                    if (Variables.ResultInt_ == -1)
                        item.KayitDurum = "Kayıt Yapılırken Sistemsel Bir Hata İle Karşılaşıldı.";

                }

                return excelCollection;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteFiyatAsync(ObservableCollection<Cls_Fiyat> excelCollection)
        {
            try
            {
                if (excelCollection == null) return false;
                if (excelCollection.Count == 0) return false;
                foreach (Cls_Fiyat item in excelCollection)
                {
                    Variables.Query_ = "delete from tblstokfiat where fiyatgrubu=@fiyatGrubu and stokkodu=@stokKodu";
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[0].Value = item.StokKodu;
                    parameters[1] = new SqlParameter("@fiyatgrubu", SqlDbType.NVarChar, 100);
                    parameters[1].Value = item.FiyatGrubu;
                    
                    Variables.Result_ = await data.ExecuteCommandWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
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
        public ObservableCollection<string> GetFiyatGrubu()
        {
            try
            {
                ObservableCollection<string> fiyatGrubuList = new();
                Variables.Query_ = "select distinct FIYATGRUBU FROM TBLSTOKFIAT ";
                if (Variables.Departman_.Contains("Planlama") ||
                    Variables.Departman_.Contains("Satin"))
                    Variables.Query_ += " where fiyatgrubu like 'H%'";
                Variables.Query_ += " order by FIYATGRUBU DESC";

                ObservableCollection<string> fiyatColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    string fiyat = reader["FIYATGRUBU"] is DBNull ? string.Empty : reader["FIYATGRUBU"].ToString();
                    return fiyat;
                });
                return fiyatColl;

            }
            catch 
            {
                ObservableCollection<string> emptyList = new();
                return emptyList;
            }
        }

        public async Task<bool> InsertBulkFiyatAsync(DataTable excelTable)
        {
            try
            {
                if (excelTable == null) return false;
                if (excelTable.Rows.Count == 0) return false;

                Dictionary<string, string> mappingDic = new();

                DataColumn a_s = new DataColumn("A_S", typeof(string));

                string stokKodu = excelTable.Rows[0]["STOKKODU"].ToString();
                if(stokKodu.StartsWith("M") ||
                    stokKodu.StartsWith("SSH"))
                    a_s.DefaultValue = "S";
                else
                    a_s.DefaultValue = "A";

                excelTable.Columns.Add(a_s);

                DateTime bittar = DateTime.Now.AddMonths(1);
                if (DateTime.TryParse(excelTable.Rows[0]["BASTAR"].ToString(), out bittar))
                {
                    bittar = bittar.AddMonths(1);
                    DataColumn bittarColumn = new DataColumn("BITTAR", typeof(DateTime));
                    bittarColumn.DefaultValue = bittar.ToString("yyyy-MM-dd hh:mm:ss.fff");
                    excelTable.Columns.Add(bittarColumn);
                }
                else
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Format Hatası");
                    return false;
                }
                DateTime bastar = DateTime.Now;
                string fiyatGrubu = string.Empty;
                if (DateTime.TryParse(excelTable.Rows[0]["BASTAR"].ToString(), out bastar))
                {
                    if (a_s.DefaultValue == "A")
                        fiyatGrubu = string.Format("H{0}{1}", bastar.Year.ToString(), bastar.Month.ToString());
                    else
                        fiyatGrubu = string.Format("VITA-{0}-{1}", bastar.Year.ToString(), bastar.Month.ToString());

                    DataColumn fiyatGrubuColumn = new DataColumn("FIYATGRUBU", typeof(string));
                    fiyatGrubuColumn.DefaultValue = fiyatGrubu;
                    excelTable.Columns.Add(fiyatGrubuColumn);
                }
                else
                {
                    CRUDmessages.GeneralFailureMessageCustomMessage("Tarih Format Hatası");
                    return false;
                }

                DataColumn kulIDColumn = new DataColumn("KAYITKULLID", typeof(int));
                kulIDColumn.DefaultValue = login.GetUserId();
                excelTable.Columns.Add(kulIDColumn);

                DataColumn kayittarColumn = new DataColumn("KAYITTAR", typeof(DateTime));
                kayittarColumn.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                excelTable.Columns.Add(kayittarColumn);

                DataColumn isletmeKoduColumn = new DataColumn("ISLETME_KODU", typeof(int));
                isletmeKoduColumn.DefaultValue = 1;
                excelTable.Columns.Add(isletmeKoduColumn);

                mappingDic.Add("STOKKODU", "STOKKODU");
                mappingDic.Add("A_S", "A_S");
                mappingDic.Add("FIYAT1", "FIYAT1");
                mappingDic.Add("FIYATDOVIZTIPI", "FIYATDOVIZTIPI");
                mappingDic.Add("KAYITTAR", "KAYITTAR");
                mappingDic.Add("BASTAR", "BASTAR");
                mappingDic.Add("BITTAR", "BITTAR");
                mappingDic.Add("FIYATGRUBU", "FIYATGRUBU");
                mappingDic.Add("KAYITKULLID", "KAYITKULLID");
                mappingDic.Add("ISLETME_KODU", "ISLETME_KODU");

                Variables.Result_ = await data.BulkInsertToSqlAsync(excelTable,"TBLSTOKFIAT",mappingDic,Variables.Yil_,Variables.Fabrika_);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }

    }
}

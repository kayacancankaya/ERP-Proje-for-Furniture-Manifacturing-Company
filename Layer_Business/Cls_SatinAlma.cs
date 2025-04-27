using Layer_2_Common.Type;
using Layer_Data;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Layer_Business
{
    public class Cls_SatinAlma : INotifyPropertyChanged
    {

        public string PlanAdi { get; set; }

        public int PlanAdiSira { get; set; }


        private string _stokKodu = "";

        public string StokKodu
        {
            get { return _stokKodu; }
            set
            {
                _stokKodu = value;
                OnPropertyChanged(nameof(StokKodu));
            }
        }
        private string _stokAdi = "";

        public string StokAdi
        {
            get { return _stokAdi; }
            set
            {
                _stokAdi = value;
                OnPropertyChanged(nameof(StokAdi));
            }
        }

        public string StokBirim { get; set; } = string.Empty;
        public float StokFiyat { get; set; } = 0;

        public int DepoKodu { get; set; }

        private string _siparisNumarasi = "";

        public string SiparisNumarasi
        {
            get { return _siparisNumarasi; }
            set
            {
                _siparisNumarasi = value;
                OnPropertyChanged(nameof(SiparisNumarasi));
            }
        }
        private int _siparisSira = 0;

        public int SiparisSira
        {
            get { return _siparisSira; }
            set
            {
                _siparisSira = value;
                OnPropertyChanged(nameof(SiparisSira));
            }
        }

        public string MusteriSiparisNumarasi { get; set; }
        public int MusteriSiparisSira { get; set; } = 0;


        private int _siparisMiktar;
        public int SiparisMiktar
        {
            get { return _siparisMiktar; }
            set
            {
                _siparisMiktar = value;
                OnPropertyChanged(nameof(SiparisMiktar));
            }
        }
        private decimal _dovizFiyat = 0;

        public decimal DovizFiyat
        {
            get { return _dovizFiyat; }
            set
            {
                _dovizFiyat = value;
                OnPropertyChanged(nameof(DovizFiyat));
            }
        }
        private decimal _siparisFiyat = 0;
        public decimal SiparisFiyat
        {
            get { return _siparisFiyat; }
            set
            {
                _siparisFiyat = value;
                OnPropertyChanged(nameof(SiparisFiyat));
            }
        }
        private double _siparisToplamTutar;
        public double SiparisToplamTutar
        {
            get { return _siparisToplamTutar; }
            set { _siparisToplamTutar = value; }
        }
        public double SiparisToplamKDV { get;set; }
        public int Vade { get; set; }

        private string _siparisTarih;

        public string SiparisTarih
        {
            get { return _siparisTarih; }
            set { _siparisTarih = value; }
        }

        public string? PlanNo { get; set; }

        private string _siparisDurum = "H";

        public string SiparisDurum
        {
            get { return _siparisDurum; }
            set
            {
                _siparisDurum = value;
                OnPropertyChanged(nameof(SiparisDurum));
            }
        }
        public string IrsaliyeNumarasi { get; set; }
        public DateTime VadeTarihi { get; set; }
        public int OdemeGunu { get; set; }

        public string TalepNumarasi { get; set; }
        
        public int TalepSira {  get; set; } 

        public float TalepMiktar { get; set; } 

        public float SiparislestirilmisTalepMiktar { get;set; }
        public decimal SiparislestirilmisMiktar { get; set; }
        public decimal KalanSiparisMiktar { get; set; }

        public float KalanTeslimMiktar { get; set; }
        public float KalanTalepMiktar { get; set; }

        public string TalepDurum { get; set; } = "H";

        public string Kod1 { get; set; } = string.Empty;

        private DateTime _terminTarih = DateTime.Now.AddMonths(2);

        public DateTime TerminTarih
        {
            get { return _terminTarih; }
            set { _terminTarih = value; }
        }

        private DateTime _talepTarih = DateTime.Now;

        public DateTime TalepTarih
        {
            get { return _talepTarih; }
            set { _talepTarih = value; }
        }
        private DateTime _teslimTarih = DateTime.Now.AddMonths(1); // işemrinin teslim tarihi
        public DateTime TeslimTarih
        {
            get { return _teslimTarih; }
            set { _teslimTarih = value; }
        }
       
        private string _siparisAciklama = "";
        public string SiparisAciklama
        {
            get { return _siparisAciklama; }
            set
            {
                _siparisAciklama = value;
                OnPropertyChanged(nameof(SiparisAciklama));
            }
        }
        public decimal TedarikSiparisMiktar { get; set; }
        public int FaturaKalemAdedi { get; set; }

        public string DovizTipi { get; set; }

        private string _siparisTipi = "";//yurt içi yurt dışı
        public string SiparisTipi
        {
            get { return _siparisTipi; }
            set
            {
                _siparisTipi = value;
                OnPropertyChanged(nameof(SiparisTipi));
            }
        }

        private decimal _stokKDV = 10;
        public decimal StokKDV
        {
            get { return _stokKDV; }
            set
            {
                _stokKDV = value;
                OnPropertyChanged(nameof(StokKDV));
            }
        }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int TeslimMiktar { get; set; }
        public int AcikSevkMiktar { get; set; }
        public decimal DepoMiktar { get; set; }
        public int SevkMiktar { get; set; }
        public string CariKodu { get; set; }
        public string CariAdi { get; set; }
        public string SonCari { get; set; }
        public string SonCariIsim { get; set; }
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
        public decimal MakSiparisMiktar { get; set; }
        public string KapaliMi { get; set; }

        public int DovizTipiInt { get; set; }
        public string ReferansIsemrino { get; set; }

        private ObservableCollection<Cls_SatinAlma> temp_coll_satin_alma = new();

        public ObservableCollection<Cls_SatinAlma> SatınAlmaCollection = new();
        
        DataLayer data = new();
        Variables variables = new();
        LoginLogic login = new();

        public Cls_SatinAlma()
        {
            variables.Fabrika = login.GetFabrika();

            Variables.Fabrika_ = variables.Fabrika;
            variables.Departman = login.GetDepartment();

            Variables.Departman_ = variables.Departman;
        }
        public ObservableCollection<Cls_SatinAlma> PopulateTalepSiparislestirmeList(Dictionary<string,string> constraints, int pageNumber)
        {
            try
            {
                Variables.Query_ = "select * from vbvTalepSiparislestirme where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanNo"]))
                {
                    Variables.Query_ = Variables.Query_ + "and PlanNo = @planNo ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and PlanAdi = @planAdi ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanNo"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["PlanNo"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                    parameters[variables.Counter].Value = constraints["PlanAdi"];
                    variables.Counter++;
                }
                Variables.Query_ += $"order by TalepNumarasi desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                SatınAlmaCollection = data.Select_Command_Data_With_Parameters(Variables.Query_,Variables.Yil_,variables.Fabrika,parameters, reader =>
                                      {
                                          Cls_SatinAlma model = new ();
                                          model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                                          model.PlanNo = reader["PlanNo"] is DBNull ? "" : reader["PlanNo"].ToString();
                                          model.MusteriSiparisNumarasi = reader["MusteriSiparisNumarasi"] is DBNull ? "" : reader["MusteriSiparisNumarasi"].ToString();
                                          model.MusteriSiparisSira = reader["MusteriSiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["MusteriSiparisSira"]);
                                          model.ReferansIsemrino = reader["ReferansIsemrino"] is DBNull ? "" : reader["ReferansIsemrino"].ToString();
                                          model.TalepNumarasi = reader["TalepNumarasi"] is DBNull ? "" : reader["TalepNumarasi"].ToString();
                                          model.TalepSira = reader["TalepSira"] is DBNull ? 0 : Convert.ToInt32(reader["TalepSira"]);
                                          model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                                          model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                                          model.SonCari = reader["SonCari"] is DBNull ? "" : reader["SonCari"].ToString();
                                          model.SonCariIsim = reader["SonCariIsim"] is DBNull ? "" : reader["SonCariIsim"].ToString();
                                          model.StokBirim = reader["StokBirim"] is DBNull ? "" : reader["StokBirim"].ToString();
                                          model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                                          model.DepoKodu = reader["DepoKodu"] is DBNull ? 0 : Convert.ToInt32(reader["DepoKodu"]);
                                          model.TeslimTarih = reader["TeslimTarih"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["TeslimTarih"]); 
                                          model.TalepMiktar = reader["TalepMiktar"] is DBNull ? 0.00000f : Convert.ToSingle(reader["TalepMiktar"]);
                                          model.SiparislestirilmisTalepMiktar = reader["SiparislestirilmisTalepMiktar"] is DBNull ? 0.00000f : Convert.ToSingle(reader["SiparislestirilmisTalepMiktar"]);
                                          model.KalanTalepMiktar = reader["KalanTalepMiktar"] is DBNull ? 0.00000f : Convert.ToSingle(reader["KalanTalepMiktar"]);
                                          model.StokFiyat = reader["fiyat"] is DBNull ? 0.00000f : Convert.ToSingle(reader["fiyat"]);
                                          model.DovizTipi = reader["DovizTipi"] is DBNull ? "" : reader["DovizTipi"].ToString();

                                          return model;
                                     });
                if (SatınAlmaCollection == null)
                    return null;

                return SatınAlmaCollection;
            }
            catch 
            {
                return null;
            }
        }
        public int CountTalepSiparislestirmeList(Dictionary<string, string> constraints, int pageNumber)
        {
            try
            {
                Variables.Query_ = "select count(*) from vbvTalepSiparislestirme where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanNo"]))
                {
                    Variables.Query_ = Variables.Query_ + "and PlanNo = @planNo ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and PlanAdi = @planAdi ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanNo"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["PlanNo"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["PlanAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                    parameters[variables.Counter].Value = constraints["PlanAdi"];
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
        public ObservableCollection<Cls_SatinAlma> PopulateTalepReportList(Dictionary<string, string> constraints, int pageNumber, bool kapaliTalepleriGosterme , bool siparislestirilenleriGosterme, bool teslimEdilenleriGosterme)
        {
            try
            {
                Variables.Query_ = "select * from vbvTedarikciTalepRapor where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and SiparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
                    variables.Counter++;
                }

                if(kapaliTalepleriGosterme)
                    Variables.Query_ += " and KapaliMi='H' ";

                if (siparislestirilenleriGosterme)
                    Variables.Query_ += " and KalanTalepMiktar > 0 ";

                if (teslimEdilenleriGosterme)
                    Variables.Query_ += " and KalanTeslimMiktar > 0 ";

                Variables.Query_ += $" order by TalepNumarasi desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                SatınAlmaCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, variables.Fabrika, parameters, reader =>
                {
                    Cls_SatinAlma model = new();
                    model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                    model.MusteriSiparisNumarasi = reader["MusteriSiparisNumarasi"] is DBNull ? "" : reader["MusteriSiparisNumarasi"].ToString();
                    model.MusteriSiparisSira = reader["MusteriSiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["MusteriSiparisSira"]);
                    model.ReferansIsemrino = reader["ReferansIsemrino"] is DBNull ? "" : reader["ReferansIsemrino"].ToString();
                    model.TalepNumarasi = reader["TalepNumarasi"] is DBNull ? "" : reader["TalepNumarasi"].ToString();
                    model.TalepSira = reader["TalepSira"] is DBNull ? 0 : Convert.ToInt32(reader["TalepSira"]);
                    model.TalepMiktar = reader["TalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["TalepMiktar"]);
                    model.KalanTalepMiktar = reader["KalanTalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["KalanTalepMiktar"]);
                    model.KalanTeslimMiktar = reader["KalanTeslimMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["KalanTeslimMiktar"]);
                    model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    model.CariKodu = reader["CariKodu"] is DBNull ? "" : reader["CariKodu"].ToString();
                    model.CariAdi = reader["CariAdi"] is DBNull ? "" : EntryControls.FixTurkishCharacters(reader["CariAdi"].ToString());
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    model.StokBirim = reader["StokBirim"] is DBNull ? "" : reader["StokBirim"].ToString();
                    model.DepoKodu = reader["DepoKodu"] is DBNull ? 0 : Convert.ToInt32(reader["DepoKodu"]);
                    model.TedarikSiparisMiktar = reader["SiparisMiktar"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["SiparisMiktar"]);
                    model.SiparislestirilmisTalepMiktar = reader["GirisMiktar"] is DBNull ? 0.00000f : Convert.ToSingle(reader["GirisMiktar"]);
                    model.KalanSiparisMiktar = reader["KalanSiparisMiktar"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["KalanSiparisMiktar"]);
                    model.StokFiyat = reader["birimFiyat"] is DBNull ? 0.00000f : Convert.ToSingle(reader["birimFiyat"]);
                    model.DovizTipi = reader["DovizTipi"] is DBNull ? "" : reader["DovizTipi"].ToString();
                    model.KapaliMi = reader["KapaliMi"] is DBNull ? "" : reader["KapaliMi"].ToString();

                    return model;
                });
                if (SatınAlmaCollection == null)
                    return null;

                return SatınAlmaCollection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_SatinAlma> PopulateSiparisReportList(Dictionary<string, string> constraints, int pageNumber, bool kapaliSiparisleriGosterme, bool teslimEdilenleriGosterme)
        {
            try
            {
                Variables.Query_ = "select * from vbvTedarikciSiparisRapor where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and SiparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
                    variables.Counter++;
                }
                if (kapaliSiparisleriGosterme)
                    Variables.Query_ += " and KapaliMi<>'K' ";
                if (teslimEdilenleriGosterme)
                    Variables.Query_ += " and KalanSiparisMiktar>0 ";

                Variables.Query_ += $" order by TalepNumarasi desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                SatınAlmaCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, variables.Fabrika, parameters, reader =>
                {
                    Cls_SatinAlma model = new();
                    model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                    model.MusteriSiparisNumarasi = reader["MusteriSiparisNumarasi"] is DBNull ? "" : reader["MusteriSiparisNumarasi"].ToString();
                    model.MusteriSiparisSira = reader["MusteriSiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["MusteriSiparisSira"]);
                    model.ReferansIsemrino = reader["ReferansIsemrino"] is DBNull ? "" : reader["MusteriSiparisNumarasi"].ToString();
                    model.TalepNumarasi = reader["TalepNumarasi"] is DBNull ? "" : reader["TalepNumarasi"].ToString();
                    model.TalepSira = reader["TalepSira"] is DBNull ? 0 : Convert.ToInt32(reader["TalepSira"]);
                    model.TalepMiktar = reader["TalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["TalepMiktar"]);
                    model.KalanTalepMiktar = reader["KalanTalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["KalanTalepMiktar"]);
                    model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    model.CariKodu = reader["CariKodu"] is DBNull ? "" : reader["CariKodu"].ToString();
                    model.CariAdi = reader["CariAdi"] is DBNull ? "" : EntryControls.FixTurkishCharacters(reader["CariAdi"].ToString());
                    model.IrsaliyeNumarasi = reader["IrsaliyeNumarasi"] is DBNull ? "" : reader["IrsaliyeNumarasi"].ToString();
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    model.StokBirim = reader["StokBirim"] is DBNull ? "" : reader["StokBirim"].ToString();
                    model.DepoKodu = reader["DepoKodu"] is DBNull ? 0 : Convert.ToInt32(reader["DepoKodu"]);
                    model.TedarikSiparisMiktar = reader["SiparisMiktar"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["SiparisMiktar"]);
                    model.SiparislestirilmisTalepMiktar = reader["GirisMiktar"] is DBNull ? 0.00000f : Convert.ToSingle(reader["GirisMiktar"]);
                    model.KalanSiparisMiktar = reader["KalanSiparisMiktar"] is DBNull ? decimal.Zero : Convert.ToDecimal(reader["KalanSiparisMiktar"]);
                    model.StokFiyat = reader["birimFiyat"] is DBNull ? 0.00000f : Convert.ToSingle(reader["birimFiyat"]);
                    model.DovizTipi = reader["DovizTipi"] is DBNull ? "" : reader["DovizTipi"].ToString();
                    model.TeslimTarih = reader["TeslimTarih"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["TeslimTarih"]);
                    model.OdemeGunu = reader["Vade"] is DBNull ? 0 : Convert.ToInt32(reader["Vade"]);
                    model.VadeTarihi = reader["VadeTarihi"] is DBNull ? DateTime.Now : Convert.ToDateTime(reader["VadeTarihi"]);
                    model.KapaliMi = reader["KapaliMi"] is DBNull ? "" : reader["KapaliMi"].ToString();

                    return model;
                });
                if (SatınAlmaCollection == null)
                    return null;

                return SatınAlmaCollection;
            }
            catch
            {
                return null;
            }
        }
        public int CountTalepReportList(Dictionary<string, string> constraints, int pageNumber,bool kapaliTalepleriGosterme, bool siparislesenleriGosterme, bool teslimEdilenleriGosterme)
        {
            try
            {
                Variables.Query_ = "select count(*) from vbvTedarikciTalepRapor where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and SiparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }

                if (kapaliTalepleriGosterme)
                    Variables.Query_ += " and KapaliMi='H' ";

                if (siparislesenleriGosterme)
                    Variables.Query_ += " and KalanTalepMiktar > 0 ";

                if (teslimEdilenleriGosterme)
                    Variables.Query_ += " and KalanTeslimMiktar > 0 ";

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
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
        public int CountSiparisReportList(Dictionary<string, string> constraints, int pageNumber, bool kapaliSiparisleriGosterme, bool teslimEdilenleriGosterme)
        {
            try
            {
                Variables.Query_ = "select count(*) from vbvTedarikciSiparisRapor where 1=1 ";

                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and SiparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and talepNumarasi like '%' + @talepNumarasi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    Variables.Query_ = Variables.Query_ + "and kod1 like '%' + @kod1 + '%' ";
                    variables.Counter++;
                }
                if (kapaliSiparisleriGosterme)
                    Variables.Query_ += " and KapaliMi<>'K' ";
                if (teslimEdilenleriGosterme)
                    Variables.Query_ += " and KalanSiparisMiktar>0 ";
                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["talepNumarasi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["talepNumarasi"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = constraints["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(constraints["Kod1"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@kod1", SqlDbType.NVarChar, 8);
                    parameters[variables.Counter].Value = constraints["Kod1"];
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

        public bool InsertTedarikciSiparisGenel(ObservableCollection<Cls_SatinAlma> siparisCollection)
        {
            try
            {
                SiparisNumarasi = siparisCollection.Select(s => s.SiparisNumarasi).FirstOrDefault();
                CariKodu = siparisCollection.Select(c => c.CariKodu).FirstOrDefault(); 
                SiparisToplamTutar = siparisCollection.Select(s => s.SiparisToplamTutar).FirstOrDefault();
                SiparisToplamKDV = siparisCollection.Select(s => s.SiparisToplamKDV).FirstOrDefault();
                Vade = siparisCollection.Select(s => s.Vade).FirstOrDefault();
                FaturaKalemAdedi = siparisCollection.Select(s => s.FaturaKalemAdedi).FirstOrDefault();
                DovizTipiInt = siparisCollection.Select(s => s.DovizTipiInt).FirstOrDefault();

                SqlParameter[] parameters = new SqlParameter[9];

                parameters[0] = new SqlParameter("@SiparisNumarasi", SqlDbType.NVarChar,15);
                parameters[0].Value = SiparisNumarasi;

                parameters[1] = new SqlParameter("@CariKodu", SqlDbType.NVarChar,15);
                parameters[1].Value = CariKodu;

                parameters[2] = new SqlParameter("@SiparisToplamTutar", SqlDbType.Float);
                parameters[2].Value = SiparisToplamTutar;

                parameters[3] = new SqlParameter("@SiparisToplamKDV", SqlDbType.Float);
                parameters[3].Value = SiparisToplamKDV;

                parameters[4] = new SqlParameter("@Vade", SqlDbType.Int);
                parameters[4].Value = Vade;

                parameters[5] = new SqlParameter("@FaturaKalemAdedi", SqlDbType.Int);
                parameters[5].Value = FaturaKalemAdedi;

                parameters[6] = new SqlParameter("@DovizTipiInt", SqlDbType.Int);
                parameters[6].Value = DovizTipiInt;

                parameters[7] = new SqlParameter("@Aciklama", SqlDbType.NVarChar,35);
                parameters[7].Value = SiparisAciklama;

                parameters[8] = new SqlParameter("@UserName", SqlDbType.NVarChar,12);
                parameters[8].Value = login.GetUserName();

                variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertTedarikciSiparisGenel",Variables.Yil_,parameters);

                return variables.Result;

            }
            catch 
            {
                return false;
            }
        }
        public bool InsertTedarikciSiparisSatir(ObservableCollection<Cls_SatinAlma> siparisCollection)
        {
            try
            {

                foreach (Cls_SatinAlma item in siparisCollection)
                {
                    item.PlanAdi = string.IsNullOrEmpty(item.PlanAdi) ? string.Empty : item.PlanAdi;
                    item.PlanNo = string.IsNullOrEmpty(item.PlanNo) ? string.Empty : item.PlanNo;
                    item.MusteriSiparisNumarasi = string.IsNullOrEmpty(item.MusteriSiparisNumarasi) ? string.Empty : item.MusteriSiparisNumarasi;

                    SqlParameter[] parameters = new SqlParameter[18];

                    parameters[0] = new SqlParameter("@StokKodu", SqlDbType.NVarChar, 35);
                    parameters[0].Value = item.StokKodu;

                    parameters[1] = new SqlParameter("@SiparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[1].Value = item.SiparisNumarasi;

                    parameters[2] = new SqlParameter("@SiparisSira", SqlDbType.Int);
                    parameters[2].Value = item.SiparisSira;

                    parameters[3] = new SqlParameter("@SiparisMiktar", SqlDbType.Float);
                    parameters[3].Value = item.KalanTalepMiktar;

                    parameters[4] = new SqlParameter("@StokKDV", SqlDbType.Float);
                    parameters[4].Value = item.StokKDV;

                    parameters[5] = new SqlParameter("@DepoKodu", SqlDbType.Int);
                    parameters[5].Value = item.DepoKodu;

                    parameters[6] = new SqlParameter("@CariKodu", SqlDbType.NVarChar, 15);
                    parameters[6].Value = item.CariKodu;

                    parameters[7] = new SqlParameter("@DovizTipiInt", SqlDbType.Int);
                    parameters[7].Value = item.DovizTipiInt;

                    parameters[8] = new SqlParameter("@Vade", SqlDbType.Int);
                    parameters[8].Value = item.Vade;

                    parameters[9] = new SqlParameter("@TalepSira", SqlDbType.Int);
                    parameters[9].Value = item.TalepSira;

                    parameters[10] = new SqlParameter("@TalepNumarasi", SqlDbType.NVarChar, 15);
                    parameters[10].Value = item.TalepNumarasi;

                    parameters[11] = new SqlParameter("@TeslimTarihi", SqlDbType.DateTime);
                    parameters[11].Value = item.TeslimTarih;

                    parameters[12] = new SqlParameter("@StokFiyat", SqlDbType.Float);
                    parameters[12].Value = item.StokFiyat;

                    parameters[13] = new SqlParameter("@PlanAdi", SqlDbType.NVarChar,400);
                    parameters[13].Value = item.PlanAdi;

                    parameters[14] = new SqlParameter("@PlanNo", SqlDbType.NVarChar, 15);
                    parameters[14].Value = item.PlanNo;

                    parameters[15] = new SqlParameter("@MusteriSiparisNumarasi", SqlDbType.NVarChar,15);
                    parameters[15].Value = item.MusteriSiparisNumarasi;

                    parameters[16] = new SqlParameter("@MusteriSiparisSira", SqlDbType.Int);
                    parameters[16].Value = item.MusteriSiparisSira;

                    parameters[17] = new SqlParameter("@ReferansIsemrino", SqlDbType.NVarChar,15);
                    parameters[17].Value = item.ReferansIsemrino;

                    variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertTedarikciSiparisSatir", Variables.Yil_, parameters);

                    if (!variables.Result)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int UpdateTalep(ObservableCollection<Cls_SatinAlma> planabaglaColl, ObservableCollection<Cls_Planlama> planlar, bool siradanPlanBagla, bool planSecerekBagla)
        {
            try
            {
                string yeniFisno = string.Empty;
                if (planabaglaColl == null) return 2;
                if (planabaglaColl.Count == 0) return 3;
                Variables.UserName = login.GetUserName();
                List<string> fisnoList = planabaglaColl.OrderBy(t=>t.TalepNumarasi).Select(t => t.TalepNumarasi).Distinct().ToList();
                foreach (string fisno in fisnoList)
                {
                    ObservableCollection<Cls_SatinAlma> guncellenecekTalepCollection = new ObservableCollection<Cls_SatinAlma>(planabaglaColl.OrderBy(s => s.TalepSira).Where(t => t.TalepNumarasi == fisno));

                    if (string.IsNullOrEmpty(fisno) || fisno == "STRING HATA")
                        return 6;

                    if (siradanPlanBagla)
                    {
                        
                        string fabrika = Variables.Departman_.ToUpper();
                        if (fabrika == "DOSEMELI PLANLAMA")
                            fabrika = "DOSEMELI";
                        else if (fabrika == "MODULER PLANLAMA")
                            fabrika = "MODULER";
                        else
                            fabrika = "DOSEMELI";

                        Variables.Query_ = "vbpUpdateTalepSiradanPlanaBagla";
                        foreach (Cls_SatinAlma item in guncellenecekTalepCollection)
                        {

                            SqlParameter[] parameters = new SqlParameter[6];
                            parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                            parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                            parameters[2] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                            parameters[3] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                            parameters[4] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                            parameters[5] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                            parameters[0].Value = fisno;
                            parameters[1].Value = item.TalepSira;
                            parameters[2].Value = item.StokKodu;
                            parameters[3].Value = Math.Round(item.KalanTalepMiktar,4);
                            parameters[4].Value = fabrika;
                            parameters[5].Value = Variables.UserName;
                            Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                            if (!Variables.Result_)
                                return 5;

                            yeniFisno = GetLastTalepNumarasi();
                            if (string.IsNullOrEmpty(yeniFisno) ||
                                yeniFisno == "STRING HATA" ||
                                yeniFisno.Substring(0, 3) != "TLP")
                                return 7;
                        }

                        Variables.Query_ = "vbpUpdateTalepHesaplat";

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameter[0].Value = yeniFisno;
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                        if (!Variables.Result_)
                            return 7;
                        
                    }
                    else if (planSecerekBagla)
                    {
                        if (Variables.Departman_.ToUpper().Contains("MODULER"))
                            Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTableModUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTableModUpdate; " +
                                                " create table tempTalepAcPlanSecerekHamTableModUpdate (hamKodu nvarchar(35),toplamTalepMiktar float,talepNumarasi nvarchar(15),talepSira int,asilTalepMiktarFazlaMi bit) " +
                                                " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTableModUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTableModUpdate; " +
                                                " create table tempTalepAcPlanSecerekPlanTableModUpdate (planAdi nvarchar(100)) ";


                        else
                            Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTableUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTableUpdate; " +
                                                " create table tempTalepAcPlanSecerekHamTableUpdate (hamKodu nvarchar(35),toplamTalepMiktar float,talepNumarasi nvarchar(15),talepSira int,asilTalepMiktarFazlaMi bit) " +
                                                " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTableUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTableUpdate; " +
                                                " create table tempTalepAcPlanSecerekPlanTableUpdate (planAdi nvarchar(100)) ";

                        Variables.Result_ = data.ExecuteCommand(Variables.Query_, Variables.Yil_);
                        if (!Variables.Result_)
                        {
                            return 5;
                        }
                        bool asilTalepMiktariFazlaMi = false;
                        foreach (Cls_SatinAlma item in guncellenecekTalepCollection)
                        {
                            if (item.KalanTalepMiktar >= (item.TalepMiktar - item.SiparisMiktar))
                                asilTalepMiktariFazlaMi = false;
                            else
                                asilTalepMiktariFazlaMi = true;
                            SqlParameter[] parameterss = new SqlParameter[5];
                            parameterss[0] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                            parameterss[1] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                            parameterss[0].Value = item.StokKodu;
                            parameterss[1].Value = Math.Round(item.KalanTalepMiktar, 4);
                            parameterss[2] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                            parameterss[3] = new SqlParameter("@talepSira", SqlDbType.Int);
                            parameterss[4] = new SqlParameter("@asilTalepMiktariFazlaMi", SqlDbType.Bit);
                            parameterss[2].Value = item.TalepNumarasi;
                            parameterss[3].Value = item.TalepSira;
                            parameterss[4].Value = asilTalepMiktariFazlaMi;
                            if (Variables.Departman_.ToUpper().Contains("MODULER"))
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTableModUpdate Values (@hamKodu,@talepMiktar,@talepNumarasi,@talepSira,@asilTalepMiktariFazlaMi) ";
                            else
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTableUpdate Values (@hamKodu,@talepMiktar,@talepNumarasi,@talepSira,@asilTalepMiktariFazlaMi) ";

                            Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterss);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                        foreach (Cls_Planlama item in planlar)
                        {
                            SqlParameter[] parameterUpdate = new SqlParameter[1];
                            parameterUpdate[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                            parameterUpdate[0].Value = item.PlanAdi;

                            if (Variables.Departman_.ToUpper().Contains("MODULER"))
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTableModUpdate Values (@planAdi) ";
                            else
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTableUpdate Values (@planAdi) ";

                            Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterUpdate);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                        string fabrika = Variables.Departman_.ToUpper();
                        int depoKodu = 0;
                        if (fabrika == "DOSEMELI PLANLAMA")
                        {
                            fabrika = "DOSEMELI";
                            depoKodu = 10;
                        }
                        else if (fabrika == "MODULER PLANLAMA")
                        {
                            fabrika = "MODULER";
                            depoKodu = 30;
                        }
                        else
                        {
                            fabrika = "DOSEMELI";
                            depoKodu = 10;
                        }
                        SqlParameter[] parameters = new SqlParameter[2];
                        parameters[0] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                        parameters[1] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                        parameters[0].Value = fabrika;
                        parameters[1].Value = Variables.UserName;

                        Variables.Query_ = "vbpUpdateTalepPlanSecerek";
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                        if (!Variables.Result_)
                        {
                            return 5;
                        }
                        yeniFisno = GetLastTalepNumarasi();
                        if (string.IsNullOrEmpty(yeniFisno) ||
                            yeniFisno == "STRING HATA" ||
                            yeniFisno.Substring(0,3) != "TLP")
                            return 7;

                        Variables.Query_ = "vbpUpdateTalepHesaplat";

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameter[0].Value = yeniFisno;
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                        if (!Variables.Result_)
                        {
                            return 7;
                        }
                    }
                    else
                    {
                        Variables.Query_ = "vbpUpdateTeklifTra";

                        foreach (Cls_SatinAlma item in guncellenecekTalepCollection)
                        {
                            SqlParameter[] parameters = new SqlParameter[5];
                            parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                            parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                            parameters[2] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[3] = new SqlParameter("@miktar", SqlDbType.Float);
                            parameters[4] = new SqlParameter("@depoKodu", SqlDbType.Int);
                            parameters[0].Value = fisno;
                            parameters[1].Value = item.TalepSira;
                            parameters[2].Value = item.StokKodu;
                            parameters[3].Value = Math.Round(item.TalepMiktar, 4);
                            parameters[4].Value = item.DepoKodu;
                            Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                    }
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public int UpdateSiparis(ObservableCollection<Cls_SatinAlma> planabaglaColl, ObservableCollection<Cls_Planlama> planlar, bool siradanPlanBagla, bool planSecerekBagla)
        {
            try
            {
                string yeniFisno = string.Empty;
                if (planabaglaColl == null) return 2;
                if (planabaglaColl.Count == 0) return 3;
                Variables.UserName = login.GetUserName();
                List<string> fisnoList = planabaglaColl.OrderBy(t=>t.SiparisNumarasi).Select(t => t.SiparisNumarasi).Distinct().ToList();
                foreach (string fisno in fisnoList)
                {
                    ObservableCollection<Cls_SatinAlma> guncellenecekSiparisCollection = new ObservableCollection<Cls_SatinAlma>(planabaglaColl.OrderBy(s => s.SiparisSira).Where(t => t.SiparisNumarasi == fisno));

                    if (string.IsNullOrEmpty(fisno) || fisno == "STRING HATA")
                        return 6;
                    // düzenlenmedi
                    if (siradanPlanBagla)
                    {
                        
                        string fabrika = Variables.Departman_.ToUpper();
                        if (fabrika == "DOSEMELI PLANLAMA")
                            fabrika = "DOSEMELI";
                        else if (fabrika == "MODULER PLANLAMA")
                            fabrika = "MODULER";
                        else
                            fabrika = "DOSEMELI";

                        Variables.Query_ = "vbpUpdateTalepSiradanPlanaBagla";
                        foreach (Cls_SatinAlma item in guncellenecekSiparisCollection)
                        {

                            SqlParameter[] parameters = new SqlParameter[6];
                            parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                            parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                            parameters[2] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                            parameters[3] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                            parameters[4] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                            parameters[5] = new SqlParameter("@user", SqlDbType.NVarChar,12);
                            parameters[0].Value = fisno;
                            parameters[1].Value = item.TalepSira;
                            parameters[2].Value = item.StokKodu;
                            parameters[3].Value = Math.Round(item.KalanTalepMiktar,4);
                            parameters[4].Value = fabrika;
                            parameters[5].Value = Variables.UserName;
                            Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                            if (!Variables.Result_)
                                return 5;

                            yeniFisno = GetLastTalepNumarasi();
                            if (string.IsNullOrEmpty(yeniFisno) ||
                                yeniFisno == "STRING HATA" ||
                                yeniFisno.Substring(0, 3) != "TLP")
                                return 7;
                        }

                        Variables.Query_ = "vbpUpdateTalepHesaplat";

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameter[0].Value = yeniFisno;
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                        if (!Variables.Result_)
                            return 7;

                    }
                    // düzenlenmedi
                    else if (planSecerekBagla)
                    {
                        if (Variables.Departman_.ToUpper().Contains("MODULER"))
                            Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTableModUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTableModUpdate; " +
                                                " create table tempTalepAcPlanSecerekHamTableModUpdate (hamKodu nvarchar(35),toplamTalepMiktar float,talepNumarasi nvarchar(15),talepSira int,asilTalepMiktarFazlaMi bit) " +
                                                " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTableModUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTableModUpdate; " +
                                                " create table tempTalepAcPlanSecerekPlanTableModUpdate (planAdi nvarchar(100)) ";


                        else
                            Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTableUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTableUpdate; " +
                                                " create table tempTalepAcPlanSecerekHamTableUpdate (hamKodu nvarchar(35),toplamTalepMiktar float,talepNumarasi nvarchar(15),talepSira int,asilTalepMiktarFazlaMi bit) " +
                                                " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTableUpdate', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTableUpdate; " +
                                                " create table tempTalepAcPlanSecerekPlanTableUpdate (planAdi nvarchar(100)) ";

                        Variables.Result_ = data.ExecuteCommand(Variables.Query_, Variables.Yil_);
                        if (!Variables.Result_)
                        {
                            return 5;
                        }
                        bool asilTalepMiktariFazlaMi = false;
                        foreach (Cls_SatinAlma item in guncellenecekSiparisCollection)
                        {
                            if (item.KalanTalepMiktar >= (item.TalepMiktar - item.SiparisMiktar))
                                asilTalepMiktariFazlaMi = false;
                            else
                                asilTalepMiktariFazlaMi = true;
                            SqlParameter[] parameterss = new SqlParameter[5];
                            parameterss[0] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                            parameterss[1] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                            parameterss[0].Value = item.StokKodu;
                            parameterss[1].Value = Math.Round(item.KalanTalepMiktar, 4);
                            parameterss[2] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                            parameterss[3] = new SqlParameter("@talepSira", SqlDbType.Int);
                            parameterss[4] = new SqlParameter("@asilTalepMiktariFazlaMi", SqlDbType.Bit);
                            parameterss[2].Value = item.TalepNumarasi;
                            parameterss[3].Value = item.TalepSira;
                            parameterss[4].Value = asilTalepMiktariFazlaMi;
                            if (Variables.Departman_.ToUpper().Contains("MODULER"))
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTableModUpdate Values (@hamKodu,@talepMiktar,@talepNumarasi,@talepSira,@asilTalepMiktariFazlaMi) ";
                            else
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTableUpdate Values (@hamKodu,@talepMiktar,@talepNumarasi,@talepSira,@asilTalepMiktariFazlaMi) ";

                            Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterss);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                        foreach (Cls_Planlama item in planlar)
                        {
                            SqlParameter[] parameterUpdate = new SqlParameter[1];
                            parameterUpdate[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                            parameterUpdate[0].Value = item.PlanAdi;

                            if (Variables.Departman_.ToUpper().Contains("MODULER"))
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTableModUpdate Values (@planAdi) ";
                            else
                                Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTableUpdate Values (@planAdi) ";

                            Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterUpdate);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                        string fabrika = Variables.Departman_.ToUpper();
                        int depoKodu = 0;
                        if (fabrika == "DOSEMELI PLANLAMA")
                        {
                            fabrika = "DOSEMELI";
                            depoKodu = 10;
                        }
                        else if (fabrika == "MODULER PLANLAMA")
                        {
                            fabrika = "MODULER";
                            depoKodu = 30;
                        }
                        else
                        {
                            fabrika = "DOSEMELI";
                            depoKodu = 10;
                        }
                        SqlParameter[] parameters = new SqlParameter[2];
                        parameters[0] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                        parameters[1] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                        parameters[0].Value = fabrika;
                        parameters[1].Value = Variables.UserName;

                        Variables.Query_ = "vbpUpdateTalepPlanSecerek";
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                        if (!Variables.Result_)
                        {
                            return 5;
                        }
                        yeniFisno = GetLastTalepNumarasi();
                        if (string.IsNullOrEmpty(yeniFisno) ||
                            yeniFisno == "STRING HATA" ||
                            yeniFisno.Substring(0,3) != "TLP")
                            return 7;

                        Variables.Query_ = "vbpUpdateTalepHesaplat";

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameter[0].Value = yeniFisno;
                        Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                        if (!Variables.Result_)
                        {
                            return 7;
                        }
                    }
                    // plandan bağımsız sipariş güncelle
                    else
                    {
                        Variables.Query_ = "vbpUpdateTedarikciSiparisTra";

                        foreach (Cls_SatinAlma item in guncellenecekSiparisCollection)
                        {
                            SqlParameter[] parameters = new SqlParameter[6];
                            parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                            parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                            parameters[2] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[3] = new SqlParameter("@miktar", SqlDbType.Float);
                            parameters[4] = new SqlParameter("@depoKodu", SqlDbType.Int);
                            parameters[5] = new SqlParameter("@cariKodu", SqlDbType.NVarChar,35);
                            parameters[0].Value = fisno;
                            parameters[1].Value = item.TalepSira;
                            parameters[2].Value = item.StokKodu;
                            parameters[3].Value = Math.Round(item.TedarikSiparisMiktar, 4);
                            parameters[4].Value = item.DepoKodu;
                            parameters[5].Value = item.CariKodu;
                            Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                            if (!Variables.Result_)
                            {
                                return 5;
                            }
                        }
                    }
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public bool UpdateTaleptenPlanKaldir(string talepNumarasi,int talepSira)
        {
            try
            {
                Variables.UserName = login.GetUserName();
                if (string.IsNullOrEmpty(talepNumarasi) ||
                    talepSira == 0)
                    return false;

                Variables.Query_ = "vbpTalepPlanKaldir";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                parameters[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parameters[0].Value = talepNumarasi;
                parameters[1].Value = talepSira;
                parameters[2].Value = Variables.UserName;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateSiparistenPlanKaldir(string siparisNumarasi,int siparisSira)
        {
            try
            {
                Variables.UserName = login.GetUserName();
                if (string.IsNullOrEmpty(siparisNumarasi)) return false;
                if (siparisSira == 0) return false;
                

                Variables.Query_ = "vbpSiparisPlanKaldir";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parameters[0].Value = siparisNumarasi;
                parameters[1].Value = siparisSira;
                parameters[2].Value = Variables.UserName;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public bool DeleteTalepSatir(string talepNumarasi, int talepSira)
        {
            try
            {
                Variables.UserName = login.GetUserName();
                if (string.IsNullOrEmpty(talepNumarasi) ||
                    talepSira == 0)
                    return false;

                Variables.Query_ = "vbpDeleteTalepSatir";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@talepNumarasi", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                parameters[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parameters[0].Value = talepNumarasi;
                parameters[1].Value = talepSira;
                parameters[2].Value = Variables.UserName;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public bool DeleteSiparisSatir(string siparisNumarasi, int siparisSira)
        {
            try
            {
                Variables.UserName = login.GetUserName();
                if (string.IsNullOrEmpty(siparisNumarasi)) return false;
                if (siparisSira == 0) return false;

                Variables.Query_ = "vbpDeleteTedarikciSiparisSatir";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parameters[0].Value = siparisNumarasi;
                parameters[1].Value = siparisSira;
                parameters[2].Value = Variables.UserName;

                Variables.Result_ = data.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public void KayitGeriAlTedarikciSiparis(string siparisNumarasi)
        { 
            try 
	        {
                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new SqlParameter("@SiparisNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = siparisNumarasi;

                data.ExecuteStoredProcedureWithParameters("vbpDeleteTalep", Variables.Yil_, parameter);

            }
	        catch (global::System.Exception)
	        {

	        }
        }

        public int GetDepoKodu(string siparisNumarasi, int siparisSira)
        {
            try
            {
                if (string.IsNullOrEmpty(siparisNumarasi))
                    return -1;
                if (siparisSira == 0)
                    return -1;

                Variables.Query_ = "Select isnull(depo_kodu,0) from tblsipatra (nolock) where fisno=@siparisNumarasi and stra_sipkont = @siparisSira";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[0].Value = siparisNumarasi;
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[1].Value = siparisSira;
                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int GetSiparisMiktar(string siparisNumarasi, int siparisSira)
        {
            try
            {
                if (string.IsNullOrEmpty(siparisNumarasi))
                    return -1;
                if (siparisSira == 0)
                    return -1;

                Variables.Query_ = "Select isnull(sthar_gcmik,0) from tblsipatra (nolock) where fisno=@siparisNumarasi and stra_sipkont = @siparisSira";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[0].Value = siparisNumarasi;
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[1].Value = siparisSira;
                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public string GetStokKodu(string siparisNumarasi, int siparisSira)
        {
            try
            {
                if (string.IsNullOrEmpty(siparisNumarasi))
                    return "STRING HATA";
                if (siparisSira == 0)
                    return "STRING HATA";

                Variables.Query_ = "Select isnull(stok_kodu,'') from tblsipatra where fisno=@siparisNumarasi and stra_sipkont = @siparisSira";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameters[0].Value = siparisNumarasi;
                parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[1].Value = siparisSira;
                Variables.ResultString_ = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultString_;
            }
            catch (Exception)
            {
                return "STRING HATA";
            }
        }

    public string GetTedarikciSiparisNumarasi()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter("@prefix",SqlDbType.NVarChar,4);
                parameters[0].Value = "SS";
                parameters[1] = new SqlParameter("@tableName",SqlDbType.NVarChar,128);
                parameters[1].Value = "TBLSIPATRA";
                parameters[2] = new SqlParameter("@columnName",SqlDbType.NVarChar,128);
                parameters[2].Value = "fisno";


                variables.ResultString = data.Get_One_String_Result_Stored_Proc_With_Parameters("vbpGetFisno",Variables.Yil_,parameters);

                return variables.ResultString;

            }
            catch 
            {
                return "STRING HATA";
            }
        }
        public string GetLastTalepNumarasi()
        {
            try
            {
                Variables.Query_ = "Select top 1 fisno from tbltekliftra order by fisno desc";

                variables.ResultString = data.Get_One_String_Result_Command(Variables.Query_, Variables.Yil_);

                return variables.ResultString;

            }
            catch
            {
                return "STRING HATA";
            }
        }
        public int GetLastSiraNo(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return -1;
                Variables.Query_ = "Select cast(max(isnull(stra_sipkont,0)) as int) from tbltekliftra (nolock) where fisno=@fisno";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;
                Variables.ResultInt_ = data.ExecuteCommandWithParametersReturnsInt(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                return Variables.ResultInt_;

            }
            catch (Exception)
            {
                return -1;
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string getStr)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(getStr));
        }
    }
}

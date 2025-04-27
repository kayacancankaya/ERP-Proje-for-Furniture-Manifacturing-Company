using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection.PortableExecutable;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Text.RegularExpressions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Reflection;
using System.Reflection.Metadata;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using PdfSharp.Charting;

namespace Layer_Business
{
	public class Cls_Planlama : Cls_Base, INotifyPropertyChanged
	{

        public string PlanAdi { get; set; } = string.Empty;
        public string PlanNo { get; set; } = string.Empty;

        public int PlanAdiSira { get; set; } 
        public int EskiPlanAdiSira { get; set; }

        public string Renk { get; set; } = string.Empty;

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

        private string _hamKodu = "";

        public string HamKodu
        {
            get { return _hamKodu; }
            set
            {
                _hamKodu = value;
                OnPropertyChanged(nameof(HamKodu));
            }
        }
        private string _hamAdi = "";

        public string HamAdi
        {
            get { return _hamAdi; }
            set
            {
                _hamAdi = value;
                OnPropertyChanged(nameof(HamAdi));
            }
        }
        private string _urunKodu = "";
        public string UrunKodu
        {
            get { return _urunKodu; }
            set
            {
                _urunKodu = value;
                OnPropertyChanged(nameof(UrunKodu));
            }
        }
        public string Kod1 { get; set; }
        public string OlcuBirimi { get; set; }
		private string _urunAdi = "";

		public string UrunAdi
		{
			get { return _urunAdi; }
			set
			{
				_urunAdi = value;
				OnPropertyChanged(nameof(UrunAdi));
			}
		}

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

		private string _siparisTarih;

		public string SiparisTarih
		{
			get { return _siparisTarih; }
			set { _siparisTarih = value; }
		}

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
		private string _destinasyon = "";
		public string Destinasyon
		{
			get { return _destinasyon; }
			set
			{
				_destinasyon = value;
				OnPropertyChanged(nameof(Destinasyon));
			}
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
        private string _talepGenelAciklama = "";
        public string TalepGenelAciklama
        {
            get { return _talepGenelAciklama; }
            set
            {
                _talepGenelAciklama = value;
                OnPropertyChanged(nameof(TalepGenelAciklama));
            }
        } private string _talepAciklama = "";
        public string TalepAciklama
        {
            get { return _talepAciklama; }
            set
            {
                _talepAciklama = value;
                OnPropertyChanged(nameof(TalepAciklama));
            }
        }
        private string _POnumarasi = "";
		public string POnumarasi
		{
			get { return _POnumarasi; }
			set
			{
				_POnumarasi = value;
				OnPropertyChanged(nameof(POnumarasi));
			}
		}
		private string _dovizTipi = "USD";
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

        public string ReferansIsemrino { get; set; }
        public string Isemrino { get; set; }
        public string TakipNo { get; set; }
        public string MamulKodu { get; set; }
        public string MamulAdi { get; set; }

        private string _isemriAciklama = "Lütfen Açıklama Giriniz...";

		public string IsemriAciklama
		{
			get { return _isemriAciklama;  }
			set { 
				_isemriAciklama = value; 
				OnPropertyChanged(nameof(IsemriAciklama));
			}
		}
		public string UretimDurumu { get; set; } = "Y";
		public int IsemriMiktar { get; set; }
		public int UretilenMiktar { get; set; }
        public int KalanIsemriMiktar { get; set; }
        public int GonderilecekIsemriMiktar { get; set; }
        public int TeslimMiktar { get; set; }
        public int AcikSevkMiktar { get; set; }
        public decimal DepoMiktar { get; set; }
        public decimal TedarikTalepMiktar { get; set; }
        public decimal TedarikSiparisMiktar { get; set; }
		public decimal HamIhtiyacMiktar { get; set; }  //işemri toplam ihtiyacı
		//single mamul hesaplarken birim ham ihtiyaç miktar birim planda öncelikli mamuller için ham ihtiyaç miktar 
        public decimal HamIhtiyacMiktarBirim { get; set; }
        public decimal HamIhtiyacMiktarKalan { get; set; } //işemri kalan ham madde ihtiyacı
        public decimal HamKoduDatMiktar { get; set; } 
        public int TalepSira { get; set; }
        public decimal MevcutTalepAdedi { get; set; }
        public decimal MevcutSiparisAdedi { get; set; }
        public int SevkMiktar { get; set; }
        public int MinimumPaketUretimAdedi { get; set; }
        public int KalanUretimAdedi { get; set; }
        public string CariKodu { get; set; }
        public string CariAdi { get; set; }
		private bool _isChecked;
		public bool IsChecked
		{
			get { return _isChecked; }
			set {
				_isChecked = value; 
				OnPropertyChanged(nameof(IsChecked));
				}
		}
        public string UrunGrup { get; set; }
        public string Model { get; set; }
        public string SatisSekil { get; set; }
        public decimal MakSiparisMiktar { get; set; }

        public int DepoKodu { get; set; }
        public int CikisDepoKodu { get; set; }

        public bool UretimBildir_ { get; set; }
		public bool DoesUrunAgaciExists { get; set; } = false;

		public Cls_Base.DovizTipi DovizTipi { get; set; } 
        public Cls_Base.UretimDurum UretimDurum { get; set; }
        public DateTime BildirimTarih { get; set; }

        private ObservableCollection<Cls_Planlama> temp_coll_planlama = new();
        private ObservableCollection<Cls_Planlama> temp_coll_renklendir = new();

        public ObservableCollection<Cls_Planlama> PlanlamaCollection = new();
        public ObservableCollection<Cls_Planlama> RenklendirCollection = new();
        DataLayer data = new();

		public Cls_Planlama()
		{
			variables.Fabrika = login.GetFabrika();

            Variables.Fabrika_ = variables.Fabrika;

        }

        Variables variables = new();
        LoginLogic login = new();

        public ObservableCollection<Cls_Planlama> PopulatePlakaSimulasyonList(Dictionary<string, string> kisitPairs)
        {
            try
            {

                Variables.Query_ = "select * from vbvPlakaSimulasyon where 1=1 ";
                variables.Counter = 0;


                if (!string.IsNullOrEmpty(kisitPairs["urunGrup"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urunGrup like '%' + @urunGrup + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["satisSekil"]))
                {
                    Variables.Query_ = Variables.Query_ + "and satisSekil like '%' + @satisSekil + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["model"]))
                {
                    Variables.Query_ = Variables.Query_ + "and model like '%' + @model + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urunKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urunAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }

                if (kisitPairs["eksikStokGosterme"] == "true")
                {
                    Variables.Query_ = Variables.Query_ + "and uretilebilecekMiktar > 0 ";
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["urunGrup"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@urunGrup", SqlDbType.NVarChar, 100);
                    parameters[variables.Counter].Value = kisitPairs["urunGrup"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["model"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@model", SqlDbType.NVarChar, 100);
                    parameters[variables.Counter].Value = kisitPairs["model"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["satisSekil"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@satisSekil", SqlDbType.NVarChar, 100);
                    parameters[variables.Counter].Value = kisitPairs["satisSekil"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = kisitPairs["stokKodu"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = kisitPairs["stokAdi"];
                    variables.Counter++;
                }
				decimal maksSiparisMiktar = 0;
                temp_coll_planlama.Clear();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters))
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
							maksSiparisMiktar = Convert.ToDecimal(reader[5]);
                            Cls_Planlama planItem = new Cls_Planlama
                            {
                                UrunGrup = reader[0].ToString(),
                                Model = reader[1].ToString(),
                                SatisSekil = reader[2].ToString(),
                                UrunKodu = reader[3].ToString(),
                                UrunAdi = reader[4].ToString(),
								
                            };


                            if (maksSiparisMiktar >= int.MinValue && maksSiparisMiktar <= int.MaxValue)
                                planItem.MakSiparisMiktar = Math.Floor(maksSiparisMiktar);
                            else
                                planItem.MakSiparisMiktar = maksSiparisMiktar;

                            temp_coll_planlama.Add(planItem);
                        }
                    }
                }

                PlanlamaCollection = temp_coll_planlama;
                return PlanlamaCollection;

            }
            catch (Exception ex){ var stri = ex.Message.ToString(); return null; }
        }
        public ObservableCollection<Cls_Planlama> PopulatePlanIhtiyacListe(Dictionary<string, string> restrictionPairs)
        {
            try
            {

				if (login.GetDepartment().Contains("Moduler"))
					Variables.Query_ = "Select * from vbtTalepIhtiyacListeMod where 1=1 and miktar>0.0001 ";
				else
					Variables.Query_ = "Select * from vbtTalepIhtiyacListeDos where 1=1 and miktar>0.0001 ";

                Variables.Counter_ = 0;

                //if (restrictionPairs.ContainsKey("@planNo"))
                //{
                //    Variables.Query_ += " and PlanNo = @planNo ";
                //    Variables.Counter_++;
                //}

                if (restrictionPairs.ContainsKey("@planAdi"))
                {
                    Variables.Query_ += " and PlanAdi = @planAdi ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                //if (restrictionPairs.ContainsKey("@planNo"))
                //{
                //    parameters[Variables.Counter_] = new("@planNo", SqlDbType.NVarChar, 15);
                //    parameters[Variables.Counter_].Value = restrictionPairs["@planNo"];
                //    Variables.Counter_++;
                //}

				if (restrictionPairs.ContainsKey("@planAdi"))
				{
					parameters[Variables.Counter_] = new("@planAdi", SqlDbType.NVarChar, 400);
					parameters[Variables.Counter_].Value = restrictionPairs["@planAdi"];
					Variables.Counter_++;
				}

                ObservableCollection<Cls_Planlama> PlanCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Planlama model = new();
                    model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                    //model.PlanNo = reader["PlanNo"] is DBNull ? "" : reader["PlanNo"].ToString();
                    model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                    model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
                    model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    model.TedarikTalepMiktar = reader["Miktar"] is DBNull ? 0 : Convert.ToDecimal(reader["Miktar"]);
					model.TerminTarih = DateTime.Now.AddDays(21);
					model.SiparisAciklama = string.Empty;
                    return model;
                });
                return PlanCollection;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Planlama> PopulateTekIsemriAcList(Dictionary<string, string> kisitPairs,bool _isEksiBakiye)
		{
			try
			{
				if(!_isEksiBakiye)
				{ 
					Variables.Query_ = "select * from vbvTekIsemriAcListe where 1=1 ";
					variables.Counter = 0;

					if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
					{
						Variables.Query_ = Variables.Query_ + "and siparisNo like '%' + @siparisNo + '%' ";
						variables.Counter++;
					}
					if (!string.IsNullOrEmpty(kisitPairs["siparisSira"]))
					{
						Variables.Query_ = Variables.Query_ + "and siparisSira = @siparisSira ";
						variables.Counter++;
					}
					if (!string.IsNullOrEmpty(kisitPairs["urunKodu"]))
					{
						Variables.Query_ = Variables.Query_ + "and urunKodu like '%' + @urunKodu + '%' ";
						variables.Counter++;
					}

					if (!string.IsNullOrEmpty(kisitPairs["urunAdi"]))
					{
						Variables.Query_ = Variables.Query_ + "and urunAdi like '%' + @urunAdi + '%' ";
						variables.Counter++;
					}
					if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
					{
						Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
						variables.Counter++;
					}

					if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
					{
						Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
						variables.Counter++;
					}


					SqlParameter[] parameters = new SqlParameter[variables.Counter];
					variables.Counter = 0;

					if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
					{
						parameters[variables.Counter] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
						parameters[variables.Counter].Value = kisitPairs["siparisNo"];
						variables.Counter++;
					}
					if (!string.IsNullOrEmpty(kisitPairs["siparisSira"]))
					{
						parameters[variables.Counter] = new SqlParameter("@siparisSira", SqlDbType.Int);
						parameters[variables.Counter].Value = kisitPairs["siparisSira"];
						variables.Counter++;
					}

					if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
					{
						parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
						parameters[variables.Counter].Value = kisitPairs["stokKodu"];
						variables.Counter++;
					}


					if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
					{
						parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
						parameters[variables.Counter].Value = kisitPairs["stokAdi"];
						variables.Counter++;
					}
					if (!string.IsNullOrEmpty(kisitPairs["urunKodu"]))
					{
						parameters[variables.Counter] = new SqlParameter("@urunKodu", SqlDbType.NVarChar, 35);
						parameters[variables.Counter].Value = kisitPairs["urunKodu"];
						variables.Counter++;
					}


					if (!string.IsNullOrEmpty(kisitPairs["urunAdi"]))
					{
						parameters[variables.Counter] = new SqlParameter("@urunAdi", SqlDbType.NVarChar, 400);
						parameters[variables.Counter].Value = kisitPairs["urunAdi"];
						variables.Counter++;
					}


					temp_coll_planlama.Clear();
					using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika))
					{
						if (reader != null && reader.HasRows)
						{
							while (reader.Read())
							{
								Cls_Planlama planItem = new Cls_Planlama
								{
									SiparisNumarasi = reader[0].ToString(),
									SiparisSira = Convert.ToInt32(reader[1]),
									SiparisMiktar = Convert.ToInt32(reader[2]),
									PlanNo = reader[3].ToString(),
									TakipNo = reader[4].ToString(),
									ReferansIsemrino = reader[5].ToString(),
									UrunKodu = reader[6].ToString(),
									UrunAdi = reader[7].ToString(),
									StokKodu = reader[8].ToString(),
									StokAdi = reader[9].ToString(),
									IsemriMiktar = Convert.ToInt32(reader[10]),
								};
								temp_coll_planlama.Add(planItem);
							}
						}
					}

					PlanlamaCollection = temp_coll_planlama;
					return PlanlamaCollection;
                }
				else
				{

					if (string.IsNullOrEmpty(kisitPairs["depoKodu"]))
						return null;

                    SqlParameter[] parameter = new SqlParameter[1];

                        parameter[0] = new SqlParameter("@depoKodu", SqlDbType.Int);
                        parameter[0].Value = Convert.ToInt32(kisitPairs["depoKodu"]);
                    

                    temp_coll_planlama.Clear();

                    using (SqlDataReader reader = data.Select_Procedure_Data_Reader_With_Parameters("vbpTekIsemriAcListeEksiBakiye", Variables.Yil_, parameter, variables.Fabrika))
                    {
						if (reader == null)
							return null;

                        while (reader.Read())
                        {
                            Cls_Planlama plan = new Cls_Planlama()
                            {
								SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString(),
								SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]),
								SiparisMiktar = reader["SiparisMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisMiktar"]),
								PlanNo = reader["PlanNo"] is DBNull ? "" : reader["PlanNo"].ToString(),
								TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString(),
								ReferansIsemrino = reader["ReferansIsemrino"] is DBNull ? "" : reader["ReferansIsemrino"].ToString(),
								UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString(),
								UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString(),
								StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString(),
								StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString(),
								IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["IsemriMiktar"]),
							};
                            temp_coll_planlama.Add(plan);
                        }
                    }
                    return temp_coll_planlama;

			
                }
            }
			catch { return null; }
		}
        public ObservableCollection<Cls_Planlama> PopulateTopluIsemriAcList(Dictionary<string, string> kisitPairs, int pageNumber)
		{
			try
			{
				Cls_Arge arge = new();
				Variables.Query_ = "select * from vbvAcilacakIsemirleri where 1=1 ";
				variables.Counter = 0;

				if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
				{
					Variables.Query_ = Variables.Query_ + "and siparisNo like '%' + @siparisNo + '%' ";
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
				{
					Variables.Query_ = Variables.Query_ + "and urunKodu like '%' + @stokKodu + '%' ";
					variables.Counter++;
				}


				if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
				{
					Variables.Query_ = Variables.Query_ + "and urunAdi like '%' + @stokAdi + '%' ";
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
				{
					Variables.Query_ = Variables.Query_ + "and cariAdi like '%' + @cariAdi + '%' ";
					variables.Counter++;
				}

				SqlParameter[] parameters = new SqlParameter[variables.Counter];
				variables.Counter = 0;

				if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
				{
					parameters[variables.Counter] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
					parameters[variables.Counter].Value = kisitPairs["siparisNo"];
					variables.Counter++;
				}

				if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
				{
					parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
					parameters[variables.Counter].Value = kisitPairs["stokKodu"];
					variables.Counter++;
				}


				if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
				{
					parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
					parameters[variables.Counter].Value = kisitPairs["stokAdi"];
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
				{
					parameters[variables.Counter] = new SqlParameter("@cariAdi", SqlDbType.NVarChar, 400);
					parameters[variables.Counter].Value = kisitPairs["cariAdi"];
					variables.Counter++;
				}

                Variables.Query_ += $"order by siparisNo desc, siparisSira asc offset {(pageNumber - 1) * 30} rows fetch next 30 rows only ";

                temp_coll_planlama.Clear();
				using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika))
				{
					if (reader != null && reader.HasRows)
					{
						while (reader.Read())
						{
							Cls_Planlama planItem = new Cls_Planlama
							{
								SiparisNumarasi = reader[0].ToString(),
								SiparisSira = Convert.ToInt32(reader[1]),
                                CariKodu = reader[2].ToString(),
                                CariAdi = reader[3].ToString(),
                                UrunKodu = reader[4].ToString(),
								UrunAdi = reader[5].ToString(),
								SiparisMiktar = Convert.ToInt32(reader[6]),
								KalanIsemriMiktar = Convert.ToInt32(reader[7]),
								GonderilecekIsemriMiktar = Convert.ToInt32(reader[7]),
								IsChecked = false,
								DoesUrunAgaciExists = arge.IfUrunAgaciExists(reader[4].ToString()),
							};
							temp_coll_planlama.Add(planItem);
						}
					}
				}

				PlanlamaCollection = temp_coll_planlama;
				return PlanlamaCollection;

			}
			catch { return null; }
		}
        public ObservableCollection<Cls_Planlama> PopulateTamamlanmamisSiparislerList(Dictionary<string, string> kisitPairs)
		{
			try
			{

				Variables.Query_ = "select * from vbvTamamlanmamisSiparisler where 1=1 ";
				variables.Counter = 0;


				if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
				{
					Variables.Query_ = Variables.Query_ + "and siparisNo like '%' + @siparisNo + '%' ";
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
				{
					Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
					variables.Counter++;
				}


				if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
				{
					Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
				{
					Variables.Query_ = Variables.Query_ + "and cariAdi like '%' + @cariAdi + '%' ";
					variables.Counter++;
				}


                SqlParameter[] parameters = new SqlParameter[variables.Counter];
				variables.Counter = 0;

				if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
				{
					parameters[variables.Counter] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
					parameters[variables.Counter].Value = kisitPairs["siparisNo"];
					variables.Counter++;
				}

				if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
				{
					parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
					parameters[variables.Counter].Value = kisitPairs["stokKodu"];
					variables.Counter++;
				}


				if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
				{
					parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
					parameters[variables.Counter].Value = kisitPairs["stokAdi"];
					variables.Counter++;
				}
				if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
				{
					parameters[variables.Counter] = new SqlParameter("@cariAdi", SqlDbType.NVarChar, 400);
					parameters[variables.Counter].Value = kisitPairs["cariAdi"];
					variables.Counter++;
				}


				if (kisitPairs["kapaliSiparis"] == "Gosterme")
				{
					Variables.Query_ = Variables.Query_ + " and siparisDurum <> 'K'";
				}
				if (kisitPairs["acilmamisIsemri"] == "Gosterme")
				{
					Variables.Query_ = Variables.Query_ + " and referansIsemri is not null";
				}

				temp_coll_planlama.Clear();
				using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameters,variables.Fabrika))
				{
					if (reader != null && reader.HasRows)
					{
						while (reader.Read())
						{
							Cls_Planlama planItem = new Cls_Planlama
							{
								SiparisNumarasi = reader[0].ToString(),
								SiparisSira = Convert.ToInt32(reader[1]),
								UrunKodu = reader[2].ToString(),
								UrunAdi = reader[3].ToString(),
								SiparisTarih = reader[4].ToString(),
								TerminTarih = Convert.ToDateTime(reader[5]),
								SiparisMiktar = Convert.ToInt32(reader[6]),
								TeslimMiktar = Convert.ToInt32(reader[7]),
								AcikSevkMiktar = Convert.ToInt32(reader[8]),
								DepoMiktar = Convert.ToInt32(reader[9]),
								MinimumPaketUretimAdedi = Convert.ToInt32(reader[10]),
								CariAdi = reader[11].ToString(),
								CariKodu = reader[12].ToString(),
							};
							temp_coll_planlama.Add(planItem);
						}
					}
				}

				PlanlamaCollection = temp_coll_planlama;
				return PlanlamaCollection;

			}
			catch { return null; }
		}
		public ObservableCollection<Cls_Planlama> GetKayitliPlanAdlari(string simulasyonTip)
		{ 	
			try 
			{
				ObservableCollection<Cls_Planlama> kayitliPlanAdlariCollection = new();

				if(simulasyonTip == "Simülasyon")
					Variables.Query_ = "Select distinct plan_adi from sipmamul";
				if(simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "Select distinct plan_adi from sipmamuldos";
                if (simulasyonTip == "Simülasyon Sunta")
                    Variables.Query_ = "Select distinct plan_adi from vbtSimulasyonSunta";
				if (simulasyonTip == "Ahsap Plan")
					Variables.Query_ = "Select distinct planAdi from vatSimulasyon";
				if (simulasyonTip == "Hepsi")
					Variables.Query_ = "Select distinct plan_Adi from sipmamul union all select distinct plan_Adi from sipmamuldos";

                temp_coll_planlama.Clear();
				using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,variables.Fabrika))
				{
					if (reader == null)
						return null;

					while (reader.Read()) 
					{ 
						Cls_Planlama plan = new Cls_Planlama
						{
							PlanAdi = reader[0].ToString(), 
						};

						temp_coll_planlama.Add(plan);
					}
				}
				if (temp_coll_planlama.Any())
				{
					kayitliPlanAdlariCollection = temp_coll_planlama;
					return kayitliPlanAdlariCollection;
				}
				else
					return null;
				
			}
			catch 
			{
				return null;
			}
		}
		public int InsertPlanAdi(ObservableCollection<Cls_Planlama> planAdlari, string planAdi, bool isNew, string simulasyonTip)
		{
			try
			{
               
                if (isNew)
				{
					if(simulasyonTip=="Simülasyon")
						Variables.Query_ = "Select distinct plan_adi from sipmamul";
					if(simulasyonTip=="Simülasyon Dosemeli")
						Variables.Query_ = "Select distinct plan_adi from sipmamuldos";
                    if (simulasyonTip == "Simülasyon Sunta")
                        Variables.Query_ = "Select distinct plan_adi from vbtSimulasyonSunta";
                    if (simulasyonTip == "Ahsap Plan")
                        Variables.Query_ = "Select distinct planAdi from vatSimulasyon";

                    temp_coll_planlama.Clear();
					using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
					{ 
						while (reader.Read())
						{
							if (!reader.HasRows)
								return 2;

							Cls_Planlama plan = new Cls_Planlama
							{
								PlanAdi = reader[0].ToString()
							};
							temp_coll_planlama.Add(plan) ;
						}
						PlanlamaCollection = temp_coll_planlama;
					}
                    //yeni açılacak plan adı mevcut mu kontrol
                    var result = PlanlamaCollection.Any(x => x.PlanAdi == planAdi);
                    if (result)
                       return 3;
					// yeni açılacak plan adı daha önce aynı addan kayıt var mı kontrol
                    if ((simulasyonTip == "Simülasyon" ||
                   simulasyonTip == "Simülasyon Dosemeli"))
                    {
                        Variables.Query_ = "vbpCheckPlanAdiExists";
                        foreach (Cls_Planlama item in planAdlari)
                        {
                            SqlParameter[] parameterCheck = new SqlParameter[3];
                            parameterCheck[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 400);
                            parameterCheck[0].Value = planAdi;
                            parameterCheck[1] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                            parameterCheck[1].Value = item.SiparisNumarasi;
                            parameterCheck[2] = new SqlParameter("@siparisSira", SqlDbType.Int);
                            parameterCheck[2].Value = item.SiparisSira;

                            Variables.ResultString_ = data.Get_One_String_Result_Stored_Proc_With_Parameters(Variables.Query_, Variables.Yil_, parameterCheck);
                            if (Variables.ResultString_ == "Bildirim Var")
                                return 6;
                            if (Variables.ResultString_ == "Siparis Var")
                                return 7;
                            if (Variables.ResultString_ == "Siparise Ait Isemrinde Baska Plan Adi Var")
                                return 8;
                            if (Variables.ResultString_ == "Siparise Ait Talepte Baska Plan Adi Var")
                                return 9;
                            if (Variables.ResultString_ == "STRING HATA" ||
                                Variables.ResultString_ == string.Empty)
                                return 10;


                        }
                    }

					//sira no al
                    if (simulasyonTip == "Simülasyon")
						Variables.Query_ = "select top 1 cast(sira_no as nvarchar(4)) from sipmamul order by sira_no desc";
                    if(simulasyonTip == "Simülasyon Dosemeli")
						Variables.Query_ = "select top 1 cast(sira_no as nvarchar(4)) from sipmamuldos order by sira_no desc";
                    if (simulasyonTip == "Simülasyon Sunta")
                        Variables.Query_ = "select top 1 cast(sira_no as nvarchar(4)) from vbtSimulasyonSunta order by sira_no desc";
                    if (simulasyonTip == "Ahsap Plan")
                        Variables.Query_ = "select top 1 cast(PlanAdiSira as nvarchar(4)) from vatSimulasyon order by planAdiSira desc";
                    using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,variables.Fabrika))
					{
						if (!reader.HasRows)
						{
							PlanAdiSira = 1;
						}

						while (reader.Read())
						{

							PlanAdiSira = Convert.ToInt32(reader[0]) + 1;
						}
						
					}
				}

				if(!isNew)
				{
                    if (simulasyonTip == "Simülasyon")
                        Variables.Query_ = "select distinct sira_no from sipmamul where plan_adi=@planAdi";
                    if (simulasyonTip == "Simülasyon Dosemeli")
                        Variables.Query_ = "select distinct sira_no from sipmamul where plan_adi=@planAdi";
                    if (simulasyonTip == "Simülasyon Sunta")
                        Variables.Query_ = "select distinct sira_no from vbtSimulasyonSunta where plan_adi=@planAdi";
                    if (simulasyonTip == "Ahsap Plan")
                        Variables.Query_ = "select distinct PlanAdiSira from vatSimulasyon where planAdi=@planAdi";
                    SqlParameter[] parameter = new SqlParameter[1];

					parameter[0] = new SqlParameter("@planAdi",SqlDbType.NVarChar,500);
					parameter[0].Value = planAdi;

					using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter,variables.Fabrika))
					{
						while (reader.Read())
						{
							if (!reader.HasRows)
								return 2;

							PlanAdiSira = Convert.ToInt32(reader[0]);
						}

					}
				}

                if (simulasyonTip == "Simülasyon" || simulasyonTip == "Simülasyon Dosemeli")
				{ 
					foreach (Cls_Planlama plan in planAdlari)
					{
						SqlParameter[] parameters = new SqlParameter[7];

						parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 500);
						parameters[0].Value = planAdi;
						parameters[1] = new SqlParameter("@planSira", SqlDbType.Int);
						parameters[1].Value = PlanAdiSira;
						parameters[2] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 50);
						parameters[2].Value = plan.UrunKodu;
						parameters[3] = new SqlParameter("@mamulAdi", SqlDbType.NVarChar, 500);
						parameters[3].Value = plan.UrunAdi;
						parameters[4] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
						parameters[4].Value = plan.SiparisNumarasi;
						parameters[5] = new SqlParameter("@siparisSira", SqlDbType.Int);
						parameters[5].Value = plan.SiparisSira;
						parameters[6] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
						parameters[6].Value = plan.SiparisMiktar;

						if(simulasyonTip=="Simülasyon")
						variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertSimulasyon", Variables.Yil_, parameters);

                        if (simulasyonTip == "Simülasyon Dosemeli")
                            variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertSimulasyonDosemeli", Variables.Yil_, parameters);

                        if (!variables.Result)
							return 4;
					}
                }

                if (simulasyonTip == "Simülasyon Sunta")
                {
                    foreach (Cls_Planlama plan in planAdlari)
                    {
                        SqlParameter[] parameters = new SqlParameter[5];

                        parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 500);
                        parameters[0].Value = planAdi;
                        parameters[1] = new SqlParameter("@planSira", SqlDbType.Int);
                        parameters[1].Value = PlanAdiSira;
                        parameters[2] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 50);
                        parameters[2].Value = plan.UrunKodu;
                        parameters[3] = new SqlParameter("@mamulAdi", SqlDbType.NVarChar, 500);
                        parameters[3].Value = plan.UrunAdi;
                        parameters[4] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
                        parameters[4].Value = plan.SiparisMiktar;

                        variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertSimulasyonSunta", Variables.Yil_, parameters);
                        if (!variables.Result)
                            return 4;
                    }
                }
                if (simulasyonTip == "Ahsap Plan")
                {
                    foreach (Cls_Planlama plan in planAdlari)
                    {
                        SqlParameter[] parameters = new SqlParameter[7];

                        parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 500);
                        parameters[0].Value = planAdi;
                        parameters[1] = new SqlParameter("@planSira", SqlDbType.Int);
                        parameters[1].Value = PlanAdiSira;
                        parameters[2] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar,15);
                        parameters[2].Value = plan.SiparisNumarasi;
                        parameters[3] = new SqlParameter("@siparisSira", SqlDbType.Int);
                        parameters[3].Value = plan.SiparisSira;
                        parameters[4] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 50);
                        parameters[4].Value = plan.UrunKodu;
                        parameters[5] = new SqlParameter("@mamulAdi", SqlDbType.NVarChar, 500);
                        parameters[5].Value = plan.UrunAdi;
                        parameters[6] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
                        parameters[6].Value = plan.SiparisMiktar;

                        variables.Result = data.ExecuteStoredProcedureWithParameters("vbpInsertAhsapPlanSimulasyon", Variables.Yil_, parameters,variables.Fabrika);
                        if (!variables.Result)
                            return 4;
                    }
                }
                return 1;
			}
			catch 
			{
				return 5;
			}
		}
        public async Task<int> InsertIsemriTekil(ObservableCollection<Cls_Planlama> isemirleriCollection,bool uretimBildir_)
        {
            try
            {
				if (isemirleriCollection == null)
					return 3;
				if (isemirleriCollection.Count == 0)
					return 4;


				string user = login.GetUserName();
                variables.Counter = 1;
                foreach (Cls_Planlama isemri in isemirleriCollection)
                {
					SqlParameter[] parameters = new SqlParameter[14];
					
					parameters[0] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
					parameters[0].Value = isemri.SiparisNumarasi;
					parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
					parameters[1].Value = isemri.SiparisSira;
					parameters[2] = new SqlParameter("@planNo", SqlDbType.NVarChar,10);
					parameters[2].Value = isemri.PlanNo;
					parameters[3] = new SqlParameter("@takipNo", SqlDbType.NVarChar,10);
					parameters[3].Value = isemri.TakipNo;
					parameters[4] = new SqlParameter("@referansisemrino", SqlDbType.NVarChar,15);
					parameters[4].Value = isemri.ReferansIsemrino;
					parameters[5] = new SqlParameter("@tepemamul", SqlDbType.NVarChar, 35);
					parameters[5].Value = isemri.UrunKodu;
					parameters[6] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
					parameters[6].Value = isemri.StokKodu;
					parameters[7] = new SqlParameter("@isemriMiktar", SqlDbType.Int);
					parameters[7].Value = isemri.IsemriMiktar;
					parameters[8] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
					parameters[8].Value = user;
                    parameters[9] = new SqlParameter("@depoKodu", SqlDbType.Int);
					parameters[9].Value = isemri.DepoKodu;
					parameters[10] = new SqlParameter("@cikisDepoKodu", SqlDbType.Int);
					parameters[10].Value = isemri.CikisDepoKodu;
					parameters[11] = new SqlParameter("@aciklama", SqlDbType.NVarChar,100);
					parameters[11].Value = isemri.IsemriAciklama;
					parameters[12] = new SqlParameter("@uretimBildir", SqlDbType.Bit);
					parameters[12].Value = isemri.UretimBildir_;
					parameters[13] = new SqlParameter("@bildirimTarih", SqlDbType.DateTime);
					parameters[13].Value = isemri.BildirimTarih;

					variables.Result = await data.ExecuteStoredProcedureWithParametersAsync("vbpInsertIsemriTek", Variables.Yil_, parameters,variables.Fabrika);
					if (!variables.Result)
					    return 2;

					variables.Counter++;
                }

                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public async Task<int> InsertTopluIsemri(ObservableCollection<Cls_Planlama> isemirleriCollection)
        {
            try
            {
				string user = login.GetUserName();
                variables.Counter = 1;
                foreach (Cls_Planlama isemri in isemirleriCollection)
                {
					SqlParameter[] parameters = new SqlParameter[8];
					
					parameters[0] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
					parameters[0].Value = isemri.SiparisNumarasi;
					parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
					parameters[1].Value = isemri.SiparisSira;
					parameters[2] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
					parameters[2].Value = isemri.SiparisMiktar;
					parameters[3] = new SqlParameter("@gonderilecekMiktar", SqlDbType.Int);
					parameters[3].Value = isemri.GonderilecekIsemriMiktar;
					parameters[4] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 800);
					parameters[4].Value = isemri.IsemriAciklama;
					parameters[5] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
					parameters[5].Value = user;
					parameters[6] = new SqlParameter("@urunKodu", SqlDbType.NVarChar,35);
					parameters[6].Value = isemri.UrunKodu;
					parameters[7] = new SqlParameter("@teslimTarihi", SqlDbType.DateTime);
					parameters[7].Value = isemri.TeslimTarih;

					variables.Result = await data.ExecuteStoredProcedureWithParametersAsync("vbpInsertIsemri", Variables.Yil_, parameters,variables.Fabrika);
					if (!variables.Result)
					    return 2;

					variables.Counter++;
                }

                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public bool TruncatePlanAdlari(string simulasyonTip)
		{
			try
			{
				if(simulasyonTip == "Simülasyon")
					Variables.Query_ = "truncate table sipmamul";
				if(simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "truncate table sipmamuldos";
                if (simulasyonTip == "Simülasyon Sunta")
                    Variables.Query_ = "truncate table vbtSimulasyonSunta";
                if (simulasyonTip == "Ahsap Plan")
                    Variables.Query_ = "truncate table vatSimulasyon";

                variables.Result = data.ExecuteCommand(Variables.Query_, Variables.Yil_,variables.Fabrika);

				return variables.Result;
			}
			catch 
			{
				return false;
			}
		}
		public int DeletePlanAdi(string simulasyonTip,string planAdi) 
		{
			try
			{
                // plan adına ait kayıt var mı
                if ((simulasyonTip == "Simülasyon" ||
               simulasyonTip == "Simülasyon Dosemeli"))
                {
                    Variables.Query_ = "vbpCheckPlanAdiOkToDel";
                    
						SqlParameter[] parameterCheck = new SqlParameter[1];
                        parameterCheck[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 400);
                        parameterCheck[0].Value = planAdi;
                   
                        Variables.ResultString_ = data.Get_One_String_Result_Stored_Proc_With_Parameters(Variables.Query_, Variables.Yil_, parameterCheck);
                        if (Variables.ResultString_ == "Bildirim Var")
                            return 6;
                        if (Variables.ResultString_ == "Siparis Var")
                            return 7;
                        if (Variables.ResultString_ == "STRING HATA" ||
                            Variables.ResultString_ == string.Empty)
                            return 10;

                }

                if (simulasyonTip == "Ahsap Plan")
					Variables.Query_ = "Delete from vatSimulasyon where planAdi = @planAdi";
				if (simulasyonTip == "Simülasyon")
					Variables.Query_ = "Delete from sipmamul where plan_adi = @planAdi";
				if (simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "Delete from sipmamuldos where plan_adi = @planAdi";
				if (simulasyonTip == "Simülasyon Sunta")
					Variables.Query_ = "Delete from vbtSimulasyonSunta where plan_adi = @planAdi";


					SqlParameter[] parameter = new SqlParameter[1];
					parameter[0] = new SqlParameter("@planAdi",SqlDbType.NVarChar,400);
					parameter[0].Value = planAdi;

					variables.Result = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_,parameter,variables.Fabrika);
					if (!variables.Result)
						return 2;
					return 1;
                
            }
			catch 
			{
				return -1;
			}
		}
		public async Task<bool> DeletePlanNoTopluAsync(ObservableCollection<Cls_Planlama> silinecekPlanlar) 
		{
			try
			{

				if (silinecekPlanlar == null)
					return false;
				if (silinecekPlanlar.Count() < 1)
					return false;

				foreach(Cls_Planlama item in silinecekPlanlar)
				{ 
					SqlParameter[] parameter = new SqlParameter[3];
					parameter[0] = new SqlParameter("@planNo", SqlDbType.NVarChar,100);
					parameter[0].Value = item.PlanNo;
                    parameter[1] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameter[1].Value = item.SiparisNumarasi;
                    parameter[2] = new SqlParameter("@siparisSira", SqlDbType.Int);
                    parameter[2].Value = item.SiparisSira;

                    Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync("vbpDeletePlanNo",Variables.Yil_,parameter,Variables.Fabrika_);
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
		public async Task<bool> DeletePlanNoAsync(string planNo,string siparisNo,int siparisSira) 
		{
			try
			{
				
					SqlParameter[] parameter = new SqlParameter[3];
					parameter[0] = new SqlParameter("@planNo", SqlDbType.NVarChar,100);
					parameter[0].Value = planNo;
					parameter[1] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar,15);
					parameter[1].Value = siparisNo;
					parameter[2] = new SqlParameter("@siparisSira", SqlDbType.Int);
					parameter[2].Value = siparisSira;

					Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync("vbpDeletePlanNo",Variables.Yil_,parameter,Variables.Fabrika_);
					if (!Variables.Result_)
						return false;
					return true;
                
            }
			catch 
			{
				return false;
			}
		}
		public int DeletePlanAdiDetay(string simulasyonTip,Cls_Planlama dataItem) 
		{
			try
			{
				if (simulasyonTip == "Ahsap Plan")
					Variables.Query_ = "Delete from vatSimulasyon where PlanAdi = @planAdi and SiparisNumarasi=@siparisNo and SiparisSira = @siparisSira and SiparisMiktar = @siparisMiktar";
				if (simulasyonTip == "Simülasyon")
					Variables.Query_ = "Delete from sipmamul where plan_adi = @planAdi and sip_no=@siparisNo and sip_sira = @siparisSira and sipmiktar = @siparisMiktar";
				if (simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "Delete from sipmamuldos where plan_adi = @planAdi and sip_no=@siparisNo and sip_sira = @siparisSira and sipmiktar = @siparisMiktar";



					SqlParameter[] parameters = new SqlParameter[4];
					parameters[0] = new SqlParameter("@planAdi",SqlDbType.NVarChar,400);
					parameters[0].Value = dataItem.PlanAdi;
					parameters[1] = new SqlParameter("@siparisNo",SqlDbType.NVarChar,15);
					parameters[1].Value = dataItem.SiparisNumarasi;
					parameters[2] = new SqlParameter("@siparisSira",SqlDbType.Int);
					parameters[2].Value = dataItem.SiparisSira;
					parameters[3] = new SqlParameter("@siparisMiktar",SqlDbType.Int);
					parameters[3].Value = dataItem.SiparisMiktar;

					variables.Result = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);
					if (!variables.Result)
						return 2;
					return 1;

            }
			catch 
			{
				return -1;
			}
		}
		public async Task<ObservableCollection<Cls_Planlama>> GetIsemriKalanIhtiyacAsync(Dictionary<string,string> restrictionPairs, string restrictionQueries)
		{
			try
			{
				if (restrictionPairs != null)
				{
					Variables.Query_ = "select * from vbvIeKalanIhtiyac (nolock)  where 1 = 1 " ;
					Variables.Counter_ = 0;



					if (restrictionPairs.ContainsKey("@siparisNumarasi"))
					{
						Variables.Query_ += " and SiparisNo like '%' + @siparisNumarasi + '%'";
						Variables.Counter_++;
					}
					if (restrictionPairs.ContainsKey("@siparisSira"))
					{
						Variables.Query_ += " and SiparisSira = @siparisSira";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@urunKodu"))
					{
						Variables.Query_ += " and TepeMamul like '%' + @urunKodu + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@urunAdi"))
					{
						Variables.Query_ += " and UrunAdi like  '%' + @urunAdi + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@referansIsemrino"))
					{
						Variables.Query_ += " and ReferansIsemri like '%' + @referansIsemrino + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@isemrino"))
					{
						Variables.Query_ += " and Isemrino like '%' + @isemrino + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@mamulKodu"))
					{
						Variables.Query_ += " and MamulKodu like '%' + @mamulKodu + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@mamulAdi"))
					{
						Variables.Query_ += " and MamulAdi like '%' + @mamulAdi + '%'";
						Variables.Counter_++;
					}
					if (restrictionPairs.ContainsKey("@hamKodu"))
					{
						Variables.Query_ += " and HamKodu like '%' + @hamKodu + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@hamAdi"))
					{
						Variables.Query_ += " and HamAdi like '%' + @hamAdi + '%'";
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@kod1"))
					{
						Variables.Query_ += " and Kod1 = @kod1 ";
						Variables.Counter_++;
					}

					if (!string.IsNullOrEmpty(restrictionQueries))
						Variables.Query_ += restrictionQueries;

					SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

					Variables.Counter_ = 0;
					if (restrictionPairs.ContainsKey("@siparisNumarasi"))
					{
						parameters[Variables.Counter_] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
						parameters[Variables.Counter_].Value = restrictionPairs["@siparisNumarasi"];
						Variables.Counter_++;
					}
					if (restrictionPairs.ContainsKey("@siparisSira"))
					{
						parameters[Variables.Counter_] = new("@siparisSira", SqlDbType.Int);
						parameters[Variables.Counter_].Value = Convert.ToInt32(restrictionPairs["@siparisSira"]);
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@urunKodu"))
					{
						parameters[Variables.Counter_] = new("@urunKodu", SqlDbType.NVarChar, 35);
						parameters[Variables.Counter_].Value = restrictionPairs["@urunKodu"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@urunAdi"))
					{
						parameters[Variables.Counter_] = new("@urunAdi", SqlDbType.NVarChar, 435);
						parameters[Variables.Counter_].Value = restrictionPairs["@urunAdi"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@referansIsemrino"))
					{
						parameters[Variables.Counter_] = new("@referansIsemrino", SqlDbType.NVarChar, 15);
						parameters[Variables.Counter_].Value = restrictionPairs["@referansIsemrino"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@isemrino"))
					{
						parameters[Variables.Counter_] = new("@isemrino", SqlDbType.NVarChar, 15);
						parameters[Variables.Counter_].Value = restrictionPairs["@isemrino"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@mamulKodu"))
					{
						parameters[Variables.Counter_] = new("@mamulKodu", SqlDbType.NVarChar, 15);
						parameters[Variables.Counter_].Value = restrictionPairs["@mamulKodu"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@mamulAdi"))
					{
						parameters[Variables.Counter_] = new("@mamulAdi", SqlDbType.NVarChar, 435);
						parameters[Variables.Counter_].Value = restrictionPairs["@mamulAdi"];
						Variables.Counter_++;
					}
					if (restrictionPairs.ContainsKey("@hamKodu"))
					{
						parameters[Variables.Counter_] = new("@hamKodu", SqlDbType.NVarChar, 15);
						parameters[Variables.Counter_].Value = restrictionPairs["@hamKodu"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@hamAdi"))
					{
						parameters[Variables.Counter_] = new("@hamAdi", SqlDbType.NVarChar, 435);
						parameters[Variables.Counter_].Value = restrictionPairs["@hamAdi"];
						Variables.Counter_++;
					}

					if (restrictionPairs.ContainsKey("@kod1"))
					{
						parameters[Variables.Counter_] = new("@kod1", SqlDbType.NVarChar, 435);
						parameters[Variables.Counter_].Value = restrictionPairs["@kod1"];
						Variables.Counter_++;
					}
					ObservableCollection<Cls_Planlama> IsemriCollection = await data.Select_Command_Data_Async_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
				{
					Cls_Planlama model = new();
					model.SiparisNumarasi = reader["SiparisNo"] is DBNull ? "" : reader["SiparisNo"].ToString();
					model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
					model.ReferansIsemrino = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
					model.UrunKodu = reader["TepeMamul"] is DBNull ? "" : reader["TepeMamul"].ToString();
					model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
					model.Isemrino = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
					model.MamulKodu = reader["MamulKodu"] is DBNull ? "" : reader["MamulKodu"].ToString();
					model.MamulAdi = reader["MamulAdi"] is DBNull ? "" : reader["MamulAdi"].ToString();
					model.UretimDurumu = reader["UretimDurum"] is DBNull ? "" : reader["UretimDurum"].ToString();
					model.IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["IsemriMiktar"]);
					model.UretilenMiktar = reader["UretilenMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["UretilenMiktar"]);
					model.KalanIsemriMiktar = reader["KalanIsemri"] is DBNull ? 0 : Convert.ToInt32(reader["KalanIsemri"]);
					model.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
					model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
					model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
					model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
					model.OlcuBirimi = reader["OlcuBirimi"] is DBNull ? "" : reader["OlcuBirimi"].ToString();
					model.HamIhtiyacMiktarBirim = reader["ReceteBirimMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["ReceteBirimMiktar"]);
					model.HamIhtiyacMiktar = reader["ReceteToplamMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["ReceteToplamMiktar"]);
					model.HamIhtiyacMiktarKalan = reader["KalanHamIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["KalanHamIhtiyac"]);
					return model;
				});
					return IsemriCollection;
				}
				else
				{
                    Variables.Query_ = "select * from vbvIeKalanIhtiyac (nolock)  where 1 = 1 " ;
                    Variables.Counter_ = 0;


                    if (!string.IsNullOrEmpty(restrictionQueries))
                        Variables.Query_ += restrictionQueries;

                    ObservableCollection<Cls_Planlama> IsemriCollection = await data.Select_Command_Data_Async(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                    {
                        Cls_Planlama model = new();
                        model.SiparisNumarasi = reader["SiparisNo"] is DBNull ? "" : reader["SiparisNo"].ToString();
                        model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                        model.ReferansIsemrino = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
                        model.UrunKodu = reader["TepeMamul"] is DBNull ? "" : reader["TepeMamul"].ToString();
                        model.Isemrino = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                        model.MamulKodu = reader["MamulKodu"] is DBNull ? "" : reader["MamulKodu"].ToString();
                        model.MamulAdi = reader["MamulAdi"] is DBNull ? "" : reader["MamulAdi"].ToString();
                        model.UretimDurumu = reader["UretimDurum"] is DBNull ? "" : reader["UretimDurum"].ToString();
                        model.IsemriMiktar = reader["IsemriMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["IsemriMiktar"]);
                        model.UretilenMiktar = reader["UretilenMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["UretilenMiktar"]);
                        model.KalanIsemriMiktar = reader["KalanIsemri"] is DBNull ? 0 : Convert.ToInt32(reader["KalanIsemri"]);
                        model.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                        model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                        model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                        model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
                        model.OlcuBirimi = reader["OlcuBirimi"] is DBNull ? "" : reader["OlcuBirimi"].ToString();
                        model.HamIhtiyacMiktarBirim = reader["ReceteBirimMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["ReceteBirimMiktar"]);
                        model.HamIhtiyacMiktar = reader["ReceteToplamMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["ReceteToplamMiktar"]);
                        model.HamIhtiyacMiktarKalan = reader["KalanHamIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["KalanHamIhtiyac"]);
                        return model;
                    });
                    return IsemriCollection;
                }
			}
			catch
			{
				return null;
			}
        }
        public ObservableCollection<Cls_Planlama> GetPlanaBagliTalepListesi(Dictionary<string, string> restrictionPairs, int pageNumber)
        {
            try
            {
                    Variables.Query_ = "select * from vbvPlanTakipNoAlListe (nolock) where 1 = 1 ";
                    Variables.Counter_ = 0;

                    if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNumarasi + '%'";
                        Variables.Counter_++;
                    }

                    if (restrictionPairs.ContainsKey("@planAdi"))
                    {
                        Variables.Query_ += " and PlanAdi like '%' + @planAdi + '%'";
                        Variables.Counter_++;
                    }

                    SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                    Variables.Counter_ = 0;
                    if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                    {
                        parameters[Variables.Counter_] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                        parameters[Variables.Counter_].Value = restrictionPairs["@siparisNumarasi"];
                        Variables.Counter_++;
                    }

                    if (restrictionPairs.ContainsKey("@planAdi"))
                    {
                        parameters[Variables.Counter_] = new("@planAdi", SqlDbType.NVarChar, 15);
                        parameters[Variables.Counter_].Value = restrictionPairs["@planAdi"];
                        Variables.Counter_++;
                    }
                Variables.Query_ += $"order by PlanAdi,SiparisNumarasi,SiparisSira desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                ObservableCollection<Cls_Planlama> SiparisCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                    {
                        Cls_Planlama model = new();
                        model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                        model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                        model.ReferansIsemrino = reader["ReferansIsemri"] is DBNull ? "" : reader["ReferansIsemri"].ToString();
                        model.UrunKodu = reader["TepeMamul"] is DBNull ? "" : reader["TepeMamul"].ToString();
                        model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                        model.Isemrino = reader["Isemrino"] is DBNull ? "" : reader["Isemrino"].ToString();
                        model.PlanNo = reader["MamulKodu"] is DBNull ? "" : reader["MamulKodu"].ToString();
						model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                        
                        return model;
                    });
                    return SiparisCollection;
                
            }
            catch
            {
                return null;
            }
        }

        public int CountTopluIsemriAcList(Dictionary<string, string> kisitPairs, int pageNumber)
        {
            try
            {
                Variables.Query_ = "select count(*) from vbvAcilacakIsemirleri where 1=1";
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
                {
                    Variables.Query_ = Variables.Query_ + "and siparisNo like '%' + @siparisNo + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urunKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urunAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and cariAdi like '%' + @cariAdi + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];
                variables.Counter = 0;

                if (!string.IsNullOrEmpty(kisitPairs["siparisNo"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = kisitPairs["siparisNo"];
                    variables.Counter++;
                }

                if (!string.IsNullOrEmpty(kisitPairs["stokKodu"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = kisitPairs["stokKodu"];
                    variables.Counter++;
                }


                if (!string.IsNullOrEmpty(kisitPairs["stokAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@stokAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = kisitPairs["stokAdi"];
                    variables.Counter++;
                }
                if (!string.IsNullOrEmpty(kisitPairs["cariAdi"]))
                {
                    parameters[variables.Counter] = new SqlParameter("@cariAdi", SqlDbType.NVarChar, 400);
                    parameters[variables.Counter].Value = kisitPairs["cariAdi"];
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
        public int CountPlanaBagliTalep(Dictionary<string, string> restrictionPairs, int pageNumber)
        {
            try
            {
                if (login.GetDepartment().Contains("Moduler"))
                    Variables.Query_ = "Select count(*) from vbtTalepIhtiyacListeMod where 1=1 and miktar>0.0001 ";
                else
                    Variables.Query_ = "Select count(*) from vbtTalepIhtiyacListeDos where 1=1 and miktar>0.0001 ";

                Variables.Counter_ = 0;

                if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                {
                    Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNumarasi + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planAdi"))
                {
                    Variables.Query_ += " and PlanAdi like '%' + @planAdi + '%'";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                {
                    parameters[Variables.Counter_] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = restrictionPairs["@siparisNumarasi"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planAdi"))
                {
                    parameters[Variables.Counter_] = new("@planAdi", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = restrictionPairs["@planAdi"];
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
        public int CountPlanNoVerilecekSiparisler(Dictionary<string, string> restrictionPairs, bool planNoVerilenleriGosterme,int pageNumber, bool isTakipNo)
        {
            try
            {
                Variables.Query_ = "with cte as (select distinct * from vbvPlanTakipNoAlListe (nolock) where 1 = 1 ";
                Variables.Counter_ = 0;

				if (isTakipNo)
					Variables.Query_ += " and TakipNo = '' ";

                if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                {
                    Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNumarasi + '%'";
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planAdi"))
                {
                    Variables.Query_ += " and PlanAdi = @planAdi ";
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%' ";
                    Variables.Counter_++;
                }
                if (restrictionPairs.ContainsKey("@planNo"))
                {
                    Variables.Query_ += " and PlanNo = @planNo ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

                Variables.Counter_ = 0;
                if (restrictionPairs.ContainsKey("@siparisNumarasi"))
                {
                    parameters[Variables.Counter_] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[Variables.Counter_].Value = restrictionPairs["@siparisNumarasi"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planNo"))
                {
                    parameters[Variables.Counter_] = new("@planNo", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = restrictionPairs["@planNo"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@planAdi"))
                {
                    parameters[Variables.Counter_] = new("@planAdi", SqlDbType.NVarChar, 300);
                    parameters[Variables.Counter_].Value = restrictionPairs["@planAdi"];
                    Variables.Counter_++;
                }

                if (restrictionPairs.ContainsKey("@urunKodu"))
                {
                    parameters[Variables.Counter_] = new("@urunKodu", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = restrictionPairs["@urunKodu"];
                    Variables.Counter_++;
                }

                if (planNoVerilenleriGosterme == true)
                    Variables.Query_ += " and PlanNo=''";

                Variables.Query_ += $") select count(*) from cte ";

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
        public ObservableCollection<Cls_Planlama> PlanNoVerilecekSiparisler(Dictionary<string, string> restrictionPairs,bool planNoVerilenleriGosterme,int pageNumber, bool isTakipNo) 
		{
			try
			{

				Variables.Query_ = "select distinct * from vbvPlanTakipNoAlListe (nolock) where 1 = 1 ";
				Variables.Counter_ = 0;

                if (restrictionPairs.ContainsKey("@siparisNumarasi"))
				{
				    Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNumarasi + '%'";
				    Variables.Counter_++;
				}

				if (restrictionPairs.ContainsKey("@planAdi"))
				{
				    Variables.Query_ += " and PlanAdi = @planAdi ";
				    Variables.Counter_++;
				}
				if (restrictionPairs.ContainsKey("@urunKodu"))
				{
				    Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%' ";
				    Variables.Counter_++;
				}
				if (restrictionPairs.ContainsKey("@planNo"))
				{
				    Variables.Query_ += " and PlanNo = @planNo ";
				    Variables.Counter_++;
				}

				SqlParameter[] parameters = new SqlParameter[Variables.Counter_];

				Variables.Counter_ = 0;
				if (restrictionPairs.ContainsKey("@siparisNumarasi"))
				{
				    parameters[Variables.Counter_] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
				    parameters[Variables.Counter_].Value = restrictionPairs["@siparisNumarasi"];
				    Variables.Counter_++;
				}
				
				if (restrictionPairs.ContainsKey("@planNo"))
				{
				    parameters[Variables.Counter_] = new("@planNo", SqlDbType.NVarChar, 100);
				    parameters[Variables.Counter_].Value = restrictionPairs["@planNo"];
				    Variables.Counter_++;
				}

				if (restrictionPairs.ContainsKey("@planAdi"))
				{
				    parameters[Variables.Counter_] = new("@planAdi", SqlDbType.NVarChar, 300);
				    parameters[Variables.Counter_].Value = restrictionPairs["@planAdi"];
				    Variables.Counter_++;
				}

				if (restrictionPairs.ContainsKey("@urunKodu"))
				{
				    parameters[Variables.Counter_] = new("@urunKodu", SqlDbType.NVarChar, 100);
				    parameters[Variables.Counter_].Value = restrictionPairs["@urunKodu"];
				    Variables.Counter_++;
				}

				if (planNoVerilenleriGosterme == true)
					Variables.Query_ += " and PlanNo='' ";

                Variables.Query_ += $" order by SiparisNumarasi desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";


                ObservableCollection<Cls_Planlama> PlanCollection = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
				{
				    Cls_Planlama model = new();
				    model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
				    model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
				    model.ReferansIsemrino = reader["RefIsemrino"] is DBNull ? "" : reader["RefIsemrino"].ToString();
				    model.IsemriMiktar = reader["IeMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["IeMiktar"]);
				    model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
				    model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
				    model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
				    model.SiparisMiktar = reader["SiparisMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisMiktar"]);
					model.PlanNo = reader["PlanNo"] is DBNull ? "" : reader["PlanNo"].ToString();
                    model.TakipNo = reader["TakipNo"] is DBNull ? "" : reader["TakipNo"].ToString();
                    model.MevcutSiparisAdedi = reader["MevcutSiparisAdedi"] is DBNull ? 0 : Convert.ToDecimal(reader["MevcutSiparisAdedi"]);
                    model.MevcutTalepAdedi = reader["MevcutTalepAdedi"] is DBNull ? 0 : Convert.ToDecimal(reader["MevcutTalepAdedi"]);
                    return model;
				});
				return PlanCollection;

			}
            catch
            {
                return null;
            }
		}
		public ObservableCollection<Cls_Planlama> PopulatePlanaBagliHammaddeDetay(string planAdi,string hamKodu)
		{
			try
			{
				if (string.IsNullOrEmpty(planAdi) ||
					string.IsNullOrEmpty(hamKodu))
					return null;

				if (Variables.Departman_.Contains("Moduler"))
					Variables.Query_ = "select * from vbvPlanaBagliHammaddeDetayMod where PlanAdi=@planAdi and HamKodu=@hamKodu";

                else
                    Variables.Query_ = "select * from vbvPlanaBagliHammaddeDetayDos where PlanAdi=@planAdi and HamKodu=@hamKodu";
                SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 15);
				parameters[0].Value= planAdi;
				parameters[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
				parameters[1].Value=hamKodu;
				ObservableCollection<Cls_Planlama> hamColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
				{
					Cls_Planlama model = new Cls_Planlama();
					model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
					//model.PlanNo = reader["PlanNumarasi"] is DBNull ? "" : reader["PlanNumarasi"].ToString();
					model.TakipNo = reader["TakipNumarasi"] is DBNull ? "" : reader["TakipNumarasi"].ToString();
					model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
					model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
					model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
					model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
					model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
					model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
					model.HamIhtiyacMiktar = reader["ToplamIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["ToplamIhtiyac"]);
					model.HamIhtiyacMiktarKalan = reader["UretimMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["UretimMiktar"]);
					model.HamKoduDatMiktar = reader["DatMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["DatMiktar"]);

					return model;
				});
				return hamColl;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<ObservableCollection<Cls_Planlama>> PopulatePlanaBagliHammaddeDetayAsync(string planNo,string hamKodu)
		{
			try
			{
				if (string.IsNullOrEmpty(planNo) ||
					string.IsNullOrEmpty(hamKodu))
					return null;

				Variables.Query_ = "select * from vbvPlanaBagliHammaddeDetay where PlanNumarasi=@planNo and HamKodu=@hamKodu";
				SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
				parameters[0].Value=planNo;
				parameters[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
				parameters[1].Value=hamKodu;

				ObservableCollection<Cls_Planlama> hamColl = await data.Select_Command_Data_Async_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
				{
					Cls_Planlama model = new Cls_Planlama();
					model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
					model.PlanNo = reader["PlanNumarasi"] is DBNull ? "" : reader["PlanNumarasi"].ToString();
					model.TakipNo = reader["TakipNumarasi"] is DBNull ? "" : reader["TakipNumarasi"].ToString();
					model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
					model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
					model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
					model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
					model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
					model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
					model.HamIhtiyacMiktar = reader["ToplamIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["ToplamIhtiyac"]);
					model.HamIhtiyacMiktarKalan = reader["UretimMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["UretimMiktar"]);
					model.HamKoduDatMiktar = reader["DatMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["DatMiktar"]);

					return model;
				});
				return hamColl;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public ObservableCollection<Cls_Planlama> GetSipnoSipSiraForTalepAc(string planNo,string hamKodu)
		{
			try
			{
				if (string.IsNullOrEmpty(planNo) ||
					string.IsNullOrEmpty(hamKodu))
					return null;

				Variables.Query_ = "select SiparisNumarasi,SiparisSira from vbvPlanaBagliHammaddeDetay where PlanNumarasi=@planNo and HamKodu=@hamKodu";
				SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
				parameters[0].Value=planNo;
				parameters[1] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
				parameters[1].Value=hamKodu;
				ObservableCollection<Cls_Planlama> hamColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
				{
					Cls_Planlama model = new Cls_Planlama();
					model.SiparisNumarasi = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
					model.SiparisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
					
					return model;
				});

				return hamColl;
			}
			catch (Exception)
			{
				return null;
			}
		}
        public List<string> GetDistinctPlanNo()
		{
			try
			{
				List<string> planlar = new();

					Variables.Query_ = "Select distinct kt_planno from tblisemriek (nolock) order by kt_planno desc";
			
				using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, Variables.Fabrika_))
				{
					if (!reader.HasRows)
						return null;

					while(reader.Read()) 
					{
						string plan = reader[0].ToString();

                        planlar.Add(plan);
                    }
                }
				return planlar;
			}
			catch 
			{
				return null;
			}
        }
        public List<string> GetDistinctPlanAdiString()
        {
            try
            {
				string departman = Variables.Departman_;
                List<string> planlar = new();

                if (departman == "Moduler")
                    Variables.Query_ = "Select distinct plan_adi from sipmamul order by plan_adi";
                else if (departman == "Doseme")
                    Variables.Query_ = "Select distinct plan_adi from sipmamuldos order by plan_adi";
                else if (departman == "Hepsi")
                    Variables.Query_ = "Select distinct plan_adi from sipmamuldos union all select distinct plan_adi from sipmamul order by plan_adi";
                else 
					Variables.Query_ = "Select distinct plan_adi from sipmamuldos union all select distinct plan_adi from sipmamul order by plan_adi";

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (!reader.HasRows)
                        return null;

                    while (reader.Read())
                    {
                        string plan = reader[0].ToString();

                        planlar.Add(plan);
                    }
                }
                return planlar;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                return null;
            }
        }
        public ObservableCollection<Cls_Planlama> GetDistinctPlanAdi()
        {
            try
            {
                string departman = Variables.Departman_;
                ObservableCollection<Cls_Planlama> planlar = new ();

                if (departman == "Moduler")
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamul order by sira_no";
                else if (departman == "Doseme")
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos order by sira_no";
                else if (departman == "Hepsi")
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos union all select distinct sira_no,plan_adi from sipmamul order by sira_no";
                else
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos union all select distinct sira_no,plan_adi from sipmamul order by sira_no";

                using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                {
                    if (!reader.HasRows)
                        return null;

                    while (reader.Read())
                    {
						Cls_Planlama planItem = new();
                        planItem.PlanAdiSira = Convert.ToInt32(reader[0]);
                        planItem.PlanAdi = reader[1].ToString();

                        planlar.Add(planItem);
                    }
                }
                return planlar;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
				return null;
            }
        }
        public ObservableCollection<Cls_Planlama> GetDistinctPlanAdi(Dictionary<string,string> constraintPairs)
        {
            try
            {
				if(constraintPairs == null)
					return null;
				if(constraintPairs.Count == 0)
                    return null;

                string departman = Variables.Departman_;
                ObservableCollection<Cls_Planlama> planlar = new();

                if (departman.Contains("Moduler"))
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamul where plan_adi like '%' + @planAdi + '%' order by sira_no, plan_adi ";
                else if (departman == "Doseme")
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos where plan_adi like '%' + @planAdi + '%' order by sira_no, plan_adi";
                else if (departman == "Hepsi")
				     Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos where plan_adi like '%' + @planAdi + '%'  union all select distinct sira_no,plan_adi from sipmamul where plan_adi like '%' + @planAdi + '%'  order by sira_no, plan_adi";
                else
                    Variables.Query_ = "Select distinct sira_no,plan_adi from sipmamuldos where plan_adi like '%' + @planAdi + '%'  union all select distinct sira_no,plan_adi from sipmamul where plan_adi like '%' + @planAdi + '%'  order by sira_no, plan_adi";

				SqlParameter[] parameter = new SqlParameter[1];
				parameter[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 35);
				parameter[0].Value = constraintPairs.Values.FirstOrDefault();

                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter, variables.Fabrika))
                {
					if (reader == null)
						return null;
                    while (reader.Read())
                    {
                        Cls_Planlama planItem = new();
                        planItem.PlanAdiSira = Convert.ToInt32(reader[0]);
                        planItem.PlanAdi = reader[1].ToString();

                        planlar.Add(planItem);
                    }
                }
                return planlar;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                return null;
            }
        }

        public Dictionary<int,string> GetDistinctPlanAdiString(string departman)
		{
			try
			{
				Dictionary<int, string> planlar = new();

                if (departman == "Moduler")
					Variables.Query_ = "Select distinct sira_no, plan_adi from sipmamul order by plan_adi";
				if(departman == "Doseme")
					Variables.Query_ = "Select distinct sira_no, plan_adi from sipmamuldos order by plan_adi";
                if (departman == "Hepsi")
                    Variables.Query_ = "Select distinct sira_no, plan_adi from sipmamuldos order by plan_adi";
               
				using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika))
				{
					if (!reader.HasRows)
						return null;

					while(reader.Read()) 
					{
						int sira = Convert.ToInt32(reader[0]);
						string plan = reader[1].ToString();

                        planlar.Add(sira,plan);
                    }
                }
				return planlar;
			}
			catch (Exception ex)
			{
				string message = ex.Message.ToString();
				return null;
			}
        }
        public List<string> GetDistinctTakipNoString(string departman)
		{
			try
			{
				List<string> takipNos = new();

                if (departman == "Moduler")
					Variables.Query_ = @"with cte as (Select distinct substring(kt_takipnum,1,5) as takipNo from tblisemriek (nolock) where kt_takipnum like 'M%' )
										select takipNo from cte order by takipNo desc";
				if(departman == "Doseme")
					Variables.Query_ = @"with cte as (Select distinct substring(kt_takipnum,1,5) as takipNo from tblisemriek (nolock) where kt_takipnum like 'D%' )
										select takipNo from cte order by takipNo desc";
                if (departman == "Hepsi")
                    Variables.Query_ = @"with cte as (Select distinct substring(kt_takipnum,1,5) as takipNo from tblisemriek (nolock) )
										select takipNo from cte order by takipNo desc";
               
				using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika))
				{
					if (!reader.HasRows)
						return null;

					while(reader.Read()) 
					{
						string takipNo = reader[0].ToString();

                        takipNos.Add(takipNo);
                    }
                }
				return takipNos;
			}
			catch (Exception ex)
			{
				return null;
			}
        }
        public ObservableCollection<Cls_Planlama> GetDistinctPlanAdi(string simulasyonTip)
		{
			try
			{
				if(simulasyonTip == "Simülasyon")
					Variables.Query_ = "Select distinct sira_no, plan_adi from sipmamul";
				if(simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "Select distinct sira_no, plan_adi from sipmamuldos";
                if (simulasyonTip == "Simülasyon Sunta")
                    Variables.Query_ = "Select distinct sira_no, plan_adi from vbtSimulasyonSunta";
                if (simulasyonTip == "Ahsap Plan")
                    Variables.Query_ = "Select distinct planAdiSira, planAdi from vatSimulasyon";
                temp_coll_planlama.Clear();
				using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_,Variables.Yil_,variables.Fabrika))
				{
					if (!reader.HasRows)
						return null;

					while(reader.Read()) 
					{ 
						Cls_Planlama item = new Cls_Planlama()
						{
							PlanAdiSira = Convert.ToInt32(reader[0]),
							PlanAdi = reader[1].ToString(),
						};
						temp_coll_planlama.Add(item);
                    }
                }

				PlanlamaCollection = temp_coll_planlama;
				return PlanlamaCollection;
			}
			catch 
			{
				return null;
			}
        }
        public ObservableCollection<Cls_Planlama> GetDistinctPlanAdiForSimulation(string simulasyonTip)
        {
            try
            {
				//eğer döşemeli ve moduler simülasyon yapılacaksa renklendirme uzun sürdüğü için excele aktarda yap, sadece plan adlarını göster. diğer simülasyonlar için renklendirme yap
				if(simulasyonTip == "Simülasyon" || 
					simulasyonTip == "Simülasyon Dosemeli")
				{
                    if (simulasyonTip == "Simülasyon")
						Variables.Query_ = "SELECT distinct sira_no,plan_adi from sipmamul " ; 
					if(simulasyonTip == "Simülasyon Dosemeli")
						Variables.Query_ = "SELECT distinct sira_no,plan_adi from sipmamuldos ";

					ObservableCollection<Cls_Planlama> planAdiColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, "Vita", reader =>
                    {
                        Cls_Planlama model = new();
                        model.PlanAdiSira = reader["sira_no"] is DBNull ? 0 : Convert.ToInt32(reader["sira_no"]);
                        model.PlanAdi = reader["plan_adi"] is DBNull ? "" : reader["plan_adi"].ToString();
                        return model;
                    });
					return planAdiColl;
                   
                }
                if (simulasyonTip == "Simülasyon Sunta")
						Variables.Query_ = "select * from vbvSimulasyonSuntaDurum";
				if(simulasyonTip == "Ahsap Plan")
						Variables.Query_ = "SELECT sabit.stok_kodu as hamKodu " +
										" ,sabit.stok_adi as hamAdi" +
										" ,ISNULL(ph1.miktar, 0) as depoAdet" +
										" ,ISNULL(sip.miktar, 0) as siparisMiktar" +
										" ,ISNULL(talep.miktar, 0) as talepMiktar, " +    
										" sim.* FROM siparistablosuAhsap sim " +
										" LEFT JOIN TBLSTSABIT sabit(NOLOCK) ON sim.ham_kodu = sabit.STOK_KODU " +
										" LEFT JOIN(SELECT stok_kodu, ISNULL(CAST(top_giris_mik AS FLOAT), 0) -ISNULL(CAST(TOP_CIKIS_MIK AS FLOAT), 0) as miktar FROM TBLSTOKPH  where DEPO_KODU = 1) ph1 ON sim.ham_kodu = ph1.STOK_KODU " +
										" LEFT JOIN(SELECT a.stok_kodu, SUM(ISNULL(a.sthar_gcmik, 0)) -SUM(ISNULL(a.firma_dovtut, 0)) - SUM(ISNULL(b.FIRMA_DOVTUT, 0)) as miktar FROM TBLTEKLIFTRA a " +
										" LEFT JOIN TBLSIPATRA b ON a.stok_kodu = b.stok_kodu AND a.fisno = b.AMBAR_KABULNO WHERE a.STHAR_GCMIK > a.FIRMA_DOVTUT AND b.sthar_gcmik - b.FIRMA_DOVTUT > 0 AND a.STHAR_HTUR<> 'K' " +
										" GROUP BY a.STOK_KODU) talep ON sim.ham_kodu = talep.stok_kodu LEFT JOIN(SELECT stok_kodu, SUM(ISNULL(sthar_gcmik, 0)) -SUM(ISNULL(firma_dovtut, 0)) as miktar " +
										" FROM TBLSIPATRA  WHERE STHAR_GCMIK > FIRMA_DOVTUT AND STHAR_HTUR<> 'K'  GROUP BY STOK_KODU) sip ON sim.ham_kodu = sip.stok_kodu";

                    temp_coll_renklendir.Clear();
					decimal depoMiktar = 0, siparisMiktar = 0, talepMiktar = 0, kumulatifDurum = 0;

                    DataTable dataTable = data.Select_Command(Variables.Query_, Variables.Yil_, variables.Fabrika);

					if (
						dataTable == null)
						return null;

                    temp_coll_renklendir.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Cls_Planlama generalItem = new();

                        depoMiktar = Convert.ToDecimal(row[2]);
                        siparisMiktar = Convert.ToDecimal(row[3]);
                        talepMiktar = Convert.ToDecimal(row[4]);
                        //plan adlarının başladığı sütundan itibaren döngüye girerek renklendir
                        for (int i = 6; i < dataTable.Columns.Count; i++)
                        {
                            Cls_Planlama item = new();
                            item.HamKodu = row[0].ToString();
                            item.HamAdi = row[1].ToString();
                            item.DepoMiktar = Convert.ToDecimal(row[2]);
                            item.TedarikSiparisMiktar = Convert.ToDecimal(row[3]);
                            item.TedarikTalepMiktar = Convert.ToDecimal(row[4]);

                            item.PlanAdi = dataTable.Columns[i].ColumnName;

                            if (row[i] == DBNull.Value || row[i] == null)
                                item.HamIhtiyacMiktar = 0; // Set a default value when the database value is DBNull or null

                            else
                                item.HamIhtiyacMiktar = Convert.ToDecimal(row[i]);


                            if (depoMiktar > 0)
                            {
                                depoMiktar = depoMiktar - item.HamIhtiyacMiktar;
                                if (depoMiktar < 0)
                                {
                                    siparisMiktar = siparisMiktar - Math.Abs(depoMiktar);
                                }
                            }
                            if (siparisMiktar > 0 && depoMiktar < 0)
                                siparisMiktar = siparisMiktar - item.HamIhtiyacMiktar;
                            if (siparisMiktar < 0)
                            {
                                talepMiktar = talepMiktar - Math.Abs(siparisMiktar);
                            }
                            if (talepMiktar > 0 && depoMiktar < 0 && siparisMiktar < 0)
                                talepMiktar = talepMiktar - item.HamIhtiyacMiktar;

                            if (depoMiktar > 0)
                            {
                                item.Renk = "Green";
                                temp_coll_renklendir.Add(item);
                                continue;
                            }
                            if (siparisMiktar > 0)
                            {
                                item.Renk = "Yellow";
                                temp_coll_renklendir.Add(item);
                                continue;
                            }
                            if (talepMiktar > 0)
                            {
                                item.Renk = "Orange";
                                temp_coll_renklendir.Add(item);
                                continue;
                            }
                            if (item.HamIhtiyacMiktar > 0)
                                item.Renk = "Brown";
                            else
                                item.Renk = "Green";

                            temp_coll_renklendir.Add(item);
                        }
                    }

					RenklendirCollection = temp_coll_renklendir;

					return RenklendirCollection;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Planlama> GetPlanAdiDetayWithOnlyPlanAdi(Cls_Planlama planDetay, string simulasyonTip)
        {
            try
            {
                if (simulasyonTip == "Simülasyon")
                    Variables.Query_ = "Select * from sipmamul where plan_adi = @planAdi ";
                if (simulasyonTip == "Simülasyon Dosemeli")
                    Variables.Query_ = "Select * from sipmamuldos where plan_adi = @planAdi ";
                if (simulasyonTip == "Simülasyon Sunta")
                    Variables.Query_ = "Select * from vbtSimulasyonSunta where plan_adi = @planAdi ";
                if (simulasyonTip == "Ahsap Plan")
                    Variables.Query_ = "Select * from vatSimulasyon where planAdi = @planAdi ";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 800);
                parameters[0].Value = planDetay.PlanAdi;


                temp_coll_planlama.Clear();
                if (variables.Fabrika == "Vita")
                {
                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {
                        if (!reader.HasRows)
                            return null;

                        while (reader.Read())
                        {
                            Cls_Planlama item = new Cls_Planlama()
                            {
                                PlanAdiSira = Convert.ToInt32(reader["SIRA_NO"]),
                                PlanAdi = reader["PLAN_ADI"].ToString(),
                                SiparisNumarasi = reader["SIP_NO"].ToString(),
                                SiparisSira = Convert.ToInt32(reader["SIP_SIRA"]),
                                UrunKodu = reader["MAMUL_KODU"].ToString(),
                                UrunAdi = reader["MAMUL_ADI"].ToString(),
                                SiparisMiktar = Convert.ToInt32(reader["SIPMIKTAR"]),
                            };
                            temp_coll_planlama.Add(item);
                        }
                    }
                }
                if (variables.Fabrika == "Ahşap")
                {
                    using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {
                        if (!reader.HasRows)
                            return null;

                        while (reader.Read())
                        {
                            Cls_Planlama item = new Cls_Planlama()
                            {
                                PlanAdiSira = Convert.ToInt32(reader["PlanAdiSira"]),
                                PlanAdi = reader["PlanAdi"].ToString(),
                                SiparisNumarasi = reader["SiparisNumarasi"].ToString(),
                                SiparisSira = Convert.ToInt32(reader["SiparisSira"]),
                                UrunKodu = reader["UrunKodu"].ToString(),
                                UrunAdi = reader["UrunAdi"].ToString(),
                                SiparisMiktar = Convert.ToInt32(reader["SiparisMiktar"]),
                            };
                            temp_coll_planlama.Add(item);
                        }
                    }
                }
                PlanlamaCollection = temp_coll_planlama;
                return PlanlamaCollection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Planlama> GetPlanAdiDetay(Cls_Planlama planDetay, string simulasyonTip)
        {
            try
            {
				if(simulasyonTip == "Simülasyon")
					Variables.Query_ = "Select * from sipmamul where plan_adi = @planAdi and sira_no = @planSira";
				if(simulasyonTip == "Simülasyon Dosemeli")
					Variables.Query_ = "Select * from sipmamuldos where plan_adi = @planAdi and sira_no = @planSira";
				if(simulasyonTip == "Simülasyon Sunta")
					Variables.Query_ = "Select * from vbtSimulasyonSunta where plan_adi = @planAdi and sira_no = @planSira";
				if(simulasyonTip == "Ahsap Plan")
					Variables.Query_ = "Select * from vatSimulasyon where planAdi = @planAdi and planAdiSira = @planSira";

                SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 800);
				parameters[0].Value = planDetay.PlanAdi;
                parameters[1] = new SqlParameter("@planSira", SqlDbType.Int);
				parameters[1].Value = planDetay.PlanAdiSira;


                temp_coll_planlama.Clear();
				if(variables.Fabrika == "Vita" && simulasyonTip != "Simülasyon Sunta")
				{ 
					using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameters,variables.Fabrika))
					{
						if (!reader.HasRows)
							return null;

						while(reader.Read()) 
						{ 
							Cls_Planlama item = new Cls_Planlama()
							{
								PlanAdiSira = Convert.ToInt32(reader["SIRA_NO"]),
								PlanAdi = reader["PLAN_ADI"].ToString(),
								SiparisNumarasi = reader["SIP_NO"].ToString(),
								SiparisSira = Convert.ToInt32(reader["SIP_SIRA"]),
								UrunKodu = reader["MAMUL_KODU"].ToString(),
								UrunAdi = reader["MAMUL_ADI"].ToString(),
								SiparisMiktar = Convert.ToInt32(reader["SIPMIKTAR"]),
							};
							temp_coll_planlama.Add(item);
						}
					}
                }
				if(variables.Fabrika == "Vita" && simulasyonTip == "Simülasyon Sunta")
				{ 
					using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameters,variables.Fabrika))
					{
						if (!reader.HasRows)
							return null;

						while(reader.Read()) 
						{ 
							Cls_Planlama item = new Cls_Planlama()
							{
								PlanAdiSira = Convert.ToInt32(reader["SIRA_NO"]),
								PlanAdi = reader["PLAN_ADI"].ToString(),
								UrunKodu = reader["MAMUL_KODU"].ToString(),
								UrunAdi = reader["MAMUL_ADI"].ToString(),
								SiparisMiktar = Convert.ToInt32(reader["SIPMIKTAR"]),
							};
							temp_coll_planlama.Add(item);
						}
					}
                }
				if(variables.Fabrika == "Ahşap")
				{ 
					using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameters,variables.Fabrika))
					{
						if (!reader.HasRows)
							return null;

						while(reader.Read()) 
						{ 
							Cls_Planlama item = new Cls_Planlama()
							{
								PlanAdiSira = Convert.ToInt32(reader["PlanAdiSira"]),
								PlanAdi = reader["PlanAdi"].ToString(),
								SiparisNumarasi = reader["SiparisNumarasi"].ToString(),
								SiparisSira = Convert.ToInt32(reader["SiparisSira"]),
								UrunKodu = reader["UrunKodu"].ToString(),
								UrunAdi = reader["UrunAdi"].ToString(),
								SiparisMiktar = Convert.ToInt32(reader["SiparisMiktar"]),
							};
							temp_coll_planlama.Add(item);
						}
					}
                }
                PlanlamaCollection = temp_coll_planlama;
                return PlanlamaCollection;
            }
            catch
            {
                return null;
            }
        }
		public async Task<ObservableCollection<Cls_Planlama>> GetMamulHammaddeIhtiyacDurum(string mamulKodu)
		{
			try
			{

				if (string.IsNullOrEmpty(mamulKodu))
					return new ObservableCollection<Cls_Planlama>();

				Variables.Query_ = "vbpSimulationForSingleProduct";
				SqlParameter[] parameter = new SqlParameter[1];
				parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 15);
				parameter[0].Value = mamulKodu;

				ObservableCollection<Cls_Planlama> mamulCollection = await data.ExecuteStoredProcedureWithParametersAsyncReturnsCollection
																		("vbpSimulationForSingleProduct", Variables.Yil_,parameter, variables.Fabrika, reader =>
                                                                        {
                                                                            Cls_Planlama model = new();
                                                                            model.UrunKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                                                                            model.UrunAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                                                                            model.HamKodu = reader["HamKodu"] is DBNull ? "" : reader["HamKodu"].ToString();
                                                                            model.HamAdi = reader["HamAdi"] is DBNull ? "" : reader["HamAdi"].ToString();
																			model.HamIhtiyacMiktarBirim = reader["BirimIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["BirimIhtiyac"]);
																			model.HamIhtiyacMiktar = reader["ToplamIhtiyac"] is DBNull ? 0 : Convert.ToDecimal(reader["ToplamIhtiyac"]);
																			model.DepoMiktar = reader["DepoMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["DepoMiktar"]);
																			model.TedarikTalepMiktar = reader["TalepMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["TalepMiktar"]);
																			model.TedarikSiparisMiktar = reader["SiparisMiktar"] is DBNull ? 0 : Convert.ToDecimal(reader["SiparisMiktar"]);
																			model.Renk = reader["RenkDurum"] is DBNull ? "" : reader["RenkDurum"].ToString();
                                                                            
																			return model;
                                                                        });

				if(!Variables.Result_)
					return new ObservableCollection<Cls_Planlama>();


            }
            catch 
            {
				return new ObservableCollection<Cls_Planlama>();

            }

            return null;
		}
        public async Task<int> Simulasyon(string simulasyonTipi)
			{
				try
				{
					if(simulasyonTipi=="Simülasyon")
						variables.Result = await data.ExecuteStoredProcedureAsync("vbpSimulasyonIhtiyacHesaplaModuler", Variables.Yil_);
					if(simulasyonTipi=="Simülasyon Dosemeli")
						variables.Result = await data.ExecuteStoredProcedureAsync("vbpSimulasyonIhtiyacHesaplaDosemeli", Variables.Yil_);
					
					if(simulasyonTipi=="Simülasyon Sunta")
						variables.Result = await data.ExecuteStoredProcedureAsync("vbpSimulasyonSunta", Variables.Yil_);
					
					if(simulasyonTipi=="Ahsap Plan")
						variables.Result = await data.ExecuteStoredProcedureAsync("vbpSimulasyonAhsapPlan", Variables.Yil_,variables.Fabrika);

						if (!variables.Result)
							return 2;

					if(simulasyonTipi=="Simülasyon" || simulasyonTipi == "Simülasyon Dosemeli")
					{
						if (simulasyonTipi == "Simülasyon")
                        Variables.Query_ = "select distinct PLAN_ADI from sipmamul";
						if (simulasyonTipi == "Simülasyon Dosemeli")
                        Variables.Query_ = "select distinct PLAN_ADI from sipmamuldos";

                    temp_coll_planlama.Clear();
						using(SqlDataReader reader = await data.Select_Command_Data_ReaderAsync(Variables.Query_,Variables.Yil_)) 
						{
							if(!reader.HasRows)
								return 3;

							while(reader.Read()) 
							{
								Cls_Planlama plan = new Cls_Planlama
								{
									PlanAdi = reader[0].ToString(),
								};
								temp_coll_planlama.Add(plan);
							}
						}

						//foreach (Cls_Planlama item in temp_coll_planlama)
						//{
						//	SqlParameter[] parameter = new SqlParameter[2];
						//	parameter[0] = new SqlParameter("@planName", SqlDbType.NVarChar, 255);
						//	parameter[0].Value = item.PlanAdi;
						//	parameter[1] = new SqlParameter("@simulationType", SqlDbType.NVarChar, 100);
						//	parameter[1].Value = simulasyonTipi;
						//	variables.Result = await data.ExecuteStoredProcedureWithParametersAsync("vbpCreateSimulationTables", Variables.Yil_, parameter);

						//	if (!variables.Result)
						//		return 4;
						//}
					}
                return 1;
				}
				catch (Exception)
				{
					return -1;
				}
			}
		public ObservableCollection<Cls_Planlama> GetPlanRenkDurum(ObservableCollection<Cls_Planlama> renkDurumBelirle)
		{
			try
			{
				var distinctPlanAdi = renkDurumBelirle.Select(p => p.PlanAdi).Distinct().ToList();

				ObservableCollection<Cls_Planlama> planRenk = new();

				foreach(String item in distinctPlanAdi)
				{
					Cls_Planlama planRenkDurum = new();
					planRenkDurum.PlanAdi = item;

					var distinctColor = renkDurumBelirle.Where(p => p.PlanAdi == item).Distinct().ToList();

					if (distinctColor.Where(r => r.Renk == "Brown").Any())
					{ planRenkDurum.Renk = "Brown"; planRenk.Add(planRenkDurum); continue; }
                    if (distinctColor.Where(r => r.Renk == "Orange").Any())
                    { planRenkDurum.Renk = "Orange"; planRenk.Add(planRenkDurum); continue; }
                    if (distinctColor.Where(r => r.Renk == "Yellow").Any())
                    { planRenkDurum.Renk = "Yellow"; planRenk.Add(planRenkDurum); continue; }

					 planRenkDurum.Renk = "Green"; planRenk.Add(planRenkDurum); 
                };

                return planRenk;
			}
			catch (Exception)
			{
				return null;
			}
		}
        public DataTable GetDataForExcelFromSimulatedTable(string simulasyonTip)
        {
			if(simulasyonTip == "Simulasyon Sunta")
				Variables.Query_ = "select * from vbvSimulasyonSuntaDurum";
			if(simulasyonTip == "Ahsap Plan")
                Variables.Query_ = "SELECT sabit.stok_kodu as hamKodu,sabit.stok_adi as hamAdi,ISNULL(ph1.miktar, 0) as depoAdet,ISNULL(cast(sip.miktar as float), 0) as siparisMiktar,ISNULL(cast(talep.miktar as float), 0) as talepMiktar,sim.* FROM siparistablosuAhsap sim LEFT JOIN TBLSTSABIT sabit (NOLOCK) ON sim.ham_kodu = sabit.STOK_KODU LEFT JOIN (SELECT stok_kodu, ISNULL(CAST(top_giris_mik AS FLOAT), 0) - ISNULL(CAST(TOP_CIKIS_MIK AS FLOAT), 0) as miktar FROM TBLSTOKPH (NOLOCK) where DEPO_KODU=1) ph1 ON sim.ham_kodu = ph1.STOK_KODU LEFT JOIN (SELECT a.stok_kodu, SUM(ISNULL(a.sthar_gcmik, 0)) - SUM(ISNULL(a.firma_dovtut, 0)) - SUM(ISNULL(b.FIRMA_DOVTUT, 0)) as miktar FROM TBLTEKLIFTRA a (NOLOCK) LEFT JOIN TBLSIPATRA b ON a.stok_kodu = b.stok_kodu AND a.fisno = b.AMBAR_KABULNO WHERE a.STHAR_GCMIK > a.FIRMA_DOVTUT AND b.sthar_gcmik - b.FIRMA_DOVTUT > 0 AND a.STHAR_HTUR <> 'K' GROUP BY a.STOK_KODU) talep ON sim.ham_kodu = talep.stok_kodu LEFT JOIN (SELECT stok_kodu, SUM(ISNULL(sthar_gcmik, 0)) - SUM(ISNULL(firma_dovtut, 0)) as miktar FROM TBLSIPATRA (NOLOCK) WHERE STHAR_GCMIK > FIRMA_DOVTUT AND STHAR_HTUR <> 'K'  GROUP BY STOK_KODU) sip ON sim.ham_kodu = sip.stok_kodu; ";
			
			if(simulasyonTip == "Simülasyon")
                Variables.Query_ = "SELECT sabit.stok_kodu as hamKodu,sabit.stok_adi as hamAdi,ISNULL(ph1.miktar, 0) as depoAdet,ISNULL(cast(sip.miktar as float), 0) as siparisMiktar,ISNULL(cast(talep.miktar as float), 0) as talepMiktar,sim.* FROM siparistablosuMod sim LEFT JOIN TBLSTSABIT sabit (NOLOCK) ON sim.ham_kodu = sabit.STOK_KODU LEFT JOIN (SELECT stok_kodu, ISNULL(CAST(top_giris_mik AS FLOAT), 0) - ISNULL(CAST(TOP_CIKIS_MIK AS FLOAT), 0) as miktar FROM TBLSTOKPH (NOLOCK) where DEPO_KODU=30) ph1 ON sim.ham_kodu = ph1.STOK_KODU LEFT JOIN (SELECT a.stok_kodu, SUM(ISNULL(a.sthar_gcmik, 0)) - SUM(ISNULL(a.firma_dovtut, 0)) - SUM(ISNULL(b.FIRMA_DOVTUT, 0)) as miktar FROM TBLTEKLIFTRA a (NOLOCK) LEFT JOIN TBLSIPATRA b ON a.stok_kodu = b.stok_kodu AND a.fisno = b.AMBAR_KABULNO WHERE a.STHAR_GCMIK > a.FIRMA_DOVTUT AND b.sthar_gcmik - b.FIRMA_DOVTUT > 0 AND a.STHAR_HTUR <> 'K' GROUP BY a.STOK_KODU) talep ON sim.ham_kodu = talep.stok_kodu LEFT JOIN (SELECT stok_kodu, SUM(ISNULL(sthar_gcmik, 0)) - SUM(ISNULL(firma_dovtut, 0)) as miktar FROM TBLSIPATRA (NOLOCK) WHERE STHAR_GCMIK > FIRMA_DOVTUT AND STHAR_HTUR <> 'K'  GROUP BY STOK_KODU) sip ON sim.ham_kodu = sip.stok_kodu; ";
			
			if(simulasyonTip == "Simülasyon Dosemeli")
                Variables.Query_ = "SELECT sabit.stok_kodu as hamKodu,sabit.stok_adi as hamAdi,ISNULL(ph1.miktar, 0) as depoAdet,ISNULL(cast(sip.miktar as float), 0) as siparisMiktar,ISNULL(cast(talep.miktar as float), 0) as talepMiktar,sim.* FROM siparistablosuDos sim LEFT JOIN TBLSTSABIT sabit (NOLOCK) ON sim.ham_kodu = sabit.STOK_KODU LEFT JOIN (SELECT stok_kodu, ISNULL(CAST(top_giris_mik AS FLOAT), 0) - ISNULL(CAST(TOP_CIKIS_MIK AS FLOAT), 0) as miktar FROM TBLSTOKPH (NOLOCK) where DEPO_KODU=10) ph1 ON sim.ham_kodu = ph1.STOK_KODU LEFT JOIN (SELECT a.stok_kodu, SUM(ISNULL(a.sthar_gcmik, 0)) - SUM(ISNULL(a.firma_dovtut, 0)) - SUM(ISNULL(b.FIRMA_DOVTUT, 0)) as miktar FROM TBLTEKLIFTRA a (NOLOCK) LEFT JOIN TBLSIPATRA b ON a.stok_kodu = b.stok_kodu AND a.fisno = b.AMBAR_KABULNO WHERE a.STHAR_GCMIK > a.FIRMA_DOVTUT AND b.sthar_gcmik - b.FIRMA_DOVTUT > 0 AND a.STHAR_HTUR <> 'K' GROUP BY a.STOK_KODU) talep ON sim.ham_kodu = talep.stok_kodu LEFT JOIN (SELECT stok_kodu, SUM(ISNULL(sthar_gcmik, 0)) - SUM(ISNULL(firma_dovtut, 0)) as miktar FROM TBLSIPATRA (NOLOCK) WHERE STHAR_GCMIK > FIRMA_DOVTUT AND STHAR_HTUR <> 'K'  GROUP BY STOK_KODU) sip ON sim.ham_kodu = sip.stok_kodu; ";

            var dataTable = new DataTable();

            dataTable.Columns.Add("Ham Kodu");
            dataTable.Columns.Add("Ham Adı");
            dataTable.Columns.Add("Depo Adet");
            dataTable.Columns.Add("Sipariş Miktar");
            dataTable.Columns.Add("Talep Miktar");

            using (SqlDataReader reader = data.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_,variables.Fabrika))
			{
				if (reader != null && reader.HasRows)
				{
					string columnName = string.Empty;
					while (reader.Read())
					{
                        var dataRow = dataTable.NewRow();

                        dataRow["Ham Kodu"] = reader[0].ToString();
                        dataRow["Ham Adı"] = reader[1].ToString();
                        dataRow["Depo Adet"] = Convert.ToDecimal(reader[2]);
                        dataRow["Sipariş Miktar"] = Convert.ToDecimal(reader[3]);
                        dataRow["Talep Miktar"] = Convert.ToDecimal(reader[4]);

						for (int i = 6; i < reader.FieldCount; i++)
						{
							columnName = reader.GetName(i);

							if (dataTable.Rows.Count == 0)
								dataTable.Columns.Add(columnName);

                            if (reader[i] == DBNull.Value || reader[i] == null)
								dataRow[columnName] = 0;

							else
								dataRow[columnName] = Convert.ToDecimal(reader[i]);
						}

                        dataTable.Rows.Add(dataRow);
                    }
                
				}
			}
            return dataTable;
        }
        public List<string> GetKod1()
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
        public List<string> GetPlanNo()
        {
            try
            {
                Variables.Query_ = "Select Distinct kt_planno from tblisemriek (nolock) order by kt_planno asc";
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
        public List<string> GetPlanAdiForPlanNo()
        {
            try
            {
                if (login.GetDepartment() == "Ahsap Planlama")
					Variables.Query_ = "select distinct PlanAdi from vatsimulasyon where plan_no is not null or planno<>''";
                if (login.GetDepartment() == "Doseme Planlama")
                    Variables.Query_ = "select distinct plan_adi from sipmamuldos where plan_no is not null or planno<>''";
                if (login.GetDepartment() == "Moduler Planlama")
                    Variables.Query_ = "select distinct plan_adi from sipmamul where plan_no is not null or planno<>''";
                if (login.GetDepartment() == "Bilgi Islem" ||
                    login.GetDepartment() == "Yonetim")
                    Variables.Query_ = "select distinct plan_adi from sipmamul where plan_no is not null or plan_no<>''" +
										"union" +
                                        "select distinct plan_adi from sipmamuldos where plan_no is not null or plan_no<>''";
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
        public List<string> GetPlanAdiRelatedPlanNos(string planAdi)
		{
			try
			{
				if (string.IsNullOrEmpty(planAdi))
					return null;

				Variables.Query_ = "select distinct isnull(kt_planno,'') as PlanNo from tblisemriek (nolock) where kt_alan1 = @planAdi ";
				SqlParameter[] parameter = new SqlParameter[1];
				parameter[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
				parameter[0].Value = planAdi;

				List<string> result = new();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter, Variables.Fabrika_))
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
			catch (Exception)
			{
				return null;
			}
        }
        public List<string> GetPlanNoRelatedPlanAdis(string planNo)
        {
            try
            {
                if (string.IsNullOrEmpty(planNo))
                    return null;

                Variables.Query_ = "select distinct isnull(kt_alan1,'') as PlanAdi from tblisemriek (nolock) where kt_planno = @planNo ";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
                parameter[0].Value = planNo;

                List<string> result = new();
                using (SqlDataReader reader = data.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_,parameter, Variables.Fabrika_))
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
            catch (Exception)
            {
                return null;
            }
        }
        public int ReOrderPlanSira (ObservableCollection<Cls_Planlama> planlar,string simulasyonTipi)
		{
			try
			{
				if (planlar.Count == 0)
					return 2;

				foreach (Cls_Planlama plan in planlar)
				{
                    SqlParameter[] parameters = new SqlParameter[3];

                    parameters[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 500);
                    parameters[0].Value = plan.PlanAdi;
                    parameters[1] = new SqlParameter("@planSira", SqlDbType.Int);
                    parameters[1].Value = plan.PlanAdiSira;
                    parameters[2] = new SqlParameter("@eskiPlanAdiSira", SqlDbType.Int);
                    parameters[2].Value = plan.EskiPlanAdiSira;

					if(simulasyonTipi == "Ahşap")
					Variables.Query_ = "update vatSimulasyon set planAdiSira=@planSira, planAdi=@planAdi where planAdiSira=@eskiPlanAdiSira";
					if(simulasyonTipi == "Simülasyon")
					Variables.Query_ = "update sipmamul set sira_no=@planSira where plan_adi=@planAdi";
					if(simulasyonTipi == "Simülasyon Dosemeli")
					Variables.Query_ = "update sipmamuldos set sira_no=@planSira where plan_adi=@planAdi";

                    variables.Result = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters,variables.Fabrika);

					if (!variables.Result)
						return 3;
                }

                return 1;
            }
			catch 
			{
				return -1;
			}
		}
		public async Task<bool> DeleteRefisemriAsync(ObservableCollection<Cls_Planlama> isemriCollection)
		{
			try
			{
				if (isemriCollection == null)
					return false;
				if (isemriCollection.Count == 0)
					return false;

				foreach(Cls_Planlama isemri in isemriCollection) 
				{ 
				
					SqlParameter[] parameter = new SqlParameter[1];
					parameter[0] = new SqlParameter("@refisemrino", SqlDbType.NVarChar, 15);
					parameter[0].Value = isemri.ReferansIsemrino;
					
					Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync("vbpDeleteRefIsemri",Variables.Yil_,parameter,Variables.Fabrika_);

					if (!Variables.Result_)
						return false;
						
				}
                return Variables.Result_;
			}
			catch 
			{
				return false;
			}
		}
		public bool DeleteIsemri(ObservableCollection<Cls_Planlama> isemriCollection, string fabrika)
		{
			try
			{
				foreach(Cls_Planlama isemri in isemriCollection) 
				{ 
				
					SqlParameter[] parameters = new SqlParameter[3];
					parameters[0] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
					parameters[1] = new SqlParameter("@siparisSira", SqlDbType.Int);
					parameters[2] = new SqlParameter("@urunKodu", SqlDbType.NVarChar,35);
					parameters[0].Value = isemri.SiparisNumarasi;
					parameters[1].Value = isemri.SiparisSira;
					parameters[2].Value = isemri.UrunKodu;
					
					variables.Result = data.ExecuteStoredProcedureWithParameters("vbpDeleteIsemri",Variables.Yil_,parameters,fabrika);

					if (!variables.Result)
						return false;
						
				}
                return variables.Result;
			}
			catch 
			{
				return false;
			}
		}
        public bool InsertPlanNo(ObservableCollection<Cls_Planlama> isemriCollection, bool isExisting)
		{
			try
			{
				if(isemriCollection == null) { return false; }
				if(isemriCollection.Count == 0) { return false; }

				Variables.Query_ = "select top 1  KT_PLANNO from TBLISEMRIEK where KT_PLANNO like 'p%' order by KT_PLANNO desc";

				string lastPlanNo = data.Get_One_String_Result_Command(Variables.Query_, Variables.Yil_);

                Variables.Query_ = " select dbo.vbfGetNextRefNo('"+ lastPlanNo + "', 'P')";

				string planNo = data.Get_One_String_Result_Command(Variables.Query_, Variables.Yil_);

				foreach(Cls_Planlama item in isemriCollection)
				{
					SqlParameter[] parameters = new SqlParameter[3];
					parameters[0] = new SqlParameter("refisemrino", SqlDbType.NVarChar,35);
					parameters[0].Value = item.ReferansIsemrino;
					parameters[1] = new SqlParameter("planAdi", SqlDbType.NVarChar,400);
					parameters[1].Value = item.PlanAdi;
					parameters[2] = new SqlParameter("planNo", SqlDbType.NVarChar,100);
					parameters[2].Value = isExisting == true ? item.PlanNo : planNo;

					Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertPlanNo", Variables.Yil_, parameters);
					if(!Variables.Result_)
					{
						PlanNoCallBack(item.ReferansIsemrino);
                        return false; 
					}
				}
				return true;

			}
			catch 
			{
				return false;
			}
        }
        public bool InsertTakipNo(ObservableCollection<Cls_Planlama> isemriCollection, bool isExisting, string mevcutTakip,bool isManuel)
        {
            try
            {
                if (isemriCollection == null) { return false; }
                if (isemriCollection.Count == 0) { return false; }

				string takipNoFirstPart = string.Empty;
                string takipNoSecondPart = string.Empty;
				
                Variables.Counter_ = 0;
				if(!isExisting)
                {
                    takipNoFirstPart = GetNextTakipNo(mevcutTakip).Substring(0, 5);
					if (takipNoFirstPart == "HATA")
						return false;
					takipNoSecondPart = "001";
                }
				else if(isExisting && isManuel)
				{
					takipNoFirstPart = mevcutTakip.Substring(0, 5);
					takipNoSecondPart = mevcutTakip.Substring(5, 3);
				}
				else if (isExisting && !isManuel)
				{
					takipNoFirstPart = mevcutTakip;
					takipNoSecondPart = GetExistingNextTakipNoSira(mevcutTakip);

					if (takipNoSecondPart == "HATA")
						return false;
				}
				bool isFirst = true;

                string takipNo = string.Empty;
                foreach (Cls_Planlama item in isemriCollection)
                {

					if (isFirst)
					{
						takipNo = string.Format("{0}{1}", takipNoFirstPart, takipNoSecondPart);
						isFirst = false;
					}
					else
						takipNo = GetNextTakipNoSira(takipNo);

                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@refisemrino", SqlDbType.NVarChar, 15);
                    parameters[0].Value = item.ReferansIsemrino;
                    parameters[1] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 50);
                    parameters[1].Value = takipNo;

					Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertTakipNo", Variables.Yil_, parameters,Variables.Fabrika_);
					takipNoSecondPart = string.Empty;
				
					if (!Variables.Result_)
                    {
                        TakipNoCallBack(item.ReferansIsemrino);
                        return false;
                    }
                }
                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<int> InsertTalepPlanaBagliAsync(ObservableCollection<Cls_Planlama> talepCollection)
        {
            try
            {

					if (talepCollection == null) return 2;
					if (talepCollection.Count == 0) return 3;

					Variables.Query_ = "vbpGetFisno";
					SqlParameter[] parameterFis = new SqlParameter[3];
					parameterFis[0] = new SqlParameter("@prefix", SqlDbType.NVarChar, 4);
					parameterFis[1] = new SqlParameter("@tableName", SqlDbType.NVarChar, 128);
					parameterFis[2] = new SqlParameter("@columnName", SqlDbType.NVarChar, 128);
					parameterFis[0].Value = "TLP";
					parameterFis[1].Value = "TBLTEKLIFMAS";
					parameterFis[2].Value = "FATIRS_NO";
				

                string fisno = await data.Get_One_String_Result_Stored_Proc_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameterFis, Variables.Fabrika_);
                if (string.IsNullOrEmpty(fisno) || fisno == "STRING HATA")
                    return 6;


                Variables.Query_ = "vbpInsertTeklifMasPlanaBagli";
                SqlParameter[] parametersMas = new SqlParameter[5];
                parametersMas[0] = new SqlParameter("@kalemAdedi", SqlDbType.Int);
                parametersMas[1] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                parametersMas[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parametersMas[3] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parametersMas[4] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
                parametersMas[0].Value = talepCollection.Count();
                parametersMas[1].Value = talepCollection.Select(i => i.TalepGenelAciklama).FirstOrDefault();
                parametersMas[2].Value = login.GetUserName();
                parametersMas[3].Value = fisno;
                parametersMas[4].Value = talepCollection.Select(p=>p.PlanNo).FirstOrDefault();


                Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parametersMas, Variables.Fabrika_);
                if (!Variables.Result_)
				{
					TalepGirisKayitlariGeriAlAsync(fisno);
                    return 4;
                }
				int depoKodu = 0;
				if (Variables.Departman_.Contains("Moduler"))
					depoKodu = 30;
				else
					depoKodu = 10;

				foreach(Cls_Planlama item in talepCollection)
				{
                    ObservableCollection<Cls_Planlama> hamDetayCollection = await PopulatePlanaBagliHammaddeDetayAsync(item.PlanAdi, item.HamKodu);
					item.SiparisNumarasi = hamDetayCollection.Select(s => s.SiparisNumarasi).FirstOrDefault() == null ? "" : hamDetayCollection.Select(s => s.SiparisNumarasi).FirstOrDefault();
					item.SiparisSira = hamDetayCollection.Select(s => s.SiparisSira).FirstOrDefault() == null ? 0 : hamDetayCollection.Select(s => s.SiparisSira).FirstOrDefault();

                    Variables.Query_ = "vbpInsertTeklifTraPlanaBagli";
               
                        SqlParameter[] parameters = new SqlParameter[11];
                        parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                        parameters[2] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                        parameters[3] = new SqlParameter("@miktar", SqlDbType.Decimal);
                        parameters[4] = new SqlParameter("@terminTarih", SqlDbType.DateTime);
                        parameters[5] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                        parameters[6] = new SqlParameter("@planNo", SqlDbType.NVarChar, 15);
                        parameters[7] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                        parameters[8] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                        parameters[9] = new SqlParameter("@siparisSira", SqlDbType.Int);
                        parameters[10] = new SqlParameter("@depoKodu", SqlDbType.Int);
                        parameters[0].Value = fisno;
                        parameters[1].Value = item.TalepSira;
                        parameters[2].Value = item.HamKodu;
                        parameters[3].Value = item.TedarikTalepMiktar;
                        parameters[4].Value = item.TerminTarih;
                        parameters[5].Value = item.TalepAciklama;
                        parameters[6].Value = item.PlanNo;
                        parameters[7].Value = item.PlanAdi;
                        parameters[8].Value = item.SiparisNumarasi;
                        parameters[9].Value = item.SiparisSira;
                        parameters[10].Value = item.DepoKodu;
                        Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                        if (!Variables.Result_)
                        {
                            TalepGirisKayitlariGeriAlAsync(fisno);
                            return 5;
                        }
                    
                }

                Variables.Query_ = "vbpUpdateTalepHesaplat";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;
                Variables.Result_ = await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                if (!Variables.Result_)
                {
                    TalepGirisKayitlariGeriAlAsync(fisno);
                    return 7;
                }


                return 1;
            }
            catch
            {
                return -1;
            }
        }
		private string GetNextTakipNo()
		{
			try
			{
				if(login.GetDepartment().Contains("Moduler") == true)
                    Variables.Query_ = "Select top 1 substring(kt_takipnum,1,5) from tblisemriek(nolock) where kt_takipnum like 'M%' order by kt_takipnum desc";
                else
					Variables.Query_ = "Select top 1 substring(kt_takipnum,1,5) from tblisemriek(nolock) where kt_takipnum like 'D%' order by kt_takipnum desc";

				string nextTakipNo = data.Get_One_String_Result_Command(Variables.Query_, Variables.Yil_);
				if (nextTakipNo == null)
					return "HATA";
				if (nextTakipNo == string.Empty)
					return "HATA";
                if (nextTakipNo == "STRING HATA")
                    return "HATA";

                if (string.IsNullOrEmpty(nextTakipNo))
                    return "HATA";
                if (nextTakipNo == "STRING HATA")
                    return "HATA";
                int nextTakipNoNumber = 0;
                if (!Int32.TryParse(nextTakipNo.Substring(1,4), out nextTakipNoNumber))
                    return "HATA";
                else
                    nextTakipNoNumber++;
				string nextTakipNoNumberString = nextTakipNoNumber.ToString();
				if(nextTakipNoNumberString.Length<4)
				{
					int zeroLength = 0;
					zeroLength = 4 - nextTakipNoNumberString.Length;
					nextTakipNoNumberString = nextTakipNoNumberString.PadLeft(zeroLength, '0');
                }
                nextTakipNo = string.Format("{0}{1}", nextTakipNo.Substring(0, 1), nextTakipNoNumberString);

                return nextTakipNo;

			}
			catch 
			{
				return "Hata";
			}
		}
		private string GetNextTakipNo(string selectedChar)
		{
			string nextTakipNo = string.Empty;
			Variables.Query_ = "select top 1 kt_takipnum from tblisemriek (nolock) where kt_takipnum like '%' + @mevcutTakip + '%' order by kt_takipnum desc";
			SqlParameter[] parameter = new SqlParameter[1];
			parameter[0] = new SqlParameter("@mevcutTakip", SqlDbType.NVarChar, 5);
			parameter[0].Value = selectedChar;
			nextTakipNo = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter);
			if (string.IsNullOrEmpty(nextTakipNo))
				return "HATA";
			if (nextTakipNo == "STRING HATA")
				return "HATA";
			int nextTakipNoNumber = 0;
			if (!Int32.TryParse(nextTakipNo.Substring(1,4), out nextTakipNoNumber))
				return "HATA";
			else
				nextTakipNoNumber++;
			nextTakipNo = string.Format("{0}{1}001", selectedChar, nextTakipNoNumber.ToString());

			return nextTakipNo;
		}
		private string GetNextTakipNoSira(string takipNo)
		{
			if(takipNo.Length == 5)
			{
				takipNo = string.Format("{0}{1}", takipNo, "001");
				return takipNo;
			}
            Variables.Counter_ = 0;
            int padLeft = 0;
			string takipNoSecondPart = string.Empty;
			string takipNoFirstPart = takipNo.Substring(0, 5);
			string nextTakipNo = string.Empty;
			

            for (int i = 1; i < 4; i++)
            {
                if (takipNo.Substring(4 + i, 1) != "0")
                {
                    if (takipNo.Substring(4 + i, 1) == "9")
                        padLeft = i - 2;
                    else
                        padLeft = i - 1;

                    Variables.Counter_ = i;
                    break;
                }
            }

            takipNoSecondPart = takipNoSecondPart.PadLeft(padLeft, '0');
            int takipNoNumber = Convert.ToInt32(takipNo.Substring(5, 3)) + 1;
            takipNoSecondPart = takipNoSecondPart + takipNoNumber.ToString();
			nextTakipNo = string.Format("{0}{1}", takipNoFirstPart, takipNoSecondPart);
			return nextTakipNo;

        }
        private string GetExistingNextTakipNoSira(string takipNo)
        {
            string nextTakipNo = string.Empty;
            if (Variables.Departman_.Contains("Moduler"))
				Variables.Query_ = "select top 1 kt_takipnum from tblisemriek where substring(kt_takipnum,1,5)=@takipNo and kt_takipnum like 'M%' order by kt_takipnum desc";
            else
                Variables.Query_ = "select top 1 kt_takipnum from tblisemriek where substring(kt_takipnum,1,5)=@takipNo and kt_takipnum like 'D%' order by kt_takipnum desc";

			SqlParameter[] parameter = new SqlParameter[1];
			parameter[0] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 8);
			parameter[0].Value = takipNo;

			nextTakipNo = data.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
			if (nextTakipNo == "STRING HATA")
				return "HATA";

            Variables.Counter_ = 0;
            int padLeft = 0;
            string takipNoSecondPart = string.Empty;
            
            for (int i = 1; i < 4; i++)
            {
                if (nextTakipNo.Substring(4 + i, 1) != "0")
                {
                    if (nextTakipNo.Substring(4 + i, 1) == "9")
                        padLeft = i - 2;
                    else
                        padLeft = i - 1;

                    Variables.Counter_ = i;
                    break;
                }
            }

            takipNoSecondPart = takipNoSecondPart.PadLeft(padLeft, '0');
            int takipNoNumber = Convert.ToInt32(nextTakipNo.Substring(5, 3)) + 1;
            takipNoSecondPart = takipNoSecondPart + takipNoNumber.ToString();
            return takipNoSecondPart;

        }
        public int CheckIfTakipNoExists(string takipNo)
        {
            try
            {
                Variables.Query_ = "Select Count(*) from tblisemriek(nolock) where kt_takipnum =@takipNo";

				SqlParameter[] parameter = new SqlParameter[1];
				parameter[0] = new SqlParameter("@takipNo", SqlDbType.NVarChar, 8);
				parameter[0].Value = takipNo;

                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter,Variables.Fabrika_);
				return Variables.ResultInt_;

            }
            catch
            {
                return -1;
            }
        }
        private async void TalepGirisKayitlariGeriAlAsync(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return;

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.Int);
                parameter[0].Value = fisno;

                Variables.Query_ = "vbpDeleteTalep";

               await data.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
            }
            catch
            {

                throw;
            }
        }
        public async Task<bool> TalepHesaplatAsync()
		{
			try
			{
				Variables.Departman_ = await login.GetDepartmentAsync();
				if(Variables.Departman_.Contains("Moduler"))
					Variables.Query_ = "vbpTalepHesaplatMod";
                else if (Variables.Departman_.Contains("Doseme"))
                    Variables.Query_ = "vbpTalepHesaplatDos";
				else
                    Variables.Query_ = "vbpTalepHesaplatDos";

                Variables.Result_ = await data.ExecuteStoredProcedureAsync(Variables.Query_,Variables.Yil_,Variables.Fabrika_);

				return Variables.Result_;
			}
			catch 
			{
				return false;
			}
		}
		private void PlanNoCallBack(string refisemrino)
		{
			if(string.IsNullOrEmpty(refisemrino))
				return;

			if (refisemrino.Length != 15)
				return;

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("refisemrino", SqlDbType.NVarChar, 35);
            parameters[0].Value = refisemrino;
          
            data.ExecuteStoredProcedureWithParameters("vbpPlanNoCallBack", Variables.Yil_, parameters);

        }
		public bool TakipNoCallBack(string refisemrino)
		{
			try
			{

				if(string.IsNullOrEmpty(refisemrino))
					return false;

				if (refisemrino.Length != 15)
					return false;

				SqlParameter[] parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("refisemrino", SqlDbType.NVarChar, 35);
				parameters[0].Value = refisemrino;
          
				Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpTakipNoCallBack", Variables.Yil_, parameters);
				return Variables.Result_;

            }
            catch
            {
				return false;
            }
        }
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string getStr)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(getStr));
		}
	
	}
}

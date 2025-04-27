using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using Layer_Data;
using Layer_2_Common.Type;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Linq;

namespace Layer_Business
{
    public class Cls_Urun : INotifyPropertyChanged
    {
        private string _urunTipi = string.Empty;

        public string UrunTipi
        {
            get { return _urunTipi; }
            set
            {
                _urunTipi = value;
                OnPropertyChanged(nameof(UrunTipi));
            }
        }

        private string _model = string.Empty;

        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
        private string _satisSekil = string.Empty;

        public string SatisSekil
        {
            get { return _satisSekil; }
            set
            {
                _satisSekil = value;
                OnPropertyChanged(nameof(SatisSekil));
            }
        }

        private string _urunKodu = string.Empty;

        public string UrunKodu
        {
            get { return _urunKodu; }
            set { _urunKodu = value;
                OnPropertyChanged(nameof(UrunKodu));
            }
        }

        private string _sablonKod = string.Empty;

        public string SablonKod
        {
            get { return _sablonKod; }
            set {
                _sablonKod = value;
                OnPropertyChanged(nameof(SablonKod));
            }
        }

        private string _urunAdi = string.Empty;

        public string UrunAdi
        {
            get { return _urunAdi; }
            set {
                _urunAdi = value;
                OnPropertyChanged(nameof(UrunAdi));
            }
        }
        private int _urunMiktar;

        public int UrunMiktar
        {
            get { return _urunMiktar; }
            set {
                _urunMiktar = value;
                OnPropertyChanged(nameof(UrunMiktar));
            }
        }

        private string _varyantVarMi = string.Empty;

        public string VaryantVarMi
        {
            get { return _varyantVarMi; }
            set {
                _varyantVarMi = value;
                OnPropertyChanged(nameof(VaryantVarMi));
            }
        }

        public string OzellikTipi { get; set; }

        private string _urunGrubuKodu = string.Empty;

        public string UrunGrubuKodu
        {
            get { return _urunGrubuKodu; }
            set { _urunGrubuKodu = value;
                OnPropertyChanged(nameof(_urunGrubuKodu));
            }
        }

        public int UrunGrubuSira { get; set; }

        private string _modelKodu = string.Empty;

        public string ModelKodu
        {
            get { return _modelKodu; }
            set {
                _modelKodu = value;
                OnPropertyChanged(nameof(ModelKodu));
            }
        }

        public string ModelIsim { get; set; } = string.Empty;
        public string UrunGrubuIsim { get; set; } = string.Empty;
        public int UrunGrubuTur { get; set; }
        public int TakimKodu { get; set; }
        public int UniteKod { get; set; }
        public string Kilit { get; set; } = string.Empty;
        public string ModelKilit { get; set; } = string.Empty;
        public string SatisSekilKilit { get; set; } = string.Empty;
        public string UrunGrupModelSatisSekil { get; set; } = string.Empty;
        public string Sayfa { get; set; } = string.Empty;
        public int Kdv { get; set; }
        public int Muhdetay { get; set; }
        public string MenuGrup { get; set; } = string.Empty;              
        public int TeslimGunu { get; set; }
        public string Kod1 { get; set; } = string.Empty;
        public string Kod2 { get; set; } = string.Empty;
        public string Kod3 { get; set; } = string.Empty;
        public string Kod4 { get; set; } = string.Empty;
        public string Kod5 { get; set; } = string.Empty;
        public string ModelKod1 { get; set; }  = string.Empty;
        public string ModelKod2 { get; set; }  = string.Empty;
        public string ModelKod3 { get; set; }  = string.Empty;
        public string ModelKod4 { get; set; }  = string.Empty;
        public string ModelKod5 { get; set; } = string.Empty;
        public string SatisSekilKod1 { get; set; }  = string.Empty;
        public string SatisSekilKod2 { get; set; }  = string.Empty;
        public string SatisSekilKod3 { get; set; }  = string.Empty;
        public string SatisSekilKod4 { get; set; }  = string.Empty;
        public string SatisSekilKod5 { get; set; } = string.Empty;

        public int ModelSira { get; set; }

        private string _satisSekilKodu = string.Empty;

        public string SatisSekilKodu
        {
            get { return _satisSekilKodu; }
            set {
                _satisSekilKodu = value;
                OnPropertyChanged(nameof(SatisSekilKodu));
            }
        }

        public string SatisSekilIsim { get; set; }
        public int SatisSekilSira { get; set; }

        private string _varyantIsmi = string.Empty;

        public string VaryantIsmi
        {
            get { return _varyantIsmi; }
            set {
                _varyantIsmi = value;
                OnPropertyChanged(nameof(VaryantIsmi));
            }
        }

        private string _varyantKodu = string.Empty;

        public string VaryantKodu
        {
            get { return _varyantKodu; }
            set {
                _varyantKodu = value;
                OnPropertyChanged(nameof(VaryantKodu));
            }
        }
        private string _varyantNo = string.Empty;

        public string VaryantNo
        {
            get { return _varyantNo; }
            set {
                _varyantNo = value;
                OnPropertyChanged(nameof(VaryantNo));
            }
        }

        private string _varyantTipi = string.Empty;

        public string VaryantTipi
        {
            get { return _varyantTipi; }
            set {
                _varyantTipi = value;
                OnPropertyChanged(nameof(VaryantTipi));
            }
        }
        private string _varyantSira = string.Empty;

        public string VaryantSira
        {
            get { return _varyantSira; }
            set {
                _varyantSira = value;
                OnPropertyChanged(nameof(VaryantSira));
            }
        }
        private string _ozKisit = string.Empty;

        public string OzKisit
        {
            get { return _ozKisit; }
            set {
                _ozKisit = value;
                OnPropertyChanged(nameof(OzKisit));
            }
        }

        private string _ozKisitIsim = string.Empty;

        public string OzKisitIsim
        {
            get { return _ozKisitIsim; }
            set {
                _ozKisitIsim = value;
                OnPropertyChanged(nameof(OzKisitIsim));
            }
        }

        private string _ingilizceIsimAnahtar = string.Empty;

        public string IngilizceIsimAnahtar
        {
            get { return _ingilizceIsimAnahtar; }
            set {
                _ingilizceIsimAnahtar = value;
                OnPropertyChanged(nameof(IngilizceIsimAnahtar));
            }
        }
        private string _ingilizceIsim = string.Empty;

        public string IngilizceIsim
        {
            get { return _ingilizceIsim; }
            set {
                _ingilizceIsim = value;
                OnPropertyChanged(nameof(IngilizceIsim));
            }
        }

        private string _isimAnahtar = string.Empty;

        public string IsimAnahtar
        {
            get { return _isimAnahtar; }
            set {
                _isimAnahtar = value;
                OnPropertyChanged(nameof(IsimAnahtar));
            }
        }
        public string OzellikIsmi { get; set; } = string.Empty;
        public string KisaKod { get; set; } = string.Empty;
        public int BaslangicSira { get; set; }
        public int KodUzunluk { get; set; }
        public string ReceteDegeri { get; set; }
        public int OzellikSayisi { get; set; }

        public string OpsiyonAciklama { get; set; } = string.Empty;
        public string Koddetay { get; set; }
        public string KoddetayIsim { get; set; }
        public string KoddetayIsimIng { get; set; }
        public int KoddetaySira{ get; set; }
        public int KisitSira { get; set; }
        public string KisitKod { get; set; }
        private string _query = string.Empty;

        public string Query
        {
            get { return _query; }
            set { _query = value; }
        }

        private bool isChecked = false;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }


        private ObservableCollection<Cls_Urun> _urunCollection;
        public ObservableCollection<Cls_Urun> UrunCollection
        {
            get { return _urunCollection; }
            set { _urunCollection = value; }
        }
        ObservableCollection<Cls_Urun> coll_urun = new ObservableCollection<Cls_Urun>();
        DataLayer dataLayer = new DataLayer();
        DataTable dataTable = new DataTable();
        SqlDataReader reader;
        DataRow firstRow;
        Object columnValueByIndex;
        Variables variables = new();
        LoginLogic login = new();


        public ObservableCollection<Cls_Urun> PopulateUrunListele(Dictionary<string, string> constraints)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvSiparisGirisArama where 1=1 ";

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["sablonKod"]))
                {
                    if (constraints["sablonKod"].Length > 11) 
                    { 
                        constraints["sablonKod"] = constraints["sablonKod"].Substring(0, 11);
					}
					Variables.Query_ = Variables.Query_ + "and stok_kodu like '%' + @sablonKod + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["urunAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stok_adi like '%' + @urunAdi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["urunTipi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urun_isim like '%' + @urunTipi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["model"]))
                {
                    Variables.Query_ = Variables.Query_ + "and model_isim like '%' + @model + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["satisSekil"]))
                {
                    Variables.Query_ = Variables.Query_ + "and ssekil_isim like '%' + @satisSekil + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;

                if (!string.IsNullOrWhiteSpace(constraints["sablonKod"]))
                {
                    parameters[variables.Counter] = new("@sablonKod", SqlDbType.NVarChar, 11);
                    parameters[variables.Counter].Value = constraints["sablonKod"];

					variables.Counter++;
				}

				if (!string.IsNullOrWhiteSpace(constraints["urunAdi"]))
				{
					parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 500);
					parameters[variables.Counter].Value = constraints["urunAdi"];

					variables.Counter++;
				}

				if (!string.IsNullOrWhiteSpace(constraints["urunTipi"]))
				{
					parameters[variables.Counter] = new("@urunTipi", SqlDbType.NVarChar, 500);
					parameters[variables.Counter].Value = constraints["urunTipi"];

					variables.Counter++;
				}

				if (!string.IsNullOrWhiteSpace(constraints["model"]))
				{
					parameters[variables.Counter] = new("@model", SqlDbType.NVarChar, 500);
					parameters[variables.Counter].Value = constraints["model"];

					variables.Counter++;
				}

				if (!string.IsNullOrWhiteSpace(constraints["satisSekil"]))
				{
					parameters[variables.Counter] = new("@satisSekil", SqlDbType.NVarChar, 500);
					parameters[variables.Counter].Value = constraints["satisSekil"];

					variables.Counter++;
				}

                //ahşap kalite giriyor ise sadece ürün isim planlamaları göster
                if (login.GetDepartment().Contains("Ahsap"))
                    Variables.Query_ += " and urun_isim='PLANLAMA' ";

				reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters);

                if(!reader.HasRows) { return null; }                

                coll_urun.Clear();

                while (reader.Read())
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        UrunKodu = reader["stok_kodu"].ToString(),
                        UrunAdi = reader["stok_adi"].ToString(),
                        UrunTipi = reader["urun_isim"].ToString(),
                        Model = reader["model_isim"].ToString(),
                        SatisSekil = reader["ssekil_isim"].ToString(),
                        UrunMiktar = 0,
                        VaryantVarMi = reader["VaryantVarMi"].ToString(),
                        UrunGrubuKodu = reader["urun_kodu"].ToString(),
                        ModelKodu = reader["model_kodu"].ToString(),
                        SatisSekilKodu = reader["ssekil_kodu"].ToString()
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;
                reader.Close();
                return UrunCollection;
            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Urun> PopulateUrunAdiGuncellenecekListele(Dictionary<string, string> constraints)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvVaryantUrunListele where 1=1 ";

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["urunKod"]))
                {
                    
                    Variables.Query_ = Variables.Query_ + "and stok_kodu like '%' + @urunKod + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["urunAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stok_adi like '%' + @urunAdi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["urunTipi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and urun_isim like '%' + @urunTipi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["model"]))
                {
                    Variables.Query_ = Variables.Query_ + "and model_isim like '%' + @model + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["satisSekil"]))
                {
                    Variables.Query_ = Variables.Query_ + "and ssekil_isim like '%' + @satisSekil + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["urunKod"]))
                {
                    parameters[variables.Counter] = new("@urunKod", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["urunKod"];

                    variables.Counter++;
                }
                if (!string.IsNullOrWhiteSpace(constraints["urunAdi"]))
                {
                    parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["urunAdi"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["urunTipi"]))
                {
                    parameters[variables.Counter] = new("@urunTipi", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["urunTipi"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["model"]))
                {
                    parameters[variables.Counter] = new("@model", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["model"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["satisSekil"]))
                {
                    parameters[variables.Counter] = new("@satisSekil", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["satisSekil"];

                    variables.Counter++;
                }

                reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters);

                if (reader == null) { return null; }

                coll_urun.Clear();

                while (reader.Read())
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        UrunKodu = reader["stok_kodu"].ToString(),
                        UrunAdi = reader["stok_adi"].ToString(),
                        UrunTipi = reader["urun_isim"].ToString(),
                        Model = reader["model_isim"].ToString(),
                        SatisSekil = reader["ssekil_isim"].ToString(),
                        UrunMiktar = 0,
                        VaryantVarMi = reader["VaryantVarMi"].ToString(),
                        UrunGrubuKodu = reader["urun_kodu"].ToString(),
                        ModelKodu = reader["model_kodu"].ToString(),
                        SatisSekilKodu = reader["ssekil_kodu"].ToString()
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;
                reader.Close();
                return UrunCollection;
            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Urun> PopulatePopUpVaryantSecimWindow()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                coll_urun.Clear();

                foreach (DataRow row in dataTable.Rows)
                {

                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        VaryantIsmi = row["ozisim"].ToString(),
                        VaryantKodu = row["ozkod"].ToString()
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;
                OnPropertyChanged(nameof(VaryantIsmi));

                return UrunCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> PopulateOzellikListe(string ozellikKisit)
        {
            try
            {
                if (ozellikKisit == "Ürün Grup")
                { 
                    Variables.Query_ = "Select * from stburungrup";

                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    UrunGrubuKodu = reader["kod"].ToString(),
                                    UrunGrubuIsim = reader["isim"].ToString(),
                                    UrunGrubuTur = Convert.ToInt32(reader["tur"]),
                                    TakimKodu = Convert.ToInt32(reader["takimkod"]),
                                    UniteKod = Convert.ToInt32(reader["unitekod"]),
                                    UrunGrubuSira = Convert.ToInt32(reader["sira"]),
                                    Kilit = reader["kilit"].ToString(),
                                    Sayfa = reader["sayfa"].ToString(),
                                    Kdv = Convert.ToInt32(reader["kdvo"]),
                                    Muhdetay = Convert.ToInt32(reader["muhdetay"]),
                                    MenuGrup = reader["menugrup"].ToString(),
                                    TeslimGunu = Convert.ToInt32(reader["teslimgunu"]),
                                    Kod1 = reader["kod1"].ToString(),
                                    Kod2 = reader["kod2"].ToString(),
                                    Kod3 = reader["kod3"].ToString(),
                                    Kod4 = reader["kod4"].ToString(),
                                    Kod5 = reader["kod5"].ToString(),
                                };

                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }
                if (ozellikKisit == "Model")
                {
                    Variables.Query_ = "Select * from stbmodel";

                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    ModelKodu = reader["kod"].ToString(),
                                    ModelIsim = reader["isim"].ToString(),
                                    ModelSira = Convert.ToInt32(reader["sira"]),
                                    ModelKilit = reader["kilit"].ToString(),
                                    ModelKod1 = reader["kod1"].ToString(),
                                    ModelKod2 = reader["kod2"].ToString(),
                                    ModelKod3 = reader["kod3"].ToString(),
                                    ModelKod4 = reader["kod4"].ToString(),
                                    ModelKod5 = reader["kod5"].ToString(),
                                };
                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }
                if (ozellikKisit == "Satış Şekil")
                {
                    Variables.Query_ = "Select * from stbsatissekil";

                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    SatisSekilKodu = reader["kod"].ToString(),
                                    SatisSekilIsim = reader["isim"].ToString(),
                                    SatisSekilSira = Convert.ToInt32(reader["sira"]),
                                    SatisSekilKilit = reader["kilit"].ToString(),
                                    SatisSekilKod1 = reader["kod1"].ToString(),
                                    SatisSekilKod2 = reader["kod2"].ToString(),
                                    SatisSekilKod3 = reader["kod3"].ToString(),
                                    SatisSekilKod4 = reader["kod4"].ToString(),
                                    SatisSekilKod5 = reader["kod5"].ToString(),
                                };
                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }

                return UrunCollection;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> PopulateOzellikListe(string ozellikKisit,string filter)
        {
            try
            {
                if (ozellikKisit == "Ürün Grup")
                { 
                    Variables.Query_ = "Select * from stburungrup where isim like '%' + @filter + '%'";

                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@filter", SqlDbType.NVarChar, 400);
                    parameter[0].Value = filter;

                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    UrunGrubuKodu = reader["kod"].ToString(),
                                    UrunGrubuIsim = reader["isim"].ToString(),
                                    UrunGrubuTur = Convert.ToInt32(reader["tur"]),
                                    TakimKodu = Convert.ToInt32(reader["takimkod"]),
                                    UniteKod = Convert.ToInt32(reader["unitekod"]),
                                    UrunGrubuSira = Convert.ToInt32(reader["sira"]),
                                    Kilit = reader["kilit"].ToString(),
                                    Sayfa = reader["sayfa"].ToString(),
                                    Kdv = Convert.ToInt32(reader["kdvo"]),
                                    Muhdetay = Convert.ToInt32(reader["muhdetay"]),
                                    MenuGrup = reader["menugrup"].ToString(),
                                    TeslimGunu = Convert.ToInt32(reader["teslimgunu"]),
                                    Kod1 = reader["kod1"].ToString(),
                                    Kod2 = reader["kod2"].ToString(),
                                    Kod3 = reader["kod3"].ToString(),
                                    Kod4 = reader["kod4"].ToString(),
                                    Kod5 = reader["kod5"].ToString(),
                                };

                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }
                if (ozellikKisit == "Model")
                {
                    Variables.Query_ = "Select * from stbmodel where isim like '%' + @filter + '%'";

                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@filter", SqlDbType.NVarChar, 400);
                    parameter[0].Value = filter;
                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    ModelKodu = reader["kod"].ToString(),
                                    ModelIsim = reader["isim"].ToString(),
                                    ModelSira = Convert.ToInt32(reader["sira"]),
                                    ModelKilit = reader["kilit"].ToString(),
                                    ModelKod1 = reader["kod1"].ToString(),
                                    ModelKod2 = reader["kod2"].ToString(),
                                    ModelKod3 = reader["kod3"].ToString(),
                                    ModelKod4 = reader["kod4"].ToString(),
                                    ModelKod5 = reader["kod5"].ToString(),
                                };
                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }
                if (ozellikKisit == "Satış Şekil")
                {
                    Variables.Query_ = "Select * from stbsatissekil where isim like '%' + @filter + '%'";

                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@filter", SqlDbType.NVarChar, 400);
                    parameter[0].Value = filter;
                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter))
                    {
                        if (reader != null && reader.HasRows)
                        {
                            coll_urun.Clear();
                            while (reader.Read())
                            {
                                Cls_Urun urun = new Cls_Urun
                                {
                                    SatisSekilKodu = reader["kod"].ToString(),
                                    SatisSekilIsim = reader["isim"].ToString(),
                                    SatisSekilSira = Convert.ToInt32(reader["sira"]),
                                    SatisSekilKilit = reader["kilit"].ToString(),
                                    SatisSekilKod1 = reader["kod1"].ToString(),
                                    SatisSekilKod2 = reader["kod2"].ToString(),
                                    SatisSekilKod3 = reader["kod3"].ToString(),
                                    SatisSekilKod4 = reader["kod4"].ToString(),
                                    SatisSekilKod5 = reader["kod5"].ToString(),
                                };
                                coll_urun.Add(urun);
                            }
                        }
                    }
                    UrunCollection = coll_urun;
                }

                return UrunCollection;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> PopulateOzellikDetayListe(string ozKod)
        {
            try
            {
                Variables.Query_ = "Select * from stbozdetay where maskod=@kod order by sira";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameter[0].Value = ozKod;
                ObservableCollection<Cls_Urun> detayColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikTipi = reader["maskod"] is DBNull ? "" : reader["maskod"].ToString();
                    model.Koddetay = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.KoddetayIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.KoddetayIsimIng = reader["ingisim"] is DBNull ? "" : reader["ingisim"].ToString();
                    model.KisitSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();

                    return model;
                });
                return detayColl;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> PopulateModelSecimList(string urunGrupKodu, string modelKodu, string satisSekilKodu)
        {
            try
            {
                Variables.Query_ = " select a.grupkod,a.modelkod,a.ssekli,b.isim as urun_grup_isim,c.isim as model_isim,d.isim as satis_sekil from stbmodelcapraz a " +
                                   " left join stburungrup b on a.grupkod = b.kod " +
                                   " left join stbmodel c on a.modelkod = c.kod " +
                                   " left join stbsatissekil d on a.ssekli = d.kod where 1=1 ";
                Variables.Counter_ = 0;

                if (!string.IsNullOrEmpty(urunGrupKodu))
                {
                    Variables.Query_ += " and a.grupkod = @urunGrupKodu ";
                    Variables.Counter_++;
                }

                if (!string.IsNullOrEmpty(modelKodu))
                {
                    Variables.Query_ += " and a.modelkod = @modelKodu ";
                    Variables.Counter_++;

                }
                if (!string.IsNullOrEmpty(satisSekilKodu))
                {
                    Variables.Query_ += " and a.ssekli = @satisSekilKodu ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;
                if (!string.IsNullOrEmpty(urunGrupKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@urunGrupKodu", SqlDbType.NVarChar, 2);
                    parameters[Variables.Counter_].Value = urunGrupKodu;
                    Variables.Counter_++;
                }

                if (!string.IsNullOrEmpty(modelKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                    parameters[Variables.Counter_].Value = modelKodu;
                    Variables.Counter_++;

                }
                if (!string.IsNullOrEmpty(satisSekilKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 2);
                    parameters[Variables.Counter_].Value = satisSekilKodu;
                    Variables.Counter_++;
                }

                ObservableCollection<Cls_Urun> modelListColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.UrunGrubuIsim = reader["urun_grup_isim"] is DBNull ? "" : reader["urun_grup_isim"].ToString();
                    model.ModelIsim = reader["model_isim"] is DBNull ? "" : reader["model_isim"].ToString();
                    model.SatisSekilIsim = reader["satis_sekil"] is DBNull ? "" : reader["satis_sekil"].ToString();
                    model.UrunGrubuKodu = reader["grupkod"] is DBNull ? "" : reader["grupkod"].ToString();
                    model.ModelKodu = reader["modelkod"] is DBNull ? "" : reader["modelkod"].ToString();
                    model.SatisSekilKodu = reader["ssekli"] is DBNull ? "" : reader["ssekli"].ToString();
                    model.UrunGrupModelSatisSekil = string.Format("{0}{1}{2}", reader["grupkod"] is DBNull ? "" : reader["grupkod"].ToString(), reader["modelkod"] is DBNull ? "" : reader["modelkod"].ToString(), reader["ssekli"] is DBNull ? "" : reader["ssekli"].ToString());
                    return model;
                });

                return modelListColl;

            }
            catch
            {
                return null;
            }
        }
        public int CheckIfSablonKodExists(string sablonKod) 
        {
            try
            {
                Variables.Query_ = "select count(*) from stbmodelcapraz where kod5=@sablonKod";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@sablonKod", SqlDbType.NVarChar, 50);
                parameter[0].Value = sablonKod;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_,Variables.Yil_, parameter, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch 
            {
                return -1;
            }
        }
        public int CheckIfOptionExists(Cls_Urun item)
        {
            try
            {
                if(item == null) return -1;

                Variables.Query_ = "select count(*) from stbmodelopsiyon where " +
                                    " sira=@ozellikSayisi and grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@varyantIsmi", SqlDbType.NVarChar, 50);
                parameters[1] = new SqlParameter("@ozellikSayisi", SqlDbType.Int);
                parameters[2] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[3] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[4] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[0].Value = item.VaryantIsmi;
                parameters[1].Value = item.OzellikSayisi;
                parameters[2].Value = item.UrunGrubuKodu;
                parameters[3].Value = item.ModelKodu;
                parameters[4].Value = item.SatisSekilKodu;

                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters,Variables.Fabrika_);

                return Variables.ResultInt_;
            }
            catch 
            {
                return -1;
            }
        }
        public int CheckIfOzMasExists(string kod)
        {
            try
            {
                Variables.Query_ = "select count(*) from stbozmas where kod=@kod";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameter[0].Value = kod;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfOzMasIsimExists(string isim)
        {
            try
            {
                Variables.Query_ = "select count(*) from stbozmas where isim=@isim";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameter[0].Value = isim;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfOzMasIsimExistsElseWhere(string kod,string isim)
        {
            try
            {
                if (string.IsNullOrEmpty(kod)) return -1;
                Variables.Query_ = "select count(*) from stbozmas where kod<>@kod and isim=@isim";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameters[0].Value = kod;
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[1].Value = isim;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfDetayKoduExistsElseWhere(string ozkod,string kod,int sira)
        {
            try
            {
                if (string.IsNullOrEmpty(ozkod) || string.IsNullOrEmpty(kod)) return -1;
                Variables.Query_ = "select count(*) from stbozdetay where maskod=@ozkod and kod=@kod and sira<>@sira";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ozkod", SqlDbType.NVarChar, 4);
                parameters[0].Value = ozkod;
                parameters[1] = new SqlParameter("@kod", SqlDbType.NVarChar, 10);
                parameters[1].Value = kod;
                parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[2].Value = sira;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfDetayKoduIsimExistsElseWhere(string ozkod,string isim,int sira)
        {
            try
            {
                if (string.IsNullOrEmpty(ozkod) || string.IsNullOrEmpty(isim)) return -1;
                Variables.Query_ = "select count(*) from stbozdetay where maskod=@ozkod and isim=@isim and sira<>@sira";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ozkod", SqlDbType.NVarChar, 4);
                parameters[0].Value = ozkod;
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[1].Value = isim;
                parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[2].Value = sira;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfDetayKoduIsimIngExistsElseWhere(string ozkod, string isim,int sira)
        {
            try
            {
                if (string.IsNullOrEmpty(ozkod) || string.IsNullOrEmpty(isim)) return -1;
                Variables.Query_ = "select count(*) from stbozdetay where maskod=@ozkod and ingisim=@isim and sira<>@sira";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ozkod", SqlDbType.NVarChar, 4);
                parameters[0].Value = ozkod;
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[1].Value = isim;
                parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[2].Value = sira;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CheckIfCaprazExists(string urunGrubuKodu, string modelKodu, string satisSekilKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(urunGrubuKodu)) return -1;
                if (string.IsNullOrEmpty(modelKodu)) return -1;
                if (string.IsNullOrEmpty(satisSekilKodu)) return -1;



                Variables.Query_ = "select count(*) from stbmodelcapraz where grupkod=@grupkod and modelkod=@modelkod and ssekli=@ssekli";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@grupkod", SqlDbType.NVarChar, 10);
                parameters[0].Value = urunGrubuKodu;
                parameters[1] = new SqlParameter("@modelkod", SqlDbType.NVarChar, 50);
                parameters[1].Value = modelKodu;
                parameters[2] = new SqlParameter("@ssekli", SqlDbType.NVarChar,10);
                parameters[2].Value = satisSekilKodu;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public bool CheckIfUrunGrubuExists(string urunGrubuKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(urunGrubuKodu)) return false;

                Variables.Query_ = "select count(*) from stburungrup where kod=@grupkod";
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@grupkod", SqlDbType.NVarChar, 10);
                parameters[0].Value = urunGrubuKodu;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                if (Variables.ResultInt_ > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckIfModelExists(string modelKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(modelKodu)) return false;

                Variables.Query_ = "select count(*) from stbmodel where kod=@modelKodu";
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 50);
                parameters[0].Value = modelKodu;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                if (Variables.ResultInt_ > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckIfSatisSekliExists(string satisSekilKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(satisSekilKodu)) return false;

                Variables.Query_ = "select count(*) from stbsatissekil where kod=@satisSekilKodu";
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 10);
                parameters[0].Value = satisSekilKodu;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                if (Variables.ResultInt_ > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public string GetKod(string ozellikTipi)
        {
            try
            {
                if(ozellikTipi == "Ürün Grup")
                    Variables.Query_ = "select top 1 kod from stburungrup order by kod desc";
                else if(ozellikTipi == "Model")
                    return string.Empty;
                else if (ozellikTipi == "Satış Şekil")
                    return string.Empty;

                UrunGrubuKodu = dataLayer.Get_One_String_Result_Command(Variables.Query_, Variables.Yil_);

                UrunGrubuKodu = (Convert.ToInt32(UrunGrubuKodu) + 1).ToString(); 

                return UrunGrubuKodu;
            }
            catch 
            {
                return "STRING HATA";
            }
        }
        public ObservableCollection<Cls_Urun> GetOzmas()
        {
            try
            {
                Variables.Query_ = "Select kod,isim from stbozmas order by kod";

                ObservableCollection<Cls_Urun>ozmasColl = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_,Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikTipi = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.OzellikIsmi = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    return model;
                });

                return ozmasColl;
            }
            catch 
            {
                return null;
            }
        }
        public Cls_Urun GetOzMasInfo(string kod)
        {
            try
            {
                Variables.Query_ = "Select * from stbozmas where kod=@kod";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameter[0].Value = kod;

                ObservableCollection<Cls_Urun>? ozmasColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_,Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Urun? model = new();
                    model.OzellikTipi = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.OzellikIsmi = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.KodUzunluk = reader["uzunluk"] is DBNull ? 0 : Convert.ToInt32(reader["uzunluk"]);
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return ozmasColl.FirstOrDefault();
            }
            catch 
            {
                return null;
            }
        }
        public int GetKodDetayUzunluk(string kod)
        {
            try
            {
                Variables.Query_ = "Select uzunluk from stbozmas where kod=@kod";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameter[0].Value = kod;
                Variables.ResultInt_ = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public ObservableCollection<Cls_Urun> GetUrunGrupIsim()
        {
            try
            {
                Variables.Query_ = "Select isim from stburungrup order by isim";

                ObservableCollection<Cls_Urun>urunGrubu = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_,Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.UrunGrubuIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    return model;
                });

                return urunGrubu;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetModelIsim()
        {
            try
            {
                Variables.Query_ = "Select isim from stbmodel order by isim";

                ObservableCollection<Cls_Urun> model = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.ModelIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    return model;
                });

                return model;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetUrunGrubuList()
        {
            try
            {
                Variables.Query_ = "Select * from stburungrup order by isim";

                ObservableCollection<Cls_Urun> urunGrup = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.UrunGrubuKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.UrunGrubuIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.UrunGrubuSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return urunGrup;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetUrunGrubuList(Dictionary<string,string> constDic)
        {
            try
            {
                if (constDic == null) return null;
                if (constDic.Count < 1) return null;

                Variables.Query_ = "Select * from stburungrup where 1=1 ";
                SqlParameter[] parameters = new SqlParameter[constDic.Count];
                Variables.Counter_ = 0;
                if (constDic.ContainsKey("Kod"))
                {
                    Variables.Query_ += " and kod like '%' + @kod + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@kod", SqlDbType.NVarChar,10);
                    parameters[Variables.Counter_].Value = constDic["Kod"].ToString();
                    Variables.Counter_++;
                }
                if (constDic.ContainsKey("Ad"))
                {
                    Variables.Query_ += " and isim like '%' + @isim + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@isim", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = constDic["Ad"].ToString();
                    Variables.Counter_++;
                }
                Variables.Query_ += " order by isim ";

                ObservableCollection<Cls_Urun> model = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.UrunGrubuKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.UrunGrubuIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.UrunGrubuSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return model;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetModelList()
        {
            try
            {
                Variables.Query_ = "Select * from stbmodel order by isim";

                ObservableCollection<Cls_Urun> model = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.ModelKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.ModelIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.ModelSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return model;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetModelList(Dictionary<string,string> constDic)
        {
            try
            {
                if (constDic == null) return null;
                if (constDic.Count < 1) return null;

                Variables.Query_ = "Select * from stbmodel where 1=1 ";
                SqlParameter[] parameters = new SqlParameter[constDic.Count];
                Variables.Counter_ = 0;
                if (constDic.ContainsKey("Kod"))
                {
                    Variables.Query_ += " and kod like '%' + @kod + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@kod", SqlDbType.NVarChar,100);
                    parameters[Variables.Counter_].Value = constDic["Kod"].ToString();
                    Variables.Counter_++;
                }
                if (constDic.ContainsKey("Ad"))
                {
                    Variables.Query_ += " and isim like '%' + @isim + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@isim", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = constDic["Ad"].ToString();
                    Variables.Counter_++;
                }
                Variables.Query_ += " order by isim ";

                ObservableCollection<Cls_Urun> model = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.ModelKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.ModelIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.ModelSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return model;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetSatisSekilList()
        {
            try
            {
                Variables.Query_ = "Select * from stbsatissekil order by isim";

                ObservableCollection<Cls_Urun> sSekil = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.SatisSekilKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.SatisSekilIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.SatisSekilSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return sSekil;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetSatisSekilList(Dictionary<string,string> constDic)
        {
            try
            {
                if (constDic == null) return null;
                if (constDic.Count<1) return null;

                Variables.Query_ = "Select * from stbsatissekil where 1=1 ";
                SqlParameter[] parameters = new SqlParameter[constDic.Count];
                Variables.Counter_ = 0;
                if(constDic.ContainsKey("Kod"))
                {
                    Variables.Query_ += " and kod like '%' + @kod + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@kod",SqlDbType.NVarChar,10);
                    parameters[Variables.Counter_].Value = constDic["Kod"].ToString();
                    Variables.Counter_ ++;
                }
                if(constDic.ContainsKey("Ad"))
                {
                    Variables.Query_ += " and isim like '%' + @isim + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@isim",SqlDbType.NVarChar,100);
                    parameters[Variables.Counter_].Value = constDic["Ad"].ToString();
                    Variables.Counter_ ++;
                }
                Variables.Query_ += " order by isim ";
                ObservableCollection<Cls_Urun> sSekil = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.SatisSekilKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.SatisSekilIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.SatisSekilSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    return model;
                });

                return sSekil;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetSatisSekilIsim()
        {
            try
            {
                Variables.Query_ = "Select isim from stbsatissekil order by isim";

                ObservableCollection<Cls_Urun> sSekil = dataLayer.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    Cls_Urun model = new();
                    model.SatisSekilIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    return model;
                });

                return sSekil;
            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetUrunGrubuKodu(string isim)
        {
            try
            {
                Variables.Query_ = "Select kod from stburungrup where isim=@isim";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameter[0].Value = isim;


                ObservableCollection<Cls_Urun> urunGrubu = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.UrunGrubuKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    return model;
                });

                return urunGrubu;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetModelKodu(string isim)
        {
            try
            {
                Variables.Query_ = "Select kod from stbmodel where isim=@isim";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameter[0].Value = isim;


                ObservableCollection<Cls_Urun> modelKod = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.ModelKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    return model;
                });

                return modelKod;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetSatisSekilKodu(string isim)
        {
            try
            {
                Variables.Query_ = "Select kod from stbsatissekil where isim=@isim";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@isim", SqlDbType.NVarChar, 100);
                parameter[0].Value = isim;


                ObservableCollection<Cls_Urun> sSekliKod = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.SatisSekilKodu = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    return model;
                });

                return sSekliKod;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetUpdatedSiraliOzellikDetay(string kod,int siraTipi)
        {
            try
            {
                if (string.IsNullOrEmpty(kod))
                    return null;
                
                Variables.Query_ = "Select * from stbozdetay where maskod=@kod";
                
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameter[0].Value = kod;

                ObservableCollection<Cls_Urun> detayColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikTipi = reader["maskod"] is DBNull ? "" : reader["maskod"].ToString();
                    model.Koddetay = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.KoddetayIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    model.KoddetayIsimIng = reader["ingisim"] is DBNull ? "" : reader["ingisim"].ToString();
                    model.KisitSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.Kilit = reader["kilit"] is DBNull ? "" : reader["kilit"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.Kod5 = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();

                    return model;
                });
                Variables.Counter_ = 1;
                if(siraTipi==1)
                {
                    foreach(Cls_Urun item in detayColl.OrderBy(k=>k.KisitKod))
                    {
                        Variables.Query_ = "update stbozdetay set sira=@yeniSira where maskod=@maskod and kod=@kod and sira=@sira";
                        SqlParameter[] parameters = new SqlParameter[4];
                        parameters[0] = new SqlParameter("@yeniSira", SqlDbType.Int);
                        parameters[1] = new SqlParameter("@maskod", SqlDbType.NVarChar, 4);
                        parameters[2] = new SqlParameter("@kod", SqlDbType.NVarChar, 10);
                        parameters[3] = new SqlParameter("@sira", SqlDbType.Int);

                        parameters[0].Value = Variables.Counter_;
                        parameters[1].Value = item.OzellikTipi;
                        parameters[2].Value = item.Koddetay;
                        parameters[3].Value = item.KisitSira;

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_,parameters);
                        if (!Variables.Result_)
                            return null;
                        item.KisitSira = Variables.Counter_;
                        Variables.Counter_ ++;
                    }
                }
                else
                {
                    foreach (Cls_Urun item in detayColl.OrderBy(k => k.KoddetayIsim))
                    {
                        Variables.Query_ = "update stbozmas set sira=@yeniSira where maskod=@maskod and kod=@kod and sira=@sira";
                        SqlParameter[] parameters = new SqlParameter[4];
                        parameters[0] = new SqlParameter("@yeniSira", SqlDbType.Int);
                        parameters[1] = new SqlParameter("@maskod", SqlDbType.NVarChar, 4);
                        parameters[2] = new SqlParameter("@kod", SqlDbType.NVarChar, 10);
                        parameters[3] = new SqlParameter("@sira", SqlDbType.Int);

                        parameters[0].Value = Variables.Counter_;
                        parameters[1].Value = item.OzellikTipi;
                        parameters[2].Value = item.Koddetay;
                        parameters[3].Value = item.KisitSira;

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                        if (!Variables.Result_)
                            return null;
                        item.KisitSira = Variables.Counter_;
                        Variables.Counter_++;
                    }
                }
                return detayColl;

            }
            catch 
            {
                return null;
            }
        }
        public Cls_Urun GetOpsiyonOlusacakUrun(Cls_Urun secilenModel)
        {
            try
            {
                if (secilenModel == null)
                    return null;

                Variables.Query_ = "Select * from stbmodelcapraz where 1=1 ";
                Variables.Counter_ = 0;

                if (!string.IsNullOrEmpty(secilenModel.UrunGrubuKodu))
                {
                    Variables.Query_ += " and grupkod = @urunGrupKodu ";
                    Variables.Counter_++;
                }

                if (!string.IsNullOrEmpty(secilenModel.ModelKodu))
                {
                    Variables.Query_ += " and modelkod = @modelKodu ";
                    Variables.Counter_++;

                }
                if (!string.IsNullOrEmpty(secilenModel.SatisSekilKodu))
                {
                    Variables.Query_ += " and ssekli = @satisSekilKodu ";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;
                if (!string.IsNullOrEmpty(secilenModel.UrunGrubuKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@urunGrupKodu", SqlDbType.NVarChar, 2);
                    parameters[Variables.Counter_].Value = secilenModel.UrunGrubuKodu;
                    Variables.Counter_++;
                }

                if (!string.IsNullOrEmpty(secilenModel.ModelKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                    parameters[Variables.Counter_].Value = secilenModel.ModelKodu;
                    Variables.Counter_++;

                }
                if (!string.IsNullOrEmpty(secilenModel.SatisSekilKodu))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 2);
                    parameters[Variables.Counter_].Value = secilenModel.SatisSekilKodu;
                    Variables.Counter_++;
                }

                ObservableCollection<Cls_Urun> modelListColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikSayisi = reader["ozsay"] is DBNull ? 0 : Convert.ToInt32(reader["ozsay"]);
                    model.SablonKod = reader["kod5"] is DBNull ? "" : reader["kod5"].ToString();
                    model.Kod1 = reader["kod1"] is DBNull ? "" : reader["kod1"].ToString();
                    model.Kod2 = reader["kod2"] is DBNull ? "" : reader["kod2"].ToString();
                    model.Kod3 = reader["kod3"] is DBNull ? "" : reader["kod3"].ToString();
                    model.Kod4 = reader["kod4"] is DBNull ? "" : reader["kod4"].ToString();
                    model.IsimAnahtar = reader["isimanahtar"] is DBNull ? "" : reader["isimanahtar"].ToString();
                    model.IngilizceIsimAnahtar = reader["isimanahtaring"] is DBNull ? "" : reader["isimanahtaring"].ToString();
                    return model;
                });

                return modelListColl.FirstOrDefault();

            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetOpsiyonlarYeniUrun(Cls_Urun varyantOlusturulacak, Cls_Urun kopyalanacakUrun)
        {
            try
            {
                Variables.Query_ = "Select a.sira,a.ozkod,a.ozisim,mas.isim as ozkodisim,a.baslangic,a.uzunluk,a.recdeg,a.kisakod from stbmodelopsiyon a left join stbozmas mas on a.ozkod=mas.kod where grupkod=@grupKodu and modelkod=@modelKodu and ssekli=@satisSekli";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@grupKodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satisSekli", SqlDbType.NVarChar, 2);
                parameters[0].Value = string.IsNullOrEmpty(kopyalanacakUrun.UrunGrubuKodu) ? varyantOlusturulacak.UrunGrubuKodu : kopyalanacakUrun.UrunGrubuKodu; 
                parameters[1].Value = string.IsNullOrEmpty(kopyalanacakUrun.ModelKodu) ? varyantOlusturulacak.ModelKodu : kopyalanacakUrun.ModelKodu;
                parameters[2].Value = string.IsNullOrEmpty(kopyalanacakUrun.SatisSekilKodu) ? varyantOlusturulacak.SatisSekilKodu : kopyalanacakUrun.SatisSekilKodu;

                ObservableCollection<Cls_Urun> modelListColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikSayisi = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.OzellikTipi = reader["ozkod"] is DBNull ? "" : reader["ozkod"].ToString();
                    model.OzellikIsmi = reader["ozkodisim"] is DBNull ? "" : reader["ozkodisim"].ToString();
                    model.VaryantIsmi = reader["ozisim"] is DBNull ? "" : reader["ozisim"].ToString();
                    model.BaslangicSira = reader["baslangic"] is DBNull ? 0 : Convert.ToInt32(reader["baslangic"]);
                    model.KodUzunluk = reader["uzunluk"] is DBNull ? 0 : Convert.ToInt32(reader["uzunluk"]); ;
                    model.ReceteDegeri = reader["recdeg"] is DBNull ? "" : reader["recdeg"].ToString();
                    model.KisaKod = reader["kisakod"] is DBNull ? "" : reader["kisakod"].ToString();
                    return model;
                });

                return modelListColl;


            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetOpsiyonlar(Cls_Urun varyantOlusturulacak)
        {
            try
            {
                Variables.Query_ = "Select a.sira,a.ozkod,a.ozisim,mas.isim as ozkodisim,a.baslangic,a.uzunluk,a.recdeg,a.kisakod from stbmodelopsiyon a left join stbozmas mas on a.ozkod=mas.kod where grupkod=@grupKodu and modelkod=@modelKodu and ssekli=@satisSekli order by sira";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@grupKodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satisSekli", SqlDbType.NVarChar, 2);
                parameters[0].Value = varyantOlusturulacak.UrunGrubuKodu; 
                parameters[1].Value = varyantOlusturulacak.ModelKodu; 
                parameters[2].Value = varyantOlusturulacak.SatisSekilKodu;

                ObservableCollection<Cls_Urun> modelListColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikSayisi = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.OzellikTipi = reader["ozkod"] is DBNull ? "" : reader["ozkod"].ToString();
                    model.OzellikIsmi = reader["ozkodisim"] is DBNull ? "" : reader["ozkodisim"].ToString();
                    model.VaryantIsmi = reader["ozisim"] is DBNull ? "" : reader["ozisim"].ToString();
                    model.BaslangicSira = reader["baslangic"] is DBNull ? 0 : Convert.ToInt32(reader["baslangic"]);
                    model.KodUzunluk = reader["uzunluk"] is DBNull ? 0 : Convert.ToInt32(reader["uzunluk"]); ;
                    model.ReceteDegeri = reader["recdeg"] is DBNull ? "" : reader["recdeg"].ToString();
                    model.KisaKod = reader["kisakod"] is DBNull ? "" : reader["kisakod"].ToString();
                    return model;
                });

                return modelListColl;


            }
            catch 
            {
                return null;
            }
        }
        public ObservableCollection<string> GetOzellikIsimleri()
        {
            Variables.Query_ = "select isim from stbozmas order by isim";

            ObservableCollection<string> ozisimColl = new();
            if (ozisimColl != null)
                ozisimColl.Clear();
            string ozisim = string.Empty;
            using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, Variables.Fabrika_))
            {
                if (reader == null)
                    return null;

                while (reader.Read())
                {

                    ozisim = reader[0].ToString();

                    ozisimColl.Add(ozisim);
                }
            }
            ozisimColl.Add("<-Seçim Yapınız->");
            return ozisimColl;
        }
        public ObservableCollection<string> GetOzellikKodlari()
        {
            Variables.Query_ = "select kod from stbozmas order by isim";

            ObservableCollection<string> ozisimColl = new();
            if (ozisimColl != null)
                ozisimColl.Clear();
            string ozkod = string.Empty;
            using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, Variables.Fabrika_))
            {
                if (reader == null)
                    return null;

                while (reader.Read())
                {

                    ozkod = reader[0].ToString();

                    ozisimColl.Add(ozkod);
                }
            }
            ozisimColl.Add("-");
            return ozisimColl;
        }
        public string GetOzellikKoduFromOzellikIsmi(string ozellikIsmi)
        {
            try
            {
                Variables.Query_ = "select kod from stbozmas where isim = @ozellikIsmi";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@ozellikIsmi", SqlDbType.NVarChar, 100);
                parameter[0].Value = ozellikIsmi;
                string result = dataLayer.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter);
                return result;
            }
            catch 
            {
                return string.Empty;
            }
        }
        public ObservableCollection<Cls_Urun> GetOzellikInfoFromOzellikKodu(string ozellikKodu)
        {
            try
            {
                Variables.Query_ = "select kod,isim from stbozdetay where maskod = @ozellikKodu order by isim";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@ozellikKodu", SqlDbType.NVarChar, 100);
                parameter[0].Value = ozellikKodu;

                ObservableCollection<Cls_Urun> opsiyonColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.Koddetay = reader["kod"] is DBNull ? "" : reader["kod"].ToString();
                    model.KoddetayIsim = reader["isim"] is DBNull ? "" : reader["isim"].ToString();
                    return model;
                });

                return opsiyonColl;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Urun> GetKisits(string urunGrupKodu,string modelKodu, string satisSekilKodu,int sira)
        {
            try
            {
                Variables.Query_ = "select * from [dbo].[stbmodelozkisit] where ugrupkod=@urunGrupKodu and modelkod=@modelKodu and ssekli=@satisSekilKodu and sira=@sira";
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@urunGrupKodu", SqlDbType.NVarChar, 2);
                parameter[0].Value = urunGrupKodu;
                parameter[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                parameter[1].Value = modelKodu;
                parameter[2] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 2);
                parameter[2].Value = satisSekilKodu;
                parameter[3] = new SqlParameter("@sira", SqlDbType.Int);
                parameter[3].Value = sira;

                ObservableCollection<Cls_Urun> kisitColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Urun model = new();
                    model.KisitSira = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.KisitKod = reader["degerkod"] is DBNull ? "" : reader["degerkod"].ToString();
                    return model;
                });

                return kisitColl;
            }
            catch
            {
                return null;
            }
        }
        public string GetIngilizceAnahtarDegeri()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                firstRow = dataTable.Rows[0];
                columnValueByIndex = firstRow[0];
                string ingilizceAnahtarDegeri = columnValueByIndex.ToString();

                return ingilizceAnahtarDegeri;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string GetVaryantKodu()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                firstRow = dataTable.Rows[0];
                columnValueByIndex = firstRow[0];
                string ozellikKodu = columnValueByIndex.ToString();

                return ozellikKodu;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int GetOzellikKodu()
        {
            try
            {

                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                firstRow = dataTable.Rows[0];
                columnValueByIndex = firstRow[0];
                int ozellikKodu = Convert.ToInt32(columnValueByIndex);

                return ozellikKodu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public ObservableCollection<Cls_Urun> VaryantOzKisitVarIseListele()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                coll_urun.Clear();

                foreach (DataRow row in dataTable.Rows)
                {

                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        VaryantTipi = row["isim"].ToString()
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;

                return UrunCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int GetMaxSira()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                firstRow = dataTable.Rows[0];
                columnValueByIndex = firstRow[0];
                int maxSira = Convert.ToInt32(columnValueByIndex);

                return maxSira;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public ObservableCollection<Cls_Urun> GetNameKeys()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                coll_urun.Clear();

                foreach (DataRow row in dataTable.Rows)
                {

                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        IsimAnahtar = row["isimanahtar"].ToString(),
                        IngilizceIsimAnahtar = row["isimanahtaring"].ToString(),
                        SablonKod = row["kod5"].ToString(),
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;

                return UrunCollection;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public Dictionary<int,int> GetKodBaslangicUzunluk(Cls_Urun yeniVaryant)
        {
            try
            {
                if(yeniVaryant == null)
                {
                    Dictionary<int, int> hataDic = new();
                    hataDic.Add(0, 0);
                    return hataDic;
                }

                Variables.Query_ = "select uzunluk from stbozmas where kod=@ozkod";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@ozkod", SqlDbType.NVarChar, 4);
                parameter[0].Value = yeniVaryant.OzellikTipi;

                int uzunluk = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                if(uzunluk == 0)
                {
                    Dictionary<int, int> hataDic = new();
                    hataDic.Add(0, 0);
                    return hataDic;
                }
                int baslangic = 0;
                if (yeniVaryant.OzellikSayisi == 1)
                    baslangic = 1;
                else if (yeniVaryant.OzellikSayisi == 0)
                {
                    Dictionary<int, int> hataDic = new();
                    hataDic.Add(0, 0);
                    return hataDic;
                }
                else 
                { 
                
                    Variables.Query_ = "select sira, baslangic,uzunluk from stbmodelopsiyon where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu and sira=@sira";

                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                    parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                    parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                    parameters[3] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[0].Value = yeniVaryant.UrunGrubuKodu;
                    parameters[1].Value = yeniVaryant.ModelKodu;
                    parameters[2].Value = yeniVaryant.SatisSekilKodu;
                    parameters[3].Value = yeniVaryant.OzellikSayisi - 1;

                    ObservableCollection<Cls_Urun> kodSiraColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                    {
                        Cls_Urun model = new();
                        model.OzellikSayisi = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                        model.BaslangicSira = reader["baslangic"] is DBNull ? 0 : Convert.ToInt32(reader["baslangic"]);
                        model.KodUzunluk = reader["uzunluk"] is DBNull ? 0 : Convert.ToInt32(reader["uzunluk"]);
                        return model;
                    });

                    if (kodSiraColl == null)
                    {
                        Dictionary<int, int> hataDic = new();
                        hataDic.Add(0, 0);
                        return hataDic;
                    }

                    if (kodSiraColl.Count == 0)
                    {
                        Dictionary<int, int> hataDic = new();
                        hataDic.Add(0, 0);
                        return hataDic;
                    }
                    baslangic = kodSiraColl.Select(b => b.BaslangicSira).FirstOrDefault() + kodSiraColl.Select(u => u.KodUzunluk).FirstOrDefault();
                }

                if (baslangic == 0 ||
                    uzunluk == 0)
                {
                    Dictionary<int, int> hataDic = new();
                    hataDic.Add(0, 0);
                    return hataDic;
                }

                Dictionary<int, int> baslangicUzunluk = new();
                baslangicUzunluk.Add(baslangic, uzunluk);
                return baslangicUzunluk;

            }
            catch
            {
                Dictionary<int, int> hataDic = new();
                hataDic.Add(0, 0);
                return hataDic;

            }
        }
        public int GetNextOzdetaySira(string maskod)
        {
            try
            {
                Variables.Query_ = "Select isnull(max(sira) + 1,1) from stbozdetay where maskod=@maskod";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@maskod", SqlDbType.NVarChar, 4);
                parameter[0].Value = maskod;
                int nextSira = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                return nextSira;
            }
            catch
            {
                return -1;
            }
        }
        public ObservableCollection<Cls_Urun> VaryantListele()
        {
            try
            {
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                coll_urun.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Urun cls_urun = new Cls_Urun
                    {
                        VaryantTipi = row["isim"].ToString()
                    };
                    coll_urun.Add(cls_urun);
                }

                UrunCollection = coll_urun;
                OnPropertyChanged(nameof(OzKisit));

                return UrunCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public bool InsertCapraz(string urunGrubuKodu, string modelKodu, string satisSekilKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(urunGrubuKodu)) return false;
                if (string.IsNullOrEmpty(modelKodu)) return false;
                if (string.IsNullOrEmpty(satisSekilKodu)) return false;

                Variables.Query_ = "insert into stbmodelcapraz values(@grupkod,@modelkod,@ssekli,'H','H','H',0,'','','','','','','','')";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@grupkod", SqlDbType.NVarChar, 10);
                parameters[0].Value = urunGrubuKodu;
                parameters[1] = new SqlParameter("@modelkod", SqlDbType.NVarChar, 100);
                parameters[1].Value = modelKodu;
                parameters[2] = new SqlParameter("@ssekli", SqlDbType.NVarChar, 10);
                parameters[2].Value = satisSekilKodu;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);

                return Variables.Result_;
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public bool InsertOzellik(Cls_Urun urunOzellik, string ozellikTipi)
        {
            try
            {
                if (ozellikTipi == "Ürün Grup")
                {
                    Variables.Query_ = "insert into stburungrup (kod,isim,tur,takimkod,unitekod,sira,kilit,sayfa,kdvo,muhdetay,menugrup,teslimgunu,kod1,kod2,kod3,kod4,kod5) values " +
                                  "(@kod,@isim,@tur,@takimkod,@unitekod,@sira,@kilit,@sayfa,@kdvo,@muhdetay,@menugrup,@teslimgunu,@kod1,@kod2,@kod3,@kod4,@kod5)";

                    SqlParameter[] parameters = new SqlParameter[17];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 3);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                    parameters[2] = new SqlParameter("@tur", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@takimkod", SqlDbType.NVarChar, 3);
                    parameters[4] = new SqlParameter("@unitekod", SqlDbType.NVarChar, 2);
                    parameters[5] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[6] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[7] = new SqlParameter("@sayfa", SqlDbType.NVarChar, 50);
                    parameters[8] = new SqlParameter("@kdvo", SqlDbType.Float);
                    parameters[9] = new SqlParameter("@muhdetay", SqlDbType.Int);
                    parameters[10] = new SqlParameter("@menugrup", SqlDbType.NVarChar, 50);
                    parameters[11] = new SqlParameter("@teslimgunu", SqlDbType.Int);
                    parameters[12] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                    parameters[13] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                    parameters[14] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                    parameters[15] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                    parameters[16] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                    parameters[0].Value = urunOzellik.UrunGrubuKodu;
                    parameters[1].Value = urunOzellik.UrunGrubuIsim;
                    parameters[2].Value = urunOzellik.UrunGrubuTur;
                    parameters[3].Value = urunOzellik.TakimKodu;
                    parameters[4].Value = urunOzellik.UniteKod;
                    parameters[5].Value = urunOzellik.UrunGrubuSira;
                    parameters[6].Value = urunOzellik.Kilit;
                    parameters[7].Value = urunOzellik.Sayfa;
                    parameters[8].Value = urunOzellik.Kdv;
                    parameters[9].Value = urunOzellik.Muhdetay;
                    parameters[10].Value = urunOzellik.MenuGrup;
                    parameters[11].Value = urunOzellik.TeslimGunu;
                    parameters[12].Value = urunOzellik.Kod1;
                    parameters[13].Value = urunOzellik.Kod2;
                    parameters[14].Value = urunOzellik.Kod3;
                    parameters[15].Value = urunOzellik.Kod4;
                    parameters[16].Value = urunOzellik.Kod5;

                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }
                if (ozellikTipi == "Model")
                {
                    Variables.Query_ = "insert into stbmodel (kod,isim,sira,kilit,kod1,kod2,kod3,kod4,kod5) values " +
                                  "(@kod,@isim,@sira,@kilit,@kod1,@kod2,@kod3,@kod4,@kod5)";

                    SqlParameter[] parameters = new SqlParameter[9];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 3);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                    parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[4] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                    parameters[5] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                    parameters[6] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                    parameters[7] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                    parameters[8] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                    parameters[0].Value = urunOzellik.ModelKodu;
                    parameters[1].Value = urunOzellik.ModelIsim;
                    parameters[2].Value = urunOzellik.ModelSira;
                    parameters[3].Value = urunOzellik.ModelKilit;
                    parameters[4].Value = urunOzellik.ModelKod1;
                    parameters[5].Value = urunOzellik.ModelKod2;
                    parameters[6].Value = urunOzellik.ModelKod3;
                    parameters[7].Value = urunOzellik.ModelKod4;
                    parameters[8].Value = urunOzellik.ModelKod5;

                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }
                if (ozellikTipi == "Satış Şekil")
                {
                    Variables.Query_ = "insert into stbsatissekil (kod,isim,sira,kilit,kod1,kod2,kod3,kod4,kod5) values " +
                                  "(@kod,@isim,@sira,@kilit,@kod1,@kod2,@kod3,@kod4,@kod5)";


                    SqlParameter[] parameters = new SqlParameter[9];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 2);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 100);
                    parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[4] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[5] = new SqlParameter("@kod2", SqlDbType.NVarChar, 100);
                    parameters[6] = new SqlParameter("@kod3", SqlDbType.NVarChar, 100);
                    parameters[7] = new SqlParameter("@kod4", SqlDbType.NVarChar, 100);
                    parameters[8] = new SqlParameter("@kod5", SqlDbType.NVarChar, 100);

                    parameters[0].Value = urunOzellik.SatisSekilKodu;
                    parameters[1].Value = urunOzellik.SatisSekilIsim;
                    parameters[2].Value = urunOzellik.SatisSekilSira;
                    parameters[3].Value = urunOzellik.SatisSekilKilit;
                    parameters[4].Value = urunOzellik.SatisSekilKod1;
                    parameters[5].Value = urunOzellik.SatisSekilKod2;
                    parameters[6].Value = urunOzellik.SatisSekilKod3;
                    parameters[7].Value = urunOzellik.SatisSekilKod4;
                    parameters[8].Value = urunOzellik.SatisSekilKod5;

                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }

                return variables.Result;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertOzellikBaslikDetay(Cls_Urun urunOzellik)
        {
            try
            {
                if (urunOzellik == null)
                    return false;

                Variables.Query_ = "insert into stbozdetay (maskod,kod,isim,ingisim,sira,kilit,kod1,kod2,kod3,kod4,kod5) values " +
                              "(@maskod,@kod,@isim,@ingisim,@sira,@kilit,@kod1,@kod2,@kod3,@kod4,@kod5)";

                SqlParameter[] parameters = new SqlParameter[11];

                parameters[0] = new SqlParameter("@maskod", SqlDbType.NVarChar, 4);
                parameters[1] = new SqlParameter("@kod", SqlDbType.NVarChar, 10);
                parameters[2] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[3] = new SqlParameter("@ingisim", SqlDbType.NVarChar,50);
                parameters[4] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[5] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                parameters[6] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[8] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[10] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                parameters[0].Value = urunOzellik.OzellikTipi;
                parameters[1].Value = urunOzellik.Koddetay;
                parameters[2].Value = urunOzellik.KoddetayIsim;
                parameters[3].Value = urunOzellik.KoddetayIsimIng;
                parameters[4].Value = urunOzellik.KoddetaySira;
                parameters[5].Value = urunOzellik.Kilit;
                parameters[6].Value = urunOzellik.Kod1;
                parameters[7].Value = urunOzellik.Kod2;
                parameters[8].Value = urunOzellik.Kod3;
                parameters[9].Value = urunOzellik.Kod4;
                parameters[10].Value = urunOzellik.Kod5;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                
                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertOzMas(Cls_Urun urunOzellik)
        {
            try
            {
                if (urunOzellik == null) return false;
                
                Variables.Query_ = "insert into stbozmas values " +
                              "(@kod,@isim,@uzunluk,@kod1,@kod2,@kod3,@kod4,@kod5)";

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[2] = new SqlParameter("@uzunluk", SqlDbType.Int);
                parameters[3] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[4] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[5] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                parameters[0].Value = urunOzellik.OzellikTipi;
                parameters[1].Value = urunOzellik.OzellikIsmi;
                parameters[2].Value = urunOzellik.KodUzunluk;
                parameters[3].Value = urunOzellik.Kod1;
                parameters[4].Value = urunOzellik.Kod2;
                parameters[5].Value = urunOzellik.Kod3;
                parameters[6].Value = urunOzellik.Kod4;
                parameters[7].Value = urunOzellik.Kod5;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
              
                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> InsertVaryantAsync(string sablonKod, string mamulKodu, string mamulAdi, string identifier, string mamulAdiIngilizce)
        {
            try
            {

                LoginLogic login = new();
                string userName = login.GetUserName();

                Variables.Result_ = await dataLayer.Insert_Stored_Proc_Param_6_Async("vbpVaryantStokKartiAc", "@sablonKod", sablonKod, "@varyantKod", mamulKodu, "@varyantIsim", mamulAdi, "@identifier", identifier, "@varyantIngIsim", mamulAdiIngilizce, "@kayitYapanKul", userName, Variables.Yil_, "Stok Kartı Tabloları", 1);
                if (!Variables.Result_)
                    return false;
                Cls_Arge arge = new();
                await arge.UpdateTuremisKodTekAsync(mamulKodu);
                return Variables.Result_;
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public bool InsertVaryantOlustur(Cls_Urun yeniVaryant)
        {
            try
            {

                Variables.Query_ = "insert into stbmodelcapraz values (@grupkodu,@modelkodu,@satissekilkodu,'H','H','H',@ozsay,@isimanahtar,@varyantkodlari, " +
                                   "@kod1,@kod2,@kod3,@kod4,@sablonkod,@isimanahtaring)";

                string varyantKodlari = string.Empty;
                for (int i = 1; i <= yeniVaryant.OzellikSayisi; i++)
                    varyantKodlari += string.Format("@o{0}", i);

                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@grupkodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[3] = new SqlParameter("@ozsay", SqlDbType.Int);
                parameters[4] = new SqlParameter("@isimanahtar", SqlDbType.NVarChar, 100);
                parameters[5] = new SqlParameter("@varyantkodlari", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[8] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[10] = new SqlParameter("@sablonkod", SqlDbType.NVarChar, 50);
                parameters[11] = new SqlParameter("@isimanahtaring", SqlDbType.NVarChar, 100);

                parameters[0].Value = yeniVaryant.UrunGrubuKodu;
                parameters[1].Value = yeniVaryant.ModelKodu;
                parameters[2].Value = yeniVaryant.SatisSekilKodu;
                parameters[3].Value = yeniVaryant.OzellikSayisi;
                parameters[4].Value = yeniVaryant.IsimAnahtar;
                parameters[5].Value = varyantKodlari;
                parameters[6].Value = yeniVaryant.Kod1;
                parameters[7].Value = yeniVaryant.Kod2;
                parameters[8].Value = yeniVaryant.Kod3;
                parameters[9].Value = yeniVaryant.Kod4;
                parameters[10].Value = yeniVaryant.SablonKod;
                parameters[11].Value = yeniVaryant.IngilizceIsimAnahtar;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertOption(Cls_Urun yeniVaryant)
        {
            try
            {
                if (yeniVaryant == null)
                    return false;

                yeniVaryant.OzellikTipi = GetOzellikKoduFromOzellikIsmi(yeniVaryant.OzellikIsmi);
                if (string.IsNullOrEmpty(yeniVaryant.OzellikTipi))
                    return false;

                Dictionary<int, int> kodBaslangicUzunluk = GetKodBaslangicUzunluk(yeniVaryant);

                if (kodBaslangicUzunluk.Keys.Contains(0) ||
                    kodBaslangicUzunluk.Values.Contains(0))
                    return false;
                yeniVaryant.BaslangicSira = kodBaslangicUzunluk.Keys.FirstOrDefault();
                yeniVaryant.KodUzunluk = kodBaslangicUzunluk.Values.FirstOrDefault();

                SqlParameter[] parameters = new SqlParameter[10];

                parameters[0] = new SqlParameter("@grupkodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[3] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[4] = new SqlParameter("@ozkod", SqlDbType.NVarChar, 4);
                parameters[5] = new SqlParameter("@ozisim", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@baslangic", SqlDbType.Int);
                parameters[7] = new SqlParameter("@uzunluk", SqlDbType.Int);
                parameters[8] = new SqlParameter("@recdeg", SqlDbType.NVarChar, 10);
                parameters[9] = new SqlParameter("@kisakod", SqlDbType.NVarChar, 100);

                parameters[0].Value = yeniVaryant.UrunGrubuKodu;
                parameters[1].Value = yeniVaryant.ModelKodu;
                parameters[2].Value = yeniVaryant.SatisSekilKodu;
                parameters[3].Value = yeniVaryant.OzellikSayisi;
                parameters[4].Value = yeniVaryant.OzellikTipi;
                parameters[5].Value = yeniVaryant.VaryantIsmi;
                parameters[6].Value = yeniVaryant.BaslangicSira;
                parameters[7].Value = yeniVaryant.KodUzunluk;
                parameters[8].Value = yeniVaryant.ReceteDegeri is null ? "" :yeniVaryant.ReceteDegeri;
                parameters[9].Value = yeniVaryant.KisaKod is null ? "" : yeniVaryant.KisaKod;

                Variables.Query_ = "insert into stbmodelopsiyon values (@grupkodu,@modelkodu,@satissekilkodu,@sira,@ozkod,@ozisim,'H','H','',@baslangic,@uzunluk,@recdeg,@kisakod,'') ";


                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                if (!Variables.Result_)
                    return false;

                Variables.Result_ = UpdateBaslangicUzunluk(yeniVaryant);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateStokAdiIfVaryantExists(string mamulKodu, string mamulAdi, string mamulAdiIngilizce)
        {
            try
            {
                Query = $"Select stok_kodu from tblstsabit where stok_kodu='{mamulKodu}'";
                dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                
                    
                    if (dataTable.Rows.Count == 0)
                {

                    return false;
                }

                else
                {

                    Query = $"Select stok_kodu from tblstsabitek where stok_kodu='{mamulKodu}'";
                    dataTable = dataLayer.Select_Command(Query, Variables.Yil_);
                    

                    if (dataTable.Rows.Count == 0)
                    {

                        Query = $"Update tblstsabit set stok_adi='{mamulAdi}' where stok_kodu='{mamulKodu}' ";
                        dataLayer.Update_Statement(Query, Variables.Yil_);
                        
                        variables.ErrorMessage = "Ürünün Stok Kartı Bulunmakta Ancak Ek Bilgileri \n Mevcut Olmadığından İngilizce İsim Kaydedilemedi.";
                        MessageBox.Show(variables.ErrorMessage);

                        return true;
                    }
                    else
                    {
                        Query = $"Update tblstsabit set stok_adi='{mamulAdi}' where stok_kodu='{mamulKodu}' ";
                        Query = Query + $"Update tblstsabitek set ingisim='{mamulAdiIngilizce}' where stok_kodu='{mamulKodu}' ";

                        dataLayer.Update_Statement(Query, Variables.Yil_);
                        return true;
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public async Task<int> UpdateUrunAdi(ObservableCollection<Cls_Urun> urunColl,bool digerVaryantlariDaBildir)
        {
            try
            {
              
                if (urunColl.Count == 0) { return 0; }

                foreach(Cls_Urun urun in urunColl) 
                { 
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[1] = new SqlParameter("@tumVaryantlariGuncelle",SqlDbType.Bit);
                    parameters[2] = new SqlParameter("@userName", SqlDbType.NVarChar,12);
                    parameters[0].Value = urun.UrunKodu;
                    parameters[1].Value = digerVaryantlariDaBildir;
                    parameters[2].Value = login.GetUserName();

                    Variables.Result_ = await dataLayer.ExecuteStoredProcedureWithParametersAsync("vbpUpdateVaryantName", Variables.Yil_, parameters);

                }
               if(Variables.Result_)
                return 1;

               return -1;
            }
            catch 
            {
                return -1;
            }

        }
        public bool UpdateOpsiyonAdedi(Cls_Urun itemToUpdate)
        {
            try
            {
                if (itemToUpdate == null)
                {
                    return false;
                }
               
                Variables.Query_ = "update stbmodelcapraz set ozsay=@opsiyonAdedi where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";
               
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[3] = new SqlParameter("@opsiyonAdedi", SqlDbType.Int);
                parameters[0].Value = itemToUpdate.UrunGrubuKodu;
                parameters[1].Value = itemToUpdate.ModelKodu;
                parameters[2].Value = itemToUpdate.SatisSekilKodu;
                parameters[3].Value = itemToUpdate.OzellikSayisi;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                
                return Variables.Result_;

            }
            catch
            {
                return false;
            }
        }
        public bool UpdateOpsiyonAdediBy1(Cls_Urun itemToUpdate, bool isItemToAdd)
        {
            try
            {
                if (itemToUpdate == null)
                {
                    return false;
                }
                if (isItemToAdd)
                    Variables.Query_ = "update stbmodelcapraz set ozsay=ozsay+1 where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";
                else
                    Variables.Query_ = "update stbmodelcapraz set ozsay=ozsay-1 where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[0].Value = itemToUpdate.UrunGrubuKodu;
                parameters[1].Value = itemToUpdate.ModelKodu;
                parameters[2].Value = itemToUpdate.SatisSekilKodu;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                
                return Variables.Result_;

            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateOpsiyonSiraRecDegRearranged(Cls_Urun itemToUpdate, int sira)
        {
            try
            {
                if (itemToUpdate == null)
                {
                    return false;
                }

                string recdeg = string.Format("@o{0}",sira - 1);

                Variables.Query_ = "update stbmodelopsiyon set sira=sira-1, recdeg=@recdeg where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu and sira=sira";

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[3] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[4] = new SqlParameter("@recdeg", SqlDbType.NVarChar,10);
                parameters[0].Value = itemToUpdate.UrunGrubuKodu;
                parameters[1].Value = itemToUpdate.ModelKodu;
                parameters[2].Value = itemToUpdate.SatisSekilKodu;
                parameters[3].Value = sira;
                parameters[4].Value = recdeg;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);

                return Variables.Result_;

            }
            catch
            {
                return false;
            }
        }
        public bool UpdateBaslangicUzunluk(Cls_Urun itemToUpdate)
        {
            try
            {
                if (itemToUpdate == null)
                {
                    return false;
                }

                Variables.Query_ = "select sira,baslangic,uzunluk from stbmodelopsiyon where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu order by sira";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[1] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[2] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[0].Value = itemToUpdate.UrunGrubuKodu;
                parameters[1].Value = itemToUpdate.ModelKodu;
                parameters[2].Value = itemToUpdate.SatisSekilKodu;

                ObservableCollection<Cls_Urun> kodSiraColl = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Urun model = new();
                    model.OzellikSayisi = reader["sira"] is DBNull ? 0 : Convert.ToInt32(reader["sira"]);
                    model.BaslangicSira = reader["baslangic"] is DBNull ? 0 : Convert.ToInt32(reader["baslangic"]);
                    model.KodUzunluk = reader["uzunluk"] is DBNull ? 0 : Convert.ToInt32(reader["uzunluk"]);
                    return model;
                });

                if (kodSiraColl == null)
                    return false;
                if (kodSiraColl.Count == 0)
                    return false;

                int baslangic = 1;
                int uzunluk = 0;
                bool isUzunlukChanged = false;
                for (int i = 1; i <= kodSiraColl.Count; i++)
                {
                    Cls_Urun kodSiraItem = kodSiraColl.Where(n => n.OzellikSayisi == i).FirstOrDefault();
                    if (isUzunlukChanged == false)
                    {
                        if (kodSiraItem.OzellikSayisi == itemToUpdate.OzellikSayisi)
                        {

                            Variables.Query_ = "update stbmodelopsiyon set uzunluk = @uzunluk where grupkod=@urungrupkodu and modelkod=@modelkodu and ssekli=@satissekilkodu and sira = @sira";

                            SqlParameter[] parametersUpdExs = new SqlParameter[5];
                            parametersUpdExs[0] = new SqlParameter("@uzunluk", SqlDbType.Int);
                            parametersUpdExs[1] = new SqlParameter("@urungrupkodu", SqlDbType.NVarChar, 2);
                            parametersUpdExs[2] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                            parametersUpdExs[3] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                            parametersUpdExs[4] = new SqlParameter("@sira", SqlDbType.Int);
                            parametersUpdExs[0].Value = itemToUpdate.KodUzunluk;
                            parametersUpdExs[1].Value = itemToUpdate.UrunGrubuKodu;
                            parametersUpdExs[2].Value = itemToUpdate.ModelKodu;
                            parametersUpdExs[3].Value = itemToUpdate.SatisSekilKodu;
                            parametersUpdExs[4].Value = itemToUpdate.OzellikSayisi;

                            Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersUpdExs);

                            if (!Variables.Result_)
                                return false;

                            baslangic = baslangic + itemToUpdate.KodUzunluk;
                            isUzunlukChanged = true;
                        }
                        else
                            baslangic = baslangic + kodSiraItem.KodUzunluk;
                    }
                    else
                    {
                        kodSiraItem.BaslangicSira = baslangic;
                        Variables.Query_ = "update stbmodelopsiyon set baslangic = @baslangic where grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu and sira = @sira";

                        SqlParameter[] parametersUpdExs = new SqlParameter[5];
                        parametersUpdExs[0] = new SqlParameter("@baslangic", SqlDbType.Int);
                        parametersUpdExs[1] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                        parametersUpdExs[2] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                        parametersUpdExs[3] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                        parametersUpdExs[4] = new SqlParameter("@sira", SqlDbType.Int);
                        parametersUpdExs[0].Value = baslangic;
                        parametersUpdExs[1].Value = itemToUpdate.UrunGrubuKodu;
                        parametersUpdExs[2].Value = itemToUpdate.ModelKodu;
                        parametersUpdExs[3].Value = itemToUpdate.SatisSekilKodu;
                        parametersUpdExs[4].Value = itemToUpdate.OzellikSayisi;

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersUpdExs);

                        if (!Variables.Result_)
                            return false;

                        baslangic = baslangic + kodSiraItem.KodUzunluk;

                    }
                }
                return true;

            }
            catch
            {
                return false;
            }
        }
        public bool UpdateVaryantOlustur(Cls_Urun guncellenecekVaryant)
        {
            try
            {
                
                Variables.Query_ = "update stbmodelcapraz set ozsay=@ozsay,isimanahtar=@isimanahtar,isimanahtaring=@isimanahtaring,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@sablonkod,iapaket=@varyantkodlari " +
                                   "where grupkod=@grupkodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                string varyantKodlari = string.Empty;
                for(int i=1;i<=guncellenecekVaryant.OzellikSayisi;i++)
                    varyantKodlari += string.Format("@o{0}", i); 
                
                SqlParameter[] parameters = new SqlParameter[12];

                parameters[0] = new SqlParameter("@ozsay", SqlDbType.Int);
                parameters[1] = new SqlParameter("@isimanahtar", SqlDbType.NVarChar, 100);
                parameters[2] = new SqlParameter("@isimanahtaring", SqlDbType.NVarChar, 100);
                parameters[3] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[4] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[5] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@sablonkod", SqlDbType.NVarChar, 50);
                parameters[8] = new SqlParameter("@varyantkodlari", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@grupkodu", SqlDbType.NVarChar, 2);
                parameters[10] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[11] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);

                parameters[0].Value = guncellenecekVaryant.OzellikSayisi;
                parameters[1].Value = guncellenecekVaryant.IsimAnahtar;
                parameters[2].Value = guncellenecekVaryant.IngilizceIsimAnahtar;
                parameters[3].Value = guncellenecekVaryant.Kod1;
                parameters[4].Value = guncellenecekVaryant.Kod2;
                parameters[5].Value = guncellenecekVaryant.Kod3;
                parameters[6].Value = guncellenecekVaryant.Kod4;
                parameters[7].Value = guncellenecekVaryant.SablonKod;
                parameters[8].Value = varyantKodlari;
                parameters[9].Value = guncellenecekVaryant.UrunGrubuKodu;
                parameters[10].Value = guncellenecekVaryant.ModelKodu;
                parameters[11].Value = guncellenecekVaryant.SatisSekilKodu;

                 Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateUrunOzellik(Cls_Urun dataItem, string secilenOzellikTipi)
        {
            try
            {
                if (secilenOzellikTipi == "Ürün Grup")
                { 
                    Variables.Query_ = "update stburungrup set isim=@isim,tur=@tur,takimkod=@takimkod,unitekod=@unitekod,sira=@sira,kilit=@kilit,sayfa=@sayfa,kdvo=@kdvo " +
                                       ",muhdetay=@muhdetay,menugrup=@menugrup,teslimgunu=@teslimgunu,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@kod5 " +
                                       "where kod=@kod";

                    SqlParameter[] parameters = new SqlParameter[17];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 2);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                    parameters[2] = new SqlParameter("@tur", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@takimkod", SqlDbType.NVarChar, 3);
                    parameters[4] = new SqlParameter("@unitekod", SqlDbType.NVarChar, 2);
                    parameters[5] = new SqlParameter("@sira",SqlDbType.Int);
                    parameters[6] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[7] = new SqlParameter("@sayfa", SqlDbType.NVarChar, 50);
                    parameters[8] = new SqlParameter("@kdvo", SqlDbType.Float);
                    parameters[9] = new SqlParameter("@muhdetay", SqlDbType.Int);
                    parameters[10] = new SqlParameter("@menugrup", SqlDbType.NVarChar, 50);
                    parameters[11] = new SqlParameter("@teslimgunu", SqlDbType.Int);
                    parameters[12] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                    parameters[13] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                    parameters[14] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                    parameters[15] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                    parameters[16] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                    parameters[0].Value = dataItem.UrunGrubuKodu;
                    parameters[1].Value = dataItem.UrunGrubuIsim;
                    parameters[2].Value = dataItem.UrunGrubuTur;
                    parameters[3].Value = dataItem.TakimKodu;
                    parameters[4].Value = dataItem.UniteKod;
                    parameters[5].Value = dataItem.UrunGrubuSira;
                    parameters[6].Value = dataItem.Kilit;
                    parameters[7].Value = dataItem.Sayfa;
                    parameters[8].Value = dataItem.Kdv;
                    parameters[9].Value = dataItem.Muhdetay;
                    parameters[10].Value = dataItem.MenuGrup;
                    parameters[11].Value = dataItem.TeslimGunu;
                    parameters[12].Value = dataItem.Kod1;
                    parameters[13].Value = dataItem.Kod2;
                    parameters[14].Value = dataItem.Kod3;
                    parameters[15].Value = dataItem.Kod4;
                    parameters[16].Value = dataItem.Kod5;

                     variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }

                if (secilenOzellikTipi == "Model")
                {
                    Variables.Query_ = "update stbmodel set isim=@isim,sira=@sira,kilit=@kilit,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@kod5 " +
                                        "where kod=@kod";

                    SqlParameter[] parameters = new SqlParameter[9];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 3);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                    parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[4] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                    parameters[5] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                    parameters[6] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                    parameters[7] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                    parameters[8] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                    parameters[0].Value = dataItem.ModelKodu;
                    parameters[1].Value = dataItem.ModelIsim;
                    parameters[2].Value = dataItem.ModelSira;
                    parameters[3].Value = dataItem.ModelKilit;
                    parameters[4].Value = dataItem.ModelKod1;
                    parameters[5].Value = dataItem.ModelKod2;
                    parameters[6].Value = dataItem.ModelKod3;
                    parameters[7].Value = dataItem.ModelKod4;
                    parameters[8].Value = dataItem.ModelKod5;

                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }

                if (secilenOzellikTipi == "Satış Şekil")
                {
                    Variables.Query_ = "update stbsatissekil set isim=@isim,sira=@sira,kilit=@kilit,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@kod5 " +
                                        "where kod=@kod";


                    SqlParameter[] parameters = new SqlParameter[9];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 2);
                    parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                    parameters[2] = new SqlParameter("@sira", SqlDbType.Int);
                    parameters[3] = new SqlParameter("@kilit", SqlDbType.NVarChar, 1);
                    parameters[4] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                    parameters[5] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                    parameters[6] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                    parameters[7] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                    parameters[8] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                    parameters[0].Value = dataItem.SatisSekilKodu;
                    parameters[1].Value = dataItem.SatisSekilIsim;
                    parameters[2].Value = dataItem.SatisSekilSira;
                    parameters[3].Value = dataItem.SatisSekilKilit;
                    parameters[4].Value = dataItem.SatisSekilKod1;
                    parameters[5].Value = dataItem.SatisSekilKod2;
                    parameters[6].Value = dataItem.SatisSekilKod3;
                    parameters[7].Value = dataItem.SatisSekilKod4;
                    parameters[8].Value = dataItem.SatisSekilKod5;

                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }

                    return variables.Result;
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateUrunOzellikKisit(ObservableCollection<Cls_Urun> kisitColl,string urunGrubuKodu,string modelKodu,string satisSekilKodu, int ozellikSira)
        {
            try
            {

                if (kisitColl == null)
                    return false;

                if(kisitColl.Count == 0)
                {
                    Variables.Query_ = "delete from stbmodelozkisit where ugrupkod=@urunGrubuKodu and modelkod=@modelKodu and ssekli=@satisSekilKodu and sira=@ozellikSira";
                    SqlParameter[] parametersDel = new SqlParameter[4];
                    parametersDel[0] = new SqlParameter("@urunGrubuKodu", SqlDbType.NVarChar, 2);
                    parametersDel[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                    parametersDel[2] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 2);
                    parametersDel[3] = new SqlParameter("@ozellikSira", SqlDbType.Int);
                    parametersDel[0].Value = urunGrubuKodu;
                    parametersDel[1].Value = modelKodu;
                    parametersDel[2].Value = satisSekilKodu;
                    parametersDel[3].Value = ozellikSira;

                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersDel);
                }
                else
                {

                    Variables.Query_ = "delete from stbmodelozkisit where ugrupkod=@urunGrubuKodu and modelkod=@modelKodu and ssekli=@satisSekli and sira=@ozellikSira";
                    SqlParameter[] parametersDel = new SqlParameter[4];
                    parametersDel[0] = new SqlParameter("@urunGrubuKodu", SqlDbType.NVarChar, 2);
                    parametersDel[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                    parametersDel[2] = new SqlParameter("@satisSekli", SqlDbType.NVarChar, 2);
                    parametersDel[3] = new SqlParameter("@ozellikSira", SqlDbType.Int);
                    parametersDel[0].Value = urunGrubuKodu;
                    parametersDel[1].Value = modelKodu;
                    parametersDel[2].Value = satisSekilKodu;
                    parametersDel[3].Value = ozellikSira;

                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersDel);

                    foreach (Cls_Urun item in kisitColl)
                    {
                        Variables.Query_ = "insert into stbmodelozkisit values(@urunGrubuKodu,@modelKodu,@satisSekli,@ozellikSira,@ozKisit)";
                        SqlParameter[] parametersUpd = new SqlParameter[5];
                        parametersUpd[0] = new SqlParameter("@urunGrubuKodu", SqlDbType.NVarChar, 2);
                        parametersUpd[1] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                        parametersUpd[2] = new SqlParameter("@satisSekli", SqlDbType.NVarChar, 2);
                        parametersUpd[3] = new SqlParameter("@ozellikSira", SqlDbType.Int);
                        parametersUpd[4] = new SqlParameter("@ozKisit", SqlDbType.NVarChar, 50);
                        parametersUpd[0].Value = item.UrunGrubuKodu;
                        parametersUpd[1].Value = item.ModelKodu;
                        parametersUpd[2].Value = item.SatisSekilKodu;
                        parametersUpd[3].Value = item.OzellikSayisi;
                        parametersUpd[4].Value = item.Koddetay;
                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersUpd);
                        if (!Variables.Result_)
                            return false;
                    }
                }
                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateIsimAnahtar(Cls_Urun item)
        {
            try
            {
                Variables.Query_ = "update stbmodelcapraz set isimanahtar=@isimanahtar,isimanahtaring=@isimanahtaring" +
                                    " where grupkod=@urunGrubuKodu and modelkod=@modelKodu and ssekli=@satisSekilKodu";

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@isimanahtar", SqlDbType.NVarChar, 100);
                parameters[1] = new SqlParameter("@isimanahtaring", SqlDbType.NVarChar, 100);
                parameters[2] = new SqlParameter("@urunGrubuKodu", SqlDbType.NVarChar,2);
                parameters[3] = new SqlParameter("@modelKodu", SqlDbType.NVarChar, 3);
                parameters[4] = new SqlParameter("@satisSekilKodu", SqlDbType.NVarChar, 2);
                
                parameters[0].Value = item.IsimAnahtar;
                parameters[1].Value = item.IngilizceIsimAnahtar;
                parameters[2].Value = item.UrunGrubuKodu;
                parameters[3].Value = item.ModelKodu;
                parameters[4].Value = item.SatisSekilKodu;
                
                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
      
                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateOpsiyon(Cls_Urun rowItem)
        {
            try
            {
                //get real özkod
                rowItem.OzellikTipi = GetOzellikKoduFromOzellikIsmi(rowItem.OzellikIsmi);

                Variables.Query_ = "update stbmodelopsiyon set ozkod=@ozellikKodu, ozisim=@varyantIsmi,kisakod=@kisaKod,recdeg=@recdeg " +
                                    "where sira=@ozellikSayisi and grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@ozellikKodu", SqlDbType.NVarChar, 4);
                parameters[1] = new SqlParameter("@varyantIsmi", SqlDbType.NVarChar, 50);
                parameters[2] = new SqlParameter("@kisaKod", SqlDbType.NVarChar, 100);
                parameters[3] = new SqlParameter("@recdeg", SqlDbType.NVarChar, 10);
                parameters[4] = new SqlParameter("@ozellikSayisi", SqlDbType.Int);
                parameters[5] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[6] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[7] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[0].Value = rowItem.OzellikTipi;
                parameters[1].Value = rowItem.VaryantIsmi;
                parameters[2].Value = rowItem.KisaKod;
                parameters[3].Value = rowItem.ReceteDegeri;
                parameters[4].Value = rowItem.OzellikSayisi;
                parameters[5].Value = rowItem.UrunGrubuKodu;
                parameters[6].Value = rowItem.ModelKodu;
                parameters[7].Value = rowItem.SatisSekilKodu;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateOzMas(Cls_Urun urunOzellik)
        {
            try
            {
                if (urunOzellik == null) return false;

                Variables.Query_ = "update stbozmas set " +
                              "isim=@isim,uzunluk=@uzunluk,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@kod5 where kod=@kod";

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[2] = new SqlParameter("@uzunluk", SqlDbType.Int);
                parameters[3] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[4] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[5] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);

                parameters[0].Value = urunOzellik.OzellikTipi;
                parameters[1].Value = urunOzellik.OzellikIsmi;
                parameters[2].Value = urunOzellik.KodUzunluk;
                parameters[3].Value = urunOzellik.Kod1;
                parameters[4].Value = urunOzellik.Kod2;
                parameters[5].Value = urunOzellik.Kod3;
                parameters[6].Value = urunOzellik.Kod4;
                parameters[7].Value = urunOzellik.Kod5;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateOzDetay(Cls_Urun urunOzellik)
        {
            try
            {
                if (urunOzellik == null) return false;

                Variables.Query_ = "update stbozdetay set " +
                              "kod=@kod,isim=@isim,ingisim=@ingisim,kilit=@kilit,kod1=@kod1,kod2=@kod2,kod3=@kod3,kod4=@kod4,kod5=@kod5 where maskod=@maskod and sira=@sira";

                SqlParameter[] parameters = new SqlParameter[11];

                parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 10);
                parameters[1] = new SqlParameter("@isim", SqlDbType.NVarChar, 50);
                parameters[2] = new SqlParameter("@ingisim", SqlDbType.NVarChar, 50);
                parameters[3] = new SqlParameter("@kilit", SqlDbType.NVarChar,1);
                parameters[4] = new SqlParameter("@kod1", SqlDbType.NVarChar, 50);
                parameters[5] = new SqlParameter("@kod2", SqlDbType.NVarChar, 50);
                parameters[6] = new SqlParameter("@kod3", SqlDbType.NVarChar, 50);
                parameters[7] = new SqlParameter("@kod4", SqlDbType.NVarChar, 50);
                parameters[8] = new SqlParameter("@kod5", SqlDbType.NVarChar, 50);
                parameters[9] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[10] = new SqlParameter("@maskod", SqlDbType.NVarChar,4);

                parameters[0].Value = urunOzellik.Koddetay;
                parameters[1].Value = urunOzellik.KoddetayIsim;
                parameters[2].Value = urunOzellik.KoddetayIsimIng;
                parameters[3].Value = urunOzellik.Kilit;
                parameters[4].Value = urunOzellik.Kod1;
                parameters[5].Value = urunOzellik.Kod2;
                parameters[6].Value = urunOzellik.Kod3;
                parameters[7].Value = urunOzellik.Kod4;
                parameters[8].Value = urunOzellik.Kod5;
                parameters[9].Value = urunOzellik.KisitSira;
                parameters[10].Value = urunOzellik.OzellikTipi;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOpsiyon(Cls_Urun rowItem, ObservableCollection<Cls_Urun> urunColl)
        {
            try
            {
                //get real özkod
                rowItem.OzellikTipi = GetOzellikKoduFromOzellikIsmi(rowItem.OzellikIsmi);

                Variables.Query_ = "delete from stbmodelopsiyon " +
                                    "where sira=@ozellikSayisi and grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@ozellikSayisi", SqlDbType.Int);
                parameters[1] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                parameters[2] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                parameters[3] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                parameters[0].Value = rowItem.OzellikSayisi;
                parameters[1].Value = rowItem.UrunGrubuKodu;
                parameters[2].Value = rowItem.ModelKodu;
                parameters[3].Value = rowItem.SatisSekilKodu;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                if (!Variables.Result_)
                    return false;

                foreach (Cls_Urun item in urunColl)
                {
                    if (item.OzellikSayisi > rowItem.OzellikSayisi)
                    {
                        int yeniSira = item.OzellikSayisi - 1;
                        string lastPart = item.ReceteDegeri.Substring(2, item.ReceteDegeri.Length - 2);
                        string yeniRecdeg = string.Format("@o{0}", (Convert.ToInt32(lastPart) - 1).ToString());

                        Variables.Query_ = "update stbmodelopsiyon set sira = @yeniSira,recdeg=@yeniRecDeg " +
                                   "where sira=@ozellikSayisi and grupkod=@urungrubukodu and modelkod=@modelkodu and ssekli=@satissekilkodu";

                        SqlParameter[] parametersUpd = new SqlParameter[6];
                        parametersUpd[0] = new SqlParameter("@yeniSira", SqlDbType.Int);
                        parametersUpd[1] = new SqlParameter("@yeniRecDeg", SqlDbType.NVarChar, 10);
                        parametersUpd[2] = new SqlParameter("@ozellikSayisi", SqlDbType.Int);
                        parametersUpd[3] = new SqlParameter("@urungrubukodu", SqlDbType.NVarChar, 2);
                        parametersUpd[4] = new SqlParameter("@modelkodu", SqlDbType.NVarChar, 3);
                        parametersUpd[5] = new SqlParameter("@satissekilkodu", SqlDbType.NVarChar, 2);
                        parametersUpd[0].Value = yeniSira;
                        parametersUpd[1].Value = yeniRecdeg;
                        parametersUpd[2].Value = item.OzellikSayisi;
                        parametersUpd[3].Value = rowItem.UrunGrubuKodu;
                        parametersUpd[4].Value = rowItem.ModelKodu;
                        parametersUpd[5].Value = rowItem.SatisSekilKodu;

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parametersUpd);
                        if (!Variables.Result_)
                            return Variables.Result_;
                    }
                }

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOzellik(Cls_Urun urunOzellik, string ozellikTipi)
        {
            try
            {
                if (ozellikTipi == "Ürün Grup")
                {
                    Variables.Query_ = "delete from stburungrup where kod=@kod";

                    SqlParameter[] parameters = new SqlParameter[1];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 2);
                    parameters[0].Value = urunOzellik.UrunGrubuKodu;


                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }
                if (ozellikTipi == "Model")
                {
                    Variables.Query_ = "delete from stbmodel where kod=@kod";

                    SqlParameter[] parameters = new SqlParameter[1];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 3);
                    parameters[0].Value = urunOzellik.ModelKodu;


                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }
                if (ozellikTipi == "Satış Şekil")
                {
                    Variables.Query_ = "delete from stbsatissekil where kod=@kod";

                    SqlParameter[] parameters = new SqlParameter[1];

                    parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 2);
                    parameters[0].Value = urunOzellik.SatisSekilKodu;


                    variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                }

                return variables.Result;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOzellikMas(string ozellikTipi)
        {
            try
            {
                if (string.IsNullOrEmpty(ozellikTipi))
                    return false;
               
                 Variables.Query_ = "delete from stbozmas where kod=@kod";

                 SqlParameter[] parameters = new SqlParameter[1];

                 parameters[0] = new SqlParameter("@kod", SqlDbType.NVarChar, 4);
                 parameters[0].Value = ozellikTipi;


                 Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOzDetay(Cls_Urun urunOzellik)
        {
            try
            {
                if (urunOzellik == null) return false;

                Variables.Query_ = "delete from stbozdetay where maskod=@maskod and sira=@sira ";
                
                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter("@sira", SqlDbType.Int);
                parameters[1] = new SqlParameter("@maskod", SqlDbType.NVarChar, 4);

                parameters[0].Value = urunOzellik.KisitSira;
                parameters[1].Value = urunOzellik.OzellikTipi;

                Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyChanged)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
		}

        

	}
}

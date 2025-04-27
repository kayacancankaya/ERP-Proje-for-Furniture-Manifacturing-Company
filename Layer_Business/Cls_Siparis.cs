using Layer_2_Common.Type;
using Layer_Data;
using Microsoft.VisualBasic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Layer_Business
{
    public class Cls_Siparis : Cls_Base, INotifyPropertyChanged
    {
        private string _stokKodu = "";

        public int TalepSira { get; set; }

        public string StokKodu
        {
            get { return _stokKodu; }
            set { _stokKodu = value;
                OnPropertyChanged(nameof(StokKodu));
            }
        }
        private string _stokAdi = "";

        public string StokAdi
        {
            get { return _stokAdi; }
            set {
                _stokAdi = value;
                OnPropertyChanged(nameof(StokAdi));
            }
        }

        private string _fisno = "";

        public string Fisno
        {
            get { return _fisno; }
            set { _fisno = value;
                OnPropertyChanged(nameof(Fisno));
            }
        }
        private int _fisSira;

        public int FisSira
        {
            get { return _fisSira; }
            set {
                _fisSira = value;
                OnPropertyChanged(nameof(FisSira));
            }
        }

        public double SatisFiyat { get; set; }
        private int _siparisMiktar = 0;
        public int SiparisMiktar
        {
            get { return _siparisMiktar; }
            set {
                _siparisMiktar = value;
                OnPropertyChanged(nameof(SiparisMiktar));
                OnPropertyChanged(nameof(SiparisToplamFiyat));
            }
        }

        private int _siparisTeslimMiktar;

        public int SiparisTeslimMiktar
        {
            get { return _siparisTeslimMiktar; }
            set {
                _siparisTeslimMiktar = value;
                OnPropertyChanged(nameof(SiparisTeslimMiktar));
            }
        }
        private int _siparisBakiye;

        //satış irsaliyesi giriş için kullanılan miktar
        public int SatisSiparisGirisMiktar { get; set; }
        public int SiparisBakiye
        {
            get { return _siparisBakiye; }
            set {
                _siparisBakiye = value;
                OnPropertyChanged(nameof(SiparisBakiye));
            }
        }

        private double _dovizFiyat = 0;

        public double DovizFiyat
        {
            get { return _dovizFiyat; }
            set {
                _dovizFiyat = value;
                OnPropertyChanged(nameof(DovizFiyat));
            }
        }
        private double _siparisFiyat = 0;
        public double SiparisFiyat
        {
            get { return _siparisFiyat; }
            set {
                _siparisFiyat = value;
                OnPropertyChanged(nameof(SiparisFiyat));
                OnPropertyChanged(nameof(SiparisToplamFiyat));
            }
        }
        public double SiparisToplamFiyat
        {
            get
            {
                return SiparisFiyat * SiparisMiktar;
            }
        }
        private double _siparisToplamTutar;

        public double SiparisToplamTutar
        {
            get { return _siparisToplamTutar; }
            set { _siparisToplamTutar = value; }
        }


        private double _siparisToplamKalanTutar;

        public double SiparisToplamKalanTutar
        {
            get { return _siparisToplamKalanTutar; }
            set { _siparisToplamKalanTutar = value; }
        }

        public double ToplamSiparisKalemi { get; set; }

        public double SiparisToplamKDV { get; set; }


        private string _siparisTarih;

        public string SiparisTarih
        {
            get { return _siparisTarih; }
            set { _siparisTarih = value; }
        }

        public DateTime SiparisTarihD { get; set; }

        private string _siparisDurum = "H";

        public string SiparisDurum
        {
            get { return _siparisDurum; }
            set { _siparisDurum = value;
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


        private string _destinasyon = "";

        public string Destinasyon
        {
            get { return _destinasyon; }
            set {
                _destinasyon = value;
                OnPropertyChanged(nameof(Destinasyon));
            }
        }

        private string _siparisAciklama = "";

        public string SiparisAciklama
        {
            get { return _siparisAciklama; }
            set {
                _siparisAciklama = value;
                OnPropertyChanged(nameof(SiparisAciklama));
            }
        }
        private string _siparisSatirAciklama = "";

        public string SiparisSatirAciklama
        {
            get { return _siparisSatirAciklama; }
            set {
                _siparisSatirAciklama = value;
                OnPropertyChanged(nameof(SiparisSatirAciklama));
            }
        }
        private string _POnumarasi = "";

        public string POnumarasi
        {
            get { return _POnumarasi; }
            set {
                _POnumarasi = value;
                OnPropertyChanged(nameof(POnumarasi));
            }
        }


        private string _dovizTipi;

        public string DovizTipi
        {
            get { return _dovizTipi; }
            set
            {
                _dovizTipi = value;
                OnPropertyChanged(nameof(DovizTipi));
            }
        }
        public int DovizTipiInt { get; set; }

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
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public string Kod5 { get; set; }
        public string TalepAciklama { get; set; } = "Lütfen Açıklama Giriniz...";
        public string TalepGenelAciklama { get; set; } = "Lütfen Açıklama Giriniz...";
        public int MyProperty { get; set; }
        public float TalepSimulasyonIhtiyacMiktar { get; set; } = 0;
        private double _stokKDV;

        public double StokKDV
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

        private string _varyantVarMi = "";

        public string VaryantVarMi
        {
            get { return _varyantVarMi; }
            set
            {
                _varyantVarMi = value;
                OnPropertyChanged(nameof(VaryantVarMi));
            }
        }


        public double TedarikSiparisMiktar { get; set; }
        public double TedarikTeslimMiktar { get; set; }
        public double TedarikTalepBakiye { get; set; }
        public double TedarikSiparisBakiye { get; set; }
        public double TedarikGirisMiktar { get; set; }
        public double Depo10Bakiye { get; set; }
        public double Depo15Bakiye { get; set; }
        public double Depo30Bakiye { get; set; }
        public double Depo35Bakiye { get; set; }
        public double Depo40Bakiye { get; set; }
        public double Depo45Bakiye { get; set; }

        public DateTime TeminTarih { get; set; }
        public int TeminSuresi { get; set; }
        public double MinimumSiparisMiktar { get; set; }
        public int Vade { get; set; }

        public int DepoKodu { get; set; }
        public int DepoMiktar { get; set; }
        public int UretilenMiktar { get; set; }

        public string Birim { get; set; }
        public string PlanAdi { get; set; }
        public float IEihtiyac { get; set; }
        public float ToplamTalepMiktar { get; set; }
        public float ToplamSiparisMiktar { get; set; }
        public float PlanTalepMiktar { get; set; }
        public float PlanAcikSiparisMiktar { get; set; }
        public float GonderilecekMiktar { get; set; } = 0;

        private Cls_Cari _associatedCari = new Cls_Cari();

        public Cls_Cari AssociatedCari
        {
            get { return _associatedCari; }
            set
            {
                if (value != null)
                {
                    _associatedCari = value;
                    OnPropertyChanged(nameof(AssociatedCari));
                }
                else
                {
                    throw new ArgumentException("Sipariş için Cari Oluşturulurken Hata İle Karşılaşıldı.");
                }
            }
        }

        private bool _doesUrunAgaciExists = false;
        public bool DoesUrunAgaciExists
        {
            get { return _doesUrunAgaciExists; }
            set
            {
                _doesUrunAgaciExists = value;
                OnPropertyChanged(nameof(DoesUrunAgaciExists));
            }
        }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }


        private ObservableCollection<Cls_Siparis> _siparisCollection = new();
        private ObservableCollection<Cls_Siparis> temp_coll_sip = new();
        public ObservableCollection<Cls_Siparis> SiparisDetayCollection = new();

        public ObservableCollection<Cls_Siparis> SiparisCollection
        {
            get { return _siparisCollection; }
            set
            {
                if (value != null)
                {
                    _siparisCollection = value;
                    OnPropertyChanged(nameof(SiparisCollection));
                }
                else
                {
                    throw new ArgumentException("Sipariş Oluşturulurken Hata İle Karşılaşıldı.");
                }
            }
        }

        DataLayer dataLayer = new();
        DataTable dataTable = new();
        Variables variables = new();
        LoginLogic login = new();
        int veriYili = 0;
        public Cls_Siparis()
        {
            variables.Fabrika = login.GetFabrika();

            Variables.Fabrika_ = variables.Fabrika;
            veriYili = Variables.Yil_;
        }

        //vita için fişno üret P0S
        public string SatisSiparisiIcinFisNoUret()
        {
            try
            {
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader("select dbo.vbfSatisSiparisiIcinFisNoUret()", Variables.Yil_))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                            Fisno = reader.GetString(0);
                        else
                            Fisno = "P0S000000000001";
                    }
                }
                return Fisno;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return null; }
        }
        //Ahşap için fiş no üret PAS
        public string SatisSiparisiIcinFisNoUret(string fabrika)
        {
            try
            {
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader("select dbo.vbfSatisSiparisiIcinFisNoUret()", Variables.Yil_, fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                            Fisno = reader.GetString(0);
                        else
                            Fisno = "PAS000000000001";
                    }
                }
                return Fisno;
            }
            catch {
                return string.Empty; }
        }

        public bool InsertSiparisGenel(string SatisCariKodu, int DovizTipi, string TeslimCariKodu, string aciklama,
                                       int satisIhracatMi, double brutTutar, double toplamKdv, string destinasyon,
                                       int toplamSiparisKalemi, string userName, string POno, string fisno)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[12];
                parameters[0] = new SqlParameter("@satisCariKodu", SqlDbType.NVarChar, 35);
                parameters[1] = new SqlParameter("@dovtip", SqlDbType.TinyInt);
                parameters[2] = new SqlParameter("@teslimCariKod", SqlDbType.NVarChar, 35);
                parameters[3] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 35);
                parameters[4] = new SqlParameter("@satisIhracatMi", SqlDbType.TinyInt);
                parameters[5] = new SqlParameter("@brutTutar", SqlDbType.Float);
                parameters[6] = new SqlParameter("@toplamKdv", SqlDbType.Float);
                parameters[7] = new SqlParameter("@destinasyon", SqlDbType.NVarChar, 20);
                parameters[8] = new SqlParameter("@toplamSiparisKalemi", SqlDbType.SmallInt);
                parameters[9] = new SqlParameter("@userName", SqlDbType.NVarChar, 12);
                parameters[10] = new SqlParameter("@POno", SqlDbType.NVarChar, 100);
                parameters[11] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[0].Value = SatisCariKodu;
                parameters[1].Value = DovizTipi;
                parameters[2].Value = TeslimCariKodu;
                parameters[3].Value = aciklama;
                parameters[4].Value = satisIhracatMi;
                parameters[5].Value = Convert.ToSingle(brutTutar);
                parameters[6].Value = Convert.ToSingle(toplamKdv);
                parameters[7].Value = destinasyon;
                parameters[8].Value = toplamSiparisKalemi;
                parameters[9].Value = userName;
                parameters[10].Value = POno;
                parameters[11].Value = fisno;

                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertSiparisOnayaGonderMas", Variables.Yil_, parameters);
                return variables.IsTrue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return false; }
        }

        public bool InsertSiparisSatir(string StokKodu, int SiparisMiktar, double SiparisFiyat, string TerminTarih,
                                       string SatisCariKodu, int sira, string TeslimCariKodu, int DovizTipi, string fisno)
        {
            try
            {

                StokKDV = GetKdvOrani(fisno, StokKodu);
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[1] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
                parameters[2] = new SqlParameter("@siparisFiyat", SqlDbType.Float);
                parameters[3] = new SqlParameter("@satisCariKodu", SqlDbType.NVarChar, 35);
                parameters[4] = new SqlParameter("@dovtip", SqlDbType.TinyInt);
                parameters[5] = new SqlParameter("@teslimCariKod", SqlDbType.NVarChar, 35);
                parameters[6] = new SqlParameter("@sira", SqlDbType.SmallInt);
                parameters[7] = new SqlParameter("@teslimTarih", SqlDbType.DateTime);
                parameters[8] = new SqlParameter("@kdv", SqlDbType.Float);
                parameters[9] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[0].Value = StokKodu;
                parameters[1].Value = SiparisMiktar;
                parameters[2].Value = Convert.ToSingle(SiparisFiyat);
                parameters[3].Value = SatisCariKodu;
                parameters[4].Value = DovizTipi;
                parameters[5].Value = TeslimCariKodu;
                parameters[6].Value = sira;
                parameters[7].Value = TerminTarih;
                parameters[8].Value = Convert.ToSingle(StokKDV);
                parameters[9].Value = fisno;

                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertSiparisOnayaGonder", Variables.Yil_, parameters);
                return variables.IsTrue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return false; }
        }


        public bool InsertSatisIrsaliyesiGenel(string fisno, string teslimCariKodu, double brutTutar, double toplamKdv, int toplamSiparisKalemi, string siparisNumarasi, string fabrika)
        {
            try
            {

                login.User = login.GetUserName().Substring(0, Math.Min(12, login.GetUserName().Length));

                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters = new Dictionary<string, (SqlDbType, int, int, object)> {
                    { "@cariKod", (SqlDbType.NVarChar, 15, 0, teslimCariKodu) },
                    { "@brutTutar", (SqlDbType.Float, 0, 0, brutTutar) },
                    { "@toplamKdv", (SqlDbType.Float, 0, 0, toplamKdv) },
                    { "@toplamSiparisKalemi", (SqlDbType.SmallInt, 0, 0, toplamSiparisKalemi) },
                    { "@userName", (SqlDbType.NVarChar, 12, 0, login.User) },
                    { "@fisno", (SqlDbType.NVarChar, 15, 0, fisno) },
                    { "@siparisNo", (SqlDbType.NVarChar, 15, 0, siparisNumarasi) },
                };

                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertSatisIrsaliyesiMas", Variables.Yil_, parameters, fabrika);
                return variables.IsTrue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return false; }
        }
        public bool InsertSatisIrsaliyesiSatir(string stokKodu, double siparisMiktar, double siparisFiyat,
                                              string cariKodu, int sira, string fisno, string siparisNumarasi,
                                              int siparisSira, int depoKodu, string fabrika, DateTime irsaliyeTarih)
        {
            try
            {

                StokKDV = GetKdvOrani(siparisNumarasi, StokKodu, fabrika);

                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters = new Dictionary<string, (SqlDbType, int, int, object)>
                    {
                        { "@stokKodu", (SqlDbType.NVarChar, 35, 0, stokKodu) },
                        { "@siparisMiktar", (SqlDbType.Float, 0, 0, siparisMiktar) },
                        { "@siparisFiyat", (SqlDbType.Float, 0, 0, siparisFiyat) },
                        { "@cariKodu", (SqlDbType.NVarChar, 15, 0, cariKodu) },
                        { "@sira", (SqlDbType.SmallInt, 0, 0, sira) },
                        { "@kdv", (SqlDbType.Float, 0, 0, StokKDV) },
                        { "@fisno", (SqlDbType.NVarChar, 15, 0, fisno) },
                        { "@siparisNo", (SqlDbType.NVarChar, 15, 0, siparisNumarasi) },
                        { "@siparisSira", (SqlDbType.Int, 0, 0, siparisSira) },
                        { "@depoKodu", (SqlDbType.Int, 0, 0, depoKodu) },
                    { "@irsaliyeTarihi", (SqlDbType.DateTime, 15, 0, irsaliyeTarih) },
                    };

                variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertSatisIrsaliyesiSatir", Variables.Yil_, parameters, fabrika);

                return variables.Result;
            }
            catch { return false; }
        }
        public bool InsertTedarikSiparisGenel(string irsaliyeNumarasi, string tedarikciCariKodu, double brutTutar, double toplamKdv, int toplamSiparisKalemi, string siparisNumarasi, string fabrika, DateTime irsaliyeTarih)
        {
            try
            {
                irsaliyeNumarasi = irsaliyeNumarasi.PadLeft(15, '0');
                login.User = login.GetUserName().Substring(0, Math.Min(12, login.GetUserName().Length));

                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters = new Dictionary<string, (SqlDbType, int, int, object)> {
                    { "@cariKod", (SqlDbType.NVarChar, 15, 0, tedarikciCariKodu) },
                    { "@brutTutar", (SqlDbType.Float, 0, 0, brutTutar) },
                    { "@toplamKdv", (SqlDbType.Float, 0, 0, toplamKdv) },
                    { "@toplamSiparisKalemi", (SqlDbType.SmallInt, 0, 0, toplamSiparisKalemi) },
                    { "@userName", (SqlDbType.NVarChar, 12, 0, login.User) },
                    { "@fisno", (SqlDbType.NVarChar, 15, 0, irsaliyeNumarasi) },
                    { "@siparisNo", (SqlDbType.NVarChar, 15, 0, siparisNumarasi) },
                    { "@irsaliyeTarih", (SqlDbType.DateTime, 0, 0, irsaliyeTarih) },
                };

                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertSatisIrsaliyesiSatir", Variables.Yil_, parameters, fabrika);
                return variables.IsTrue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return false; }
        }

        public int InsertTedarikSiparisSatir(string stokKodu, double siparisMiktar, double siparisFiyat,
                                              string cariKodu, int sira, string irsaliyeNumarasi, string siparisNumarasi,
                                              int siparisSira, int depoKodu, string fabrika, DateTime irsaliyeTarih)
        {
            try
            {
                irsaliyeNumarasi = irsaliyeNumarasi.PadLeft(15, '0');
                StokKDV = GetKdvOrani(siparisNumarasi, StokKodu, fabrika);

                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters = new Dictionary<string, (SqlDbType, int, int, object)>
                    {
                        { "@stokKodu", (SqlDbType.NVarChar, 35, 0, stokKodu) },
                        { "@siparisMiktar", (SqlDbType.Float , 0 , 0, siparisMiktar) },
                        { "@siparisFiyat", (SqlDbType.Float, 0, 0, siparisFiyat) },
                        { "@cariKodu", (SqlDbType.NVarChar, 15, 0, cariKodu) },
                        { "@sira", (SqlDbType.SmallInt, 0, 0, sira) },
                        { "@kdv", (SqlDbType.Float, 0, 0, StokKDV) },
                        { "@fisno", (SqlDbType.NVarChar, 15, 0, irsaliyeNumarasi) },
                        { "@siparisNo", (SqlDbType.NVarChar, 15, 0, siparisNumarasi) },
                        { "@siparisSira", (SqlDbType.Int, 0, 0, siparisSira) },
                        { "@depoKodu", (SqlDbType.Int, 0, 0, depoKodu) },
                        { "@irsaliyeTarihi", (SqlDbType.DateTime, 0, 0, irsaliyeTarih) },
                    };

                variables.ResultInt = dataLayer.Get_One_Int_Result_Stored_Proc_With_Parameters("vbpInsertIrsaliyeSatir", Variables.Yil_, parameters, fabrika);

                return variables.ResultInt;
            }
            catch { return -1; }
        }
        public bool InsertTedarikSiparisGenel(string irsaliyeNumarasi, string tedarikciCariKodu, double brutTutar, double toplamKdv, int toplamSiparisKalemi, string siparisNumarasi, DateTime irsaliyeTarih)
        {
            try
            {
                irsaliyeNumarasi = irsaliyeNumarasi.PadLeft(15, '0');

                SqlParameter[] parametersMas = new SqlParameter[8];
                parametersMas[0] = new SqlParameter("@cariKod", SqlDbType.NVarChar, 15);
                parametersMas[1] = new SqlParameter("@brutTutar", SqlDbType.Float);
                parametersMas[2] = new SqlParameter("@toplamKdv", SqlDbType.Float);
                parametersMas[3] = new SqlParameter("@toplamSiparisKalemi", SqlDbType.SmallInt);
                parametersMas[4] = new SqlParameter("@userName", SqlDbType.NVarChar, 12);
                parametersMas[5] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parametersMas[6] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
                parametersMas[7] = new SqlParameter("@irsaliyeTarih", SqlDbType.DateTime);
                parametersMas[0].Value = tedarikciCariKodu;
                parametersMas[1].Value = Math.Round(Convert.ToSingle(brutTutar), 4);
                parametersMas[2].Value = Math.Round(Convert.ToSingle(toplamKdv), 4);
                parametersMas[3].Value = toplamSiparisKalemi;
                parametersMas[4].Value = login.GetUserName();
                parametersMas[5].Value = irsaliyeNumarasi;
                parametersMas[6].Value = siparisNumarasi;
                parametersMas[7].Value = irsaliyeTarih;

                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertIrsaliyeMas", Variables.Yil_, parametersMas);
                return variables.IsTrue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return false; }
        }

        public bool InsertTedarikSiparisSatir(string stokKodu, double siparisMiktar, double siparisFiyat,
                                              string cariKodu, int sira, string irsaliyeNumarasi, string siparisNumarasi,
                                              int siparisSira, int depoKodu, DateTime irsaliyeTarih)
        {
            try
            {
                irsaliyeNumarasi = irsaliyeNumarasi.PadLeft(15, '0');
                StokKDV = GetKdvOrani(siparisNumarasi, StokKodu);

                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[1] = new SqlParameter("@siparisMiktar", SqlDbType.Float);
                parameters[2] = new SqlParameter("@siparisFiyat", SqlDbType.Float);
                parameters[3] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 15);
                parameters[4] = new SqlParameter("@sira", SqlDbType.SmallInt);
                parameters[5] = new SqlParameter("@kdv", SqlDbType.Float);
                parameters[6] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[7] = new SqlParameter("@siparisNo", SqlDbType.NVarChar, 15);
                parameters[8] = new SqlParameter("@siparisSira", SqlDbType.Int);
                parameters[9] = new SqlParameter("@depoKodu", SqlDbType.Int);
                parameters[10] = new SqlParameter("@irsaliyeTarihi", SqlDbType.DateTime);
                parameters[0].Value = stokKodu;
                parameters[1].Value = Math.Round(Convert.ToSingle(siparisMiktar), 4);
                parameters[2].Value = Math.Round(Convert.ToSingle(siparisFiyat), 4);
                parameters[3].Value = cariKodu;
                parameters[4].Value = sira;
                parameters[5].Value = Math.Round(Convert.ToSingle(StokKDV), 4);
                parameters[6].Value = irsaliyeNumarasi;
                parameters[7].Value = siparisNumarasi;
                parameters[8].Value = siparisSira;
                parameters[9].Value = depoKodu;
                parameters[10].Value = irsaliyeTarih;

                Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters("vbpInsertIrsaliyeSatir", Variables.Yil_, parameters, Variables.Fabrika_);

                return Variables.Result_;
            }
            catch { return false; }
        }
        public bool InsertSiparisKaydetGenel(Cls_Siparis siparisGenel, string fabrika)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 15);
                parameters[0].Value = siparisGenel.AssociatedCari.TeslimCariKodu;
                parameters[1] = new SqlParameter("@genelAciklama", SqlDbType.NVarChar, 20);
                parameters[1].Value = siparisGenel.SiparisAciklama;
                parameters[2] = new SqlParameter("@brutTutar", SqlDbType.Float);
                parameters[2].Value = Convert.ToDouble(siparisGenel.SiparisToplamTutar);
                parameters[3] = new SqlParameter("@toplamKdv", SqlDbType.Float);
                parameters[3].Value = siparisGenel.SiparisToplamKDV;
                parameters[4] = new SqlParameter("@toplamSiparisKalemi", SqlDbType.SmallInt);
                parameters[4].Value = siparisGenel.ToplamSiparisKalemi;
                parameters[5] = new SqlParameter("@kullaniciAdi", SqlDbType.NVarChar, 12);
                parameters[5].Value = login.GetUserName().Substring(0, Math.Min(12, login.GetUserName().Length));
                parameters[6] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[6].Value = siparisGenel.Fisno;

                variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vapInsertSiparisMas", Variables.Yil_, parameters, fabrika);
                if (!variables.Result)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertSiparisKaydetSatir(ObservableCollection<Cls_Siparis> siparisCollection, string fisno, string fabrika)
        {
            try
            {
                variables.Counter = 1;
                foreach (Cls_Siparis item in siparisCollection)
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[0].Value = item.StokKodu;
                    parameters[1] = new SqlParameter("@siparisMiktar", SqlDbType.Int);
                    parameters[1].Value = item.SiparisMiktar;
                    parameters[2] = new SqlParameter("@siparisFiyat", SqlDbType.Float);
                    parameters[2].Value = item.SiparisFiyat;
                    parameters[3] = new SqlParameter("@satirAciklama", SqlDbType.NVarChar, 300);
                    parameters[3].Value = item.SiparisSatirAciklama;
                    parameters[4] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 15);
                    parameters[4].Value = item.AssociatedCari.TeslimCariKodu;
                    parameters[5] = new SqlParameter("@sira", SqlDbType.SmallInt);
                    parameters[5].Value = variables.Counter;
                    parameters[6] = new SqlParameter("@kdv", SqlDbType.Int);
                    parameters[6].Value = item.StokKDV;
                    parameters[7] = new SqlParameter("@depoKodu", SqlDbType.Int);
                    parameters[7].Value = item.DepoKodu;
                    parameters[8] = new SqlParameter("@vade", SqlDbType.Int);
                    parameters[8].Value = item.Vade;
                    parameters[9] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameters[9].Value = fisno;

                    variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vapInsertSiparisSatir", Variables.Yil_, parameters, fabrika);

                    if (!variables.Result)
                        return false;

                    variables.Counter++;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int InsertTalep(ObservableCollection<Cls_Siparis> siparisCollection, float brutTutar, float toplamKDV, ObservableCollection<Cls_Planlama> planlar, bool siradanPlanaBagla, bool planSecerekBagla)
        {
            try
            {
                if (siparisCollection == null) return 2;
                if (siparisCollection.Count == 0) return 3;

                Variables.Query_ = "vbpGetFisno";
                SqlParameter[] parameterFis = new SqlParameter[3];
                parameterFis[0] = new SqlParameter("@prefix", SqlDbType.NVarChar, 4);
                parameterFis[1] = new SqlParameter("@tableName", SqlDbType.NVarChar, 128);
                parameterFis[2] = new SqlParameter("@columnName", SqlDbType.NVarChar, 128);
                parameterFis[0].Value = "TLP";
                parameterFis[1].Value = "TBLTEKLIFMAS";
                parameterFis[2].Value = "FATIRS_NO";


                string fisno = dataLayer.Get_One_String_Result_Stored_Proc_With_Parameters(Variables.Query_, Variables.Yil_, parameterFis, Variables.Fabrika_);
                if (string.IsNullOrEmpty(fisno) || fisno == "STRING HATA")
                    return 6;


                Variables.Query_ = "vbpInsertTeklifMas";
                SqlParameter[] parametersMas = new SqlParameter[4];
                parametersMas[0] = new SqlParameter("@kalemAdedi", SqlDbType.Int);
                parametersMas[1] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                parametersMas[2] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                parametersMas[3] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parametersMas[0].Value = siparisCollection.Count();
                parametersMas[1].Value = siparisCollection.Select(i => i.TalepGenelAciklama).FirstOrDefault();
                parametersMas[2].Value = login.GetUserName();
                parametersMas[3].Value = fisno;

                Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parametersMas, Variables.Fabrika_);
                if (!Variables.Result_)
                    return 4;


                if (siradanPlanaBagla)
                {
                    Variables.Query_ = "vbpInsertTalepSiradanPlanaBagla";
                    Variables.Counter_ = 0;
                    string fabrika = Variables.Departman_.ToUpper();
                    if (fabrika == "DOSEMELI PLANLAMA")
                        fabrika = "DOSEMELI";
                    else if (fabrika == "MODULER PLANLAMA")
                        fabrika = "MODULER";
                    else
                        fabrika = "DOSEMELI";

                    foreach (Cls_Siparis item in siparisCollection)
                    {
                        SqlParameter[] parameters = new SqlParameter[9];
                        parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                        parameters[2] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                        parameters[3] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                        parameters[4] = new SqlParameter("@kdv", SqlDbType.Float);
                        parameters[5] = new SqlParameter("@terminTarih", SqlDbType.DateTime);
                        parameters[6] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                        parameters[7] = new SqlParameter("@depoKodu", SqlDbType.Int);
                        parameters[8] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                        parameters[0].Value = fisno;
                        parameters[1].Value = (int)Variables.Counter_;
                        parameters[2].Value = item.StokKodu;
                        parameters[3].Value = item.GonderilecekMiktar;
                        parameters[4].Value = item.StokKDV;
                        parameters[5].Value = item.TerminTarih;
                        parameters[6].Value = item.TalepAciklama;
                        parameters[7].Value = (int)item.DepoKodu;
                        parameters[8].Value = fabrika;
                        Variables.ResultInt_ = dataLayer.ExecuteStoredProcedureWithParametersReturnsInt(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                        if (Variables.ResultInt_ == -1)
                        {
                            TalepGirisKayitlariGeriAl(fisno);
                            return 5;
                        }
                        Variables.Counter_ = Variables.ResultInt_;
                    }

                    Variables.Query_ = "vbpUpdateTalepHesaplat";

                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameter[0].Value = fisno;
                    Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                    if (!Variables.Result_)
                    {
                        TalepGirisKayitlariGeriAl(fisno);
                        return 7;
                    }
                }
                else if (planSecerekBagla)
                {
                    if (Variables.Departman_.ToUpper().Contains("MODULER"))
                        Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTableMod', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTableMod; " +
                                            " create table tempTalepAcPlanSecerekHamTableMod (hamKodu nvarchar(35),toplamTalepMiktar float) " +
                                            " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTableMod', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTableMod; " +
                                            " create table tempTalepAcPlanSecerekPlanTableMod (planAdi nvarchar(100)) ";


                    else
                        Variables.Query_ = " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekHamTable', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekHamTable; " +
                                            " create table tempTalepAcPlanSecerekHamTable (hamKodu nvarchar(35),toplamTalepMiktar float) " +
                                            " IF OBJECT_ID('dbo.tempTalepAcPlanSecerekPlanTable', 'U') IS NOT NULL DROP TABLE dbo.tempTalepAcPlanSecerekPlanTable; " +
                                            " create table tempTalepAcPlanSecerekPlanTable (planAdi nvarchar(100)) ";

                    Variables.Result_ = dataLayer.ExecuteCommand(Variables.Query_, Variables.Yil_);
                    if (!Variables.Result_)
                    {
                        TalepGirisKayitlariGeriAl(fisno);
                        return 5;
                    }

                    foreach (Cls_Siparis item in siparisCollection)
                    {
                        SqlParameter[] parameterss = new SqlParameter[2];
                        parameterss[0] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                        parameterss[1] = new SqlParameter("@talepMiktar", SqlDbType.Float);
                        parameterss[0].Value = item.StokKodu;
                        parameterss[1].Value = item.GonderilecekMiktar;
                        if (Variables.Departman_.ToUpper().Contains("MODULER"))
                            Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTableMod Values (@hamKodu,@talepMiktar) ";
                        else
                            Variables.Query_ = " insert into tempTalepAcPlanSecerekHamTable Values (@hamKodu,@talepMiktar) ";

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterss);
                        if (!Variables.Result_)
                        {
                            TalepGirisKayitlariGeriAl(fisno);
                            return 5;
                        }
                    }
                    foreach (Cls_Planlama item in planlar)
                    {
                        SqlParameter[] parameterUpdate = new SqlParameter[1];
                        parameterUpdate[0] = new SqlParameter("@planAdi", SqlDbType.NVarChar, 100);
                        parameterUpdate[0].Value = item.PlanAdi;

                        if (Variables.Departman_.ToUpper().Contains("MODULER"))
                            Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTableMod Values (@planAdi) ";
                        else
                            Variables.Query_ = " insert into tempTalepAcPlanSecerekPlanTable Values (@planAdi) ";

                        Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterUpdate);
                        if (!Variables.Result_)
                        {
                            TalepGirisKayitlariGeriAl(fisno);
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
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameters[1] = new SqlParameter("@terminTarih", SqlDbType.DateTime);
                    parameters[2] = new SqlParameter("@fabrika", SqlDbType.NVarChar, 100);
                    parameters[3] = new SqlParameter("@depoKodu", SqlDbType.Int);
                    parameters[0].Value = fisno;
                    parameters[1].Value = siparisCollection.Select(i => i.TerminTarih).FirstOrDefault();
                    parameters[2].Value = fabrika;
                    parameters[3].Value = depoKodu;

                    Variables.Query_ = "vbpInsertTalepPlanSecerek";
                    Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters);
                    if (!Variables.Result_)
                    {
                        TalepGirisKayitlariGeriAl(fisno);
                        return 5;
                    }

                    Variables.Query_ = "vbpUpdateTalepHesaplat";

                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameter[0].Value = fisno;
                    Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                    if (!Variables.Result_)
                    {
                        TalepGirisKayitlariGeriAl(fisno);
                        return 7;
                    }

                }
                else
                {
                    Variables.Query_ = "vbpInsertTeklifTra";

                    foreach (Cls_Siparis item in siparisCollection)
                    {
                        SqlParameter[] parameters = new SqlParameter[8];
                        parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@talepSira", SqlDbType.Int);
                        parameters[2] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                        parameters[3] = new SqlParameter("@miktar", SqlDbType.Float);
                        parameters[4] = new SqlParameter("@kdv", SqlDbType.Float);
                        parameters[5] = new SqlParameter("@terminTarih", SqlDbType.DateTime);
                        parameters[6] = new SqlParameter("@aciklama", SqlDbType.NVarChar, 100);
                        parameters[7] = new SqlParameter("@depoKodu", SqlDbType.Int);
                        parameters[0].Value = fisno;
                        parameters[1].Value = item.TalepSira;
                        parameters[2].Value = item.StokKodu;
                        parameters[3].Value = Math.Round(item.GonderilecekMiktar, 4);
                        parameters[4].Value = item.StokKDV;
                        parameters[5].Value = item.TerminTarih;
                        parameters[6].Value = item.TalepAciklama;
                        parameters[7].Value = item.DepoKodu;
                        Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                        if (!Variables.Result_)
                        {
                            TalepGirisKayitlariGeriAl(fisno);
                            return 5;
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

        private void TalepGirisKayitlariGeriAl(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return;

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;

                Variables.Query_ = "vbpDeleteTalep";

                dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
            }
            catch
            {

                throw;
            }
        }

        public bool DeleteSiparisGeriAl(string fisno, string fabrika)
        {
            try
            {
                Variables.Query_ = "delete from tblsipamas where fatirs_no = @fisno delete from tblsipatra where fisno = @fisno delete from tblfatuek where fatirsno=@fisno";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;

                variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameter, fabrika);
                if (!variables.Result) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public ObservableCollection<Cls_Siparis> GetSiparisGenelInfo(string fisno)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvSiparisGenelBilgileri WHERE FATIRS_NO LIKE '%' + @fisno + '%'";


                SqlParameter[] parameterArray = new SqlParameter[1];

                parameterArray[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 35);
                parameterArray[0].Value = fisno;

                int convertedNumber;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameterArray))
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string userId = reader["kullaniciKodu"].ToString();
                            if (int.TryParse(userId, out convertedNumber))
                            {
                                login.UserID = login.GetUserIDFromPortalID(Convert.ToInt32(reader["kullaniciKodu"]));
                                login.User = login.GetUserNameFromUserID(login.UserID);
                            }
                            else
                                login.User = userId;

                            Cls_Siparis cls_Siparis = new Cls_Siparis
                            {
                                Fisno = reader[0].ToString(),
                                UserName = login.User,

                            };

                            // Create an instance of AssociatedCari and populate its properties
                            cls_Siparis.AssociatedCari = new Cls_Cari
                            {
                                SatisCariKodu = reader[1].ToString(),
                                SatisCariAdi = reader[2].ToString(),
                                TeslimCariKodu = reader[3].ToString(),
                                TeslimCariAdi = reader[4].ToString(),
                            };

                            cls_Siparis.SiparisTarih = !string.IsNullOrEmpty(reader["TARIH"].ToString()) ?
                                                        Convert.ToDateTime(reader["TARIH"]).ToString("dd.MM.yyyy")
                                                        : string.Empty;

                            cls_Siparis.AssociatedCari.SiparisTipi = (Cls_Base.SiparisTipi)Convert.ToInt32(reader[6]);
                            cls_Siparis.Destinasyon = reader[7].ToString();
                            cls_Siparis.SiparisDurum = reader["siparisDurum"].ToString();

                            _siparisCollection.Add(cls_Siparis);
                        }
                    }
                }

                SiparisCollection = _siparisCollection;
                return SiparisCollection;

            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Siparis> GetOnayBekleyenSiparisler()
        {
            try
            {
                Cls_Arge arge = new();
                Variables.Query_ = "select * from vbvOnayBekleyenSiparisler";

                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);

                temp_coll_sip.Clear();

                foreach (DataRow row in dataTable.Rows)
                {

                    Cls_Cari cari = new Cls_Cari()
                    {
                        SatisCariAdi = row["satisCariIsim"].ToString(),
                        TeslimCariAdi = row["teslimCariIsim"].ToString(),
                        IlIlce = row["cariIlIlce"].ToString(),
                    };

                    Cls_Siparis cls_Siparis = new Cls_Siparis()
                    {
                        Fisno = row["fisno"].ToString(),
                        SiparisTarih = !string.IsNullOrEmpty(row["tarih"].ToString()) ?
                                        Convert.ToDateTime(row["tarih"]).ToString("dd.MM.yyyy")
                                        : string.Empty,
                        UserName = row["kullaniciKodu"].ToString(),
                        SiparisToplamTutar = Convert.ToDouble(row["brutTutar"]),
                        AssociatedCari = cari,
                        DovizTipi = row["dovizTipi"].ToString(),
                        DovizFiyat = Convert.ToSingle(row["dovizTutar"]),
                        Destinasyon = row["destinasyon"].ToString(),
                        POnumarasi = row["POno"].ToString(),
                        SatisFiyat = Convert.ToSingle(row["satisFiyat"]),
                        DoesUrunAgaciExists = arge.IfSiparisUrunAgaciExists(row["fisno"].ToString(),false)
                    };
                    temp_coll_sip.Add(cls_Siparis);
                }

                SiparisCollection = temp_coll_sip;
                return SiparisCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool UpdatePO_DestinasyonTermin(ObservableCollection<Cls_Siparis> ordersToUpdate)
        {
            try
            {
                if (ordersToUpdate == null ||
                    ordersToUpdate.Count == 0)
                    return false;

                Variables.Query_ = "update tblsipatra set sthar_testar = @terminTarih where fisno=@siparisNumarasi and stra_sipkont=@siparisSatir ";
                Variables.Query_ += "update tblsipamas set aciklama = @destinasyon, DUZELTMEYAPANKUL=@userName, DUZELTMETARIHI=GETDATE() where fatirs_no=@siparisNumarasi ";
                Variables.Query_ += "update tblfatuek set acik12 = @ponumarasi where fatirsno=@siparisNumarasi ";

                SqlParameter[] parameters = new SqlParameter[6];



                foreach (Cls_Siparis siparis in ordersToUpdate)
                {
                    parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[0].Value = siparis.Fisno;
                    parameters[1] = new SqlParameter("@siparisSatir", SqlDbType.Int);
                    parameters[1].Value = siparis.FisSira;
                    parameters[2] = new SqlParameter("@terminTarih", SqlDbType.DateTime);
                    parameters[2].Value = siparis.TerminTarih;
                    parameters[3] = new SqlParameter("@destinasyon", SqlDbType.NVarChar, 20);
                    parameters[3].Value = siparis.Destinasyon;
                    parameters[4] = new SqlParameter("@ponumarasi", SqlDbType.NVarChar, 100);
                    parameters[4].Value = siparis.POnumarasi;
                    parameters[5] = new SqlParameter("@userName", SqlDbType.NVarChar, 12);
                    parameters[5].Value = login.GetUserName();

                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
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
        public bool UpdateMusteriSiparisCari(ObservableCollection<Cls_Siparis> ordersToUpdate)
        {
            try
            {
                if (ordersToUpdate == null ||
                    ordersToUpdate.Count == 0)
                    return false;

                Variables.Query_ = "update tblsipatra set sthar_aciklama = @satisCariKodu,sthar_carikod=@teslimCariKodu where fisno=@siparisNumarasi ";
                Variables.Query_ += "update tblsipamas set cari_kodu=@satisCariKodu,cari_kod2=@teslimCariKodu, DUZELTMEYAPANKUL=@userName, DUZELTMETARIHI=GETDATE() where fatirs_no=@siparisNumarasi ";
                Variables.Query_ += "update tblfatuek set ckod=@satisCariKodu where fatirsno=@siparisNumarasi ";
                SqlParameter[] parameters = new SqlParameter[5];

                foreach (Cls_Siparis siparis in ordersToUpdate)
                {
                    parameters[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[0].Value = siparis.Fisno;
                    parameters[1] = new SqlParameter("@siparisSatir", SqlDbType.Int);
                    parameters[1].Value = siparis.FisSira;
                    parameters[2] = new SqlParameter("@teslimCariKodu", SqlDbType.NVarChar, 35);
                    parameters[2].Value = siparis.AssociatedCari.TeslimCariKodu;
                    parameters[3] = new SqlParameter("@satisCariKodu", SqlDbType.NVarChar, 35);
                    parameters[3].Value = siparis.AssociatedCari.SatisCariKodu;
                    parameters[4] = new SqlParameter("@userName", SqlDbType.NVarChar, 12);
                    parameters[4].Value = login.GetUserName();

                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
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

        public bool CheckIfCariExists(string cariKodu)
        {
            try
            {
                Variables.Query_ = "select top 1 cari_kod from tblcasabit (nolock) where cari_kod = @cariKodu";

                SqlParameter[] parameter = new SqlParameter[1];

                parameter[0] = new SqlParameter("@cariKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = cariKodu;

                Variables.ResultString_ = dataLayer.Get_One_String_Result_Command_With_Parameters(Variables.Query_, veriYili, parameter);

                if (Variables.ResultString_.Length > 0 &&
                    Variables.ResultString_ != "STRING HATA")
                    return true;
                else return false;

            }
            catch
            {
                return false;
            }
        }
        public ObservableCollection<Cls_Siparis> GetCustomerOrdersToBeUpdated(string siparisNumarasi)
        {
            try
            {
                Variables.Query_ = "select * from vbvSiparis_PO_Destinasyon_TTarih_Guncelle where SiparisNumarasi like " + "'%" + @siparisNumarasi + "%'";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = siparisNumarasi;

                ObservableCollection<Cls_Siparis> temp_coll = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, variables.Fabrika, parameter, reader =>
                {
                    Cls_Siparis model = new();
                    model.Fisno = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.FisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    model.StokKodu = reader["UrunKodu"] is DBNull ? "" : reader["UrunKodu"].ToString();
                    model.StokAdi = reader["UrunAdi"] is DBNull ? "" : reader["UrunAdi"].ToString();
                    model.AssociatedCari.TeslimCariAdi = reader["CariAdi"] is DBNull ? "" : reader["CariAdi"].ToString();
                    model.POnumarasi = reader["POno"] is DBNull ? "" : reader["POno"].ToString();
                    model.Destinasyon = reader["Destinasyon"] is DBNull ? "" : reader["Destinasyon"].ToString();
                    model.TerminTarih = reader["TerminTarih"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["TerminTarih"]);
                    model.IsChecked = true;
                    return model;
                });
                if (temp_coll.Count() == 0 ||
                    temp_coll == null)
                    return null;

                return temp_coll;

            }
            catch
            {
                return null;
            }
        }

        public ObservableCollection<Cls_Siparis> GetCustomerOrdersToBeUpdatedCari(string siparisNumarasi)
        {
            try
            {
                Variables.Query_ = "select * from vbvMusteriSiparisCariGuncelle where SiparisNumarasi like " + "'%" + @siparisNumarasi + "%'";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = siparisNumarasi;

                ObservableCollection<Cls_Siparis> temp_coll = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, variables.Fabrika, parameter, reader =>
                {
                    Cls_Siparis model = new();
                    model.Fisno = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.AssociatedCari.SatisCariKodu = reader["SatisCariKodu"] is DBNull ? "" : reader["SatisCariKodu"].ToString();
                    model.AssociatedCari.SatisCariAdi = reader["SatisCariAdi"] is DBNull ? "" : reader["SatisCariAdi"].ToString();
                    model.AssociatedCari.TeslimCariKodu = reader["TeslimCariKodu"] is DBNull ? "" : reader["TeslimCariKodu"].ToString();
                    model.AssociatedCari.TeslimCariAdi = reader["TeslimCariAdi"] is DBNull ? "" : reader["TeslimCariAdi"].ToString();
                    model.Destinasyon = reader["Destinasyon"] is DBNull ? "" : reader["Destinasyon"].ToString();
                    model.POnumarasi = reader["PoNumarasi"] is DBNull ? "" : reader["PoNumarasi"].ToString();
                    model.IsChecked = true;
                    return model;
                });
                if (temp_coll.Count() == 0 ||
                    temp_coll == null)
                    return null;

                return temp_coll;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Siparis> GetSiparisSatirInfo(string fisno, string talepEden)
        {
            try
            {

                if (talepEden == "Onay")
                {
                    Variables.Query_ = $"select a.stra_sipkont, a.stok_kodu, b.stok_adi, case when sthar_dovtip=0 then cast(isnull(sthar_nf,0) as float) else cast(isnull(sthar_dovfiat,0) as float) end as miktar, a.sthar_testar,a.STHAR_GCMIK from werpsipatra a left join tblstsabit b on a.stok_kodu=b.stok_kodu where fisno='{fisno}'";
                }

                if (talepEden == "Asil")
                {
                    Variables.Query_ = $"select a.fisno, a.stra_sipkont, a.stok_kodu, b.stok_adi, a.sthar_gcmik, a.sthar_testar,a.sthar_htur,case when sthar_dovtip=0 then cast(isnull(sthar_nf,0) as float) else cast(isnull(sthar_dovfiat,0) as float) end as miktar from tblsipatra a left join tblstsabit b on a.stok_kodu=b.stok_kodu where fisno='{fisno}'";
                }

                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);

                temp_coll_sip.Clear();

                foreach (DataRow row in dataTable.Rows)
                {

                    if (talepEden == "Onay")
                    {
                        Cls_Siparis cls_Siparis = new Cls_Siparis()
                        {
                            FisSira = Convert.ToInt32(row["stra_sipkont"]),
                            StokKodu = row["stok_kodu"].ToString(),
                            StokAdi = row["stok_adi"].ToString(),
                            SiparisMiktar = Convert.ToInt32(row["STHAR_GCMIK"]),
                            TerminTarih = Convert.ToDateTime(row["sthar_testar"]),
                            SiparisFiyat = Convert.ToSingle(row["miktar"]),
                        };
                        temp_coll_sip.Add(cls_Siparis);
                    }

                    if (talepEden == "Asil")
                    {
                        Cls_Siparis cls_Siparis = new Cls_Siparis()
                        {
                            Fisno = row["fisno"].ToString(),
                            FisSira = Convert.ToInt32(row["stra_sipkont"]),
                            StokKodu = row["stok_kodu"].ToString(),
                            StokAdi = row["stok_adi"].ToString(),
                            SiparisMiktar = Convert.ToInt32(row["sthar_gcmik"]),
                            TerminTarih = Convert.ToDateTime(row["sthar_testar"]),
                            SiparisDurum = row["sthar_htur"].ToString(),
                        };
                        temp_coll_sip.Add(cls_Siparis);
                    }
                }

                SiparisDetayCollection = temp_coll_sip;
                return SiparisDetayCollection;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public ObservableCollection<Cls_Siparis> GetDuzenleOnayBekleyenSiparisler(string fisno)
        {
            try
            {

                Variables.Query_ = $"select * from vbvOnayBekleyenSiparisler where fisno='{fisno}'";

                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);

                temp_coll_sip.Clear();

                int DovizTipiInt = 0, SatisTipiInt = 0;

                foreach (DataRow row in dataTable.Rows)
                {

                    Cls_Cari cari = new Cls_Cari()
                    {
                        SatisCariKodu = row["satisCariKod"].ToString(),
                        TeslimCariKodu = row["teslimCariKod"].ToString(),
                        SatisCariAdi = row["satisCariIsim"].ToString(),
                        TeslimCariAdi = row["teslimCariIsim"].ToString(),
                        SiparisTipi = (Cls_Base.SiparisTipi)Convert.ToInt32(row["siparisTipi"]),
                        DovizTipi = (Cls_Base.DovizTipi)Convert.ToInt32(row["dovizTipi"]),
                    };

                    Cls_Siparis cls_Siparis = new Cls_Siparis()
                    {
                        Fisno = row["fisno"].ToString(),
                        Destinasyon = row["destinasyon"].ToString(),
                        POnumarasi = row["POno"].ToString(),
                        SiparisAciklama = row["Aciklama"].ToString(),
                        AssociatedCari = cari,
                    };

                    temp_coll_sip.Add(cls_Siparis);
                }

                SiparisCollection = temp_coll_sip;
                return SiparisCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Siparis> GetDuzenleSiparisSatirInfo(string fisno)
        {
            try
            {
                Cls_Arge arge = new();
                Variables.Query_ = $"select a.fisno, a.stra_sipkont, a.stok_kodu, b.stok_adi, a.sthar_gcmik, a.sthar_testar,case when isnull(sthar_dovfiat,0) = 0 then sthar_nf else sthar_dovfiat end as siparisFiyat,sthar_dovtip from werpsipatra a left join tblstsabit b on a.stok_kodu=b.stok_kodu where fisno='{fisno}'";
                dataTable = dataLayer.Select_Command(Variables.Query_, Variables.Yil_);

                temp_coll_sip.Clear();

                foreach (DataRow row in dataTable.Rows)
                {


                    Cls_Siparis cls_Siparis = new Cls_Siparis()
                    {
                        Fisno = row["fisno"].ToString(),
                        FisSira = Convert.ToInt32(row["stra_sipkont"]),
                        StokKodu = row["stok_kodu"].ToString(),
                        StokAdi = row["stok_adi"].ToString(),
                        SiparisMiktar = Convert.ToInt32(row["sthar_gcmik"]),
                        TerminTarih = Convert.ToDateTime(row["sthar_testar"]),
                        SiparisFiyat = Convert.ToSingle(row["siparisFiyat"]),
                        DovizTipiInt = Convert.ToInt32(row["sthar_dovtip"]),
                        DoesUrunAgaciExists = arge.IfUrunAgaciExists(row["stok_kodu"].ToString())
                    };


                    temp_coll_sip.Add(cls_Siparis);
                }

                SiparisDetayCollection = temp_coll_sip;
                return SiparisDetayCollection;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public bool OnayBekleyenSiparislestir(string fisno)
        {
            try
            {
                if (string.IsNullOrEmpty(fisno))
                    return false;

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@SiparisNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = fisno;
                Variables.Query_ = "vbpInsertOnayBekleyenSiparislestir";

                Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                return Variables.Result_;
            }
            catch
            {
                return false;
            }
        }
        public string GetNextFisnoForSatisIrsaliyesi(string fabrika)
        {
            try
            {
                Variables.Query_ = "select top 1 cast(FATIRS_NO as int) + 1 from TBLFATUIRS where FTIRSIP='3' or FTIRSIP='A' and LEFT(fatirs_no,3)='000' order by FATIRS_NO desc";

                int fisSayisi = dataLayer.Get_One_Int_Result_Command(Variables.Query_, Variables.Yil_, fabrika);
                if (fisSayisi == -1)
                    return string.Empty;

                Fisno = fisSayisi.ToString();
                Fisno = Fisno.PadLeft(15, '0');
                return Fisno;
            }
            catch
            {
                return string.Empty;
            }
        }
        public float GetKdvOrani(string fisno, string stokKodu, string fabrika)
        {
            try
            {
                Variables.Query_ = $"select top 1 isnull(sthar_kdv,0) from tblsipatra (nolock) where stok_kodu=@stokKodu and fisno=@fisno";

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = stokKodu;
                parameters[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[1].Value = fisno;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        StokKDV = reader.GetFloat(0);
                    }
                }
                return Convert.ToSingle(StokKDV);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return 0; }

        }
        public float GetKdvOrani(string fisno, string stokKodu)
        {
            try
            {
                Variables.Query_ = $"select top 1 kdv_orani from tblsipatra a left join tblstsabit b on a.stok_kodu=b.stok_kodu where a.stok_kodu=@stokKodu and a.fisno=@fisno";

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = stokKodu;
                parameters[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[1].Value = fisno;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        StokKDV = Convert.ToSingle(reader[0]);
                    }
                }
                return Convert.ToSingle(StokKDV);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return 0; }

        }
        public string GetPrefixForSiparisStokAdi(string stokKodu, string fisNo, int fisSira, string fabrika)
        {
            try
            {
                Variables.Query_ = "select ekalan from tblsipatra where stok_kodu=@stokKodu and fisno=@fisno and stra_sipkont=@fisSira";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameters[1] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameters[2] = new SqlParameter("@fisSira", SqlDbType.Int);
                parameters[0].Value = stokKodu;
                parameters[1].Value = fisNo;
                parameters[2].Value = fisSira;

                string prefix = dataLayer.Get_One_String_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika);



                return prefix;

            }
            catch
            {
                return string.Empty;
            }
        }

        public ObservableCollection<Cls_Siparis> GetPOno(string siparisNumarasi)
        {
            try
            {
                Variables.Query_ = "Select isnull(fatirs_no,'') as SiparisNumarasi,isnull(CARI_ISIM,'') as CariAdi, isnull(ACIKLAMA,'') as Destinasyon,isnull(acik12,'') as POnumarasi from tblsipamas mas (nolock)" +
                                    " left join tblfatuek ek (nolock) on mas.fatirs_no = ek.fatirsno" +
                                    " left join tblcasabit cari (nolock) on mas.cari_kod2 = cari.cari_kod" +
                                    " where fatirs_no like '%' + @siparisNumarasi + '%'";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@siparisNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = siparisNumarasi;

                ObservableCollection<Cls_Siparis> siparisCollection = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameter, reader =>
                {
                    Cls_Siparis model = new();
                    model.Fisno = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    model.AssociatedCari.TeslimCariAdi = reader["CariAdi"] is DBNull ? "" : reader["CariAdi"].ToString();
                    model.Destinasyon = reader["Destinasyon"] is DBNull ? "" : reader["Destinasyon"].ToString();
                    model.POnumarasi = reader["POnumarasi"] is DBNull ? "" : reader["POnumarasi"].ToString();
                    return model;
                });
                return siparisCollection;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool DeleteOnayBekleyenSiparis(string fisno)
        {

            if (string.IsNullOrEmpty(fisno)) { MessageBox.Show("Fiş No Boş Olamaz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }

            Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters = new Dictionary<string, (SqlDbType, int, int, object)>
            {

                { "@fisNo", (SqlDbType.NVarChar, 15, 0, fisno) },

            };


            try
            {
                variables.IsTrue = dataLayer.ExecuteStoredProcedureWithParameters("vbpOnayBekleyenSiparisSil", Variables.Yil_, parameters);

                return variables.IsTrue;
            }

            catch { return false; }
        }
        public bool SiparisKapatAcSatir(string fisno, int sira, bool kapat_)
        {
            try 
            { 

                if (kapat_)
                {
                    Variables.Query_ = "vbpMusteriSiparisKapat";

                    SqlParameter[] parameters = new SqlParameter[2];

                    parameters[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameters[0].Value = fisno;
                    parameters[1] = new SqlParameter("@satirNo", SqlDbType.Int);
                    parameters[1].Value = sira;
                    Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                    return Variables.Result_;

                }
                if (!kapat_)
                {
                    Variables.Query_ = "update tblsipatra set sthar_htur='H' where fisno=@fisno and stra_sipkont=@sira ";
                    SqlParameter[] parameter = new SqlParameter[2];

                    parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameter[0].Value = fisno;
                    parameter[1] = new SqlParameter("@sira", SqlDbType.Int);
                    parameter[1].Value = sira;
                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
                    return Variables.Result_;
                }
                return false;
            }
            catch { return false; }
        }
        public bool SiparisKapatAc(string fisno, bool kapat_)
        {
            try
            {
                if (kapat_)
                {
                    Variables.Query_ = "vbpMusteriSiparisKapatFisNo";

                    SqlParameter[] parameter = new SqlParameter[1];

                    parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameter[0].Value = fisno;
                    Variables.Result_ = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_,Variables.Yil_,parameter,Variables.Fabrika_);
                    return Variables.Result_;

                }
                if (!kapat_)
                {
                    Variables.Query_ = "update tblsipatra set sthar_htur='H' where fisno=@fisno ";

                    SqlParameter[] parameter = new SqlParameter[1];

                    parameter[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                    parameter[0].Value = fisno;

                    Variables.Result_ = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameter);

                    return Variables.Result_;
                }
                return false;

            }
            catch { return false; }
        }
        public bool UpdateOnayBekleyenSiparis(ObservableCollection<Cls_Siparis> siparisMas, ObservableCollection<Cls_Siparis> siparisSatir)
        { 
            
            try
            {

                variables.Counter = 0;
                float kumulatifSatisToplam = 0;
                float kdv = 0;
                float qumulativeKdv = 0;

                foreach(Cls_Siparis siparisItem in siparisSatir)
                {
                    kumulatifSatisToplam = kumulatifSatisToplam + Convert.ToSingle((siparisItem.SiparisMiktar * siparisItem.SiparisFiyat));
                    kdv = GetKdvOrani(siparisItem.Fisno, siparisItem.StokKodu);
                    qumulativeKdv = qumulativeKdv + Convert.ToSingle((siparisItem.SiparisFiyat * (kdv / 100) * siparisItem.SiparisMiktar));
                    variables.Counter++;
                }


                string satisCariKodu = siparisMas.First().AssociatedCari.SatisCariKodu;
                string teslimCariKodu = siparisMas.First().AssociatedCari.TeslimCariKodu;
                int dovizTipi = Convert.ToInt32(siparisMas.First().AssociatedCari.DovizTipi);
                int siparisTipi = Convert.ToInt32(siparisMas.First().AssociatedCari.SiparisTipi);
                string aciklama = siparisMas.First().SiparisAciklama;
                string destinasyon = siparisMas.First().Destinasyon;
                string POno = siparisMas.First().POnumarasi;
                string fisno = siparisMas.First().Fisno;
                int portalID = login.GetPortalID();
                
                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parametersMas = new Dictionary<string, (SqlDbType, int, int, object)>
                {
                    { "@satisCariKodu", (SqlDbType.NVarChar, 15, 0,  satisCariKodu)},
                    { "@dovtip", (SqlDbType.TinyInt, 0, 0, dovizTipi) },
                    { "@teslimCariKod", (SqlDbType.NVarChar, 15, 0, teslimCariKodu) },
                    { "@aciklama", (SqlDbType.NVarChar, 35, 0, aciklama) },
                    { "@satisIhracatMi", (SqlDbType.TinyInt, 0, 0, siparisTipi) },
                    { "@brutTutar", (SqlDbType.Float, 0, 0, kumulatifSatisToplam) },
                    { "@toplamKdv", (SqlDbType.Float, 0, 0, qumulativeKdv) },
                    { "@destinasyon", (SqlDbType.NVarChar, 20, 0, destinasyon) },
                    { "@toplamSiparisKalemi", (SqlDbType.SmallInt, 0, 0, variables.Counter) },
                    { "@portalID", (SqlDbType.SmallInt, 0, 0, portalID) },
                    { "@POno", (SqlDbType.NVarChar, 100, 0, POno) },
                    { "@fisno", (SqlDbType.NVarChar, 15, 0, fisno) },
                };

                    variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpUpdateOnayBekleyenSiparisGenel", Variables.Yil_, parametersMas);

                    if (!variables.Result)
                        return variables.Result;


                int maxSira = GetMaxSiraNoFromOnayBekleyen(fisno);
                    if(maxSira == 0)
                        return false;
                    
                    if(maxSira > variables.Counter)
                        variables.Result = DeleteExtraSiparisSatir(fisno,variables.Counter);

                    if (!variables.Result)
                    return variables.Result;

                variables.Counter = 0;

                foreach (Cls_Siparis siparisItem in siparisSatir)
                { 

                siparisItem.StokKDV = GetKdvOrani(siparisItem.Fisno, siparisItem.StokKodu);
                variables.Counter++;

                Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parametersSatir = new Dictionary<string, (SqlDbType, int, int, object)>
                    {
                        { "@stokKodu", (SqlDbType.NVarChar, 35, 0, siparisItem.StokKodu) },
                        { "@siparisMiktar", (SqlDbType.Int, 0, 0, siparisItem.SiparisMiktar) },
                        { "@siparisFiyat", (SqlDbType.Float, 0, 0, siparisItem.SiparisFiyat) },
                        { "@satisCariKodu", (SqlDbType.NVarChar, 35, 0, satisCariKodu) },
                        { "@dovtip", (SqlDbType.TinyInt, 0, 0, dovizTipi) },
                        { "@teslimCariKod", (SqlDbType.NVarChar, 15, 0, teslimCariKodu) },
                        { "@sira", (SqlDbType.SmallInt, 0, 0, variables.Counter) },
                        { "@teslimTarih", (SqlDbType.DateTime, 0, 0, string.Format(siparisItem.TerminTarih.ToString(), "yyyy-MM-dd")) },
                        { "@kdv", (SqlDbType.Float, 0, 0, siparisItem.StokKDV) },
                        { "@fisno", (SqlDbType.NVarChar, 15, 0, fisno) },
                    };

                        variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpUpdateOnayBekleyenSiparisSatir", Variables.Yil_, parametersSatir);

                        if (!variables.Result)
                        return variables.Result;

                }

                return variables.Result;
            }
            catch 
            {
                return false;
            }
        }
        private int GetMaxSiraNoFromOnayBekleyen(string fisno)
        {
            try
            {
                int maxSira = 0;
                Variables.Query_ = "select max(stra_sipkont) from WERPSIPATRA where FISNO=@fisno";

                SqlParameter[] parameterArray = new SqlParameter[1]; // Initialize the array with one element

                parameterArray[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameterArray[0].Value = fisno;


                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameterArray))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        maxSira = reader.GetInt16(0);
                    }
                }
                return maxSira;

            }
            catch
            { return 0; }
        }

        public ObservableCollection<Cls_Siparis> GetTalepCollection(Dictionary<string, string> talepRestrictions, int pageNumber)
        {
            try
            {
                int depoKodu = 0;
                if (Variables.Departman_.Contains("Moduler"))
                {
                    Variables.Query_ = "Select * from vbvTalepIhtiyacListeSerbestHamKoduMod (nolock) where 1=1 ";
                    depoKodu = 30;
                }
                else
                {
                    Variables.Query_ = "Select * from vbvTalepIhtiyacListeSerbestHamKoduDos (nolock) where 1=1 ";
                    depoKodu = 10;
                }

                Variables.Counter_ = 0;
                if (talepRestrictions.ContainsKey("@stokKodu"))
                {
                    Variables.Query_ += " and StokKodu like '%' + @stokKodu + '%' ";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod1"))
                {
                    Variables.Query_ += " and kod1 = @kod1";
                    Variables.Counter_++;
                }
               

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;
                if (talepRestrictions.ContainsKey("@stokKodu"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = talepRestrictions["@stokKodu"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod1"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod1"];
                    Variables.Counter_++;
                }
                

                Variables.Query_ += $" order by StokKodu desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";
                ObservableCollection <Cls_Siparis> talepCollection = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    Cls_Siparis model = new();
                    //model.Fisno = reader["SiparisNumarasi"] is DBNull ? "" : reader["SiparisNumarasi"].ToString();
                    //model.FisSira = reader["SiparisSira"] is DBNull ? 0 : Convert.ToInt32(reader["SiparisSira"]);
                    //model.PlanAdi = reader["PlanAdi"] is DBNull ? "" : reader["PlanAdi"].ToString();
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.StokAdi = reader["StokAdi"] is DBNull ? "" : reader["StokAdi"].ToString();
                    model.Kod1 = reader["Kod1"] is DBNull ? "" : reader["Kod1"].ToString();
                    model.Birim = reader["Brm"] is DBNull ? "" : reader["Brm"].ToString();
                    model.DepoKodu = depoKodu;
                    model.IEihtiyac = reader["Kalan_IE_IhtiyacMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["Kalan_IE_IhtiyacMiktar"]);
                    model.ToplamSiparisMiktar = reader["ToplamAcikSiparisMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamAcikSiparisMiktar"]);
                    model.ToplamTalepMiktar = reader["ToplamTalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["ToplamTalepMiktar"]);
                    //model.PlanAcikSiparisMiktar = reader["PlanAcikSiparisMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["PlanAcikSiparisMiktar"]);
                    //model.PlanTalepMiktar = reader["PlanTalepMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["PlanTalepMiktar"]);
                    model.TalepSimulasyonIhtiyacMiktar = reader["TalepSimulasyonIhtiyacMiktar"] is DBNull ? 0 : Convert.ToSingle(reader["TalepSimulasyonIhtiyacMiktar"]);
                    model.Depo10Bakiye = reader["DepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["DepoBakiye"]);
                    model.Depo15Bakiye = reader["UretimDepoBakiye"] is DBNull ? 0 : Convert.ToSingle(reader["UretimDepoBakiye"]);
                    model.MinimumSiparisMiktar = reader["MinSipMik"] is DBNull ? 0 : Convert.ToSingle(reader["MinSipMik"]);
                    model.StokKDV = reader["KDV"] is DBNull ? 0 : Convert.ToSingle(reader["KDV"]);

                    return model;
                });
                return talepCollection;
            }
            catch
            {
                return null;
            }
        }
        private bool DeleteExtraSiparisSatir(string fisno, int maxSira)
        {
            try
            {

                Variables.Query_ = "delete from WERPSIPATRA where FISNO=@fisno and stra_sipkont>@maxSira";

                SqlParameter[] parameterArray = new SqlParameter[2]; // Initialize the array with two elements

                parameterArray[0] = new SqlParameter("@fisno", SqlDbType.NVarChar, 15);
                parameterArray[0].Value = fisno;
                parameterArray[1] = new SqlParameter("@maxSira", SqlDbType.Int);
                parameterArray[1].Value = maxSira;

                variables.Result = dataLayer.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameterArray);

                if (!variables.Result)
                    return false;

                return true;

            }
            catch
            { return false; }
        }

        //irsaliye kayıt için sipariş listele
        public ObservableCollection<Cls_Siparis> PopulateSatisIrsaliyesiSiparisListele(Dictionary<string, string> constraints, string fabrika)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvSatisIrsaliyesiSiparisListele where 1=1 ";

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["cariIsim"]))
                {
                    Variables.Query_ = Variables.Query_ + "and cariIsım like '%' + @cariIsim + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    if (constraints["siparisNumarasi"].Length > 15)
                    {
                        constraints["siparisNumarasi"] = constraints["siparisNumarasi"].Substring(0, 15);
                    }
                    Variables.Query_ = Variables.Query_ + "and siparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    Variables.Query_ = Variables.Query_ + "and siparisSira = @siparisSira ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;

                if (!string.IsNullOrWhiteSpace(constraints["cariIsim"]))
                {
                    parameters[variables.Counter] = new("@cariIsim", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["cariIsim"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                    parameters[variables.Counter].Value = constraints["siparisSira"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new("@stokAdi", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["stokAdi"];

                    variables.Counter++;
                }
                string cariKodu = string.Empty;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,fabrika))

                {
                    if (!reader.HasRows) { return null; }
                    temp_coll_sip.Clear();

                    while (reader.Read())
                    {
                        // Create an instance of the ViewModel and populate it from the DataRow
                        Cls_Siparis siparis = new Cls_Siparis
                        {
                            Fisno = reader["siparisNumarasi"].ToString(),
                            FisSira = Convert.ToInt32(reader["siparisSira"]),
                            StokKodu = reader["stokKodu"].ToString(),
                            StokAdi = reader["stokAdi"].ToString(),
                            SiparisMiktar = Convert.ToInt32(reader["siparisMiktar"]),
                            SiparisTeslimMiktar = Convert.ToInt32(reader["teslimMiktar"]),
                            SiparisBakiye = Convert.ToInt32(reader["bakiye"]),
                            DepoKodu = Convert.ToInt32(reader["depoKodu"]),
                            SiparisTarih = reader["siparisTarih"].ToString().Substring(0,11),
                            SiparisFiyat = Convert.ToSingle(reader["siparisFiyat"]),
                        };
                        siparis.AssociatedCari.TeslimCariKodu = reader["cariKodu"].ToString();
                        siparis.AssociatedCari.TeslimCariAdi = reader["cariIsim"].ToString();
                        temp_coll_sip.Add(siparis);
                    }
                }
                SiparisCollection = temp_coll_sip;
                return SiparisCollection;
            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Siparis> PopulateTedarikciSiparisListele(Dictionary<string, string> constraints, string fabrika)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvTedarikciSiparisListele where 1=1 ";

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["cariIsim"]))
                {
                    Variables.Query_ = Variables.Query_ + "and cariIsim like '%' + @cariIsim + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    if (constraints["siparisNumarasi"].Length > 15)
                    {
                        constraints["siparisNumarasi"] = constraints["siparisNumarasi"].Substring(0, 15);
                    }
                    Variables.Query_ = Variables.Query_ + "and siparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    Variables.Query_ = Variables.Query_ + "and siparisSira = @siparisSira ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;

                if (!string.IsNullOrWhiteSpace(constraints["cariIsim"]))
                {
                    parameters[variables.Counter] = new("@cariIsim", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["cariIsim"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                    parameters[variables.Counter].Value = constraints["siparisSira"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new("@stokAdi", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["stokAdi"];

                    variables.Counter++;
                }
                string cariKodu = string.Empty;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters,fabrika))

                {
                    if (!reader.HasRows) { return null; }
                    temp_coll_sip.Clear();

                    while (reader.Read())
                    {
                        // Create an instance of the ViewModel and populate it from the DataRow
                        Cls_Siparis siparis = new Cls_Siparis
                        {
                            Fisno = reader["siparisNumarasi"].ToString(),
                            FisSira = Convert.ToInt32(reader["siparisSira"]),
                            StokKodu = reader["stokKodu"].ToString(),
                            StokAdi = reader["stokAdi"].ToString(),
                            TedarikSiparisMiktar = Convert.ToSingle(reader["siparisMiktar"]),
                            TedarikTeslimMiktar = Convert.ToSingle(reader["teslimMiktar"]),
                            TedarikSiparisBakiye = Convert.ToSingle(reader["bakiye"]),
                            DepoKodu = Convert.ToInt32(reader["depoKodu"]),
                            SiparisTarih = reader["siparisTarih"].ToString().Substring(0,11),
                            SiparisFiyat = Convert.ToSingle(reader["siparisFiyat"]),
                        };
                        cariKodu = reader["cariKodu"].ToString();
                        siparis.AssociatedCari.TedarikciCariKodu = cariKodu;
                        temp_coll_sip.Add(siparis);
                    }
                }
                SiparisCollection = temp_coll_sip;
                return SiparisCollection;
            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Siparis> PopulateTedarikciSiparisListele(Dictionary<string, string> constraints)
        {
            try
            {
                Variables.Query_ = "SELECT * FROM vbvTedarikciSiparisListele where 1=1 ";

                variables.Counter = 0;


                if (!string.IsNullOrWhiteSpace(constraints["cariKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and cariKodu like '%' + @cariKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    if (constraints["siparisNumarasi"].Length > 15)
                    {
                        constraints["siparisNumarasi"] = constraints["siparisNumarasi"].Substring(0, 15);
                    }
                    Variables.Query_ = Variables.Query_ + "and siparisNumarasi like '%' + @siparisNumarasi + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    Variables.Query_ = Variables.Query_ + "and siparisSira = @siparisSira ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokKodu like '%' + @stokKodu + '%' ";
                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    Variables.Query_ = Variables.Query_ + "and stokAdi like '%' + @stokAdi + '%' ";
                    variables.Counter++;
                }

                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;

                if (!string.IsNullOrWhiteSpace(constraints["cariKodu"]))
                {
                    parameters[variables.Counter] = new("@cariKodu", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["cariKodu"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisNumarasi"]))
                {
                    parameters[variables.Counter] = new("@siparisNumarasi", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = constraints["siparisNumarasi"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["siparisSira"]))
                {
                    parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                    parameters[variables.Counter].Value = constraints["siparisSira"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokKodu"]))
                {
                    parameters[variables.Counter] = new("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[variables.Counter].Value = constraints["stokKodu"];

                    variables.Counter++;
                }

                if (!string.IsNullOrWhiteSpace(constraints["stokAdi"]))
                {
                    parameters[variables.Counter] = new("@stokAdi", SqlDbType.NVarChar, 500);
                    parameters[variables.Counter].Value = constraints["stokAdi"];

                    variables.Counter++;
                }
                string cariKodu = string.Empty;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters))

                {
                    if (!reader.HasRows) { return null; }
                    temp_coll_sip.Clear();

                    while (reader.Read())
                    {
                        // Create an instance of the ViewModel and populate it from the DataRow
                        Cls_Siparis siparis = new Cls_Siparis
                        {
                            Fisno = reader["siparisNumarasi"].ToString(),
                            FisSira = Convert.ToInt32(reader["siparisSira"]),
                            StokKodu = reader["stokKodu"].ToString(),
                            StokAdi = reader["stokAdi"].ToString(),
                            Kod1 = reader["kod1"].ToString(),
                            TedarikSiparisMiktar = Convert.ToSingle(reader["siparisMiktar"]),
                            TedarikTeslimMiktar = Convert.ToSingle(reader["teslimMiktar"]),
                            TedarikSiparisBakiye = Convert.ToSingle(reader["bakiye"]),
                            DepoKodu = Convert.ToInt32(reader["depoKodu"]),
                            SiparisTarih = reader["siparisTarih"].ToString().Substring(0,11),
                            SiparisFiyat = Convert.ToSingle(reader["siparisFiyat"]),
                            TedarikGirisMiktar = Convert.ToSingle(reader["bakiye"]),
                            Birim = reader["birim"].ToString(),
                        };
                        cariKodu = reader["cariKodu"].ToString();
                        siparis.AssociatedCari.TedarikciCariKodu = cariKodu;
                        temp_coll_sip.Add(siparis);
                    }
                }
                SiparisCollection = temp_coll_sip;
                return SiparisCollection;
            }
            catch { return null; }

        }
        public ObservableCollection<Cls_Siparis> PopulateOrderReportCollection(Dictionary<string, string> restrictionPairs, string restrictionQueries)
        {
            try
            {
                if (restrictionPairs != null)
                {
                    Variables.Query_ = "select * from vbvSiparisStokUretimDurum (nolock)  where 1 = 1 " + restrictionQueries;
                    variables.Counter = 0;



                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and [Sipariş Numarası] like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and [Sip Sıra] = @siparisSira";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and [Stok Kodu] like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and [Stok Adı] like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@cariAdi"))
                    {
                        Variables.Query_ += " and [Teslim Cari] like '%' + @cariAdi + '%'";
                        variables.Counter++;
                    }

                    if (!string.IsNullOrEmpty(restrictionQueries))
                        Variables.Query_ += restrictionQueries;

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];

                    variables.Counter = 0;
                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        parameters[variables.Counter] = new("@siparisNo", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisNo"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                        parameters[variables.Counter].Value = Convert.ToInt32(restrictionPairs["@siparisSira"]);
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        parameters[variables.Counter] = new("@stokKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@stokKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        parameters[variables.Counter] = new("@stokAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@stokAdi"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@cariAdi"))
                    {
                        parameters[variables.Counter] = new("@cariAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@cariAdi"];
                        variables.Counter++;
                    }


                    ObservableCollection<Cls_Siparis> siparisCollection = dataLayer.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, variables.Fabrika, parameters, reader =>
                    {
                        Cls_Siparis model = new();
                        model.Fisno = reader[0] is DBNull ? "" : reader[0].ToString();
                        model.FisSira = reader[1] is DBNull ? 0 : Convert.ToInt32(reader[1]);
                        model.SiparisDurum = reader[2] is DBNull ? "" : reader[2].ToString();
                        model.SiparisTarihD = reader[3] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader[3]);
                        model.AssociatedCari.TeslimCariAdi = reader["Teslim Cari"] is DBNull ? "" : reader["Teslim Cari"].ToString();
                        model.Destinasyon = reader["Destinasyon"] is DBNull ? "" : reader["Destinasyon"].ToString();
                        model.POnumarasi = reader["PO Numarası"] is DBNull ? "" : reader["PO Numarası"].ToString();
                        model.StokKodu = reader["Stok Kodu"] is DBNull ? "" : reader["Stok Kodu"].ToString();
                        model.StokAdi = reader["Stok Adı"] is DBNull ? "" : reader["Stok Adı"].ToString();
                        model.SiparisMiktar = reader["Sip Miktar"] is DBNull ? 0 : Convert.ToInt32(reader["Sip Miktar"]);
                        model.SiparisTeslimMiktar = reader["Yük Mik"] is DBNull ? 0 : Convert.ToInt32(reader["Yük Mik"]);
                        model.UretilenMiktar = reader["Ürs Mik"] is DBNull ? 0 : Convert.ToInt32(reader["Ürs Mik"]);
                        model.DepoMiktar = reader["Depo Mik"] is DBNull ? 0 : Convert.ToInt32(reader["Depo Mik"]);

                        return model;
                    });
                    if (siparisCollection.Count() == 0 ||
                        siparisCollection == null)
                        return null;

                    return siparisCollection;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    
        public ObservableCollection<Cls_Siparis> PopulateUrunAgaciOlmayanSiparisler(int pageNumber)
        { 
           
            try 
	        {
                ObservableCollection<Cls_Siparis> UrunAgaciOlmayanSiparislerCollection = new();
                Variables.Query_ = "Select * from vbvUrunAgaciOlmayanSiparisler";
                string cariKodu = string.Empty;
                string cariAdi = string.Empty;
                Variables.Query_ += $" order by siparisNumarasi desc offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))

                {
                    if (!reader.HasRows) { return null; }
                    temp_coll_sip.Clear();

                    while (reader.Read())
                    {
                        // Create an instance of the ViewModel and populate it from the DataRow
                        Cls_Siparis siparis = new Cls_Siparis
                        {
                            Fisno = reader["siparisNumarasi"].ToString(),
                            FisSira = Convert.ToInt32(reader["siparisSira"]),
                            StokKodu = reader["stokKodu"].ToString(),
                            StokAdi = reader["stokAdi"].ToString(),
                            SiparisTarih = reader["siparisTarih"].ToString().Substring(0, 11),
                            TerminTarih = Convert.ToDateTime(reader["teslimTarih"]),
                        };
                        cariKodu = reader["cariKodu"].ToString();
                        cariAdi = reader["cariAdi"].ToString();
                        siparis.AssociatedCari.TeslimCariKodu = cariKodu;
                        siparis.AssociatedCari.TeslimCariAdi = cariAdi;
                        temp_coll_sip.Add(siparis);
                    }
                }
                UrunAgaciOlmayanSiparislerCollection = temp_coll_sip;
                return UrunAgaciOlmayanSiparislerCollection;
            }
            catch 
	        {
                return null;
            }
        }
        public int CountUrunAgaciOlmayanSiparisler(int pageNumber)
        {
            try
            {
                Variables.Query_ = Variables.Query_ = "Select count(*) from vbvUrunAgaciOlmayanSiparisler"; 
                
                int totalCount = 0;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, Variables.Fabrika_))
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
        public ObservableCollection<Cls_Siparis> PopulateFiyatsizSiparisler()
        {

            try
            {
                ObservableCollection<Cls_Siparis> FiyatsizlarCollection = new();
                Variables.Query_ = "Select * from vbvFiyatsizSiparisler";
                string cariKodu = string.Empty;
                string cariAdi = string.Empty;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))

                {
                    if (!reader.HasRows) { return null; }
                    temp_coll_sip.Clear();

                    while (reader.Read())
                    {
                        // Create an instance of the ViewModel and populate it from the DataRow
                        Cls_Siparis siparis = new Cls_Siparis
                        {
                            Fisno = reader["siparisNumarasi"].ToString(),
                            FisSira = Convert.ToInt32(reader["siparisSira"]),
                            StokKodu = reader["stokKodu"].ToString(),
                            StokAdi = reader["stokAdi"].ToString(),
                        };
                        cariKodu = reader["cariKodu"].ToString();
                        cariAdi = reader["cariAdi"].ToString();
                        siparis.AssociatedCari.TeslimCariKodu = cariKodu;
                        siparis.AssociatedCari.TeslimCariAdi = cariAdi;
                        temp_coll_sip.Add(siparis);
                    }
                }
                FiyatsizlarCollection = temp_coll_sip;
                return FiyatsizlarCollection;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfFazTesExceed(double siparisMiktar, double tedarikGirisMiktar, string stokKodu, string kod1,string birim)
        {
            try
            {
                
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu",SqlDbType.NVarChar,35);
                parameter[0].Value = stokKodu;
                Variables.Query_ = "select top 1 isnull(faztesoran,0) from tblcaristok where stok_kodu=@stokKodu";
                double faztesoran = dataLayer.Get_One_Double_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

                switch (kod1)
                {
                    case "KUMAS":
                        if (siparisMiktar < 100)
                            return true;
                        else
                        {
                            if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                return false;
                        }
                        break;
                    case "K.BANT":
                        if (siparisMiktar < 500)
                            return true;
                        else
                        {
                            if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                return false;
                        }
                        break;
                    case "SUNGER":
                        if(birim == "AD")
                        {
                            if (siparisMiktar < 20)
                                return true;
                            {
                                if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                    return false;
                            }
                        }
                        if (birim == "M3")
                        {
                            if (siparisMiktar < 5)
                                return true;
                            {
                                if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                    return false;
                            }
                        }
                     
                        break;
                    case "KAZTUYU":
                        
                            if (siparisMiktar < 50)
                                return true;
                            {
                                if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                    return false;
                            }
                        
                        break;
                    case "PLAKA":
                        if (birim == "AD")
                        {
                            if (siparisMiktar < 30)
                                return true;
                            {
                                if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                    return false;
                            }
                        }
                        if (birim == "M2")
                        {
                            if (siparisMiktar < 300)
                                return true;
                            {
                                if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                                    return false;
                            }
                        }

                        break;
                    default:
                        if (siparisMiktar + (siparisMiktar * (faztesoran / 100)) < tedarikGirisMiktar)
                            return false;
                        else return true;
                }
                return true;
                
            }
            catch 
            {
                return false;
            }
        }
        public int CheckIfIrsaliyeNoExists(string irsaliyeNumarasi,string fabrika)
        {
            try
            {
                Variables.Query_ = "select count(*) from tblsthar where fisno = @irsaliyeNumarasi";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameters[0].Value = irsaliyeNumarasi;
                
                variables.ResultInt = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika);
                return variables.ResultInt;


            }
            catch
            {
                return -1;
            }
        }

        public int CountTalepAcList(Dictionary<string, string> talepRestrictions, int pageNumber)
        {
            try
            {
                Variables.Query_ = "Select count(*) from vbvTalepDurumListele (nolock) where 1=1 ";
                Variables.Counter_ = 0;
                if (talepRestrictions.ContainsKey("@stokKodu"))
                {
                    Variables.Query_ += " and StokKodu like '%' + @stokKodu + '%' ";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod1"))
                {
                    Variables.Query_ += " and kod1 = @kod1";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod2"))
                {
                    Variables.Query_ += " and kod2 = @kod2";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod3"))
                {
                    Variables.Query_ += " and kod3 = @kod3";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod4"))
                {
                    Variables.Query_ += " and kod4 = @kod4";
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod5"))
                {
                    Variables.Query_ += " and kod5 = @kod5";
                    Variables.Counter_++;
                }

                SqlParameter[] parameters = new SqlParameter[Variables.Counter_];
                Variables.Counter_ = 0;
                if (talepRestrictions.ContainsKey("@stokKodu"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = talepRestrictions["@stokKodu"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod1"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod1", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod1"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod2"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod2", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod2"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod3"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod3", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod3"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod4"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod4", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod4"];
                    Variables.Counter_++;
                }
                if (talepRestrictions.ContainsKey("@kod5"))
                {
                    parameters[Variables.Counter_] = new SqlParameter("@kod5", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = talepRestrictions["@kod5"];
                    Variables.Counter_++;
                }

                int totalCount = 0;
                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
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

        public bool GetFarkliSiparislerIcinVadeVeDovizTipi(string itemFisno,string collectionFisno,string fabrika)
        {
            try
            {
                Variables.Query_ =   "with cte as (" +
                                    "select odemegunu,DOVIZTIP,tipi from  tblsipamas (nolock) where fatirs_no =@itemFisno " +
                                    "union " +
                                    "select odemegunu, doviztip,tipi from tblsipamas (nolock) where fatirs_no =@collectionFisno) " +
                                    "select count(*) from cte";

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@itemFisno", SqlDbType.NVarChar, 15);
                parameters[0].Value = itemFisno;
                parameters[1] = new SqlParameter("@collectionFisno", SqlDbType.NVarChar, 15);
                parameters[1].Value = collectionFisno;
                variables.ResultInt = dataLayer.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, fabrika);

                if (variables.ResultInt != 1) 
                    return false;
                
                return true;

            }       
            catch 
            {
                return false;
            }
        }
        public bool IrsaliyeGeriAl(string irsaliyeNumarasi, string fabrika)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = irsaliyeNumarasi;

                variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpIrsaliyeGeriAl",Variables.Yil_,parameter,fabrika);
                return variables.Result;

            }
            catch
            {
                return false;
            }
        }
        public bool SatisIrsaliyesiGeriAl(string irsaliyeNumarasi, string fabrika)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = irsaliyeNumarasi;

                variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpSatisIrsaliyesiGeriAl", Variables.Yil_,parameter,fabrika);
                return variables.Result;

            }
            catch
            {
                return false;
            }
        }
        public bool IrsaliyeGeriAl(string irsaliyeNumarasi)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@irsaliyeNumarasi", SqlDbType.NVarChar, 15);
                parameter[0].Value = irsaliyeNumarasi;

                variables.Result = dataLayer.ExecuteStoredProcedureWithParameters("vbpIrsaliyeGeriAl",Variables.Yil_,parameter);
                return variables.Result;

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

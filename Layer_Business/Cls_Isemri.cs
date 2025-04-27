using Layer_Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Input;
using Layer_2_Common.Type;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Threading.Tasks;

namespace Layer_Business
{
    public class Cls_Isemri : INotifyPropertyChanged
    {
        public string PLAN_NO { get; set; } = string.Empty;
        public string TAKIP_NO { get; set; } = string.Empty;    
        public string SIPARIS_NO { get; set; } = string.Empty;
        public int SIPARIS_SIRA { get; set; }

        public string CARI_KODU { get; set; } = string.Empty;
        public string CARI_ADI { get; set; } = string.Empty;
        public string URUN_KODU { get; set; } = string.Empty;
        public string URUN_ADI { get; set; } = string.Empty;
        public string STOK_KODU { get; set; } = string.Empty;
        public string STOK_ADI { get; set; } = string.Empty;
        public int DEPO_KODU { get; set; }
        public int CIKIS_DEPO_KODU { get; set; }
        public string REFISEMRINO { get; set; } = string.Empty;
        public string ISEMRINO { get; set; } = string.Empty;
        public DateTime BildirimTarih { get; set; } = DateTime.Now;
        public int IE_MIKTAR { get; set; }
        public int KALAN_IE_MIKTAR { get; set; }
        public string HAM_KODU { get; set; } = string.Empty;
        public string HAM_ADI { get; set; } = string.Empty;

        public decimal RECETE_MIKTAR { get; set; }
        public decimal BILDIRILEN_MIKTAR { get; set; }
        public decimal TOPLAM_BILDIRILEN_MIKTAR { get; set; }
        public decimal BIRIM_HAM_MIKTAR { get; set; }
        public decimal TUKETILEN_MIKTAR { get; set; } //URETSON_MIKTAR*RECETE MIKTAR
        public int BildirilecekIsemriMiktar { get; set; }
        public string ID { get; set; } = string.Empty;
        public int ReceteID { get; set; }

        public string KullaniciAdi { get; set; } = string.Empty;

        public string UretimDurum { get; set; } = string.Empty;
        public string MAMUL_KODU { get; set; } = string.Empty;
        public string MAMUL_ADI { get; set; } = string.Empty;

        public string EskiHamKodu { get; set; } = string.Empty;
        public decimal EskiReceteMiktar { get; set; } = decimal.Zero;

        public string OPNO { get; set; } = string.Empty;
        public string SIPARIS_GENEL_ACIKLAMA { get; set; } = string.Empty;

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }


        private decimal _metre;

        public decimal Metre
        {
            get { return _metre; }
            set { _metre = value; }
        }

        private decimal _kumulatifIhtiyac = 0;

        public decimal KumulatifIhtiyac
        {
            get { return _kumulatifIhtiyac; }
            set { _kumulatifIhtiyac = value; }
        }
        private decimal _birimIhtiyac = 0;

        public decimal BirimIhtiyac
        {
            get { return _birimIhtiyac; }
            set { _birimIhtiyac = value; }
        }
        private decimal _katSayi = 0;

        public decimal KatSayi
        {
            get { return _katSayi; }
            set { _katSayi = value; }
        }

        public DateTime ArgeKayitTarihi { get; set; } = DateTime.MinValue;

        private ObservableCollection<Cls_Isemri> _isemirleriCollection;
        public ObservableCollection<Cls_Isemri> IsemirleriCollection
        {
            get { return _isemirleriCollection; }
            set { _isemirleriCollection = value; }
        }


        readonly ObservableCollection<Cls_Isemri> coll_isemri = new ObservableCollection<Cls_Isemri>();

        readonly DataLayer dataLayer = new DataLayer();
        DataTable dataTable = new DataTable();
        private SqlDataReader reader;
        Variables variables = new();
        LoginLogic login = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }

        public Cls_Isemri()
        {
            variables.Fabrika = login.GetFabrika();
            Variables.Fabrika_ = variables.Fabrika;
        }

        public ObservableCollection<Cls_Isemri> PopulateReceteGuncelleList(Dictionary<string, string> restrictionPairs)
        {
            try
            {
                variables.Counter = 0;

                Variables.Query_ = "select * from vbvIsemriReceteListele where 1=1 ";

                if (restrictionPairs.ContainsKey("@refisemrino"))
                {
                    Variables.Query_ += " and Refisemrino like '%' + @refisemrino + '%'";
                    variables.Counter++;
                }
                if (restrictionPairs.ContainsKey("@isemrino"))
                {
                    Variables.Query_ += " and Isemrino like '%' + @isemrino + '%'";
                    variables.Counter++;
                }
                if (restrictionPairs.ContainsKey("@takipNo"))
                {
                    Variables.Query_ += " and TakipNo like '%' + @takipNo + '%'";
                    variables.Counter++;
                }
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
                if (restrictionPairs.ContainsKey("@tamamlananlariListeleme"))
                {
                    Variables.Query_ += " and UretimDurum<>'T'";
                }



                SqlParameter[] parameters = new SqlParameter[variables.Counter];

                variables.Counter = 0;
                if (restrictionPairs.ContainsKey("@refisemrino"))
                {
                    parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                    variables.Counter++;
                }
                if (restrictionPairs.ContainsKey("@isemrino"))
                {
                    parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
                    variables.Counter++;
                }
                if (restrictionPairs.ContainsKey("@takipNo"))
                {
                    parameters[variables.Counter] = new("@takipNo", SqlDbType.NVarChar, 15);
                    parameters[variables.Counter].Value = restrictionPairs["@takipNo"];
                    variables.Counter++;
                }
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

                Variables.Query_ += " order by SiparisNumarasi,SiparisSira,TakipNo,Refisemrino,Isemrino,UrunKodu";

                ObservableCollection<Cls_Isemri> temp_coll_ie = new();

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                {

                    while (reader.Read())
                    {
                        Cls_Isemri isemri = new Cls_Isemri
                        {
                            SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                            SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                            TAKIP_NO = reader["TakipNo"].ToString(),
                            REFISEMRINO = reader["Refisemrino"].ToString(),
                            ISEMRINO = reader["Isemrino"].ToString(),
                            URUN_KODU = reader["urunKodu"].ToString(),
                            URUN_ADI = reader["urunAdi"].ToString(),
                            MAMUL_KODU = reader["mamulKodu"].ToString(),
                            MAMUL_ADI = reader["mamulAdi"].ToString(),
                            HAM_KODU = reader["hamKodu"].ToString(),
                            HAM_ADI = reader["hamAdi"].ToString(),
                            RECETE_MIKTAR = Convert.ToDecimal(reader["miktar"]),
                            UretimDurum = reader["UretimDurum"].ToString(),
                            OPNO = reader["OPNO"].ToString(),
                            ReceteID = Convert.ToInt32(reader["ReceteID"]),
                        };
                        temp_coll_ie.Add(isemri);
                    }
                    reader.Close();
                }

                return temp_coll_ie;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateMamulReceteList(string mamulKodu,bool tamamlananlariListeleme)
        {
            try
            {
                variables.Counter = 0;

                Variables.Query_ = "select ierec.isemrino, mamul_kodu,mamul_adi,ham_kodu,ham_adi,ierec.miktar,opno,ierec.inckeyno,ie.usk_status from tblisemrirec (nolock) ierec " +
                                    "left join (select stok_kodu,stok_adi as mamul_adi from tblstsabit(nolock)) sabit on ierec.mamul_kodu = sabit.stok_kodu " +
                                    "left join (select stok_kodu,stok_adi as ham_adi from tblstsabit(nolock)) sabitHam on ierec.ham_kodu = sabitHam.stok_kodu " +
                                    "left join tblisemri (nolock) ie on ierec.mamul_kodu = ie.stok_kodu and ierec.isemrino=ie.isemrino " +
                                    "where mamul_kodu=@mamulKodu ";
                if (tamamlananlariListeleme)
                    Variables.Query_ += " and usk_status<>'T' ";

                Variables.Query_ += " order by ie.isemrino"; 


                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new("@mamulKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = mamulKodu;


                ObservableCollection<Cls_Isemri> temp_coll_ie = new();

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        Cls_Isemri ie = new Cls_Isemri
                        {
                            ReceteID = Convert.ToInt32(reader["inckeyno"]),
                            ISEMRINO = reader["isemrino"].ToString(),
                            MAMUL_KODU = reader["mamul_kodu"].ToString(),
                            MAMUL_ADI = reader["mamul_adi"].ToString(),
                            HAM_KODU = reader["ham_kodu"].ToString(),
                            HAM_ADI = reader["ham_adi"].ToString(),
                            RECETE_MIKTAR = Convert.ToDecimal(reader["miktar"]),
                            OPNO = reader["opno"].ToString(),
                        };
                        temp_coll_ie.Add(ie);
                    }

                    reader.Close();
                }

                return temp_coll_ie;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateReceteEkleList(string mamulKodu,bool tamamlananlariListeleme)
        {
            try
            {
                variables.Counter = 0;

                Variables.Query_ = "select distinct SiparisNumarasi,SiparisSira,TakipNo,Refisemrino,ISEMRINO,mamulKodu,mamulAdi,urunKodu,urunAdi,uretimdurum,REPLICATE('0',4 - len(max(opno) + 1)) + cast(max(opno) + 1 as nvarchar(4)) as opno from vbvIsemriReceteListele " +
                                    " where mamulKodu=@mamulKodu ";

                if (tamamlananlariListeleme)
                    Variables.Query_ += " and UretimDurum<>'T' ";

                    Variables.Query_ += " group by SiparisNumarasi,SiparisSira,TakipNo,Refisemrino,ISEMRINO,mamulKodu,mamulAdi,urunKodu,urunAdi,uretimdurum"; 
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new("@mamulKodu", SqlDbType.NVarChar, 35);
                parameters[0].Value = mamulKodu;


                ObservableCollection<Cls_Isemri> temp_coll_ie = new();

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_))
                {

                    while (reader.Read())
                    {
                        Cls_Isemri isemri = new Cls_Isemri
                        {
                            SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                            SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                            TAKIP_NO = reader["TakipNo"].ToString(),
                            REFISEMRINO = reader["Refisemrino"].ToString(),
                            ISEMRINO = reader["Isemrino"].ToString(),
                            URUN_KODU = reader["urunKodu"].ToString(),
                            URUN_ADI = reader["urunAdi"].ToString(),
                            MAMUL_KODU = reader["mamulKodu"].ToString(),
                            MAMUL_ADI = reader["mamulAdi"].ToString(),
                            UretimDurum = reader["UretimDurum"].ToString(),
                            OPNO = reader["OPNO"].ToString(),
                        };
                        temp_coll_ie.Add(isemri);
                    }

                    reader.Close();
                }

                return temp_coll_ie;
            }
            catch
            {
                return null;
            }
        }
        
        public ObservableCollection<Cls_Isemri> PopulateIsemriBildirimList(Dictionary<string, string> restrictionPairs,bool isPaketBildirim_)
        {
            try
            {
                if (restrictionPairs == null)
                    return null;

                if (isPaketBildirim_)
                {

                    Variables.Departman_ = login.GetDepartment();
                    Variables.Query_ = "select * from vbvBildirilecekPaketIsemirleriListele where 1=1 ";
                    if (Variables.Departman_.Contains("Moduler"))
                    {
                        Variables.Query_ += " and ((StokKodu like 'PM%' or StokKodu like 'PT%' or StokKodu like 'PS%')";
                        Variables.Query_ += " OR UrunAdi like '%KARYOLA%')";
                    }
                    if (Variables.Departman_.Contains("Doseme"))
                        Variables.Query_ += " and (StokKodu like 'PD%' or StokKodu like 'PT%' or StokKodu like 'PS%')";
                }
                else 
                    Variables.Query_ = "select * from vbvBildirilecekIsemirleriListele where 1=1";
                    variables.Counter = 0;


                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and SiparisSira = @siparisSira ";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        Variables.Query_ += " and Refisemrino like '%' + @refisemrino + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and Isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and UrunAdi like  '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and StokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and StokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        Variables.Query_ += " and TakipNo like  '%' + @takipno + '%'";
                        variables.Counter++;
                    }

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
                        parameters[variables.Counter].Value = restrictionPairs["@siparisSira"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        parameters[variables.Counter] = new("@takipno", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@takipno"];
                        variables.Counter++;
                    }

                    coll_isemri.Clear();
                    if (IsemirleriCollection != null)
                        IsemirleriCollection.Clear();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {

                    if (reader == null)
                        return null;

                        while (reader.Read())
                        {
                            Cls_Isemri isemri = new Cls_Isemri
                            {
                                PLAN_NO = reader["Planno"].ToString(),
                                TAKIP_NO = reader["TakipNo"].ToString(),
                                CARI_KODU = reader["CariKod"].ToString(),
                                CARI_ADI = reader["CariIsim"].ToString(),
                                SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                                REFISEMRINO = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString(),
                                ISEMRINO = reader["Isemrino"].ToString(),
                                URUN_KODU = reader["UrunKodu"].ToString(),
                                URUN_ADI = reader["UrunAdi"].ToString(),
                                STOK_KODU = reader["StokKodu"].ToString(),
                                STOK_ADI = reader["StokAdi"].ToString(),
                                DEPO_KODU = Convert.ToInt32(reader["DepoKodu"]),
                                CIKIS_DEPO_KODU = Convert.ToInt32(reader["CikisDepoKodu"]),
                                IE_MIKTAR = Convert.ToInt32(reader["IsemriMiktar"]),
                                BILDIRILEN_MIKTAR = Convert.ToInt32(reader["UretilenMiktar"]),
                                KALAN_IE_MIKTAR = Convert.ToInt32(reader["KalanMiktar"]),
                            };
                            coll_isemri.Add(isemri);
                        }
                        reader.Close();
                    }
               
                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch
            {
                return null;
            }

        }
        public ObservableCollection<Cls_Isemri> PopulateIsemriGeriAlList(Dictionary<string, string> restrictionPairs,bool refisemriMi_)
        {
            try
            {
                if (restrictionPairs == null)
                {
                    return null;
                }
                if (refisemriMi_)
                    Variables.Query_ = "select * from vbvGeriAlinacakIsemirleriListeleReferans where 1=1";
                else 
                    Variables.Query_ = "select * from vbvGeriAlinacaKIsemirleriListele where 1=1 ";

                if (login.GetDepartment().Contains("Doseme") && 
                    !refisemriMi_)
                    Variables.Query_ += " and TakipNo like 'D%' ";

                if (login.GetDepartment().Contains("Moduler") &&
                    !refisemriMi_)
                    Variables.Query_ += " and TakipNo like 'M%' ";

                variables.Counter = 0;

                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and SiparisSira = @siparisSira ";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        Variables.Query_ += " and refisemrino like '%' + @refisemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and urunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and urunAdi like  '%' + @urunAdi + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and stokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and stokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@cariAdi"))
                    {
                        Variables.Query_ += " and cariIsim like  '%' + @cariAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        Variables.Query_ += " and TakipNo like  '%' + @takipno + '%'";
                        variables.Counter++;
                    }

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
                        parameters[variables.Counter].Value = restrictionPairs["@siparisSira"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        parameters[variables.Counter] = new("@takipno", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@takipno"];
                        variables.Counter++;
                    }

                    coll_isemri.Clear();
                    if (IsemirleriCollection != null)
                        IsemirleriCollection.Clear();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {
                        if (reader == null)
                            return null;

                        while (reader.Read())
                        {
                            if(!refisemriMi_)
                            { 
                                Cls_Isemri isemri = new Cls_Isemri
                                {
                                    TAKIP_NO = reader["TakipNo"].ToString(),
                                    CARI_KODU = reader["CariKod"].ToString(),
                                    CARI_ADI = reader["CariIsim"].ToString(),
                                    SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                                    SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                                    REFISEMRINO = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString(),
                                    ISEMRINO = reader["Isemrino"].ToString(),
                                    URUN_KODU = reader["UrunKodu"].ToString(),
                                    URUN_ADI = reader["UrunAdi"].ToString(),
                                    STOK_KODU = reader["StokKodu"].ToString(),
                                    STOK_ADI = reader["StokAdi"].ToString(),
                                    IE_MIKTAR = Convert.ToInt32(reader["IsemriMiktar"]),
                                    BILDIRILEN_MIKTAR = Convert.ToInt32(reader["UretilenMiktar"]),
                                };
                                coll_isemri.Add(isemri);
                            }
                            if(refisemriMi_)
                            { 
                                Cls_Isemri isemri = new Cls_Isemri
                                {
                                    TAKIP_NO = reader["TakipNo"].ToString(),
                                    CARI_KODU = reader["CariKod"].ToString(),
                                    CARI_ADI = reader["CariIsim"].ToString(),
                                    SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                                    SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                                    REFISEMRINO = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString(),
                                    URUN_KODU = reader["UrunKodu"].ToString(),
                                    URUN_ADI = reader["UrunAdi"].ToString(),
                                };
                                coll_isemri.Add(isemri);
                            }
                        }
                        reader.Close();
                    }
                
                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateUSKGeriAlList(Dictionary<string, string> restrictionPairs,bool refisemriMi_)
        {
            try
            {
                if (restrictionPairs == null)
                {
                    return null;
                }
                if (refisemriMi_)
                    Variables.Query_ = "select * from vbvGeriAlinacakUretimBildirilmleriListeleReferans where 1=1";
                else 
                    Variables.Query_ = "select * from vbvGeriAlinacakUretimBildirilmleriListele where 1=1 ";

                if (login.GetDepartment().Contains("Doseme") && 
                    !refisemriMi_)
                    Variables.Query_ += " and TakipNo like 'D%' ";

                if (login.GetDepartment().Contains("Moduler") &&
                    !refisemriMi_)
                    Variables.Query_ += " and TakipNo like 'M%' ";

                if(login.GetDepartment() == "Moduler Paketleme")
                    Variables.Query_ += " and StokKodu like 'PM%' ";
                if(login.GetDepartment() == "Doseme Paketleme")
                    Variables.Query_ += " and StokKodu like 'PD%' ";

                variables.Counter = 0;

                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and SiparisSira = @siparisSira ";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        Variables.Query_ += " and refisemrino like '%' + @refisemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and urunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and urunAdi like  '%' + @urunAdi + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and stokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and stokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@cariAdi"))
                    {
                        Variables.Query_ += " and cariIsim like  '%' + @cariAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        Variables.Query_ += " and TakipNo like  '%' + @takipno + '%'";
                        variables.Counter++;
                    }

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
                        parameters[variables.Counter].Value = restrictionPairs["@siparisSira"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        parameters[variables.Counter] = new("@takipno", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@takipno"];
                        variables.Counter++;
                    }

                    coll_isemri.Clear();
                    if (IsemirleriCollection != null)
                        IsemirleriCollection.Clear();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {
                        if (reader == null)
                            return null;

                        while (reader.Read())
                        {
                            if(!refisemriMi_)
                            { 
                                Cls_Isemri isemri = new Cls_Isemri
                                {
                                    TAKIP_NO = reader["TakipNo"].ToString(),
                                    CARI_KODU = reader["CariKod"].ToString(),
                                    CARI_ADI = reader["CariIsim"].ToString(),
                                    SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                                    SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                                    REFISEMRINO = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString(),
                                    ISEMRINO = reader["Isemrino"].ToString(),
                                    URUN_KODU = reader["UrunKodu"].ToString(),
                                    URUN_ADI = reader["UrunAdi"].ToString(),
                                    STOK_KODU = reader["StokKodu"].ToString(),
                                    STOK_ADI = reader["StokAdi"].ToString(),
                                    IE_MIKTAR = Convert.ToInt32(reader["IsemriMiktar"]),
                                    BILDIRILEN_MIKTAR = Convert.ToInt32(reader["UretilenMiktar"]),
                                };
                                coll_isemri.Add(isemri);
                            }
                            if(refisemriMi_)
                            { 
                                Cls_Isemri isemri = new Cls_Isemri
                                {
                                    TAKIP_NO = reader["TakipNo"].ToString(),
                                    CARI_KODU = reader["CariKod"].ToString(),
                                    CARI_ADI = reader["CariIsim"].ToString(),
                                    SIPARIS_NO = reader["SiparisNumarasi"].ToString(),
                                    SIPARIS_SIRA = Convert.ToInt32(reader["SiparisSira"]),
                                    REFISEMRINO = reader["Refisemrino"] is DBNull ? "" : reader["Refisemrino"].ToString(),
                                    URUN_KODU = reader["UrunKodu"].ToString(),
                                    URUN_ADI = reader["UrunAdi"].ToString(),
                                };
                                coll_isemri.Add(isemri);
                            }
                        }
                        reader.Close();
                    }
                
                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateIsemriBildirimList(Dictionary<string, string> restrictionPairs)
        {
            try
            {
                if (restrictionPairs != null)
                {
                    Variables.Query_ = "select * from vbvBildirilecekIsemirleriListele where isemrino=refisemrino ";
                    variables.Counter = 0;

                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and siparisNo like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and stokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and stokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@cariAdi"))
                    {
                        Variables.Query_ += " and cariAdi like  '%' + @cariAdi + '%'";
                        variables.Counter++;
                    }

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];

                    variables.Counter = 0;
                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        parameters[variables.Counter] = new("@siparisNo", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisNo"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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

                    coll_isemri.Clear();
                    if (IsemirleriCollection != null)
                        IsemirleriCollection.Clear();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {

                        while (reader.Read())
                        {
                            Cls_Isemri isemri = new Cls_Isemri
                            {
                                SIPARIS_NO = reader["siparisNo"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["siparisSira"]),
                                CARI_KODU = reader["cariKod"].ToString(),
                                CARI_ADI = reader["cariAdi"].ToString(),
                                ISEMRINO = reader["isemrino"].ToString(),
                                URUN_KODU = reader["urunKodu"].ToString(),
                                URUN_ADI = reader["urunAdi"].ToString(),
                                STOK_KODU = reader["stokKodu"].ToString(),
                                STOK_ADI = reader["stokAdi"].ToString(),
                                IE_MIKTAR = Convert.ToInt32(reader["isemriMiktar"]),
                                BILDIRILEN_MIKTAR = Convert.ToInt32(reader["bildirilenIeMiktar"]),
                                KALAN_IE_MIKTAR = Convert.ToInt32(reader["kalanIeMiktar"]),
                                BildirilecekIsemriMiktar = Convert.ToInt32(reader["kalanIeMiktar"]),
                            };
                            coll_isemri.Add(isemri);
                        }
                        reader.Close();
                    }
                }
                else
                {
                    Variables.Query_ = "select * from vbvBildirilecekIsemirleriListele where refisemrino=isemrino and ";

                    coll_isemri.Clear();
                    IsemirleriCollection.Clear();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.Yil_, variables.Fabrika))
                    {

                        while (reader.Read())
                        {
                            Cls_Isemri isemri = new Cls_Isemri
                            {
                                SIPARIS_NO = reader["siparisNo"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["siparisSira"]),
                                ISEMRINO = reader["isemrino"].ToString(),
                                URUN_KODU = reader["urunKodu"].ToString(),
                                URUN_ADI = reader["urunAdi"].ToString(),
                                STOK_KODU = reader["stokKodu"].ToString(),
                                STOK_ADI = reader["stokAdi"].ToString(),
                                IE_MIKTAR = Convert.ToInt32(reader["isemriMiktar"]),
                                BILDIRILEN_MIKTAR = Convert.ToInt32(reader["bildirilenIeMiktar"]),
                                KALAN_IE_MIKTAR = Convert.ToInt32(reader["kalanIeMiktar"]),
                                BildirilecekIsemriMiktar = Convert.ToInt32(reader["kalanIeMiktar"]),
                            };
                            coll_isemri.Add(isemri);
                        }
                        reader.Close();
                    }
                }

                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateUretimDurumList(Dictionary<string, string> restrictionPairs,int pageNumber)
        {
            try
            {
                if (restrictionPairs == null)
                    return null;
                
                    Variables.Query_ = "select * from vbvUretimDurumListele where 1=1 ";
                    variables.Counter = 0;

                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        Variables.Query_ += " and refisemrino like '%' + @refisemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and urunAdi like  '%' + @urunAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and StokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and StokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and SiparisSira = @siparisSira ";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        Variables.Query_ += " and TakipNo like  '%' + @takipno + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@baslangicTarih"))
                    {
                        Variables.Query_ += " and UretimTarihi >=@baslangicTarih";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@bitisTarih"))
                    {
                        Variables.Query_ += " and UretimTarihi <=@bitisTarih";
                        variables.Counter++;
                    }

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];

                    variables.Counter = 0;
                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        parameters[variables.Counter] = new("@siparisNo", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisNo"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisSira"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        parameters[variables.Counter] = new("@takipno", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@takipno"];
                        variables.Counter++;
                    }

                    //if (login.GetDepartment().Contains("Doseme Paket"))
                    //    Variables.Query_ += " and stokKodu like 'PD%'";
                    //if (login.GetDepartment().Contains("Moduler Paket"))
                    //    Variables.Query_ += " and stokKodu like 'PM%'";
                    //if (login.GetDepartment().Contains("Moduler Plan") ||
                    //    login.GetDepartment().Contains("Moduler Depo"))
                    //    Variables.Query_ += " and TakipNo like 'M%'";
                    //if (login.GetDepartment().Contains("Doseme Plan") ||
                    //    login.GetDepartment().Contains("Doseme Depo") ||
                    //    login.GetDepartment().Contains("Konfeksiyon Depo"))
                    //    Variables.Query_ += " and TakipNo like 'D%'";

                    Variables.Query_ += " order by SiparisNumarasi desc";
                    Variables.Query_ += $" offset {(pageNumber - 1) * 100} rows fetch next 100 rows only ";

                    ObservableCollection<Cls_Isemri> tempColl = new();


                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
                    {

                        while (reader.Read())
                        {
                            Cls_Isemri isemri = new Cls_Isemri
                            {
                                TAKIP_NO = reader["TakipNo"].ToString(),
                                SIPARIS_NO = reader["siparisNumarasi"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["siparisSira"]),
                                CARI_KODU = reader["cariKod"].ToString(),
                                CARI_ADI = reader["CariIsim"].ToString(),
                                ISEMRINO = reader["isemrino"].ToString(),
                                STOK_KODU = reader["stokKodu"].ToString(),
                                STOK_ADI = reader["stokAdi"].ToString(),
                                IE_MIKTAR = Convert.ToInt32(reader["isemriMiktar"]),
                                TOPLAM_BILDIRILEN_MIKTAR = Convert.ToInt32(reader["ToplamUretilenMiktar"]),
                                KALAN_IE_MIKTAR = Convert.ToInt32(reader["KalanMiktar"]),
                                UretimDurum = reader["UretimDurum"].ToString(),
                                KullaniciAdi = reader["KayitYapan"].ToString(),
                                DEPO_KODU = Convert.ToInt32(reader["UretilenDepo"]),
                                BILDIRILEN_MIKTAR = Convert.ToInt32(reader["UretimMiktar"]),
                                BildirimTarih = Convert.ToDateTime(reader["UretimTarihi"]),
                            };
                            tempColl.Add(isemri);
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

        public int CountUretimDurumList(Dictionary<string, string> restrictionPairs,int pageNumber)
        {
            try
            {
                if (restrictionPairs == null)
                    return 0;
                
                    Variables.Query_ = $"select count(*) from vbvUretimDurumListele where 1=1 ";
                    variables.Counter = 0;

                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        Variables.Query_ += " and SiparisNumarasi like '%' + @siparisNo + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        Variables.Query_ += " and refisemrino like '%' + @refisemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        Variables.Query_ += " and UrunKodu like '%' + @urunKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        Variables.Query_ += " and urunAdi like  '%' + @urunAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        Variables.Query_ += " and isemrino like '%' + @isemrino + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokKodu"))
                    {
                        Variables.Query_ += " and StokKodu like '%' + @stokKodu + '%'";
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@stokAdi"))
                    {
                        Variables.Query_ += " and StokAdi like  '%' + @stokAdi + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        Variables.Query_ += " and SiparisSira = @siparisSira ";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        Variables.Query_ += " and TakipNo like  '%' + @takipno + '%'";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@baslangicTarih"))
                    {
                        Variables.Query_ += " and UretimTarihi >=@baslangicTarih";
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@bitisTarih"))
                    {
                        Variables.Query_ += " and UretimTarihi <=@bitisTarih";
                        variables.Counter++;
                    }

                    SqlParameter[] parameters = new SqlParameter[variables.Counter];

                    variables.Counter = 0;
                    if (restrictionPairs.ContainsKey("@siparisNo"))
                    {
                        parameters[variables.Counter] = new("@siparisNo", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisNo"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@refisemrino"))
                    {
                        parameters[variables.Counter] = new("@refisemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@refisemrino"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@urunKodu"))
                    {
                        parameters[variables.Counter] = new("@urunKodu", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@urunKodu"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@urunAdi"))
                    {
                        parameters[variables.Counter] = new("@urunAdi", SqlDbType.NVarChar, 435);
                        parameters[variables.Counter].Value = restrictionPairs["@urunAdi"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@isemrino"))
                    {
                        parameters[variables.Counter] = new("@isemrino", SqlDbType.NVarChar, 35);
                        parameters[variables.Counter].Value = restrictionPairs["@isemrino"];
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
                    if (restrictionPairs.ContainsKey("@siparisSira"))
                    {
                        parameters[variables.Counter] = new("@siparisSira", SqlDbType.Int);
                        parameters[variables.Counter].Value = restrictionPairs["@siparisSira"];
                        variables.Counter++;
                    }

                    if (restrictionPairs.ContainsKey("@takipno"))
                    {
                        parameters[variables.Counter] = new("@takipno", SqlDbType.NVarChar, 15);
                        parameters[variables.Counter].Value = restrictionPairs["@takipno"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@baslangicTarih"))
                    {
                        parameters[variables.Counter] = new("@baslangicTarih", SqlDbType.DateTime);
                        parameters[variables.Counter].Value = restrictionPairs["@baslangicTarih"];
                        variables.Counter++;
                    }
                    if (restrictionPairs.ContainsKey("@bitisTarih"))
                    {
                        parameters[variables.Counter] = new("@bitisTarih", SqlDbType.DateTime);
                        parameters[variables.Counter].Value = restrictionPairs["@bitisTarih"];
                        variables.Counter++;
                    }

                    if (login.GetDepartment().Contains("Doseme Paket"))
                        Variables.Query_ += " and stokKodu like 'PD%'";
                    if (login.GetDepartment().Contains("Moduler Paket"))
                        Variables.Query_ += " and stokKodu like 'PM%'";
                    if (login.GetDepartment().Contains("Moduler Plan"))
                        Variables.Query_ += " and TakipNo like 'M%'";
                    if (login.GetDepartment().Contains("Doseme Plan"))
                        Variables.Query_ += " and TakipNo like 'D%'";

                    int totalCount = 0;
                    using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika))
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
        public ObservableCollection<Cls_Isemri> PopulateGridViewWithIsemirleriCollection(string query, int yil)
        {
            try
            {
                dataTable = dataLayer.Select_Command(query, yil);

                coll_isemri.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Isemri cls_isemri = new Cls_Isemri
                    {
                        TAKIP_NO = row["TAKIP_NO"].ToString(),
                        URUN_KODU = row["URUN_KODU"].ToString(),
                        URUN_ADI = row["URUN_ADI"].ToString(),
                        STOK_KODU = row["STOK_KODU"].ToString(),
                        STOK_ADI = row["STOK_ADI"].ToString(),
                        DEPO_KODU = Convert.ToInt32(row["DEPO_KODU"]),
                        ISEMRINO = row["ISEMRINO"].ToString(),
                        IE_MIKTAR = Convert.ToInt32(row["IE_MIKTAR"]),
                        KALAN_IE_MIKTAR = Convert.ToInt32(row["KALAN_IE_MIKTAR"]),
                        HAM_KODU = row["HAM_KODU"].ToString(),
                        HAM_ADI = row["HAM_ADI"].ToString(),
                        BIRIM_HAM_MIKTAR = Convert.ToDecimal(row["BIRIM_HAM_MIKTAR"]),
                        ID = row["INCKEYNO"].ToString(),
                        BildirilecekIsemriMiktar = Convert.ToInt32(row["KALAN_IE_MIKTAR"]),
                        IsChecked = false
                    };

                    coll_isemri.Add(cls_isemri);
                }

                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public ObservableCollection<Cls_Isemri> PopulateGridViewWithBildirilenIsemirleriCollection(string query, int yil, Dictionary<string, string> kisitlar)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (kisitlar.ContainsKey("@hamAdi"))
                {
                    parameters.Add(new SqlParameter("@hamAdi", SqlDbType.NVarChar, 500) { Value = kisitlar["@hamAdi"] });
                }

                if (kisitlar.ContainsKey("@takipNo"))
                {
                    parameters.Add(new SqlParameter("@takipNo", SqlDbType.NVarChar, 15) { Value = kisitlar["@takipNo"] });
                }
                if (kisitlar.ContainsKey("@urunKodu"))
                {
                    parameters.Add(new SqlParameter("@urunKodu", SqlDbType.NVarChar, 35) { Value = kisitlar["@urunKodu"] });
                }

                // Convert the list to an array
                SqlParameter[] parameterArray = parameters.ToArray();


                reader = dataLayer.Select_Command_Data_Reader_With_Parameters(query, yil, parameterArray);

                coll_isemri.Clear();

                while (reader.Read())
                {
                    // Create an instance of the ViewModel and populate it from the DataRow
                    Cls_Isemri cls_isemri = new Cls_Isemri
                    {
                        ReceteID = Convert.ToInt32(reader["recID"]),
                        TAKIP_NO = reader["takip_no"].ToString(),
                        URUN_KODU = reader["urun_kodu"].ToString(),
                        URUN_ADI = reader["urun_adi"].ToString(),
                        STOK_KODU = reader["yari_mamul_kodu"].ToString(),
                        REFISEMRINO = reader["ref_ie"].ToString(),
                        ISEMRINO = reader["isemri_no"].ToString(),
                        HAM_KODU = reader["ham_kodu"].ToString(),
                        HAM_ADI = reader["ham_adi"].ToString(),
                        BIRIM_HAM_MIKTAR = Convert.ToDecimal(reader["birim_miktar"]),
                        TUKETILEN_MIKTAR = Convert.ToDecimal(reader["tuketilen_miktar"]),
                        BILDIRILEN_MIKTAR = Convert.ToDecimal(reader["bildirilenMiktar"]),
                        IsChecked = false
                    };

                    coll_isemri.Add(cls_isemri);
                }

                IsemirleriCollection = coll_isemri;
                return IsemirleriCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int InsertIsemri(ObservableCollection<Cls_Isemri> bildirimCollection, bool altBildir)
        { 
            try 
	        {
                Variables.Query_ = "vbpInsertUretimSonu";
                
                if(variables.Fabrika == "Ahşap")
                { 
                    foreach(Cls_Isemri isemri in  bildirimCollection)
                    { 
                        SqlParameter[] parameters = new SqlParameter[4];
                        parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@miktar", SqlDbType.Int);
                        parameters[2] = new SqlParameter("@KullaniciAdi", SqlDbType.NVarChar,12);
                        parameters[3] = new SqlParameter("@tarih", SqlDbType.DateTime);
                        parameters[0].Value = isemri.ISEMRINO;
                        parameters[1].Value = isemri.BildirilecekIsemriMiktar;
                        parameters[2].Value = login.GetUserName();
                        parameters[3].Value = isemri.BildirimTarih;

                        variables.Result = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);

                        if(variables.Result == false)
                            return -2;

                    }
                }


                if (variables.Fabrika == "Vita")
                {

                    foreach (Cls_Isemri isemri in bildirimCollection)
                    {
                        SqlParameter[] parameters = new SqlParameter[7];
                        parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                        parameters[2] = new SqlParameter("@depoKodu", SqlDbType.Int);
                        parameters[3] = new SqlParameter("@cikisDepoKodu", SqlDbType.Int);
                        parameters[4] = new SqlParameter("@miktar", SqlDbType.Int);
                        parameters[5] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                        parameters[6] = new SqlParameter("@tarih", SqlDbType.DateTime);
                        parameters[0].Value = isemri.ISEMRINO;
                        parameters[1].Value = isemri.STOK_KODU;
                        parameters[2].Value = isemri.DEPO_KODU;
                        parameters[3].Value = isemri.CIKIS_DEPO_KODU;
                        parameters[4].Value = isemri.KALAN_IE_MIKTAR;
                        parameters[5].Value = login.GetUserName();
                        parameters[6].Value = isemri.BildirimTarih;

                        variables.Result = dataLayer.ExecuteStoredProcedureWithParameters(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika);

                        if (variables.Result == false)
                            return -2;
                    }
                }

                return 1;

            }
	        catch 
	        {
                return -1;
	        }
        }
        public async Task<int> InsertIsemriAsync(ObservableCollection<Cls_Isemri> bildirimCollection, bool altBildir)
        { 
            try 
	        {
                Variables.Query_ = "vbpInsertUretimSonu";
                
                if(variables.Fabrika == "Ahşap")
                { 
                    foreach(Cls_Isemri isemri in  bildirimCollection)
                    { 
                        SqlParameter[] parameters = new SqlParameter[4];
                        parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[1] = new SqlParameter("@miktar", SqlDbType.Int);
                        parameters[2] = new SqlParameter("@KullaniciAdi", SqlDbType.NVarChar,12);
                        parameters[3] = new SqlParameter("@tarih", SqlDbType.DateTime);
                        parameters[0].Value = isemri.ISEMRINO;
                        parameters[1].Value = isemri.BildirilecekIsemriMiktar;
                        parameters[2].Value = login.GetUserName();
                        parameters[3].Value = isemri.BildirimTarih;

                        variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);

                        if(variables.Result == false)
                            return -2;

                    }
                }


                if (variables.Fabrika == "Vita")
                {

                        foreach (Cls_Isemri isemri in bildirimCollection)
                        {
                            SqlParameter[] parameters = new SqlParameter[7];
                            parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                            parameters[1] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                            parameters[2] = new SqlParameter("@depoKodu", SqlDbType.Int);
                            parameters[3] = new SqlParameter("@cikisDepoKodu", SqlDbType.Int);
                            parameters[4] = new SqlParameter("@miktar", SqlDbType.Int);
                            parameters[5] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                            parameters[6] = new SqlParameter("@tarih", SqlDbType.DateTime);
                            parameters[0].Value = isemri.ISEMRINO;
                            parameters[1].Value = isemri.STOK_KODU;
                            parameters[2].Value = isemri.DEPO_KODU;
                            parameters[3].Value = isemri.CIKIS_DEPO_KODU;
                            parameters[4].Value = isemri.KALAN_IE_MIKTAR;
                            parameters[5].Value = login.GetUserName();
                            parameters[6].Value = isemri.BildirimTarih;

                            variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika);

                            if (variables.Result == false)
                                return -2;
                        }
                    
                }

                return 1;

            }
	        catch 
	        {
            return -1;
	        }
        }
        public async Task<int> IsemriGeriAlAsync(ObservableCollection<Cls_Isemri> gerialCollection, bool isRefisemri_)
        { 
            try 
	        {
                Variables.Query_ = "vbpIsemriSil";
                string isemrino = string.Empty;

                    foreach(Cls_Isemri isemri in gerialCollection)
                    {
                        if (isRefisemri_)
                            isemrino = isemri.REFISEMRINO;
                        else
                            isemrino = isemri.ISEMRINO;

                        SqlParameter[] parameters = new SqlParameter[2];
                        parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[0].Value = isemrino;
                        parameters[1] = new SqlParameter("@isRefisemri_", SqlDbType.Bit);
                        parameters[1].Value = isRefisemri_;

                        variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);

                        if(variables.Result == false)
                            return -2;

                    }
                    
                return 1;
                
            }
	        catch 
	        {
            return -1;
	        }
        }
        public async Task<int> USKGeriAlRefAsync(ObservableCollection<Cls_Isemri> gerialCollection, bool altBildir)
        { 
            try 
	        {
                Variables.Query_ = "vbpUretimKaydiSilRef";
                
                    foreach(Cls_Isemri isemri in gerialCollection)
                    { 
                        SqlParameter[] parameters = new SqlParameter[1];
                        parameters[0] = new SqlParameter("@refisemrino", SqlDbType.NVarChar, 15);
                        parameters[0].Value = isemri.REFISEMRINO;

                        variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);

                        if(variables.Result == false)
                            return -2;

                    }
                    
                return 1;
                
            }
	        catch 
	        {
            return -1;
	        }
        }
        public async Task<int> USKGeriAlAsync(ObservableCollection<Cls_Isemri> gerialCollection, bool altBildir)
        { 
            try 
	        {
                Variables.Query_ = "vbpUretimKaydiSil";
                
                    foreach(Cls_Isemri isemri in gerialCollection)
                    { 
                        SqlParameter[] parameters = new SqlParameter[1];
                        parameters[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 15);
                        parameters[0].Value = isemri.ISEMRINO;

                        variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_,Variables.Yil_,parameters,variables.Fabrika);

                        if(variables.Result == false)
                            return -2;

                    }
                    
                return 1;
                
            }
	        catch 
	        {
            return -1;
	        }
        }
        public decimal IsemirleriToplamIhtiyacHesapla(string query, int yil)
        {
            try
            {
                dataTable = dataLayer.Select_Command(query, yil);
                KumulatifIhtiyac = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                   
                    try { Convert.ToInt32(row["KALAN_IE_MIKTAR"]); } catch { ConversionErrors conversionErrors = new ConversionErrors(); MessageBox.Show(conversionErrors.ConversionError("İşemri Miktar")); Mouse.OverrideCursor = null; };

                    try{ Convert.ToDecimal(row["BIRIM_HAM_MIKTAR"]); } catch { ConversionErrors conversionErrors = new ConversionErrors(); MessageBox.Show(conversionErrors.ConversionError("Ham Madde İhtiyaç Miktar")); Mouse.OverrideCursor = null; };

                    int ieMiktar = Convert.ToInt32(row["KALAN_IE_MIKTAR"]);
                    decimal birimHamMiktar = Convert.ToDecimal(row["BIRIM_HAM_MIKTAR"]);

                    KumulatifIhtiyac += (ieMiktar * birimHamMiktar);
                }

                return KumulatifIhtiyac;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public decimal IsemirleriBirimIhtiyacHesapla(int isemriMiktar, decimal birimHamMiktar)
        {
            BirimIhtiyac = 0;
            BirimIhtiyac = isemriMiktar * birimHamMiktar;
            return BirimIhtiyac;
        }

        public ObservableCollection<Cls_Isemri> GetUretimTakipKartiCollection(ObservableCollection<Cls_Isemri> referansIsemirleri)
        {
            try
            {

            ObservableCollection<Cls_Isemri> UretimTakipColl = new();
            coll_isemri.Clear();
            
            foreach (Cls_Isemri referansIsemri in referansIsemirleri) 
            {
                Variables.Query_ = "Select * from vbvBildirilecekIsemirleriListele where isemrino=@refisemrino";
                SqlParameter[] parameter = new SqlParameter[1]; 
                parameter[0] = new SqlParameter("@refisemrino", SqlDbType.NVarChar, 15);
                parameter[0].Value = referansIsemri.ISEMRINO;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, variables.Fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cls_Isemri isemri = new Cls_Isemri
                            {
                                SIPARIS_NO = reader["siparisNo"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["siparisSira"]),
                                URUN_KODU = reader["urunKodu"].ToString(),
                                URUN_ADI = reader["urunAdi"].ToString(),
                                CARI_KODU = reader["cariKod"].ToString(),
                                CARI_ADI = reader["cariAdi"].ToString(),
                                REFISEMRINO = reader["refisemrino"].ToString(),
                                SIPARIS_GENEL_ACIKLAMA = reader["genelAciklama"].ToString(),
                                ISEMRINO = reader["isemrino"].ToString(),
                                STOK_KODU = reader["stokKodu"].ToString(),
                                STOK_ADI = reader["stokAdi"].ToString(),
                                IE_MIKTAR = Convert.ToInt32(reader["isemriMiktar"]),
                                BILDIRILEN_MIKTAR = Convert.ToInt32(reader["bildirilenIeMiktar"]),
                                KALAN_IE_MIKTAR = Convert.ToInt32(reader["kalanIeMiktar"]),
                                BildirilecekIsemriMiktar = Convert.ToInt32(reader["kalanIeMiktar"]),
                            };
                            coll_isemri.Add(isemri);
                        }
                        reader.Close();
                    }
                }
            }
                UretimTakipColl = coll_isemri;
                return UretimTakipColl;
            }
            catch 
            {
                return null;
            }

        }

        public ObservableCollection<Cls_Isemri> GetHamMaddeCollection(Cls_Isemri isemri)
        {
            try
            {

                ObservableCollection<Cls_Isemri> HamMaddeColl = new();
                coll_isemri.Clear();

                Variables.Query_ = "Select * from vbvGetHamKoduFromIsemri where isemrino=@isemrino";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@isemrino", SqlDbType.NVarChar, 35);
                parameter[0].Value = isemri.ISEMRINO;

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, variables.Fabrika))
                {
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cls_Isemri hammadde = new Cls_Isemri
                            {
                                SIPARIS_NO = reader["siparisNo"].ToString(),
                                SIPARIS_SIRA = Convert.ToInt32(reader["siparisSira"]),
                                URUN_KODU = reader["urunKodu"].ToString(),
                                HAM_KODU = reader["hamKodu"].ToString(),
                                HAM_ADI = reader["hamAdi"].ToString(),
                                RECETE_MIKTAR = Convert.ToDecimal(reader["miktar"]),

                            };
                            coll_isemri.Add(hammadde);
                        }
                        reader.Close();
                    }
                }

                HamMaddeColl = coll_isemri;
                return HamMaddeColl;
            }
            catch
            {
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

                using (SqlDataReader reader = dataLayer.Select_Command_Data_Reader_With_Parameters(Variables.Query_, Variables.Yil_, parameter, variables.Fabrika))
                {
                    if (reader == null || reader.FieldCount <= 0)
                        return result;

                    while (reader.Read())
                    {
                        opno = Convert.ToInt32(reader[0]);
                    }
                }

                if (opno == 0)
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

        public async Task<bool> CheckIfOpnoExists(string opno, string mamulKodu)
        {
            if (string.IsNullOrEmpty(opno))
                return false;

            Variables.Query_ = "Select count(*) from tblstokurm (nolock) where opno = @opno and mamul_kodu=@mamulKodu";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@opno", SqlDbType.NVarChar, 4);
            parameter[0].Value = opno;
            parameter[1] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
            parameter[1].Value = mamulKodu;

            Variables.ResultInt_ = await dataLayer.Get_One_Int_Result_Command_With_Parameters_Async(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);

            if (Variables.ResultInt_ == 0)
                return true; return false;

        }
        public decimal KatSayiHesapla(decimal metre, decimal kumulatifIhtiyac)
        {
            KatSayi = 0;

            if (kumulatifIhtiyac == 0) { MessageBox.Show("Toplam İhtiyaç 0 olamaz"); return 0; }

            KatSayi = metre/kumulatifIhtiyac;

            return KatSayi;
        }

        public async Task<int> UpdateUrunAgaciAsync(ObservableCollection<Cls_Isemri> guncellemeCollection, string guncellemeTipi)
        {
            try
            {
                Variables.Query_ = "vbpUpdateIsemriRecetesi";

                foreach (Cls_Isemri ie in guncellemeCollection)
                {
                    SqlParameter[] parameters = new SqlParameter[15];
                    parameters[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
                    parameters[1] = new SqlParameter("@eskiHamKodu", SqlDbType.NVarChar, 35);
                    parameters[2] = new SqlParameter("@yeniHamKodu", SqlDbType.NVarChar, 35);
                    parameters[3] = new SqlParameter("@eskiMiktar", SqlDbType.Decimal);
                    parameters[4] = new SqlParameter("@miktar", SqlDbType.Decimal);
                    parameters[5] = new SqlParameter("@guncellemeTipi", SqlDbType.NVarChar, 100);
                    parameters[6] = new SqlParameter("@user", SqlDbType.NVarChar, 12);
                    parameters[7] = new SqlParameter("@id", SqlDbType.Int);
                    parameters[8] = new SqlParameter("@opno", SqlDbType.Int);
                    parameters[9] = new SqlParameter("@mailDurumGuncelle", SqlDbType.Bit);
                    parameters[10] = new SqlParameter("@isemrino", SqlDbType.NVarChar,15);
                    parameters[11] = new SqlParameter("@SiparisNumarasi", SqlDbType.NVarChar,15);
                    parameters[12] = new SqlParameter("@SiparisSira", SqlDbType.Int);
                    parameters[13] = new SqlParameter("@TakipNo", SqlDbType.NVarChar,15);
                    parameters[14] = new SqlParameter("@Refisemrino", SqlDbType.NVarChar,15);
                    parameters[0].Value = ie.MAMUL_KODU;
                    parameters[1].Value = ie.EskiHamKodu;
                    parameters[2].Value = ie.HAM_KODU;
                    parameters[3].Value = ie.EskiReceteMiktar;
                    parameters[4].Value = ie.RECETE_MIKTAR;
                    parameters[5].Value = guncellemeTipi;
                    parameters[6].Value = login.GetUserName();
                    parameters[7].Value = ie.ReceteID;
                    parameters[8].Value = ie.OPNO;
                    parameters[9].Value = false;
                    parameters[10].Value = ie.ISEMRINO;
                    parameters[11].Value = ie.SIPARIS_NO;
                    parameters[12].Value = ie.SIPARIS_SIRA;
                    parameters[13].Value = ie.TAKIP_NO;
                    parameters[14].Value = ie.REFISEMRINO;

                    variables.Result = await dataLayer.ExecuteStoredProcedureWithParametersAsync(Variables.Query_, Variables.Yil_, parameters, variables.Fabrika);

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
    }
}


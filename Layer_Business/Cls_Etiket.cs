using Layer_2_Common.Type;
using Layer_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Business
{
    public class Cls_Etiket : INotifyPropertyChanged
    {
        public string StokKodu { get; set; }
        public string CariKodu { get; set; }
        public string CariStokKodu { get; set; }
        public string CariStokIsim { get; set; }
        public string UserName { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string SetCode { get; set; }
        public string Model { get; set; }
        public float Stack { get; set; }
        public float BrutAgirlik { get; set; }
        public string Renk { get; set; }
        public string EANcode { get; set; }
        public int KoliMiktar { get; set; }
        public string PaketKodu { get; set; }
        public string InsertStatus { get; set; } = "Beklemede...";

        

        private string dimensions;

        public string Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }



        // Override the setter for Dimensions property to automatically generate the dimensions string
        private float _en = 0;
        public float En
        {
            get { return _en; }
            set
            {
                _en = value;
                UpdateDimensions();
            }
        }
        private float _boy = 0;
        public float Boy
        {
            get { return _boy; }
            set
            {
                _boy = value;
                UpdateDimensions();
            }
        }
        private float _yukseklik = 0;
        public float Yukseklik
        {
            get { return _yukseklik; }
            set
            {
                _yukseklik = value;
                UpdateDimensions();
            }
        }

        public int POnumarasi { get; set; } = 0;

        private bool _isChecked = false;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private void UpdateDimensions()
        {
            Dimensions = $"{En} * {Boy} * {Yukseklik}";
        }

        LoginLogic login = new();
        DataLayer data = new();

        public Cls_Etiket()
        { 
            Variables.Fabrika_ = login.GetFabrika();
            Variables.UserName = login.GetUserName();
        }
        public string InsertEslenikKaydet(Cls_Etiket etiket,string cariKodu)
        {
            try
            {
                string errorMessage = string.Empty;
                if(etiket == null ) return "Stok Bilgileri Bulunamadı...";

                Variables.Query_ = "select count(*) from vbtCariStok where StokKodu=@StokKodu and CariKodu=@CariKodu and CariStokKodu = @CariStokKodu";

                SqlParameter[] queryParameters = new SqlParameter[3];

                queryParameters[0] = new SqlParameter("@StokKodu", SqlDbType.NVarChar, 35) { Value = etiket.StokKodu };
                queryParameters[1] = new SqlParameter("@CariKodu", SqlDbType.NVarChar, 35) { Value = cariKodu };
                queryParameters[2] = new SqlParameter("@CariStokKodu", SqlDbType.NVarChar, 35) { Value = etiket.CariStokKodu };

                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, queryParameters, Variables.Fabrika_);
                if (Variables.ResultInt_ != 0)
                    return "Kayıt Mevcut...";

                SqlParameter[] parameters = new SqlParameter[13];

                    parameters[0] = new SqlParameter("@StokKodu", SqlDbType.NVarChar, 35) { Value = etiket.StokKodu };
                    parameters[1] = new SqlParameter("@CariKodu", SqlDbType.NVarChar, 35) { Value = cariKodu };
                    parameters[2] = new SqlParameter("@CariStokKodu", SqlDbType.NVarChar, 35) { Value = etiket.CariStokKodu };
                    parameters[3] = new SqlParameter("@CariStokIsim", SqlDbType.NVarChar, 400) { Value = etiket.CariStokIsim };
                    parameters[4] = new SqlParameter("@UserName", SqlDbType.NVarChar, 15) { Value = Variables.UserName };
                    parameters[5] = new SqlParameter("@SetCode", SqlDbType.NVarChar, 100) { Value = etiket.SetCode };
                    parameters[6] = new SqlParameter("@Model", SqlDbType.NVarChar, 100) { Value = etiket.Model };
                    parameters[7] = new SqlParameter("@Stack", SqlDbType.Float) { Value = etiket.Stack };
                    parameters[8] = new SqlParameter("@BrutAgirlik", SqlDbType.Float) { Value = etiket.BrutAgirlik };
                    parameters[9] = new SqlParameter("@Renk", SqlDbType.NVarChar, 100) { Value = etiket.Renk };
                    parameters[10] = new SqlParameter("@EANcode", SqlDbType.VarChar, 13) { Value = etiket.EANcode };
                    parameters[11] = new SqlParameter("@KoliMiktar", SqlDbType.Int) { Value = etiket.KoliMiktar };
                    parameters[12] = new SqlParameter("@PaketKodu", SqlDbType.NVarChar, 10) { Value = etiket.PaketKodu };

                    Variables.Result_ = data.ExecuteStoredProcedureWithParameters("vbpInsertCariStokKodu",Variables.Yil_, parameters,Variables.Fabrika_);
                    if (!Variables.Result_)
                        return "Kaydedilemedi...";
                
                    return "Kaydedildi...";
            }

            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public ObservableCollection<Cls_Etiket> GetEtiketInfo(string stokKodu,bool isCariStokKodu)
        {
            try
            {
                if (string.IsNullOrEmpty(stokKodu))
                    return null;

                if(isCariStokKodu)
                Variables.Query_ = "select a.StokKodu,a.CariKodu,a.CariStokKodu,a.CariStokIsim,a.UserName,a.KayitTarihi,a.SetCode,a.Model,a.Stack,isnull(a.BrutAgirlik,0) as BrutAgirlik,a.Renk,a.EANcode,isnull(a.KoliMiktar,0) as KoliMiktar,a.PaketKodu,isnull(b.en,0) as en,isnull(b.boy,0) as boy, isnull(b.genislik,0) as yukseklik from vbtCariStok a " + 
                                    " left join tblstsabit (nolock) b on a.StokKodu=b.stok_kodu where CariStokKodu=@stokKodu";
                else
                    Variables.Query_ = "select a.StokKodu,a.CariKodu,a.CariStokKodu,a.CariStokIsim,a.UserName,a.KayitTarihi,a.SetCode,a.Model,a.Stack,isnull(a.BrutAgirlik,0) as BrutAgirlik,a.Renk,a.EANcode,isnull(a.KoliMiktar,0) as KoliMiktar,a.PaketKodu,isnull(b.en,0) as en,isnull(b.boy,0) as boy, isnull(b.genislik,0) as yukseklik from vbtCariStok a " +
                                        " left join tblstsabit (nolock) b on a.StokKodu=b.stok_kodu where StokKodu=@stokKodu";

                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@stokKodu", SqlDbType.NVarChar, 35);
                parameter[0].Value = stokKodu;

                ObservableCollection<Cls_Etiket> etiketColl = data.Select_Command_Data_With_Parameters<Cls_Etiket>(Variables.Query_, Variables.Yil_, Variables.Fabrika_,parameter, reader =>
                {
                    Cls_Etiket model = new Cls_Etiket();
                    model.StokKodu = reader["StokKodu"] is DBNull ? "" : reader["StokKodu"].ToString();
                    model.CariKodu = reader["CariKodu"] is DBNull ? "" : reader["CariKodu"].ToString();
                    model.CariStokKodu = reader["CariStokKodu"] is DBNull ? "" : reader["CariStokKodu"].ToString();
                    model.CariStokIsim = reader["CariStokIsim"] is DBNull ? "" : reader["CariStokIsim"].ToString();
                    model.UserName = reader["UserName"] is DBNull ? "" : reader["UserName"].ToString();
                    model.KayitTarihi = reader["KayitTarihi"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["KayitTarihi"]);
                    model.SetCode = reader["SetCode"] is DBNull ? "" : reader["SetCode"].ToString();
                    model.Model = reader["Model"] is DBNull ? "" : reader["Model"].ToString();
                    model.Stack = reader["Stack"] is DBNull ? 0 : Convert.ToInt32(reader["Stack"]);
                    model.BrutAgirlik = reader["BrutAgirlik"] is DBNull ? 0 : Convert.ToSingle(reader["BrutAgirlik"]);
                    model.Renk = reader["Renk"] is DBNull ? "" : reader["Renk"].ToString();
                    model.EANcode = reader["EANcode"] is DBNull ? "" : reader["EANcode"].ToString();
                    model.KoliMiktar = reader["KoliMiktar"] is DBNull ? 0 : Convert.ToInt32(reader["KoliMiktar"]);
                    model.PaketKodu = reader["PaketKodu"] is DBNull ? "" : reader["PaketKodu"].ToString();
                    model.En = reader["en"] is DBNull ? 0 : Convert.ToSingle(reader["en"]);
                    model.Boy = reader["boy"] is DBNull ? 0 : Convert.ToSingle(reader["boy"]);
                    model.Yukseklik = reader["yukseklik"] is DBNull ? 0 : Convert.ToSingle(reader["yukseklik"]);
                    return model;
                });

                return etiketColl;

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

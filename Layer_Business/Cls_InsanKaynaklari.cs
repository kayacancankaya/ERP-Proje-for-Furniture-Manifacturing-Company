using Layer_2_Common.Type;
using Layer_Business.ViewModels;
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
    public class Cls_InsanKaynaklari : INotifyPropertyChanged
    {
        public int Id { get; set; } 
        public string Adi { get; set; } = string.Empty;
        public DateTime DogumGunuTarih { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = false;
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
        Variables variables = new();
        LoginLogic login = new();
        DataLayer data = new();
        public Cls_InsanKaynaklari()
        {
            Variables.Fabrika_ = variables.Fabrika;
            Variables.UserName = login.GetUserName();
        }
        public ObservableCollection<Cls_InsanKaynaklari> PopulateDogumGunuList(Dictionary<string,string> filterDic,int pageNumber)
        {
            try
            {


                Variables.Query_ = "select * from vbtDogumGunu where ActiveUser = 1 ";
                
                if (filterDic == null)
                    return null;
                Variables.Counter_ = 0;
                SqlParameter[] param = new SqlParameter[filterDic.Count];
                if (filterDic.ContainsKey("Adı"))
                {
                    param[Variables.Counter_] = new SqlParameter("@adi", SqlDbType.NVarChar, 100);
                    param[Variables.Counter_].Value = filterDic["Adı"];
                    Variables.Query_ += " and AdiSoyadi like '%' + @adi + '%'";
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Doğum Günü"))
                {
                    param[Variables.Counter_] = new SqlParameter("@tarih", SqlDbType.Int);
                    param[Variables.Counter_].Value = Convert.ToInt32(filterDic["Doğum Günü"]);
                    Variables.Query_ += " and cast(MONTH(DogumGunu) as int) = @tarih";
                    Variables.Counter_++;
                }
                Variables.Query_ += " order by AdiSoyadi ";
                Variables.Query_ += $" offset {(pageNumber - 1) * 30} rows fetch next 30 rows only ";
                ObservableCollection<Cls_InsanKaynaklari> ikColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, param, reader =>
                {
                    Cls_InsanKaynaklari model = new();
                    model.Adi = reader["AdiSoyadi"] is DBNull ? "" : reader["AdiSoyadi"].ToString();
                    model.DogumGunuTarih = reader["DogumGunu"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["DogumGunu"]);
                    model.Id = reader["id"] is DBNull ? 0 : Convert.ToInt32(reader["id"]);
                    return model;
                });
                return ikColl;
            }
            catch 
            {
                return null;
            }
        }
        public int CountDogumGunuList(Dictionary<string,string> filterDic)
        {
            try
            {

                Variables.Query_ = "select count(*) from vbtDogumGunu where ActiveUser = 1 ";

                if (filterDic == null)
                    return -1;

                Variables.Counter_ = 0;
                SqlParameter[] param = new SqlParameter[filterDic.Count];
                if (filterDic.ContainsKey("Adı"))
                {
                    param[Variables.Counter_] = new SqlParameter("@adi", SqlDbType.NVarChar, 100);
                    param[Variables.Counter_].Value = filterDic["Adı"];
                    Variables.Query_ += " and AdiSoyadi like '%' + @adi + '%'";
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Tarih"))
                {
                    param[Variables.Counter_] = new SqlParameter("@tarih", SqlDbType.DateTime);
                    param[Variables.Counter_].Value = Convert.ToInt32(filterDic["Tarih"]);
                    Variables.Query_ += " and MONTH(DogumGunu) = Tarih";
                    Variables.Counter_++;
                }

                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, param, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch 
            {
                return -1;
            }
        }
        public bool InsertDogumGunu(string adi,DateTime tarih)
        {
            try
            {
                if(string.IsNullOrEmpty(adi)) return false;
                DateTime control = DateTime.MinValue;
                if(tarih == DateTime.MinValue) return false;
                if(tarih == null) return false;
                if(!DateTime.TryParse(tarih.ToString(), out control)) return false; 

                Variables.Query_ = "insert into vbtDogumGunu (AdiSoyadi,DogumGunu,ActiveUser) Values (@adi,@tarih,1)";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@adi",SqlDbType.NVarChar, 400);
                parameters[1] = new SqlParameter("@tarih", SqlDbType.DateTime);
                parameters[0].Value = adi;
                parameters[1].Value = tarih;
                Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_,Variables.Yil_, parameters);
                return Variables.Result_;
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateDogumGunu(ObservableCollection<Cls_InsanKaynaklari> updateColl)
        {
            try
            {
               if(updateColl == null) return false;
               if(updateColl.Count == 0) return false;

                foreach (Cls_InsanKaynaklari item in updateColl)
                {
                    Variables.Query_ = "update vbtDogumGunu set AdiSoyadi = @adi ,DogumGunu = @tarih where id=@id";
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@adi", SqlDbType.NVarChar, 400);
                    parameters[1] = new SqlParameter("@tarih", SqlDbType.DateTime);
                    parameters[2] = new SqlParameter("@id", SqlDbType.Int);
                    parameters[0].Value = item.Adi;
                    parameters[1].Value = item.DogumGunuTarih;
                    parameters[2].Value = item.Id;
                    Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameters);
                    if(!Variables.Result_) return false;

                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool DeleteDogumGunu(ObservableCollection<Cls_InsanKaynaklari> deleteColl)
        {
            try
            {
               if(deleteColl == null) return false;
               if(deleteColl.Count == 0) return false;

                foreach (Cls_InsanKaynaklari item in deleteColl)
                {
                    Variables.Query_ = "delete from vbtDogumGunu where id=@id";
                    SqlParameter[] parameter = new SqlParameter[1];
                    parameter[0] = new SqlParameter("@id", SqlDbType.Int);
                    parameter[0].Value = item.Id;
                    Variables.Result_ = data.ExecuteCommandWithParameters(Variables.Query_, Variables.Yil_, parameter);
                    if(!Variables.Result_) return false;

                }
                return true;
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

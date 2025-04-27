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

namespace Layer_Business.ViewModels
{
    public class UrunAgacViewModel : INotifyPropertyChanged
    {
        public string UrunKodu { get; set; } = string.Empty;
        public string UrunAdi { get; set; } = string.Empty;
        public string MamulKodu { get; set; } = string.Empty;
        public string MamulAdi { get; set; } = string.Empty;
        public string HamKodu { get; set; } = string.Empty;
        public string HamAdi { get; set; } = string.Empty;
        public float Miktar { get; set; } = 0;
        public string Opbil { get; set; } = string.Empty;
        public string Opno { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public int Seviye { get; set; } = 0;

        public ObservableCollection<UrunAgacViewModel> UrunAgac { get; set; } = new();
        DataLayer data = new DataLayer();
        public ObservableCollection<UrunAgacViewModel> GetTop30UrunAgac()
        {
            try
            {
                ObservableCollection<UrunAgacViewModel> top30AgacColl = new();

                Variables.Query_ = @"with cte as (select row_number() over(partition by mamul_kodu 
				                                    order by kayittarihi desc) as line, MAMUL_KODU,KAYITTARIHI 
				                                    from tblstokurm (nolock) where mamul_kodu like 'm%')
                                    select top 30 mamul_kodu,sabit.stok_adi from cte 
                                    left join tblstsabit sabit (nolock) on cte.mamul_kodu=sabit.stok_kodu 
                                    where line = 1 
                                    order by KAYITTARIHI desc";

                ObservableCollection<UrunAgacViewModel> mamulKoduColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    UrunAgacViewModel model = new();
                    model.MamulKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString();
                    model.MamulAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString();
                    model.HamKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString(); ;
                    model.HamAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString(); ;
                    model.Aciklama = "-";
                    model.Opno = "-";
                    model.Opbil = "-";
                    return model;
                });

                foreach (UrunAgacViewModel mamul in mamulKoduColl)
                {

                    var urunAgaciList = GetUrunAgacFullList(mamul.MamulKodu);

                    PopulateUrunAgacTree(mamul, urunAgaciList, mamul.MamulKodu);
                    if (mamul != null)
                        top30AgacColl.Add(mamul);
                }

                return top30AgacColl;

            }
            catch
            {
                return null;
            }
        }
        public UrunAgacViewModel GetUrunAgac(string urunKodu)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(urunKodu))
                    return null;
                var urunAgaciList = GetUrunAgacFullList(urunKodu);

                // Create a root node 
                var root = new UrunAgacViewModel
                {
                    UrunKodu = "Deneme",
                    UrunAgac = new ObservableCollection<UrunAgacViewModel>()
                };

                // A recursive method to map hierarchical data to UrunAgacViewModel
                PopulateUrunAgacTree(root, urunAgaciList, urunKodu);
                return root;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<UrunAgacViewModel> GetUrunAgac(Dictionary<string, string> filterDic, int pageNumber)
        {
            try
            {

                if (filterDic == null)
                    return null;
                if (filterDic.Count == 0)
                    return null;
                ObservableCollection<UrunAgacViewModel> filteredAgacColl = new();
                Variables.Query_ = @"with cte as (select row_number() over(partition by mamul_kodu 
				                                    order by kayittarihi desc) as line, MAMUL_KODU,KAYITTARIHI 
				                                    from tblstokurm (nolock) where mamul_kodu like 'm%')
                                    select mamul_kodu,sabit.stok_adi from cte 
                                    left join tblstsabit sabit (nolock) on cte.mamul_kodu=sabit.stok_kodu 
                                    where line = 1 ";

                SqlParameter[] parameters = new SqlParameter[filterDic.Count];
                Variables.Counter_ = 0;
                if (filterDic.ContainsKey("Mamul Kodu"))
                {
                    Variables.Query_ += " and mamul_kodu like + '%' + @mamul_kodu + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@mamul_kodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = filterDic["Mamul Kodu"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Mamul Adı"))
                {
                    Variables.Query_ += " and stok_adi like + '%' + @stok_adi + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@stok_adi", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = filterDic["Mamul Adı"];
                    Variables.Counter_++;
                }
                //if (filterDic.ContainsKey("Ham Kodu"))
                //{
                //    Variables.Query_ += " and HamKodu like + '%' + @hamKodu + '%' ";
                //    parameters[Variables.Counter_] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                //    parameters[Variables.Counter_].Value = filterDic["Ham Kodu"];
                //    Variables.Counter_++;
                //}
                //if (filterDic.ContainsKey("Ham Adı"))
                //{
                //    Variables.Query_ += " and HamAdi like + '%' + @hamAdi + '%' ";
                //    parameters[Variables.Counter_] = new SqlParameter("@hamAdi", SqlDbType.NVarChar, 100);
                //    parameters[Variables.Counter_].Value = filterDic["Ham Adı"];
                //    Variables.Counter_++;
                //}

                Variables.Query_ += " order by kayittarihi desc";
                Variables.Query_ += $" offset {(pageNumber - 1) * 30} rows fetch next 30 rows only ";

                ObservableCollection<UrunAgacViewModel> mamulKoduColl = data.Select_Command_Data_With_Parameters(Variables.Query_, Variables.Yil_, Variables.Fabrika_, parameters, reader =>
                {
                    UrunAgacViewModel model = new();
                    model.MamulKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString();
                    model.MamulAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString();
                    model.HamKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString(); ;
                    model.HamAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString(); ;
                    model.Aciklama = "-";
                    model.Opno = "-";
                    model.Opbil = "-";
                    return model;
                });

                foreach (UrunAgacViewModel mamul in mamulKoduColl)
                {

                    var urunAgaciList = GetUrunAgacFullList(mamul.MamulKodu);

                    PopulateUrunAgacTree(mamul, urunAgaciList, mamul.MamulKodu);
                    if (mamul != null)
                        filteredAgacColl.Add(mamul);
                }

                return filteredAgacColl;

            }
            catch
            {
                return null;
            }
        }
        public ObservableCollection<UrunAgacViewModel> GetUrunAgac(int pageNumber)
        {
            try
            {
                ObservableCollection<UrunAgacViewModel> filteredAgacColl = new();
                Variables.Query_ = @"with cte as (select row_number() over(partition by mamul_kodu 
				                                    order by kayittarihi desc) as line, MAMUL_KODU,KAYITTARIHI 
				                                    from tblstokurm (nolock) where mamul_kodu like 'm%')
                                    select mamul_kodu,sabit.stok_adi from cte 
                                    left join tblstsabit sabit (nolock) on cte.mamul_kodu=sabit.stok_kodu 
                                    where line = 1 ";

                Variables.Query_ += " order by kayittarihi desc";
                Variables.Query_ += $" offset {(pageNumber - 1) * 30} rows fetch next 30 rows only ";

                ObservableCollection<UrunAgacViewModel> mamulKoduColl = data.Select_Command_Data(Variables.Query_, Variables.Yil_, Variables.Fabrika_, reader =>
                {
                    UrunAgacViewModel model = new();
                    model.MamulKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString();
                    model.MamulAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString();
                    model.HamKodu = reader["mamul_kodu"] is DBNull ? "" : reader["mamul_kodu"].ToString();
                    model.HamAdi = reader["stok_adi"] is DBNull ? "" : reader["stok_adi"].ToString();
                    model.Aciklama = "-";
                    model.Opno = "-";
                    model.Opbil = "-";
                    return model;
                });

                foreach (UrunAgacViewModel mamul in mamulKoduColl)
                {

                    var urunAgaciList = GetUrunAgacFullList(mamul.MamulKodu);

                    PopulateUrunAgacTree(mamul, urunAgaciList, mamul.MamulKodu);
                    if (mamul != null)
                        filteredAgacColl.Add(mamul);
                }

                return filteredAgacColl;

            }
            catch
            {
                return null;
            }
        }
        private ObservableCollection<UrunAgacViewModel> GetUrunAgacFullList(string urunKodu)
        {

            Variables.Query_ = "vbpUrunAgaci";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@urunKodu", SqlDbType.NVarChar, 35);
            parameter[0].Value = urunKodu;

            ObservableCollection<UrunAgacViewModel> agacColl = data.ExecuteStoredProcedureWithParametersReturnsCollection(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_, reader =>
            {
                UrunAgacViewModel model = new();
                model.UrunKodu = reader["urunKodu"] is DBNull ? "" : reader["urunKodu"].ToString();
                model.UrunAdi = reader["urunAdi"] is DBNull ? "" : reader["urunAdi"].ToString();
                model.MamulKodu = reader["MAMUL_KODU"] is DBNull ? "" : reader["MAMUL_KODU"].ToString();
                model.MamulAdi = reader["Mamul_adi"] is DBNull ? "" : reader["Mamul_adi"].ToString();
                model.HamKodu = reader["HAM_KODU"] is DBNull ? "" : reader["HAM_KODU"].ToString();
                model.HamAdi = reader["Ham_Adi"] is DBNull ? "" : reader["Ham_Adi"].ToString();
                model.Opno = reader["Opno"] is DBNull ? "" : reader["Opno"].ToString();
                model.Opbil = reader["Opbil"] is DBNull ? "" : reader["Opbil"].ToString();
                model.Aciklama = reader["Aciklama"] is DBNull ? "" : reader["Aciklama"].ToString();
                model.Seviye = reader["Seviye"] is DBNull ? 0 : Convert.ToInt32(reader["Seviye"]);
                model.Miktar = reader["Miktar"] is DBNull ? 0 : Convert.ToSingle(reader["Miktar"]);
                return model;
            });


            return agacColl;
        }
        private void PopulateUrunAgacTree(UrunAgacViewModel parent, ObservableCollection<UrunAgacViewModel> urunAgaciList, string mamulKodu)
        {
            var children = urunAgaciList.Where(x => x.MamulKodu == mamulKodu).ToList();
            if (children != null)
            {
                if (children.Count > 0)
                {
                    foreach (var child in children)
                    {
                        parent.UrunAgac.Add(child);
                        PopulateUrunAgacTree(child, urunAgaciList, child.HamKodu);
                    }
                }
            }
        }
        public int CountAgac(Dictionary<string, string> filterDic)
        {
            try
            {
                if (filterDic == null)
                    return -1;
                if (filterDic.Count == 0)
                    return -1;
                ObservableCollection<UrunAgacViewModel> filteredAgacColl = new();
                Variables.Query_ = @"with cte as (select row_number() over(partition by mamul_kodu 
				                                    order by kayittarihi desc) as line, MAMUL_KODU,KAYITTARIHI 
				                                    from tblstokurm (nolock) where mamul_kodu like 'm%')
                                    select count(*) from cte 
                                    left join tblstsabit sabit (nolock) on cte.mamul_kodu=sabit.stok_kodu 
                                    where line = 1 ";

                SqlParameter[] parameters = new SqlParameter[filterDic.Count];
                Variables.Counter_ = 0;
                if (filterDic.ContainsKey("Mamul Kodu"))
                {
                    Variables.Query_ += " and mamul_kodu like + '%' + @mamul_kodu + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@mamul_kodu", SqlDbType.NVarChar, 35);
                    parameters[Variables.Counter_].Value = filterDic["Mamul Kodu"];
                    Variables.Counter_++;
                }
                if (filterDic.ContainsKey("Mamul Adı"))
                {
                    Variables.Query_ += " and stok_adi like + '%' + @stok_adi + '%' ";
                    parameters[Variables.Counter_] = new SqlParameter("@stok_adi", SqlDbType.NVarChar, 100);
                    parameters[Variables.Counter_].Value = filterDic["Mamul Adı"].ToString();
                    Variables.Counter_++;
                }
                //if (filterDic.ContainsKey("Ham Kodu"))
                //{
                //    Variables.Query_ += " and HamKodu like + '%' + @hamKodu + '%' ";
                //    parameters[Variables.Counter_] = new SqlParameter("@hamKodu", SqlDbType.NVarChar, 35);
                //    parameters[Variables.Counter_].Value = filterDic["Ham Kodu"];
                //    Variables.Counter_++;
                //}
                //if (filterDic.ContainsKey("Ham Adı"))
                //{
                //    Variables.Query_ += " and HamAdi like + '%' + @hamAdi + '%' ";
                //    parameters[Variables.Counter_] = new SqlParameter("@hamAdi", SqlDbType.NVarChar, 100);
                //    parameters[Variables.Counter_].Value = filterDic["Ham Adı"];
                //    Variables.Counter_++;
                //}


                Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameters, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        public int CountAgac()
        {
            try
            {
                Variables.Query_ = @"with cte as (select row_number() over(partition by mamul_kodu 
				                                    order by kayittarihi desc) as line, MAMUL_KODU,KAYITTARIHI 
				                                    from tblstokurm (nolock) where mamul_kodu like 'm%')
                                    select count(*) from cte 
                                    left join tblstsabit sabit (nolock) on cte.mamul_kodu=sabit.stok_kodu 
                                    where line = 1 ";



                Variables.ResultInt_ = data.Get_One_Int_Result_Command(Variables.Query_, Variables.Yil_, Variables.Fabrika_);
                return Variables.ResultInt_;
            }
            catch
            {
                return -1;
            }
        }
        private bool CheckIfYariMamul(UrunAgacViewModel child)
        {
            if (child == null)
                return false;
            if (string.IsNullOrEmpty(child.MamulKodu)) return false;

            Variables.Query_ = "Select count(*) from tblstokurm (nolock) where mamul_kodu = @mamulKodu";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@mamulKodu", SqlDbType.NVarChar, 35);
            parameter[0].Value = child.MamulKodu;
            Variables.ResultInt_ = data.Get_One_Int_Result_Command_With_Parameters(Variables.Query_, Variables.Yil_, parameter, Variables.Fabrika_);
            if (Variables.ResultInt_ > 0)
                return true;
            else
                return false;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

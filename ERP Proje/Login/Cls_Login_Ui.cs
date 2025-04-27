using Layer_2_Common.Type;
using Layer_Business;
using Layer_Data;
using Layer_UI.Arge.Yardimci;
using Layer_UI.Bilgi_Islem;
using Layer_UI.Depo.Stok_Hareket;
using Layer_UI.Finans;
using Layer_UI.InsanKaynaklari;
using Layer_UI.Konfeksiyon;
using Layer_UI.Satın_Alma;
using Layer_UI.Satis;
using Layer_UI.Satis.Sevk;
using Layer_UI.Satis.Siparis;
using Layer_UI.Uretim;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Layer_UI.Login
{
    public class Cls_Login_Ui
    {
        LoginLogic login = new();
        Variables variables = new();
        DataLayer dataLayer = new();
        SqlDataReader? reader;
        string departman = string.Empty;

        public Window? IfStatusAutoLoginSuccedGetDepartment()
        {
            try
            {
                login.Address = login.CallGetMotherboardSerialNumber();

                string hashedAddr = login.CallHashPassword(login.Address);
                Variables.Query_ = $"select top 1 Departman from vbtUserInfo where adres='{hashedAddr}'";

                reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.CurrentYear_);

                if (reader.Read())
                {
                    departman = reader[0].ToString();

                    reader.Close();

                    Window window = DepartmanSelection(departman);

                    if (window == null)
                        return null;

                    return window;
                }
                else
                {
                    reader.Close();
                    return null;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }

        }
        public Window DecideMainPage(string kullanici_adi, string password)
        {
            try
            {
                string hashedPass = login.CallHashPassword(password);

                Variables.Query_ = $"select top Departman from vbtUserInfo where KullaniciAdi='{kullanici_adi}' and sifre='{hashedPass}'";

                reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.CurrentYear_);

                if (reader.Read())
                {
                    departman = reader[0].ToString();

                    reader.Close();

                    Window window = DepartmanSelection(departman);

                    if (window == null)
                        return null;

                    return window;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }
        public Window GetDepartmentForMainPage(string kullanciAdi, string password)
        {
            try
            {

                login.Address = login.CallGetMotherboardSerialNumber();

                string hashedPass = login.CallHashPassword(password);

                Variables.Query_ = $"select top 1 Departman from vbtUserInfo where KullaniciAdi='{kullanciAdi}' and sifre='{hashedPass}'";

                reader = dataLayer.Select_Command_Data_Reader(Variables.Query_, Variables.CurrentYear_);

                if (!reader.HasRows)
                    return null;
                while (reader.Read())
                {
                    departman = reader[0].ToString();
                }
                reader.Close();

                //giriş yapan ahşaptan ise fabrikayı değiştir
                if (departman.Contains("Ahsap"))
                {
                    variables.Fabrika = "Ahşap";
                    Variables.Fabrika_ = "Ahşap";
                }

                Window window = DepartmanSelection(departman);
                if (window == null)
                    return null;


                return window;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }

        }
        private Window DepartmanSelection(string departman)
        {
            if (departman.Equals("Satis", StringComparison.OrdinalIgnoreCase))
            {
                return new frm_musteri_secim();
            }
            if (departman.Equals("Moduler Planlama", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Talep_Siparislestir();
            }
            if (departman.Equals("Doseme Planlama", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Talep_Siparislestir();
            }
            else if (departman.Equals("Konfeksiyon", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Uretim_Durumu();
            }
            else if (departman.Equals("Yonetici", StringComparison.OrdinalIgnoreCase))
            {
                return new frm_main_window();
            }
            else if (departman.Equals("Bilgi Islem", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Bilgi_Islem();
            }
            else if (departman.Equals("Doseme Kalite", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Uretim_Durumu();
            }
            else if (departman.Equals("Doseme Paketleme", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Paket_Bildir();
            }
            else if (departman.Equals("Moduler Paketleme", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Paket_Bildir();
            }
            else if (departman.Equals("Lojistik", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_SSH_MT_Sevk_Et();
            }
            else if (departman.Equals("Ahsap Planlama", StringComparison.OrdinalIgnoreCase))
            {
                return new Layer_UI.Ahsap.Siparis.Frm_Siparis_Takip();
            }
            else if (departman.Equals("Ahsap Kalite", StringComparison.OrdinalIgnoreCase))
            {
                return new Layer_UI.Ahsap.Irsaliye.Frm_Irsaliye_Kaydet();
            }
            else if (departman.Contains("Depo", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Stok_Hareket_Sorgu();
            }
            else if (departman.Contains("Ar-Ge", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Koli_Uyarla();
            }
            else if (departman.Equals("Satin Alma Dosemeli", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Talep_Siparislestir();
            }
            else if (departman.Equals("Satin Alma Moduler", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Talep_Siparislestir();
            }
            else if (departman.Equals("Optimizasyon", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Uretim_Takip_Karti();
            }
            else if (departman.Equals("Finans", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Irsaliye_Aktarim();
            }
            else if (departman.Equals("Insan Kaynaklari", StringComparison.OrdinalIgnoreCase))
            {
                return new Frm_Dogum_Gunu();
            }
            else { return null; }

        }
    }
}

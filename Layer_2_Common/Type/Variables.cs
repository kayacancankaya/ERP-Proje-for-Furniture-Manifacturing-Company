using PdfSharp.Charting;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Management;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Layer_Data;


namespace Layer_2_Common.Type
{ 

    public class Variables : INotifyPropertyChanged
    {
		public static string Query_ = string.Empty;
		public static int Counter_ = 0;
		public static bool Result_ = false;
		public static bool? FormResult_ = false;
		public static int NumberOfAsyncExecutions = 0;
		public static int ResultInt_ = 0;
		public static string ResultString_ = string.Empty;
        public static string Fabrika_ = "Vita";
        public static string Departman_ = string.Empty;
        public static string ErrorMessage_ = string.Empty;
        public static string WarningMessage_ = string.Empty;
        public static string SuccessMessage_ = string.Empty;
        public static float QumulativeSum_ = 0; 

        private string _query = string.Empty;

		public string Query
		{
			get { return _query; }
			set { _query = value;
                OnPropertyChanged(nameof(Query));
            }
		}
		
		private string _query2 = string.Empty;

		public string Query2
		{
			get { return _query2; }
			set { _query2 = value;
                OnPropertyChanged(nameof(Query2));
            }
		}

		private string _fabrika = "Vita";

		public string Fabrika
		{
			get { return _fabrika; }
			set { 
				_fabrika = value; 
				OnPropertyChanged(nameof(_fabrika));
				}
		}

        public string Departman { get; set; }
        public static string UserName { get; set; }
        public static int UserID { get; set; }

        public static int CurrentYear_ = DateTime.Now.Year;
        public static int Yil_ = GetYil();

        private int _current_year = DateTime.Now.Year;


		public static string ImagePath = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\Images\\";
		
        public static string AppDir = "\\\\192.168.1.11\\Vitabianca\\17-Güncel_Planlar\\";
		
        public static string FilePath = string.Empty;
        public static string FileName = string.Empty;
        public static string SheetName = string.Empty;

        private bool _isChecked = false;
        public bool IsChecked
		{
			get { return _isChecked; }
			set { _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
                
            }
		}
        private bool _isTrue = false;
        public bool IsTrue
		{
			get { return _isTrue; }
			set {
                _isTrue = value;
                OnPropertyChanged(nameof(IsTrue));
                
            }
		}

		private bool _result = false;

		public bool Result
		{
			get { return _result; }
			set { 
				_result = value; 
				OnPropertyChanged(nameof(Result));
				}
		}
		private bool _continue = true;

		public bool Continue
		{
			get { return _continue; }
			set {
                _continue = value; 
				OnPropertyChanged(nameof(Continue));
				}
		}
		private int _resultInt = 0;

		public int ResultInt
		{
			get { return _resultInt; }
			set {
                _resultInt = value; 
				OnPropertyChanged(nameof(ResultInt));
				}
		}
		private int _resultInt16 = 0;

		public int ResultInt16
		{
			get { return _resultInt16; }
			set {
                _resultInt16 = value; 
				OnPropertyChanged(nameof(ResultInt));
				}
		}
        public string ResultString { get; set; }

        private int _counter = 0;

		public int Counter
		{
			get { return _counter; }
			set { _counter = value; }
		}

		private string _errorMessage = string.Empty;

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}

		private string _successMessage = string.Empty;

		public string SuccessMessage
		{
			get { return _successMessage; }
			set {
                _successMessage = value;
				OnPropertyChanged(nameof(SuccessMessage));
			}
		}

        private string _warningMessage = string.Empty;

        public string WarningMessage
        {
            get { return _warningMessage; }
            set
            {
                _warningMessage = value;
                OnPropertyChanged(nameof(WarningMessage));
            }
        }

		private double _qumulativeSum;

		public double QumulativeSum
		{
			get { return _qumulativeSum; }
			set { _qumulativeSum = value; 
				OnPropertyChanged(nameof(QumulativeSum));
				}
		}

        private string _updateMessage = string.Empty;
        public string UpdateMessage
        {
            get { return _updateMessage; }
            set
            {
                _updateMessage = value;
                OnPropertyChanged(nameof(UpdateMessage));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;


        private static int GetYil()
        {
    	    try
    	    {
                int yil = 0;
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@user", SqlDbType.Int);
                parameter[0].Value = GetUserId();

                string query = "Select Yil from vbtuserinfo where UserId = @user";
                using (SqlDataReader reader = DataLayer.Select_Command_Data_Reader_S_Param(query, CurrentYear_,parameter))
                {
                    while (reader.Read())
                    {
                        yil = Convert.ToInt32(reader[0]);
                    }
                    if (!reader.HasRows)
                        return -1;

                    reader.Close();

                    return yil;
                }
            }
            catch 
    	    {
    		    return Convert.ToInt32(DateTime.Now.Year);
    	    }
		}
        public static bool UpdateYil(int yil)
        {
    	    try
    	    {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@user", SqlDbType.Int);
                parameters[0].Value = GetUserId();
                parameters[1] = new SqlParameter("@yil", SqlDbType.Int);
                parameters[1].Value = yil;

                string query = "update vbtuserinfo set Yil=@yil from vbtuserinfo where UserId = @user";
                Result_ = DataLayer.ExecuteCommandWithParametersS(query, CurrentYear_, parameters);
                Variables.Yil_ = GetYil();
                return Result_;
            }
            catch 
    	    {
    		    return false;
    	    }
		}

        public static int GetUserId()
        {
            string hashedAddr = HashPassword(GetMotherboardSerialNumber());
            try
            {

                string query = $"select top 1 UserId from vbtUserInfo where Adres='{hashedAddr}'";

                using (SqlDataReader reader = DataLayer.Select_Command_Data_Reader_S(query, CurrentYear_) )
                {
                    while (reader.Read())
                    {
                        UserID = Convert.ToInt32(reader[0]);
                    }
                    if (!reader.HasRows)
                        return -1;

                    reader.Close();

                    return UserID;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return -1; }
        }
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert the byte to a hexadecimal string.
                }

                return builder.ToString();
            }
        }
        private static string GetMotherboardSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject obj in collection)
                {
                    if (obj["SerialNumber"] != null)
                    {
                        return obj["SerialNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            //// Fallback to a random unique identifier if unable to retrieve the motherboard serial number.
            //return Guid.NewGuid().ToString();
            return null;
        }
        protected void OnPropertyChanged(string propertyChanged)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyChanged));
        }
        
    }
}

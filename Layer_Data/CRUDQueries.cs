using System.Data.SqlClient;
using System.Data;
using System.Windows.Input;
using System;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using System.Windows;

namespace Layer_Data
{
    public class DataLayer
    {
        private string ConnectionString { get; set; } = "";
        SqlConnection connection = null;

        public ObservableCollection<T> Select_Command_Data<T>(string query, int yil, string fabrika, Func<SqlDataReader, T> mapFunction)
        {
            string ConnectionString;

            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            else
                ConnectionString = GetConnectionString(yil);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            ObservableCollection<T> result = new ObservableCollection<T>();

                            while (reader.Read())
                            {
                                T item = mapFunction(reader);
                                result.Add(item);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
                return new ObservableCollection<T>(); 
            }
        }
        public async Task<ObservableCollection<T>> Select_Command_Data_Async<T>(string query, int yil, string fabrika, Func<SqlDataReader, T> mapFunction)
        {
            string ConnectionString;

            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            else
                ConnectionString = GetConnectionString(yil);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            ObservableCollection<T> result = new ObservableCollection<T>();

                            while (reader.Read())
                            {
                                T item = mapFunction(reader);
                                result.Add(item);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                return null;
            }
        }
        public async Task<ObservableCollection<T>> Select_Command_Data_Async_With_Parameters<T>(string query, int yil, string fabrika, SqlParameter[] parameters, Func<SqlDataReader, T> mapFunction)
        {
            string ConnectionString;

            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            else
                ConnectionString = GetConnectionString(yil);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            ObservableCollection<T> result = new ObservableCollection<T>();

                            while (reader.Read())
                            {
                                T item = mapFunction(reader);
                                result.Add(item);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                return null;
            }
        }
        public ObservableCollection<T> Select_Command_Data_With_Parameters<T>(string query, int yil, string fabrika,SqlParameter[] parameters,
                                                                            Func<SqlDataReader, T> mapFunction)
        {
            string ConnectionString;

            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            else
                ConnectionString = GetConnectionString(yil);

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);
                        
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            ObservableCollection<T> result = new ObservableCollection<T>();

                            while (reader.Read())
                            {
                                T item = mapFunction(reader);
                                result.Add(item);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
                return new ObservableCollection<T>(); 
            }
        }

        public SqlDataReader Select_Procedure_Data_Reader_With_Parameters(string query, int yil, SqlParameter[] parameters, string fabrika)
        {
            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            
            else
                ConnectionString = GetConnectionString(yil);

            try
            {

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                
                SqlDataReader sdr = command.ExecuteReader(); 
                return sdr;
            }
            
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                connection.Close();
                return null;
            }
            catch (Exception)
            {
                connection.Close();
                return null;
            }
        }
        public SqlDataReader Select_Command_Data_Reader_With_Parameters(string query, int yil, SqlParameter[] parameters, string fabrika)
        {
            if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
            
            else
                ConnectionString = GetConnectionString(yil);

            try
            {

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 3600;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                
                SqlDataReader sdr = command.ExecuteReader(); 
                return sdr;
            }
            
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                connection.Close();
                return null;
            }
            catch (Exception)
            {
                connection.Close();
                return null;
            }
        }
        public SqlDataReader Select_Command_Data_Reader_With_Parameters(string query, int yil, SqlParameter[] parameters)
        {

            ConnectionString = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                
                SqlDataReader sdr = command.ExecuteReader(); 
                return sdr;
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
                return null;
            }
        }
        public SqlDataReader Select_Command_Data_Reader(string query, int yil, string fabrika)
        {

            if (fabrika == "Ahşap")
               ConnectionString = GetConnectionStringAhsap(yil);
            
            else
                ConnectionString = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(ConnectionString);
                
                    connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                    
                        SqlDataReader sdr = command.ExecuteReader(CommandBehavior.CloseConnection);
                        return sdr;
                    
                
            }

            catch (Exception ex)
            { 
                System.Windows.MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public SqlDataReader Select_Command_Data_Reader(string query, int yil)
        {

            //Create a SqlConnection to the database.
            ConnectionString = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader sdr = command.ExecuteReader(CommandBehavior.CloseConnection); // Ensure that the connection is closed when the reader is closed
                return sdr;
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public static SqlDataReader Select_Command_Data_Reader_S(string query, int yil)
        {

            //Create a SqlConnection to the database.
            string conn = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader sdr = command.ExecuteReader(CommandBehavior.CloseConnection); // Ensure that the connection is closed when the reader is closed
                return sdr;
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public static SqlDataReader Select_Command_Data_Reader_S_Param(string query, int yil,SqlParameter[] parameters)
        {

            //Create a SqlConnection to the database.
            string conn = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
             
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataReader sdr = command.ExecuteReader();
                return sdr;
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public async Task<SqlDataReader> Select_Command_Data_ReaderAsync(string query, int yil)
        {

            ConnectionString = GetConnectionString(yil);
            try
            {

                SqlConnection connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader sdr = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection); 
                return sdr;
            }

            catch
            {
                return null;
            }

        }
        public DataTable Select_Command(string query, int yil, string fabrika)
        { 
             if (fabrika == "Ahşap")
                ConnectionString = GetConnectionStringAhsap(yil);
           
            else
                ConnectionString = GetConnectionString(yil);
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);

                        }
                    }
                    connection.Close();
                }

                return dataTable;
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public DataTable Select_Command(string query, int yil)
        {

            //Create a SqlConnection to the database.
            ConnectionString = GetConnectionString(yil);
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);

                        }
                        return dataTable;
                    }

                }
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public DataTable Select_CommandLtd(string query, int yil)
        {
            string yilCorrection = yil.ToString();
            char secondLastChar = yilCorrection[yilCorrection.Length - 2];
            char lastChar = yilCorrection[yilCorrection.Length - 1];
            string yilShortedstr = new string(new[] { secondLastChar, lastChar });
            int yilShorted = Convert.ToInt32(yilShortedstr);
            //Create a SqlConnection to the database.
            ConnectionString = GetConnectionStringLTD(yilShorted);
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        DataTable dataTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);

                        }
                        return dataTable;
                    }

                }
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
                return null;
            }

        }
        public bool ExecuteCommand(string query, int yil)
        {
            try
            {
                //if(Variables.Yil_<Variables.CurrentYear_ || 
                //    Variables.Yil_>Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                connection = new SqlConnection(ConnectionString);
                
                connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.ExecuteNonQuery();

                    }
                connection.Close ();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool ExecuteCommand(string query, int yil, string fabrika)
        {
            
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.ExecuteNonQuery();

                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public static bool ExecuteCommandWithParametersS(string query, int yil, SqlParameter[] parameters)
        {
            try
            {

                //if ((Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_) &&
                //    query.StartsWith("update vbtuserinfo set Yil=") == false)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                string connStr = GetConnectionString(yil);
                SqlConnection conn = new SqlConnection(connStr);
                
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        command.ExecuteNonQuery();

                    }
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        public bool ExecuteCommandWithParameters(string query, int yil, SqlParameter[] parameters)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                connection = new SqlConnection(ConnectionString);
                
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        command.ExecuteNonQuery();

                    }
                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool ExecuteCommandWithParameters(string query, int yil, SqlParameter[] parameters, string fabrika)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public async Task<bool> ExecuteCommandAsync(string query, int yil, string fabrika)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> ExecuteCommandWithParametersAsync(string query, int yil, SqlParameter[] parameters, string fabrika)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<int> ExecuteCommandWithParametersAsyncReturnsRecordsAffected(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {

            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //    Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return -1;
            //}
            SqlConnection connection = null;

            try
            {
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 1000;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        await connection.OpenAsync();
                    }
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
                return 1;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool ExecuteStoredProcedure(string procedure, int yil)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public async Task <bool> ExecuteStoredProcedureAsync(string procedure, int yil)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 6000;
                        await command.ExecuteNonQueryAsync();

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public async Task <bool> ExecuteStoredProcedureAsync(string procedure, int yil, string fabrika)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 1000;
                        await command.ExecuteNonQueryAsync();

                    }
                }

                return true;
            }
            catch
            {
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public bool ExecuteStoredProcedureWithParameters(string procedure, int yil, Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters, string fabrika)
        {
            try
            {

                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (fabrika == "Ahşap")
                   ConnectionString = GetConnectionStringAhsap(yil);
               
                else
                    ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters from the dictionary
                        foreach (var param in parameters)
                        {
                            if (param.Value.Type == SqlDbType.Decimal)
                            {
                                // For SqlDbType.Decimal, set Precision and Scale
                                SqlParameter decimalParam = new SqlParameter(param.Key, param.Value.Type)
                                {
                                    Precision = (byte)param.Value.Precision,
                                    Scale = (byte)param.Value.Scale,
                                    Value = param.Value.Value,
                                };
                                command.Parameters.Add(decimalParam);
                            }
                            else
                            {
                                // For other types, use Size
                                command.Parameters.Add(new SqlParameter(param.Key, param.Value.Type, param.Value.Precision)).Value = param.Value.Value;
                            }
                        }

                        command.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch 
            {
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public bool ExecuteStoredProcedureWithParameters(string procedure, int yil, Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters from the dictionary
                        foreach (var param in parameters)
                        {
                            if (param.Value.Type == SqlDbType.Decimal)
                            {
                                // For SqlDbType.Decimal, set Precision and Scale
                                SqlParameter decimalParam = new SqlParameter(param.Key, param.Value.Type)
                                {
                                    Precision = (byte)param.Value.Precision,
                                    Scale = (byte)param.Value.Scale,
                                    Value = param.Value.Value,
                                };
                                command.Parameters.Add(decimalParam);
                            }
                            else
                            {
                                // For other types, use Size
                                command.Parameters.Add(new SqlParameter(param.Key, param.Value.Type, param.Value.Precision)).Value = param.Value.Value;
                            }
                        }

                        command.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch 
            {
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public bool ExecuteStoredProcedureWithParameters(string procedure, int yil, SqlParameter[] parameters,string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return false;
            //}
            SqlConnection connection = null;
            try
            {

                
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                
                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 1000;

                        if(parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        command.ExecuteNonQuery();

                    }

                connection.Close();

                return true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public int ExecuteCommandWithParametersReturnsInt(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return -1;
            //}
            int result = 0;
            SqlConnection connection = null;
            try
            {

               
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 1000;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    result = (int)command.ExecuteScalar();

                }

                connection.Close();

                return result;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public int ExecuteStoredProcedureWithParametersReturnsInt(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return -1;
            //}
            int result = 0;
            SqlConnection connection = null;
            try
            {

               
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1000;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    result = (int)command.ExecuteScalar();

                }

                connection.Close();

                return result;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public SqlDataReader ExecuteStoredProcedureWithParametersReturnsReader(string procedure, int yil, SqlParameter[] parameters,string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return null;
            //}
            SqlConnection connection = null;
            try
            {
               
                SqlDataReader reader = null;
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                
                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.Open();

                using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if(parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }

                return reader;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool ExecuteStoredProcedureWithParameters(string procedure, int yil, SqlParameter[] parameters)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if(parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        command.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return false;
            }
        }
        public async Task<bool> ExecuteStoredProcedureWithParametersAsync(string procedure, int yil, SqlParameter[] parameters)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 3600;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                        }
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message.ToString(), "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        public async Task<int> ExecuteStoredProcedureWithParametersAsyncReturnsRecordsAffected(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return -1;
            //}
            SqlConnection connection = null;
            
            try
            {
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();
               
                using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 1000;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                        }
                        await command.ExecuteNonQueryAsync();
                    }
                await connection.CloseAsync();
                return 1;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public async Task<bool> ExecuteStoredProcedureWithParametersAsync(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return false;
            //}
            SqlConnection connection = null;
            try
            {
                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);

                else
                    ConnectionString = GetConnectionString(yil);

                connection = new SqlConnection(ConnectionString);
                connection.OpenAsync();
               
                using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 6000;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                        }
                        await command.ExecuteNonQueryAsync();
                    }
                connection.CloseAsync();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);
                HandleSqlException(ex);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.CloseAsync();
                }
            }
        }
        public ObservableCollection<T> ExecuteStoredProcedureWithParametersReturnsCollection<T>(string procedure, int yil, SqlParameter[] parameters, string fabrika
                                                                                                              , Func<SqlDataReader, T> mapFunction)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return null;
            //}
            SqlConnection connection = null;

            try
            {
                ObservableCollection<T> resultCollection = new ObservableCollection<T>();

                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            ObservableCollection<T> result = new ObservableCollection<T>();

                            while (reader.Read())
                            {
                                T item = mapFunction(reader);
                                result.Add(item);
                            }

                            return result;
                        }

                    }
                }

                return resultCollection;
            }
            catch (SqlException ex)
            {

                HandleSqlException(ex);
                // Consider throwing an exception here or logging the error
                return null; // or an empty ObservableCollection<T>
            }
            catch (Exception)
            {
                // Consider throwing an exception here or logging the error
                return null; // or an empty ObservableCollection<T>
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        
        public async Task<ObservableCollection<T>> ExecuteStoredProcedureWithParametersAsyncReturnsCollection<T>(string procedure, int yil, SqlParameter[] parameters, string fabrika
                                                                                                              , Func<SqlDataReader, T> mapFunction)
        {
            //if (Variables.Yil_ < Variables.CurrentYear_ ||
            //        Variables.Yil_ > Variables.CurrentYear_)
            //{
            //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
            //    return null;
            //}
            SqlConnection connection = null;

            try
            {
                ObservableCollection<T> resultCollection = new ObservableCollection<T>();

                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                    T item = mapFunction(reader);
                                    resultCollection.Add(item);
                            }
                        }

                    }
                }

                return resultCollection;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                // Consider throwing an exception here or logging the error
                return null; // or an empty ObservableCollection<T>
            }
            catch (Exception)
            {
                // Consider throwing an exception here or logging the error
                return null; // or an empty ObservableCollection<T>
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public async Task<bool> ExecuteStoredProcedureWithParametersAsync(string procedure, int yil, Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            if (param.Value.Type == SqlDbType.Decimal)
                            {
                                // For SqlDbType.Decimal, set Precision and Scale
                                SqlParameter decimalParam = new SqlParameter(param.Key, param.Value.Type)
                                {
                                    Precision = (byte)param.Value.Precision,
                                    Scale = (byte)param.Value.Scale,
                                    Value = param.Value.Value,
                                };
                                command.Parameters.Add(decimalParam);
                            }
                            else
                            {
                                // For other types, use Size
                                command.Parameters.Add(new SqlParameter(param.Key, param.Value.Type, param.Value.Precision)).Value = param.Value.Value;
                            }
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                        }
                        await command.ExecuteNonQueryAsync();

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert_Stored_Proc_Param(string proc, string param1, string param1Val, string param2, string param2Val, string param3, string param3Val, string param4, string param4Val, string param5, string param5Val, string param6, string param6Val, int yil, string tableName, int adet)
        {

            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(proc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Define and set the parameters
                        command.Parameters.Add(new SqlParameter(param1, SqlDbType.NVarChar, 35)).Value = param1Val;
                        command.Parameters.Add(new SqlParameter(param2, SqlDbType.NVarChar, 35)).Value = param2Val;
                        command.Parameters.Add(new SqlParameter(param3, SqlDbType.NVarChar, 100)).Value = param3Val;
                        command.Parameters.Add(new SqlParameter(param4, SqlDbType.NVarChar, 50)).Value = param4Val;
                        command.Parameters.Add(new SqlParameter(param5, SqlDbType.NVarChar, 100)).Value = param5Val;
                        command.Parameters.Add(new SqlParameter(param6, SqlDbType.NVarChar, 12)).Value = param6Val;

                        command.ExecuteNonQuery();

                        connection.Close();
						string message = tableName.ToUpper() + " Tablosu Başarıyla Güncellendi. " + adet + " Kayıt Güncellendi.";
						MessageBox.Show(message, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
						
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return false;
            }

        }

        public async Task <bool> Insert_Stored_Proc_Param_6_Async(string proc, string param1, string param1Val, string param2, string param2Val, string param3, string param3Val, string param4, string param4Val, string param5, string param5Val, string param6, string param6Val, int yil, string tableName, int adet)
        {

            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(proc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Define and set the parameters
                        command.Parameters.Add(new SqlParameter(param1, SqlDbType.NVarChar, 35)).Value = param1Val;
                        command.Parameters.Add(new SqlParameter(param2, SqlDbType.NVarChar, 35)).Value = param2Val;
                        command.Parameters.Add(new SqlParameter(param3, SqlDbType.NVarChar, 100)).Value = param3Val;
                        command.Parameters.Add(new SqlParameter(param4, SqlDbType.NVarChar, 50)).Value = param4Val;
                        command.Parameters.Add(new SqlParameter(param5, SqlDbType.NVarChar, 100)).Value = param5Val;
                        command.Parameters.Add(new SqlParameter(param6, SqlDbType.NVarChar, 12)).Value = param6Val;

                        await command.ExecuteNonQueryAsync();
                        
                        await connection.CloseAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return false;
            }

        }
        
        public bool Insert_Stored_Proc_Param_6(string proc, string param1, string param1Val, string param2, string param2Val, string param3, string param3Val, string param4, string param4Val, string param5, string param5Val, string param6, string param6Val, int yil, string tableName, int adet)
        {

            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(proc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Define and set the parameters
                        command.Parameters.Add(new SqlParameter(param1, SqlDbType.NVarChar, 35)).Value = param1Val;
                        command.Parameters.Add(new SqlParameter(param2, SqlDbType.NVarChar, 35)).Value = param2Val;
                        command.Parameters.Add(new SqlParameter(param3, SqlDbType.NVarChar, 100)).Value = param3Val;
                        command.Parameters.Add(new SqlParameter(param4, SqlDbType.NVarChar, 50)).Value = param4Val;
                        command.Parameters.Add(new SqlParameter(param5, SqlDbType.NVarChar, 100)).Value = param5Val;
                        command.Parameters.Add(new SqlParameter(param6, SqlDbType.NVarChar, 12)).Value = param6Val;

                        command.ExecuteNonQuery();
                        
                        connection.Close();
						string message = tableName.ToUpper() + " Tablosu Başarıyla Güncellendi. " + adet + " Kayıt Güncellendi.";
						MessageBox.Show(message, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return false;
            }

        }

        public int Get_One_Int_Result_Command(string query, int yil, string fabrika)
        {
            try
            {
                if(fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }
        public async Task<int> Get_One_Int_Result_Command_With_Parameters_Async(string query, int yil,SqlParameter[] parameters, string fabrika)
        {
            try
            {
                if(fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                        }

                        object result = await command.ExecuteScalarAsync();

                        await connection.CloseAsync();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }
        public double Get_One_Double_Result_Command_With_Parameters(string query, int yil,SqlParameter[] parameters, string fabrika)
        {
            try
            {
                if(fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return Convert.ToSingle(result);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }
        public int Get_One_Int_Result_Command_With_Parameters(string query, int yil,SqlParameter[] parameters, string fabrika)
        {
            try
            {
                if(fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }
        public string Get_One_String_Result_Command(string procedure, int yil)
        {
            try
            {
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        public string Get_One_String_Result_Command_With_Parameters(string procedure, int yil, SqlParameter[] parameters,string fabrika)
        {
            try
            {
                if(fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        public string Get_One_String_Result_Command_With_Parameters(string procedure, int yil, SqlParameter[] parameters)
        {
            try
            {

                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddRange(parameters);
                        
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        
        public string Get_One_String_Result_Stored_Proc_With_Parameters(string procedure, int yil, SqlParameter[] parameters)
        {
            try
            {

                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters);
                        
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        public string Get_One_String_Result_Stored_Proc_With_Parameters(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            try
            {

                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        public async Task<string> Get_One_String_Result_Stored_Proc_With_Parameters_Async(string procedure, int yil, SqlParameter[] parameters, string fabrika)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (fabrika == "Ahşap")
                        ConnectionString = GetConnectionStringAhsap(yil);
                    else
                        ConnectionString = GetConnectionString(yil);
                });

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddRange(parameters);

                        if (connection.State == ConnectionState.Closed)
                        {
                           await connection.OpenAsync();
                        }

                        object result = await command.ExecuteScalarAsync();

                        await connection.CloseAsync();
                       
                            if (result != null)
                            {
                                return result.ToString();
                            }
                            else
                            {
                                return string.Empty;
                            }
                        
                    }
                }
            }
            catch { return "STRING HATA"; }

        }
        public int Get_One_Int_Result_Stored_Proc_With_Parameters(string procedure, int yil, Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters, string fabrika)
        {
            try
            {
                if (fabrika == "Ahşap")
                   ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            if (param.Value.Type == SqlDbType.Decimal)
                            {
                                // For SqlDbType.Decimal, set Precision and Scale
                                SqlParameter decimalParam = new SqlParameter(param.Key, param.Value.Type)
                                {
                                    Precision = (byte)param.Value.Precision,
                                    Scale = (byte)param.Value.Scale,
                                    Value = param.Value.Value,
                                };
                                command.Parameters.Add(decimalParam);
                            }
                            else
                            {
                                // For other types, use Size
                                command.Parameters.Add(new SqlParameter(param.Key, param.Value.Type, param.Value.Precision)).Value = param.Value.Value;
                            }
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return Convert.ToInt32(result); 
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }
        public int Get_One_Int_Result_Stored_Proc_With_Parameters(string procedure, int yil, Dictionary<string, (SqlDbType Type, int Precision, int Scale, object Value)> parameters)
        {
            try
            {

                ConnectionString = GetConnectionString(yil);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(procedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            if (param.Value.Type == SqlDbType.Decimal)
                            {
                                // For SqlDbType.Decimal, set Precision and Scale
                                SqlParameter decimalParam = new SqlParameter(param.Key, param.Value.Type)
                                {
                                    Precision = (byte)param.Value.Precision,
                                    Scale = (byte)param.Value.Scale,
                                    Value = param.Value.Value,
                                };
                                command.Parameters.Add(decimalParam);
                            }
                            else
                            {
                                // For other types, use Size
                                command.Parameters.Add(new SqlParameter(param.Key, param.Value.Type, param.Value.Precision)).Value = param.Value.Value;
                            }
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return Convert.ToInt32(result); 
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch { return -1; }

        }

        public string Get_One_String_Result_Stored_Proc(string procedure, int yil)
		{
			try
			{

				ConnectionString = GetConnectionString(yil);
				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(procedure, connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						
						if (connection.State == ConnectionState.Closed)
						{
							connection.Open();
						}

						object result = command.ExecuteScalar();

						connection.Close();

						if (result != null)
						{
							return result.ToString();
						}
						else
						{
							return string.Empty;
						}
					}
				}
			}
			catch { return "STRING HATA"; }

		}

        public DataTable Stored_Proc_With_Parameters_Returns_Table(string procedureName, int yil,SqlParameter[] parameters)
        {
            
                try
                {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                string connectionString = GetConnectionString(yil); 

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(procedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            if(parameters != null)
                                command.Parameters.AddRange(parameters);

                           DataTable dataTable = new DataTable();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return null;
            }
            
        }
        public DataTable Stored_Proc_Parameterless_Returns_Table(string procedureName, int yil)
        {
            
                try
                {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                string connectionString = GetConnectionString(yil); 

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(procedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            DataTable dataTable = new DataTable();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return null;
            }
            
        }
        public DataTable Select_Stored_Proc_LTD(string proc,string param1,string param2,int param1Val,string param2Val, int yil)
        {
            
                try
                {
                    string yilCorrection = yil.ToString();
                    char secondLastChar = yilCorrection[yilCorrection.Length - 2];
                    char lastChar = yilCorrection[yilCorrection.Length - 1];
                    string yilShortedstr = new string(new[] { secondLastChar, lastChar });
                    int yilShorted = Convert.ToInt32(yilShortedstr);
                    string connectionString = GetConnectionStringLTD(yilShorted); 
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(proc, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Define and set the parameters
                            command.Parameters.Add(new SqlParameter(param1, SqlDbType.Int)).Value = param1Val; 
                            command.Parameters.Add(new SqlParameter(param2, SqlDbType.NVarChar, -1)).Value = param2Val; 

                            // Create a DataTable to hold the results
                            DataTable dataTable = new DataTable();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                Mouse.OverrideCursor = null;
                return null;
            }
            
        }
        public void Insert_Statement(string query, int yil, string tableName, int adet)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return ;
                //}

                using (SqlConnection connection = new SqlConnection(GetConnectionString(yil)))
                {
                    connection.Open();

                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();

                    connection.Close();
					string message = tableName.ToUpper() + " Tablosu Başarıyla Güncellendi. " + adet + " Kayıt Güncellendi.";
					MessageBox.Show(message, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
                catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null; 
            }
        }
        public void Update_Statement(string query, int yil, string tableName, int adet)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //   Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return;
                //}
                using (SqlConnection connection = new SqlConnection(GetConnectionString(yil)))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
					string message = tableName.ToUpper() + " Tablosu Başarıyla Güncellendi. " + adet + " Kayıt Güncellendi.";
					MessageBox.Show(message, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
					
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
            }
        }
        public void Update_Statement(string query, int yil)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //    Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                using (SqlConnection connection = new SqlConnection(GetConnectionString(yil)))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
            }
        }
        public void Delete_Statement(string query, int yil, string tableName, int adet)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //   Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return;
                //}
                using (SqlConnection connection = new SqlConnection(GetConnectionString(yil)))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
					string message = tableName.ToUpper() + " Tablosu Başarıyla Güncellendi. " + adet + " Kayıt Silindi.";
					MessageBox.Show(message, "Başarılı İşlem", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the task
                System.Windows.MessageBox.Show(ex.Message);
                // loading durumunda ise kaldır
                Mouse.OverrideCursor = null;
            }
        }

        public async Task<bool> BulkInsertToSqlAsync(DataTable excelData, string tableName,Dictionary<string,string>mappingDic,int yil,string fabrika)
        {
            try
            {
                //if (Variables.Yil_ < Variables.CurrentYear_ ||
                //   Variables.Yil_ > Variables.CurrentYear_)
                //{
                //    CRUDmessages.GeneralFailureMessageCustomMessage("Sadece Şimdiki Yıla Ait Veriler Güncellenebilir.");
                //    return false;
                //}
                if (string.IsNullOrEmpty(tableName))
                    return false;
                if (excelData == null)
                    return false;
                if (excelData.Rows.Count == 0)
                    return false;
                if (mappingDic == null)
                    return false;
                if (mappingDic.Count == 0)
                    return false;

                string ConnectionString;

                if (fabrika == "Ahşap")
                    ConnectionString = GetConnectionStringAhsap(yil);
                else
                    ConnectionString = GetConnectionString(yil);


                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        
                        foreach(var item in mappingDic)
                            bulkCopy.ColumnMappings.Add(item.Key, item.Value);
                        

                        await bulkCopy.WriteToServerAsync(excelData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        private void HandleSqlException(SqlException ex)
        {
            foreach (SqlError error in ex.Errors)
            {
                if (error.Number == -2)  // SQL Server timeout error
                {
					MessageBox.Show("Sorgu Zaman Aşımına Uğradı.", "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
                }

                if(error.Message.Contains("Arithmetic overflow"))
				{
					MessageBox.Show("Miktar Dönüşümünde", "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
				}
				MessageBox.Show(ex.Message.ToString(), "Başarısız İşlem", MessageBoxButton.OK, MessageBoxImage.Warning);


                return;
            }
        }
        static private string GetConnectionStringLTD(int yil)
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=192.168.1.11;Initial Catalog=VITALTD" + yil + ";Persist Security Info=True;User ID=sa;Password=sapass;TrustServerCertificate=true";
        }
        static private string GetConnectionStringAhsap(int yil)
        {
            yil = yil % 100;
            return "Data Source=192.168.1.11;Initial Catalog=VITAHSAPLTD" + yil + ";Persist Security Info=True;User ID=sa;Password=sapass;TrustServerCertificate=true";
        }
        static private string GetConnectionString(int yil)
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=192.168.1.11;Initial Catalog=VITA" + yil + ";Persist Security Info=True;User ID=sa;Password=sapass;TrustServerCertificate=true";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVDLDataAccess
{
   public class clsDriversDataAccess
    {

        static public bool GetDriversByID(int DriverID, ref int PersonID, ref int CreateByUserID, ref DateTime CreateDate)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Drivers where DriverID=@DriverID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    DriverID = (int)reader["DriverID"];
                    PersonID = (int)reader["PersonID"];
                    CreateByUserID = (int)reader["CreateByUserID"];
                    CreateDate = (DateTime)reader["CreateDate"];

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();

            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        static public bool GetDriversByPersonID(int PersonID, ref int DriverID, ref int CreateByUserID, ref DateTime CreateDate)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
                    PersonID = (int)reader["PersonID"];
                    CreateByUserID = (int)reader["CreateByUserID"];
                    CreateDate = (DateTime)reader["CreateDate"];

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();

            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        static public int AddNewDrivers(int PersonID, int CreateByUserID)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO Drivers (PersonID,CreateByUserID,CreateDate)
                VALUES(@PersonID,@CreateByUserID,@CreateDate);
                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);
            command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }
            catch
            {
                DriverID = -1;
            }
            finally
            {
                connection.Close();
            }
            return DriverID;

        }

        static public bool UpdateDrivers(int DriverID, int PersonID, int CreateByUserID, DateTime CreateDate)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE Drivers   SET PersonID =@PersonID
                                               , CreateDate =@CreateDate
                                               ,CreateByUserID =@CreateByUserID
                                               , IsActive =@IsActive where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("CreateByUserID", CreateByUserID);
            command.Parameters.AddWithValue("CreateDate", CreateDate);
            command.Parameters.AddWithValue("DriverID", DriverID);
            try
            {
                connection.Open();
                RowAffect = command.ExecuteNonQuery();

            }
            catch
            {
                RowAffect = 0;
            }
            finally
            {
                connection.Close();
            }
            return (RowAffect > 0);
        }
        static public DataTable GetAllDriverss()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT * FROM  Drivers_View ";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

    }
}

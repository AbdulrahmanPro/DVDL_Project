using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVDLDataAccess
{
    public class CountryDataAccess
    {
        static public bool GetCountryInfoByID(int CountryID,ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * from Countries where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
                
            }catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public bool GetCountryInfoByID(ref int CountryID,  string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * from Countries where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public bool IsExist(int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select Found=1 from Countries Where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();

            }catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        static public DataTable GetAllCountries()
        {
            DataTable dt=new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "Select * from Countries";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }catch(Exception ex)
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

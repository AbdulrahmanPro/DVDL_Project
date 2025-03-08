using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVDLDataAccess
{
    public class clsApplicationType
    {
        static public bool GetApplicationtypeByID(int ApplicationTypeID,ref string ApplicationTypeTitle,ref float ApplicatiionFees)
        {
                bool IsFound = false;
                SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
                string Query = "select * From ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";
                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                    IsFound = true;
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicatiionFees =Convert.ToSingle(reader["ApplicatiionFees"]);

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

        static public int AddNewApplicationType(string ApplicationTypeTitle, float ApplicatiionFees)
        {
            int ApplixationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO ApplicationTypes (ApplicationTypeTitle,ApplicatiionFees)
                VALUES(@ApplicationTypeTitle,@ApplicatiionFees);
                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicatiionFees", ApplicatiionFees);
             try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplixationID = insertedID;
                }
            }
            catch
            {
                ApplixationID = -1;
            }
            finally
            {
                connection.Close();
            }
            return ApplixationID;

        }

        static public bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicatiionFees)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE ApplicationTypes   SET ApplicationTypeTitle =@ApplicationTypeTitle
                                               , ApplicatiionFees =@ApplicatiionFees
                                                where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("ApplicatiionFees", ApplicatiionFees);
              command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                RowAffect= command.ExecuteNonQuery();

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
        static public DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT ApplicationTypeID,ApplicationTypeTitle,ApplicatiionFees FROM  ApplicationTypes ";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVDLDataAccess
{
    public class ApplicationsDataAccess
    {
        static public bool GetApplicationInfoByID(int ApplicationID, ref int ApplicationPersonID,
            ref DateTime ApplicationDate, ref int ApplicationTypeID, ref short ApplicationStatus
            , ref DateTime LastStatusDate, ref float PaidFees, ref int CreateByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * from Applications Where ApplicationID=@ApplicationID ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    ApplicationPersonID = (int)reader["ApplicationPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreateByUserID = (int)reader["CreateByUserID"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        static public int AddNewApplication(int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID
           , short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreateByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO Applications (ApplicationPersonID,ApplicationDate,
                            ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreateByUserID)
     VALUES
           ( @ApplicationPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees
             ,@CreateByUserID);
                select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;

        }

        static public bool UpdateApplictaion(int ID, int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID
         , short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreateByUserID)
        {
            int RowCount = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE Applications
                SET ApplicationPersonID =@ApplicationPersonID
              ,ApplicationDate =@ApplicationDate
              ,ApplicationTypeID =@ApplicationTypeID
              ,ApplicationStatus =@ApplicationStatus
              ,LastStatusDate =@LastStatusDate
              ,PaidFees =@PaidFees
              ,CreateByUserID =@CreateByUserID
              WHERE ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);

            try
            {
                connection.Open();

                RowCount = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowCount > 0);

        }

        static public bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
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

        static public DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT Applications.ApplicationID, Applications.LastStatusDate,
            Applications.ApplicationPersonID, Applications.ApplicationDate, Applications.ApplicationTypeID, Applications.ApplicationStatus,
			  Applications.PaidFees, Applications.CreateByUserID from Applications ";
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
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        static public bool DeleteApplication(int ApplicationID)
        {
            int RowCount = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"DELETE FROM Applications WHERE ApplicationID =@ApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                RowCount = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowCount > 0);
        }

        static public int GetActiveApplicationID(int PersonID,int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"Select ActiveApplication=ApplicationID from Applications where ApplicationPersonID=@PersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int AppID)) {
                    ActiveApplicationID = AppID;
                } ;

            }
            catch
            {
                return ActiveApplicationID;

            }
            finally
            {
                connection.Close();
            }
            return ActiveApplicationID;
        }
        static public int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID) {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplcationID
                            WHERE ApplicationPersonID = @ApplicationPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result!= null && int.TryParse(result.ToString(),out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch
            {
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }
            return ActiveApplicationID;
        }

        static public bool DoesPersonIDHaveApplicationID(int PersonID,int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID )!=-1);
        }

        static public bool UpdateStatus(int ApplicationID,short NewStatus)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"update Applications set ApplicationStatus=@NewStatus,
                                                  LastStatusDate = @LastStatusDate
                                                  where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            try
            {
                connection.Open();
              RowAffect=  command.ExecuteNonQuery();
            }
            catch
            {
                return false;

            }
            finally
            {
                connection.Close();
            }
            return (RowAffect > 0);

        

        }
    }
}

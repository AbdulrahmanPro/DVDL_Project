using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVDLDataAccess
{
    public class clsTestAppointments
    {


        static public bool GetTestAppointmentInfoByID(int TestAppointmentID, ref int TestTypeID,
            ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,
             ref float PaidFees, ref int CreateByUser,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * from TestAppointments Where TestAppointmentID=@TestAppointmentID ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreateByUser = (int)reader["CreateByUser"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        static public bool GetLastTestAppointment(int TestAppointmentID,  int TestTypeID,
          ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,
           ref float PaidFees, ref int CreateByUser, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"select top 1  * from TestAppointments Where TestTypeID=@TestTypeID
                                and LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
                                    order by TestAppointmentID desc ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreateByUser = (int)reader["CreateByUser"];
                    IsLocked = (bool)reader["IsLocked"];
                    RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        static public DataTable GetAllTestAppointment()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"select * from TestAppointment_View
                                order by AppointmentDate Desc ";
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

        static public DataTable GetApplicationTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"select TestAppointmentID,AppointmentDate, PaidFees, IsLocked from TestAppointments
                                where (TestTypeID=@TestTypeID)
                                and (LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID)
                                   order by TestAppointmentID Desc ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
        static public int AddNewTestAppointments(  int TestTypeID,
             int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,
              float PaidFees,  int CreateByUser,  bool IsLocked,  int RetakeTestApplicationID)
        {
            int TestAppointment = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO TestAppointments
                        (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate
                        ,PaidFees,CreateByUser,IsLocked,RetakeTestApplicationID)
                        VALUES(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees
                ,@CreateByUser,@IsLocked,@RetakeTestApplicationID)



                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreateByUser", CreateByUser);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);


            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointment = insertedID;
                }
            }
            catch
            {
                TestTypeID = -1;
            }
            finally
            {
                connection.Close();
            }
            return TestAppointment;

        }

        static public bool UpdateTestAppointments(int TestAppointmentID, int TestTypeID,
             int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
              float PaidFees, int CreateByUser, bool IsLocked, int RetakeTestApplicationID)
        {
            int RowAffectc = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"Update  TestAppointments  
                            set TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked=@IsLocked,
                                RetakeTestApplicationID=@RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID)



                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreateByUser", CreateByUser);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);


            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);


            try
            {
                connection.Open();
                RowAffectc = command.ExecuteNonQuery();

            }
            catch
            {
                RowAffectc=0;
            }
            finally
            {
                connection.Close();
            }
            return (RowAffectc>0);

        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }

    }
}

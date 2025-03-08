using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVDLDataAccess
{
    public class clsTest
    {
        static public bool GetTestByTestID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreateByUser)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Tests where TestID=@TestID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreateByUser = (int)reader["CreateByUser"];

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
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
               (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
                 ref int TestAppointmentID, ref bool TestResult,
                 ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreateByUser, Applications.ApplicationPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplcationID = Applications.ApplicationID
                WHERE        (Applications.ApplicationPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID Desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        static public int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreateByUser)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO Tests (TestAppointmentID, TestResult,Notes,CreateByUser)
                VALUES(@TestAppointmentID,@TestResult,@Notes,@CreateByUser);
                      Update TestAppointments set IsLocked=1 Where  TestAppointmentID=@TestAppointmentID;
                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreateByUser", CreateByUser);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }
            catch
            {
                TestID = -1;
            }
            finally
            {
                connection.Close();
            }
            return TestID;

        }

        static public bool UpdateUser(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreateByUser)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE Tests   SET TestAppointmentID =@TestAppointmentID
                                               , TestResult =@TestResult
                                               , Notes =@Notes
                                               , CreateByUser =@CreateByUser where TestID=@TestID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("TestResult", TestResult);
            command.Parameters.AddWithValue("Notes", Notes);
            command.Parameters.AddWithValue("CreateByUser", CreateByUser);
            command.Parameters.AddWithValue("TestID", TestID);
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
        static public DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT *  FROM  Tests order by TestID";
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
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);

            string query = @"select PassTest =Count(TestTypeID) from Tests inner join 
		                        TestAppointments on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
		                        where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestResult=1

		                       ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
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

            return PassedTestCount;



        }



    }
}

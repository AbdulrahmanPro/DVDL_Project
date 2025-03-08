using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVDLDataAccess
{
    public class clsTestType
    {
        static public bool GetTestTypeByID(int TestTypeID,ref string TestTypeTitle,ref string TestTypeDescripation,ref float TestTypeFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From TestType where TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescripation = (string)reader["TestTypeDescripation"];
                    TestTypeFees =Convert.ToSingle(reader["TestTypeFees"]);

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

        static public int AddNewTestType(string TestTypeTitle, string TestTypeDescripation, float TestTypeFees)
        {
            int TestTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO TestType (TestTypeTitle,TestTypeDescripation,TestTypeFees)
                VALUES(@TestTypeTitle,@TestTypeDescripation,@TestTypeFees);
                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescripation", TestTypeDescripation);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
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
            return TestTypeID;

        }

        static public bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescripation, float TestTypeFees)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE TestType   SET TestTypeTitle =@TestTypeTitle
                                               , TestTypeFees =@TestTypeFees
                                               ,TestTypeDescripation =@TestTypeDescripation
                                               where TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("TestTypeDescripation", TestTypeDescripation);
            command.Parameters.AddWithValue("TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
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
        static public DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT TestTypeID,TestTypeTitle,TestTypeDescripation,TestTypeFees FROM  TestType ";
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

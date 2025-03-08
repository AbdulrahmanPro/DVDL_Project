using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVDLDataAccess
{
  public  class clsUsersDataAccess
    {
        static public bool GetUserByUserID(int UserID,ref int PersonID,ref string UserName,ref string Password,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }
                else
                {
                    IsFound = false;
                }
                reader.Close();

            }
            catch {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        static public bool GetUserByPersonID(ref int UserID,  int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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

        static public bool GetUserByUserNameAndPassword(ref int UserID, ref int PersonID,ref  string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = "select * From Users where UserName=@UserName and Password=@Password";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName); 
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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

        static public int AddNewUser(int PersonID,string UserName,string Password,bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"INSERT INTO Users (PersonID, UserName,Password,IsActive)
                VALUES(@PersonID,@UserName,@Password,@IsActive);
                    select SCOPE_IDENTITY ();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID)){
                    UserID = insertedID;
                }
            }
            catch
            {
                UserID = -1;
            }
            finally
            {
                connection.Close();
            }
            return UserID;

        }

        static public bool UpdateUser(int UserID,int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"UPDATE Users   SET PersonID =@PersonID
                                               , UserName =@UserName
                                               , Password =@Password
                                               , IsActive =@IsActive where UserID=@UserID";
            
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("UserName", UserName);
            command.Parameters.AddWithValue("Password", Password);
            command.Parameters.AddWithValue("IsActive", IsActive);
            command.Parameters.AddWithValue("UserID", UserID);
            try
            {
                connection.Open();
                RowAffect = command.ExecuteNonQuery();

            }
            catch
            {
                RowAffect =0;
            }
            finally
            {
                connection.Close();
            }
            return (RowAffect > 0);
        }
        static public DataTable GetAllUsers()
        {
            DataTable dt=new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = Pepole.FirstName + ' ' + Pepole.SecondName + ' ' + ISNULL( Pepole.ThirdName,'') +' ' + Pepole.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    Pepole ON Users.PersonID = Pepole.PersonID";
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
        static public bool DeleteUser(int UserID)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"delete Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("UserID", UserID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
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
       
        static public bool IsUserExist(int UserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"Select found=1 from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        static public bool IsUserExist(string UserName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"Select found=1 from Users where UserName=@UserName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Open();
            }
            return IsFound;
        }
        static public bool IsUserExistForPersonID(int PersonID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"Select found=1 from Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        static public bool ChangePassword(int UserID, string NewPassword)
        {
            int RowAffect = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessString.ConnectionString);
            string Query = @"update Users set Password=@Password where UserID=@UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
               RowAffect= command.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowAffect > 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BackEndCointerest.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;
        public DBServices()
        {

        }
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //---------------------------------------------------------------------------------
        // Users sections
        //---------------------------------------------------------------------------------

        public List<User> Get_users(string username1)
        {
            SqlConnection con = null;
            List<User> users = new List<User>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM Users_2022 WHERE username = '" + username1 +"'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                while (dr.Read())
                {
                    User usr = new User();
                    usr.Username = (string)dr["username"];
                    usr.Birthdate = (DateTime)dr["birthdate"];
                    usr.Image = (string)dr["userImage"];
                    usr.Password = (string)dr["userPassword"];
                    users.Add(usr);

                }
                return users;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int Insert(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return 0;
                }
                else throw;
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        private String BuildInsertCommand(User user)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}')", user.Username, user.Password, user.Image, user.Birthdate);
            String prefix = "INSERT INTO Users_2022 " + "([username], [userPassword], [userImage],[birthdate])";
            command = prefix + sb.ToString();

            return command;
        }




    }
}
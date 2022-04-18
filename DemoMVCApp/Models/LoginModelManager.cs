

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DemoMVCApp.Models
{
    public class LoginModelManager
    {
        public void InsertStudentInfo(LoginModel loginModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection CN = new SqlConnection(scn))
            {
                using (SqlCommand cmd = new SqlCommand("SP_RegisterStudent_info",CN))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    try
                    {
                        //@StudentEmail,@StudentPassword,@StudentName,@Role
                        cmd.Parameters.AddWithValue("@StudentEmail",loginModel.UserEmail);
                        cmd.Parameters.AddWithValue("@StudentPassword",loginModel.UserPassword);
                        cmd.Parameters.AddWithValue("@StudentName",loginModel.UserName);
                        cmd.Parameters.AddWithValue("@Role",loginModel.Role);
                        CN.Open();
                        int count = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        if (CN.State == System.Data.ConnectionState.Open)
                        {
                            CN.Close();
                        }
                    }

                }
            }
        }

        public LoginModel UserAuthentication(LoginModel loginModel)
        {
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using(SqlConnection cn=new SqlConnection(scn))
            {
                using(SqlCommand cmd=new SqlCommand("SP_ValidateUserInfo",cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserEmail", System.Data.SqlDbType.VarChar, 40).Value = loginModel.UserEmail;
                    cmd.Parameters.Add("@UserPassword", System.Data.SqlDbType.VarChar, 40).Value = loginModel.UserPassword;

                    try
                    {
                        cn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        while(dr.Read())
                        {
                            if (dr["StudentEmail"]!=DBNull.Value&& dr["StudentPassword"]!=DBNull.Value)
                            {
                                loginModel.UserEmail = Convert.ToString(dr["StudentEmail"]);
                                loginModel.Role = Convert.ToString(dr["Role"]);
                                loginModel.UserName = Convert.ToString(dr["StudentName"]);
                                loginModel.IsValid = 1;

                            }
                            else
                            {
                                loginModel.IsValid = 0;

                            }
                        }
                        dr.Close();
                    }
                    catch (Exception Ex)
                    {

                        throw;
                    }
                    finally
                    {
                        if (cn.State==System.Data.ConnectionState.Open)
                        {
                            cn.Close();
                        }
                    }
                    return loginModel;
                }
            }
        }
    }
}
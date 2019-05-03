using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using User.Helper;
using User.Models.User;

namespace User.Services.User
{
    public class UserServices
    {
        private readonly string _connectionString = "Data Source=DESKTOP-DCUVCOM\\SQLEXPRESS; Initial Catalog=User; User ID=sa; Password=root";
        public int i_user_id { get; set; }
        public string vc_first_name { get; set; }
        public string vc_last_name { get; set; }
        public string vc_email { get; set; }
        public string vc_password { get; set; }
        public string vc_user_role { get; set; }
        public string need { get; set; }

        public bool b_is_deleted { get; set; }
        public bool b_is_admin { get; set; }

        int ExecuteProc(string need)
        {
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(nameof(need)           ,need),
                new SqlParameter(nameof(i_user_id)      ,i_user_id),
                new SqlParameter(nameof(vc_first_name)  ,vc_first_name),
                new SqlParameter(nameof(vc_last_name)   ,vc_last_name),
                new SqlParameter(nameof(vc_email)       ,vc_email),
                new SqlParameter(nameof(vc_password)    ,vc_password),
                new SqlParameter(nameof(vc_user_role)   ,vc_user_role),

            };
            return SqlQuery.SetDataFromProcedureGetID(list, "prc_i_u_s_d_tbl_user", _connectionString);
        }

        public DefaultResponse CheckLogin(string email_id, string password)
        {
            vc_email = email_id;
            vc_password = password;
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(nameof (vc_email),vc_email),
                new SqlParameter(nameof(vc_password), vc_password)
            };

            DefaultResponse defaultResponse = new DefaultResponse();
            need = "s";
            try
            {
                DataTable dt = SqlQuery.GetDataTableFromProcedure(list, "prc_s_check_login", _connectionString);
                if (dt.Rows.Count > 0)
                {
                    List<UserModel> lsusermodel = GetModelList(dt);

                    defaultResponse.payload = lsusermodel;
                    defaultResponse.result = DefaultResponse.ActionResult.success.ToString();
                    defaultResponse.message = "User Loged In Successfully";

                }
                else
                {
                    defaultResponse.result = DefaultResponse.ActionResult.failure.ToString();
                    defaultResponse.message = "Invalid Login.";
                }
            }
            catch (Exception ex)
            {
                defaultResponse.result = DefaultResponse.ActionResult.failure.ToString();
                defaultResponse.message = "Invalid Login.";
            }
            return defaultResponse;
        }

        public DefaultResponse GetUserList()
        {
            DefaultResponse defaultResponse = new DefaultResponse();
            need = "s";
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(nameof(need), need)
            };
            try
            {
                DataTable dt = SqlQuery.GetDataTableFromProcedure(list, "prc_i_u_s_d_tbl_user", _connectionString);
                if (dt.Rows.Count > 0)
                {
                    List<UserModel> lsusermodel = GetModelList(dt);
                    defaultResponse.payload = lsusermodel;
                    defaultResponse.result = DefaultResponse.ActionResult.success.ToString();
                    defaultResponse.message = "The data of user list";

                }
            }
            catch (Exception ex)
            {
                defaultResponse.result = DefaultResponse.ActionResult.failure.ToString();
                defaultResponse.message = "Error while getting user list";
            }
            return defaultResponse;
        }

        public DefaultResponse Inactive(int userid)
        {
            DefaultResponse defaultResponse = new DefaultResponse();
            need = "ia";
            i_user_id = userid;
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(nameof(need), need),
                new SqlParameter(nameof(i_user_id), i_user_id)
            };
            try
            {
                DataTable dt = SqlQuery.GetDataTableFromProcedure(list, "prc_i_u_s_d_tbl_user", _connectionString);
                if (dt.Rows.Count > 0)
                {
                    defaultResponse.result = DefaultResponse.ActionResult.success.ToString();
                    defaultResponse.message = "user deleted successfully";

                }
            }
            catch (Exception ex)
            {
                defaultResponse.result = DefaultResponse.ActionResult.failure.ToString();
                defaultResponse.message = "Error while deleting user";
            }
            return defaultResponse;
        }

        public DefaultResponse Active(int userid)
        {
            DefaultResponse defaultResponse = new DefaultResponse();
            need = "a";
            i_user_id = userid;
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter(nameof(need), need),
                new SqlParameter(nameof(i_user_id), i_user_id)
            };
            try
            {
                DataTable dt = SqlQuery.GetDataTableFromProcedure(list, "prc_i_u_s_d_tbl_user", _connectionString);
                if (dt.Rows.Count > 0)
                {
                    defaultResponse.result = DefaultResponse.ActionResult.success.ToString();
                    defaultResponse.message = "user deleted successfully";

                }
            }
            catch (Exception ex)
            {
                defaultResponse.result = DefaultResponse.ActionResult.failure.ToString();
                defaultResponse.message = "Error while deleting user";
            }
            return defaultResponse;
        }

        public static List<UserModel> GetModelList(DataTable dt)
        {
            return dt.AsEnumerable().Select(item => new UserModel()
            {
                user_id     = Convert.ToInt32(item[nameof(i_user_id)]),
                first_name  = item[nameof(vc_first_name)].ToString(),
                last_name   = item[nameof(vc_last_name)].ToString(),
                email       = item[nameof(vc_email)].ToString(),
                password    = item[nameof(vc_password)].ToString(),
                user_role   = item[nameof(vc_user_role)].ToString(),
                is_admin    = Convert.ToBoolean(item[nameof(b_is_admin)]),
                is_deleted  = Convert.ToBoolean(item[nameof(b_is_deleted)]),
            }).ToList();
        }

        public DefaultResponse Create()
        {
            DefaultResponse defaultRespose = new DefaultResponse();
            try
            {
                int id = ExecuteProc("i");
                if (id > 0)
                {
                    defaultRespose.result = DefaultResponse.ActionResult.success.ToString();
                    defaultRespose.payload = id;
                    defaultRespose.message = "The user created successfully";
                }
                else
                {
                    defaultRespose.result = DefaultResponse.ActionResult.failure.ToString();
                    defaultRespose.message = "Error while creating user";
                }
            }
            catch (Exception ex)
            {
                defaultRespose.result = DefaultResponse.ActionResult.failure.ToString();
                defaultRespose.message = "Error while creating user";
            }
            return defaultRespose;
        }

        public DefaultResponse Update()
        {
            DefaultResponse defaultRespose = new DefaultResponse();
            try
            {
                int id = ExecuteProc("u");
                if (id > 0)
                {
                    defaultRespose.result = DefaultResponse.ActionResult.success.ToString();
                    defaultRespose.payload = id;
                    defaultRespose.message = "The user updated successfully";
                }
                else
                {
                    defaultRespose.result = DefaultResponse.ActionResult.failure.ToString();
                    defaultRespose.message = "Error while updated user";
                }
            }
            catch (Exception ex)
            {
                defaultRespose.result = DefaultResponse.ActionResult.failure.ToString();
                defaultRespose.message = "Error while updated user";
            }
            return defaultRespose;
        }

    }


}
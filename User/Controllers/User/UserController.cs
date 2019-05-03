using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using User.Helper;
using User.Models.User;
using User.Services.User;

namespace User.Controllers.User
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/user/login")]
        // GET: api/User/5
        public DefaultResponse GetLoginCheck(string email_id, string password)
        {
            UserServices user_services = new UserServices();
            return user_services.CheckLogin(email_id, password);
        }

        [HttpGet]
        [Route("api/user/user_list")]
        // GET: api/User/5
        public DefaultResponse GetUserList()
        {
            UserServices userServices = new UserServices();
            return userServices.GetUserList();
        }

        [HttpPost]
        [Route("api/user/create")]
        // POST: api/User
        public DefaultResponse Create([FromBody]UserModel userModel)
        {
            UserServices userService = new UserServices()
            {
                i_user_id       = userModel.user_id,
                vc_first_name   = userModel.first_name,
                vc_last_name    = userModel.last_name,
                vc_email        = userModel.email,
                vc_password     = userModel.password,
                vc_user_role    = userModel.user_role,
            };
            return userService.Create();
        }

        [HttpPost]
        [Route("api/user/update")]
        // POST: api/User
        public DefaultResponse Update([FromBody]UserModel userModel)
        {
            UserServices userService = new UserServices()
            {
                i_user_id       = userModel.user_id,
                vc_first_name   = userModel.first_name,
                vc_last_name    = userModel.last_name,
                vc_email        = userModel.email,
                vc_password     = userModel.password,
                vc_user_role    = userModel.user_role,
            };
            return userService.Update();
        }

        // DELETE: api/User/5
        [HttpDelete]
        [Route("api/user/status/active")]
        public DefaultResponse Inactive([FromBody]UserModel userModel)
        {
            UserServices userServices = new UserServices();
            return userServices.Inactive(userModel.user_id);
        }

        [HttpDelete]
        [Route("api/user/status/inactive")]
        public DefaultResponse Active([FromBody]UserModel userModel)
        {
            UserServices userServices = new UserServices();
            return userServices.Active(userModel.user_id);
        }
    }
}

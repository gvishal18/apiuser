using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Models.User
{
    public class UserModel
    {
        public int    user_id      {get;set;}
        public string first_name   {get;set;}
        public string last_name    {get;set;}
        public string email        {get;set;}
        public string password     {get;set;}
        public string user_role    {get;set;}
        public bool is_admin       { get; set; }
        public bool is_deleted     { get; set; }
    }
}
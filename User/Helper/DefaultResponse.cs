using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Helper
{
    public class DefaultResponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public object payload { get; set; }
        public enum ActionResult
        {
            success,
            failure
        }

    }
}
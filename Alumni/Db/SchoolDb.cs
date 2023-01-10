using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Db
{
    public class SchoolDb : DbDapperExtension
    {
        public SchoolDb()
              : base(ConfigurationManager.ConnectionStrings["School"].ConnectionString)
        {
        }
    }
}
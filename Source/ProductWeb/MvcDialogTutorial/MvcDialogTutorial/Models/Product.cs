using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcDialogTutorial.Models
{
    public class Product
    {
        public int ProductId
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwasiHealthCare.Model
{
    public class CountryModel
    {
        public CountryModel()
        {
            CountryList = new List<SelectListItem>();
        }
        public List<SelectListItem> CountryList { get; set; }

        public string SelCountry { get; set; }
    }
}

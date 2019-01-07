﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.Models
{
    public class ObjectViewModel
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public string DiaGioiHanhChinhCode { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string DuLieuVe { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public int? Zoom { get; set; }
    }
}
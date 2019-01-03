using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.Models
{
    public class GetThongTinByLatLngViewModel
    {
        public string Id { get; set; }
        public string MaxLat { get; set; }
        public string MinLat { get; set; }
        public string MaxLng { get; set; }
        public string MinLng { get; set; }
        public string Ten { get; set; }
        public string DiaGioiHanhChinhCode { get; set; }
    }
}
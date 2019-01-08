namespace GisTest.ViewModels
{
    public class ObjectViewModel
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public string DiaGioiHanhChinhCode { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string DuLieuVe { get; set; }
        public int? Level { get; set; }
        public string Description { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public int? Zoom { get; set; }
    }
}
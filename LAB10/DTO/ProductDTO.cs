namespace LAB10.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Depth { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}

namespace DesignPatterns.Creational.Builder
{
    public class Item
    {
        public Item(ItemBuilder itemBuilder)
        {
            IdItem = itemBuilder.IdItem;
            Category = itemBuilder.Category;
            Description = itemBuilder.Description;
            Width = itemBuilder.Width;
            Height = itemBuilder.Height;
            Length = itemBuilder.Length;
            Weight = itemBuilder.Weight;
        }

        public int IdItem { get; private set; }

        public string Category { get; private set; }
        
        public string Description { get; private set; }
        
        public decimal? Width { get; private set; }
        
        public decimal? Height { get; private set; }
        
        public decimal? Length { get; private set; }
        
        public decimal? Weight { get; private set; }

        public decimal? GetVolume()
        {
            if(Width == null || Height == null || Length == null) return 0;

            return Width / 100 * Height / 100 * Length / 100;
        }

        public decimal? GetDensity()
        {
            if(Width == null || Height == null || Length == null || Height == null || Weight == null) return 0;

            return Weight / GetVolume();
        }
    }
}

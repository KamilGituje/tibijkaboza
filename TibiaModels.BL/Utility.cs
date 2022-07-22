namespace TibiaModels.BL
{
    public static class Utility
    {
        public static bool Drop(decimal dropRate)
        {
            bool drop = false;
            Random rng = new Random();
            decimal rngNumber = Convert.ToDecimal(rng.NextDouble());
            if (rngNumber <= dropRate)
            {
                drop = true;
            }
            return drop;
        }
    }
}

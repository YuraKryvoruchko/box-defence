namespace BoxDefence.Towers
{
    public interface ITowerImprovementPricelist
    {
        int PriceImprovement { get; }
    }
    public interface ITowerImprovement : ITowerImprovementPricelist
    {
        int Index { get; }
    }
}


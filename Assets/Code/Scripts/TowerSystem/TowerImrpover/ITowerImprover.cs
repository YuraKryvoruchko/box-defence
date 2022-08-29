namespace BoxDefence.Towers
{
    public interface ITowerImproverPricelist
    {
        int GetNextLevelPrice();
        ITowerImprovementPricelist GetTowerImprovementPricelist();
    }
    public interface ITowerImprover : ITowerImproverPricelist
    {
        void Improve();
        bool IsMaxLevel();
    }
}

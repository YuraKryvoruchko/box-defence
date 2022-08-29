using System;

namespace BoxDefence.Towers
{
    public interface ITowerPricelist
    {
        public int Price { get; }
        public int PriceReturn { get; }

        public event Action<ITowerPricelist> OnSet;
        public event Action<ITowerPricelist> OnDelete;
        public event Action<ITowerImprovement> OnImproving;
    }
}

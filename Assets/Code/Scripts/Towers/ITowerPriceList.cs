using System;

namespace BoxDefence.Towers
{
    public interface ITowerPriceList
    {
        public int Price { get; }
        public int PriceReturn { get; }

        public event Action<ITowerPriceList> OnSet;
        public event Action<ITowerPriceList> OnDelete;
    }
}

namespace BoxDefence.Towers
{
    public interface ITowerCharacteristic<T>
    {
        T[] Levels { get; }

        void SetLevelCharacteristics(T levelCharacteristic);
    }
}

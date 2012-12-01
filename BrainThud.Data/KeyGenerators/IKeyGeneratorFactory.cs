namespace BrainThud.Data.KeyGenerators
{
    public interface IKeyGeneratorFactory 
    {
        ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>();
    }
}
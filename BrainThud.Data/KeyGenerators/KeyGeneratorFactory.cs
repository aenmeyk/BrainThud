namespace BrainThud.Data.KeyGenerators
{
    public class KeyGeneratorFactory: IKeyGeneratorFactory
    {
        public ITableStorageKeyGenerator GetTableStorageKeyGenerator<T>()
        {
            return new TableStorageKeyGenerator();
        }
    }
}
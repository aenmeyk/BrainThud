using BrainThud.Core;
using BrainThud.Web.Data.AzureTableStorage;
using Moq;

namespace BrainThudTest.Builders
{
    public class TableStorageContextFactoryMockBuilder
    {
        private readonly Mock<ITableStorageContextFactory> tableStorageContextFactory;

        public TableStorageContextFactoryMockBuilder()
        {
            this.tableStorageContextFactory = new Mock<ITableStorageContextFactory> {DefaultValue = DefaultValue.Mock};
        }

        public TableStorageContextFactoryMockBuilder SetTableStorageContext(Mock<ITableStorageContext> tableStorageContext)
        {
            return this.SetTableStorageContext(tableStorageContext, AzureTableNames.CARD, TestValues.NAME_IDENTIFIER);
        }

        public TableStorageContextFactoryMockBuilder SetTableStorageContext(Mock<ITableStorageContext> tableStorageContext, string tableName, string nameIdentifier)
        {
            tableStorageContextFactory.Setup(x => x.CreateTableStorageContext(tableName, nameIdentifier)).Returns(tableStorageContext.Object);
            return this;
        }

        public Mock<ITableStorageContextFactory> Build()
        {
            return tableStorageContextFactory;
        }
    }
}
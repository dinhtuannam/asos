namespace Catalog.Application.Commons.Interfaces;

public interface IDataContextInitializer
{
	Task SeedAsync();
	Task InitProduct();

	Task InitBrand();
	Task InitCategory();
	Task  InitColor();
}

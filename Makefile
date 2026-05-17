run:
	@dotnet run --project ProductCatalog.Api

dev:
	@dotnet watch --project ProductCatalog.Api

build_publish:
	@dotnet publish -c Release ProductCatalog.Api

migrate:
	@dotnet ef migrations add InitialCreate --project ProductCatalog.Api

migrate-apply:
	@dotnet ef database update --project ProductCatalog.Api

userId:
	@dotnet user-secrets init --project ProductCatalog.Api

package:
	@dotnet list package --project ProductCatalog.Api

test:
	@dotnet test ProductCatalog.Api.Tests
using Ecommerce.CredentialProvider;
using Ecommerce.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

# region Credentials

ICredentialProvider credentialProvider =
    CredentialProviderFactory.GetCredentialProvider(builder.Environment, builder.Configuration);

string productsDbConnection = credentialProvider.GetProductsDbConnection();

# endregion

# region Core

builder.Services
    .AddInfrastructure(productsDbConnection)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

# endregion

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
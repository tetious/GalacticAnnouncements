using Marten;
using Weasel.Postgresql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("main"));
    options.CreateDatabasesForTenants(db =>
    {
        db.ForTenant()
            .CheckAgainstPgDatabase()
            .WithEncoding("UTF-8")
            .CreatePLV8()
            .ConnectionLimit(-1);
    });
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

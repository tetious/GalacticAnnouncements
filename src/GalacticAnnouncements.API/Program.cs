using GalacticAnnouncements.API;
using Marten;
using Marten.NodaTime;
using Weasel.Postgresql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IClock>(SystemClock.Instance);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("main"));
    options.UseNodaTime();
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

builder.Services.AddControllers()
    .AddJsonOptions(options => Json.SetSerializerSettings(options.JsonSerializerOptions));

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

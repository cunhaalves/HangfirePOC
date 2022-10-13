using Hangfire.SqlServer;
using Hangfire;


var builder = WebApplication.CreateBuilder(args);

//Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Hangfire;Integrated Security=True", new SqlServerStorageOptions
{
    
    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.Zero,
    UseRecommendedIsolationLevel = true
}));

builder.Services.AddHangfireServer();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.UseAuthorization();

//GlobalConfiguration.Configuration
//    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//    .UseColouredConsoleLogProvider()
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseSqlServerStorage(app.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
//    {
//        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//        QueuePollInterval = TimeSpan.Zero,
//        UseRecommendedIsolationLevel = true
//    });

app.MapControllers();

app.UseHangfireDashboard();

app.Run();




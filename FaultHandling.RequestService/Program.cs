using FaultHandling.RequestService.Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ClientPolicy>();

builder.Services.AddControllers();

//configure global Policy for HttpClient with Test name
builder.Services.AddHttpClient("Test")
    .AddPolicyHandler(request => request.Method != HttpMethod.Get ?
        new ClientPolicy().ImmediateHttpRetry :
        new ClientPolicy().LinearHttpRetry);

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

app.UseAuthorization();

app.MapControllers();

app.Run();

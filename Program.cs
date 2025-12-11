var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<MockVotingService>();
app.MapGrpcService<MockRegistrationService>();

app.MapGet("/", () => "Voting Mock Service Running");

//local host
//app.Run();

var port = Environment.GetEnvironmentVariable("PORT") ?? "9091";
app.Run($"http://0.0.0.0:{port}");


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<MockVotingService>();
app.MapGrpcService<MockRegistrationService>();

app.MapGet("/", () => "Voting Mock Service Running");

app.Run();

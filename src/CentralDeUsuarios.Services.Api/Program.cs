using CentralDeUsuarios.Infra.Messages.Consumers;
using CentralDeUsuarios.Services.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Setup.AddRegisterServices(builder);
Setup.AddEntityFrameworkServices(builder);
Setup.AddMessageServices(builder);
Setup.AddAutoMapperServices(builder);
Setup.AddMongoDBServices(builder);

//Mensageria
builder.Services.AddHostedService<MessageQueueConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

//Declaração publica da classe para testes de integração
public partial class Program { }
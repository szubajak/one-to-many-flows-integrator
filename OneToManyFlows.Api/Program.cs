using OneToManyFlows.Api.Core;
using OneToManyFlows.Api.Flows;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddKeyedTransient<ISomeFlowHandler, GoogleSomeFlowRequestHandler>(Provider.Google);
builder.Services.AddKeyedTransient<ISomeFlowHandler, MicrosoftSomeFlowRequestHandler>(Provider.Microsoft);
builder.Services.AddKeyedTransient<ISomeFlowHandler, AwsSomeFlowRequestHandler>(Provider.Aws);

builder.Services.AddKeyedTransient<IOtherFlowHandler, GoogleOtherFlowRequestHandler>(Provider.Google);
builder.Services.AddKeyedTransient<IOtherFlowHandler, MicrosoftOtherRequestHandler>(Provider.Microsoft);
builder.Services.AddKeyedTransient<IOtherFlowHandler, AwsOtherFlowRequestHandler>(Provider.Aws);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<EntraIdOptions>(builder.Configuration.GetSection(EntraIdOptions.SectionName));

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseRouting();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

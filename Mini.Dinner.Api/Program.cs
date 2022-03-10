using Autofac;
using Autofac.Extensions.DependencyInjection;
using Mini.Dinner.Api.DependencyInjecttion;
using Mini.Dinner.Services.Common;
using Mini.Dinner.Services.Common.Impl;
using Mini.Dinner.Services.Common.Interface;
using Mini.Dinner.Utill;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
//注入
//builder.Services.AddScoped<IWxService, WxService>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterModule()));
builder.Services.AddControllers().AddControllersAsServices();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
IHttpClientFactory httpClientFactory = (IHttpClientFactory)app.Services.GetService(typeof(IHttpClientFactory));
#pragma warning disable CS8604 // 引用类型参数可能为 null。
HttpRequestServices.SetHttpClientFactory(httpClientFactory);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://*:9000");

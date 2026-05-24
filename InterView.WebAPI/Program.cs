using DotNetCore.AspNetCore;
using DotNetCore.IoC;
using DotNetCore.Results;
using DotNetCore.Security;
using DotNetCore.Services;
using InterView.Application.Core;
using InterView.Application.Interfaces.Dictionary;
using InterView.Application.Interfaces.Exam;
using InterView.Application.Interfaces.Lesson;
using InterView.Application.Interfaces.User;
using InterView.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression();
builder.Services.AddJsonStringLocalizer();
builder.Services.AddResultService();
builder.Services.AddHashService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtService();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(Context))));
builder.Services.AddClassesMatchingInterfaces(nameof(InterView));
builder.Services.AddLogging();
builder.Services.AddScoped<IDictionaryRepository, DictionaryService>();
builder.Services.AddScoped<ILessonRepository, LessonService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IExamRepository, ExamService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerDefault();
builder.Services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyResolvers();
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowAll");

app.UseException();
app.UseHttps();
app.UseLocalization("en", "pt");
app.UseResponseCompression();
app.UseRouting();
app.UseHttpMethodOverride();
app.UseAuthentication();
app.UseStaticFiles();
app.UseSwagger().UseSwaggerUI();
app.MapControllers();

app.MapFallbackToFile("index.html");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;
using web.Db;
using web.Models;

namespace web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        /*
        builder.Services.AddRazorPages()
        .AddMvcOptions(options =>
        {
            options.MaxModelValidationErrors = 50;
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                _ => "The field is required.");
        });
        */

        builder.Services.AddSwaggerGen();

        //QRPulseConfig
        builder.Services.Configure<QRPulseConfig>(builder.Configuration.GetSection("QRPulseConfig"));

        //Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
        builder.Host.UseSerilog((hostingContext, services, configuration) => {
            // Email. НЕ РАБОТАЕТ С MailKit >= 3.0
            var log = configuration.ReadFrom.Configuration(hostingContext.Configuration);
            if (!string.IsNullOrEmpty(hostingContext.Configuration["Serilog.Smtp:MailServer"]))
            {
                var port = 465;
                int.TryParse(hostingContext.Configuration["Serilog.Smtp:Port"], out port);
                log.WriteTo.Email(new EmailConnectionInfo()
                {
                    FromEmail = hostingContext.Configuration["Serilog.Smtp:FromEmail"],
                    EmailSubject = hostingContext.Configuration["Serilog.Smtp:MailSubject"],
                    ToEmail = hostingContext.Configuration["Serilog.Smtp:ToEmail"],
                    MailServer = hostingContext.Configuration["Serilog.Smtp:MailServer"],
                    Port = port,
                    EnableSsl = bool.Parse(hostingContext.Configuration["Serilog.Smtp:EnableSsl"]),
                    NetworkCredentials = new NetworkCredential(
                        hostingContext.Configuration["Serilog.Smtp:UserName"],
                        hostingContext.Configuration["Serilog.Smtp:Password"]
                    ),
                    ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true,
                },
                restrictedToMinimumLevel: (LogEventLevel)hostingContext.Configuration.GetValue<int>("Serilog.Smtp:LogLevel"),
                batchPostingLimit: hostingContext.Configuration.GetValue<int>("Serilog.Smtp:BatchPostingLimit"),
                outputTemplate: hostingContext.Configuration["Serilog.Smtp:outputTemplate"]
                )
                .Enrich.FromLogContext()
                .Enrich.WithProperty("qrpulse3", "qrpulse3");
            }

            /*
            configuration
                .WriteTo.File("logs\\qrpulse3-.log")
                .WriteTo.Console();
            */
        });







        var serverVersion = new MariaDbServerVersion(new Version(10, 5, 10));
        //            services.AddDbContextFactory<PuskContext>(options => {
        builder.Services.AddDbContext<MyContext>(options => {
            switch (Environment.MachineName)
            {
                case "REDMI2022-01":
                    options
                        .UseMySql(builder.Configuration.GetConnectionString("db-Redmi2022"),
                            serverVersion, opt =>
                                opt.UseNewtonsoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedSemantically)
                                .EnableRetryOnFailure()
                        )
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .LogTo(Log.Logger.Information, LogLevel.Information);
                    break;
                    
                case "MacBook-Pro-Vlada":
                    options
                        .UseMySql(builder.Configuration.GetConnectionString("db-vlada"),
                            serverVersion, opt =>
                                opt.UseNewtonsoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedSemantically)
                                .EnableRetryOnFailure()
                        )
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .LogTo(Log.Logger.Information, LogLevel.Information);
                    break;

                case "Sasha":
                    options
                        .UseMySql(builder.Configuration.GetConnectionString("db-sasha"),
                            serverVersion, opt =>
                                opt.UseNewtonsoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedSemantically)
                                .EnableRetryOnFailure()
                        )
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .LogTo(Log.Logger.Information, LogLevel.Information);
                    break;

                case "MacBook-Air-Anna":
                    options
                        .UseMySql(builder.Configuration.GetConnectionString("db-anya"),
                            serverVersion, opt =>
                                opt.UseNewtonsoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedSemantically)
                                .EnableRetryOnFailure()
                        )
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .LogTo(Log.Logger.Information, LogLevel.Information);
                    break;

                default:
                    options
                        .UseMySql(builder.Configuration.GetConnectionString("db-default"), serverVersion, opt =>
                        opt.UseNewtonsoftJson(MySqlCommonJsonChangeTrackingOptions.FullHierarchyOptimizedSemantically))
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors()
                            .LogTo(Log.Logger.Information, LogLevel.Information);
                    break;
            }
        });



        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/home/login");
        builder.Services.AddAuthorization();

        builder.Services.AddCors(p => p.AddPolicy("AllowCors", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }    
}
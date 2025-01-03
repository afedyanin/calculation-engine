using Hangfire.PostgreSql;
using Hangfire;
using HfDemo.WebApi.BackgroundServices;
using Hangfire.Client;
using Hangfire.Server;
using Hangfire.States;
using HfDemo.WebApi.JobManagement;
using Hangfire.Common;
using CalcEngine.Application;

namespace HfDemo.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCalcEngineApplication();

        builder.Services.AddSingleton<IBackgroundJobFactory>(x => new CustomBackgroundJobFactory(
            new BackgroundJobFactory(x.GetRequiredService<IJobFilterProvider>())));

        builder.Services.AddSingleton<IBackgroundJobPerformer>(x => new CustomBackgroundJobPerformer(
            new BackgroundJobPerformer(
                x.GetRequiredService<IJobFilterProvider>(),
                x.GetRequiredService<JobActivator>(),
                TaskScheduler.Default)));

        builder.Services.AddSingleton<IBackgroundJobStateChanger>(x => new CustomBackgroundJobStateChanger(
            new BackgroundJobStateChanger(x.GetRequiredService<IJobFilterProvider>())));

        builder.Services.AddHangfire(config =>
            config.UseSimpleAssemblyNameTypeSerializer()
            .UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection"))));

        builder.Services.AddHostedService<RecurringJobsService>();

        builder.Services.AddHangfireServer(options =>
        {
            options.StopTimeout = TimeSpan.FromSeconds(15);
            options.ShutdownTimeout = TimeSpan.FromSeconds(30);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.UseHangfireDashboard();

        app.Run();
    }
}

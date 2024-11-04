using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Service.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HospitalLibrary.Core.Service
{
    public class ReportGenerator : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        
        private readonly ILogger<ReportGenerator> _logger;

        public ReportGenerator(IServiceScopeFactory scopeFactory, ILogger<ReportGenerator> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (DateTime.Now.Day != 1)
            {
                _logger.LogInformation("Today is not the first day of the month. No report generation.");
                return;
            }
            using var scope = _scopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

            var users = userService.GetAll();

            foreach (var user in users.Where(u => u.Role == Role.Author))
            {
                try
                {
                    reportService.CreateReport(user.Id);
                    _logger.LogInformation($"Report generated for user {user.Username}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error generating report for user {user.Username}.");
                }
            }

            var topAuthor = reportService.TopAuthorThisMonth();
            _logger.LogInformation($"TOP AUTHOR THIS MONTH IS {topAuthor.Username}.");

            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
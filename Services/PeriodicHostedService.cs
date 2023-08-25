using funda_assignment.Utils;

namespace funda_assignment.Services
{
    class PeriodicHostedService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromHours(Constants.PERIODIC_SERVICE_HOURS_INTERVAL);
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<PeriodicHostedService> _logger;

        public PeriodicHostedService(
            ILogger<PeriodicHostedService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            bool firstExecution = true;
            if (firstExecution)
            {
                firstExecution = false;
                _logger.LogInformation($"Executed PeriodicHostedService");
                await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                IAgentsService agentsService = asyncScope.ServiceProvider.GetRequiredService<IAgentsService>();
                await agentsService.SaveAgentsWithMostProperties();
                await agentsService.SaveAgentsWihMostPropertiesWithGarden();
            }

                while (
                    !stoppingToken.IsCancellationRequested &&
                    await timer.WaitForNextTickAsync(stoppingToken)
                )
                {
                    try
                    {

                        _logger.LogInformation($"Executed PeriodicHostedService");
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                        IAgentsService agentsService = asyncScope.ServiceProvider.GetRequiredService<IAgentsService>();
                        await agentsService.SaveAgentsWithMostProperties();
                        await agentsService.SaveAgentsWihMostPropertiesWithGarden();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError ($"{ex.Message}");
                    }
                }
            }
        }
    }
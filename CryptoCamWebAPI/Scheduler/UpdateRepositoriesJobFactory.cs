using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace CryptoCamWebAPI.Scheduler
{   
    public class UpdateRepositoriesJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateRepositoriesJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<UpdateRepositoriesJob>();
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}

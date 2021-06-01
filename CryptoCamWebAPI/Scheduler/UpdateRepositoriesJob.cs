using CryptoCamWebAPI.WebServices.CoinGecko;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CryptoCamWebAPI.WebServices;
using CryptoCamWebAPI.Controllers;

namespace CryptoCamWebAPI.Scheduler
{
    public class UpdateRepositoriesJob : IJob
	{
		private readonly IExchangeRates_API exchangeRates_API;
		public UpdateRepositoriesJob(IExchangeRates_API exchangeRates_API)
        {
			this.exchangeRates_API = exchangeRates_API;
        }
		public async Task Execute(IJobExecutionContext context)
		{
			 await new SyncAssetsRepositoriesController(this.exchangeRates_API).UpdateRepositories();
		}
	}
}

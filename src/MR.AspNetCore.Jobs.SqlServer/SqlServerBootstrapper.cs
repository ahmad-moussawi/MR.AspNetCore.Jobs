using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MR.AspNetCore.Jobs.Server;

namespace MR.AspNetCore.Jobs
{
	public class SqlServerBootstrapper : BootstrapperBase
	{
		public SqlServerBootstrapper(
			JobsOptions options,
			IStorage storage,
			IProcessingServer server,
			IApplicationLifetime appLifetime)
			: base(options, storage, server, appLifetime)
		{
		}

		public override Task BootstrapCoreAsync()
		{
			return base.BootstrapCoreAsync();
		}
	}
}

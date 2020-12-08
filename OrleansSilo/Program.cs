using Grains.Abstractions;
using Grains.Implementations;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrleansSilo
{
    public class Program
    {
        private static readonly AutoResetEvent Closing = new AutoResetEvent(false);

        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Silo is ready!");

                Console.CancelKeyPress += OnExit;
                Closing.WaitOne();

                Console.WriteLine("Shutting down...");

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            string invariant = "MySql.Data.MySqlClient";
            //string mySqlConnectionString = "Server=myssql-container;Port=3306;Database=OrleansV2;Uid=root;Pwd=1428010;";
            string mySqlConnectionString = "Server=10.100.106.217;Port=3306;Database=OrleansV2;Uid=root;Pwd=1428010;";
            
            var builder = new SiloHostBuilder()
                .Configure<ClusterOptions>(options => { options.ClusterId = "testcluster"; options.ServiceId = "testservice"; })
                //.ConfigureEndpoints(new Random(1).Next(30001, 30100), new Random(1).Next(20001, 20100), listenOnAnyHostAddress: true)
                .ConfigureEndpoints(11118, 30001, listenOnAnyHostAddress: true)
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = mySqlConnectionString;
                })
                .UseAdoNetReminderService(options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = mySqlConnectionString;
                })
                .AddAdoNetGrainStorage("MySqlStore", options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = mySqlConnectionString;
                })
                //.AddMemoryGrainStorageAsDefault()
                //.UseKubeMembership()
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TituloGrain).Assembly).WithReferences())
                .UseDashboard(options => options.Port = 9292)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TituloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());


            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Closing.Set();
        }
    }
}

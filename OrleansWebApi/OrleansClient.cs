using Business.Abstractions;
using Grains.Abstractions;
using Grains.Implementations;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansWebApi
{
    public class OrleansClient : IOrleansClient
    {
        private readonly IClusterClient _client;
        private const string SERVICE_ID = "testservice";

        public OrleansClient()
        {
            string invariant = "MySql.Data.MySqlClient";
            //string mySqlConnectionString = "Server=myssql-container;Port=3306;Database=OrleansV2;Uid=root;Pwd=1428010;";
            string mySqlConnectionString = "Server=10.100.106.217;Port=3306;Database=OrleansV2;Uid=root;Pwd=1428010;";
            
            string clusterId = "testcluster";

            _client = new ClientBuilder()
                    .UseAdoNetClustering(options =>
                    {
                        options.Invariant = invariant;
                        options.ConnectionString = mySqlConnectionString;
                    })
                   .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = clusterId;
                       options.ServiceId = SERVICE_ID;
                   })
                   .ConfigureLogging(builder =>
                   {
                       builder.SetMinimumLevel(LogLevel.Information);
                       //builder.AddFile($"{SERVICE_ID}-{DateTime.Now:yyyyMMdd}.log", LogLevel.Information);
                   })
                   //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ITituloGrain).Assembly).WithReferences())
                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TituloGrain).Assembly).WithReferences())
                   .Build();

            _client.Connect().Wait();
        }

        public Task PersistirCotizacion(string grainKey, ICotizacion cotizacion)
        {
            var tituloGrain = GetGrain(grainKey);

            return tituloGrain.NuevaCotizacion(cotizacion, false, false);
        }

        public Task<ICotizacion> ObtenerCotizacion(string grainKey)
        {
            var tituloGrain = GetGrain(grainKey);

            return tituloGrain.GetCotizacionRealTime();
        }

        private ITituloGrain GetGrain(string grainKeyModel)
        {

            return _client.GetGrain<ITituloGrain>(grainKeyModel);
        }
    }
}


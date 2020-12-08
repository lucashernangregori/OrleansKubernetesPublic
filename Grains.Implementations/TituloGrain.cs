using Business.Abstractions;
using Grains.Abstractions;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Implementations
{
    [StorageProvider(ProviderName = "MySqlStore")]
    public class TituloGrain : Grain<TituloGrainState>, ITituloGrain//, IRemindable
    {
        private readonly ILogger logger;

        public TituloGrain(ILogger<TituloGrain> logger)
        {
            this.logger = logger;
        }

        //public Task<ICotizacion> GetCotizacionRealTime()
        //{
        //    var hora = DateTime.Now;
        //    this.logger.LogInformation($"TituloGrain activated = '{hora}'");
            
        //    var cotizacion = new Cotizacion {
        //        FechaHora = hora,
        //        Ultima = new Random(1).Next(80, 120)
        //    };

        //    return Task.FromResult((ICotizacion)cotizacion);
        //}

        public Task<ICotizacion> GetCotizacionRealTime()
        {
            return Task.FromResult(State.Cotizacion);
        }

        public async Task NuevaCotizacion(ICotizacion nuevaCotizacion, bool guardarEnBaseDeDatos, bool forzarPersistencia)
        {
            State.SetUltimaCotizacion(nuevaCotizacion);
            await base.WriteStateAsync();
        }

        //public Task ReceiveReminder(string reminderName, TickStatus status)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}

using Business.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Abstractions
{
    public interface ITituloGrain : Orleans.IGrainWithStringKey
    {
        Task<ICotizacion> GetCotizacionRealTime();
        Task NuevaCotizacion(ICotizacion nuevaCotizacion, bool guardarEnBaseDeDatos, bool forzarPersistencia);
    }
}

using Business.Abstractions;
using System.Threading.Tasks;

namespace OrleansWebApi
{
    public interface IOrleansClient
    {
        Task<ICotizacion> ObtenerCotizacion(string grainKey);
        Task PersistirCotizacion(string grainKey, ICotizacion cotizacion);

    }
}
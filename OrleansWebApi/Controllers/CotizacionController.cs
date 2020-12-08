using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace OrleansWebApi.Controllers
{
    [ApiController]
    [Route("api/cotizacion")]
    public class CotizacionController : Controller
    {
        private readonly IOrleansClient _orleansClient;

        public CotizacionController(IOrleansClient orleansClient)
        {
            _orleansClient = orleansClient;
        }

        //[HttpGet]
        //public async Task<ActionResult> Index()
        //{
        //    return Ok(24);

        //}

        [HttpGet]
        public async Task<ActionResult> Obtener([FromQuery] string titulo)
        {
            var cotizacion = await _orleansClient.ObtenerCotizacion(titulo);
            return Ok(cotizacion.Ultima);

        }

        /* 
         curl --location --request GET 'http://3.139.60.116:31536/api/cotizacion?titulo=pamp'
         */

        //[HttpGet("{id}")]
        //[HttpGet]
        //public async Task<ActionResult> Poner([FromQuery]string titulo, [FromQuery]decimal cotizacion)
        //{
        //    await _orleansClient.PersistirCotizacion(titulo, new Cotizacion { FechaHora = DateTime.Now, Ultima = cotizacion });
        //    return Ok("ok");
        //}

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> Poner([FromBody] ParametrosPost parametros)
        {
            await _orleansClient.PersistirCotizacion(parametros.Titulo, new Cotizacion { FechaHora = DateTime.Now, Ultima = parametros.Cotizacion });
            //await _orleansClient.PersistirCotizacion(parametros.Titulo, new Cotizacion { FechaHora = DateTime.Now, Ultima = 10.52m });
            return Ok("ok");
        }

        /*
            * curl --location --request POST 'http://3.139.60.116:31536/api/cotizacion' \
                --header 'Content-Type: application/json' \
                --data-raw '{
                "Titulo":"ay24",
                "Cotizacion":12.3
}'
         */


        //public IActionResult Index()
        //{

        //    try
        //    {
        //        //var cotizacion = _orleansClient.ObtenerCotizacion(grainKeyModel);
        //        //var cotizacionWrapper = new OrleansCotizacionWrapper { Cotizacion = cotizacion };
        //        var cotizacionWrapper = 35;

        //        return Ok(cotizacionWrapper);
        //    }
        //    catch (Exception exc)
        //    {
        //        //Helper.IOLLogger.LogMensaje(TipoLog.Fatal, exc);

        //        //return InternalServerError(exc);
        //    }
        //    return View();
        //}

    }

    public class ParametrosPost
    {
        public string Titulo { get; set; }
        public decimal Cotizacion { get; set; }

    }
}

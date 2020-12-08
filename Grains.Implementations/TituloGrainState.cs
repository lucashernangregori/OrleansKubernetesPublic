using Business.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grains.Implementations
{
    public class TituloGrainState
    {
        //public long ID { get; set; }
        public ICotizacion Cotizacion { get; private set; }

        public void SetUltimaCotizacion(ICotizacion ultimaCotizacion)
        {
            Cotizacion = ultimaCotizacion;
        }
    }
}

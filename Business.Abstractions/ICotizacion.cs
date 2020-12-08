using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstractions
{
    public interface ICotizacion
    {
        public DateTime FechaHora { get; set; }
        public decimal Ultima { get; set; }
    }
}

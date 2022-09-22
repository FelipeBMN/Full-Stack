using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Realiza a conexão entre palestrante e eventos
namespace dotnetWebApi.Domain
{
    public class PalestranteEvent
    {
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MinCurriculo { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvent> PalestrantesEvents { get; set; }
    }
}
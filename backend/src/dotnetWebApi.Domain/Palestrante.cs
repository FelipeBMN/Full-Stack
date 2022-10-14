using System;
using System.Collections.Generic;
using dotnetWebApi.Domain.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string MinCurriculo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvent> PalestrantesEvents { get; set; }
    }
}
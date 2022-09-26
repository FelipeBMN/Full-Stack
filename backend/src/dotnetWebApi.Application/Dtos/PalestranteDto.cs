using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Application.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MinCurriculo { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEvents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnetWebApi.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Domain
{
    // Alterando nome das variáveis no banco de dados.
    //[Table("EventosDetalhes")]
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvent { get; set; }
        public string Tema { get; set; }
        public int Quantidade { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone {get; set;}
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvent> PalestrantesEvents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetWebApi.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="O {0} deve ser valido.")]
        public string Local { get; set; }

        [Display(Name = "Data do Evento")]
        [Required(ErrorMessage="O {0} deve ser valido.")]
        public string DataEvent { get; set; }

        [Required(ErrorMessage="O {0} deve ser valido.")]
        //[MinLength(3)]
        //[MaxLength(16)]
        [StringLength(16, MinimumLength = 3)]
        public string Tema { get; set; }

        [Required(ErrorMessage="O {0} deve ser valido.")]
        [Range(3,10000000,ErrorMessage = "A quantidade de pessoas esta invalida")]
        public int Quantidade { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|html|bmp|png)$",ErrorMessage = " Não é uma imagem valida (gif,jpg,jpeg,bmp ou png) ")]
        public string ImagemURL { get; set; }

        [Required]
        [Phone(ErrorMessage="O campo {0} está invalido")]
        public string Telefone { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage="O {0} deve ser valido.")]
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEvents { get; set; }
    }
}

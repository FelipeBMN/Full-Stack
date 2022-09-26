using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetWebApi.Application.Dtos;
using dotnetWebApi.Domain;

namespace dotnetWebApi.API.Helpers
{
    public class DotnetWebApiProfile: Profile
    {
        public DotnetWebApiProfile(){
            CreateMap<Event, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
        }
    }
}
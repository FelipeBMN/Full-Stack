using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Application.Dtos;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contratos;

namespace dotnetWebApi.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist geralPersist;
        private readonly ILotePersist lotePersist;
        private readonly IMapper mapper;

        public LoteService(IGeralPersist geralPersist,
                            ILotePersist lotePersist,
                            IMapper mapper)
        {
            this.lotePersist = lotePersist;
            this.mapper = mapper;
            this.geralPersist = geralPersist;
        }

        public async Task<LoteDto> AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = this.mapper.Map<Lote>(model);

                lote.EventId = eventoId;

                this.geralPersist.Add<Lote>(lote);

                await this.geralPersist.SaveChangesAsync();

                // Caso queira indicar qual o lote adicionado
                var retorno = await this.lotePersist.GetLoteByIdAsync(model.EventId, lote.Id);

                return this.mapper.Map<LoteDto>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await this.lotePersist.GetAllLotesByEventIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);

                        model.EventId = eventoId;

                        this.mapper.Map(model, lote);

                        this.geralPersist.Update<Lote>(lote);

                        await this.geralPersist.SaveChangesAsync();
                    }
                }
                // Caso queira indicar qual o evento adicionado
                var lotesRetorno = await this.lotePersist.GetAllLotesByEventIdAsync(eventoId);

                return this.mapper.Map<LoteDto[]>(lotesRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await this.lotePersist.GetLoteByIdAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete nâo encontrado.");

                this.geralPersist.Delete<Lote>(lote);
                return await this.geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdAsync(int eventId, int loteId)
        {
            try
            {
                var lote = await this.lotePersist.GetLoteByIdAsync(eventId, loteId);
                if (lote == null) return null;


                var resultado = this.mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public async Task<LoteDto[]> GetAllLotesByEventIdAsync(int eventoId)
        {
            try
            {
                var lotes = await this.lotePersist.GetAllLotesByEventIdAsync(eventoId);
                if (lotes == null) return null;

                var resultados = this.mapper.Map<LoteDto[]>(lotes);

                return resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
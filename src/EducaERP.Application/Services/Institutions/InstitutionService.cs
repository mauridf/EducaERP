using EducaERP.Application.DTOs.Institutions;
using EducaERP.Application.DTOs.Institutions.Responses;
using EducaERP.Application.Interfaces.Institutions;
using EducaERP.Core.Domain.Institutions;
using EducaERP.Core.Exceptions;

namespace EducaERP.Application.Services.Institutions
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _repository;

        public InstitutionService(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public async Task<InstitutionResponse> Create(InstitutionDTO dto)
        {
            var institution = new Institution(
                dto.Nome, dto.Cnpj, dto.Email, dto.Telefone,
                dto.Endereco, dto.Cidade, dto.Uf);

            await _repository.AddAsync(institution);

            return MapToResponse(institution);
        }

        public async Task Delete(Guid id)
        {
            var institution = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Instituição não encontrada");

            await _repository.DeleteAsync(institution);
        }

        public async Task<IEnumerable<InstitutionResponse>> GetAll()
        {
            var institutions = await _repository.GetAllAsync();
            return institutions.Select(MapToResponse);
        }

        public async Task<InstitutionResponse> GetById(Guid id)
        {
            var institution = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Instituição não encontrada");

            return MapToResponse(institution);
        }

        public async Task<InstitutionResponse> Update(Guid id, InstitutionDTO dto)
        {
            var institution = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Instituição não encontrada");

            institution.Update(
                dto.Nome, dto.Email, dto.Telefone,
                dto.Endereco, dto.Cidade, dto.Uf);

            await _repository.UpdateAsync(institution);

            return MapToResponse(institution);
        }

        private InstitutionResponse MapToResponse(Institution institution)
        {
            return new InstitutionResponse
            {
                Id = institution.Id,
                Nome = institution.Nome,
                Cnpj = institution.Cnpj,
                Email = institution.Email,
                Telefone = institution.Telefone,
                Endereco = institution.Endereco,
                Cidade = institution.Cidade,
                Uf = institution.Uf,
                DataCriacao = institution.DataCriacao,
                DataAtualizacao = institution.DataAtualizacao
            };
        }
    }
}
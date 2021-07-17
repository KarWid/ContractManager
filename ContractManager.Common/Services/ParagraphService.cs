﻿namespace ContractManager.Common.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using ContractManager.Common.Models;
    using ContractManager.Repository.Entities;
    using ContractManager.Repository.Repositories;

    public interface IParagraphService
    {
        Task<Paragraph> GetAsync(string id);
        Task<Paragraph> AddAsync(Paragraph model);
        Task<Paragraph> EditAsync(Paragraph model);
        Task<bool> DeleteAsync(string id);
        Task<List<Paragraph>> GetListAsync();
    }
    public class ParagraphService : IParagraphService
    {
        private readonly IRepository<ParagraphEntity> _paragraphRepository;
        private readonly IMapper _mapper;

        public ParagraphService(IRepository<ParagraphEntity> paragraphRepository, IMapper mapper)
        {
            _paragraphRepository = paragraphRepository;
            _mapper = mapper;
        }

        public async Task<Paragraph> GetAsync(string id)
        {
            var entity = await _paragraphRepository.GetAsync(id);
            return _mapper.Map<Paragraph>(entity);
        }

        public async Task<Paragraph> AddAsync(Paragraph model)
        {
            var newEntity = _mapper.Map<ParagraphEntity>(model);
            await _paragraphRepository.AddAsync(newEntity);
            
            return _mapper.Map<Paragraph>(newEntity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _paragraphRepository.DeleteAsync(id);
        }

        public async Task<Paragraph> EditAsync(Paragraph model)
        {
            var updatedEntity = _mapper.Map<ParagraphEntity>(model);
            var result = await _paragraphRepository.ReplaceAsync(updatedEntity);

            // TODO @KWidla: some validation

            return _mapper.Map<Paragraph>(updatedEntity);
        }

        public async Task<List<Paragraph>> GetListAsync()
        {
            var list = await _paragraphRepository.GetListAsync();
            return _mapper.Map<List<Paragraph>>(list);
        }
    }
}
namespace ContractManager.Common.Mapper
{
    using AutoMapper;
    using ContractManager.Common.Models;
    using ContractManager.Repository.Entities;

    public class ParagraphProfile : Profile
    {
        public ParagraphProfile()
        {
            CreateMap<ParagraphEntity, Paragraph>()
                .ReverseMap()
                .ForMember(_ => _.CreatedAt, _ => _.Ignore());
        }
    }
}

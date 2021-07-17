namespace ContractManager.Mapper
{
    using AutoMapper;
    using ContractManager.Common.Models;
    using ContractManager.ViewModels.Paragraphs;

    public class ParagraphProfile2 : Profile
    {
        public ParagraphProfile2()
        {
            CreateMap<Paragraph, ParagraphVM>()
                .ReverseMap()
                .ForMember(_ => _.CreatedAt, _ => _.Ignore());
        }
    }
}

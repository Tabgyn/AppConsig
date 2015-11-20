using AppConsig.Entities;
using AppConsig.Web.Gestor.Models;
using AutoMapper;

namespace AppConsig.Web.Gestor.Mapping
{
    public class ConsignacaoMapping : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Consignacao, ConsignacaoListModel>()
                .ForMember(dest => dest.Consignataria,
                    opt => opt.MapFrom(src => src.Consignataria.Nome))
                .ForMember(dest => dest.Servico,
                    opt => opt.MapFrom(src => src.Servico.Nome));
            Mapper.CreateMap<Consignacao, ConsignacaoEditModel>();
            Mapper.CreateMap<ConsignacaoEditModel, Consignacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AtualizadoEm, opt => opt.Ignore())
                .ForMember(dest => dest.AtualizadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.CriadoEm, opt => opt.Ignore())
                .ForMember(dest => dest.CriadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.Excluido, opt => opt.Ignore())
                .ForMember(dest => dest.Consignataria, opt => opt.Ignore())
                .ForMember(dest => dest.Servico, opt => opt.Ignore());
        }
    }
}
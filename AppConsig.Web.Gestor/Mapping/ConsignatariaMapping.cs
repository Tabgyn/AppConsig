using System.Text.RegularExpressions;
using AppConsig.Entities;
using AppConsig.Web.Gestor.Models;
using AutoMapper;

namespace AppConsig.Web.Gestor.Mapping
{
    public class ConsignatariaMapping : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Consignataria, ConsignatariaListModel>()
                .ForMember(dest => dest.CNPJ,
                    opt => opt.MapFrom(src => $@"{long.Parse(src.CNPJ):00\.000\.000\/0000\-00}"));
            Mapper.CreateMap<Consignataria, ConsignatariaEditModel>()
                .ForMember(dest => dest.CNPJ,
                    opt => opt.MapFrom(src => $@"{long.Parse(src.CNPJ):00\.000\.000\/0000\-00}"));
            Mapper.CreateMap<ConsignatariaEditModel, Consignataria>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AtualizadoEm, opt => opt.Ignore())
                .ForMember(dest => dest.AtualizadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.CriadoEm, opt => opt.Ignore())
                .ForMember(dest => dest.CriadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.Excluido, opt => opt.Ignore())
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => new Regex(@"\s|[./-]").Replace(src.CNPJ, "")));
        }
    }
}
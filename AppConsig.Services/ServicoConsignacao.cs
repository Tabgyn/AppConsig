using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoConsignacao : ServicoBasico<Consignacao>, IServicoConsignacao
    {
        public ServicoConsignacao(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Consignacao>();
        }

        public override void Criar(Consignacao entity)
        {
            var consignataria = Context.Consignatarias.Find(entity.ConsignatariaId);
            var servico = Context.Servicos.Find(entity.ServicoId);

            entity.Consignataria = consignataria;
            entity.Servico = servico;

            base.Criar(entity);
        }

        public override void Atualizar(Consignacao entity)
        {
            var consignataria = Context.Consignatarias.Find(entity.ConsignatariaId);
            var servico = Context.Servicos.Find(entity.ServicoId);

            entity.Consignataria = consignataria;
            entity.Servico = servico;

            base.Atualizar(entity);
        }
    }
}
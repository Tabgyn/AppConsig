using System;
using System.Data.Entity.Migrations;
using AppConsig.Common.Enums;
using AppConsig.Entities;

namespace AppConsig.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppContext context)
        {
            //var rnd = new Random();
            //// Add avisos
            //for (var i = 0; i < 10; i++)
            //{
            //    var aviso = new Aviso
            //    {
            //        Texto = "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto."
            //    };

            //    context.Avisos.Add(aviso);
            //}

            //context.SaveChanges();

            //// Add sistemas de folha
            //for (var i = 0; i < 10; i++)
            //{
            //    var sistema = new SistemaFolha
            //    {
            //        Nome = $"Sistema {i}"
            //    };

            //    context.SistemasFolha.Add(sistema);
            //}

            //context.SaveChanges();

            //// Add departamentos
            //for (var i = 0; i < 10; i++)
            //{
            //    var departamento = new Departamento
            //    {
            //        CodigoDepartamento = i.ToString(),
            //        Nome = $"Departamento {i}",
            //        Descricao = $"Departamento {i}",
            //        SistemaFolha = context.SistemasFolha.Find(rnd.Next(1,11))
            //    };

            //    context.Departamentos.Add(departamento);
            //}

            //context.SaveChanges();

            //// Add servidores
            //for (var i = 0; i < 100; i++)
            //{
            //    var servidor = new Servidor
            //    {
            //        Nome = $"Servidor {i}",
            //        Departamento = context.Departamentos.Find(rnd.Next(1,11)),
            //        AdmitidoEm = DateTime.Now.AddMonths(-i).AddDays(-i),
            //        CPF = "12345678912",
            //        Matricula = i.ToString(),
            //        NascidoEm = DateTime.Now.AddMonths(-(i+7)).AddDays(-(i+4))
            //    };

            //    context.Servidores.Add(servidor);
            //}

            //context.SaveChanges();

            //// Add servicos
            //for (var i = 0; i < 5; i++)
            //{
            //    var servico = new Servico
            //    {
            //        Nome = $"Serviço {i}",
            //        Descricao = $"Serviço {i}",
            //        Ordem = i+1,
            //        TipoServicoInerente = TipoServicoInerente.Emprestimo,
            //        TipoServicoRelacao = TipoServicoRelacao.Efetivo
            //    };

            //    context.Servicos.Add(servico);
            //}

            //context.SaveChanges();

            //// Add perfis
            //for (var i = 0; i < 5; i++)
            //{
            //    var perfil = new Perfil
            //    {
            //        Nome = $"Perfil {i}",
            //        Descricao = $"Perfil {i}",
            //        EhEditavel = true,
            //    };

            //    context.Perfis.Add(perfil);
            //}

            //context.SaveChanges();

            //// Add consignatarias
            //for (var i = 0; i < 5; i++)
            //{
            //    var consignataria = new Consignataria
            //    {
            //        Codigo = i.ToString(),
            //        Sigla = i.ToString(),
            //        Nome = $"Consignataria {i}",
            //        CNPJ = "12345678901234",
            //        Email = $"Consignataria {i}",
            //        TipoRepresentante = TipoRepresentante.Matriz
            //    };

            //    context.Consignatarias.Add(consignataria);
            //}

            //context.SaveChanges();

            //// Add consignacoes
            //for (var i = 0; i < 5; i++)
            //{
            //    var consignacao = new Consignacao
            //    {
            //        Codigo = i.ToString(),
            //        Nome = $"Consignação {i}",
            //        Descricao = $"Consignação {i}",
            //        Servico = context.Servicos.Find(i+1),
            //        InicioDaVigenciaEm = DateTime.Now.AddYears(-i),
            //        MaximoParcela = 60,
            //        PermiteDescontoParcial = false,
            //        PermiteLancamentoManual = false,
            //        PermiteOutrasOcorrencias = true,
            //        ValorMinimo = 1,
            //        Consignataria = context.Consignatarias.Find(rnd.Next(1,6))
            //    };

            //    context.Consignacoes.Add(consignacao);
            //}

            //context.SaveChanges();
        }
    }
}

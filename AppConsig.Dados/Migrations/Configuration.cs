using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AppConsig.Comum;
using AppConsig.Entidades;

namespace AppConsig.Dados.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppContexto context)
        {
            //var listaPermissoesPai = new List<Permissao>
            //{
            //    new Permissao
            //    {
            //        Nome = "Administrativo",
            //        Descricao = "",
            //        Url = "#",
            //        Action = "",
            //        Controller = "",
            //        ParenteId = 0,
            //        UrlImagem = "fa fa-cogs",
            //        Ordem = 1
            //    }
            //};

            //foreach (var perm in listaPermissoesPai)
            //{
            //    context.Permissoes.Add(perm);
            //}

            //context.SaveChanges();

            //var idAdministrativo = context.Permissoes.First(p => p.Nome == "Administrativo").Id;

            //var lsitaPermissoesFilho = new List<Permissao>
            //{
            //    new Permissao
            //    {
            //        Nome = "Acessos",
            //        Descricao = "Controle de acessos de usuários",
            //        Url = "",
            //        Action = "Index",
            //        Controller = "Acesso",
            //        ParenteId = idAdministrativo,
            //        UrlImagem = "",
            //        Ordem = 1
            //    },
            //    new Permissao
            //    {
            //        Nome = "Avisos",
            //        Descricao = "Avisos e notícias",
            //        Action = "Index",
            //        Controller = "Aviso",
            //        ParenteId = idAdministrativo,
            //        UrlImagem = "",
            //        Ordem = 2
            //    },
            //    new Permissao
            //    {
            //        Nome = "Perfis",
            //        Descricao = "Controle de perfis de usuários",
            //        Action = "Index",
            //        Controller = "Perfil",
            //        ParenteId = idAdministrativo,
            //        UrlImagem = "",
            //        Ordem = 3
            //    }
            //};
            
            //foreach (var perm in lsitaPermissoesFilho)
            //{
            //    context.Permissoes.Add(perm);
            //}

            //var permissoes = context.Permissoes.ToList();

            //var perfil = new Perfil()
            //{
            //    Nome = "Master",
            //    Descricao = "Perfil master dos usuários",
            //    Permissoes = permissoes
            //};

            //context.Perfil.Add(perfil);

            //var usuario = new Usuario
            //{
            //    Nome = "Tiago",
            //    Sobrenome = "Azevedo Borges",
            //    Login = "admin@appconsig.com.br",
            //    Senha = PasswordHash.CriarCriptografia("123"),
            //    Email = "admin@appconsig.com.br",
            //    Perfil = context.Perfil.Find(1)
            //};

            //context.Usuarios.Add(usuario);

            //var aviso = new Aviso()
            //{
            //    Texto =
            //        "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto."
            //};

            //context.Avisos.Add(aviso);

            //base.Seed(context);
        }
    }
}

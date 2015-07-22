using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            //var listaPermissoes = new List<Permissao>
            //                      {
            //                          new Permissao()
            //                          {
            //                              Nome = "Visão Geral",
            //                              Descricao = "Dados e estatísticas",
            //                              Action = "Index",
            //                              Controller = "VisaoGeral"
            //                          },
            //                          new Permissao()
            //                          {
            //                              Nome = "Acessos",
            //                              Descricao = "Controle de acessos de usuários",
            //                              Action = "Index",
            //                              Controller = "Acesso"
            //                          },
            //                          new Permissao()
            //                          {
            //                              Nome = "Avisos",
            //                              Descricao = "Avisos e notícias",
            //                              Action = "Index",
            //                              Controller = "Aviso"
            //                          },
            //                          new Permissao()
            //                          {
            //                              Nome = "Perfis",
            //                              Descricao = "Controle de perfis de usuários",
            //                              Action = "Index",
            //                              Controller = "Perfil"
            //                          }
            //                      };

            //foreach (var perm in listaPermissoes)
            //{
            //    context.Permissoes.Add(perm);
            //}

            //var perfil = new Perfil()
            //{
            //    Nome = "Master",
            //    Descricao = "Perfil master dos usuários",
            //    Permissoes = listaPermissoes
            //};

            //context.Perfil.Add(perfil);

            //var usuario = new Usuario()
            //{
            //    Login = "admin",
            //    Senha = "123",
            //    Email = "admin@appconsig.com.br",
            //    Perfil = perfil
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

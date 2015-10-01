﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Comum;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoUsuario : ServicoEntidade<Usuario>, IServicoUsuario
    {
        public ServicoUsuario(IContexto contexto)
            : base(contexto)
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Usuario>();
        }

        public override void Criar(Usuario usuario)
        {
            // Gera uma nova senha.
            var strSenha = StringHelper.ObterTextoAleatorio();
            // Criptografa a senha.
            usuario.Senha = HashHelper.HashPbkdf2(strSenha);

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                De = "",
                Para = usuario.Email,
                ComCopia = null,
                Assunto = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Corpo = email.CorpoSenha(usuario.Nome, usuario.Sobrenome, strSenha);
            email.Send();

            base.Criar(usuario);
        }

        public bool ValidarUsuario(string email, string senha)
        {
            var usuario = Dbset.FirstOrDefault(x => x.Email == email);

            return usuario != null && HashHelper.ValidarHash(senha, usuario.Senha);
        }

        public void ReeviarSenha(Usuario usuario)
        {
            // Gera uma nova senha.
            var strSenha = StringHelper.ObterTextoAleatorio();
            // Criptografa a senha.
            usuario.Senha = HashHelper.HashPbkdf2(strSenha);

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                De = "",
                Para = usuario.Email,
                ComCopia = null,
                Assunto = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Corpo = email.CorpoSenha(usuario.Nome, usuario.Sobrenome, strSenha);
            email.Send();

            Atualizar(usuario);
        }

        public List<Permissao> ObterPermissoesDoUsuario(Guid usuarioId)
        {
            var usuario = Dbset.Find(usuarioId);
            var permissoes =
                Contexto.Perfis.Where(p => p.Id == usuario.PerfilId)
                    .Include(p => p.Permissoes)
                    .First()
                    .Permissoes.ToList();

            return permissoes;
        }
    }
}
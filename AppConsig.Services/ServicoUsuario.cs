using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppConsig.Common;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoUsuario : ServicoBasico<Usuario>, IServicoUsuario
    {
        public ServicoUsuario(IContext context)
            : base(context)
        {
            Context = context;
            Dbset = Context.Set<Usuario>();
        }

        public override void Criar(Usuario usuario)
        {
            // Gera uma nova senha.
            var strPassword = StringHelper.GetRandomText();
            // Criptografa a senha.
            usuario.Senha = HashHelper.CreateHash(strPassword);
            // Por padrao o nome de usuario sera o mesmo do email
            usuario.NomeDeUsuario = usuario.Email;

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                From = "",
                To = usuario.Email,
                CopyTo = null,
                Subject = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Body = email.PasswordBody(usuario.Nome, usuario.Sobrenome, strPassword);
            email.Send();

            base.Criar(usuario);
        }

        public override async Task CriarAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            // Gera uma nova senha.
            var strPassword = StringHelper.GetRandomText();
            // Criptografa a senha.
            usuario.Senha = HashHelper.CreateHash(strPassword);
            // Por padrao o nome de usuario sera o mesmo do email
            usuario.NomeDeUsuario = usuario.Email;

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                From = "",
                To = usuario.Email,
                CopyTo = null,
                Subject = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Body = email.PasswordBody(usuario.Nome, usuario.Sobrenome, strPassword);
            await email.SendAsync();

            await base.CriarAsync(usuario, cancellationToken);
        }

        public bool ValidarUsuario(string nomeDeUsuario, string senha)
        {
            var usuario = Dbset.FirstOrDefault(x => x.NomeDeUsuario == nomeDeUsuario);

            if (usuario == null) return false;

            var validado = HashHelper.ValidateHash(senha, usuario.Senha);
            usuario.UltimoAcesso = DateTime.Now;

            Atualizar(usuario);

            return validado;
        }

        public void ResetarSenha(Usuario usuario)
        {
            // Gera uma nova senha.
            var strPassword = StringHelper.GetRandomText();
            // Criptografa a senha.
            usuario.Senha = HashHelper.CreateHash(strPassword);

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                From = "",
                To = usuario.Email,
                CopyTo = null,
                Subject = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Body = email.PasswordBody(usuario.Nome, usuario.Sobrenome, strPassword);
            email.Send();

            Atualizar(usuario);
        }

        public List<Permissao> ObterPermissoesDoUsuario(long usuarioId)
        {
            var user = Dbset.Find(usuarioId);

            return Context.Perfis.Where(p => p.Id == user.PerfilId)
                    .Include(p => p.Permissoes)
                    .First()
                    .Permissoes.ToList();
        }
    }
}
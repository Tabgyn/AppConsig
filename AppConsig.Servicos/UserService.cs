using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Common;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        public UserService(IContext context)
            : base(context)
        {
            Context = context;
            Dbset = Context.Set<User>();
        }

        public override void Insert(User user)
        {
            // Gera uma nova senha.
            var strPassword = StringHelper.GetRandomText();
            // Criptografa a senha.
            user.Password = HashHelper.CreateHash(strPassword);

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                From = "",
                To = user.Email,
                CopyTo = null,
                Subject = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Body = email.PasswordBody(user.Name, user.Surname, strPassword);
            email.Send();

            base.Insert(user);
        }

        public bool ValidateUser(string login, string password)
        {
            var user = Dbset.FirstOrDefault(x => x.Email == login);

            return user != null && HashHelper.ValidatePassword(password, user.Password);
        }

        public void ResetPassword(User user)
        {
            // Gera uma nova senha.
            var strPassword = StringHelper.GetRandomText();
            // Criptografa a senha.
            user.Password = HashHelper.CreateHash(strPassword);

            // Envia um e-mail ao usuário informando sua senha.
            var email = new EmailHelper
            {
                From = "",
                To = user.Email,
                CopyTo = null,
                Subject = "AppConsig - Senha de acesso"
            };

            //Obtem corpo formatado para senha
            email.Body = email.PasswordBody(user.Name, user.Surname, strPassword);
            email.Send();

            Update(user);
        }

        public List<Permission> GetPermissionsFromUser(long userId)
        {
            var user = Dbset.Find(userId);
            var permissons =
                Context.Profiles.Where(p => p.Id == user.ProfileId)
                    .Include(p => p.Permissions)
                    .First()
                    .Permissions.ToList();

            return permissons;
        }
    }
}
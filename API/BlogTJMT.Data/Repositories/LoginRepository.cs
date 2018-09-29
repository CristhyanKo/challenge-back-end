﻿using System;
using BlogTJMT.Common.Security;
using BlogTJMT.Common.Validations;
using BlogTJMT.Data.DataContexts;
using BlogTJMT.Domain.Contract.Repositories;
using BlogTJMT.Domain.Model;
using System.Linq;

namespace BlogTJMT.Data.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private BlogTJMTDataContext _db = new BlogTJMTDataContext();

        public LoginRepository(BlogTJMTDataContext context)
        {
            _db = context;
        }

        public void Dispose() => _db.Dispose();

        public Usuario AutenticaUsuario(Login login)
        {
            ValidationClass.ValidaClasse(login);
            login.Senha.Encrypta();
            return ValidaUsuario(ProcuraUsuario(login));
        }

        private Usuario ProcuraUsuario(Login login)
        {
            return (from item in _db.Usuarios
                    where item.Email.ToLower() == (login.Email.ToLower()) && item.Senha == (login.Senha)
                    select item).FirstOrDefault();
        }

        private Usuario ValidaUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            return usuario;
        }
    }
}

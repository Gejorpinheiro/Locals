using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BO
{
    public class UsuarioBO
    {
        public UsuarioDTO AutenticaUsuario(string email, string senha)
        {
            return new UsuarioDAO().AutenticaUsuario(email, senha);
        }

        public UsuarioDTO GetUsuarioById(int id_usuario)
        {
            return new UsuarioDAO().getUsuarioById(id_usuario);
        }

        public int SalvarUsuario(UsuarioDTO usuario)
        {
            if(usuario.Id_usuario > 0)
            {
                if (new UsuarioDAO().UpdateUsuario(usuario))
                    return usuario.Id_usuario;
                else
                    return 0;
                
            }
            else
            {
                int email_existente = new UsuarioDAO().VerificaUsuarioExistente(usuario.Email);

                if (email_existente > 0)
                    return 0;
                else
                    return new UsuarioDAO().SalvarUsuario(usuario);
            }
        }
    }
}
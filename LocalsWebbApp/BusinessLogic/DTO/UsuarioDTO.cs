using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO
{
    public class UsuarioDTO
    {
        public int Id_usuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Imagem { get; set; }  
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO
{
    public class ComentarioDTO
    {
        public int Id_comentario { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int Id_publicacao { get; set; }
        public int Id_usuario { get; set; }

        public string Avatar { get; set; }
        public string Nome_usuario { get; set; }
        public string Data_formatada
        {
            get
            {
                return string.Format("{0:HH:mm - dd/MM/yyyy}", Data);
            }
        }
    }
}
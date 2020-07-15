using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO
{
    public class PublicacaoDTO
    {
        public int Id_publicacao { get; set; }
        public int Id_usuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public DateTime Data_publicacao { get; set; }
        public bool Status { get; set; }
        public string Imagem { get; set; }
        public string Localizacao { get; set; }
        public int Likes { get; set; }

        public string Nome_usuario { get; set; }
        public string Imagem_usuario { get; set; }

        public string Data_publicacao_formatada
        {
            get
            {
                return string.Format("{0:HH:mm - dd/MM/yyyy}", Data_publicacao);
            }
        }
    }
}
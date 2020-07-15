using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BO
{
    public class ComentarioBO
    {
        public List<ComentarioDTO> GetComentariosByPublicacao(int id_publicacao)
        {
            try
            {
                return new ComentarioDAO().GetComentariosByPublicacao(id_publicacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SalvarComentario(ComentarioDTO comentario)
        {
            try
            {
                return new ComentarioDAO().SalvarComentario(comentario);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void ExcluirComentario(int id_comentario)
        {
            try
            {
                new ComentarioDAO().ExcluirComentario(id_comentario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
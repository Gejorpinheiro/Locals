using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BO
{
    public class PublicacaoBO
    {
        public int SalvarPublicacao(PublicacaoDTO publicacao)
        {
            try
            {
                if (publicacao.Id_publicacao == 0)
                    return new PublicacaoDAO().SalvarPublicacao(publicacao);
                else
                    return 99;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PublicacaoDTO> GetAllPublicacoes()
        {
            try
            {
                return new PublicacaoDAO().GetAllPublicacoes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PublicacaoDTO GetPublicacaoById(int id_publicacao)
        {
            try
            {
                return new PublicacaoDAO().GetPublicacaoById(id_publicacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PublicacaoDTO> GetPublicacoesByUsuario(int id_usuario)
        {
            try
            {
                return new PublicacaoDAO().GetPublicacoesByUsuario(id_usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PublicacaoDTO> GetPublicacoesByCidade(string cidade)
        {
            try
            {
                return new PublicacaoDAO().GetPublicacoesByCidade(cidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLike(int id_publicacao, int likes)
        {
            try
            {
                new PublicacaoDAO().UpdateLike(id_publicacao, likes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
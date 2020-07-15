using BO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Feed : Page
    {
        public UsuarioDTO Usuario
        {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Usuario == null)
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack)
            {
                CarregaFeed();
            }

            hdnIdUsuario.Value = Usuario.Id_usuario.ToString();
        }

        public void CarregaFeed()
        {
            try
            {
                List<PublicacaoDTO> publicacoes = new PublicacaoBO().GetAllPublicacoes();

                if(publicacoes.Count > 0)
                {
                    rptPublicacao.DataSource = publicacoes;
                    rptPublicacao.DataBind();
                }
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }

        [WebMethod]
        public static List<ComentarioDTO> VerComentarios(int id_publicacao)
        {
            try
            {
                return new ComentarioBO().GetComentariosByPublicacao(id_publicacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static ComentarioDTO SalvarComentario(ComentarioDTO comentario)
        {
            try
            {
                UsuarioDTO usuario = (UsuarioDTO)HttpContext.Current.Session["Usuario"];

                comentario.Descricao = comentario.Descricao;
                comentario.Data = DateTime.Now;
                comentario.Id_usuario = usuario.Id_usuario;
                comentario.Id_publicacao = comentario.Id_publicacao;
                comentario.Avatar = usuario.Imagem;
                comentario.Nome_usuario = usuario.Nome;

                comentario.Id_comentario = new ComentarioBO().SalvarComentario(comentario);

                return comentario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static void ExcluirComentario(int id_comentario)
        {
            try
            {
                new ComentarioBO().ExcluirComentario(id_comentario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static void UpdateLike(int id_publicacao, int likes)
        {
            try
            {
                new PublicacaoBO().UpdateLike(id_publicacao, likes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static List<PublicacaoDTO> Pesquisar(string cidade)
        {
            try
            {
                return new PublicacaoBO().GetPublicacoesByCidade(cidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
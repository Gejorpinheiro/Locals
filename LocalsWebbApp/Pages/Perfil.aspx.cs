using BO;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Perfil : Page
    {
        public UsuarioDTO Usuario {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }

        public int Id_usuario { 
            get
            {
                if (Request.QueryString["id_usuario"] != null && Convert.ToInt32(Request.QueryString["id_usuario"]) > 0)
                    return Convert.ToInt32(Request.QueryString["id_usuario"]);
                else
                    return Usuario.Id_usuario;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Usuario != null)
            {
                if (!IsPostBack)
                {
                    UsuarioDTO retorno = new UsuarioDTO();

                    hdnIdUsuario.Value = Usuario.Id_usuario.ToString();
                    retorno = getDadosUsuario(Id_usuario);

                    if(retorno.Id_usuario > 0)
                    {
                        lblNome.Text = retorno.Nome.ToLower();
                        lblEmail.Text = retorno.Email.ToLower();
                        lblCidade.Text = retorno.Cidade.ToLower();
                        lblEstado.Text = retorno.Estado;

                        if (!string.IsNullOrEmpty(retorno.Imagem))
                            imgUsuario.Style.Add("background-image", "'../assets/imgs/Avatar/" + retorno.Imagem + "'");
                        else
                            imgUsuario.Style.Add("background-image", "'../assets/imgs/Avatar/person.png'");

                        CarregaPublicacoes(Id_usuario);

                        if(Usuario.Id_usuario != Id_usuario)
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "disableEdit", "disableEdit();", true);
                    }
                    else
                    {
                        Response.Redirect("NotFound.aspx");
                    }
                }

            }
            else
                Response.Redirect("Login.aspx");
        }

        public UsuarioDTO getDadosUsuario(int id_usuario)
        {
            return new UsuarioBO().GetUsuarioById(id_usuario);
        }

        [WebMethod]
        public static string updateUsuario(UsuarioDTO usuario)
        {
            UsuarioDTO Usuario = (UsuarioDTO)HttpContext.Current.Session["Usuario"];
            usuario.Imagem = Usuario.Imagem;

            int retorno = new UsuarioBO().SalvarUsuario(usuario);

            if (retorno > 0)
                return "OK";
            else
                return "NOK";
        }

        [WebMethod]
        public static PublicacaoDTO GetPublicacaoById(int id_publicacao)
        {
            try
            {
                return new PublicacaoBO().GetPublicacaoById(id_publicacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CarregaPublicacoes(int id_usuario)
        {
            List<PublicacaoDTO> list = new List<PublicacaoDTO>();

            try
            {
                list = new PublicacaoBO().GetPublicacoesByUsuario(id_usuario);

                if (list.Count > 1)
                    lblPublicacoes.Text = "Publicações";

                lblCountPublicacao.Text = list.Count.ToString();

                rptPublicacoes.DataSource = list;
                rptPublicacoes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Upload_file(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                if (CheckFileType(FileUpload.FileName))
                {
                    if (FileUpload.FileContent.Length < 6000000)
                    {
                        try
                        {
                            string fileName = string.Format("{0}_{1}", Usuario.Id_usuario, FileUpload.FileName.Replace(' ', '_'));
                            string path = Path.Combine(Server.MapPath("~/Assets/imgs/Avatar"), fileName);

                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                            FileUpload.SaveAs(path);
                            Usuario.Imagem = fileName;

                            new UsuarioBO().SalvarUsuario(Usuario);

                            imgUsuario.Style.Add("background-image", "'../assets/imgs/Avatar/" + Usuario.Imagem + "'");
                        }
                        catch (Exception ex)
                        {
                            phFileMessage.Visible = true;
                            lblFileMessage.Text = "Ocorreu o seguinte erro: " + ex.Message;
                        }

                    }
                }
                else
                {
                    phFileMessage.Visible = true;
                    lblFileMessage.Text = "Ocorreu o seguinte erro: Formato de arquivo inválido.";
                }
            }
        }

        public bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);

            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                default:
                    return false;
            }
        }
    }
}
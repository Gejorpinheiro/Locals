using BO;
using DTO;
using System;
using System.IO;
using System.Web.UI;

namespace LocalsWebbApp.Pages
{
    public partial class Publicacao : Page
    {
        public UsuarioDTO usuario
        {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (usuario != null)
                {

                }
                else
                    Response.Redirect("Login.aspx");
            }
            else
            {
                Publicar();
            }
        }

        public void Publicar()
        {
            if (FileUpload.HasFile)
            {
                if (CheckFileType(FileUpload.FileName))
                {
                    if (FileUpload.FileContent.Length < 6000000)
                    {
                        try
                        {
                            PublicacaoDTO publicacao = new PublicacaoDTO();
                            publicacao.Id_usuario = usuario.Id_usuario;
                            publicacao.Titulo = txtTitulo.Value;
                            publicacao.Descricao = txtDescricao.Value;
                            publicacao.Cidade = txtCidade.Value;
                            publicacao.Estado = ddlEstado.Value;
                            publicacao.Data_publicacao = DateTime.Now;
                            publicacao.Localizacao = txtCords.Value;

                            string fileName = string.Format("{0}_{1}", usuario.Id_usuario, FileUpload.FileName.Replace(' ', '_'));
                            publicacao.Imagem = fileName;

                            new PublicacaoBO().SalvarPublicacao(publicacao);

                            string path = Path.Combine(Server.MapPath("~/Assets/imgs/Publicacoes"), fileName);

                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                            FileUpload.SaveAs(path);

                            string message = string.Format("showMensagem('{0}', '{1}')", "success", "Publicação criada com sucesso.");
                            Page.ClientScript.RegisterStartupScript(GetType(), "alert", message, true);
                        }
                        catch (Exception ex)
                        {
                            string message = string.Format("showMensagem('{0}', '{1}')", "danger", "Ocorreu o seguinte erro: " + ex.Message);
                            Page.ClientScript.RegisterStartupScript(GetType(), "alert", message, true);
                        }
                    }
                    else
                    {
                        string message = string.Format("showMensagem('{0}', '{1}')", "danger", "A imagem não pode conter mais do que 6 MB.");
                        Page.ClientScript.RegisterStartupScript(GetType(), "alert", message, true);
                    }
                }
                else
                {
                    string message = string.Format("showMensagem('{0}', '{1}')", "danger", "Ocorreu o seguinte erro: Formato de arquivo inválido.");
                    Page.ClientScript.RegisterStartupScript(GetType(), "alert", message, true);
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
using BO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["logout"] != null && Request.QueryString["logout"].ToString().Equals("true"))
                {
                    Session["Usuario"] = null;
                    Response.Redirect("Login.aspx");
                }

                hdnMenssagem.Visible = false;

                if (Session["Usuario"] != null)
                    Response.Redirect("Feed.aspx");
            }
            else
            {
                if(hdnOperacao.Value == "login")
                {
                    string email = txtEmail.Value;
                    string senha = HashPass(txtSenha.Value);

                    Autenticacao(email, senha);
                }
                else if(hdnOperacao.Value == "cadastro")
                {
                    UsuarioDTO usuario = new UsuarioDTO();

                    usuario.Id_usuario = 0;
                    usuario.Nome = txtNovoNome.Value;
                    usuario.Email = txtNovoEmail.Value;
                    usuario.Cidade = txtNovaCidade.Value;
                    usuario.Imagem = "person.png";
                    usuario.Senha = HashPass(txtNovaSenhaConfirma.Value);
                    usuario.Estado = ddlEstado.Value == "0" ? null : ddlEstado.Value.ToString();

                    usuario.Id_usuario = new UsuarioBO().SalvarUsuario(usuario);

                    if(usuario.Id_usuario > 0)
                    {
                        Session["Usuario"] = usuario;
                        Response.Redirect("Feed.aspx");
                    }
                    else
                    {
                        hdnMenssagem.Text = "Email já cadastrado";
                        hdnMenssagem.Visible = true;
                    }
                }
                
            }
        }

        protected void Autenticacao(string email, string senha)
        {
            UsuarioDTO usuario = new UsuarioBO().AutenticaUsuario(email, senha);

            if (usuario.Id_usuario > 0)
            {
                Session["Usuario"] = usuario;
                Response.Redirect("Feed.aspx");
            }
            else
            {
                hdnMenssagem.Text = "Email ou senha incorretos.";
                hdnMenssagem.Visible = true;
            }
        }

        public string HashPass(string senha)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.ASCII.GetBytes(senha));

            byte[] hash = md5.Hash;
            StringBuilder str = new StringBuilder();

            for(int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("x2"));
            }

            return str.ToString();
        }
    }
}
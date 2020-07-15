using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalsWebbApp.Pages
{
    public partial class Mapa : Page
    {
        public UsuarioDTO usuario
        {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }

        public string Cordenadas
        {
            get
            {
                return Request.QueryString["cord"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (usuario != null)
            {
                StringBuilder script = new StringBuilder();

                script.AppendLine("function adicionaPin(){");

                if (!string.IsNullOrEmpty(Cordenadas))
                {
                    string[] latLng = Cordenadas.Split('|');

                    script.AppendLine(@" 
                        var latLng = new google.maps.LatLng('" + latLng[0].ToString() + "','" + latLng[1].ToString() + "'); " +
                        "var marker = new google.maps.Marker({position: latLng, map: map}); " +
                        "map.setCenter(latLng);"
                    );
                }
                else
                {
                    List<PublicacaoDTO> publicacoes = GetPublicacoes();

                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
                     new System.Web.Script.Serialization.JavaScriptSerializer();

                    string sJSON = oSerializer.Serialize(publicacoes);

                    script.AppendLine("var json = " + sJSON + ";");

                    script.AppendLine(@"
                        json.map(function(e, i){
                            var latLng = e.Localizacao.split('|');

                            marker = new google.maps.Marker({
                                position: new google.maps.LatLng(latLng[0], latLng[1]),
                                map: map,
                                icon: '../Assets/Imgs/icons/ico_32.png'
                            });

                            google.maps.event.addListener(marker, 'click', (function(marker, i) {
                                return function() {
                                    infowindow.setContent(btn.replace('#idPublicacao', e.Id_publicacao));
                                    infowindow.open(map, marker);
                                    
                                    $('body').on('click', '.verPublicacao', function(){ loadModalPublicacao(e); })
                                }
                            })(marker, i));
                        });
                    ");
                }

                script.AppendLine("}");

                Page.ClientScript.RegisterStartupScript(this.GetType(), "getPublicacoes", script.ToString(), true);
            }
            else
                Response.Redirect("Login.aspx");
        }

        public List<PublicacaoDTO> GetPublicacoes()
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
    }
}
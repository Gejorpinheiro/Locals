<%@ Page Title="Publicacao" Language="C#" MasterPageFile="~/Pages/Root.Master" AutoEventWireup="true" CodeBehind="Publicacao.aspx.cs" Inherits="LocalsWebbApp.Pages.Publicacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="panel col-12 col-sm-10 col-md-7 col-lg-6">
            <div id="alerta" class="alert alert-danger alert-dismissible" role="alert">
                <span id="mensagem"></span>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
            </div>
            <div id="fase1" class="input-group mb-2">
                <div class="custom-file">
                    <asp:FileUpload ID="FileUpload" runat="server" CssClass="custom-file-input" ClientIDMode="Static" />
                    <label class="custom-file-label" for="UploadImagem">Selecionar arquivo</label>
                </div>
            </div>
            <div id="fase2">
                <span id="imgPublicacao"></span>
                <input id="txtTitulo" type="text" name="titulo" value="" runat="server" maxlength="45" class="form-control required" placeholder="Titulo"/>
                <div class="invalid-feedback">Campo Obrigatório.</div>
                <textarea id="txtDescricao" type="text" name="descricao" value="" runat="server" maxlength="145" class="form-control required" placeholder="Descrição"></textarea>
                <div class="invalid-feedback">Campo Obrigatório.</div>
                <input id="txtCidade" type="text" name="cidade" value="" runat="server" maxlength="95" class="form-control required" placeholder="Cidade"/>
                <div class="invalid-feedback">Campo Obrigatório.</div>
                <select id="ddlEstado" class="form-control required rounded-bottom" runat="server">
                    <option value="">Selecione o estado</option>
                    <option value="AC">Acre - AC</option>
                    <option value="AL">Alagoas - AL</option>
                    <option value="AP">Amapá - AP</option>
                    <option value="AM">Amazonas - AM</option>
                    <option value="BA">Bahia  - BA</option>
                    <option value="CE">Ceará - CE</option>
                    <option value="DF">Distrito Federal  - DF</option>
                    <option value="ES">Espírito Santo - ES</option>
                    <option value="GO">Goiás - GO</option>
                    <option value="MA">Maranhão - MA</option>
                    <option value="MT">Mato Grosso - MT</option>
                    <option value="MS">Mato Grosso do Sul - MS</option>
                    <option value="MG">Minas Gerais - MG</option>
                    <option value="PA">Pará - PA</option>
                    <option value="PB">Paraíba - PB</option>
                    <option value="PR">Paraná - PR</option>
                    <option value="PE">Pernambuco - PE</option>
                    <option value="PI">Piauí - PI</option>
                    <option value="RJ">Rio de Janeiro - RJ</option>
                    <option value="RN">Rio Grande do Norte - RN</option>
                    <option value="RS">Rio Grande do Sul - RS</option>
                    <option value="RO">Rondônia - RO</option>
                    <option value="RR">Roraima - RR</option>
                    <option value="SC">Santa Catarina - SC</option>
                    <option value="SP">São Paulo - SP</option>
                    <option value="SE">Sergipe - SE</option>
                    <option value="TO">Tocantins - TO</option>
                </select>
                <div class="invalid-feedback">Campo Obrigatório.</div>
                <asp:HiddenField ID="txtCords" value="" runat="server" ClientIDMode="Static" />
                <input id="btnAvancar" type="button" name="Avancar" value="Avançar" class="btn btn-success float-right mt-2" />
            </div>
            <div id="fase3">
                <h4>Localização</h4>
                <div id="map" class="mb-2"></div>
                <input id="btnVoltar" type="button" name="Voltar" value="Voltar" class="btn btn-primary float-left" />
                <input id="btnPublicar" type="button" name="Publicar" value="Publicar" class="btn btn-success float-right" />
            </div>
        </div>
    </div>
    <script>
        var map, infoWindow;
        var markers = [];

        function initMap() {
            
            map = new google.maps.Map(document.getElementById('map'), {zoom: 15});

            infoWindow = new google.maps.InfoWindow;

            google.maps.event.addListener(map, 'click', function (event) {
                if (markers.length > 0) {
                    var remove   = markers.pop();
                    remove.setMap(null);
                }

                marker = new google.maps.Marker({
                    position: event.latLng,
                    map: map
                });

                markers.push(marker);
            });

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    map.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                map.setCenter({ center: { lat: -30.028370860069906, lng: 51.2281118551042 } });

                handleLocationError(false, infoWindow, map.getCenter());
            }
        }

        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                'Error: The Geolocation service failed.' :
                'Error: Your browser doesn\'t support geolocation.');
            infoWindow.open(map);
        }
    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA2-URVRT-w7G3mGCwxkQWu1Y47rNeKQvU&callback=initMap">
    </script>
    <script src="../Assets/Scripts/Publicacao.js"></script>
</asp:Content>

<%@ Page Title="Mapa" Language="C#" MasterPageFile="~/Pages/Root.Master" AutoEventWireup="true" CodeBehind="Mapa.aspx.cs" Inherits="LocalsWebbApp.Pages.Mapa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="map" class="col-12 col-sm-10 col-md-7 col-lg-6 mx-auto"></div>
    </div>
    <div class="modal fade" id="ModalPublicacao" tabindex="-1" role="dialog" aria-labelledby="ModalPublicacao" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="panel report-panel m-0">
                    <input type="hidden" id="hdnIdPublicacao" class="hdnIdPublicacao" name="hdnIdPublicacao" value="" />
                    <div class="report-user">
                        <figure class="avatar report-avatar"></figure>
                        <div class="report-user-label">
                            <a href="javascript:void(0);" class="user">usuario</a>
                            <p class="data">data</p>
                        </div>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="report-img row">
                        <figure></figure>
                    </div>
                    <div class="report-badge row">
                        <i class="far fa-clock"></i>
                    </div>
                    <div class="report-details row">
                        <div class="col-6">
                            <p class="report-title">titulo</p>
                        </div>
                        <div class="col-6 text-right">
                        </div>
                        <div>
                            <p class="report-description">Descricao</p>
                        </div>
                    </div>
                    <div class="report-info row text-center">
                        <div class="col-4 text-left">
                            <a href="javascript:void(0);" data-toggle="tooltip" data-placement="top" title="Colaboradores" style="display: none"><i class="fas fa-users"></i>2</a>
                        </div>
                        <div class="col-4">
                            <a href="javascript:void(0);" class="verComentarios" data-id_publicacao="">Ver comentários</a>
                        </div>
                        <div class="col-4 text-right">
                            <span class="badge"></span>
                            <a href="javascript:void(0);" class="curtir" data-toggle="tooltip" data-placement="top" title="Curtir"><i class="fas fa-thumbs-up"></i></a>
                        </div>
                    </div>
                    <div class="report-comments row"></div>
                    <div class="report-input row">
                        <textarea id="txtComentario" placeholder="Adicione um comentário"></textarea>
                        <a href="javascript:void(0);" class="btnAddComentario" data-id_publicacao=""><i class="fas fa-plus-circle"></i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Assets/Scripts/Interacao.js"></script>
    <script>
        var map, infowindow, marker;
        var btn = "<div><a href='javascript:void(0);' class='btn btn-success verPublicacao' data-id_publicacao='#idPublicacao'>Ver publicação</a></div>";

        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -34.397, lng: 150.644 },
                zoom: 15
            });

            infowindow = new google.maps.InfoWindow;

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    map.setCenter(pos);

                    marker = new google.maps.Marker({
                        position: pos,
                        map: map,
                        icon: '../Assets/Imgs/icons/user-32x32.png'
                    });

                    adicionaPin();

                    
                }, function () {
                    handleLocationError(true, infowindow, map.getCenter());
                });
            } else {
                handleLocationError(false, infowindow, map.getCenter());
            }
        }

        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                'Algo deu errado.' :
                'Oops, parece que seu navegador não suporta geolocalização.');
            infoWindow.open(map);
        }

        function loadModalPublicacao(obj) {
            $('#ModalPublicacao #hdnIdPublicacao').val(obj.Id_publicacao);
            $('#ModalPublicacao .report-details .report-title').text(obj.Titulo);
            $('#ModalPublicacao .report-details .report-description').text(obj.Descricao);
            $('#ModalPublicacao .report-user .data').text(obj.Data_publicacao_formatada);
            $('#ModalPublicacao .report-user .user').prop('href', 'Perfil.aspx?id_usuario=' + obj.Id_usuario);
            $('#ModalPublicacao .report-badge').addClass(obj.Status);
            $('#ModalPublicacao .report-img figure').css('background-image', 'url(' + "'" + '../assets/imgs/Publicacoes/' + obj.Imagem + "'" + ')');
            $('#ModalPublicacao .report-details .report-local').prop('href', 'Mapa.aspx?cord=' + obj.Localizacao);
            $('#ModalPublicacao .report-user .user').text(obj.Nome_usuario);
            $('#ModalPublicacao .report-user figure').css('background-image', 'url(' + "'" + '../assets/imgs/Avatar/' + obj.Imagem_usuario + "'" + ')');
            $('#ModalPublicacao .report-info .verComentarios').data('id_publicacao', obj.Id_publicacao);
            $('#ModalPublicacao .report-info .badge').text(obj.Likes);
            $('#ModalPublicacao .report-input .btnAddComentario').data('id_publicacao', obj.Id_publicacao);

            $('.report-info .verComentarios').removeClass('disable');
            $('.report-comments .report-comment').remove();

            $('#ModalPublicacao').modal();
        }
    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA2-URVRT-w7G3mGCwxkQWu1Y47rNeKQvU&callback=initMap">
    </script>
</asp:Content>

$(document).ready(function () {
    $('.header-panel .header-search').show();

    $('.header-panel #txtPesquisar').on('keypress', function (e) {
        if (e.keyCode == 13) {
            $('.header-panel .header-search').trigger('click');
            return false;
        }
    });

    $('.header-panel .header-search').on('click', function () {
        if ($('.header-panel #txtPesquisar').is(":visible")) {
            var filtro = $('.header-panel #txtPesquisar').val().trim();

            if (filtro != "") {
                $.ajax({
                    type: "POST",
                    url: "../Pages/Feed.aspx/Pesquisar",
                    data: '{cidade: ' + JSON.stringify(filtro) + ' }',
                    contentType: "application/json",
                    dataType: "json",
                    complete: function (result) {
                        var retorno = result.responseJSON.d;

                        if (retorno.length > 0) {
                            $('#listPublicacao .row').remove();

                            retorno.map(function (e) {
                                var publicacao = "";

                                publicacao += "<div class='row'>";
                                publicacao += " <div class='panel report-panel col-12 col-sm-10 col-md-7 col-lg-6'>";
                                publicacao += "     <input type='hidden' class='hdnIdPublicacao' name='hdnIdPublicacao' value='" + e.Id_publicacao + "' />";
                                publicacao += "     <div class='report-user'>";
                                publicacao += '         <figure class="avatar report-avatar" style="background-image:url(' + "'" + '../assets/imgs/Avatar/' + e.Imagem_usuario + "'" + ');"></figure>';
                                publicacao += "         <div class='report-user-label'>";
                                publicacao += "             <a href='Perfil.aspx?id_usuario=" + e.Id_usuario + "' class='user'>" + e.Nome_usuario + "</a>";
                                publicacao += "             <p>" + e.Data_publicacao_formatada + "</p>";
                                publicacao += "         </div>";
                                publicacao += "     </div>";
                                publicacao += "     <div class='report-img row'>";
                                publicacao += '         <figure style="background-image:url(' + "'" + '../assets/imgs/Publicacoes/' + e.Imagem + "'" + ');"></figure>';
                                publicacao += "     </div>";
                                publicacao += "     <div class='report-badge row'>";
                                publicacao += "         <i class='far fa-clock'></i>";
                                publicacao += "     </div>";
                                publicacao += "     <div class='report-details row'>";
                                publicacao += "         <div class='col-6'>";
                                publicacao += "             <p class='report-title'>" + e.Titulo + "</p>";
                                publicacao += "         </div>";
                                publicacao += "         <div class='col-6 text-right'>";
                                publicacao += "             <a href='Mapa.aspx?cord=" + e.Localizacao + "' class='report-local'><i class='fas fa-map-marker-alt'></i> Mapa</a>";
                                publicacao += "         </div>";
                                publicacao += "         <div>";
                                publicacao += "             <p class='report-description'>" + e.Descricao + "</p>";
                                publicacao += "         </div>";
                                publicacao += "     </div>";
                                publicacao += "     <div class='report-info row text-center'>";
                                publicacao += "         <div class='col-4 text-left'>";
                                publicacao += "             <a href='javascript:void(0);' data-toggle='tooltip' data-placement='top' title='Colaboradores' class='d-none'><i class='fas fa-users'></i></a>";
                                publicacao += "         </div>";
                                publicacao += "         <div class='col-4'>";
                                publicacao += "             <a href='javascript:void(0);' class='verComentarios'>Ver comentários</a>";
                                publicacao += "         </div>";
                                publicacao += "         <div class='col-4 text-right'>";
                                publicacao += "             <span class='badge'>" + e.Likes + "</span>";
                                publicacao += "             <a href='javascript:void(0);' class='curtir' data-toggle='tooltip' data-placement='top' title='Curtir'><i class='fas fa-thumbs-up'></i></a>";
                                publicacao += "         </div>";
                                publicacao += "     </div>";
                                publicacao += "     <div class='report-comments row'></div>";
                                publicacao += "     <div class='report-input row'>";
                                publicacao += "         <textarea id='txtComentario' placeholder='Adicione um comentário'></textarea>";
                                publicacao += "         <a href='javascript:void(0);' class='btnAddComentario'><i class='fas fa-plus-circle'></i></a>";
                                publicacao += "     </div>";
                                publicacao += " </div>";
                                publicacao += "</div>";

                                $('#listPublicacao').append(publicacao);
                            });

                            $('.header-panel #txtPesquisar').hide();
                            $('.header-panel #txtPesquisar').val('');
                        }
                    }
                });
            }
        }
        else {
            $('.header-panel #txtPesquisar').css('box-shadow', '0 0 0 0.2rem rgba(0, 152, 40, 0.25)');
            $('.header-panel #txtPesquisar').fadeIn();
            $('.header-panel #txtPesquisar').focus();
        }
        return false;
    });
});
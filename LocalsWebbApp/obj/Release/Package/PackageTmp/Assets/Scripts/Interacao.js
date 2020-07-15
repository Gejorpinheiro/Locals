$(document).ready(function () {
    $('.report-input #txtComentario').on('keydown', function (e) { if (e.keyCode == 13) $('.report-input .btnAddComentario').trigger('click')});

    $('#listPublicacao, #ModalPublicacao').on('click', '.report-input .btnAddComentario', function () {
        var btn = $(this);
        var comentario = new Object();
        comentario.id_publicacao = $(this).parents('.report-panel').find('.hdnIdPublicacao').val();
        comentario.descricao = $(btn).prev('#txtComentario').val().trim();

        if (comentario.descricao != "" && comentario.id_publicacao > 0) {
            $.ajax({
                type: "POST",
                url: "../Pages/Feed.aspx/SalvarComentario",
                data: '{comentario: ' + JSON.stringify(comentario) + ' }',
                contentType: "application/json",
                dataType: "json",
                complete: function (result) {
                    var retorno = result.responseJSON.d;

                    var comentario = "";
                    comentario += "<div class='report-comment col-12'>";
                    comentario += "    <div class='mb-2'>";
                    comentario += "        <img src='../Assets/Imgs/Avatar/" + retorno.Avatar + "' alt='Avatar' class='avatar report-comment-avatar'>";
                    comentario += "            <div class='report-user-label'>";
                    comentario += "                <a href='Perfil.aspx?id_usuario=" + retorno.Id_usuario + "'>" + retorno.Nome_usuario + "</a>";
                    comentario += "                <p>" + retorno.Data_formatada + "</p>";
                    comentario += "            </div>";
                    comentario += "            <div class='options float-right close'>";
                    comentario += "                <a href='javascript:void(0);' data-id_comentario='" + retorno.Id_comentario + "' class='excluir'><i class='fas fa-times'></i></a>";
                    comentario += "            </div>";
                    comentario += "        </div>";
                    comentario += "        <div>";
                    comentario += "            <p>" + retorno.Descricao + "</p>";
                    comentario += "        </div>";
                    comentario += "    </div>";
                    comentario += "</div>";

                    $(btn).parent('.report-input').prev('.report-comments').append(comentario);
                    $(btn).prev('#txtComentario').val('');
                }
            });
        }

        return false;
    });

    $('#listPublicacao, #ModalPublicacao').on('click', '.report-info .verComentarios', function () {
        var id_publicacao = $(this).parents('.report-panel').find('.hdnIdPublicacao').val();
        var btn = $(this);

        if (id_publicacao > 0 && !$(btn).hasClass('disable')) {
            $.ajax({
                type: "POST",
                url: "../Pages/Feed.aspx/VerComentarios",
                data: '{id_publicacao: ' + JSON.stringify(id_publicacao) + ' }',
                contentType: "application/json",
                dataType: "json",
                complete: function (result) {
                    var retorno = result.responseJSON.d;

                    if (retorno.length > 0) {
                        var list = [];

                        retorno.map(function (e) {
                            var comentario = "";
                            comentario += "<div class='report-comment col-12'>";
                            comentario += "    <div class='mb-2'>";
                            comentario += "        <img src='../Assets/Imgs/Avatar/" + e.Avatar + "' alt='Avatar' class='avatar report-comment-avatar'>";
                            comentario += "            <div class='report-user-label'>";
                            comentario += "                <a href='Perfil.aspx?id_usuario=" + e.Id_usuario + "'>" + e.Nome_usuario + "</a>";
                            comentario += "                <p>" + e.Data_formatada + "</p>";
                            comentario += "            </div>";

                            if (e.Id_usuario == $('#hdnIdUsuario').val()) {
                                comentario += "            <div class='options float-right close'>";
                                comentario += "                <a href='javascript:void(0);' data-id_comentario='" + e.Id_comentario + "' class='excluir'><i class='fas fa-times'></i></a>";
                                comentario += "            </div>";
                            }

                            comentario += "        </div>";
                            comentario += "        <div>";
                            comentario += "            <p>" + e.Descricao + "</p>";
                            comentario += "        </div>";
                            comentario += "    </div>";
                            comentario += "</div>";

                            list.push(comentario);
                        });

                        $(btn).parents('.report-info').next('.report-comments').append(list);
                        $(btn).addClass('disable');
                    }
                }
            });
        }

        return false;
    });

    var id_usuario_sessao = $('#hdnIdUsuario').val();

    $('.report-comment .report-user-label a').map(function (e) {
        var id_usuario_comentario = $(this).prop('href').split('id_usuario=')[1];

        if (id_usuario_comentario != id_usuario_sessao) {

        }

    })


    $('#listPublicacao, #ModalPublicacao').on('click', '.report-comments .excluir', function () {
        var id_comentario = $(this).data('id_comentario');
        var comentario = $(this).parents('div.report-comment');

        $.ajax({
            type: "POST",
            url: "../Pages/Feed.aspx/ExcluirComentario",
            data: '{id_comentario: ' + JSON.stringify(id_comentario) + ' }',
            contentType: "application/json",
            dataType: "json",
            complete: function (result) {
                comentario.remove();
            }
        });

        return false;
    });

    $('#listPublicacao, #ModalPublicacao').on('click', '.report-info .curtir', function () {
        if (!$(this).hasClass('disable')) {
            var id_publicacao = $(this).parents('.report-panel').find('.hdnIdPublicacao').val();
            var count = Number($(this).prev('.badge').text());
            count += 1;

            $.ajax({
                type: "POST",
                url: "../Pages/Feed.aspx/UpdateLike",
                data: '{id_publicacao: ' + JSON.stringify(id_publicacao) + ', likes: ' + JSON.stringify(count) + '}',
                contentType: "application/json",
                dataType: "json",
                complete: function (result) {
                }
            });

            $(this).prev('.badge').text(count);
            $(this).addClass('disable');
        }

        return false;
    });
});
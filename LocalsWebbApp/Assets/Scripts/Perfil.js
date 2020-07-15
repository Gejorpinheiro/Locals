$(document).ready(function () {
    $('.header-panel .header-sair').show();

    $('.usuario-panel .usuario-edit').on('click', function () {
        limpaForm('#form-usuario');

        $('[id$=txtNovoNome]').val($('[id$=lblNome]').text());
        $('[id$=txtNovoEmail]').val($('[id$=lblEmail]').text());
        $('[id$=txtNovaCidade]').val($('[id$=lblCidade]').text());
        $('[id$=ddlEstado]').val($('[id$=lblEstado]').text());
    });

    $('#form-usuario .form-control').on('keydown', function (event) {
        if (event.keyCode == 13) {
            $('#ModalUsuario #btnSalvarUsuario').trigger('click');
            return false;
        }
    });

    $('#ModalUsuario #btnSalvarUsuario').on('click', function () {
        var Usuario = new Object;

        if (validaForm('#form-usuario') && validateEmail($('[id$=txtNovoEmail]'))) {

            Usuario.id_usuario = $('#hdnIdUsuario').val();
            Usuario.nome = $('[id$=txtNovoNome]').val();
            Usuario.email = $('[id$=txtNovoEmail]').val();
            Usuario.cidade = $('[id$=txtNovaCidade]').val();
            Usuario.estado = $('[id$=ddlEstado]').val();

            if (Usuario.id_usuario > 0) {
                $.ajax({
                    type: "POST",
                    url: "../Pages/Perfil.aspx/updateUsuario",
                    data: '{usuario: ' + JSON.stringify(Usuario) + ' }',
                    contentType: "application/json",
                    dataType: "json",
                    complete: function (result) {
                        var retorno = result.responseJSON.d;

                        switch (retorno) {
                            case "NOK":
                                console.log('deu ruim');
                                break;
                            case "OK":
                                $('[id$=lblNome]').text($('[id$=txtNovoNome]').val());
                                $('[id$=lblEmail]').text($('[id$=txtNovoEmail]').val());
                                $('[id$=lblCidade]').text($('[id$=txtNovaCidade]').val());
                                $('[id$=lblEstado]').text($('[id$=ddlEstado]').val());

                                $('#ModalUsuario').modal('hide');
                                break;
                            default:
                        }

                    }
                });
            }
        }
        else {
            event.preventDefault();
        }

        return false;
    });

    $('#FileUpload').on('change', function () {
        //get the file name
        var fileName = $(this).val().split('\\');
        //replace the "Choose a file" label
        $(this).next('.custom-file-label').html(fileName[fileName.length - 1]);
    });

    $('.usuario-publicacao a').on('click', function () {
        var id_publicacao = $(this).data('id_publicacao');
        $('#hdnIdPublicacao').val(id_publicacao);

        $.ajax({
            type: "POST",
            url: "../Pages/Perfil.aspx/GetPublicacaoById",
            data: '{id_publicacao: ' + JSON.stringify(id_publicacao) + ' }',
            contentType: "application/json",
            dataType: "json",
            complete: function (result) {
                var retorno = result.responseJSON.d;

                $('#ModalPublicacao .report-details .report-title').text(retorno.Titulo);
                $('#ModalPublicacao .report-details .report-description').text(retorno.Descricao);
                $('#ModalPublicacao .report-user .data').text(retorno.Data_publicacao_formatada);
                $('#ModalPublicacao .report-badge').addClass(retorno.Status);
                $('#ModalPublicacao .report-img figure').css('background-image', 'url(' + "'" + '../assets/imgs/Publicacoes/' + retorno.Imagem + "'" + ')');
                $('#ModalPublicacao .report-details .report-local').prop('href', 'Mapa.aspx?cord=' + retorno.Localizacao);
                $('#ModalPublicacao .report-user .user').text(retorno.Nome_usuario);
                $('#ModalPublicacao .report-user figure').css('background-image', 'url(' + "'" + '../assets/imgs/Avatar/' + retorno.Imagem_usuario + "'" + ')');
                $('#ModalPublicacao .report-info .verComentarios').data('id_publicacao', retorno.Id_publicacao);
                $('#ModalPublicacao .report-info .badge').text(retorno.Likes);
                $('#ModalPublicacao .report-input .btnAddComentario').data('id_publicacao', retorno.Id_publicacao);

                $('.report-info .verComentarios').removeClass('disable');
                $('.report-comments .report-comment').remove();

                $('#ModalPublicacao').modal();
            }
        });
    });

    // --FUNCTIONS

    function validaForm(formId) {
        var retorno = true;

        $(formId + ' .required').map(function (i, e) {

            if ($(e).val() == "") {
                $(e).addClass('is-invalid');
                $(e).next('.invalid-feedback').show();

                retorno = false;
            }
            else {
                $(e).removeClass('is-invalid');
                $(e).addClass('is-valid');
                $(e).next('.invalid-feedback').hide();
            }
        });

        return retorno;
    }

    function limpaForm(formId) {
        $(formId + ' .form-control').val('');
        $(formId + ' .required').removeClass('is-invalid');
        $(formId + ' .required').removeClass('is-valid');
        $(formId + ' .required').next('.invalid-feedback').hide();
    }

    function validateEmail(emailElement) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (re.test(String(emailElement.val()).toLowerCase()))
            return true;
        else {
            $(emailElement).addClass('is-invalid');
            $(emailElement).next('.invalid-feedback').show();
            $(emailElement).next('.invalid-feedback').text('Email Inválido');

            return false;
        }
    }
});
function showMensagem(classe, mensagem) {
    switch (classe) {
        case 'success':
            $('#alerta #mensagem').text(mensagem);
            $('#alerta').removeClass('alert-danger');
            $('#alerta').addClass('alert-success');
            break;
        case 'danger':
            $('#alerta #mensagem').text(mensagem);
            $('#alerta').removeClass('alert-success');
            $('#alerta').addClass('alert-danger');
            break;
        default:
            $('#alerta #mensagem').text(mensagem);
            $('#alerta').removeClass('alert-success');
            $('#alerta').addClass('alert-danger');
    }

    $('#alerta').fadeIn();
}

function validaForm(formId) {
    var retorno = true;

    $(formId + ' .required').map(function (i, e) {

        if ($(e).val().trim() == "") {
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

function disableEdit() {
    $('.usuario-avatar a').on('click', function (e) { e.stopPropagation(); });
    $('.usuario-avatar a figure').attr('data-original-title', '');
    $('.usuario-panel a.usuario-edit').hide();
}
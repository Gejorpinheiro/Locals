<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LocalsWebbApp.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="../Plugins/Bootstrap/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Assets/Imgs/Icons/ico_16.png" rel="icon" type="image-png" sizes="16x16" />
    <link href="../Assets/Imgs/Icons/ico_32.png" rel="icon" type="image-png" sizes="32x32" />

    <style>
        body {
            text-align: center;
            background: #dee2e6;
        }

        .form-login,
        .form-cadastro{
            width: 100%;
            max-width: 330px;
            padding: 30px;
            margin: 100px auto;
            background: #fff;
            border-radius: 5px;
        }

        .form-login .form-control,
        .form-cadastro .form-control{
            border-radius: 0;
            margin-bottom: -1px;
        }

        .form-login .form-control.usuario,
        .form-cadastro #txtNovoNome{
            border-top-right-radius: 5px;
            border-top-left-radius: 5px;
        }

        .form-login #txtSenha,
        .form-cadastro #txtNovaSenhaConfirma{
            border-bottom-right-radius: 5px;
            border-bottom-left-radius: 5px;
        }

        .form-login .invalid-feedback,
        .form-cadastro .invalid-feedback{
            text-align: left;
        }

        .form-login .alert,
        .form-cadastro .alert{
            display:none;
        }

        .form-cadastro{
            display: none;
        }
    </style>

    <script src="../Plugins/Bootstrap/Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Plugins/Bootstrap/Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" novalidate="novalidate" runat="server">
        <asp:HiddenField ID="hdnOperacao" Value="login" runat="server" />
        <div class="form-login">
            <img class="mb-5 col-10" src="../Assets/Imgs/Logo_locals.png" alt="Locals_logo"/>
            <h1 class="h3 mb-3 font-weight-normal">Acessar conta</h1>
            <div class="alert alert-warning" role="alert" runat="server"><asp:Label ID="hdnMenssagem" Text="" Visible="false" CssClass="" runat="server"/></div>
            <input type="email" id="txtEmail" class="form-control usuario required mt-1" placeholder="Email" autofocus runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="password" id="txtSenha" class="form-control senha required" placeholder="Senha" maxlength="6" runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="submit" class="login btn btn-lg btn-success btn-block mt-5 mb-2" value="Entrar"/>
            <a href="javascript:void(0);" class="btnCadastrar">Não é cadastrado ? Clique aqui!</a>
        </div>
        <div class="form-cadastro">
            <img class="mb-5 col-10" src="../Assets/Imgs/Logo_locals.png" alt="Locals_logo"/>
            <h1 class="h3 mb-3 font-weight-normal">Criar conta</h1>
            <div class="alert alert-warning" role="alert" runat="server"><asp:Label ID="Label1" Text="" Visible="false" CssClass="" runat="server"/></div>
            <input type="text" id="txtNovoNome" class="form-control required mt-1" placeholder="Nome Completo" runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="email" id="txtNovoEmail" class="form-control required" placeholder="Email" autofocus runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="text" id="txtNovaCidade" class="form-control required" placeholder="Cidade" runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <select id="ddlEstado" class="form-control required" runat="server">
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
            <input type="password" id="txtNovaSenha" class="form-control senha required" placeholder="Senha" maxlength="6" runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="password" id="txtNovaSenhaConfirma" class="form-control senha confirma required" maxlength="6" placeholder="Confirmar senha" runat="server"/>
            <div class="invalid-feedback">Campo Obrigatório.</div>
            <input type="submit" class="login btn btn-lg btn-success btn-block mt-5 mb-2" value="Salvar"/>
        </div>
    </form>
</body>
<script>
    $(document).ready(function () {
        if ($('form .alert span').length > 0)
            $('form .alert').show();

        $('form').on('submit', function (event) {
            if ($('#hdnOperacao').val() == 'login') {
                if (validateEmail($('.form-login #txtEmail').val())) {
                    $('.form-login .required').map(function (i, e) {

                        if ($(e).val() == "") {
                            $(e).addClass('is-invalid');
                            $(e).closest('.invalid-feedback').show();

                            event.preventDefault();
                        }
                        else {
                            $(e).removeClass('is-invalid');
                            $(e).addClass('is-valid');
                            $(e).closest('.invalid-feedback').hide();
                        }
                    });

                    return true;
                }
                else {
                    $('.form-login #txtEmail').addClass('is-invalid');
                    $('.form-login #txtEmail').next('.invalid-feedback').text('Email Inválido.');
                    $('.form-login #txtEmail').next('.invalid-feedback').show();

                    event.preventDefault();
                }
            }
            else {
                if (validateEmail($('.form-cadastro #txtNovoEmail').val())) {
                    $('.form-cadastro .required').map(function (i, e) {

                        if ($(e).val() == "") {
                            $(e).addClass('is-invalid');
                            $(e).closest('.invalid-feedback').show();

                            event.preventDefault();
                        }
                        else {
                            $(e).removeClass('is-invalid');
                            $(e).addClass('is-valid');
                            $(e).next('.invalid-feedback').hide();
                        }
                    });

                    if ($('.form-cadastro #txtNovaSenha').val().trim() != $('.form-cadastro #txtNovaSenhaConfirma').val().trim()) {
                        $('.form-cadastro #txtNovaSenhaConfirma').addClass('is-invalid');
                        $('.form-cadastro #txtNovaSenhaConfirma').next('.invalid-feedback').text('Senha diferente da anterior.');
                        $('.form-cadastro #txtNovaSenhaConfirma').next('.invalid-feedback').show();

                        event.preventDefault();
                    }

                    return;
                }
                else {
                    $('.form-cadastro #txtNovoEmail').addClass('is-invalid');
                    $('.form-cadastro #txtNovoEmail').next('.invalid-feedback').text('Email Inválido.');
                    $('.form-cadastro #txtNovoEmail').next('.invalid-feedback').show();

                    event.preventDefault();
                }
            }
        });

        $('.btnCadastrar').on('click', function () {
            $('.form-login').hide();
            $('.form-cadastro').fadeIn();
            $('#hdnOperacao').val('cadastro');
        });

        function validateEmail(email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            console.log(re.test(String(email).toLowerCase()));
            return re.test(String(email).toLowerCase());
        }
    });
</script>
</html>

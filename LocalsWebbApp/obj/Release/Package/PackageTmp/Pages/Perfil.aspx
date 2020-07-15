<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/Pages/Root.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="LocalsWebbApp.Pages.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnIdUsuario" ClientIDMode="Static" runat="server" Value="0" />
    <style>
        #form-usuario .form-control {
            margin-bottom: -1px;
            border-radius: 0;
        }

        #form-usuario .alert {
            display: none;
        }
    </style>
    <div class="row">
        <div class="col-md-6 mx-auto text-center usuario-avatar">
            <a href="javascript:void(0);" data-toggle="modal" data-target="#ModalAvatar">
                <figure id="imgUsuario" class="avatar" runat="server" data-toggle="tooltip" data-placement="right" title="" data-original-title="Alterar imagem"></figure>
            </a>
        </div>
    </div>
    <div class="row">
        <div class="panel usuario-panel mb-5 col-10 col-sm-8 col-md-5 col-lg-4">
            <div class="row">
                <div class="col-12 text-right"><a href="javascript:void(0);" class="usuario-edit" data-toggle="modal" data-target="#ModalUsuario"><i class="fas fa-pen"></i></a></div>
                <div class="usuario-info text-center border-bottom pb-3 col-12">
                    <h3 class="font-weight-bold text-capitalize">
                        <asp:Label ID="lblNome" Text="" runat="server" /></h3>
                    <p>
                        <asp:Label ID="lblEmail" Text="" runat="server" />
                    </p>
                    <p>
                        <asp:Label ID="lblCidade" Text="" runat="server" CssClass="text-capitalize" />-<asp:Label ID="lblEstado" Text="" runat="server" />
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-12 pt-2 text-center border-right">
                    <h3 class="font-weight-bold"><asp:Label ID="lblCountPublicacao" Text="1" runat="server" /></h3>
                    <p><asp:Label ID="lblPublicacoes" Text="Publicação" runat="server" /></p>
                </div>
                <!--
                <div class="col-6 pt-2 text-center">
                    <h3 class="font-weight-bold">0</h3>
                    <p>Participações</p>
                </div>
                -->
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 mx-auto">
            <div class="usuario-publicacoes">
                <div class="row">
                    <asp:Repeater ID="rptPublicacoes" runat="server">
                        <ItemTemplate>
                            <div class="usuario-publicacao mx-auto mb-3 col-10 col-md-6 col-lg-4">
                                <a href="javascript:void(0);" data-id_publicacao="<%# DataBinder.Eval(Container.DataItem,"Id_publicacao") %>">
                                    <figure style="background-image:url('../assets/imgs/Publicacoes/<%# DataBinder.Eval(Container.DataItem,"Imagem") %>');" class="rounded"></figure>
                                </a>
                                <div class="publicacao-badge text-center">
                                    <i class="far fa-clock p-1 rounded-circle bg-warning"></i>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalUsuario" tabindex="-1" role="dialog" aria-labelledby="modalUsuario" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Usuário</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="form-usuario">
                        <div class="alert alert-warning" role="alert" runat="server">
                            <asp:Label ID="hdnMenssagem" Text="" Visible="false" CssClass="" runat="server" />
                        </div>
                        <input type="text" id="txtNovoNome" class="form-control required mt-1 rounded-top" placeholder="Nome Completo" runat="server" />
                        <div class="invalid-feedback">Campo Obrigatório.</div>
                        <input type="email" id="txtNovoEmail" class="form-control required" placeholder="Email" autofocus runat="server" />
                        <div class="invalid-feedback">Campo Obrigatório.</div>
                        <input type="text" id="txtNovaCidade" class="form-control required" placeholder="Cidade" runat="server" />
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
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnSalvarUsuario" class="btn btn-success">Salvar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalAvatar" tabindex="-1" role="dialog" aria-labelledby="ModalAvatar" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Alterar Imagem de perfil</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:PlaceHolder ID="phFileMessage" Visible="false" runat="server">
                        <div class="alert alert-warning" role="alert" runat="server">
                            <asp:Label ID="lblFileMessage" Text="" CssClass="" runat="server" />
                        </div>
                    </asp:PlaceHolder>
                    <div class="input-group">
                        <div class="custom-file">
                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="custom-file-input" ClientIDMode="Static" />
                            <label class="custom-file-label" for="UploadImagem">Selecionar arquivo</label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button Text="Salvar" runat="server" CssClass="btn btn-success" OnClick="Upload_file" />
                </div>
            </div>
        </div>
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
                            <a href="javascript:void(0);" class="report-local"><i class="fas fa-map-marker-alt"></i> Mapa</a>
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
    <script src="../Assets/Scripts/Perfil.js"></script>
    <script src="../Assets/Scripts/Interacao.js"></script>
</asp:Content>

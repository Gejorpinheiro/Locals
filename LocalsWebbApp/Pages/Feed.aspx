<%@ Page Title="Feed" Language="C#" MasterPageFile="~/Pages/Root.Master" CodeBehind="Feed.aspx.cs" Inherits="LocalsWebbApp.Pages.Feed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnIdUsuario" Value="" runat="server" ClientIDMode="Static" />
    <div id="listPublicacao">
        <asp:Repeater ID="rptPublicacao" runat="server">
            <ItemTemplate>
                <div class="row">
                    <div class="panel report-panel col-12 col-sm-10 col-md-7 col-lg-6">
                        <input type="hidden" class="hdnIdPublicacao" name="hdnIdPublicacao" value="<%# DataBinder.Eval(Container.DataItem,"Id_publicacao") %>" />
                        <div class="report-user">
                            <figure class="avatar report-avatar" style="background-image:url('../assets/imgs/Avatar/<%# DataBinder.Eval(Container.DataItem,"Imagem_usuario") %>');"></figure>
                            <div class="report-user-label">
                                <a href="Perfil.aspx?id_usuario=<%# DataBinder.Eval(Container.DataItem,"Id_usuario") %>" class="user"><%# DataBinder.Eval(Container.DataItem,"Nome_usuario") %></a>
                                <p><%# DataBinder.Eval(Container.DataItem,"Data_publicacao_formatada") %></p>
                            </div>
                        </div>
                        <div class="report-img row">
                            <figure style="background-image:url('../assets/imgs/Publicacoes/<%# DataBinder.Eval(Container.DataItem,"Imagem") %>');"></figure>
                        </div>
                        <div class="report-badge row">
                            <i class="far fa-clock"></i>
                        </div>
                        <div class="report-details row">
                            <div class="col-6">
                                <p class="report-title"><%# DataBinder.Eval(Container.DataItem,"Titulo") %></p>
                            </div>
                            <div class="col-6 text-right">
                                <a href="Mapa.aspx?cord=<%# DataBinder.Eval(Container.DataItem,"Localizacao") %>" class="report-local"><i class="fas fa-map-marker-alt"></i> Mapa</a>
                            </div>
                            <div>
                                <p class="report-description"><%# DataBinder.Eval(Container.DataItem,"Descricao") %></p>
                            </div>
                        </div>
                        <div class="report-info row text-center">
                            <div class="col-4 text-left">
                                <a href="javascript:void(0);" data-toggle="tooltip" data-placement="top" title="Colaboradores" style="display: none"><i class="fas fa-users"></i></a>
                            </div>
                            <div class="col-4">
                                <a href="javascript:void(0);" class="verComentarios" data-id_publicacao="<%# DataBinder.Eval(Container.DataItem,"Id_publicacao") %>">Ver comentários</a>
                            </div>
                            <div class="col-4 text-right">
                                <span class="badge"><%# DataBinder.Eval(Container.DataItem,"Likes") %></span>
                                <a href="javascript:void(0);" class="curtir" data-toggle="tooltip" data-placement="top" title="Curtir"><i class="fas fa-thumbs-up"></i></a>
                            </div>
                        </div>
                        <div class="report-comments row"></div>
                        <div class="report-input row">
                            <textarea id="txtComentario" placeholder="Adicione um comentário"></textarea>
                            <a href="javascript:void(0);" class="btnAddComentario" data-id_publicacao="<%# DataBinder.Eval(Container.DataItem,"Id_publicacao") %>"><i class="fas fa-plus-circle"></i></a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script src="../Assets/Scripts/Feed.js"></script>
    <script src="../Assets/Scripts/Interacao.js"></script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profesor.aspx.cs" Inherits="Escuela.Vista.Profesor" EnableEventValidation="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="../Recurso/CSS/StyleProfe.css" rel="stylesheet" />
    <title>Profesor</title>
</head>
<body>
    <div class="d-flex justify-content-between p-3">
        <asp:Image ID="img_logo" runat="server" ImageUrl="~/Recurso/img/shool.png" Style="width: 75px;" />
        <div class="mx-auto font-weight-bold text-black justify-content-center" style="width: 250px">
            <asp:Label runat="server" CssClass="h2" ID="lbltitulo" Text="Listado de Notas" ForeColor="Black"></asp:Label>
        </div>
    </div>
    <br />
    <form id="form_profe" runat="server" class="h-50 d-flex align-items-center">
        <br />
        <div class="container">
            <div class="table m-4">
                <asp:GridView runat="server" ID="gvusuarios" class="table table-borderless table-hover">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row-cols-auto">
                <div class="col m-5">
                    <asp:Button runat="server" ID="btn_crear" CssClass="btn btn-success form-control-sm btn-margin" Text="Crear" OnClick="btn_crear_Click" />
                    <asp:Button runat="server" ID="btn_actualizar" CssClass="btn form-control-sm btn-warning btn-margin" Text="Editar" OnClick="btn_actualizar_Click" />
                </div>
            </div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

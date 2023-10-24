<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estudiante.aspx.cs" Inherits="Escuela.Vista.Estudiante" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link href="../Recurso/CSS/StyleEst.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarventana(mensaje) {
            alert(mensaje);
        }
        function mostrarAlerta(mensaje) {
            alert(mensaje);
        }
    </script>
    <title>Estudiante</title>
</head>
<body>
    <br />
    <div class="mx-auto font-weight-bold text-black justify-content-center" style="width: 250px">
        <asp:Label runat="server" CssClass="h2" ID="lbltitulo" Text="Mis Notas" ForeColor="Black"></asp:Label>
    </div>
    <br />
    <form id="form_est" runat="server" class="h-50 d-flex align-items-center">
        <br />
        <div class="container">
            <div class="table m-4">
                <asp:GridView runat="server" ID="gv_notas" class="table table-borderless table-hover">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

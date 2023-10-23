<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Escuela.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <link href="Recurso/CSS/Style.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarventana(mensaje) {
            alert(mensaje);
        }
        function mostrarAlerta(mensaje) {
            alert(mensaje);
        }
    </script>
    <title>Login</title>
</head>
<body>
    <div class="wrapper con-estilo">
        <div class="formcontent">
            <form id="form_login" runat="server">
                <div class="form-control">
                    <div class="col-md-12 text-center mb-5">
                        <br />
                        <asp:Label class="h3" ID="lbl_bienvenida" runat="server" Text="Bienvenido/a al Sistema"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lbl_correo" runat="server" Text="Correo: "></asp:Label>
                        <asp:TextBox ID="txt_correo" CssClass="form-control" placeholder="Correo" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="lbl_pass" runat="server" Text="Contraseña: "></asp:Label>
                        <asp:TextBox ID="txt_pass" CssClass="form-control" placeholder="Contraseña" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div>
                        <hr />
                        <asp:Button ID="btn_ingresar" OnClick="btn_ingresar_Click" CssClass="btn btn-primary" BackColor="#00CC66" runat="server" Text="Ingresar" BorderColor="#00CC66" />
                        <asp:Button ID="btn_registrar" OnClick="btn_registrar_Click" CssClass="btn btn-primary" runat="server" Text="Registrar" BackColor="#0066FF" BorderColor="#0066FF" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>

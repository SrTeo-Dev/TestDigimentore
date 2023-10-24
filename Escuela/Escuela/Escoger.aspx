<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Escoger.aspx.cs" Inherits="Escuela.Escoger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <link href="Recurso/CSS/StyleEscoge.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarventana(mensaje) {
            alert(mensaje);
        }
        function mostrarAlerta(mensaje) {
            alert(mensaje);
        }
        function mostrarOcultarDropDown() {
            var ddlPerfil = document.getElementById('<%= cmb_perfil.ClientID %>');
            var ddlExtra = document.getElementById('<%= ddl_curso.ClientID %>');
            var lblExtra = document.getElementById('<%= lbl_curso.ClientID %>');

            if (ddlPerfil.value === "4") {
                ddlExtra.style.display = 'block'; // Mostrar el segundo DropDownList
                lblExtra.style.display = 'block'; // Mostrar
            } else {
                ddlExtra.style.display = 'none'; // Ocultar el segundo DropDownList
                lblExtra.style.display = 'none'; // Ocultar el segundo DropDownList
            }
        }
    </script>

    <title>Escoge</title>
</head>
<body>
    <div class="wrapper con-estilo">
        <div class="formcontent">
            <form id="form_escoge" runat="server">
                <div class="form-control">
                    <div class="col-md-12 text-center mb-5">
                        <br />
                        <asp:Label class="h3" ID="lbl_registro" runat="server" Text="Registrate    "></asp:Label>
                        <asp:Image ID="img_logo" runat="server" ImageUrl="~/Recurso/img/shool.png" Style="width: 150px;" />
                    </div>
                    <div>
                        <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                        <asp:TextBox ID="txt_nombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label ID="lbl_correo" runat="server" Text="Correo: "></asp:Label>
                        <asp:TextBox ID="txt_correo" CssClass="form-control" placeholder="Correo" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label ID="lbl_pass" runat="server" Text="Contraseña: "></asp:Label>
                        <asp:TextBox ID="txt_pass" CssClass="form-control" placeholder="Contraseña" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="lbl_perfil" runat="server" Text="Perfil: "></asp:Label>
                        <asp:DropDownList ID="cmb_perfil" class="form-select" runat="server" onchange="mostrarOcultarDropDown();" AppendDataBoundItems="True"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="lbl_curso" runat="server" Text="Curso: " Style="display: none;"></asp:Label>
                        <asp:DropDownList ID="ddl_curso" class="form-select" runat="server" Style="display: none;" AppendDataBoundItems="True"></asp:DropDownList>
                    </div>
                    <div>
                        <hr />
                        <asp:Button ID="btn_registrar" OnClick="btn_registrar_Click" CssClass="btn btn-primary" runat="server" Text="Registrar" BackColor="#0066FF" BorderColor="#0066FF" />
                        <asp:Button ID="btn_regresar" OnClick="btn_regresar_Click" CssClass="btn btn-primary" runat="server" Text="Regresar" BackColor="#E74C3C" BorderColor="#E74C3C" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>

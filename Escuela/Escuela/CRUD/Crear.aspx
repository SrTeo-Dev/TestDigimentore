<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="Escuela.Crear" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.caom/jquery-3.7.1.min.js"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <link href="../Recurso/CSS/StyleCrear.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarventana(mensaje) {
            alert(mensaje);
        }
        function mostrarAlerta(mensaje) {
            alert(mensaje);
        }
    </script>
    <title>Subir Notas</title>
</head>
<body>
    <div class="wrapper con-estilo">
        <div class="formcontent">
            <form id="form_crear" runat="server">
                <div class="form-control">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="text-center mb-5">
                                <br />
                                <asp:Label class="h3" ID="lbl_subir" runat="server" Text="Subir Nota"></asp:Label>
                            </div>
                            <br />
                            <div>
                                <asp:Label ID="lbl_n1" runat="server" Text="Nota 1: "></asp:Label>
                                <asp:TextBox ID="txt_n1" CssClass="form-control" placeholder="Nota" runat="server"></asp:TextBox>
                            </div>
                            <br />
                            <div>
                                <asp:Label ID="lbl_n2" runat="server" Text="Nota 2: "></asp:Label>
                                <asp:TextBox ID="txt_n2" CssClass="form-control" placeholder="Nota" runat="server"></asp:TextBox>
                            </div>
                            <br />
                            <div>
                                <asp:Label ID="lbl_n3" runat="server" Text="Nota 3: "></asp:Label>
                                <asp:TextBox ID="txt_n3" CssClass="form-control" placeholder="Nota" runat="server"></asp:TextBox>
                            </div>
                            <br />
                            <div>
                                <asp:Label ID="lbl_examne" runat="server" Text="Examen: "></asp:Label>
                                <asp:TextBox ID="txt_examen" CssClass="form-control" placeholder="Examen" runat="server"></asp:TextBox>
                            </div>
                            <hr />
                            <div>
                                <asp:Button ID="btn_subir" CssClass="btn btn-primary" runat="server" Text="Subir Notas"/>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="text-center mb-5">
                                <br />
                                <asp:Label class="h3" ID="lbl_estudiante" runat="server" Text="Establacer Nota"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lbl_nombre" runat="server" Text="Nombre: "></asp:Label>
                                <asp:DropDownList ID="ddl_nombre" class="form-select" runat="server" AppendDataBoundItems="True"></asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <asp:Label ID="lbl_curso" runat="server" Text="Curso: "></asp:Label>
                                <asp:DropDownList ID="ddl_curso" class="form-select" runat="server" AppendDataBoundItems="True"></asp:DropDownList>
                            </div>
                            <hr />
                            <div>
                                <asp:Button ID="btn_alumno" CssClass="btn btn-primary" runat="server" Text="Establecer Alumno" />
                            </div>
                        </div>
                    </div>
                    <hr />
                    <asp:Button ID="btn_regresar" CssClass="btn btn-primary" runat="server" Text="Regresar" BackColor="#E74C3C" BorderColor="#E74C3C" OnClick="btn_regresar_Click" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>

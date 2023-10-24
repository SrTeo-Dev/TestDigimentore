using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Escuela
{
    public partial class Login : System.Web.UI.Page
    {
        //cadena de conexion
        private SqlConnection con = new SqlConnection(@"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Escoger.aspx");
        }
        protected void btn_ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                Login_u(txt_correo.Text, txt_pass.Text);
            }
            catch (Exception ex)
            {
                //aqui quiero el codigo del error para mostrar una ventana emergente
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('" + ex.Message + "');", true);
            }
        }

        private void Login_u(string correo, string pass)
        {
            if (string.IsNullOrEmpty(correo) && string.IsNullOrEmpty(pass))
            {
                // Ventana de alerta en la página web
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarventana", "mostrarventana('Los campos están vacíos...');", true);
            }
            else if (string.IsNullOrEmpty(correo))
            {
                // Ventana de alerta en la página web
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarventana", "mostrarventana('Ingrese su usuario...');", true);
            }
            else if (string.IsNullOrEmpty(pass))
            {
                // Ventana de alerta en la página web
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarventana", "mostrarventana('Ingrese su contraseña...');", true);
            }
            else if (txt_pass.Text.Length <= 4)
            {
                // Ventana de alerta en la página web
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mostrarventana", "mostrarventana('La contraseña debe tener al menos 5 dígitos.');", true);
            }
            else
            {
                validar_usu_cor(correo, pass);
            }
        }

        private void validar_usu_cor(string correo, string pass)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT u.usu_nombre, u.usu_correo, u.tusu_id, tu.tusu_nombre, u.usu_estado FROM tbl_usuario u JOIN tbl_tusuario tu ON u.tusu_id = tu.tusu_id WHERE u.usu_correo = @mail AND u.usu_pass = @pass", con);
                cmd.Parameters.AddWithValue("@mail", correo);
                cmd.Parameters.AddWithValue("@pass", pass);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count == 1)
                {
                    string estadoUsuario = dt.Rows[0]["usu_estado"].ToString();

                    if (estadoUsuario == "I")
                    {
                        this.txt_correo.Text = "";
                        this.txt_pass.Text = "";
                    }
                    else
                    {
                        if (dt.Rows[0]["tusu_id"].ToString() == "3")
                        {
                            Response.Redirect("Vista/Profesor.aspx");
                        }
                        else if (dt.Rows[0]["tusu_id"].ToString() == "4")
                        {
                            Response.Redirect("Vista/Estudiante.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('" + ex.Message + "');", true);
            }
        }
    }
}
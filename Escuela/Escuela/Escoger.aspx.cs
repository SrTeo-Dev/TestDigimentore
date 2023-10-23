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
    public partial class Escoger : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela");
        string connectionString = @"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                con.Open();
                SqlCommand cmd_perfil = new SqlCommand("select tusu_id, tusu_nombre from tbl_tusuario", con);

                SqlDataReader reader_perfil = cmd_perfil.ExecuteReader();

                // Limpia el DropDownList antes de agregar datos
                cmb_perfil.Items.Clear();

                // Agrega la opción "Seleccione" con valor 0
                cmb_perfil.Items.Add(new ListItem("Seleccione", "0"));

                // Enlaza los datos del lector a DropDownList
                cmb_perfil.DataTextField = "tusu_nombre";
                cmb_perfil.DataValueField = "tusu_id";
                cmb_perfil.DataSource = reader_perfil;
                cmb_perfil.DataBind();
                con.Close();

                con.Open();
                SqlCommand cmd_curso = new SqlCommand("select cur_id, cur_nombre from tbl_curso", con);
                SqlDataReader reader_curso = cmd_curso.ExecuteReader();
                // Limpia el DropDownList antes de agregar datos
                ddl_curso.Items.Clear();

                // Agrega la opción "Seleccione" con valor 0
                ddl_curso.Items.Add(new ListItem("Seleccione", "0"));

                ddl_curso.DataTextField = "cur_nombre";
                ddl_curso.DataValueField = "cur_id";
                ddl_curso.DataSource = reader_curso;
                ddl_curso.DataBind();
                con.Close();
            }
        }
        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text) || cmb_perfil.Text == "Seleccione" || string.IsNullOrEmpty(txt_correo.Text) || string.IsNullOrEmpty(txt_pass.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showalert", "mostrarventana('Se han encontrado campos vacios.\nIngrese todos los campos.');", true);
            }
            else if (ddl_curso.Text == "Seleccione"  && ddl_curso.SelectedItem.Value == "0")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showalert", "mostrarventana('Se han encontrado el campo vacio.\nIngrese su curso.');", true);

            }
            else if (txt_pass.Text.Length <= 4)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showalert", "mostrarventana('La contraseña es muy débil al menos debe tener al menos 5 dígitos.');", true);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_usuario (usu_nombre, usu_pass, usu_correo, usu_estado, tusu_id) " +
                                                         "VALUES (@nom, @contra, @corre, 'A', @tusu)", con);
                        cmd.Parameters.AddWithValue("@nom", txt_nombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@contra", txt_pass.Text.Trim());
                        cmd.Parameters.AddWithValue("@corre", txt_correo.Text.Trim());
                        cmd.Parameters.AddWithValue("@tusu", Convert.ToInt32(cmb_perfil.SelectedValue));
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (ddl_curso.SelectedItem.Value != "0")
                        {
                            con.Open();
                            // Luego, obtenemos el id del usuario recién insertado
                            SqlCommand cmdObtenerId = new SqlCommand("SELECT MAX(usu_id) FROM tbl_usuario", con);
                            int usuId = (int)cmdObtenerId.ExecuteScalar();

                            // Finalmente, insertamos en la tabla tbl_estudiante
                            SqlCommand cmdEstudiante = new SqlCommand("INSERT INTO tbl_estudiante (usu_id, cur_id) " +
                                                                    "VALUES (@usu_id, @cur_id)", con);
                            cmdEstudiante.Parameters.AddWithValue("@usu_id", usuId);
                            cmdEstudiante.Parameters.AddWithValue("@cur_id", Convert.ToInt32(ddl_curso.SelectedValue));
                            cmdEstudiante.ExecuteNonQuery();

                            con.Close();
                        }
                        
                        Response.Redirect("Login.aspx");
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Datos no guardados. " + ex.Message + "');", true);
                }
            }
        }
        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
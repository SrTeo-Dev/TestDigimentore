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
    public partial class Actualizar : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargar_ddl();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            verificar_alumno();
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Vista/Profesor.aspx");
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            int idEstudiante = Convert.ToInt32(ddl_nombre.SelectedValue);
            int idCurso = Convert.ToInt32(ddl_curso.SelectedValue);
            editar_notas(idEstudiante, idCurso);
        }
        #region Metodos
        void limpiar_notas()
        {
            txt_n1.Text = txt_n2.Text = txt_n3.Text = txt_examen.Text = "";
        }
        void editar_notas(int Id_est, int Id_cur)
        {
            // Obtén los valores de las notas de los TextBox y conviértelos a decimal
            decimal nNota1 = string.IsNullOrEmpty(txt_n1.Text) ? 0 : Convert.ToDecimal(txt_n1.Text);
            decimal nNota2 = string.IsNullOrEmpty(txt_n2.Text) ? 0 : Convert.ToDecimal(txt_n2.Text);
            decimal nNota3 = string.IsNullOrEmpty(txt_n3.Text) ? 0 : Convert.ToDecimal(txt_n3.Text);
            decimal nExamen = string.IsNullOrEmpty(txt_examen.Text) ? 0 : Convert.ToDecimal(txt_examen.Text);
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE tbl_nota SET not_nuno = @nuevo_not_nuno, not_ndos = @nuevo_not_ndos, not_ntres = @nuevo_not_ntres, not_examen = @nuevo_not_examen WHERE not_id IN(SELECT n.not_id FROM tbl_estudiante e INNER JOIN tbl_nota n ON e.est_id = n.not_id WHERE e.est_id = @estudiante_id AND e.cur_id = @curso_id)", con))
                {
                    // Asigna los nuevos valores a los parámetros
                    cmd.Parameters.AddWithValue("@nuevo_not_nuno", nNota1);
                    cmd.Parameters.AddWithValue("@nuevo_not_ndos", nNota2);
                    cmd.Parameters.AddWithValue("@nuevo_not_ntres", nNota3);
                    cmd.Parameters.AddWithValue("@nuevo_not_examen", nExamen);
                    cmd.Parameters.AddWithValue("@estudiante_id", Id_est);
                    cmd.Parameters.AddWithValue("@curso_id", Id_cur);

                    // Ejecuta la consulta para actualizar los datos
                    cmd.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Se actualizo con éxito.');", true);
                limpiar_notas();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('" + ex.Message + "');", true);
            }

        }
        void verificar_alumno()
        {
            // Obtener los valores seleccionados en los DropDownList
            int id_estudiante = Convert.ToInt32(ddl_nombre.SelectedValue); // Obtener el ID del usuario
            int id_curso = Convert.ToInt32(ddl_curso.SelectedValue);

            if (id_estudiante == 0 && id_curso == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('El Nombre y el Curso del estudiante no se a seleccionado.');", true);
            }
            else if (id_estudiante == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('El Nombre del estudiante no se a seleccionado.');", true);
            }
            else if (id_curso == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('El Curso del estudiante no se a seleccionado.');", true);
            }
            else
            {
                con.Open();

                // Consulta para obtener las notas basadas en el usuario y el curso
                SqlCommand cmd = new SqlCommand("SELECT n.not_nuno, n.not_ndos, n.not_ntres, n.not_examen FROM tbl_estudiante e INNER JOIN tbl_nota n ON e.est_id = n.not_id WHERE e.est_id = @est_id AND e.cur_id = @cur_id", con);
                cmd.Parameters.AddWithValue("@est_id", id_estudiante);
                cmd.Parameters.AddWithValue("@cur_id", id_curso);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Las notas se encuentran en tbl_nota, asumiendo que esta es la tabla correcta.
                    decimal nota1 = (decimal)reader["not_nuno"];
                    decimal nota2 = (decimal)reader["not_ndos"];
                    decimal nota3 = (decimal)reader["not_ntres"];
                    decimal examen = (decimal)reader["not_examen"];

                    // Convierte las notas a cadenas y muestra en los TextBox correspondientes
                    txt_n1.Text = nota1.ToString();
                    txt_n2.Text = nota2.ToString();
                    txt_n3.Text = nota3.ToString();
                    txt_examen.Text = examen.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Este estudiante no tiene notas registradas. Deben agregarse notas para este estudiante y curso.');", true);
                    limpiar_notas();
                }

                con.Close();
            }
        }
        void cargar_ddl()
        {
            con.Open();
            SqlCommand cmd_usu = new SqlCommand("SELECT est_id, usu_nombre FROM tbl_estudiante INNER JOIN tbl_usuario ON tbl_estudiante.usu_id = tbl_usuario.usu_id", con);
            SqlDataReader reader_usu = cmd_usu.ExecuteReader();
            ddl_nombre.Items.Clear();
            ddl_nombre.Items.Add(new ListItem("Seleccione", "0"));

            // Enlaza los datos del lector al DropDownList
            ddl_nombre.DataTextField = "usu_nombre";
            ddl_nombre.DataValueField = "est_id";
            ddl_nombre.DataSource = reader_usu;
            ddl_nombre.DataBind();
            con.Close();

            con.Open();
            SqlCommand cmd_cur = new SqlCommand("SELECT cur_id, cur_nombre FROM tbl_curso", con);
            SqlDataReader reader_cur = cmd_cur.ExecuteReader();
            ddl_curso.Items.Clear();
            ddl_curso.Items.Add(new ListItem("Seleccione", "0"));

            // Enlaza los datos del lector al DropDownList
            ddl_curso.DataTextField = "cur_nombre";
            ddl_curso.DataValueField = "cur_id";
            ddl_curso.DataSource = reader_cur;
            ddl_curso.DataBind();
            con.Close();
        }
        #endregion
    }
}
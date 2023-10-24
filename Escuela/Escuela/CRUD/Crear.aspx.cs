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
    public partial class Crear : System.Web.UI.Page
    {
        public SqlConnection con = new SqlConnection(@"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargar_ddl();
            }
        }
        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Vista/Profesor.aspx");
        }
        protected void btn_subir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_n1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('La Nota 1 esta vacía.');", true);
            }
            else if (string.IsNullOrEmpty(txt_n2.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('La Nota 2 esta vacía.');", true);
            }
            else if (string.IsNullOrEmpty(txt_n3.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('La Nota 3 esta vacía.');", true);
            }
            else if (string.IsNullOrEmpty(txt_examen.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('El Examen esta vacía.');", true);
            }
            else
            {
                subir_notas();
            }
        }
        #region Metodos
        void subir_notas()
        {
            decimal nNota1 = !string.IsNullOrEmpty(txt_n1.Text) ? Convert.ToDecimal(txt_n1.Text) : 0;
            decimal nNota2 = !string.IsNullOrEmpty(txt_n2.Text) ? Convert.ToDecimal(txt_n2.Text) : 0;
            decimal nNota3 = !string.IsNullOrEmpty(txt_n3.Text) ? Convert.ToDecimal(txt_n3.Text) : 0;
            decimal nExamen = !string.IsNullOrEmpty(txt_examen.Text) ? Convert.ToDecimal(txt_examen.Text) : 0;

            con.Open();

            // Insertar en tbl_nota
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_nota ( not_nuno, not_ndos, not_ntres, not_examen) VALUES(@nuevo_not_nuno, @nuevo_not_ndos, @nuevo_not_ntres, @nuevo_not_examen);", con);
            cmd.Parameters.AddWithValue("@nuevo_not_nuno", nNota1);
            cmd.Parameters.AddWithValue("@nuevo_not_ndos", nNota2);
            cmd.Parameters.AddWithValue("@nuevo_not_ntres", nNota3);
            cmd.Parameters.AddWithValue("@nuevo_not_examen", nExamen);


            if (cmd.ExecuteNonQuery() > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Las notas se subieron con éxito.');", true);

                // Después de insertar la nota con éxito, obtén el último ID insertado usando SELECT SCOPE_IDENTITY().
                SqlCommand getIdCmd = new SqlCommand("SELECT TOP 1 not_id FROM tbl_nota ORDER BY not_id DESC", con);
                object result = getIdCmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    int newNotId = Convert.ToInt32(result);
                    con.Close();
                    // Llama a la función para establecer el ID de la nota en la tabla del estudiante.
                    EstablecerIdNotaEnEstudiante(newNotId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Error al establecer la nota la nota.');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Error al subir las notas.');", true);
            }

            con.Close();
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
        void limpiar_notas()
        {
            txt_n1.Text = txt_n2.Text = txt_n3.Text = txt_examen.Text = "";
        }
        void EstablecerIdNotaEnEstudiante(int notaId)
        {
            int estudianteId = Convert.ToInt32(ddl_nombre.SelectedValue);
            int cursoId = Convert.ToInt32(ddl_curso.SelectedValue);
            con.Open();

            // Actualiza el campo not_id del estudiante con el nuevo ID de nota.
            SqlCommand updateEstudianteCmd = new SqlCommand("UPDATE tbl_estudiante SET not_id = @notId WHERE est_id = @estudianteId AND cur_id = @cursoId", con);
            updateEstudianteCmd.Parameters.AddWithValue("@notId", notaId);
            updateEstudianteCmd.Parameters.AddWithValue("@estudianteId", estudianteId);
            updateEstudianteCmd.Parameters.AddWithValue("@cursoId", cursoId);

            int updateRows = updateEstudianteCmd.ExecuteNonQuery();

            if (updateRows > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('El ID de nota se asignó al estudiante correctamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "mostrarAlerta('Error al asignar el ID de nota al estudiante.');", true);
            }
            con.Close();
        }

        #endregion

        protected void btn_alumno_Click(object sender, EventArgs e)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Escuela.Vista
{
    public partial class Profesor : System.Web.UI.Page
    {
        string connectionString = @"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT u.usu_nombre, c.cur_nombre, n.not_nuno, n.not_ndos, n.not_ntres, n.not_examen FROM tbl_usuario u INNER JOIN tbl_estudiante e ON u.usu_id = e.usu_id LEFT JOIN tbl_nota n ON e.not_id = n.not_id LEFT JOIN tbl_curso c ON c.cur_id = e.cur_id;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvusuarios.DataSource = dt;
                gvusuarios.DataBind();
                con.Close();
            }
        }

        protected void btn_crear_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"..\CRUD\Crear.aspx");
        }
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"..\CRUD\Actualizar.aspx");
        }
        protected void btn_borrar_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"..\CRUD\Borrar.aspx");
        }
    }
}
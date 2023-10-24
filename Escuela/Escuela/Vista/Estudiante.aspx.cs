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
    public partial class Estudiante : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"workstation id=BD_Escuela.mssql.somee.com;packet size=4096;user id=Mateo2001_SQLLogin_1;pwd=Mateo2001;data source=BD_Escuela.mssql.somee.com;persist security info=False;initial catalog=BD_Escuela");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["correo"] != null)
                {
                    string correo = Request.QueryString["correo"].ToString();
                    CargarNotasDelEstudiante(correo);
                }
            }
        }
        private void CargarNotasDelEstudiante(string correoEstudiante)
        {
            con.Open();
            string query = "SELECT u.usu_nombre, n.not_nuno, n.not_ndos, n.not_ntres, n.not_examen FROM tbl_nota n INNER JOIN tbl_estudiante e ON n.not_id = e.est_id INNER JOIN tbl_usuario u ON e.usu_id = u.usu_id WHERE u.usu_correo = @correo";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@correo", correoEstudiante);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gv_notas.DataSource = dt;
                    gv_notas.DataBind();
                }
            }

        }

    }
}
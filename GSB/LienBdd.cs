using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;

namespace hippodrome
{
    class LienBdd
    {
        //
        // propriétés membres
        //
        private SqlConnection cn;
        private SqlCommand cde;
        private SqlDataAdapter da;
        private DataTable dt;
        //
        // méthodes
        //
        /// <summary>
        /// constructeur de la connexion
        /// </summary>
        public LienBdd()
        {
            try
            {
                // on commence par récupérer dans ch les informations contenues dans le fichier app.config
                // pour la connectionString de nom MaConnection
                string ch = ConfigurationManager.ConnectionStrings["MaConnection"].ConnectionString;
                cn = new SqlConnection(ch);
                cn.Open();
            }
            catch (SqlException)
            {
                throw new Exception("Erreur à la connexion");
            }
        }
        /// <summary>
        /// Méthode permettant de fermer la connexion
        /// </summary>
        public void FermerConnexion()
        {
            this.cn.Close();
        }
        ///// <summary>
        ///// permet de récupérer le contenu de la table course. 
        ///// </summary>
        ///// <returns>un objet de type datatable contenant les données récupérées</returns>
        public DataTable ObtenirCourses()
        {
            String req = "select * from course";
            this.cde = new SqlCommand(req, cn);
            da = new SqlDataAdapter();
            da.SelectCommand = this.cde;
            dt = new DataTable();
            //Le DataAdapter da va se charger ensuite de remplir la DataTable 
            da.Fill(dt);

            return dt;
        }

       
         public DataTable ObtenirCheveaux(){

            String req = "SELECT * FROM cheval";
            this.cde = new SqlCommand(req,cn);
            da = new SqlDataAdapter();
            da.SelectCommand = this.cde;
            dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable ObtenirParticipe(){
            
            String req = "SELECT * FROM participe";
            this.cde = new SqlCommand(req,cn);
            da = new SqlDataAdapter();
            da.SelectCommand = this.cde;
            dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable ObtenirResultat(string pIdCourse){

            string req = "SELECT nomche,place FROM participe INNER JOIN cheval ON participe.idche = cheval.id WHERE idcourse = @unIdCourse";
            this.cde = new SqlCommand(req, cn);
            this.cde.Parameters.Add("@unIdCourse", SqlDbType.VarChar).Value = pIdCourse;
            da = new SqlDataAdapter();
            da.SelectCommand = this.cde;
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        ///// <summary>
        ///// permet de mettre à jour  la table course  
        ///// </summary>

        public DataTable ObtenirCavaliers()
        {
            String req = "select * from cavalier";
            this.cde = new SqlCommand(req, cn);
            da = new SqlDataAdapter();
            da.SelectCommand = this.cde;
            dt = new DataTable();
            //Le DataAdapter da va se charger ensuite de remplir la DataTable 
            da.Fill(dt);

            return dt;
        }

        public void SupprimerCourse(string pIdCourse)
        {
            string req = "DELETE FROM course WHERE id = @unIdCourse";
            this.cde = new SqlCommand(req, cn);
            this.cde.Parameters.Add("@unIdCourse", SqlDbType.VarChar).Value = pIdCourse;

            this.cde.ExecuteNonQuery();

        }

        public void AjouterCourses(string pid, string phippodrome, DateTime pdate, int pdistance)
        {
            string req = "INSERT INTO course(id,hippodrome,date,distance) VALUES (@unIdCourse, @unHippo, @uneDate, @unDistance)";
            this.cde = new SqlCommand(req, cn);
            this.cde.Parameters.Add("@unIdCourse", SqlDbType.VarChar).Value = pid;
            this.cde.Parameters.Add("@unHippo", SqlDbType.VarChar).Value = phippodrome;
            this.cde.Parameters.Add("@uneDate", SqlDbType.VarChar).Value = pdate;
            this.cde.Parameters.Add("@unDistance", SqlDbType.VarChar).Value = pdistance;

            this.cde.ExecuteNonQuery();

        }

        public void MajCourse(string i, string pHippodrome, DateTime pDate, int pDistance)
        {
            
            string req = "UPDATE course SET hippodrome = @pHippodrome,date = @pdate, distance = @pDistance WHERE id = @i";
            this.cde = new SqlCommand(req, cn);
            this.cde.Parameters.Add("@i", SqlDbType.VarChar).Value = i;
            this.cde.Parameters.Add("@pHippodrome", SqlDbType.VarChar).Value = pHippodrome;
            this.cde.Parameters.Add("@pdate", SqlDbType.Date).Value = pDate;
            this.cde.Parameters.Add("@pDistance", SqlDbType.VarChar).Value = pDistance;

            this.cde.ExecuteNonQuery();

        }
    }
}
     
    
        


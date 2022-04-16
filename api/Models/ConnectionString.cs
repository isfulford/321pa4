using MySql.Data.MySqlClient;

namespace api.Models
{
    public class ConnectionString
    {
        public string cs {get;set;}

        public ConnectionString(){
            string server = "m7az7525jg6ygibs.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "pvzm5z2gsejvmagg";
            string port = "3306";
            string userName = "zx9b8z819k3qdr3f";
            string password = "z6d97axn1uu7o10g";

            cs = $@"server={server};user={userName};database={database};port={port};password={password};";

            using var con =new MySqlConnection(cs);
            con.Open();
            string stm = @"CREATE TABLE IF NOT EXISTS songs(id INTEGER NOT NULL AUTO_INCREMENT, title TEXT, time TIMESTAMP DEFAULT CURRENT_TIMESTAMP, deleted BOOLEAN DEFAULT false, PRIMARY KEY(id))";
            using var cmd = new MySqlCommand(stm, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
using FreeContacts.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FreeContacts.Data
{
    // Data Access Object
    internal class ContactDAO
    {
        private string connectionString = "Data Source=GMC031700;Initial Catalog=businessContactsDB;Integrated Security=True";


        // Buscar todos los contactos de la base de datos.
        public List<CRUDModel> FetchAll(string userid)
        {
            List<CRUDModel> returnList = new List<CRUDModel>();

            //Acceder a la base de datos
            try
            {
                // Establecer la conexion
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Crear el query de SQL
                String sql = "SELECT * FROM ContactsDb.contacts WHERE userid=@userid";

                // Pasar el string de query a ser un comando.
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@userid", userid);

                //Reader para leer el query.
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                     {
                      //Crea un objeto de contactos y le añade valores a sus atributos.
                      CRUDModel newContact = new CRUDModel();
                      newContact.id = reader.GetInt32(0);
                      newContact.name = reader.GetString(1);
                      newContact.email = reader.GetString(2); 
                      newContact.phone = reader.GetString(3);
                      newContact.address = reader.GetString(4);
                      newContact.created_at = reader.GetDateTime(5).ToString();
                                
                      //Anade el objeto a la lista.
                      returnList.Add(newContact);
                      }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }

            // Devuelve la lista.
            return returnList;
        }




        // Buscar solo un contacto.
        public CRUDModel FetchOne(int Id)
        {
            //Crea un objeto de contactos y le añade valores a sus atributos.
            CRUDModel newContact = new CRUDModel();

            //Acceder a la base de datos
            try
            {
                    // Establecer la conexion
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    
                    // Crear el query de SQL
                    String query = "SELECT * FROM ContactsDb.contacts WHERE Id = @id";
                    
                    // Pasar el string de query a ser un comando.
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //Asociacion de @id con el parametro del id
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;

                    //Reader para leer el query.
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                       {
                            newContact.id = reader.GetInt32(0);
                            newContact.name = reader.GetString(1);
                            newContact.email = reader.GetString(2);
                            newContact.phone = reader.GetString(3);
                            newContact.address = reader.GetString(4);
                            newContact.created_at = reader.GetDateTime(5).ToString();
                        }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }

            // Devuelve la lista.
            return newContact;
        }




        // Crear un contacto
        public void CreateOrUpdate(CRUDModel contactModel)
        {
            int newId = 0; 

            //Acceder a la base de datos
            try
            {
                // Establecer la conexion
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Crear el query de SQL
                    string query = "";

                    if (contactModel.id <= 0)
                    {
                        // Create
                       query = "INSERT INTO ContactsDb.contacts " +
                        "(name, email, phone, address, userid) VALUES " +
                        "(@name, @email, @phone, @address, @userid);";
                    }
                    else if (contactModel.id > 0)
                    {

                        // Update
                        query = "UPDATE ContactsDb.contacts " +
                                "SET name=@name, email=@email, phone=@phone, address=@address " +
                                "WHERE id=@id";
                    }
                        
                       

                    // Pasar el string de query a ser un comando.
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //Asociacion de datos con su @parametro.
                    cmd.Parameters.AddWithValue("@name", contactModel.name);
                    cmd.Parameters.AddWithValue("@email", contactModel.email);
                    cmd.Parameters.AddWithValue("phone", contactModel.phone);
                    cmd.Parameters.AddWithValue("@address", contactModel.address);
                    cmd.Parameters.AddWithValue("@id", contactModel.id);
                    cmd.Parameters.AddWithValue("@userid", contactModel.userid);

                    newId = cmd.ExecuteNonQuery();

                    }
                }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }




        // Borrar un contacto
        internal int Delete(int id)
        {
            int deletedId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM ContactsDb.contacts WHERE id=@id";


                    SqlCommand cmd = new SqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@id", id);

                    deletedId = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }

            return deletedId;
        }


    }
}
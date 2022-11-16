using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace FreeContacts.Models
{
    public class CRUDModel
    {
        public int id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string email { get; set; }

        [Required]
        [DisplayName("Phone #")]
        public string phone { get; set; }

        [Required]
        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Created At")]
        public string created_at { get; set; }

        public string userid { get; set; }

        public CRUDModel()
        {
            id = 0;
            name = "";
            email = "";
            phone = "";
            address = "";
            created_at = "";
        }

        public CRUDModel(int id, string name, string email, string phone, string address, string created_at)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.created_at = created_at;
        }
    }
}

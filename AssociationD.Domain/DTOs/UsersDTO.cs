using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationD.Domain.DTOs
{
    public class UsersDTO
    {
        public string _id { get; set; }

        public string civilite { get; set; }

        public string nom { get; set; }

        public string prenom { get; set; }

        public DateTime dateDeNaissance { get; set; }

        public string email { get; set; }

        public string telephone { get; set; }

        public string genre { get; set; }
    }
}

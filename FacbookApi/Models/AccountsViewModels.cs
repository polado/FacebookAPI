using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacbookApi.Models
{
    public class AccountsViewModels
    {
        public class ExternalLoginViewModel
        {
            [DataType(DataType.EmailAddress)]
            public string Mail { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class RegisterViewModel
        {
            public string UserFirstName { get; set; }

            public string UserLastName { get; set; }

            [DataType(DataType.EmailAddress)]
            public string UserMail { get; set; }

            [DataType(DataType.Password)]
            public string UserPassword { get; set; }

            public string UserAddress { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string UserPhone { get; set; }

            public DateTime UserDateOfBirth { get; set; }

            public string UserGender { get; set; }

            //public int UserRoleID { get; set; }
            
            public byte[] UserProfilePicture { get; set; }

        }

        public class EditPasswordViewModel
        {
            public int ID { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
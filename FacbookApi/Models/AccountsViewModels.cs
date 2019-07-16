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

        public class EditPasswordViewModel
        {
            public int ID { get; set; }
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
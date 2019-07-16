namespace FacbookApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table ("Users")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Friends = new HashSet<Friend>();
            Posts = new HashSet<Post>();
        }

        public int UserID { get; set; }

        [StringLength(50)]
        public string UserFirstName { get; set; }

        [StringLength(50)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(300)]
        [DataType(DataType.EmailAddress)]
        public string UserMail { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [StringLength(50)]
        public string UserAddress { get; set; }

        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UserDateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string UserGender { get; set; }

        public int UserRoleID { get; set; }

        public bool? UserIsBlocked { get; set; }

        public byte[] UserProfilePicture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Friend> Friends { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }

        public virtual Role Role { get; set; }
    }
}

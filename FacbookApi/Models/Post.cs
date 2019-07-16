namespace FacbookApi.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        public int PostID { get; set; }

        public int PostUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime PostDate { get; set; }

        public string PostContent { get; set; }

        public bool? PostIsDeleted { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}

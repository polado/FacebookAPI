namespace FacbookApi.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Friend
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FriendSenderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FriendRecevierID { get; set; }

        public bool FriendState { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }

    public enum FriendState
    {
        Friends = 1,
        Request = 0
    }
}

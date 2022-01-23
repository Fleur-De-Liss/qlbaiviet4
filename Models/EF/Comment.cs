namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int CommentId { get; set; }

        [StringLength(255)]
        public string Ccontent { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}

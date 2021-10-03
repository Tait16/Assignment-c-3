using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ASSIGNMENT.Models
{
    [Table("DanhBa")]
    public partial class DanhBa
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Sdt1 { get; set; }
        [StringLength(20)]
        public string Sdt2 { get; set; }
        [StringLength(50)]
        public string GhiChu { get; set; }
        public int? IdNguoi { get; set; }

        [ForeignKey(nameof(IdNguoi))]
        [InverseProperty(nameof(Nguoi.DanhBas))]
        public virtual Nguoi IdNguoiNavigation { get; set; }
    }
}

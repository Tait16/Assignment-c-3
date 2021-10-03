using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ASSIGNMENT.Models
{
    [Table("Nguoi")]
    public partial class Nguoi
    {
        public Nguoi()
        {
            DanhBas = new HashSet<DanhBa>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string Ho { get; set; }
        [StringLength(10)]
        public string TenDem { get; set; }
        [StringLength(10)]
        public string Ten { get; set; }
        public int? NamSinh { get; set; }
        public bool? GioiTinh { get; set; }

        [InverseProperty(nameof(DanhBa.IdNguoiNavigation))]
        public virtual ICollection<DanhBa> DanhBas { get; set; }
    }
}

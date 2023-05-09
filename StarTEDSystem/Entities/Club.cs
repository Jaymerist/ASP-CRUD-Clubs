﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StarTEDSystem.Entities
{
    [Index("EmployeeID", Name = "IX_EmployeeID")]
    public partial class Club
    {
        [Key]
        [StringLength(10)]
        [Unicode(false)]
        public string ClubID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string ClubName { get; set; }
        public bool Active { get; set; }
        public int? EmployeeID { get; set; }
        [Column(TypeName = "money")]
        public decimal Fee { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("Clubs")]
        public virtual Employee Employee { get; set; }
    }
}
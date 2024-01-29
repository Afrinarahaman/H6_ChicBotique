﻿
using H6_ChicBotique.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace H5_Webshop.Database.Entities
{
    public class User //Basically profile
    {

        [Key]
        public int Id { get; set; }  //Unique Key for identifying each entity.

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; } 

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }



        [Column(TypeName = "nvarchar(128)")]
        public string Email { get; set; }



        // Role is an Enum datatype which consists set of constant values(Admin, Member, Guest )
        public Role Role { get; set; }
        public AccountInfo Account { get; set; } /////Navigational object for getting Account table's data from this class

    }

}

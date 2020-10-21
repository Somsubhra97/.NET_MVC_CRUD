using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_MVC.Models
{
    public class DesignationModel
    {

        public int Id { get; set; }

        [Required]
        public string Designation_Emp { get; set; }



    }
}
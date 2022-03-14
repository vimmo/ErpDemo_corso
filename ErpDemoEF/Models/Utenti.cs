using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ErpDemoEF.Models
{
    public partial class Utenti
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
}

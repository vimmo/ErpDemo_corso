using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ErpDemoEF.Models
{
    public partial class Clienti
    {
        public int Id { get; set; }
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Settore { get; set; }
    }
}

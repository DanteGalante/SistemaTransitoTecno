//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseDeDatos
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Fotografia
    {
        public int idFotografia { get; set; }
        public byte[] imagen { get; set; }
    
        public virtual Reporte Reporte { get; set; }
    }
}

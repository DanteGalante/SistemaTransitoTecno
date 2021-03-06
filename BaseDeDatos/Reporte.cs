//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseDeDatos
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Reporte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reporte()
        {
            this.Vehiculos = new ObservableCollection<Vehiculo>();
            this.Fotografias = new ObservableCollection<Fotografia>();
        }
    
        public int idReporte { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha { get; set; }
        public string estatus { get; set; }
    
        public virtual Dictamen Dictamen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Vehiculo> Vehiculos { get; set; }
        public virtual DelegacionMunicipal DelegacionMunicipal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Fotografia> Fotografias { get; set; }
    }
}

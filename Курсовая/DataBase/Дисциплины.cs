//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Курсовая.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Дисциплины
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Дисциплины()
        {
            this.Журнал = new HashSet<Журнал>();
            this.Занятие = new HashSet<Занятие>();
        }
    
        public int ID_дисциплины { get; set; }
        public string Название_дисциплины { get; set; }
        public Nullable<int> ID_преподавателя { get; set; }
        public string Код_кафедры { get; set; }
    
        public virtual Преподаватели Преподаватели { get; set; }
        public virtual Кафедры Кафедры { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Журнал> Журнал { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Занятие> Занятие { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Konsolika.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int kod { get; set; }
        public int kod_product { get; set; }
        public int Kod_customer { get; set; }
        public System.DateTime Order_data { get; set; }
        public string Order { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Product_list Product_list { get; set; }
    }
}

namespace QuanLyNongSan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(50)]
        [Display(Name = "Họ và Tên")]
        public string ShipName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string ShipMobile { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string ShipAddress { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string ShipEmail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

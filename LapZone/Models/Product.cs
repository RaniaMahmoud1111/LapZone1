using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LapZone.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Product Name")]
    [Column(TypeName = "varchar(250)")]
    public string ProductName { get; set; } = string.Empty;

    [Column("_Description", TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal Price { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    public int StockQuantity { get; set; }
    [Display(Name = "Product Image ")]
    [Column(TypeName = "varchar(250)")]

    public string? ImagePath { get; set; }
    //laptopDetails

    [StringLength(50)]
    public string? Brand { get; set; }

    [Column("CPU")]
    [StringLength(100)]
    public string? CPU { get; set; }

    [StringLength(50)]
    public string? Storage { get; set; }

    [Column("RAM")]
    [StringLength(50)]
    public string? RAM { get; set; }

    [Column("GPU")]
    [StringLength(50)]
    public string? GPU { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? ScreenSize { get; set; }

    [Column("_Weight", TypeName = "decimal(5, 2)")]
    // public decimal? Weight { get; set; }

    //public bool? HasWebcam { get; set; }


    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    // [InverseProperty("Product")]
    //public virtual LaptopDetail LaptopDetail { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    //[InverseProperty("Product")]
    //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Product")]
    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    [NotMapped]
    public IFormFile? clientFile { get; set; }
    [NotMapped]
    public string? SearchTerm { get; set; } = "";


}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
 
namespace csmvc.Models;
public class Product
{
    [Column("product_id")]
    public int ProductId { get; set; }  //auto match with product_id
 
    [Column("category_id")]
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
 
    public decimal Price { get; set; }
    public int Stock { get; set; }
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    public string Status { get; set; } = "Active";
    public string? slug { get; set; }
 /*
    public string? Slug { get; set; }
 
    public double revenue { get; set; }  //this is private info that not show to user
    public int provider_id { get; set; }  //this is private info that not show to user*/
 
}
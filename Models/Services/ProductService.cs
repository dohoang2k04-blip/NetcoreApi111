using System.Text.Json;
using csmvc.Services;
using csmvc.Data;
using csmvc.Models;
 using System.Text.RegularExpressions;
public class ProductService
{
    private readonly ExternalService _externalService;
    private readonly ApplicationDbContext _db;
 
    public ProductService(ExternalService externalService ,ApplicationDbContext db)
    {
        _externalService = externalService;
       _db = db;
    }
    public List<ProductDto> getDummyProducts()
    {
        List<ProductDto> products = new List<ProductDto>();
        Dictionary<string, JsonElement> rawDict = _externalService.sendGetRequest("https://dummyjson.com/products");
        if (!rawDict.TryGetValue("products", out JsonElement productsElement))
        {
            throw new Exception("JSON does not contain 'products'");
        }
        List<Dictionary<string, JsonElement>> rawProducts = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(rawDict["products"])!;
        foreach (var item in rawProducts)
        {
            var product = new ProductDto
            {
                Id = item["id"] is JsonElement jeId ? jeId.GetInt32() : 0,
                Name = item["title"] is JsonElement jeName ? jeName.GetString() ?? "" : "",
                Price = item["price"] is JsonElement jePrice ? jePrice.GetDouble() : 0,
                description = item["description"] is JsonElement jeDesc ? jeDesc.GetString() ?? "" : "",
                image_url = item["thumbnail"] is JsonElement jeImg ? jeImg.GetString() ?? "" : "",
                category_name = item["category"] is JsonElement jeCategory ? jeCategory.GetString() ?? "" : "",
            };
            products.Add(product);
        }
        return products;
    } 
    public List<Product> getAllProducts()
    {
        return _db.Product.ToList();
    }

    public List<CategoryDto> getDummyCategory()
    {
        JsonElement rawDict = _externalService.sendGetRequestElement("https://dummyjson.com/products/category-list");
        List<string> rawCategory = JsonSerializer.Deserialize<List<string>>(rawDict)!;
        return rawCategory.Select(x => new CategoryDto { Name = x }).ToList();
    }   

    public ProductDto getDummyDetailProducts()
    {
        Dictionary<string, JsonElement> rawDict = _externalService.sendGetRequest("https://dummyjson.com/products/1");
        
       
            var product = new ProductDto
            {
                Id = rawDict["id"] is JsonElement jeId ? jeId.GetInt32() : 0,
                Name = rawDict["title"] is JsonElement jeName ? jeName.GetString() ?? "" : "",
                Price = rawDict["price"] is JsonElement jePrice ? jePrice.GetDouble() : 0,
                description = rawDict["description"] is JsonElement jeDesc ? jeDesc.GetString() ?? "" : "",
                image_url = rawDict["thumbnail"] is JsonElement jeImg ? jeImg.GetString() ?? "" : ""
            };
            
        
        return product;
    } 
    public Product? GetProductDetailById(int id)
    {
        return _db.Product.FirstOrDefault(p => p.ProductId == id); //return null if not found
    }

    public Product? GetProductDetailBySlug(string slug)
    {
        return _db.Product.FirstOrDefault(p => p.slug == slug); //return null if not found
    }

    public List<Product> SearchProducts(string keyword)
    {
        var query = _db.Product
        .Where(p => p.Name.Contains(keyword)); //case insensitive
        return query.ToList();
    }

    public void convertSlugForAllProducts()
    {
        var items = _db.Product.ToList();
        //lấy tất cả sản phẩm
        foreach (var product in items)
        {
            product.slug = Utils.ToSlug(product.Name);
            //đổi tên sản phẩm qua slug
            _db.Product.Update(product);
            //lưu lại sản phẩm với slug mới
            _db.SaveChanges();
        }
    }
    private readonly string[] allowedImgExtensions = { ".jpg", ".jpeg", ".png" };
    string validateImageFile(IFormFile? file){
    if (file == null || file.Length == 0)
        return "No file uploaded.";
    var ext = Path.GetExtension(file.FileName).ToLower();
    if (!allowedImgExtensions.Contains(ext))
        return "Only image file is allowed.";
    if (!file.ContentType.StartsWith("image/"))
        return "Invalid file type.";
    if (file.Length > 5 * 1024 * 1024) //maximum 5MB
        return "File too large.";
    //add more validation, if any
        return ""; //this file is a valid image file
    }

}
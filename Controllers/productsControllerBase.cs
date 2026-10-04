
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
    using csmvc.Services;
    using System.Text.Json;
    using csmvc.Models;
    using csmvc.Data;
[Route("api/product")] //đường dẫn API chung cho controller này
public class ProductsControllerBase : ControllerBase

{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _db;
    public ProductsControllerBase(ProductService productService,ApplicationDbContext db)
    {
        _productService = productService;
        _db = db;
    }
    
    [HttpGet]
//API này dùng phương thức GET
    [Route("test")] //API riêng cho API này “/api/product/test”
    public IActionResult test(string name)
    {
        string receivedName = "Received: " + name;
        return Ok(receivedName); //mã kết quả thành công 200
    }
    [HttpGet]
    [Route("pd")]
   public IActionResult getProductsFromExternalSource()
   {
       var products = _productService.getDummyProducts();
       return Ok(products);
   }
   [HttpGet]
    [Route("ctgr")]
   public IActionResult getcategory()
   {
       var products = _productService.getDummyCategory();
       return Ok(products);
   }
   [Route("ctgr1")]
   public List<Product> getAllProducts()
    {
        return _db.Product.ToList();
    }

    [HttpPut]
    [Route("convert-slug")]
    public IActionResult convertSlugForAllProducts()
    {
         _productService.convertSlugForAllProducts();
        return Ok();
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
    private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    [HttpPost]

    [Route("upload_image")]

    public async Task<IActionResult> UploadImage(){

        var file = Request.Form.Files.FirstOrDefault();
        string errorValidation = validateImageFile(file);
        if (errorValidation != "")
            return BadRequest(errorValidation);
        var filePath = Path.Combine(folderPath, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        // Save file to server, at defined folder
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{file.FileName}";
            return Ok(new{
            message = "Upload successful",

            url = fileUrl //public url

        });

    }

}
namespace csmvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using csmvc.Services;
    using System.Text.Json;
    using csmvc.Data;
    using csmvc.Models;
    //[Route("api/product")]
    public class productsController : Controller
    {
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _db;
    public productsController(ProductService productService,ApplicationDbContext db)
    {
        _productService = productService;
        _db = db;
    }
        public IActionResult detail(int id)
    {
        ProductDto sampleProduct = new ProductDto
        {
            Id = 1,
            Name = "Keyboard",
            Price = 20.5,
            description = "Mechanical keyboard",
            image_url = "https://images.pexels.com/photos/585752/pexels-photo-585752.jpeg",
            category_id = 1
        };
        ViewBag.detail = sampleProduct; //inject this info to view
        return View();
    }
    public List<ProductDto> getSampleList()
    {
        // Sample product list
        var products = new List<ProductDto>
        {
            new ProductDto
            {
                Id = 1,
                Name = "Keyboard",
                Price = 20.5,
                description = "Mechanical keyboard",
                image_url = "https://images.pexels.com/photos/585752/pexels-photo-585752.jpeg",
                category_id = 1
            },
            new ProductDto
            {
                Id = 2,
                Name = "Mouse",
                Price = 10.0,
                description = "Wireless mouse",
                image_url = "https://images.pexels.com/photos/119550/pexels-photo-119550.jpeg",
                category_id = 2
            }
        };
        return products;
    }
 
    public IActionResult list()
    {
        ViewBag.products = getSampleList();
        //sample categories
        ViewBag.categories = new List<string> { "Keyboard", "Mouse", "PC", "Printer" };
        //
        return View();
    }
    [Route("ogani/shopGrid")]
    public IActionResult ExternalList(int page=1)
    {
        const int pageSize = 3;
        var products = _productService.getDummyProducts();
        ViewBag.products = _productService.getDummyProducts();
        //sample categories
        ViewBag.categories = new List<string> { "Keyboard", "Mouse", "PC", "Printer" };
        ViewBag.category = _productService.getDummyCategory();
        ViewBag.pd= _productService.getAllProducts();
        //
        return View("~/Views/ogani/shop-grid.cshtml");
    }
    [Route("ogani/shopDetails/{Id}")]
    public IActionResult shopDetails(int Id)
    {
        var products = _productService.getDummyProducts();
        ViewBag.products = _productService.getDummyProducts();
        ViewBag.product = _productService.getDummyDetailProducts();
        ViewBag.pd= _productService.getAllProducts();
        ViewBag.product1 = _productService.GetProductDetailById(Id);
        
        //sample categories
        ViewBag.categories = new List<string> { "Keyboard", "Mouse", "PC", "Printer" };
        //
        return View("~/Views/ogani/shop-details.cshtml");
    }
    [Route("ogani/product/{slug}")]
    public IActionResult getProductDetailBySlug(string slug)
    {
        ViewBag.prd = _productService.GetProductDetailBySlug(slug);
        return View("~/Views/ogani/shop-details.cshtml");
    }

    [Route("products/search")]

    public IActionResult searchProducts(string keyword)
    {
        ViewBag.products = _productService.SearchProducts(keyword);

        return View("~/Views/ogani/checkout.cshtml");
    }

   /* [Route("products/detail/{id}")]
    public IActionResult getProductDetailById(int id)
    {
        ViewBag.product = _productService.GetProductDetailById(id);
        return View("~/Views/ogani/shop-details.cshtml");

    }*/



    [HttpGet]
   [Route("external/list")]
   public IActionResult getProductsFromExternalSource()
   {
       var products = _productService.getDummyProducts();
       //return Ok(products);
       return View("~/Views/ogani/shop-grid.cshtml");
   }
    
    [HttpGet]
   [Route("dashmin/index")]
   public IActionResult gete()
   {
       var products = _productService.getDummyProducts();
       //return Ok(products);
       return View("~/Views/dashmin/chart.cshtml");
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

        /*
        [Route("discount")]
        public Dictionary<string, object> list()
        {
            

            return new Dictionary<string, object>
            {
                { "name", "Keyboard" },
                { "price", 20.5 },
                { "description", "Mechanical keyboard" }
            };


        }

        [Route("discount1")]
    public JsonResult UpdatePrice(double newPrice)
    {
        // 1. Lấy dữ liệu mẫu
        var product = list();

        // 2. Cập nhật giá mới
        // Vì Dictionary là <string, object>, ta gán trực tiếp giá trị mới vào key "price"
        product["price"] = newPrice;

        // 3. Trả về kết quả dưới dạng JSON
        return Json(product);
    }

        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
    }*/
    }}
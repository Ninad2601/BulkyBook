using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            //Before using VM
            /* Product productObj = new Product();
             IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll().Select(c=> new SelectListItem
             {
                 Text = c.Name,
                 Value = c.Id.ToString(),
             });
             IEnumerable<SelectListItem> CoverTypeList = _unitofwork.CoverType.GetAll().Select(c => new SelectListItem
             {
                 Text = c.Name,
                 Value = c.Id.ToString(),
             });
            */
            //To Bind dropdown of category & cover type
            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                CoverTypeList = _unitofwork.CoverType.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
            };

            if (id == null || id == 0)
            {
                //Create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                //Update product
            }
            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ProductVMS.ImageUrl = @"\Images\Products\" + fileName + extension;
                }
                _unitofwork.Product.Add(obj.ProductVMS);
                _unitofwork.Save();
                TempData["Success"] = "Added Successfully !!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDbFirst = _unitofwork.CoverType.GetFirstOrDefault(x => x.Id == id);

            //Check if categories are null
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //   var obj = _db.Categories.Find(id);
            var obj = _unitofwork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.CoverType.Remove(obj);
            _unitofwork.Save();
            TempData["Success"] = "Deleted Successfully !!";
            return RedirectToAction("Index");
        }
        #region API Call 
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var productList = _unitofwork.Product.GetAll();
            return Json(new { data = productList });
        }

        #endregion

    }
};

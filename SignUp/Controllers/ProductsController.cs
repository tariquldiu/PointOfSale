using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllerss
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        [Route("api/Products/Get")]
        public IHttpActionResult Get()
        {
            //var model = new Category();
            //model.CategoriesList =db.Categorys.ToList().Select(c => new SelectListItem()
            //{
            //    Value=c.CategoryId.ToString(),
            //    Text=c.CategoryName
            //}).ToList();

            var PList = db.Products.ToList();
            var CatList = db.Categorys.ToList();
            var TotalList = from c in CatList
                            join p in PList on c.CategoryId equals p.CategoryId
                            select new
                            {
                                c.CategoryName,
                                c.CategoryType,
                                p.CategoryId,
                                p.Description,
                                p.MarkupPrice,
                                p.ProductName,
                                p.ProductNo,
                                p.ProductQty,
                                p.Status,
                                p.OriginalPrice,
                                p.ProductId
                            };
            //var listObj = db.Products.ToList();
            //List<Product> products = new List<Product>();

            //foreach (var item in listObj)
            //{
            //    products.Add(new Product
            //    {
            //        CateName=item.Categorys.CategoryName,
            //        Description=item.Description,
            //        MarkupPrice=item.MarkupPrice,
            //        ProductNo=item.ProductNo,
            //        ProductName=item.ProductName,
            //        OriginalPrice=item.OriginalPrice,
            //        ProductId=item.ProductId,
            //        Status=item.Status,

            //    });
            //}

            return Json(TotalList);
        }
        // GET: api/Categorys
        [HttpGet]
        [Route("api/Products/GetCategorys")]
        public IQueryable<Category> GetCategorys()
        {
            return db.Categorys;
        }

        // GET: api/Categorys/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        // POST: api/Categorys
        [HttpPost]
        [Route("api/Products/PostCategory")]
        public IHttpActionResult PostCategory(List<Product> product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // product.CategoryId = 2;


            foreach (var item in product)
            {
                if (item.ProductId == 0)
                {
                    db.Products.Add(item);
                    db.SaveChanges();
                }
                else
                {

                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    // db.Products.AddOrUpdate(item);
                }


            }


            return CreatedAtRoute("DefaultApi", new { product }, product);
        }
        [HttpDelete]
        //[Route("/api/Categorys/DeleteCategorys/{id}")]
        [Route("api/Products/DeleteCategorys/{id}")]
        public IHttpActionResult DeleteCategorys(int id)
        {

            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with ProductId= " + id.ToString() + " is not found"));

                }
                else
                {

                    db.Products.Remove(product);
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Product Deleted"));
                }

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}

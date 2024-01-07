using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopping.Models;

namespace shopping.Controllers
{

    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>();

        public IActionResult Index()
        {


            AddProductViewModel model = new AddProductViewModel();
            model.ProductList = _products;
            return View(model);
        }

        [HttpGet]
        public IActionResult GetProductlist()
        {
       
            return Json(_products);

        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            try
            {

                if (!newProduct.Id.HasValue)
                {
                    int maxProductId = _products.Count == 0 ? 0 : _products.Max(x => x.Id.GetValueOrDefault());
                    // Assign a unique ID to the new product
                    newProduct.Id = maxProductId + 1;
                    // Add the new product to the list
                    _products.Add(newProduct);
                }
                //else
                //{
                  
                //}

                // Return the newly added product

                return Json(JsonConvert.SerializeObject(newProduct));

            }
            catch (Exception ex)
            {

                // If the model state is not valid, return bad request with validation errors
                return BadRequest();
            }

        }
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product updatedProduct)
        {
            try
            {
                // If the updatedProduct object has an ID, search for an existing product with the same ID and update its properties
                var product = _products.FirstOrDefault(x => x.Id == updatedProduct.Id);

                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;

                    // Return the updated product as JSON
                    return Json(JsonConvert.SerializeObject(product));
                }
                else
                {
                    // If the product with the specified ID is not found, return not found response
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a bad request response
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound(); // Customer not found
            }
           
            return Json(product);
          
        }




       
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
           var product = _products.FirstOrDefault(x=> x.Id == id);
                // remove  product from the list
                _products.Remove(product);
            return Json(JsonConvert.SerializeObject(_products));
        }


    }

}


using Microsoft.AspNetCore.Razor.Language.Intermediate;
using shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace shopping.Controllers
{
    public class SaleController : Controller
    {
        private static List<Sale> _sales = new List<Sale>();
        
         public IActionResult Index()
        {

            return View (_sales);
        }


        [HttpGet]
        public IActionResult GetSalelist()
        {
            return Json(_sales);

        }


        [HttpPost]
        public IActionResult Addsale([FromBody] Sale newSale)
        {
            try
            {

                if (!newSale.Id.HasValue)
                {
                    int maxSaleId = _sales.Count == 0 ? 0 : _sales.Max(x => x.Id.GetValueOrDefault());
                    // Assign a unique ID 
                    newSale.Id = maxSaleId + 1;
                    
                    _sales.Add(newSale);
                }
                //else
                //{
                //    UpdateCustomer(newCustomer);
                //}

                // Return the newly added customer

              return Json(JsonConvert.SerializeObject(newSale));
             //return RedirectToAction("GetSalelist");
            }
            catch (Exception ex)
            {

                // If the model state is not valid, return bad request with validation errors
                return BadRequest();
            }

        }
        [HttpPut]
        public IActionResult UpdateSale([FromBody] Sale updatedSale)
        {
            try
            {
             
                var sale = _sales.FirstOrDefault(x => x.Id == updatedSale.Id);

                if (sale != null)
                {
                    sale.CustomerId = updatedSale.CustomerId;
                    sale.ProductId = updatedSale.ProductId;
                    sale.Quantity = updatedSale.Quantity;
                    sale.TotalPrice = updatedSale.TotalPrice;


                    return Json(JsonConvert.SerializeObject(sale));
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


        [HttpDelete]
        public IActionResult DeleteSale(int id)
        {
            var Sale = _sales.FirstOrDefault(x => x.Id == id);
            // remove the sale from the list
            _sales.Remove(Sale);
            return Json(JsonConvert.SerializeObject(_sales));
        }



        [HttpGet]
        public IActionResult GetSale(int id)
        {
            var sale = _sales.FirstOrDefault(x => x.Id == id);

            if (sale == null)
            {
                return NotFound(); // sale not found
            }

            return Json(sale);
        }
    }
}
   
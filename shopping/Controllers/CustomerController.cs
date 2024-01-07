using Microsoft.AspNetCore.Mvc;
using shopping.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace shopping.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> _customers = new List<Customer>();

        public IActionResult Index()
        {


            AddCustomerViewModel model = new AddCustomerViewModel();
            model.CustomerList = _customers;
            return View(model);
        }

        [HttpGet]
        public IActionResult GetCustomerlist()
        {
            return Json(_customers);

        }
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer newCustomer)
        {
            try
            {

                if (!newCustomer.Id.HasValue)
                {
                    int maxCustomerId = _customers.Count == 0 ? 0 : _customers.Max(x => x.Id.GetValueOrDefault());
                    // Assign a unique ID to the new customer
                    newCustomer.Id = maxCustomerId + 1;
                    // Add the new customer to the list
                    _customers.Add(newCustomer);
                }
                //else
                //{
                //    UpdateCustomer(newCustomer);
                //}

                // Return the newly added customer

                return Json(JsonConvert.SerializeObject(newCustomer));

            }
            catch (Exception ex)
            {

                // If the model state is not valid, return bad request with validation errors
                return BadRequest();
            }

        }
        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer updatedCustomer)
        {
            try
            {
                // If the updatedCustomer object has an ID, search for an existing customer with the same ID and update its properties
                var customer = _customers.FirstOrDefault(x => x.Id == updatedCustomer.Id);

                if (customer != null)
                {
                    customer.Name = updatedCustomer.Name;
                    customer.Email = updatedCustomer.Email;

                    // Return the updated customer as JSON
                    return Json(JsonConvert.SerializeObject(customer));
                }
                else
                {
                    // If the customer with the specified ID is not found, return not found response
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
        public IActionResult GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound(); // Customer not found
            }

            return Json(customer);
        }

       
        [HttpDelete]
        public IActionResult Deletecustomer(int id)
        {
            var Customer = _customers.FirstOrDefault(x => x.Id == id);
            // remove the new product from the list
            _customers.Remove(Customer);
            return Json(JsonConvert.SerializeObject(_customers));
        }
        
       
    }
}

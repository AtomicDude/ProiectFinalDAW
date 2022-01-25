using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.OrderRepository;
using ProiectFinalDAW.Repositories.UserRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Models.DTOs;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private IOrderRepository orderRepository;
        private IUserRepository userRepository;

        public OrderController(IOrderRepository orderR, IUserRepository userR)
        {
            orderRepository = orderR;
            userRepository = userR;
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            var new_order = new Order
            {
                Email_address = order.Email_address,
                Phone_number = order.Phone_number,
                Address = order.Address,
                Status = order.Status,
                User = order.User
            };

            orderRepository.Create(new_order);
            var result = orderRepository.Save();

            if (result)
                return Ok();
            else
                return BadRequest(new { message = "Eroare" });
        }

        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "User is not logged in" });
            }
            var orders = userRepository.GetAllOrders(user.Username);
            var dto = new AllOrdersDTO()
            {
                Orders = new List<OrderDTO>()
            };
            
            foreach (var order in orders.Orders)
            {
                var orderDTO = new OrderDTO()
                {
                    Products = new List<ProductDTO>(),
                    Adresa = order.Address,
                    Status = order.Status.ToString()
                };
                foreach (var orderDetail in order.OrderDetails)
                {
                    var productDTO = new ProductDTO()
                    {
                        BarCode = orderDetail.Product.BarCode,
                        Product_Name = orderDetail.Product.Name,
                        Price = orderDetail.Product.Price,
                        Quantity = orderDetail.Quantity
                    };
                    orderDTO.Products.Add(productDTO);
                }
                dto.Orders.Add(orderDTO);
            } 
            return Ok(dto);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.OrderRepository;
using ProiectFinalDAW.Repositories.UserRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Models.DTOs;
using ProiectFinalDAW.Utility;

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

        /*
        [HttpPost]
        public IActionResult Post(Order order)
        {
            var new_order = new Order
            {
                Order_Number = Order_Counter.Order_Number + 1,
                Email_address = order.Email_address,
                Phone_number = order.Phone_number,
                Address = order.Address,
                Status = order.Status,
                User = order.User
            };

            Order_Counter.Order_Number += 1;

            orderRepository.Create(new_order);
            var result = orderRepository.Save();

            if (result)
                return Ok();
            else
                return BadRequest(new { message = "Eroare" });
        }*/

        [HttpGet("{order_number}")]
        public IActionResult GetOrder(int order_number)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Utilizatorul nu este logat" });
            }
            var order = orderRepository.GetByOrderNumber(order_number);

            if (order == null)
                return BadRequest(new { message = "Comanda cu numarul introdus nu exista" });

            var orderDTO = new OrderDTO()
            {
                Order_Number = order.Order_Number,
                Adresa = order.Address,
                Status = order.Status.ToString(),
                Products = new List<ProductDTO>()
            };

            foreach (var orderdetail in order.OrderDetails)
            {
                var productDTO = new ProductDTO()
                {
                    BarCode = orderdetail.Product.BarCode,
                    Product_Name = orderdetail.Product.Name,
                    Price = orderdetail.Product.Price,
                    Quantity = orderdetail.Quantity
                };
                orderDTO.Products.Add(productDTO);
            }

            return Ok(orderDTO);
        }

        [HttpGet("show_all")]
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
                    Order_Number = order.Order_Number,
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

        [HttpGet("show/{order_number}")]
        [Authorization(role.Admin)]
        public IActionResult GetOrderByNumber(int order_number)
        {
            var order = orderRepository.GetByOrderNumber(order_number);
            if (order == null)
                return BadRequest(new { message = "Comanda cu numarul introdus nu exista" });

            var orderDTO = new OrderDTO()
            {
                Order_Number = order.Order_Number,
                Adresa = order.Address,
                Status = order.Status.ToString(),
                Products = new List<ProductDTO>()
            };

            foreach (var orderdetail in order.OrderDetails)
            {
                var productDTO = new ProductDTO()
                {
                    BarCode = orderdetail.Product.BarCode,
                    Product_Name = orderdetail.Product.Name,
                    Price = orderdetail.Product.Price,
                    Quantity = orderdetail.Quantity
                };
                orderDTO.Products.Add(productDTO);
            }

            return Ok(orderDTO);
        }

        [HttpPut("update")]
        public IActionResult UpdateOrderStatus(UpdateOrderStatusDTO dto) 
        {
            var order = orderRepository.GetByOrderNumber(dto.Order_Number);
            if (order == null)
                return BadRequest(new { message = "Comanda cu numarul introdus nu exista" });

            if (!string.IsNullOrEmpty(dto.Status))
            {
                order.Status = (Order_Status)Enum.Parse(typeof(Order_Status), dto.Status);
            }

            orderRepository.Update(order);
            var result = orderRepository.Save();

            if (result)
                return Ok(new{ message =  "Statusul comenzii a fost actualizat" });
            return BadRequest(new { message = "Eroare" });
        }
    }
}


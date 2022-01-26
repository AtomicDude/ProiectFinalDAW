using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.OrderRepository;
using ProiectFinalDAW.Repositories.OrderDetailsRepository;
using ProiectFinalDAW.Repositories.UserRepository;
using ProiectFinalDAW.Repositories.ProductRepository;
using ProiectFinalDAW.Repositories.FavouriteAddressRepository;
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
        private IFavouriteAddressRepository favouriteAddressRepository;
        private IOrderDetailsRepository orderDetailsRepository;
        private IUserRepository userRepository;
        private IProductRepository productRepository;

        public OrderController(IOrderRepository orderR, IUserRepository userR, IProductRepository productR, IOrderDetailsRepository orderDetailsR, IFavouriteAddressRepository favouriteAddressR)
        {
            orderRepository = orderR;
            userRepository = userR;
            productRepository = productR;
            orderDetailsRepository = orderDetailsR;
            favouriteAddressRepository = favouriteAddressR;
        }

        [HttpPost]
        public IActionResult Post(NewOrderDTO dto)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Utilizatorul nu este logat" });
            }

            var order_no = Order_Counter.get_order_number();
            Order_Counter.counter();

            var get_fav_add = userRepository.GetUserAndFavouriteAddress(user.Username);

            var new_order = new Order
            {
                Order_Number = order_no,
                Email_address = user.Email_Address,
                Phone_number = string.IsNullOrEmpty(dto.Phone_Number) ? user.Phone_Number : dto.Phone_Number,
                Address = string.IsNullOrEmpty(dto.Address) ? get_fav_add.FavouriteAddress.Fav_Address : dto.Address,
                Status = Order_Status.Received,
                UserId = user.Id
            };

            orderRepository.Create(new_order);
            var result = orderRepository.Save();

            if (result)
            {
                var order = orderRepository.GetByOrderNumber(order_no);

                if (order == null)
                {
                    return BadRequest(new { message = "Eroare 1" });
                }
                foreach (var product in dto.Products)
                {
                    var prod = productRepository.GetByProductBarCode(product.BarCode);
                    
                    if (prod == null)
                        return BadRequest(new { message = "Produsul nu exista" });
                    var order_detail = new OrderDetail
                    {
                        ProductId = prod.Id,
                        Quantity = product.Quantity,
                        OrderId = order.Id
                    };

                    orderDetailsRepository.Create(order_detail);

                    result = orderDetailsRepository.Save();

                    if (!result)
                    {
                        return BadRequest(new { message = "Eroare 2" });
                    }
                }
            }
            
            return Ok();
        }

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


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.UserRepository;
using ProiectFinalDAW.Repositories.OrderRepository;
using ProiectFinalDAW.Repositories.FavouriteAddressRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Utility;
using ProiectFinalDAW.Models.DTOs;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Globalization;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;
        private IFavouriteAddressRepository favouriteAddress;
        private IOrderRepository orderRepository;
        private IJWTutils jwtUtils;

        public UserController(IUserRepository userR, IJWTutils jwtUti, IFavouriteAddressRepository fav_addR, IOrderRepository orderR)
        {
            userRepository = userR;
            jwtUtils = jwtUti;
            favouriteAddress = fav_addR;
            orderRepository = orderR;
        }

        [HttpGet("{user}")]
        public IActionResult Get(string user)
        {
            var user_name = userRepository.GetByUsername(user);
            if (user_name == null)
                return BadRequest(new { message = "Utilizatorul introdus nu exista" });
            return Ok(user_name);
        }

        [HttpGet]
        [Authorization(role.User, role.Admin)]
        public IActionResult GetMyUser()
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "User does not exist" });
            }
            var fav_add_and_user = userRepository.GetUserAndFavouriteAddress(user.Username);
            if (fav_add_and_user == null)
            {
                return BadRequest(new { message = "User doesn't have a favourite address" });
            }
            UserAndFavAddDTO dto = new UserAndFavAddDTO
            {
                Username = fav_add_and_user.Username,
                Email_Address = fav_add_and_user.Email_Address,
                Phone_Number = fav_add_and_user.Phone_Number,
                First_Name = fav_add_and_user.First_Name,
                Last_Name = fav_add_and_user.Last_Name,
                FavouriteAddress = fav_add_and_user.FavouriteAddress.Fav_Address
            };
            return Ok(dto);
        }

        [HttpGet("secret")]
        [Authorization(role.Admin)]
        public IActionResult GetmyAdmin()
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "User does not exist" });
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = userRepository.GetByUsername(dto.Username);
            if (user == null || !BCryptNet.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { Message = "User does not exist or login failed" });
            }
            var token = jwtUtils.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var user = userRepository.GetByUsername(dto.Username);
            if (user != null)
            {
                return BadRequest(new { Message = "User already exists!" });
            }

            var new_fav_add = new FavouriteAddress
            {
                Fav_Address = dto.FavouriteAddress
            };

            favouriteAddress.Create(new_fav_add);
            var result = favouriteAddress.Save();
            if (result)
            {
                var new_user = new User
                {
                    Username = dto.Username,
                    Password = BCryptNet.HashPassword(dto.Password),
                    Role = role.User,
                    Email_Address = dto.Email_Address,
                    Phone_Number = dto.Phone_Number,
                    First_Name = dto.First_Name,
                    Last_Name = dto.Last_Name,
                    FavouriteAddressId = new_fav_add.Id
                };
                userRepository.Create(new_user);
                result = userRepository.Save();

                if (result)
                {
                    var token = jwtUtils.GenerateToken(new_user);
                    return Ok(new { Token = token });
                }
                else return BadRequest(new { message = "Eroare" });
            }

            return BadRequest(new { message = "Eroare" });
        }

        [HttpPut("update")]
        public IActionResult Update_User(UserAndFavAddDTO dto)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "User does not exist!" });
            }
            var fav_add_and_user = userRepository.GetUserAndFavouriteAddress(user.Username);
            
            if (!string.IsNullOrEmpty(dto.Email_Address))
            {
                fav_add_and_user.Email_Address = dto.Email_Address;
            }
            
            if (!string.IsNullOrEmpty(dto.Phone_Number))
            {
                fav_add_and_user.Phone_Number = dto.Phone_Number;
            }

            if (!string.IsNullOrEmpty(dto.FavouriteAddress))
            {
                fav_add_and_user.FavouriteAddress.Fav_Address = dto.FavouriteAddress;
            }
            favouriteAddress.Update(fav_add_and_user.FavouriteAddress);
            var result = favouriteAddress.Save();
            if (result)
            {
                //fav_add_and_user.DateModified = DateTime.Now;
                //Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"));
                userRepository.Update(fav_add_and_user);
                result = userRepository.Save();
                
                if (result)
                {
                    return Ok(new { message = "Userul a fost updatat" });
                }
                else
                {
                    return BadRequest(new { message = "Eroare" });
                }
            }
            else
            {
                return BadRequest(new { message = "Eroare" });
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteMyUser()
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "User does not exist" });
            }
            userRepository.Delete(user);
            var result = userRepository.Save();
            if (result)
            {
                return Ok(new { Message = "User was succesfully deleted" });
            }
            return BadRequest(new { Message = "Eroare" });
        }

        [HttpDelete("delete/{user}")]
        [Authorization(role.Admin)]
        public IActionResult DeleteUserByAdmin(string user)
        {
            var user_name = userRepository.GetByUsername(user);
            if (user_name == null)
                return BadRequest(new { message = "Utilizatorul introdus nu exista" });

            userRepository.Delete(user_name);
            var result = userRepository.Save();
            if (result)
            {
                return Ok(new { Message = "User was succesfully deleted" });
            }
            return BadRequest(new { Message = "Eroare" });
        }
    }
}

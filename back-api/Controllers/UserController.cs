using back_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace back_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApiMauContext _context;

        public UserController(ApiMauContext context)
        {
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        [Route("[action]")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = (from u in _context.Users.OrderByDescending(u => u.Name)
                             join a in _context.Areas on u.IdArea equals a.IdArea
                             join r in _context.Roles on u.IdRole equals r.IdRole
                             select new
                             {
                                 name = u.Name,
                                 area = a.Name,
                                 role = r.Name,
                                 status = u.Status
                             }).Count();

                if (users >= 10)
                {
                    return Ok("estas al limit");
                } 
                else {
                    return Ok("Ok");
                }

            }
            catch(Exception exc)
            {
                return Ok(exc.Message);
            }
        }

        [Route("[action]")]
        public IActionResult GetActiveUsers()
        {
            try
            {
                var usersList = (from u in _context.Users
                             join a in _context.Areas on u.IdArea equals a.IdArea
                             join r in _context.Roles on u.IdRole equals r.IdRole
                             where u.Status == 1
                             select new
                             {
                                 name = u.Name,
                                 area = a.Name,
                                 role = r.Name,
                                 status = u.Status
                             }).ToList();
                return Ok(usersList);
            }
            catch(Exception exc)
            {
                return Ok(exc.Message);
            }
        }


        [Route("[action]")]
        public IActionResult GetInactiveUsers()
        {
            try
            {
                var usersList = (from u in _context.Users
                                 join a in _context.Areas on u.IdArea equals a.IdArea
                                 join r in _context.Roles on u.IdRole equals r.IdRole
                                 where u.Status == 0
                                 select new
                                 {
                                     name = u.Name,
                                     area = a.Name,
                                     role = r.Name,
                                     status = u.Status
                                 }).ToList();
                return Ok(usersList);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }


        [Route("[action]")]
        public IActionResult GetActiveAdmin()
        {
            try
            {
                var usersList = (from u in _context.Users
                                 join a in _context.Areas on u.IdArea equals a.IdArea
                                 join r in _context.Roles on u.IdRole equals r.IdRole
                                 where u.Status == 1 && u.IdRole == 2
                                 select new
                                 {
                                     name = u.Name,
                                     area = a.Name,
                                     role = r.Name,
                                     status = u.Status
                                 }).ToList();
                return Ok(usersList);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }


        [Route("[action]")]
        public IActionResult GetInactiveRoleUser()
        {
            try
            {
                var usersList = (from u in _context.Users
                                 join a in _context.Areas on u.IdArea equals a.IdArea
                                 join r in _context.Roles on u.IdRole equals r.IdRole
                                 where u.Status == 0 && u.IdRole == 1
                                 select new
                                 {
                                     name = u.Name,
                                     area = a.Name,
                                     role = r.Name,
                                     status = u.Status
                                 }).ToList();
                return Ok(usersList);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }

    }
}

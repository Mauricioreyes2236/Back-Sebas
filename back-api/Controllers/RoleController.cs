using back_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace back_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly ApiMauContext _context;

        public RoleController(ApiMauContext context)
        {
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult GetRoles()
        {
            try
            {
                var rolesList = (from r in _context.Roles
                                 select new
                                 {
                                     id = r.IdRole,
                                     name = r.Name,
                                     description = r.Description
                                 }).ToList();
                return Ok(rolesList);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }
        [Route("[action]")]
        public IActionResult PostRole([FromBody] dynamic postData)
        {
            try
            {

                var name = (string)postData["name"];
                var description = (string)postData["description"];

                var roleExists = (from r in _context.Roles
                                  where r.Name == name
                                  select new
                                  {
                                      id = r.IdRole
                                  }).Count();

                var role = new Role
                {
                    Name = name,
                    Description = description,
                };

                if (roleExists >= 1)
                {
                    return Ok(0);
                }
                else
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    return Ok(1);
                }

            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }

        [Route("[action]/{id:int}")]
        public IActionResult GetRole(int id)
        {
            try
            {

                var role = (from r in _context.Roles
                            where r.IdRole == id
                            select new
                            {
                                id = r.IdRole,
                                name = r.Name,
                                description = r.Description
                            }).FirstOrDefault();
                return Ok(role);
            }
            catch(Exception exc)
            {
                return Ok(exc.Message);
            }
        }
    }
}

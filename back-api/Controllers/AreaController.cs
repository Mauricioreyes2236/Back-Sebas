using back_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace back_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : Controller
    {
        private readonly ApiMauContext _context;

        public AreaController(ApiMauContext context)
        {
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult PostArea([FromBody] dynamic postData)
        {
            try
            {

                var name = (string)postData["name"];
                var description = (string)postData["description"];

                var areaexists = (from a in _context.Areas
                                 where a.Name == name
                                 select new
                                 {
                                     id = a.IdArea
                                 }).Count();

                var area = new Area
                {
                    Name = name,
                    Description = description,
                };

                if(areaexists >= 1)
                {
                    return Ok(0);
                }
                else
                {
                    _context.Areas.Add(area);
                    _context.SaveChanges();
                    return Ok(1);
                }

            }
            catch(Exception exc)
            {
                return Ok(exc.Message);
            }
        }
        [Route("[action]")]
        public IActionResult GetAreas()
        {
            try
            {
                var areasList = (from a in _context.Areas
                                 select new
                                 {
                                     id = a.IdArea,
                                     name = a.Name, 
                                     description = a.Description
                                 }).ToList();
                return Ok(areasList);
            }
            catch (Exception exc)
            {
                return Ok(exc.Message);
            }
        }
    }
}

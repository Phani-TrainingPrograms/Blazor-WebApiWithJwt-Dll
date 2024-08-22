using DataComponentLib.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDataService dataService;

        public EmployeeController(IEmployeeDataService service)
        {
            dataService = service;
        }

        [Route("All")]
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> All()
        {
            var data = await dataService.GetAllEmployees();
            return Ok(data);
        }

        [Route("All/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            try
            {
                var data = await dataService.GetEmployeeById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/AddNew")]
        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee emp)
        {
            await dataService.AddEmployee(emp);
            return Ok();
        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await dataService.DeleteEmployeeById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult> Update(Employee emp)
        {
            try
            {
                await dataService.UpdateEmployee(emp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

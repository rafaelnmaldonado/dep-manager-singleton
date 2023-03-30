using dep_manager_singleton.Entities;
using dep_manager_singleton.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace dep_manager_singleton.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly EmployeesDbContext _context;

        public EmployeesController(EmployeesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os colaboradores
        /// </summary>
        /// <returns>Coleção de colaboradores</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var employees = _context.Employees.Where(emp => !emp.IsDeleted).ToList();

            return Ok(employees);
        }

        /// <summary>
        /// Obter todos os colaboradores associados a um departamento
        /// </summary>
        /// <param name="idDep">Identificador do departamento</param>
        /// <returns>Coleção de colaboradores</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Nenhum colaborador associado ao departamento</response>
        [HttpGet("dep/{idDep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByDep(Guid idDep)
        {
            var employees = _context.Employees.Where(emp => emp.IdDepartment == idDep).ToList();

            if (employees == null) return NotFound();

            return Ok(employees);
        }

        /// <summary>
        /// Obter um colaborador específico
        /// </summary>
        /// <param name="id">Identificador do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Colaborador não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var employee = _context.Employees.SingleOrDefault(emp => emp.Id == id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Cadastrar um colaborador
        /// </summary>
        /// <remarks>
        /// { "name": "nome", "picture": "foto", "rg": "XX.XXX.XXX-X", idDepartment: "Guid" }
        /// </remarks>
        /// <param name="employee">Dados do colaborador</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Employee employee)
        {
            _context.Employees.Add(employee);

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Atualiza um colaborador
        /// </summary>
        /// <remarks>
        /// { "name": "nome", "picture": "foto", "rg": "XX.XXX.XXX-X", idDepartment: "Guid" }
        /// </remarks>
        /// <param name="id">Identificador do colaborador</param>
        /// <param name="input">Dados do colaborador</param>
        /// <returns>Void</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Colaborador não encontrado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, Employee input)
        {
            var employee = _context.Employees.SingleOrDefault(emp => emp.Id == id);

            if (employee == null) return NotFound();

            employee.Update(input.Name, input.Picture, input.Rg, input.IdDepartment);

            return NoContent();
        }

        /// <summary>
        /// Deletar um colaborador
        /// </summary>
        /// <param name="id">Identificador do colaborador</param>
        /// <returns>Void</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Colaborador não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var employee = _context.Employees.SingleOrDefault(emp => emp.Id == id);

            if (employee == null) return NotFound();

            employee.Delete();

            return NoContent();
        }
    }
}

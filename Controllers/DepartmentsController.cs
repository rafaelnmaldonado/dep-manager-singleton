using dep_manager_singleton.Entities;
using dep_manager_singleton.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace dep_manager_singleton.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentsDbContext _context;

        public DepartmentsController(DepartmentsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os departamentos
        /// </summary>
        /// <returns>Coleção de departamentos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var departments = _context.Departments.Where(dep => !dep.IsDeleted).ToList();

            return Ok(departments);
        }

        /// <summary>
        /// Obter um departamento específico
        /// </summary>
        /// <param name="id">Identificador do departamento</param>
        /// <returns>Dados do departamento</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var department = _context.Departments.SingleOrDefault(dep => dep.Id == id);

            if (department == null) return NotFound();

            return Ok(department);
        }

        /// <summary>
        /// Cadastrar um departamento
        /// </summary>
        /// <remarks>
        /// { "name": "nome", "acronym": "sigla" }
        /// </remarks>
        /// <param name="department">Dados do departamento</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(Department department)
        {
            _context.Departments.Add(department);

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);

        }

        /// <summary>
        /// Atualizar um departamento
        /// </summary>
        /// <remarks>
        /// { "name": "nome", "acronym": "sigla" }
        /// </remarks>
        /// <param name="id">Identificador do departamento</param>
        /// <param name="input">Dados do departamento</param>
        /// <returns>Void</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, Department input)
        {
            var department = _context.Departments.SingleOrDefault(dep => dep.Id == id);

            if (department == null) return NotFound();

            department.Update(input.Name, input.Acronym);

            return NoContent();
        }

        /// <summary>
        /// Excluir um departamento
        /// </summary>
        /// <param name="id">Identificador do departamento</param>
        /// <returns>Void</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Departamento não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var department = _context.Departments.SingleOrDefault(dep => dep.Id == id);

            if (department == null) return NotFound();

            department.Delete();

            return NoContent();
        }
    }
}

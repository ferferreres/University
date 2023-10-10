using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    //[Authorize]
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private IMapper mapper;
        private readonly CourseService courseService = new CourseService(new CourseRepository(UniversityContext.Create()));

        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Obtiene los objetos de cursos
        /// </summary>
        /// <returns>Listado de los objetos de cursos</returns>
        /// <response code="200">OK. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CourseDTO>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var courses = await courseService.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));

            return Ok(coursesDTO);
        }
        /// <summary>
        /// Obtiene un objeto Course por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto Course</returns>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>
        /// <response code="404">NotFound. No se ha entontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(CourseDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var course = await courseService.GetById(id);

            if (course == null)
                return NotFound();
            
            var courseDTO = mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);
        }

        [HttpGet]
        [Route("{id}/students")]
        [ResponseType(typeof(IEnumerable<StudentDTO>))]
        public async Task<IHttpActionResult> GetStudentsByCourseId(int id)
        {
            var flag = await courseService.GetById(id);
            if (flag == null)
                return NotFound();

            var students = await courseService.GetStudentsByCourseId(id);
            var studentsDTO = students.Select(s => mapper.Map<StudentDTO>(s));

            return Ok(studentsDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(CourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //400

            try
            {
                var course = mapper.Map<Course>(courseDTO);
                course = await courseService.Insert(course);
                return Ok(course);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(CourseDTO courseDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (courseDTO.CourseID != id)
                return BadRequest();

            var course = await courseService.GetById(id);

            if (course == null)
                return NotFound();

            try
            {
                course = mapper.Map<Course>(courseDTO);
                course = await courseService.Update(course);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await courseService.GetById(id);

            if (flag == null)
                return NotFound();

            try
            {
                if(!await courseService.DeleteCheckOnEntity(id))
                    await courseService.Delete(id);
                else
                {
                    throw new Exception("ForeignKeys");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

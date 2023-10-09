using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
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
    [RoutePrefix("api/Enrollments")]
    public class EnrollmentsController : ApiController
    {
        private IMapper mapper;
        private readonly EnrollmentService enrollmentService =
            new EnrollmentService(new EnrollmentRepository(UniversityContext.Create()));

        private readonly CourseService courseService =
            new CourseService(new CourseRepository((UniversityContext.Create())));

        private readonly StudentService studentService =
            new StudentService(new StudentRepository((UniversityContext.Create())));

        public EnrollmentsController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Obtiene los objetos de estudiantes
        /// </summary>
        /// <returns>Listado de los objetos de estudiantes</returns>
        /// <response code="200">OK. Devuelve el listado de objetos solicitado.</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EnrollmentDTO>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var enrollments = await enrollmentService.GetAll();
            var enrollmentsDTO = enrollments.Select(x => mapper.Map<EnrollmentDTO>(x));

            return Ok(enrollmentsDTO);
        }

        [HttpGet]
        [ResponseType(typeof(EnrollmentDTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var enrollment = await enrollmentService.GetById(id);
            if (enrollment == null)
                return NotFound();

            var enrollmentDTO = mapper.Map<EnrollmentDTO>(enrollment);

            return Ok(enrollmentDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var enrollment = mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Insert(enrollment);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(EnrollmentDTO enrollmentDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (enrollmentDTO.EnrollmentID != id)
                return BadRequest();

            var enrollment = await enrollmentService.GetById(id);

            if (enrollment == null)
                return NotFound();

            try
            {
                enrollment = mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Update(enrollment);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await enrollmentService.GetById(id);

            if (flag == null)
                return NotFound();

            try
            {
                await enrollmentService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{courseId}/Students")]

        public async Task<IHttpActionResult> GetStudentsByCourseId(int courseId)
        {

            var enrollments = await enrollmentService.GetAll();
            var filteredEnrollments = enrollments.Where(x => x.CourseID == courseId);
            var enrollmentsDTO = filteredEnrollments.Select(x => mapper.Map<EnrollmentDTO>(x));

            //var studentIds = filteredEnrollments.Select(x => x.StudentID).ToList();

            //var students = await studentService.GetStudentsByIds(studentIds);

            //var studentDTOs = students.Select(x => mapper.Map<StudentDTO>(x));

            return Ok(enrollmentsDTO);
        }
    }
}
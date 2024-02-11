using ManageBook.Data;
using ManageBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageBook.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly ManageBooksDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public AuthorsController(ManageBooksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BornYear"></param>
        /// <param name="DiedYear"></param>
        /// <returns></returns>
        private int CalculateAge(int BornYear, int? DiedYear)
        {
            if (DiedYear == null)
            {
                // Calculate age based on the current year if the author is alive
                int currentYear = DateTime.Now.Year;
                int age = currentYear - BornYear;
                return age;
            }
            else
            {
                // Calculate age if the author is deceased
                int age = DiedYear.Value - BornYear;
                return age;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await dbContext.Author.ToListAsync();

            var listdata = authors.Select(author => new FormatAuthorResponse
            {
                Id = author.Id,
                Name = author.Name,
                Female = author.Female,
                BornYear = author.Born,
                DiedYear = author.Died,
                Age = CalculateAge(author.Born, author.Died)
            }).ToList();

            return Ok(listdata);
        }
    }
}

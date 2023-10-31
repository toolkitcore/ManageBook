using ManageBook.Data;
using ManageBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageBook.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly ManageBooksDbContext dbContext;
        public BooksController(ManageBooksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        private int CheckSelection(int? value_a, int? value_b, int? value_c) {
            int select = 0;

            if (value_a == null && value_b == null && value_c == null)
            {
                select = 0;
            }
            else
            {
                if (value_a != null)
                {
                    select = 1;
                }
                else
                {
                    if (value_b != null)
                    {
                        select = 2;
                    }
                    else
                    {
                        if (value_c != null)
                        {
                            select = 3;
                        }
                    }
                }
            }
            return select;
        }

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

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] int? authorId, [FromQuery] int? rating, [FromQuery] int? publishYear)
        {
            try {
                List<Book> books = new List<Book>();
                int select = CheckSelection(authorId, rating, publishYear);

                switch (select)
                {
                    case 1:
                        books = await dbContext.Book.Where(p => p.AuthorId == authorId).Include(p => p.Author).ToListAsync();
                        break;
                    case 2:
                        books = await dbContext.Book.Where(p => p.Rating == rating).Include(p => p.Author).ToListAsync();
                        break;
                    case 3:
                        books = await dbContext.Book.Where(p => p.PublishYear == publishYear).Include(p => p.Author).ToListAsync();
                        break;
                    default:
                        books = await dbContext.Book.Include(p => p.Author).ToListAsync();
                        break;
                }

                var listdata = books.Select(book => new FormatBookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Topic = book.Topic,
                    AuthorName = book.Author.Name,
                    PublishYear = book.PublishYear,
                    Price = Convert.ToDecimal(book.Price.ToString("0.00")),
                    Rating = book.Rating,
                }).ToList();
                
                return Ok(listdata);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetbookbyId([FromRoute] int id)
        {
            var book = await dbContext.Book
                                    .Include(b => b.Author)
                                    .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {   
                return NotFound();
            }

            var custombook = new {
                Id = book.Id,
                Title = book.Title,
                Topic = book.Topic,
                Author = new {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Female = book.Author.Female,
                    BornYear = book.Author.Born,
                    DiedYear = book.Author.Died,
                    Age = CalculateAge(book.Author.Born, book.Author.Died),
                },
                PublishYear = book.PublishYear,
                Price = Convert.ToDecimal(book.Price.ToString("0.00")),
                Rating = book.Rating
            };

            return Ok(custombook);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooks(FormatBookRequest formatBookRequest)
        {
            try {
                var checkauthorId = await dbContext.Author.FirstOrDefaultAsync(p => p.Id == formatBookRequest.AuthorId);
                if (checkauthorId == null) {
                    var mess = new
                    {
                        message = "Author is not found"
                    };
                    return BadRequest(mess);
                }

                var checkTitle = await dbContext.Book.FirstOrDefaultAsync(p => p.Title == formatBookRequest.Title);
                if (checkTitle != null)
                {
                    var mess = new
                    {
                        message = "There are already other items in the database with the same name."
                    };
                    return BadRequest(mess);
                }

                var book = new Book()
                {
                    Title = formatBookRequest.Title,
                    Topic = formatBookRequest.Topic,
                    AuthorId = formatBookRequest.AuthorId,
                    PublishYear = formatBookRequest.PublishYear,
                    Price = formatBookRequest.Price,
                    Rating = formatBookRequest.Rating
                };
                await dbContext.Book.AddAsync(book);
                await dbContext.SaveChangesAsync();
                return Created("","");
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateContracts([FromRoute] int id, FormatBookRequest formatBookRequest)
        {
            var book = await dbContext.Book.FindAsync(id);
            if (book != null)
            {
                if (book.AuthorId == formatBookRequest.AuthorId) {
                    book.Title = formatBookRequest.Title;
                    book.Topic = formatBookRequest.Topic;
                    book.AuthorId = formatBookRequest.AuthorId;
                    book.PublishYear = formatBookRequest.PublishYear;
                    book.Price = formatBookRequest.Price;
                    book.Rating = formatBookRequest.Rating;

                    await dbContext.SaveChangesAsync();
                }
                else {
                    var mess = new
                    {
                        message = "Author is not found"
                    };
                    return BadRequest(mess);
                }           
            }
            else {
                return BadRequest();
            }

            var showitembook = await dbContext.Book.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
            var custombook = new
            {
                Id = showitembook.Id,
                Title = showitembook.Title,
                Topic = showitembook.Topic,
                Author = new
                {
                    Id = showitembook.Author.Id,
                    Name = showitembook.Author.Name,
                    Female = showitembook.Author.Female,
                    BornYear = showitembook.Author.Born,
                    DiedYear = showitembook.Author.Died,
                    Age = CalculateAge(showitembook.Author.Born, showitembook.Author.Died),
                },
                PublishYear = showitembook.PublishYear,
                Price = showitembook.Price,
                Rating = showitembook.Rating
            };
            return Ok(custombook);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBooks([FromRoute] int id)
        {
            var book = await dbContext.Book.FindAsync(id);
            if (book != null)
            {
                dbContext.Book.Remove(book);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}

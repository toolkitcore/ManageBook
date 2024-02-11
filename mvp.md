# Struct

https://localhost:7206/api/books?authorId=1
https://localhost:7206/api/books
https://localhost:7206/api/books?rating=1
https://localhost:7206/api/books?publishYear=2009
https://localhost:7206/api/books/15
https://localhost:7206/api/books
https://localhost:7206/api/books/1
https://localhost:7206/api/books/100
https://localhost:7206/api/TestJwtAuth/havejwt
https://localhost:7206/api/auth/Login
https://localhost:7206/api/auth/Register
https://localhost:7206/api/authors

# Flow

Data
    DbInitializer
    ManageBooksDbContext
Middlewares
    MiddlewareCheckTime
Models
    LoginUser
    RegisterUser
    Author
    FormatAuthorResponse
    Book
    FormatBookRequest
    FormatBookResponse
Services
    IAuthService
    AuthService
Controllers
    AuthController
    AuthorsController
    BooksController
    TestJwtAuthController
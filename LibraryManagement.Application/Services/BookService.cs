using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILibraryRepository _libraryRepository;

    public BookService(
        IBookRepository bookRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ILibraryRepository libraryRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _libraryRepository = libraryRepository;
    }

    public async Task<ShowBookDto> AddBook(AddBookDto dto)
    {
        var category = _categoryRepository.GetCategoryById(dto.CategoryId);
        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }

        var library = await _libraryRepository.GetLibraryByIdAsync(dto.LibraryId);
        if (library == null)
            throw new ArgumentException("Library not found");

        var existingBook = await _bookRepository.GetBookByTitleAndLibrary(dto.Title, dto.LibraryId);
        if (existingBook != null)
            throw new InvalidOperationException("A book with the same title already exists in this library.");

        var newBook = _mapper.Map<Book>(dto);
        newBook.AvailableCopies = dto.TotalCopies;
        newBook.Id = Guid.NewGuid();
        newBook.PublishDate = DateTime.Now;

        await _bookRepository.AddBook(newBook); // ✅


        var a = newBook;
        return _mapper.Map<ShowBookDto>(newBook);
    }
    public async Task<List<ShowBookDto>> GetAllBooks()
    {
        var books = await _bookRepository.GetAllBooks();
        return books.Select(book => _mapper.Map<ShowBookDto>(book)).ToList();
    }


    public async Task<ShowBookDto?> GetBookById(Guid id)
    {
        var book = await _bookRepository.GetBookById(id);
        return book == null ? null : _mapper.Map<ShowBookDto>(book);
    }


    public async Task<ShowBookDto> UpdateBook(UpdateBookDto dto, Guid id)
    {
        var existingBook = await _bookRepository.GetBookById(id); // <-- await here
        if (existingBook == null)
        {
            throw new ArgumentException("Book not found");
        }

        _mapper.Map(dto, existingBook);

        var updatedBook = await _bookRepository.UpdateBook(existingBook); // <-- await here

        return _mapper.Map<ShowBookDto>(updatedBook);
    }


}

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { inject, TestBed } from '@angular/core/testing';
import { BookService } from 'src/app/services/book.service';
import { Book } from 'src/app/book-catalogue/book-catalogue.component';


const mockData = [
  { id: 1, name: 'Book A', text: "Book A text", price: 2, isPurchased: true },
  { id: 2, name: 'Book B', text: "Book B text", price: 5, isPurchased: false },
  { id: 3, name: 'Book C', text: "Book C text", price: 7, isPurchased: true },
] as Book[];

describe('BookService', () => {

  let service;
  let httpTestingController: HttpTestingController;
  let mockBooks: Book[];
  let mockBook: Book = new Book();

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [BookService]
    });
    httpTestingController = TestBed.get(HttpTestingController);
  });

  beforeEach(inject([BookService], s => {
    service = s;
  }));

  beforeEach(() => {
    mockBooks = [...mockData];
    mockBook = mockBooks[0];
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getBooks', () => {

    it('should return mock books', () => {
      service.getBooks().subscribe(
        books => expect(books.length).toEqual(mockBooks.length),
        fail
      );

      const req = httpTestingController.expectOne(service.url);
      expect(req.request.method).toEqual('GET');

      req.flush(mockBooks);
    });
  });
});

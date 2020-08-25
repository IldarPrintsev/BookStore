import { Component, Input, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { PaginationInstance } from 'ngx-pagination';
import { Book } from 'src/app/models/book'

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css'],
  providers: [BookService]
})
export class AdminPanelComponent implements OnInit {
  book: Book = new Book();
  newBook: Book = new Book();
  @Input('data') books: Book[] = [];

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.getBooks();
  }

  getBooks() {
    this.bookService.getBooks()
      .subscribe((data: Book[]) =>
        this.books = data
        , err => {
          console.log(err);
        });
  }

  saveBook() {
    if (this.book.id != null) {
      this.bookService.updateBook(this.book)
        .subscribe(() => this.getBooks());
      this.cancel();
    } else {
      this.bookService.addBook(this.newBook)
        .subscribe((data: Book) => this.books.push(data));
      this.newBook = new Book();
    }
  }

  editBook(b: Book) {
    this.book = new Book(b.id, b.name, b.text, b.price);
  }

  cancel() {
    this.book = new Book();
  }

  deleteBook(b: Book) {
    this.bookService.deleteBook(b.id)
      .subscribe(() => this.getBooks());
  }

  public maxSize: number = 7;
  public directionLinks: boolean = true;
  public autoHide: boolean = false;
  public responsive: boolean = false;
  public config: PaginationInstance = {
    id: 'advanced',
    itemsPerPage: 10,
    currentPage: 1
  };
  public labels: any = {
    previousLabel: 'Previous',
    nextLabel: 'Next',
    screenReaderPaginationLabel: 'Pagination',
    screenReaderPageLabel: 'page',
    screenReaderCurrentLabel: `You're on page`
  };

  onPageChange(number: number) {
    this.config.currentPage = number;
  }

  onPageBoundsCorrection(number: number) {
    this.config.currentPage = number;
  }
}

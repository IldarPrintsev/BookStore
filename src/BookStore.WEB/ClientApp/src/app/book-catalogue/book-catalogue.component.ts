import { Component, Input, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { UserService } from 'src/app/services/user.service';
import { PaginationInstance } from 'ngx-pagination';
import { Book } from 'src/app/models/book'

@Component({
  selector: 'app-book-catalogue',
  templateUrl: './book-catalogue.component.html',
  styleUrls: ['./book-catalogue.component.css'],
  providers: [BookService, UserService]
})
export class BookCatalogueComponent implements OnInit {
  @Input('data') books: Book[] = [];

  constructor(private bookService: BookService, private userService: UserService) { }

  ngOnInit() {
    this.getBooks();
  }

  getBooks() {
    this.bookService.getBooks()
      .subscribe(response => {
        this.books = <any>response;
      }, err => {
          console.log(err);
        });
  }

  updateSubscription(b: Book) {
    this.bookService.updateSubscription(b)
      .subscribe(response => {
        b.isPurchased = !b.isPurchased;
      }, err => {
        console.log(err);
      });
  }

  isUserAuthenticated() {
    return this.userService.isUserAuthenticated();
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


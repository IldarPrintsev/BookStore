import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Book } from 'src/app/models/book';

@Injectable()
export class BookService {

  private url = "/api/bookstore";
  private adminUrl = "/api/admin";

  constructor(private http: HttpClient) {
  }

  getBooks() {
    return this.http.get(this.url, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  updateSubscription(book: Book) {
    return this.http.post(this.url, book, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }


  addBook(book: Book) {
    return this.http.post(this.adminUrl, book, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  updateBook(book: Book) {
    return this.http.put(this.adminUrl, book, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  deleteBook(id: number) {
    return this.http.delete(this.adminUrl + '/' + id, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../models/Produto';

@Injectable()

export class ProdutoService {
baseURL = 'https://localhost:44352/api/Product/';
constructor(private http: HttpClient) { }

public getProdutos(): Observable<Produto[]>{
  return this.http.get<Produto[]>(this.baseURL);
}

public getProdutoById(id: number): Observable<Produto>{
  return this.http.get<Produto>(`${this.baseURL}/${id}`);
}

}

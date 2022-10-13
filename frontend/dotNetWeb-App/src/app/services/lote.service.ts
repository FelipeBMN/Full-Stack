import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/model/Lote';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable()
export class LoteService {

  baseURL = 'https://localhost:5001/Lote';

  constructor(private http: HttpClient) { }

  // Para se descrever dos subscribes usar .pipe(take(1));
  getAllLotesByEventoId(eventoId: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseURL}/${eventoId}`).pipe(take(1));
  }

  public saveLotes(eventoId: number, lotes: Lote[]): Observable<Lote[]> {
    return this.http.put<Lote[]>(`${this.baseURL}/${eventoId}`, lotes).pipe(take(1));
  }

  public deleteLote(eventoId: number, id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${eventoId}/${id}`).pipe(take(1));
  }

}

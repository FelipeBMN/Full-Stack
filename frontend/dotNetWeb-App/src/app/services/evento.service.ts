import { Evento } from './../model/Evento';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';


// service -> serve outro componente
// Injectable faz o serviço ser injetável nas outra classes
@Injectable(
  //{ providedIn: 'root' }
)
export class EventoService {

  baseURL = 'https://localhost:5001/Events';

  constructor(private http: HttpClient) { }

  // Para se descrever dos subscribes usar .pipe(take(1));

  getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL).pipe(take(1));
  }

  getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`).pipe(take(1));
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public deleteEvento(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

  // Piimeiro metodo para o put e post

  public post(evento: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL, evento).pipe(take(1));
  }

  public put(evento: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento).pipe(take(1));
  }

}

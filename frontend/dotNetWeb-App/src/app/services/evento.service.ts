import { Evento } from './../model/Evento';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


// service -> serve outro componente
// Injectable faz o serviço ser injetável nas outra classes
@Injectable(
  //{ providedIn: 'root' }
  )
export class EventoService {
baseURL = 'https://localhost:5001/Events';

constructor(private http: HttpClient) { }

 getEventos(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseURL)
 }
 getEventosByTema(tema: string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`)
 }
 getEventoById(id: number): Observable<Evento>{
  return this.http.get<Evento>(`${this.baseURL}/${id}`)
 }
}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/model/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})

export class EventoListaComponent implements OnInit {
  modalRef?: BsModalRef;
  public message?: string;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public larguraImg: number = 100;
  public mostrarImg: boolean = true;
  private filtroListado: string = '';

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router,
              ) { }

  public ngOnInit(): void {
    this.getEventos();
    this.spinner.show();
  }

  public imgViewer(): void {
    this.mostrarImg = !this.mostrarImg;
  }

  public get filtroLista(): string{
    return this.filtroListado
  }

  public set filtroLista(value: string){
    this.filtroListado = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor= filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: Evento) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({

      next: (eventos: Evento[]) => {
        this.eventos = eventos
        this.eventosFiltrados = this.eventos
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar eventos!','Erro')
      },
      complete: () => this.spinner.hide(),
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef?.hide();
    this.toastr.success('Deletado','Evento deletado com Sucesso')
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }

  detalheEvento(id: number):void{
    this.router.navigate([`eventos/detalhe/${id}`])
    console.log(id)
  }
}

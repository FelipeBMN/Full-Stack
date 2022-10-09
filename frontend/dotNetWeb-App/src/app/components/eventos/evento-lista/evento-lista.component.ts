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
  public eventoId = 0;

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
    this.carregarEventos();
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

  public carregarEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.eventos = eventos
        this.eventosFiltrados = this.eventos
      },
      error: (_error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar eventos!','Erro')
      },
      complete: () => this.spinner.hide(),
    });
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next:(result: any)=>{
        this.toastr.success('Deletado','Evento deletado com Sucesso');
        this.spinner.hide();
        console.log('evento deletado');
        this.carregarEventos();
      },
      error:(error)=>{
        this.toastr.error(`Erro ao tentar deletar evento ${this.eventoId}`, 'Erro')
        this.spinner.hide();
        this.carregarEventos();
      },
      complete:()=>{this.spinner.hide();this.carregarEventos();}
    } );

    this.message = 'Confirmed!';

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

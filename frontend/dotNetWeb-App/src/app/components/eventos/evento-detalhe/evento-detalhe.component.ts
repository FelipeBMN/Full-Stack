import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Evento } from '@app/model/Evento';
import { EventoService } from '@app/services/evento.service';
import { Lote } from '@app/model/Lote';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private route: ActivatedRoute,
    private router: Router,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
  }

  form!: FormGroup;
  evento = {} as Evento;
  mode = 'post';
  titleDetalhe = 'Criando Novo Evento';


  get f(): any {
    return this.form.controls;
  }

  get lotes(): FormArray{
    return this.form.get('lotes') as FormArray
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      isAnimated: true,
      containerClass: 'theme-default',
      showWeekNumbers: false,
      dateInputFormat: 'DD-MM-YYYY hh:mm a'
    }
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

  onSubmit() {
    if (this.form.invalid) {
      return
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      local: new FormControl('', Validators.required),
      dataEvent: new FormControl('', Validators.required),
      quantidade: new FormControl('3', [Validators.required, Validators.max(10000000), Validators.min(3)]),
      telefone: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      imagemURL: new FormControl('', Validators.required),
      lotes: this.fb.array([])
    })
  }

  // @ Métodos para lotes
  adicionarLote(): void {
    this.lotes.push(this.criarLote({id: 0} as Lote));
  }

  criarLote(lote: Lote): FormGroup{
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome , Validators.required],
      quantidade: [lote.quantidade , Validators.required],
      preço: [lote.preço , Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim]
    })
  }

  public cssValidator(formCampo: FormControl | AbstractControl): any {
    return { 'is-invalid': formCampo.errors && formCampo.touched };
  }

  //pegando dados do formulário de acordo com o id passado

  public carregarEvento(): void {
    const eventoIdParameter = this.route.snapshot.paramMap.get('id');

    if (eventoIdParameter !== null) {
      this.titleDetalhe = `Editando Evento ${eventoIdParameter}`;
      this.spinner.show();
      this.mode = 'put';
      this.eventoService.getEventoById(+eventoIdParameter).subscribe({

        next: (evento: Evento) => {
          this.evento = { ...evento }; // Spread ... copia os atributos
          this.form.patchValue(this.evento); //Atualiza os valores do formulario
        },
        error: (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao tentar carregar evento');
        }
      }
      ).add(() => { this.spinner.hide(); });
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {

      this.evento = (this.mode === 'post')
        ? this.evento = { ...this.form.value }
        : this.evento = { id: this.evento.id, ...this.form.value };

      this.eventoService[this.mode](this.evento).subscribe(

        () => {
          this.toastr.success('Evento atualizado com sucesso');
          this.router.navigate(['/eventos/lista']);
        },
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao atualizar evento', 'Erro');
          this.spinner.hide();
        },
        () => {
          this.spinner.hide();
        }
      );

    }
  }


}

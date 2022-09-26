import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form = new FormGroup({
    tema: new FormControl(),
    local: new FormControl(),
    dataEvento: new FormControl(),
    quantidade: new FormControl(),
    telefone: new FormControl(),
    email: new FormControl(),
    imagemURL: new FormControl(),
  });

  get f(): any{
    return this.form.controls;
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

  onSubmit() {
    if (this.form.invalid){
      return
    }
  }

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group({
    tema: ['', [Validators.required, Validators.minLength(4),
                  Validators.maxLength(50)]],
    local: ['', Validators.required],
    dataEvento: ['', Validators.required],
    quantidade: ['3', [Validators.required, Validators.max(10000000), Validators.min(3)]],
    telefone: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    imagemURL: ['', Validators.required],
    })
  }
}

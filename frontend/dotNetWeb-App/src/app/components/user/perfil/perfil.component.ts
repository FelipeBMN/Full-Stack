import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/validatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
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


  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'c_senha')
    }

    this.form = this.fb.group({
      p_nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      u_nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      telefone:['', [Validators.required, Validators.minLength(11), Validators.maxLength(12)]],
      descricao:['',Validators.required],
      funcao:['',Validators.required],
      email: ['', [Validators.required, Validators.email]],
      usuario: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      titulo:['',[Validators.required, Validators.minLength(3)]],
      senha: ['', [Validators.required, Validators.maxLength(16), Validators.minLength(8), Validators.minLength(8)]],
      c_senha: ['', [Validators.required]]
    }, formOptions);
  }
}

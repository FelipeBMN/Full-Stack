import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helpers/validatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  form!: FormGroup<any>;

  get f() {
    return this.form.controls;
  }

  public resetForm(): void {
    this.form.reset();
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'c_senha')
    }

    this.form = this.fb.group({
      p_nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      u_nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      email: ['', [Validators.required, Validators.email]],
      usuario: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      senha: ['', [Validators.required, Validators.maxLength(16), Validators.minLength(8), Validators.minLength(8)]],
      c_senha: ['', [Validators.required]]
    }, formOptions);
  }
}

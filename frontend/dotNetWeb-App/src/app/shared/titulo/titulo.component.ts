import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})

export class TituloComponent implements OnInit {

  @Input() title: string = "Carregando Título";
  @Input() iconClass: string = "fa fa-user";
  @Input() subtitle: string = "Desde 2022";
  @Input() buttonList: boolean = false;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  button(title: string): void {

    // Botão para eventos
    if (title.toLowerCase() == "eventos".toLowerCase()) {
      this.router.navigate([`/${title.toLowerCase() }/lista`])
    }

  }

}

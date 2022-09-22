import { Evento } from "./Evento";
import { RedeSocial } from "./RedesSocial";

export interface Palestrante {
  id: number;
  name: string;
  minCurriculo: string;
  imageURL: string;
  telefone: string;
  email: string;
  redesSociais: RedeSocial[];
  palestrantesEvents: Evento[];
}


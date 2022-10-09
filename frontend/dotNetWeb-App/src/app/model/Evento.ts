import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedesSocial";

export interface Evento {
  id: number;
  local: string;
  dataEvent: Date;
  tema: string;
  quantidade: number;
  imagemURL: string;
  telefone: string;
  email: string;
  lotes: Lote[];
  redesSociais: RedeSocial[];
  palestrantesEvents: Palestrante[];
}


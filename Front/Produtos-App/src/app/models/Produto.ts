export interface Produto {
  id: number;
  description: string;
  estReg: string;
  dataFabricacao: Date;
  dataValidade: Date;
  codigoFornecedor: number;
  descricaoFornecedor: string;
  cnpj: string;
}

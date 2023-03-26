import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/Produto';
import { ProdutoService } from '../services/Produto.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  public produtos: Produto[] = [];
  public produtosFiltrado:Produto[] = [];
  public ImageWidth:number = 100;
  public ImageMargin: number = 10;
  isCollapsed = true;
  ExibirImagem: boolean = true;
  private _filtroListaPaginaInicial: string = '';

  constructor(private produtoService: ProdutoService) { }

  ngOnInit(): void {
    this.getProdutos()
  }
  public get filtroListaPaginaInicial(): string{
    return this._filtroListaPaginaInicial;
  }
  public set filtroListaPaginaInicial(NewFiltroListaPaginaInicial: string ){
    this._filtroListaPaginaInicial = NewFiltroListaPaginaInicial;
    this.produtosFiltrado = this.filtroListaPaginaInicial ? this.FiltrarProdutos(this.filtroListaPaginaInicial) : this.produtos;
  }
  public FiltrarProdutos(filtrarPor: string): Produto[]{
    filtrarPor = filtrarPor.toLocaleUpperCase();
    return this.produtos.filter(
      (produto: any) => produto.description.toLocaleUpperCase().indexOf(filtrarPor) !== -1
      || produto.descricaoFornecedor.toLocaleUpperCase().indexOf(filtrarPor) !== - 1
      || produto.dataFabricacao.toLocaleUpperCase().indexOf(filtrarPor) !== - 1
    )
  }

  public controleDeExibicaoDeImagem(): void{
    this.ExibirImagem = !this.ExibirImagem;
  }

  public getProdutos(): void{
    this.produtoService.getProdutos().subscribe(
    (_produtos:Produto[])  => {
        this.produtos = _produtos,
        this.produtosFiltrado = this.produtos
        console.log(_produtos)
      },
      error => console.log(error),
    );
  }
}

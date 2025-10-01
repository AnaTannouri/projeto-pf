using ProjetoPF.Dao;
using ProjetoPF.Dao.Compra;
using ProjetoPF.Dao.Compras;
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Transactions;
using CompraModel = ProjetoPF.Modelos.Compra.Compra;

namespace ProjetoPF.Servicos.Compra
{
    public class CompraServicos : BaseServicos<CompraModel>
    {
        private readonly ItemCompraService _itemService = new ItemCompraService();
        private readonly ParcelaCompraService _parcelaService = new ParcelaCompraService();

        public CompraServicos() : base(new CompraDao()) { }

        public void CriarCompraCompleta(ProjetoPF.Modelos.Compra.Compra compra)
        {
            if (compra == null)
                throw new Exception("Compra inválida.");

            if (compra.Itens == null || compra.Itens.Count == 0)
                throw new Exception("É necessário adicionar ao menos um item.");

            if (compra.Parcelas == null || compra.Parcelas.Count == 0)
                throw new Exception("É necessário gerar ao menos uma parcela.");

            var custosRateados = RatearCustosAdicionais(compra);

            var compraDao = new CompraDao();
            compraDao.SalvarCompraComItensParcelas(compra, compra.Itens, compra.Parcelas);

            var produtoFornecedorService = new ProdutoFornecedorService();
            var produtoDao = new BaseDao<Produtos>("Produtos");

            foreach (var item in compra.Itens)
            {
                if (custosRateados.TryGetValue(item.IdProduto, out decimal custoRealRateado))
                {
    
                    produtoFornecedorService.CriarOuAtualizar(
                        item.IdProduto,
                        compra.IdFornecedor,
                        custoRealRateado,
                        compra.DataEmissao,
                        compra.Observacao
                    );

                    var produto = produtoDao.BuscarPorId(item.IdProduto);
                    if (produto != null)
                    {
                        decimal estoqueAnterior = produto.Estoque;
                        decimal custoMedioAnterior = produto.PrecoCusto;

                        produto.Estoque = estoqueAnterior + (int)item.Quantidade;

                        if (produto.Estoque > 0)
                        {
                            produto.PrecoCusto = (
                                (estoqueAnterior * custoMedioAnterior) +
                                (item.Quantidade * custoRealRateado)
                            ) / produto.Estoque;
                        }
                        else
                        {
                            produto.PrecoCusto = custoRealRateado;
                        }

                        produto.CustoUltimaCompra = custoRealRateado;


                        produtoDao.Atualizar(produto);
                    }
                }
            }
        }
        private Dictionary<int, decimal> RatearCustosAdicionais(ProjetoPF.Modelos.Compra.Compra compra)
        {
            decimal valorTotalItens = 0;
            foreach (var item in compra.Itens)
            {
                valorTotalItens += item.ValorUnitario * item.Quantidade;
            }

            if (valorTotalItens == 0) return new Dictionary<int, decimal>();

            decimal totalCustosAdicionais = compra.ValorFrete + compra.ValorSeguro + compra.OutrasDespesas;

            var custosReais = new Dictionary<int, decimal>();

            foreach (var item in compra.Itens)
            {
                decimal valorItem = item.ValorUnitario * item.Quantidade;
                decimal proporcao = valorItem / valorTotalItens;
                decimal custoRateado = totalCustosAdicionais * proporcao;
                decimal custoUnitarioRateado = custoRateado / item.Quantidade;

                decimal custoReal = item.ValorUnitario + custoUnitarioRateado;
                custosReais.Add(item.IdProduto, custoReal);
            }

            return custosReais;
        }
    }
    
    public class ItemCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ItemCompra>
    {
        public ItemCompraService() : base(new ItemCompraDao()) { }
    }

    public class ParcelaCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ParcelaCompra>
    {
        public ParcelaCompraService() : base(new ParcelaCompraDao()) { }
    }
}

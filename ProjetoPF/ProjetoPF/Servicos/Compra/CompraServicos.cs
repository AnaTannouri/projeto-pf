using ProjetoPF.Dao;
using ProjetoPF.Dao.Compra;
using ProjetoPF.Dao.Compras;
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            compra.Id = compraDao.SalvarCompraComItensParcelas(compra, compra.Itens, compra.Parcelas);

            var produtoFornecedorDao = new ProdutoFornecedorDAO();
            var produtoDao = new BaseDao<Produtos>("Produtos");

            foreach (var item in compra.Itens)
            {
                if (custosRateados.TryGetValue(item.IdProduto, out decimal custoRealRateado))
                {
                    custoRealRateado = Math.Round(custoRealRateado, 2);

                    var associacao = new ProdutoFornecedor
                    {
                        IdProduto = item.IdProduto,
                        IdFornecedor = compra.IdFornecedor,
                        PrecoUltimaCompra = custoRealRateado,
                        DataUltimaCompra = compra.DataEmissao
                    };

                    produtoFornecedorDao.CriarOuAtualizarProdutoFornecedor(associacao);

                    var produto = produtoDao.BuscarPorId(item.IdProduto);
                    if (produto != null)
                    {
                        decimal estoqueAnterior = produto.Estoque;
                        decimal custoMedioAnterior = produto.PrecoCusto;

                        produto.Estoque = estoqueAnterior + item.Quantidade;

                        if (produto.Estoque > 0)
                        {
                            produto.PrecoCusto = Math.Round(
                                ((estoqueAnterior * custoMedioAnterior) +
                                (item.Quantidade * custoRealRateado)) / produto.Estoque, 2);
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
        public void CancelarCompra(int idCompra, string motivo)
        {
            if (idCompra <= 0)
                throw new ArgumentException("ID de compra inválido.");

            var compraDao = new CompraDao();
            var itemDao = new ItemCompraDao();
            var contasDao = new ContasAPagarDao();
            var produtoDao = new BaseDao<Produtos>("Produtos");
            var produtoFornecedorDao = new ProdutoFornecedorDAO();

            var compra = compraDao.BuscarPorId(idCompra);
            if (compra == null)
                throw new Exception("Compra não encontrada.");

            var itensCompra = itemDao.BuscarPorCompraId(idCompra);

            foreach (var item in itensCompra)
            {
                var produto = produtoDao.BuscarPorId(item.IdProduto);
                if (produto == null)
                    continue;

                produto.Estoque -= item.Quantidade;
                if (produto.Estoque < 0)
                    produto.Estoque = 0;

                decimal novoCustoMedio = compraDao.CalcularCustoMedioProduto(item.IdProduto, idCompra);
                produto.PrecoCusto = novoCustoMedio > 0 ? novoCustoMedio : 0;

                produtoFornecedorDao.InativarPorCompra(item.IdProduto, compra.IdFornecedor);

                produtoDao.Atualizar(produto);
            }

            compra.Itens = null;
            compra.Parcelas = null;

            compra.Ativo = false;
            compra.MotivoCancelamento = string.IsNullOrWhiteSpace(motivo)
                ? "Cancelamento não informado"
                : motivo;
            compra.DataAtualizacao = DateTime.Now;

            var compraLimpa = new ProjetoPF.Modelos.Compra.Compra
            {
                Id = compra.Id,
                Modelo = compra.Modelo,
                Serie = compra.Serie,
                NumeroNota = compra.NumeroNota,
                DataEmissao = compra.DataEmissao,
                DataEntrega = compra.DataEntrega,
                IdFornecedor = compra.IdFornecedor,
                IdCondicaoPagamento = compra.IdCondicaoPagamento,
                ValorFrete = compra.ValorFrete,
                ValorSeguro = compra.ValorSeguro,
                OutrasDespesas = compra.OutrasDespesas,
                ValorTotal = compra.ValorTotal,
                Observacao = compra.Observacao,
                DataCriacao = compra.DataCriacao,
                DataAtualizacao = DateTime.Now,
                Ativo = false,
                MotivoCancelamento = string.IsNullOrWhiteSpace(motivo)
        ? "Cancelamento não informado"
        : motivo
            };

            compraLimpa.Itens = null;
            compraLimpa.Parcelas = null;

            compraDao.CancelarCompra(compraLimpa.Id, "Cancelamento não informado");
        }


        private Dictionary<int, decimal> RatearCustosAdicionais(ProjetoPF.Modelos.Compra.Compra compra)
        {
            decimal valorTotalItens = 0;
            foreach (var item in compra.Itens)
                valorTotalItens += item.ValorUnitario * item.Quantidade;

            if (valorTotalItens == 0)
                return new Dictionary<int, decimal>();

            decimal totalCustosAdicionais = compra.ValorFrete + compra.ValorSeguro + compra.OutrasDespesas;

            var custosReais = new Dictionary<int, decimal>();

            foreach (var item in compra.Itens)
            {
                decimal valorItem = item.ValorUnitario * item.Quantidade;
                decimal proporcao = valorItem / valorTotalItens;
                decimal custoRateado = totalCustosAdicionais * proporcao;
                decimal custoUnitarioRateado = item.ValorUnitario + (custoRateado / item.Quantidade);


                custosReais[item.IdProduto] = Math.Round(custoUnitarioRateado, 2);
            }

            return custosReais;
        }
    }
    
    public class ItemCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ItemCompra>
    {
        public ItemCompraService() : base(new ItemCompraDao()) { }
    }

    public class ParcelaCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ContasAPagar>
    {
        public ParcelaCompraService() : base(new ContasAPagarDao()) { }
    }
}

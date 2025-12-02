using ProjetoPF.Dao.Vendas;
using ProjetoPF.Dao;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Venda
{
    public class VendaServicos : BaseServicos<ProjetoPF.Modelos.Venda.Venda>
    {
        public VendaServicos() : base(new VendaDao()) { }

        public void CancelarVenda(VendaKey vendaKey, string motivo)
        {
            var vendaDao = new VendaDao();
            var itemDao = new ItemVendaDao();
            var produtoDao = new BaseDao<Produtos>("Produtos");
            var contaDao = new ContasAReceberDao();

            var venda = vendaDao.BuscarPorChave(vendaKey);
            if (venda == null)
                throw new Exception("Venda não encontrada.");

            var itensVenda = itemDao.BuscarPorChaveVenda(venda.Modelo, venda.Serie, venda.NumeroNota, venda.IdCliente);

            foreach (var item in itensVenda)
            {
                var produto = produtoDao.BuscarPorId(item.IdProduto);
                if (produto != null)
                {
                    produto.Estoque += item.Quantidade;
                    produtoDao.Atualizar(produto);
                }
            }

            venda.Ativo = false;
            venda.MotivoCancelamento = string.IsNullOrWhiteSpace(motivo)
                ? "Cancelamento não informado"
                : motivo;
            venda.DataAtualizacao = DateTime.Now;

            vendaDao.CancelarVenda(vendaKey, motivo);
        }
    }
}

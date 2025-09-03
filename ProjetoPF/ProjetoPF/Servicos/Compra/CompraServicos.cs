using ProjetoPF.Dao.Compras;
using ProjetoPF.Modelos.Compra;
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

        public void CriarCompraCompleta(CompraModel compra)
        {
            if (compra == null)
                throw new Exception("Compra inválida.");

            if (compra.Itens == null || compra.Itens.Count == 0)
                throw new Exception("É necessário adicionar ao menos um item.");

            if (compra.Parcelas == null || compra.Parcelas.Count == 0)
                throw new Exception("É necessário gerar ao menos uma parcela.");

            using (var scope = new TransactionScope())
            {
                compra.DataCriacao = DateTime.Now;
                compra.DataAtualizacao = DateTime.Now;
                Criar(compra);

                foreach (var item in compra.Itens)
                {
                    item.IdCompra = compra.Id;
                    item.DataCriacao = DateTime.Now;
                    item.DataAtualizacao = DateTime.Now;
                    _itemService.Criar(item);
                }

                foreach (var parcela in compra.Parcelas)
                {
                    parcela.IdCompra = compra.Id;
                    parcela.DataCriacao = DateTime.Now;
                    parcela.DataAtualizacao = DateTime.Now;
                    _parcelaService.Criar(parcela);
                }

                scope.Complete();
            }
        }
    }

    public class ItemCompraService : BaseServicos<ItemCompra>
    {
        public ItemCompraService() : base(new ItemCompraDao()) { }
    }

    public class ParcelaCompraService : BaseServicos<ParcelaCompra>
    {
        public ParcelaCompraService() : base(new ParcelaCompraDao()) { }
    }
}

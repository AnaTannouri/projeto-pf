using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web;

public class CondicaoPagamentoParcelasServicos : BaseServicos<CondicaoPagamentoParcelas>
{
    public CondicaoPagamentoParcelasServicos() : base(new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas")) { }

    public void CriarCondicaoPagamentoParcelas(CondicaoPagamentoParcelas condicaoPagamentoParcelas)
    {
        if (condicaoPagamentoParcelas.NumParcela <= 0)
            throw new Exception("Número da parcela inválido.");

        if (condicaoPagamentoParcelas.Prazo <= 0)
            throw new Exception("Prazo inválido.");

        if (condicaoPagamentoParcelas.Porcentagem < 0)
            throw new Exception("Porcentagem inválida.");

        condicaoPagamentoParcelas.DataCriacao = DateTime.Now;
        condicaoPagamentoParcelas.DataAtualizacao = DateTime.Now;

        Criar(condicaoPagamentoParcelas);
    }
}

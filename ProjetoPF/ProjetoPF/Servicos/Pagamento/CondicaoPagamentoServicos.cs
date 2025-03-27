using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;

public class CondicaoPagamentoServicos : BaseServicos<CondicaoPagamento>
{
    public CondicaoPagamentoServicos() : base(new CondicaoPagamentoDAO()) { }

    public void SalvarCondicaoComParcelas(CondicaoPagamento condicao, List<CondicaoPagamentoParcelas> parcelas)
    {
        var dao = (CondicaoPagamentoDAO)_dao;
        dao.SalvarCondicaoComParcelas(condicao, parcelas);
    }

    public void RemoverComParcelas(int idCondicaoPagamento)
    {
        var dao = (CondicaoPagamentoDAO)_dao;
        dao.RemoverComParcelas(idCondicaoPagamento);
    }
    public void AtualizarCondicaoComParcelas(CondicaoPagamento condicao, List<CondicaoPagamentoParcelas> parcelas)
    {
        var dao = (CondicaoPagamentoDAO)_dao;
        dao.AtualizarCondicaoComParcelas(condicao, parcelas);
    }
}

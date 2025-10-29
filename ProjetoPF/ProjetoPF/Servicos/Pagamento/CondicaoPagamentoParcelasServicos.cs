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
}

using ProjetoPF.Dao;
using ProjetoPF.Servicos;
using System;

public class FormaPagamentoServicos : BaseServicos<FormaPagamento>
{
    public FormaPagamentoServicos() : base(new FormaPagamentoDAO()) { }
}

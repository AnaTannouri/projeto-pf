using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;

public class CondicaoPagamentoServicos : BaseServicos<CondicaoPagamento>
{
    public CondicaoPagamentoServicos() : base(new CondicaoPagamentoDAO()) { }

    public void CadastrarCondicaoPagamento(CondicaoPagamento condicaoPagamento)
    {
        if (string.IsNullOrWhiteSpace(condicaoPagamento.Descricao))
            throw new Exception("Descrição é obrigatória!");

        if (condicaoPagamento.TaxaJuros < 0)
            throw new Exception("A taxa de juros deve ser maior ou igual a 0.");

        if (condicaoPagamento.Multa < 0)
            throw new Exception("A multa deve ser maior ou igual a 0.");

        condicaoPagamento.DataCriacao = DateTime.Now;
        condicaoPagamento.DataAtualizacao = DateTime.Now;

        Criar(condicaoPagamento);
    }
}

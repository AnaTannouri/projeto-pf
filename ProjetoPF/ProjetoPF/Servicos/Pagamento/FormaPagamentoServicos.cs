using ProjetoPF.Dao;
using ProjetoPF.Servicos;
using System;

public class FormaPagamentoServicos : BaseServicos<FormaPagamento>
{
    public FormaPagamentoServicos() : base(new FormaPagamentoDAO()) { }

    public void CadastrarFormaPagamento(FormaPagamento formaPagamento)
    {
        if (string.IsNullOrWhiteSpace(formaPagamento.Descricao))
            throw new Exception("Descrição é obrigatório!");

        formaPagamento.DataCriacao = DateTime.Now;
        formaPagamento.DataAtualizacao = DateTime.Now;
        Criar(formaPagamento);
    }

    public void AtualizarFormaPagamento(FormaPagamento formaPagamento)
    {
        if (formaPagamento.Id == 0)
            throw new Exception("Forma de pagamento não encontrada para atualização.");

        if (string.IsNullOrWhiteSpace(formaPagamento.Descricao))
            throw new Exception("Descrição é obrigatória.");

        formaPagamento.DataAtualizacao = DateTime.Now;
        AtualizarFormaPagamento(formaPagamento);
    }
}

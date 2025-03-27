using ProjetoPF.Model;
using ProjetoPF.Modelos.Pagamento;
using System;

public class CondicaoPagamentoParcelas : BaseModelos
{
    public int IdCondicaoPagamento { get; set; }  
    public int IdFormaPagamento { get; set; }  
    public int NumParcela { get; set; }
    public int Prazo { get; set; }
    public decimal Porcentagem { get; set; }
}
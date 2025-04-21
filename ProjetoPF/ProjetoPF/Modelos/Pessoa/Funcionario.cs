using ProjetoPF.Modelos.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Pessoa
{
    public class Funcionario: BasePessoa
    {
        public string Matricula { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime? DataNascimentoCriacao { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string Turno { get; set; }
        public string CargaHoraria { get; set; }
        public int IdCidade { get; set; }
    }
}

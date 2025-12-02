using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pessoa;
using System;
using System.Linq;

namespace ProjetoPF.Servicos
{
    public class ClienteServicos : BaseServicos<Cliente>
    {
        public ClienteServicos() : base(new BaseDao<Cliente>("Clientes")) { }

        public bool DocumentoDuplicado(Cliente cliente)
        {
            var todosClientes = BuscarTodos();

            if (cliente.Id != 0)
                todosClientes = todosClientes.FindAll(c => c.Id != cliente.Id);

            string tipo = cliente.TipoPessoa?.ToUpper() ?? "";

            if ((tipo == "FÍSICA" || tipo == "FISICA") && !string.IsNullOrWhiteSpace(cliente.CpfCnpj))
            {
                return todosClientes.Any(c => c.CpfCnpj == cliente.CpfCnpj);
            }

            if ((tipo == "JURÍDICA" || tipo == "JURIDICA") && !string.IsNullOrWhiteSpace(cliente.CpfCnpj))
            {
                return todosClientes.Any(c => c.CpfCnpj == cliente.CpfCnpj);
            }

            if ((tipo == "FÍSICA" || tipo == "FISICA") && string.IsNullOrWhiteSpace(cliente.CpfCnpj) && !string.IsNullOrWhiteSpace(cliente.RgInscricaoEstadual))
            {
                return todosClientes.Any(c => c.RgInscricaoEstadual == cliente.RgInscricaoEstadual);
            }

            return false;
        }

    }
}

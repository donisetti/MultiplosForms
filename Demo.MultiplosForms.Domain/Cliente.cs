using System;
using System.Collections.Generic;

namespace Demo.MultiplosForms.Domain
{
    public class Cliente
    {
        public static List<Cliente> ClientesCadastrados { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }

        public Cliente()
        {
            if (ClientesCadastrados == null)
                ClientesCadastrados = new List<Cliente>();
        }


    }
}

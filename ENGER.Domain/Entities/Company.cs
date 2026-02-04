using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Company
    {
        public int CodigoEmpresa { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public Guid CodigoAssinatura { get; private set; }

        protected Company() { }

        public Company(Guid codigoAssinatura)
        {
            CodigoAssinatura = codigoAssinatura;
            DataEntrada = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}

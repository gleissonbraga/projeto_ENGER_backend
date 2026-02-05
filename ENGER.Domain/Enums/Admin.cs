using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Enums
{
    public enum Admin
    {
        Master = 7, // Dono do site
        Especial = 6, // Owner
        Gestor = 5, // Engenheiro/Arquiteto
        Encarregado = 4, // Fiscal Obra
        Administrador = 3, // Administrador Obra/ Administrador Geral
        Fiscal = 2, // Fiscal Obra/ Sub encarregado
        Colaborador = 1, // Funcionario comum / Operacional
    }
}

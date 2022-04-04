using GlamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Repository.Interfaces
{
    public interface IEmpresaRepository
    {
        void Atualizar(Empresa empresa);
        Empresa ObterEmpresa();
    }
}

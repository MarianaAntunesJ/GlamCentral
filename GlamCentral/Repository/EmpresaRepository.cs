using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public EmpresaRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        public void Atualizar(Empresa empresa)
        {
            var empresaExiste = ObterEmpresa();
            if(empresaExiste != null)
            {
                empresaExiste.RazaoSocial = empresa.RazaoSocial;
                empresaExiste.CNPJ = empresa.CNPJ;
                empresaExiste.Telefone = empresa.Telefone;
                _banco.Update(empresaExiste);
            }
            else
            {
                _banco.Add(empresa);
            }            
            _banco.SaveChanges();
        }

        public Empresa ObterEmpresa()
            => _banco.Empresa.FirstOrDefault();
    }
}

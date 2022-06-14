using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace GlamCentral.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public AgendaRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion


        public bool Cadastrar(Agenda agenda)
        {
            if (ObterAgendamento(agenda.Id) == null)
            {
                _banco.Add(agenda);
                _banco.SaveChanges();
                return true;
            }
            else
            {
                Atualizar(agenda);
                return true;
            }
        }

        public void Atualizar(Agenda agendaNova)
        {
            if (agendaNova != null)
            {
                var local = _banco.Set<Agenda>()
                    .Local
                    .FirstOrDefault(_ => _.Id.Equals(agendaNova.Id));

                if (local != null)
                    _banco.Entry(local).State = EntityState.Detached;

                _banco.Entry(agendaNova).State = EntityState.Modified;
                _banco.SaveChanges();
            }
        }

        public bool Excluir(int id)
        {
            var agenda = ObterAgendamento(id);
            if (agenda != null)
            {
                _banco.Remove(agenda);
                _banco.SaveChanges();
                return true;
            }
            return false;
        }

        public Agenda ObterAgendamento(int id)
        => _banco.Agenda.Find(id);

        public IEnumerable<Agenda> ObterTodosAgendamentos()
        => _banco.Agenda;
    }
}

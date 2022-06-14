using GlamCentral.Models;
using System.Collections.Generic;

namespace GlamCentral.Repository.Interfaces
{
    public interface IAgendaRepository
    {
        bool Cadastrar(Agenda agenda);
        void Atualizar(Agenda agenda);
        bool Excluir(int id);

        Agenda ObterAgendamento(int id);
        IEnumerable<Agenda> ObterTodosAgendamentos();

    }
}

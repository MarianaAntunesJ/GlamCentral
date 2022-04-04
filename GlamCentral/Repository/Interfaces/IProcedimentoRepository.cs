﻿using GlamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IProcedimentoRepository
    {
        void Cadastrar(Procedimento procedimento);
        void Atualizar(Procedimento procedimento);
        void Excluir(int id);
        Procedimento ObterProcedimento(int id);
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa);
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao, bool status);
    }
}
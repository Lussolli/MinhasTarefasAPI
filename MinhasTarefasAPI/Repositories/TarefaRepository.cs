using System;
using System.Collections.Generic;
using System.Linq;
using MinhasTarefasAPI.Database;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;

namespace MinhasTarefasAPI.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContexto _contexto;
        public TarefaRepository(MinhasTarefasContexto contexto)
        {
            _contexto = contexto;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSincronizacao)
        {
            var query = _contexto
                .Tarefas
                .Where(a => a.UsuarioId == usuario.Id)
                .AsQueryable();
            
            if (dataUltimaSincronizacao != null)
            {
                query
                    .Where(a => a.Criado >= dataUltimaSincronizacao || a.Atualizado <= dataUltimaSincronizacao);
            }

            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sincronizacao(List<Tarefa> tarefas)
        {
            // Cadastrar novos registros
            var tarefasNovas = tarefas.Where(a => a.IdTarefaApi == 0);
            if (tarefasNovas.Count() > 0)
            {
                foreach (var tarefa in tarefasNovas)
                {
                    _contexto.Tarefas.Add(tarefa);
                }
            }

            // Atualização de registro (Excluído)
            var tarefasExcluidasAtualizadas = tarefas.Where(a => a.IdTarefaApi != 0);
            if (tarefasExcluidasAtualizadas.Count() > 0)
            {
                foreach (var tarefa in tarefasExcluidasAtualizadas)
                {
                    _contexto.Tarefas.Update(tarefa);
                }
            }
            _contexto.SaveChanges();

            return tarefasNovas.ToList();
        }
    }
}

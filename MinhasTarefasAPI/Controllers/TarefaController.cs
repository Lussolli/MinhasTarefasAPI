using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories.Contracts;

namespace MinhasTarefasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public TarefaController(ITarefaRepository tarefaRepository, UserManager<ApplicationUser> userManager)
        {
            _tarefaRepository = tarefaRepository;
            _userManager = userManager;
        }

        public ActionResult Sincronizar([FromBody]List<Tarefa> tarefas)
        {
            return Ok(_tarefaRepository.Sincronizacao(tarefas));
        }

        public ActionResult Restaurar(DateTime data)
        {
            ApplicationUser usuario = _userManager.GetUserAsync(HttpContext.User).Result;
            return Ok(_tarefaRepository.Restauracao(usuario, data));
        }
    }
}

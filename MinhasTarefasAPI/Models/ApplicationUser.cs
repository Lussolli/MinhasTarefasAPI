using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MinhasTarefasAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
using ProjetoEmprestimosLivroCurso.Dto.Endereco;
using ProjetoEmprestimosLivroCurso.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimosLivroCurso.Dto.Usuario
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o Usuario!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione um Perfil!")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Selecione um Turno!")]
        public TurnoEnum Turno { get; set; }
        public EnderecoEditarDto Endereco { get; set; }

    }
}
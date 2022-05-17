using System.ComponentModel.DataAnnotations;
using BlogPessoal.src.utilidades;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo usuario</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NovoUsuarioDTO
    {
        [Required, StringLength(50)]
        public string Nome { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Senha { get; set; }

        public string Foto { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }

        public NovoUsuarioDTO(string nome, string email, string senha, string foto, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Foto = foto;
            Tipo = tipo;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um usuario</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarUsuarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Nome { get; set; }

        [Required, StringLength(30)]
        public string Senha { get; set; }

        public string Foto { get; set; }

        public AtualizarUsuarioDTO(int id, string nome, string senha, string foto)
        {
            Id = id;
            Nome = nome;
            Senha = senha;
            Foto = foto;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.modelos
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_postagens no banco.</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    [Table("tb_postagens")]
    public class PostagemModelo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [ForeignKey("fk_usuario"), InverseProperty("MinhasPostagens")]
        public UsuarioModelo Criador { get; set; }

        [ForeignKey("fk_tema"), InverseProperty("PostagensRelacionadas")]
        public TemaModelo Tema { get; set; }
    }
}

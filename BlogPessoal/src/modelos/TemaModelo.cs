using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.modelos
{
    [Table("tb_temas")]
    public class TemaModelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Descricao { get; set; }

        [JsonIgnore]
        public List<PostagemModelo> PostagensRelacionadas { get; set; }
    }
}

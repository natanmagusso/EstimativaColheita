using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados das variedades.
    /// </summary>
    public class VariedadeModel
    {
        /// <summary>
        /// Campo id da variedade.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo código interno da variedade.
        /// </summary>
        [Required(ErrorMessage = "Código obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Código inválido")]
        [Display(Name = "Código")]
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo descrição da variedade.
        /// </summary>
        [Required(ErrorMessage = "Descrição obrigatória")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Classe coleção dos talhões.
        /// </summary>
        public List<TalhaoModel> Talhoes { get; set; }

        /// <summary>
        /// Campo descrição completa da variedade.
        /// </summary>
        [NotMapped]
        [Display(Name = "Descrição")]
        public string DescricaoCompleta => CodigoInterno.ToString() + " | " + Descricao;
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class VariedadeConfiguration : IEntityTypeConfiguration<VariedadeModel>
    {
        public void Configure(EntityTypeBuilder<VariedadeModel> builder)
        {
            builder.ToTable("Variedades");
            builder.Property(var => var.CodigoInterno).HasColumnType("int").IsRequired();
            builder.Property(var => var.Descricao).HasColumnType("varchar(50)").IsRequired();
            builder.Property(var => var.Ativo).HasColumnType("bit");
        }
    }
}
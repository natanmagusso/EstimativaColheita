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
        [Required(ErrorMessage = "O 'Código' é de preenchimento obrigatório")]
        [Display(Name = "Código")]
        [Range(1, 9999)]
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo descrição da variedade.
        /// </summary>
        [Required(ErrorMessage = "A 'Descrição' é de preenchimento obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        /// <summary>
        /// Classe coleção dos talhões.
        /// </summary>
        public List<TalhaoModel> Talhoes { get; set; }

        /// <summary>
        /// Campo descrição completa da variedade.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta
        {
            get
            {
                return CodigoInterno.ToString() + " | " + Descricao;
            }
        }
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
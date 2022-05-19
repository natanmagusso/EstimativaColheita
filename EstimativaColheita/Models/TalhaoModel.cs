using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos talhões.
    /// </summary>
    public class TalhaoModel
    {
        /// <summary>
        /// Campo id do talhão.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo código interno do talhão.
        /// </summary>
        [Range(1, 9999)]
        [Required(ErrorMessage = "O 'Código' é de preenchimento obrigatório")]        
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo ano do plantio.
        /// </summary>
        [Required(ErrorMessage = "O 'Ano do plantio' é de preenchimento obrigatório")]
        public int AnoPlantio { get; set; }

        /// <summary>
        /// Campo quantidade de pés.
        /// </summary>
        [Required(ErrorMessage = "A 'Quantidade de pés' é de preenchimento obrigatório")]
        public int QuantidadePes { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Campo id do contrato.
        /// </summary>
        [ForeignKey("IdContrato")]
        public int IdContrato { get; set; }
        /// <summary>
        /// Classe contrato.
        /// </summary>
        public ContratoModel Contrato { get; set; }

        /// <summary>
        /// Campo id da variedade.
        /// </summary>
        [ForeignKey("IdVariedade")]
        public int IdVariedade { get; set; }
        /// <summary>
        /// Classe variedade.
        /// </summary>
        public VariedadeModel Variedade { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }

        /// <summary>
        /// Classe coleção das colheitas realizadas.
        /// </summary>
        public List<ColheitaRealizadaModel> ColheitasRealizadas { get; set; }

        /// <summary>
        /// Campo descrição completa do talhão.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta { get { return CodigoInterno.ToString() + " | " + AnoPlantio.ToString(); }}
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class TalhaoConfiguration : IEntityTypeConfiguration<TalhaoModel>
    {
        public void Configure(EntityTypeBuilder<TalhaoModel> builder)
        {
            builder.ToTable("Talhoes");
            builder.Property(tal => tal.CodigoInterno).HasColumnType("int").IsRequired();
            builder.Property(tal => tal.AnoPlantio).HasColumnType("int").IsRequired();
            builder.Property(tal => tal.QuantidadePes).HasColumnType("int").IsRequired();
            builder.Property(tal => tal.Ativo).HasColumnType("bit");

            builder.HasOne<ContratoModel>(tal => tal.Contrato)
                .WithMany(con => con.Talhoes)
                .HasForeignKey(tal => tal.IdContrato)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<VariedadeModel>(tal => tal.Variedade)
                .WithMany(var => var.Talhoes)
                .HasForeignKey(tal => tal.IdVariedade)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
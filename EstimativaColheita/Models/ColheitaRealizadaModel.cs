using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados das colheitas realizadas.
    /// </summary>
    public class ColheitaRealizadaModel
    {
        /// <summary>
        /// Campo id da colheita realizada.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo data de lançamento da colheita realizada.
        /// </summary>
        public DateTime DataLancamento { get; set; }

        /// <summary>
        /// Campo quantidade de caixas.
        /// </summary>
        [Range(1, 999)]
        [Required(ErrorMessage = "A 'Quantidade de caixas' é de preenchimento obrigatório")]        
        public int Caixas { get; set; }

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
        /// Campo id do talhão.
        /// </summary>
        [ForeignKey("IdTalhao")]
        public int IdTalhao { get; set; }
        /// <summary>
        /// Classe talhão.
        /// </summary>
        public TalhaoModel Talhao { get; set; }
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class ColheitaRealizadaConfiguration : IEntityTypeConfiguration<ColheitaRealizadaModel>
    {
        public void Configure(EntityTypeBuilder<ColheitaRealizadaModel> builder)
        {
            builder.ToTable("ColheitasRealizadas");
            builder.Property(col => col.Caixas).HasColumnType("int").IsRequired();

            builder.HasOne<ContratoModel>(col => col.Contrato)
                .WithMany(con => con.ColheitasRealizadas)
                .HasForeignKey(col => col.IdContrato)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TalhaoModel>(col => col.Talhao)
                .WithMany(tal => tal.ColheitasRealizadas)
                .HasForeignKey(col => col.IdTalhao)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
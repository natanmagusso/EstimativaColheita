using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados das estimativas.
    /// </summary>
    public class EstimativaColheitaModel
    {
        /// <summary>
        /// Campo id da estimativa.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo data de lançamento da estimativa.
        /// </summary>
        [Display(Name = "Data de lançamento")]
        public DateTime DataLancamento { get; set; }

        /// <summary>
        /// Campo quantidade da fruta.
        /// </summary>
        [Required(ErrorMessage = "A 'Quantidade de caixas' é de preenchimento obrigatório")]
        [Display(Name = "Caixas")]
        public int Caixas { get; set; }

        /// <summary>
        /// Campo data de alteração da estimativa.
        /// </summary>
        [Display(Name = "Data de alteração")]
        public DateTime DataAlteracao { get; set; }

        /// <summary>
        /// Campo id do contrato.
        /// </summary>
        [ForeignKey("IdContrato")]
        [Display(Name = "Contrato")]
        public int IdContrato { get; set; }
        /// <summary>
        /// Classe contrato.
        /// </summary>
        [Display(Name = "Contrato")]
        public ContratoModel Contrato { get; set; }

        /// <summary>
        /// Campo id do talhão.
        /// </summary>
        [ForeignKey("IdTalhao")]
        [Display(Name = "Talhão")]
        public int IdTalhao { get; set; }
        /// <summary>
        /// Classe talhão.
        /// </summary>
        [Display(Name = "Talhão")]
        public TalhaoModel Talhao { get; set; }

        /// <summary>
        /// Campo id do motivo de alteração.
        /// </summary>
        [ForeignKey("IdMotivoAlteracao")]
        [Display(Name = "Motivo de alteração")]
        public int IdMotivoAlteracao { get; set; }
        /// <summary>
        /// Classe motivo de alteração.
        /// </summary>
        [Display(Name = "Motivo de alteração")]
        public MotivoAlteracaoModel MotivoAlteracao { get; set; }

        /// <summary>
        /// Campo id do tipo de lançamento.
        /// </summary>
        [ForeignKey("IdTipoLancamento")]
        [Display(Name = "Tipo de lançamento")]
        public int IdTipoLancamento { get; set; }
        /// <summary>
        /// Classe tipo de lançamento.
        /// </summary>
        [Display(Name = "Tipo de lançamento")]
        public TipoLancamentoModel TipoLancamento { get; set; }
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class EstimativaColheitaConfiguration : IEntityTypeConfiguration<EstimativaColheitaModel>
    {
        public void Configure(EntityTypeBuilder<EstimativaColheitaModel> builder)
        {
            builder.ToTable("EstimativasColheita");
            builder.Property(est => est.Caixas).HasColumnType("int").IsRequired();

            builder.HasOne<ContratoModel>(est => est.Contrato)
                .WithMany(con => con.Estimativas)
                .HasForeignKey(est => est.IdContrato)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TalhaoModel>(est => est.Talhao)
                .WithMany(tal => tal.Estimativas)
                .HasForeignKey(est => est.IdTalhao)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<MotivoAlteracaoModel>(est => est.MotivoAlteracao)
                .WithMany(mot => mot.Estimativas)
                .HasForeignKey(est => est.IdMotivoAlteracao)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TipoLancamentoModel>(est => est.TipoLancamento)
                .WithMany(tip => tip.Estimativas)
                .HasForeignKey(est => est.IdTipoLancamento)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime DataLancamento { get; set; }

        /// <summary>
        /// Campo quantidade da fruta.
        /// </summary>
        [Required(ErrorMessage = "Caixas obrigatórias")]
        [DisplayFormat(DataFormatString = "{0:#}")]
        public int Caixas { get; set; }

        /// <summary>
        /// Campo id do contrato.
        /// </summary>
        [ForeignKey("IdContrato")]
        [Required(ErrorMessage = "Contrato obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo inválido")]
        public int IdContrato { get; set; }

        /// <summary>
        /// Campo id do talhão.
        /// </summary>
        [ForeignKey("IdTalhao")]
        [Required(ErrorMessage = "Talhão obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo inválido")]
        public int IdTalhao { get; set; }

        /// <summary>
        /// Campo id do encarregado.
        /// </summary>
        [ForeignKey("IdEncarregado")]
        [Required(ErrorMessage = "Coordenador obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo inválido")]
        public int IdEncarregado { get; set; }

        /// <summary>
        /// Campo id do motivo de alteração.
        /// </summary>
        [ForeignKey("IdEstimativaMotivo")]
        [Required(ErrorMessage = "Motivo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo inválido")]
        public int IdEstimativaMotivo { get; set; }

        /// <summary>
        /// Campo id do tipo de lançamento.
        /// </summary>
        [ForeignKey("IdTipoLancamento")]
        public int IdTipoLancamento { get; set; }

        /// <summary>
        /// Classe contrato.
        /// </summary>
        public ContratoModel Contrato { get; set; }

        /// <summary>
        /// Classe talhão.
        /// </summary>
        public TalhaoModel Talhao { get; set; }

        /// <summary>
        /// Classe encarregado.
        /// </summary>
        public EncarregadoModel Encarregado { get; set; }

        /// <summary>
        /// Classe motivo de estimativa.
        /// </summary>
        public EstimativaMotivoModel EstimativaMotivo { get; set; }

        /// <summary>
        /// Classe tipo de lançamento.
        /// </summary>
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

            builder.HasOne<EncarregadoModel>(est => est.Encarregado)
                .WithMany(enc => enc.Estimativas)
                .HasForeignKey(est => est.IdEncarregado)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ContratoModel>(est => est.Contrato)
                .WithMany(con => con.Estimativas)
                .HasForeignKey(est => est.IdContrato)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TalhaoModel>(est => est.Talhao)
                .WithMany(tal => tal.Estimativas)
                .HasForeignKey(est => est.IdTalhao)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<EstimativaMotivoModel>(est => est.EstimativaMotivo)
                .WithMany(mot => mot.Estimativas)
                .HasForeignKey(est => est.IdEstimativaMotivo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TipoLancamentoModel>(est => est.TipoLancamento)
                .WithMany(tip => tip.Estimativas)
                .HasForeignKey(est => est.IdTipoLancamento)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
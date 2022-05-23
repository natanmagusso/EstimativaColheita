using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos tipos de lançamento.
    /// </summary>
    public class TipoLancamentoModel
    {
        /// <summary>
        /// Campo id do tipo de lançamento.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo descrição do tipo de lançamento.
        /// </summary>
        [Required(ErrorMessage = "Descrição obrigatória")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }

        /// <summary>
        /// Campo descrição completa do tipo de lançamento.
        /// </summary>
        [NotMapped]
        [Display(Name = "Tipo")]
        public string DescricaoCompleta => Descricao;
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class TipoLancamentoConfiguration : IEntityTypeConfiguration<TipoLancamentoModel>
    {
        public void Configure(EntityTypeBuilder<TipoLancamentoModel> builder)
        {
            builder.ToTable("TiposLancamento").HasData(
                new TipoLancamentoModel { Id = 1, Descricao = "ESTIMADO" },
                new TipoLancamentoModel { Id = 2, Descricao = "COLHIDO" }
            );

            builder.Property(tip => tip.Descricao).HasColumnType("varchar(50)").IsRequired();
        }
    }
}
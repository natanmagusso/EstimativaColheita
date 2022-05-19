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
        /// Classe usada para imputar as informações na base de dados dentro do arquivo AppDbContext.
        /// </summary>
        public TipoLancamentoModel(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        /// <summary>
        /// Campo id do tipo de lançamento.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo descrição do tipo de lançamento.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class TipoLancamentoConfiguration : IEntityTypeConfiguration<TipoLancamentoModel>
    {
        public void Configure(EntityTypeBuilder<TipoLancamentoModel> builder)
        {
            builder.ToTable("TiposLancamento").
                HasData(new List<TipoLancamentoModel>() {
                    new TipoLancamentoModel(1, "ESTIMADO"),
                    new TipoLancamentoModel(2, "COLHIDO"),
            });

            builder.Property(tip => tip.Descricao).HasColumnType("varchar(50)").IsRequired();
        }
    }
}
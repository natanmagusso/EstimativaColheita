using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos encarregados.
    /// </summary>
    public class EncarregadoModel
    {
        /// <summary>
        /// Campo id do contrato.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo código interno do encarregado.
        /// </summary>        
        [Range(1, 99999)]
        [Required(ErrorMessage = "O 'Código' é de preenchimento obrigatório")]
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo nome do encarregado.
        /// </summary>
        [Required(ErrorMessage = "O 'Nome' é de preenchimento obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Campo id do fiscal de campo.
        /// </summary>
        [ForeignKey("IdFiscalCampo")]
        public int IdFiscalCampo { get; set; }
        /// <summary>
        /// Classe fiscal de campo.
        /// </summary>
        public FiscalCampoModel FiscalCampo { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }

        /// <summary>
        /// Classe coleção das colheitas realizadas.
        /// </summary>
        public List<ColheitaRealizadaModel> ColheitasRealizadas { get; set; }

        /// <summary>
        /// Campo descrição completa do encarregado.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta { get { return CodigoInterno.ToString() + " | " + Nome; } }
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class EncarregadoConfiguration : IEntityTypeConfiguration<EncarregadoModel>
    {
        public void Configure(EntityTypeBuilder<EncarregadoModel> builder)
        {
            builder.ToTable("Encarregados");
            builder.Property(con => con.CodigoInterno).HasColumnType("int").IsRequired();
            builder.Property(con => con.Nome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(con => con.Ativo).HasColumnType("bit");

            builder.HasOne<FiscalCampoModel>(con => con.FiscalCampo)
                .WithMany(fis => fis.Encarregados)
                .HasForeignKey(con => con.IdFiscalCampo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

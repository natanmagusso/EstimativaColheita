using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos contratos.
    /// </summary>
    public class ContratoModel
    {
        /// <summary>
        /// Campo id do contrato.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo código interno do contrato.
        /// </summary>        
        [Display(Name = "Código")]
        [Range(1, 99999)]
        [Required(ErrorMessage = "O 'Código' é de preenchimento obrigatório")]
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo propriedade do contrato.
        /// </summary>
        [Required(ErrorMessage = "O 'Nome da propriedade' é de preenchimento obrigatório")]
        [Display(Name = "Propriedade")]
        public string Propriedade { get; set; }

        /// <summary>
        /// Campo titular do contrato.
        /// </summary>
        [Required(ErrorMessage = "O 'Nome do titular' é de preenchimento obrigatório")]
        [Display(Name = "Titular")]
        public string Titular { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        /// <summary>
        /// Campo id do fiscal de campo.
        /// </summary>
        [ForeignKey("IdFiscalCampo")]
        [Display(Name = "Coordenador/Supervisor")]
        public int IdFiscalCampo { get; set; }
        /// <summary>
        /// Classe fiscal de campo.
        /// </summary>
        [Display(Name = "Coordenador/Supervisor")]
        public FiscalCampoModel FiscalCampo { get; set; }

        /// <summary>
        /// Classe coleção dos talhões.
        /// </summary>
        public List<TalhaoModel> Talhoes { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }

        /// <summary>
        /// Classe coleção das colheitas realizadas.
        /// </summary>
        public List<ColheitaRealizadaModel> ColheitasRealizadas { get; set; }

        /// <summary>
        /// Campo descrição completa do contrato.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta
        {
            get
            {
                return CodigoInterno.ToString() + " | " + Propriedade;
            }
        }
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class ContratoConfiguration : IEntityTypeConfiguration<ContratoModel>
    {
        public void Configure(EntityTypeBuilder<ContratoModel> builder)
        {
            builder.ToTable("Contratos");
            builder.Property(con => con.CodigoInterno).HasColumnType("int").IsRequired();
            builder.Property(con => con.Propriedade).HasColumnType("varchar(200)").IsRequired();
            builder.Property(con => con.Titular).HasColumnType("varchar(200)").IsRequired();
            builder.Property(con => con.Ativo).HasColumnType("bit");

            builder.HasOne<FiscalCampoModel>(con => con.FiscalCampo)
                .WithMany(fis => fis.Contratos)
                .HasForeignKey(con => con.IdFiscalCampo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
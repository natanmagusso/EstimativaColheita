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
        [Required(ErrorMessage = "Código obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Código inválido")]
        [Display(Name = "Código")]
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo propriedade do contrato.
        /// </summary>
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [Display(Name = "Propriedade")]
        public string Propriedade { get; set; }

        /// <summary>
        /// Campo titular do contrato.
        /// </summary>
        [Required(ErrorMessage = "Titular obrigatório")]
        [Display(Name = "Titular")]
        public string Titular { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

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
        [Display(Name = "Descrição")]
        public string DescricaoCompleta => CodigoInterno.ToString() + " | " + Propriedade;
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
        }
    }
}
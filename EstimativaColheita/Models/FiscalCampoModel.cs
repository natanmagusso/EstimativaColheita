using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos fiscais de campo.
    /// </summary>
    public class FiscalCampoModel
    {
        /// <summary>
        /// Campo id do fiscal de campo.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo código interno do fiscal de campo.
        /// </summary>
        [Range(1, 99)]
        [Required(ErrorMessage = "O 'Código' é de preenchimento obrigatório")]      
        public int CodigoInterno { get; set; }

        /// <summary>
        /// Campo nome do fiscal de campo.
        /// </summary>
        [Required(ErrorMessage = "O 'Nome' é de preenchimento obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Campo apelido do fiscal de campo.
        /// </summary>
        [Required(ErrorMessage = "O 'Apelido' é de preenchimento obrigatório")]
        public string Apelido { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Classe coleção dos encarregados.
        /// </summary>
        public List<EncarregadoModel> Encarregados { get; set; }

        /// <summary>
        /// Campo descrição completa do fiscal de campo.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta { get { return CodigoInterno.ToString() + " | " + Apelido; }}
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class FiscalCampoConfiguration : IEntityTypeConfiguration<FiscalCampoModel>
    {
        public void Configure(EntityTypeBuilder<FiscalCampoModel> builder)
        {
            builder.ToTable("FiscaisCampo");
            builder.Property(fis => fis.CodigoInterno).HasColumnType("int").IsRequired();
            builder.Property(fis => fis.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(fis => fis.Apelido).HasColumnType("varchar(20)").IsRequired();
            builder.Property(fis => fis.Ativo).HasColumnType("bit");
        }
    }
}
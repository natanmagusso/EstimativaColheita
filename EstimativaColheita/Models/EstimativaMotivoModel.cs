﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstimativaColheita.Models
{
    /// <summary>
    /// Classe modelo para gerenciamento dos dados dos motivos de alterações.
    /// </summary>
    public class EstimativaMotivoModel
    {
        /// <summary>
        /// Campo id do motivo de alteração.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Campo descrição do motivo de alteração.
        /// </summary>
        [Required(ErrorMessage = "Descrição obrigatória")]
        public string Descricao { get; set; }

        /// <summary>
        /// Campo para verificar se o registro está ativo ou não.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Classe coleção das estimativas.
        /// </summary>
        public List<EstimativaColheitaModel> Estimativas { get; set; }

        /// <summary>
        /// Campo descrição completa do motivo de alteração.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta => Descricao;
    }

    /// <summary>
    /// Classe modelo para configuração da tabela no banco de dados.
    /// </summary>
    public class EstimativaMotivoConfiguration : IEntityTypeConfiguration<EstimativaMotivoModel>
    {
        public void Configure(EntityTypeBuilder<EstimativaMotivoModel> builder)
        {
            builder.ToTable("EstimativaMotivos").HasData(
                new EstimativaMotivoModel { Id = 1, Descricao = "APONTAMENTO DE CAMPO", Ativo = true }
            );

            builder.Property(mot => mot.Descricao).HasColumnType("varchar(200)").IsRequired();
            builder.Property(mot => mot.Ativo).HasColumnType("bit");
        }
    }
}
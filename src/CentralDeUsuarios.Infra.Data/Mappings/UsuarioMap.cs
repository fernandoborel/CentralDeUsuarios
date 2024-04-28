using CentralDeUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralDeUsuarios.Infra.Data.Mappings;

/// <summary>
/// Classe de mapeamento ORM da entidade Usuario.
/// </summary>
public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasIndex(u => u.Email).IsUnique();
    }
}

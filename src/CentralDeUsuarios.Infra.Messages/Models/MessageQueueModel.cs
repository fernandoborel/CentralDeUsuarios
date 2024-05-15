namespace CentralDeUsuarios.Infra.Messages.Models;

/// <summary>
/// Classe para modelar as msgs da fila
/// </summary>
public class MessageQueueModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Conteudo { get; set; }
    public DateTime? DataHoraCriacao { get; set; } = DateTime.Now;
}

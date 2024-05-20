namespace CentralDeUsuarios.Infra.Messages.Models;

/// <summary>
/// Classe para modelar as msgs da fila
/// </summary>
public class MessageQueueModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public TipoMensagem? Tipo { get; set; }
    public string? Conteudo { get; set; }
    public DateTime? DataHoraCriacao { get; set; } = DateTime.Now;
}

public enum TipoMensagem
{
    CONFIRMACAO_DE_CADASTRO = 1,
    RECUPERACAO_DE_SENHA = 2
}

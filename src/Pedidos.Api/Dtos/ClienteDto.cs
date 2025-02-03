using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pedidos.Api.Dtos;

public class ClienteDto
{
    [JsonPropertyName("id")]
    [Display(Name = "Id")]
    public Guid? ClienteId { get; private set; }

    [Display(Name = "CPF")]
    [Required(ErrorMessage = "CPF deve estar preenchido.")]
    [RegularExpression(@"^(\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "CPF inválido")]
    public string Cpf { get; set; } = null!;

    [Required(ErrorMessage = "O nome é requerido.")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Nome")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O email deve ser informado", AllowEmptyStrings = false)]
    [EmailAddress(ErrorMessage = "Formato do email inválido")]
    public string Email { get; set; } = null!;

    public void SetId(Guid id)
    {
        ClienteId = id;
    }
}
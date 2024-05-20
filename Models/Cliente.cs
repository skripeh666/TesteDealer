using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace testeDealer.Models
{

    public class Cliente
    {
        [JsonPropertyName("idCliente")]
        [Key]
        [DisplayName("Cliente")]
        public int IdCliente { get; set; }

        [JsonPropertyName("nmCliente")]
        [DisplayName("Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
        public string NmCliente { get; set; }

        [JsonPropertyName("Cidade")]
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Cidade é obrigatória")]
        [StringLength(100, ErrorMessage = "Cidade não pode ter mais que 100 caracteres")]
        public string Cidade { get; set; }
    }
}


using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace testeDealer.Models
{

    public class Produto
    {
        [JsonProperty("idProduto")]
        [DisplayName("Produto")]
        [Key]
        public int IdProduto { get; set; }

        [JsonProperty("dscProduto")]
        [DisplayName("Descrição do Produto")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "Descrição não pode ter mais que 200 caracteres")]
        public string DscProduto { get; set; }


        [JsonProperty("vlrUnitario")]
        [DisplayName("Valor unitário")]
        [Required(ErrorMessage = "Valor Unitário é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor Unitário deve ser maior que zero")]
        public float VlrUnitario { get; set; }
    }
}
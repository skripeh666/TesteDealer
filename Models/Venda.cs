using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace testeDealer.Models
{

    public class Venda
    {
        [JsonProperty("idVenda")]
        [Key]
        [DisplayName("Venda")]
        public int IdVenda { get; set; }

        [JsonProperty("idCliente")]
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "Cliente é obrigatório")]
        public int IdCliente { get; set; }

        [JsonProperty("idProduto")]
        [DisplayName("Produto")]
        [Required(ErrorMessage = "Produto é obrigatório")]
        public int IdProduto { get; set; }

        [JsonProperty("qtdVenda")]
        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int QtdVenda { get; set; }

        [JsonProperty("vlrUnitarioVenda")]
        [DisplayName("Valor unitário")]
        [Required(ErrorMessage = "Valor Unitário é obrigatório")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Valor Unitário deve ser maior que zero")]
        public float VlrUnitarioVenda { get; set; }

        [JsonProperty("dthVenda")]
        [Required(ErrorMessage = "Data da Venda é obrigatória")]
        [DisplayName("Data da Venda")]
        public DateTime DthVenda { get { return DateTime.Now; } }


        [DisplayName("Valor Total")]
        public float VlrTotalVenda
        {
            get { return QtdVenda * VlrUnitarioVenda; }
        }


        public virtual Cliente Cliente { get; set; }
        public virtual Produto Produto { get; set; }

    }

}
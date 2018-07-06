using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Models
{
    public class Evento
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome do evento")]
        [MaxLength(100, ErrorMessage = "O valor máximo de caracteres é 100")]
        [MinLength(2, ErrorMessage = "O valor minimo de caracters é 2")]
        public string Nome { get; set; }

        [Range(10, 1000)]
        [Required(ErrorMessage = "Preencha o valor")]
        public decimal Valor { get; set; }
        public bool Gratuito { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório")]
        [MaxLength(500, ErrorMessage = "o valor máximo de caracteres é 1000")]
        public string Descricao { get; set; }

        public DateTime Data { get; set; }
    }
}

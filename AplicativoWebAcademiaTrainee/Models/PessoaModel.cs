using System.ComponentModel.DataAnnotations;

namespace AplicativoWebAcademiaTrainee.Models
{
    public class PessoaModel
    {
        [Key]
        public int Codigo { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Quantidade de filhos")]
        public int QuantidadeFilhos { get; set; }
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }
        [Display(Name = "Situação")]
        public string Situacao { get; set; }

    }
}

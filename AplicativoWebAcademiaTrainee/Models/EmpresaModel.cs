using System.ComponentModel.DataAnnotations;

namespace AplicativoWebAcademiaTrainee.Models
{
    public class EmpresaModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        [Key]
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        public string? NomeFantasia { get; set; }
        public string? CNPJ { get; set; }
    }
}
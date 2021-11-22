namespace AplicativoWebAcademiaTrainee.Models
{
    public class EmpresaModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
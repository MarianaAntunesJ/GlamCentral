namespace GlamCentral.Models
{
    public class ErrorViewModel
    {
        #region "Propriedades Públicas
        public string RequestId { get; set; }
        public string Message { get; set; }
        #endregion

        #region "Métodos Públicos"
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion
    }
}

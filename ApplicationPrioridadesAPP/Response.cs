
namespace ApplicationPrioridadesAPP
{
    public enum ErrorCodes
    {
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_ID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,

        CATEGORIA_DUPLICATE = 5,
        CATEGORIA_NOT_FOUND = 6,

        PRIORIDADE_NOT_FOUND = 7

    }
    public abstract class Response
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ErrorCodes ErrorCode { get; set; }

    }
}

    

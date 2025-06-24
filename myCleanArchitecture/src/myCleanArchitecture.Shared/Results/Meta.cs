using System.Net;

namespace myCleanArchitecture.Shared.Results
{
    public class Meta
    {
        public Meta()
        {
                
        }
        public Meta(bool ısSuccess, string message, HttpStatusCode statusCode)
        {
            IsSuccess = ısSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }

        public static Meta Success(string message = "İşlem Başarılı", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta Error(string message = "İşlem Başarısız", HttpStatusCode statusCode = HttpStatusCode.FailedDependency)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta NotFound(string message = "Veri Bulunamadı", HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta BadRequest(string message = "Geçersiz İstek", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta Unauthorized(string message = "Yetkisiz İstek", HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta ServerError(string message = "Server Hatası", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta ValidationError(string message = "Gerekli Alanları Giriniz", HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Meta(true, message, statusCode);
        }
        public static Meta Custom(bool isSuccess, string message, HttpStatusCode statusCode)
        {
            return new Meta(isSuccess, message, statusCode);
        }
    }
}

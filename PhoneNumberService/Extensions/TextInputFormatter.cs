using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace PhoneNumberService.Extensions
{
    
    // This class tells the controller how to serialise text/plain requests.
    // Helped me test the calls using swagger easier +441234568 instead of this "+441234568"
    public class TextInputFormatter : InputFormatter
    {
        private const string _contentType = "text/plain";

        public TextInputFormatter()
        {
            SupportedMediaTypes.Add(_contentType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var contentType = context.HttpContext.Request.ContentType;
            return contentType.StartsWith(_contentType);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.Http
{
    public interface IResponse
    {
        MediaTypeFormatterCollection Formatters { get; }

        Task<HttpResponseMessage> AsMessage();

        Task<T> As<T>();

        Task<List<T>> AsList<T>();

        Task<byte[]> AsByteArray();

        Task<string> AsString();

        Task<Stream> AsStream();
    }
}
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Miki.Extensions.POSTLinux
{
    public class POST
    {
        public string UploadString(object content, string website)
        {
            string result = "";
            WebRequest request = WebRequest.Create(website);
            string postData = JsonConvert.SerializeObject(content);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            return result;
        }
    }
}
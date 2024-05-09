using System;
using System.Net;
using System.Text;
using System.Threading;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up HTTP listener
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            Console.WriteLine("Listening for incoming requests...");

            while (true)
            {
                // Wait for a request
                var context = listener.GetContext();
                var request = context.Request;

                // Read the incoming request data
                var requestData = new StringBuilder();
                using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                {
                    requestData.Append(reader.ReadToEnd());
                }

                // Send back the received data as response
                var responseData = Encoding.UTF8.GetBytes(requestData.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.OutputStream.Write(responseData, 0, responseData.Length);
                context.Response.Close();

                // Output received request data
                Console.WriteLine($"Received message: {requestData}");
            }
        }
    }
}
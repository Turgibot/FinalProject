//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace CSA_Project.Controllers
//{
//    public class TestController : ApiController
//    {
//        public async Task<IHttpActionResult> GetImage(string data)
//        {
//            if (!File.Exists(@"C:/mohammad.txt"))
//            {
//                var f  = File.Create(@"C:/mohammad.txt");
//                f.Close();
//            }
//            var file = new StreamWriter(@"C:/mohammad.txt");
//            await file.WriteAsync(data);
//            file.Close();
//            return Ok("done");
//        }
//        [Route("api/Test")]
//        public async Task<IHttpActionResult> PostImage(long index)
//        {
//            /*string root = HttpContext.Current.Server.MapPath("~/App_Data");
//            var provider = new MultipartFormDataStreamProvider(root);
//            var t = await Request.Content.ReadAsStringAsync();*/

//            // This illustrates how to get the file names.
//            /*foreach (MultipartFileData file in provider.FileData)
//            {
//                Trace.WriteLine(file.Headers.ContentDisposition.FileName);
//                Trace.WriteLine("Server file path: " + file.LocalFileName);
//            }
//            return Ok("done");
//            if (!File.Exists(@"C:/mohammad.png"))
//            {
//                var f = File.Create(@"C:/mohammad.png");
//                f.Close();
//            }
//            byte[] bytes = Encoding.ASCII.GetBytes(t);
//            File.WriteAllText(@"C:/mohammad.png", Convert.ToBase64String(bytes));
//            return Ok("done");*/
//            HttpRequestMessage request = this.Request;
//            if (!request.Content.IsMimeMultipartContent())
//            {
//                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
//            }

//            string root = (@"C:\images");
//            var provider = new MultipartFormDataStreamProvider(root);

//            var task = await request.Content.ReadAsMultipartAsync(provider);
//            string file1 = provider.FileData[0].LocalFileName;
//            File.Move(provider.FileData[0].LocalFileName, root + @"\img-" + index + ".jpg");

            
//            return Ok("done");
//        }
//    }
//}

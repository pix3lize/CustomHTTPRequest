using System;
using System.IO;
using System.Net;
using System.Text;
using CustomHTTPRequestNS;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomHTTPRequest CRequest = new CustomHTTPRequest();
            CustomWebResponse CResponse = new CustomWebResponse();

            CookieContainer CContainer = new CookieContainer();

            // CResponse = CRequest.HTTPCustomRequest("https://docushare.xerox.com/doug/dsweb/ApplyLogin", "username=hendri.tjiptowibowo@sgp.fujixerox.com&password=FX_oofsc123&domain=DocuShare&Login=Login", ref CContainer);
            // CResponse.SafeToFile(@"C:\Development\DocushareDownload\theresponse.txt");
            // //GET  https://www.google.com 200 OK
            // Console.WriteLine(CResponse.Method + "  " +
            //     CResponse.UrlRequest + " " +
            //     CResponse.StatusCode + " " +
            //     CResponse.Status);

            // //Time taken : 00:00:00:05
            // //Console.WriteLine("Time taken : " + CResponse.TimeTaken);
            // //Console.WriteLine("Response : " + CResponse.Response);

            // CRequest.ReturnStream = true;
            // CResponse = CRequest.HTTPCustomRequest(@"https://docushare.xerox.com/doug/dsweb/Get/Document-121470", CContainer);


            // CResponse.SafeToFile(@"C:\Development\DocushareDownload\newfile.pdf");

            // Console.WriteLine("Hello World!");
            // CustomHTTPRequest CRequest = new CustomHTTPRequest();
            // CustomWebResponse CResponse = new CustomWebResponse();

            // CResponse = CRequest.HTTPCustomRequest("https://www.google.com");

            // //GET  https://www.google.com 200 OK
            // Console.WriteLine(CResponse.Method + "  " +
            //     CResponse.UrlRequest + " " +
            //     CResponse.StatusCode + " " +
            //     CResponse.Status);

            // //Time taken : 00:00:00:05
            // Console.WriteLine("Time taken : " + CResponse.TimeTaken);

            //REST API Sample
            RESTRequest RRequest = new RESTRequest();
            CResponse = new CustomWebResponse();

            CResponse = RRequest.RESTBearer(@"https://api.sandbox.transferwise.tech/v1/profiles", RESTRequest.Method.GET, "", "TokenHere");

            //GET  https://www.google.com 200 OK
            Console.WriteLine(CResponse.Method + "  " +
                CResponse.UrlRequest + " " +
                CResponse.StatusCode + " " +
                CResponse.Status);

            Console.WriteLine(CResponse.Response);

            //Time taken : 00:00:00:05
            Console.WriteLine("Time taken : " + CResponse.TimeTaken);
        }
    }
}

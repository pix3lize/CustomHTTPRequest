using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Net.Security;

namespace CustomHTTPRequestNS
{
    /// <summary>
    /// 
    /// </summary>
    public class RESTRequest : CustomHTTPRequest
    {
        /// <summary>
        /// Specify the method 
        /// </summary>
        public enum Method
        {
            /// <summary>
            /// Get method 
            /// </summary>
            GET = 0,
            /// <summary>
            /// Post method 
            /// </summary>
            POST = 1,
            /// <summary>
            /// PUT method 
            /// </summary>         
            PUT = 2,
            /// <summary>
            /// Delete method 
            /// </summary>  
            DELETE = 3,
            /// <summary>
            /// Patch method 
            /// </summary>  
            PATCH = 4
    
        }

        /// <summary>
        /// 
        /// </summary>
        public RESTRequest()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            UserAgent = "Agent X";

            XDeveloper = "";
            this.Timeout = 120 * 1000;

            CharEncode = Encoding.Default;
        }

        /// <summary>
        /// Token Authentication custom REST request
        /// Please specify the method as parameter 
        /// </summary>
        /// <param name="url">Web URL / REST API URL</param>
        /// <param name="method">Web request method</param>
        /// <param name="bodymessage">Body message string</param>
        /// <param name="Token">Your token / JWT</param>
        /// <returns>It will return the customer web response</returns>
        public CustomWebResponse RESTBearer(string url,Method method, string bodymessage, string Token)
        {
            HttpWebResponse Hresponse = null;
            Stream tstream = null;
            StreamReader reader = null;
            Stopwatch StopWatch = new Stopwatch();
            HttpWebRequest Hrequest = (HttpWebRequest)WebRequest.Create(url);
            CustomWebResponse CustWR;

            try
            {
                StopWatch.Start();
                byte[] Data = CharEncode.GetBytes(bodymessage);

                Hrequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                Hrequest.Timeout = this.Timeout;
                Hrequest.Method = method.ToString();
                Hrequest.UserAgent = UserAgent;
                Hrequest.ContentType = "application/json";

                Hrequest.Headers.Add("Authorization", @"Bearer " + Token);
                Hrequest.ContentLength = Data.Length;

                if (bodymessage != "")
                {
                    using (Stream stream = Hrequest.GetRequestStream())
                    {
                        stream.Write(Data, 0, Data.Length);
                        stream.Close();
                    }
                }

                Hrequest.AllowAutoRedirect = true;
                Hresponse = (HttpWebResponse)Hrequest.GetResponse();

                tstream = Hresponse.GetResponseStream();
                reader = new StreamReader(tstream, CharEncode);

                StopWatch.Stop();
                if (ReturnStream)
                {
                    CustWR = new CustomWebResponse(tstream, null, Hresponse.StatusDescription, (int)Hresponse.StatusCode,
                        StopWatch.Elapsed.ToString(@"hh\:mm\:ss\:ff"), Hrequest.Method + " " + url, Hresponse.ContentType);
                }
                else
                {
                    CustWR = new CustomWebResponse(reader.ReadToEnd(), null, Hresponse.StatusDescription, (int)Hresponse.StatusCode,
                        StopWatch.Elapsed.ToString(@"hh\:mm\:ss\:ff"), Hrequest.Method + " " + url, Hresponse.ContentType);
                }

                CustWR.Method = Hrequest.Method;
                CustWR.UrlRequest = url;
                return CustWR;
            }
            catch (WebException webex)
            {
                return WebExceptHandle(webex);
            }
            catch (Exception ex)
            {
                CustWR = new CustomWebResponse(ex.Message, null, "Custom error this error thrown by other exception on the function", 1000);
                CustWR.Method = Hrequest.Method;
                CustWR.UrlRequest = url;
                return CustWR;
            }
            finally
            {
                if (tstream != null) tstream.Close();
                tstream = null;
                if (reader != null) reader.Close();
                reader = null;
                if (Hresponse != null) Hresponse.Close();
                Hresponse = null;
                Hrequest = null;
                CustWR = null;
                StopWatch = null;
            }
        }

    }
}

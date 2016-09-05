using System;
using System.IO;
using System.Net;
using System.Text;

namespace VLCControlWF
{
    public static class HttpSender
    {
        public static void Post(string uriString, Action<string, bool> callback)
        {
            try
            {
                var uri = new Uri(uriString);
                var req = WebRequest.Create(uri);

                //var credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("" + ":" + "graffin"));
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("" + ":" + Properties.Settings.Default.Password));
                //var outputData = new StringBuilder();
                req.Credentials= CredentialCache.DefaultNetworkCredentials;
                req.Headers.Add("Authorization", "Basic " + credentials);

                string content;

                using (var res = req.GetResponse())
                {
                    using (var dataStream = res.GetResponseStream())
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }

                //callback(content, false);

            }
            catch (WebException ex)
            {
                callback(ex.ToString(), true);

            }
        }
    }
}

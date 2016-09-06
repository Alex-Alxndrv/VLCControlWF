using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VLCControlWF
{
    public static class HttpSender
    {
        public static void Post(string uriString)
        {
            try
            {
                var uri = new Uri(uriString);
                var req = WebRequest.Create(uri);

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("" + ":" + Properties.Settings.Default.Password));
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
            }
            catch (WebException ex)
            {
                MessageBox.Show("There is no VLC http server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

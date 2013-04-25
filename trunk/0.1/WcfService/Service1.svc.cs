using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    public class Service1 : IService1
    {
        private static Uri uri;

        public void Subscribe(string subscriberUri)
        {
            uri = new Uri(subscriberUri);
        }

        public void SendToast(string title, string message)
        {
            string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                        "<wp:Text1>" + title + "</wp:Text1>" +
                        "<wp:Text2>" + message + "</wp:Text2>" +
                    "</wp:Toast>" +
                "</wp:Notification>";

            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(toastMessage);
            SendMessage(uri, messageBytes);
        }

        private static void SendMessage(Uri uri, byte[] message)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "text/xml";
            request.ContentLength = message.Length;

            request.Headers.Add("X-MessageID", Guid.NewGuid().ToString());

            request.Headers["X-WindowsPhone-Target"] = "toast";
            request.Headers.Add("X-NotificationClass", "2");

            var requestStream = request.GetRequestStream();
            requestStream.Write(message, 0, message.Length);
        }
    }
}

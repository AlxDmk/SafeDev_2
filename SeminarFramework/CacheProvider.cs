using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SeminarFramework
{
   public class CacheProvider
    {
        static byte[] _optionalEntropy  = {1, 3, 4,5};
        public void CacheConnections(List<ConnectingString> connectings)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectingString>));
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlWriter, connectings);

                byte[] protectedData = Protect(memoryStream.ToArray());

                File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected", protectedData);
                Console.WriteLine("Data protected! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialize data error");
            }
        }

        public List<ConnectingString> GetConnectingsStringsFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectingString>));
                byte[] data = Unprotect(File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected"));
                return (List<ConnectingString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deselialize data error!");
                return null;
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, _optionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Protecting Error!");
                return null;
            }
            
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, _optionalEntropy, DataProtectionScope.LocalMachine);

            }
            catch(CryptographicException ex)
            {
                Console.WriteLine("Unprotecting Error");
                return null;
            }
            
        }

    }
}

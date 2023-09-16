using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace profiles2
{
    public class XmlFileService : IFileService // реализация интерфейса работы с файлами
    {
        public List<Profile> Open(string filename) // загрузка данных из файла
        {
            List<Profile> profiles = new List<Profile>(); 
            XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>)); 
            using (Stream reader = new FileStream(filename, FileMode.Open)) 
            {
                profiles = (List<Profile>)serializer.Deserialize(reader); 
            }

            return profiles; 
        }

        public void Save(string filename, List<Profile> profilesList) // сохранение данных в файл
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>)); 
            using (FileStream filestream = new FileStream(filename, FileMode.OpenOrCreate)) 
            {
                serializer.Serialize(filestream, profilesList); 
            }
        }

    }
    public interface IFileService // интерфейс работы с файлами
    {
        List<Profile> Open(string filename);
        void Save(string filename, List<Profile> ProfilesList);
    }

}

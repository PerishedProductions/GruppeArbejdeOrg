using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GruppeArbejdeOrg
{
    public class Project
    {

        public string Name { get; set; } = "Untitled Project";
        public string Path { get; set; }

        public Project()
        {

        }

        public Project(string path)
        {
            this.Path = path;
            LoadFromFile();
        }

        void LoadFromFile()
        {
            if (Path != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Path);

                Name = doc.SelectSingleNode("Project").Attributes["name"].Value;
                var test = doc.SelectSingleNode("Project");
            }
        }

        public void SaveToFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement mainNode = doc.CreateElement("Project");
            mainNode.SetAttribute("name", Name);

            doc.AppendChild(mainNode);

            doc.Save(Path);
        }

    }
}

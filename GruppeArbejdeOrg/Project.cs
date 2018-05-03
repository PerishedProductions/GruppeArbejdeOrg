using System;
using System.Collections.Generic;
using System.Xml;

namespace GruppeArbejdeOrg
{
    public class Project
    {

        public string Name { get; set; } = "Untitled Project";
        public string ProblemDefinitionPath { get; set; }
        public string TimeSchedulePath { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }

        public List<string> Files { get; set; } = new List<string>();

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

                string problemdef = doc.SelectSingleNode("Project").Attributes["problemdef"].Value;

                if (problemdef != "null")
                {
                    ProblemDefinitionPath = problemdef;
                }

                string timesched = doc.SelectSingleNode("Project").Attributes["timesched"].Value;

                if (timesched != "null")
                {
                    TimeSchedulePath = timesched;
                }

                XmlNodeList files =  doc.SelectSingleNode("Project").SelectSingleNode("Files").ChildNodes;

                for (int i = 0; i < files.Count; i++)
                {
                    Files.Add(files[i].Attributes["path"].Value);
                }

                Notes = doc.SelectSingleNode("Project").SelectSingleNode("Notes").InnerText;

            }
        }

        public void SaveToFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement mainNode = doc.CreateElement("Project");
            mainNode.SetAttribute("name", Name);

            if (ProblemDefinitionPath != null)
            {
                mainNode.SetAttribute("problemdef", ProblemDefinitionPath);
            }
            else
            {
                mainNode.SetAttribute("problemdef", "null");
            }

            if (TimeSchedulePath != null)
            {
                mainNode.SetAttribute("timesched", ProblemDefinitionPath);
            }
            else
            {
                mainNode.SetAttribute("timesched", "null");
            }

            XmlElement noteNode = doc.CreateElement("Notes");
            noteNode.InnerText = Notes;

            XmlElement filesNode = doc.CreateElement("Files");
            XmlElement fileNode = doc.CreateElement("File");

            fileNode.SetAttribute("path", $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GruppeOmaticProjects\\document.docx");

            filesNode.AppendChild(fileNode);
            
            mainNode.AppendChild(noteNode);
            mainNode.AppendChild(filesNode);
            doc.AppendChild(mainNode);

            doc.Save(Path);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using GruppeArbejdeOrg.Windows;
using System.ComponentModel;
using Microsoft.Office.Interop.Word;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace GruppeArbejdeOrg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        //Variabler
        Project currentProject;
        ProjectSettings projectSettings;

        //Constructor
        public MainWindow()
        {
            InitializeComponent();
            UpdateTitle();
        }

        //Ovveride function af OnClosing, som bliver kaldt når vinduet lukker
        protected override void OnClosing(CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        #region Menu Items

        //Opretter et ny projekt
        private void NewProject(object sender, RoutedEventArgs e)
        {
            currentProject = new Project();
            UpdateTitle();
        }

        //Åbner en eksisterende projekt (.dild fil)
        private void OpenProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\GruppeOmaticProjects";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Project newProject = new Project(dialog.FileName);
                currentProject = newProject;
            }

            Notes.Text = currentProject.Notes;
            UpdateTitle();
        }

        //Hvis projektet allerede er gemt et sted gemmer den det samme sted, ellers ber den brugeren om at gemme den et sted
        private void SaveProject(object sender, RoutedEventArgs e)
        {

            currentProject.Notes = Notes.Text;

            if (currentProject != null)
            {
                if (currentProject.Path != null)
                {
                    currentProject.SaveToFile();
                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.AddExtension = true;
                    dialog.DefaultExt = ".dild";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        currentProject.Path = dialog.FileName;
                        currentProject.SaveToFile();
                    }
                }

                Notes.Text = currentProject.Notes;
                UpdateTitle();
            }
            else
            {
                System.Windows.MessageBox.Show("You need to make a new project, or load an existing one to save!");
            }
            
        }

        //Åbner project indstillings vinduet
        private void OpenProjectSettings(object sender, RoutedEventArgs e)
        {
            if (currentProject != null)
            {
                if (projectSettings == null)
                {
                    projectSettings = new ProjectSettings(currentProject);
                    projectSettings.Show();
                }
                else if (projectSettings.IsVisible == false)
                {
                    projectSettings.Show();
                }

                if (projectSettings != null)
                {
                    projectSettings.ReloadCurrentProject(currentProject);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("You need to make a new project, or load an existing one to edit the project settings!");
            }
            

        }

        #endregion

        #region Window Buttons

        //Bliver kaldt når brugeren åbner et dokument
        private void DocumentClick(object sender, RoutedEventArgs e)
        {

            if (currentProject != null)
            {
                OpenWordDocument(currentProject.Files[0]);
            }
        }

        //Bliver kald når brugeren trykke på knappen "Open Problem Definition", og den åbner så ens problemstilling, eller siger at du skal linke til den hvis du ikke har gjort det
        private void OpenProblemDefinitionFile(object sender, RoutedEventArgs e)
        {
            if (currentProject != null && currentProject.ProblemDefinitionPath != null)
            {
                OpenWordDocument(currentProject.ProblemDefinitionPath);
            }
            else
            {
                System.Windows.MessageBox.Show("You have not yet added A path to your Problem Definition. To add it go to Edit->Project Settings");
            }
        }

        //Gør det samme som ovenstående funktion, men bare med tids foredelingen istedet
        private void OpenTimeScheduleFile(object sender, RoutedEventArgs e)
        {
            if (currentProject != null && currentProject.TimeSchedulePath != null)
            {
                OpenWordDocument(currentProject.TimeSchedulePath);
            }
            else
            {
                System.Windows.MessageBox.Show("You have not yet added A path to your Time Schedule. To add it go to Edit->Project Settings");
            }
        }

        //Laver en ny reminder baseret på bruger input
        private void AddNewReminder(object sender, RoutedEventArgs e)
        {

            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Write your reminder message", "Add Reminder", "Reminder!!!");

            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode("Project Omatic 4000 Reminder!"));
            stringElements[1].AppendChild(toastXml.CreateTextNode(userInput));

            var test = toastXml.GetElementsByTagName("command");

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Toast Test").Show(toast);
        }

        #endregion

        //Opdatere titlen af vinduet baseret på navnet af det åbnede projekt
        private void UpdateTitle()
        {
            if (currentProject != null)
            {
                Title = $"Gruppe Omatic 4000 - { currentProject.Name }";
            }
        }

        //Åbner et word dokument baseret på en filepath
        private void OpenWordDocument(string path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = true;
            word.Documents.Open(path);
        }

       
    }
}

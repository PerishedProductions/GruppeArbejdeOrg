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

        Project currentProject;

        ProjectSettings projectSettings;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTitle();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

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

        #region Menu

        private void NewProject(object sender, RoutedEventArgs e)
        {
            currentProject = new Project();
            UpdateTitle();
        }

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



        private void UpdateTitle()
        {
            if (currentProject != null)
            {
                Title = $"Gruppe Omatic 4000 - { currentProject.Name }";
            }
        }

        private void OpenWordDocument(string path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = true;
            word.Documents.Open(path);
        }

        private void DocumentClick(object sender, RoutedEventArgs e)
        {

            if (currentProject != null)
            {
                OpenWordDocument(currentProject.Files[0]);
            }
        }

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
    }
}

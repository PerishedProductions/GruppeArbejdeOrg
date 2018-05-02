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

namespace GruppeArbejdeOrg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("help");
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

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Project newProject = new Project(dialog.FileName);
                currentProject = newProject;
            }

            UpdateTitle();
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
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
                UpdateTitle();
            }
            else
            {
                System.Windows.MessageBox.Show("You need to make a new project, or load an existing one to save!");
            }
            
        }

        private void OpenProjectSettings(object sender, RoutedEventArgs e)
        {
            if (projectSettings == null)
            {
                projectSettings = new ProjectSettings(currentProject);
                projectSettings.Show();
            }
            else if(projectSettings.IsVisible == false)
            {
                projectSettings.Show();
            }

            if (projectSettings != null)
            {
                projectSettings.ReloadCurrentProject(currentProject);
            }

        }

        #endregion



        void UpdateTitle()
        {
            if (currentProject != null)
            {
                Title = $"Gruppe Omatic 4000 - { currentProject.Name }";
            }
        }

        
    }
}

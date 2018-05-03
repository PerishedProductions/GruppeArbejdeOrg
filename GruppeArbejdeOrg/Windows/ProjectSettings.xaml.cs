using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GruppeArbejdeOrg.Windows
{
    /// <summary>
    /// Interaction logic for ProjectSettings.xaml
    /// </summary>
    public partial class ProjectSettings : Window
    {

        public Project CurrentProject { get; set; }

        public ProjectSettings(Project project)
        {
            InitializeComponent();
            ReloadCurrentProject(project);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void ApplySettings(object sender, RoutedEventArgs e)
        {
            CurrentProject.Name = ProjectName.Text;
            CurrentProject.ProblemDefinitionPath = ProjectDef.Text;
            CurrentProject.TimeSchedulePath = TimeSched.Text;
            this.Hide();
        }

        public void ReloadCurrentProject(Project project)
        {
            this.CurrentProject = project;
            ProjectName.Text = CurrentProject.Name;

            if (CurrentProject.ProblemDefinitionPath != null)
            {
                ProjectDef.Text = CurrentProject.ProblemDefinitionPath;
            }

            if (CurrentProject.TimeSchedulePath != null)
            {
                TimeSched.Text = CurrentProject.TimeSchedulePath;
            }
        }

        private void OpenProjectDefinition(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectDef.Text = dialog.FileName;
            }
        }

        private void OpenTimeSchedule(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TimeSched.Text = dialog.FileName;
            }
        }
    }
}

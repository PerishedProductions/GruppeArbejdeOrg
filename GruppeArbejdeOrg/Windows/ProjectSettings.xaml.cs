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

            this.Hide();
        }

        public void ReloadCurrentProject(Project project)
        {
            this.CurrentProject = project;
            ProjectName.Text = CurrentProject.Name;
        }

    }
}

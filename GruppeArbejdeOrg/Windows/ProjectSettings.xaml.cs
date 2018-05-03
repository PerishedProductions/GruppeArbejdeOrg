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

        //Properties og Variabler
        public Project CurrentProject { get; set; }

        //Cunstroctor
        public ProjectSettings(Project project)
        {
            InitializeComponent();
            ReloadCurrentProject(project);
        }

        //Ovveride function af OnClosing, som bliver kaldt når vinduet lukker
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        //Gemmer endringerne til Projekt objektet (CurrentObject)
        private void ApplySettings(object sender, RoutedEventArgs e)
        {
            CurrentProject.Name = ProjectName.Text;
            CurrentProject.ProblemDefinitionPath = ProjectDef.Text;
            CurrentProject.TimeSchedulePath = TimeSched.Text;
            this.Hide();
        }

        //Henter data fra Projekt objektet (CurrentObject)
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

        //Åbner et vindu hvor du kan vælge din problemformulering
        private void OpenProjectDefinition(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectDef.Text = dialog.FileName;
            }
        }

        //Åbner et vindu hvor du kan vælge din tidsplan
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

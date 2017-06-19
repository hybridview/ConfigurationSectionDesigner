using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace Dsl.Wizard
{
    public class ConfigurationSectionDesignerWizard : IWizard
    {
        #region IWizard implementation

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            var dte = automationObject as DTE;
            var project = GetCurrentProject(dte);

            // Setup assemblyname as project namespace
            string assemblyName = replacementsDictionary["$rootnamespace$"];
            if (project != null)
            {
                assemblyName = project.Properties.Item("AssemblyName").Value.ToString();
            }

            replacementsDictionary.Add("$assemblyname$", assemblyName);
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        } 

        #endregion

        /// <summary>
        /// Returns the current active project.
        /// </summary>
        /// <param name="dte">The dte objects.</param>
        /// <returns>The current project</returns>
        private static Project GetCurrentProject(DTE dte)
        {
            Project activeProject = null;

            Array activeSolutionProjects = dte.ActiveSolutionProjects as Array;
            if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
            {
                activeProject = activeSolutionProjects.GetValue(0) as Project;
            }

            return activeProject;
        }
    }
}
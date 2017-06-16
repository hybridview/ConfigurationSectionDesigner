using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Xml;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;
using System.Text.RegularExpressions;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using EnvDTE100;

/*
 * Based on the code found here:
 * http://www.codeproject.com/KB/cs/VsMultipleFileGenerator.aspx
 */

namespace ConfigurationSectionDesigner
{
    internal static class VsHelper
    {
        static VSDOCUMENTPRIORITY[] StandardDocumentPriority = new VSDOCUMENTPRIORITY[(int)VSDOCUMENTPRIORITY.DP_Standard];

        /// <summary>
        /// Get the current Hierarchy
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IVsHierarchy GetCurrentHierarchy(IServiceProvider provider)
        {
            DTE vs = (DTE)provider.GetService(typeof(DTE));

            if (vs == null) throw new InvalidOperationException("DTE not found.");

            return ToHierarchy(vs.SelectedItems.Item(1).ProjectItem.ContainingProject);
        }

        /// <summary>
        /// Get the hierarchy corresponding to a Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static IVsHierarchy _oldToHierarchy(EnvDTE.Project project)
        {
            if (project == null) throw new ArgumentNullException("project");
            string projectGuid = null;
            // DTE does not expose the project GUID that exists at in the msbuild project file.        
            // Cannot use MSBuild object model because it uses a static instance of the Engine,         
            // and using the Project will cause it to be unloaded from the engine when the         
            // GC collects the variable that we declare.  
            using (XmlReader projectReader = XmlReader.Create(project.FileName))
            {
                projectReader.MoveToContent();
                if (projectReader.NameTable != null)
                {
                    object nodeName = projectReader.NameTable.Add("ProjectGuid");
                    while (projectReader.Read())
                    {
                        if (Object.Equals(projectReader.LocalName, nodeName))
                        {
                            projectGuid = (String)projectReader.ReadElementContentAsString();
                            break;
                        }
                    }
                }
            }
            Debug.Assert(!String.IsNullOrEmpty(projectGuid));

            // ABM 20140904: With a null projectGuid, we should NEVER try call GetHierarchy.
            // TODO: Determine if this error should be fatal, or soft. Handle accordingly. At least log it.
            if (string.IsNullOrEmpty(projectGuid))
            {
                // TODO: Quickly added to indicate a problem in code flow. Better exception/error handling should be implemented.
                throw new Exception("Could not locate a ProjectGuid in this project's source file.");
            }

            IServiceProvider serviceProvider = new ServiceProvider(project.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
            return VsShellUtilities.GetHierarchy(serviceProvider, new Guid(projectGuid));

        }

        /// <summary>
        /// Get the hierarchy corresponding to a Project.
        /// </summary>
        /// <remarks>
        /// The original ToHierarchy method has problems when project hasn't yet been written to file. 
        /// This function addresses that.
        /// 
        /// http://www.codeproject.com/Articles/16515/Creating-a-Custom-Tool-to-Generate-Multiple-Files?msg=3962376#xx3962376xx
        /// </remarks>
        /// <param name="project"></param>
        /// <returns></returns>
        private static IVsHierarchy ToHierarchy(EnvDTE.Project project)
        {
            if (project == null) throw new ArgumentNullException("project");

            string uniqueName = project.UniqueName;
            IVsSolution solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));

            IVsHierarchy hierarchy;

            solution.GetProjectOfUniqueName(uniqueName, out hierarchy);

            return hierarchy;
        }

        /// <summary>
        /// Converts an EnvDTE.Project to a Visual Studio project hierarchy.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static IVsProject3 ToVsProject(EnvDTE.Project project)
        {
            if (project == null) throw new ArgumentNullException("project");
            IVsProject3 vsProject = ToHierarchy(project) as IVsProject3;
            if (vsProject == null)
            {
                throw new ArgumentException("Project is not a recognized VS project.");
            }
            return vsProject;
        }

        /// <summary>
        /// Converts a Visual Studio project hierarchy to an EnvDTE.Project.
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        public static EnvDTE.Project ToDteProject(IVsHierarchy hierarchy)
        {
            if (hierarchy == null) throw new ArgumentNullException("hierarchy");
            object prjObject = null;
            if (hierarchy.GetProperty(0xfffffffe, -2027, out prjObject) >= 0)
            {
                return (EnvDTE.Project)prjObject;
            }
            else
            {
                throw new ArgumentException("Hierarchy is not a project.");
            }
        }

        /// <summary>
        /// Converts a Visual Studio project hierarchy to an EnvDTE.Project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static EnvDTE.Project ToDteProject(IVsProject project)
        {
            if (project == null) throw new ArgumentNullException("project");
            return ToDteProject(project as IVsHierarchy);
        }





        #region NEW

        public static bool FindProjectContainingFile(string projectItemFilePath, out ProjectItem projectItem, out Project parentProject)
        {
            projectItem = null;
            parentProject = null;

            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            //foreach (var project in dte.Solution.FindProjectItem())
            projectItem = dte.Solution.FindProjectItem(projectItemFilePath);
            if (projectItem != null)
            {
                parentProject = projectItem.ContainingProject;
                if (parentProject != null)
                {
                    return true;
                }
            }

            return false;
        }


        #endregion




        #region OLD
        /// <summary>
        /// Locates the corresponding EnvDTE.ProjectItem associated with <paramref name="file"/>.
        /// </summary>
        /// <param name="project">the project to search through.</param>
        /// <param name="file">the path of the file we are locating.</param>
        /// <returns></returns>
        public static EnvDTE.ProjectItem FindProjectItem(EnvDTE.Project project, string file)
        {
            return FindProjectItem(project.ProjectItems, file);
        }

        /// <summary>
        /// Locates the corresponding EnvDTE.ProjectItem associated with <paramref name="file"/>.
        /// </summary>
        /// <param name="items">the items to search through.</param>
        /// <param name="file">the path of the file we are locating.</param>
        /// <returns></returns>
        public static EnvDTE.ProjectItem FindProjectItem(EnvDTE.ProjectItems items, string file)
        {
            string atom = file.Substring(0, file.IndexOf("\\") + 1);
            foreach (EnvDTE.ProjectItem item in items)
            {
                //if ( item
                //if (item.ProjectItems.Count > 0)
                if (atom.StartsWith(item.Name))
                {
                    // then step in
                    EnvDTE.ProjectItem ritem = FindProjectItem(item.ProjectItems, file.Substring(file.IndexOf("\\") + 1));
                    if (ritem != null)
                        return ritem;
                }
                if (Regex.IsMatch(item.Name, file))
                {
                    return item;
                }
                if (item.ProjectItems.Count > 0)
                {
                    EnvDTE.ProjectItem ritem = FindProjectItem(item.ProjectItems, file.Substring(file.IndexOf("\\") + 1));
                    if (ritem != null)
                        return ritem;
                }
            }
            return null;
        }

        /// <summary>
        /// Locates the corresponding EnvDTE.ProjectItem's whos name matches the regex <paramref name="match"/>.
        /// </summary>
        /// <param name="items">the items to search through.</param>
        /// <param name="match">the regex match string.</param>
        /// <returns></returns>
        public static List<EnvDTE.ProjectItem> FindProjectItems(EnvDTE.ProjectItems items, string match)
        {
            List<EnvDTE.ProjectItem> values = new List<EnvDTE.ProjectItem>();

            foreach (EnvDTE.ProjectItem item in items)
            {
                if (Regex.IsMatch(item.Name, match))
                {
                    values.Add(item);
                }
                if (item.ProjectItems.Count > 0)
                {
                    values.AddRange(FindProjectItems(item.ProjectItems, match));
                }
            }
            return values;
        }


        // TODO: Figure out what we are trying to achieve here. Do we need these complicated methods? Can we use standard VSHELPER functions to get access to a simpler object and search that way?

        /// <summary>
        /// Looks through the <paramref name="project"/> and all subitems / subprojects to find the project hosting the active configuration section designer window.
        /// </summary>
        /// <remarks>
        /// Checks if <paramref name="csdDocumentFilePath"/> is inside <paramref name="project"/>. If true, the id of 
        /// <paramref name="csdDocumentFilePath"/> is located and used to return the CSD file as a ProjectItem object 
        /// <paramref name="configurationSectionModelFile"/>.
        /// </remarks>
        /// <param name="project">the parent project.</param>
        /// <param name="configurationSectionModelFile">(out) the project item represented by csdDocumentFilePath, if found (ex: MyProject\ConfigurationSection.csd).</param>
        /// <param name="csdDocumentFilePath">the file path of the CSD project item.</param>
        /// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
        public static bool TryFindProjectItemWithFilePath(Project project, string csdDocumentFilePath, out ProjectItem configurationSectionModelFile)
        {
            return TryFindProjectItemWithFilePath(project, StandardDocumentPriority, csdDocumentFilePath, out  configurationSectionModelFile);
        }

        /// <summary>
        /// Looks through the <paramref name="project"/> and all subitems / subprojects to find the project hosting the active configuration section designer window.
        /// </summary>
        /// <remarks>
        /// Checks if <paramref name="csdDocumentFilePath"/> is inside <paramref name="project"/>. If true, the id of 
        /// <paramref name="csdDocumentFilePath"/> is located and used to return the CSD file as a ProjectItem object 
        /// <paramref name="configurationSectionModelFile"/>.
        /// </remarks>
        /// <param name="project">the parent project.</param>
        /// <param name="docPriority">Specifies the priority level of a document within a project.</param>
        /// <param name="configurationSectionModelFile">(out) the project item represented by csdDocumentFilePath, if found (ex: MyProject\ConfigurationSection.csd).</param>
        /// <param name="csdDocumentFilePath">the file path of the CSD project item.</param>
        /// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
        public static bool TryFindProjectItemWithFilePath(Project project, VSDOCUMENTPRIORITY[] docPriority, string csdDocumentFilePath, out ProjectItem configurationSectionModelFile)
        {
            Diagnostics.DebugWrite("VsHelper.TryFindProjectItemWithFilePath (VH.TFP).");
            bool projectFound = false;
            configurationSectionModelFile = null;

            // Nothing useful for this project. Process the next one.
            if (string.IsNullOrEmpty(project.FileName) || !File.Exists(project.FileName))
            {
                Diagnostics.DebugWrite("VH.TFP >> Nothing useful found in this current dte.Solution.Projects item. Continuing loop...");
                return false;
            }

            // obtain a reference to the current project as an IVsProject type
            IVsProject vsProject = VsHelper.ToVsProject(project); // ABM - Issue 10435: 2nd WindowsService project results in FAILURE here.
            Diagnostics.DebugWrite("VH.TFP >> IVsProject vsProject = VsHelper.ToVsProject( project='{0}' )", project.Name);

            int iFound = 0;
            uint itemId = 0;

            // this locates, and returns a handle to our source file, as a ProjectItem
            int result = vsProject.IsDocumentInProject(csdDocumentFilePath, out iFound, docPriority, out itemId); // ABM - Issue 10435: WindowsService project results in iFound=0!!!

            // itemid: Pointer to the item identifier of the document within the project. 
            // NOTE: itemId is either a specific id pointing to the file, or is one of VSITEMID enumeration.
            // VSITEMID represents Special items inside a VsHierarchy:
            /*
        public enum VSITEMID
        {
            Selection = 4294967293, // all the currently selected items. // represents the currently selected item or items, which can include the root of the hierarchy.
            Root = 4294967294,      // the hierarchy itself. // represents the root of a project hierarchy and is used to identify the entire hierarchy, as opposed to a single item.
            Nil = 4294967295,       // no node.// represents the absence of a project item. This value is used when there is no current selection.
        }
             * http://visual-studio.todaysummary.com/q_visual-studio_76086.html
             * 
             * http://msdn.microsoft.com/en-us/subscriptions/downloads/microsoft.visualstudio.shell.interop.ivshierarchy(v=vs.100).aspx
             */


            if (result != VSConstants.S_OK)
                throw new Exception("Unexpected error calling IVsProject.IsDocumentInProject.");
            
            Diagnostics.DebugWrite("VH.TFP >> vsProject.IsDocumentInProject(inputFilePath, out iFound={0}, pdwPriority, out itemId={1}).", iFound, itemId);

            // TODO: [abm] Below here, project build failed! Error was "type is not of VsProject". This only occured on a brand new 
            // project with brand new csd. After failed rebuild attempt of project, it all worked.
            // Find out why and fix (or create warning message) to guide future users! Not yet reproducible...

            // if this source file is found in this project
            if (iFound != 0 && itemId != 0)
            {
                Diagnostics.DebugWrite("VH.TFP >> (iFound != 0 && itemId != 0) == TRUE!!!");
                Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleSp = null;
                vsProject.GetItemContext(itemId, out oleSp);
                if (oleSp != null)
                {
                    Diagnostics.DebugWrite("VH.TFP >> vsProject.GetItemContext( itemId, out oleSp ) >> oleSp != null! Getting ServiceProvider sp...");
                    ServiceProvider sp = new ServiceProvider(oleSp);
                    // convert our handle to a ProjectItem
                    configurationSectionModelFile = sp.GetService(typeof(ProjectItem)) as ProjectItem;

                    if (configurationSectionModelFile != null)
                    {
                        Diagnostics.DebugWrite("VH.TFP >>  configurationSectionModelFile = sp.GetService( typeof( ProjectItem ) ) as ProjectItem is NOT null! Setting this._project to the project we were working on...");
                        // We now have what we need. Stop looking.
                        projectFound = true;
                    }
                }
            }
            return projectFound;
        }

        /// <summary>
        /// Recursively traverses the Solution hierarchy looking for the CSD document designated by <paramref name="filePath"/>, and 
        /// the project it is found in.
        /// </summary>
        /// <remarks>
        /// The recursion in this method has gotten ugly and complex, but it appears to work. It may be nice to find a more elegant solution 
        /// to the problem of locating these items in the future. [a.moore]
        /// </remarks>
        /// <param name="csdDocumentFilePath">The path of the CSD file to locate.</param>
        /// <param name="parentProject">(out) the parent project of the file, if found.</param>
        /// <param name="fileProjectItem">(out) the project item representation of the CSD file, if found.</param>
        /// <returns><c>true</c> if found, otherwise <c>false</c></returns>
        public static bool TryFindProjectItemAndParentProject(string csdDocumentFilePath, out Project parentProject, out ProjectItem fileProjectItem)
        {
            Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject (Sol Level) >> csdDocumentFilePath = '{0}'.", csdDocumentFilePath);
            parentProject = null;
            fileProjectItem = null;
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            foreach (Project project in dte.Solution.Projects)
            {
                Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> Current project = '{0}'.", project.Name ?? "<null>");
                if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                {
                    Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> project.ProjectItems != null && Number of project items ({0}) > 0", project.ProjectItems.Count);
                    // First, look for the project at the solution level (No solution folders, subprojects, etc).
                    if (TryFindProjectItemWithFilePath(project, csdDocumentFilePath, out fileProjectItem))
                    {
                        // The project was found at the solution level.
                        parentProject = project;
                        Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> PROJECT FOUND! Name='{0}'", project.Name);
                        return true;
                    }
                    else
                    {
                        // Project NOT found at the solution level. Start drilling down into sub project items...
                        Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> TryFindProjectItemWithFilePath(project, filePath, out fileProjectItem) == FALSE");
                        if (_TryFindProjectItemAndParentProject(csdDocumentFilePath, project.ProjectItems, out parentProject, out fileProjectItem))
                        {
                            // The project was found after drilling down.
                            Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> PROJECT FOUND! Name='{0}'", project.Name);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Recursively traverses the Solution hierarchy, starting at <paramref name="currentProjectItems"/>, looking for 
        /// the project item designated by <paramref name="filePath"/>, and it's parent project.
        /// </summary>
        /// <remarks>
        /// The recursion in this method has gotten ugly and complex, but it appears to work. It may be nice to find a more elegant solution 
        /// to the problem of locating these items in the future. [a.moore]
        /// </remarks>
        /// <param name="filePath">The path of the CSD file to locate.</param>
        /// <param name="currentProjectItems">Project item hiearchy we are traversing to find possible SubProjects (IE. in solution folder type)</param>
        /// <param name="parentProject">(out) the parent project of the file, if found.</param>
        /// <param name="fileProjectItem">(out) the project item representation of the CSD file, if found.</param>
        /// <returns><c>true</c> if found, otherwise <c>false</c></returns>
        private static bool _TryFindProjectItemAndParentProject(string filePath, ProjectItems currentProjectItems, out Project parentProject, out ProjectItem fileProjectItem)
        {
            // TODO: Would it be safe to only check subitems if current item is a solution folder? We are checking alot of items that would never have sub projects.
            Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject (ProjectItems Level) >> currentProjectItems.Count = '{0}'.", currentProjectItems.Count);
            parentProject = null;
            fileProjectItem = null;

            // Already checked by calling method, but leaving null check for future use...
            if (currentProjectItems == null)
            {
                throw new ArgumentException("currentProjectItems cannot be null.");
            }
            
            foreach (ProjectItem projectItem in currentProjectItems)
            {
                Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> Current projectItem = '{0}', projectItem.ProjectItems.Count={1},", projectItem.Name, (projectItem.ProjectItems != null ? projectItem.ProjectItems.Count.ToString() : "0"));
                if (projectItem.SubProject != null) // Check SubProject.
                {
                    Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> projectItem.SubProject EXISTS.");

                    // If item was matched to file or recursive call returned true, exit.
                    if ((!string.IsNullOrEmpty(projectItem.SubProject.FullName)
                        && TryFindProjectItemWithFilePath(projectItem.SubProject, filePath, out fileProjectItem)))
                    {
                        Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> PROJECT FOUND in SubProject!");
                        parentProject = projectItem.SubProject;
                        return true;
                    }

                    // Look through the SubProject's project items.
                    if (projectItem.SubProject.ProjectItems != null
                        && _TryFindProjectItemAndParentProject(filePath, projectItem.SubProject.ProjectItems, out parentProject, out fileProjectItem))
                    {
                        Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> PROJECT FOUND in SubProject.ProjectItems!");
                        return true;
                    }
                }
                else if (
                    projectItem.ProjectItems != null
                    && projectItem.ProjectItems.Count > 0
                    && _TryFindProjectItemAndParentProject(filePath, projectItem.ProjectItems, out parentProject, out fileProjectItem)
                    ) // Check project items.
                {
                    Diagnostics.DebugWrite("VsHelper.TryFindProjectItemAndParentProject >> PROJECT FOUND in projectItem!");
                    return true;
                }
            }
            
            return false;
        }
        #endregion

        /// <summary>
        /// Gets a list of all project references.
        /// </summary>
        /// <remarks>
        /// REF=http://www.codeproject.com/KB/macros/EnvDTE.aspx
        /// </remarks>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetReferences(Project project)
        {
            if (project.Object is VSLangProj.VSProject)
            {
                VSLangProj.VSProject vsproject = (VSLangProj.VSProject)project.Object;
                List<KeyValuePair<string, string>> list =
                   new List<KeyValuePair<string, string>>();
                foreach (VSLangProj.Reference reference in vsproject.References)
                {
                    if (reference.StrongName)
                        //System.Configuration, Version=2.0.0.0,
                        //Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A
                        list.Add(new KeyValuePair<string, string>(reference.Identity,
                            reference.Identity +
                            ", Version=" + reference.Version +
                            ", Culture=" + (string.IsNullOrEmpty(reference.Culture) ? "neutral" : reference.Culture) +
                            ", PublicKeyToken=" + reference.PublicKeyToken));
                    else
                        list.Add(new KeyValuePair<string, string>(
                                     reference.Identity, reference.Path));
                }
                return list;
            }
            else if (project.Object is VsWebSite.VSWebSite)
            {
                VsWebSite.VSWebSite vswebsite = (VsWebSite.VSWebSite)project.Object;
                //List<string> list = new List<string>();
                List<KeyValuePair<string, string>> list =
                   new List<KeyValuePair<string, string>>();
                foreach (VsWebSite.AssemblyReference reference in vswebsite.References)
                {
                    string value = "";
                    if (reference.FullPath != "")
                    {
                        FileInfo f = new FileInfo(reference.FullPath + ".refresh");
                        if (f.Exists)
                        {
                            using (FileStream stream = f.OpenRead())
                            {
                                using (StreamReader r = new StreamReader(stream))
                                {
                                    value = r.ReadToEnd().Trim();
                                }
                            }
                        }
                    }
                    if (value == "")
                    {
                        list.Add(new KeyValuePair<string, string>(reference.Name,
                                 reference.StrongName));
                    }
                    else
                    {
                        list.Add(new KeyValuePair<string, string>(reference.Name, value));
                    }
                }
                return list;
            }
            else
            {
                throw new Exception("Currently, system is only set up to " +
                                    "do references for normal projects.");
            }
        }

        /// <summary>
        /// Adds a reference to the selected project.
        /// </summary>
        /// <remarks>
        /// REF=http://www.codeproject.com/KB/macros/EnvDTE.aspx
        /// </remarks>
        /// <param name="project"></param>
        /// <param name="referenceStrIdentity"></param>
        /// <param name="browseUrl"></param>
        public static void AddProjectReference(Project project, string referenceStrIdentity, string browseUrl)
        {
            //browseUrl is either the File Path or the Strong Name
            //(System.Configuration, Version=2.0.0.0, Culture=neutral,
            //                       PublicKeyToken=B03F5F7F11D50A3A)
            string path = "";

            if (!browseUrl.StartsWith(referenceStrIdentity))
            {
                //it is a path
                path = browseUrl;
            }

            if (project.Object is VSLangProj.VSProject)
            {
                VSLangProj.VSProject vsproject = (VSLangProj.VSProject)project.Object;
                VSLangProj.Reference reference = null;
                try
                {
                    reference = vsproject.References.Find(referenceStrIdentity);
                }
                catch (Exception)
                {
                    //it failed to find one, so it must not exist. 
                    //But it decided to error for the fun of it. :)
                }
                if (reference == null)
                {
                    if (path == "")
                        vsproject.References.Add(browseUrl);
                    else
                        vsproject.References.Add(path);
                }
                else
                {
                    throw new Exception("Reference already exists.");
                }
            }
            else if (project.Object is VsWebSite.VSWebSite)
            {
                VsWebSite.VSWebSite vswebsite = (VsWebSite.VSWebSite)project.Object;
                VsWebSite.AssemblyReference reference = null;
                try
                {
                    foreach (VsWebSite.AssemblyReference r in vswebsite.References)
                    {
                        if (r.Name == referenceStrIdentity)
                        {
                            reference = r;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    //it failed to find one, so it must not exist. 
                    //But it decided to error for the fun of it. :)
                }
                if (reference == null)
                {
                    if (path == "")
                        vswebsite.References.AddFromGAC(browseUrl);
                    else
                        vswebsite.References.AddFromFile(path);

                }
                else
                {
                    throw new Exception("Reference already exists.");
                }
            }
            else
            {
                throw new Exception("Currently, system is only set up " +
                          "to do references for normal projects.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not a given item is under source control.
        /// </summary>
        /// <param name="dte">The dte object.</param>
        /// <param name="item">The item to ispect.</param>
        /// <returns><c>true</c> if under source control, <c>false</c> otherwise</returns>
        public static bool IsItemUnderSourceControl(DTE dte, ProjectItem item)
        {
            if (dte == null)
                throw new ArgumentNullException("dte");

            for (short i = 0; i < item.FileCount; i++)
            {
                if (dte.SourceControl.IsItemUnderSCC(item.FileNames[i]))
                    return true;
            }

            if (item.ProjectItems != null)
            {
                return item.ProjectItems.Cast<ProjectItem>().Any(projectItem => IsItemUnderSourceControl(dte, projectItem));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Performa a checkout for a given item.
        /// </summary>
        /// <param name="dte">The dte object.</param>
        /// <param name="item">The item to checkout.</param>
        public static void CheckoutItem(DTE dte, ProjectItem item)
        {
            CheckoutItem(dte, item, true);
        }

        /// <summary>
        /// Performa a checkout for a given item.
        /// </summary>
        /// <param name="dte">The dte object.</param>
        /// <param name="item">The item to checkout.</param>
        /// <param name="recursive">If true, any subitems for this item will also be checked out.</param>
        public static void CheckoutItem(DTE dte, ProjectItem item, bool recursive)
        {
            if (dte == null)
                throw new ArgumentNullException("dte");

            for (short i = 0; i < item.FileCount; i++)
            {
                string itemPath = item.FileNames[i];
                if (!dte.SourceControl.IsItemCheckedOut(itemPath))
                {
                    dte.SourceControl.CheckOutItem(itemPath);
                }
            }

            if (recursive && item.ProjectItems != null)
            {
                foreach (ProjectItem childItem in item.ProjectItems.Cast<ProjectItem>())
                {
                    CheckoutItem(dte, childItem);
                }
            }
        }
    }

    // See http://blogs.msdn.com/b/mshneer/archive/2009/12/07/interop-type-xxx-cannot-be-embedded-use-the-applicable-interface-instead.aspx
    // for more info on why we have this.
    public abstract class EnvDTEConstants
    {
        public const string vsDocumentKindText = "{8E7B96A8-E33D-11D0-A6D5-00C04FB67F6A}";

        public const string vsWindowKindOutput = "{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}";

        public const string vsSolutionFolderType = "2150E333-8FDC-42a3-9474-1A3956D46DE8";
    }

    // TODO: Move to common area.
    public class Diagnostics
    {
        public const bool ForceReleaseModeDebugLogging = true;

        /*
        public static void DebugWriteLine(string format, params object[] args)
        {
            DebugWrite(format + "\r\n", args);
        }
        */
        public static void DebugWrite(string format, params object[] args)
        {
            if (ForceReleaseModeDebugLogging)
            {
                System.Diagnostics.Debugger.Log(0, "", string.Format(CultureInfo.InvariantCulture, format, args));
            }
            else
            {
                Debug.Write(string.Format(CultureInfo.InvariantCulture, format, args));
            }
        }
    }
}

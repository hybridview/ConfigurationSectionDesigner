using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;


/*
 * Based on the code found here:
 * http://www.codeproject.com/KB/cs/VsMultipleFileGenerator.aspx
 */

namespace ConfigurationSectionDesigner
{
    public interface IChangable
	{
		#region Properties

		bool IsChanged
		{
			get;
		}

		#endregion Properties
	}

    public abstract class VsMultipleFileGenerator2<T> : IEnumerable<T>, IVsSingleFileGenerator, IObjectWithSite
        where T : IChangable
    {
        #region Fields

        protected bool cancelGenerating = false;

        private string defaultNamespace;
        private string inputFileContents;
        private string inputFilePath;
        private List<string> newFileNames;
        private List<string> oldFileNames;
        private Project project;
        private ServiceProvider serviceProvider = null;
        private object site;

        #endregion Fields

        #region Constructors

        public VsMultipleFileGenerator2()
        {
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            Array ary = (Array)dte.ActiveSolutionProjects;

            if (ary.Length > 0)
            {
                project = (Project)ary.GetValue(0);
            }

            newFileNames = new List<string>();
            oldFileNames = new List<string>();
        }

        #endregion Constructors

        #region Properties

        protected string DefaultNamespace
        {
            get { return defaultNamespace; }
        }

        protected string InputFileContents
        {
            get { return inputFileContents; }
        }

        protected string InputFilePath
        {
            get { return inputFilePath; }
        }

        protected Project Project
        {
            get { return project; }
        }

        private ServiceProvider SiteServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleServiceProvider = site as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

                    serviceProvider = new ServiceProvider(oleServiceProvider);
                }

                return serviceProvider;
            }
        }

        #endregion Properties

        #region Methods
        

      

   
        public int Generate(string inputFilePath, string inputFileContents, string defaultNamespace,
           IntPtr[] rgbOutputFileContents, 
            out uint output, 
            IVsGeneratorProgress generateProgress)
        {
            try
            {
                
                
                if (cancelGenerating)
                {
                    byte[] fileData = File.ReadAllBytes(Path.ChangeExtension(inputFilePath, GetDefaultExtension()));

                    // Return our summary data, so that Visual Studio may write it to disk.
                    //outputFileContents = Marshal.AllocCoTaskMem(fileData.Length);
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(fileData.Length);

                    //Marshal.Copy(fileData, 0, outputFileContents, fileData.Length);
                    Marshal.Copy(fileData, 0, rgbOutputFileContents[0], fileData.Length);


                    //output = fileData.Length;
                    output = (uint)fileData.Length;

                    //return 0;
                    return VSConstants.E_ABORT; // ??????
                }

                this.inputFileContents = inputFileContents;
                this.inputFilePath = inputFilePath;
                this.defaultNamespace = defaultNamespace;

                int iFound = 0;
                uint itemId = 0;
                ProjectItem projectItem;

                Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[] pdwPriority = new Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[1];

                // Obtain a reference to the current project as an IVsProject type
                Microsoft.VisualStudio.Shell.Interop.IVsProject VsProject = VsHelper.ToVsProject(project);

                // This locates, and returns a handle to our source file, as a ProjectItem
                VsProject.IsDocumentInProject(InputFilePath, out iFound, pdwPriority, out itemId);

                // If our source file was found in the project (which it should have been)
                if (iFound != 0 && itemId != 0)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleSp = null;

                    VsProject.GetItemContext(itemId, out oleSp);

                    if (oleSp != null)
                    {
                        ServiceProvider sp = new ServiceProvider(oleSp);

                        // Convert our handle to a ProjectItem
                        projectItem = sp.GetService(typeof(ProjectItem)) as ProjectItem;
                    }
                    else
                    {
                        throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem");
                    }
                }
                else
                {
                    throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem");
                }

                oldFileNames.Clear();
                newFileNames.Clear();

                foreach (ProjectItem childItem in projectItem.ProjectItems)
                {
                    oldFileNames.Add(childItem.Name);

                    if (!childItem.Name.EndsWith(".cs"))
                    {
                        childItem.Properties.Item("BuildAction").Value = 0;
                    }
                }

                // Now we can start our work, iterate across all the 'items' in our source file
                foreach (T item in this)
                {
                    // Obtain a name for this target file
                    string fileName = GetFileName(item);
                    // Add it to the tracking cache
                    newFileNames.Add(fileName);
                    // Fully qualify the file on the filesystem
                    string filePath = Path.Combine(Path.GetDirectoryName(inputFilePath), fileName);

                    if (!(item as IChangable).IsChanged && File.Exists(filePath))
                    {
                        continue;
                    }

                    try
                    {
                        bool isNewAdded = !oldFileNames.Contains(fileName);

                        if (!isNewAdded)
                        {
                            // Check out this file.
                            if (project.DTE.SourceControl.IsItemUnderSCC(filePath) &&
                                !project.DTE.SourceControl.IsItemCheckedOut(filePath))
                            {
                                project.DTE.SourceControl.CheckOutItem(filePath);
                            }
                        }

                        // Create the file
                        FileStream fs = File.Create(filePath);

                        try
                        {
                            // Generate our target file content
                            byte[] data = GenerateContent(item);

                            // Write it out to the stream
                            fs.Write(data, 0, data.Length);

                            fs.Close();

                            OnFileGenerated(filePath, isNewAdded);

                            /*
                             * Here you may wish to perform some addition logic
                             * such as, setting a custom tool for the target file if it
                             * is intented to perform its own generation process.
                             * Or, set the target file as an 'Embedded Resource' so that
                             * it is embedded into the final Assembly.

                            EnvDTE.Property prop = itm.Properties.Item("CustomTool");

                            if (String.IsNullOrEmpty((string)prop.Value) || !String.Equals((string)prop.Value, typeof(AnotherCustomTool).Name))
                            {
                                prop.Value = typeof(AnotherCustomTool).Name;
                            }
                            */
                        }
                        catch (Exception ex) // An error generating the content and writing to file. Not fatal?
                        {
                            fs.Close();

                            OnError(ex);

                            LogException(ex);
                        }
                    }
                    catch (Exception ex) // An error during core SS and/or file system operations. This is thrown to parent.
                    {
                        throw ex;
                    }
                }

                // Perform some clean-up, making sure we delete any old (stale) target-files
                foreach (ProjectItem childItem in projectItem.ProjectItems)
                {
                    if (!(childItem.Name.EndsWith(GetDefaultExtension()) || newFileNames.Contains(childItem.Name)))
                    {
                        // Then delete it
                        childItem.Delete();
                    }
                }

                foreach (var newFileName in newFileNames)
                {
                    if (!oldFileNames.Contains(newFileName))
                    {
                        string fileName = Path.Combine(inputFilePath.Substring(0, inputFilePath.LastIndexOf(Path.DirectorySeparatorChar)), newFileName);

                        // Add the newly generated file to the solution, as a child of the source file...
                        ProjectItem itm = projectItem.ProjectItems.AddFromFile(fileName);

                        // Set buildaction to none
                        if (!newFileName.EndsWith(".cs"))
                        {
                            itm.Properties.Item("BuildAction").Value = 0;
                        }
                    }
                }

                // Generate our summary content for our 'single' file
                byte[] summaryData = GenerateSummaryContent();


                if (summaryData == null)
                {
                    //outputFileContents = IntPtr.Zero;
                    rgbOutputFileContents[0] = IntPtr.Zero;
                    output = 0;
                }
                else
                {
                    // Return our summary data, so that Visual Studio may write it to disk.
                    //outputFileContents = Marshal.AllocCoTaskMem(summaryData.Length);
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(summaryData.Length);

                   // Marshal.Copy(summaryData, 0, outputFileContents, summaryData.Length);
                    Marshal.Copy(summaryData, 0, rgbOutputFileContents[0], summaryData.Length);

                    output = (uint)summaryData.Length;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);

                LogException(ex);

                //outputFileContents = IntPtr.Zero;
                rgbOutputFileContents[0] = IntPtr.Zero;
                output = 0;
            }


            return VSConstants.S_OK;
        }


        public abstract byte[] GenerateContent(T element);

        public abstract byte[] GenerateSummaryContent();

        public abstract string GetDefaultExtension();

        public abstract int DefaultExtension(out string s);

       

        public abstract IEnumerator<T> GetEnumerator();

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (this.site == null)
            {
                throw new Win32Exception(-2147467259);
            }

            IntPtr objectPointer = Marshal.GetIUnknownForObject(this.site);

            try
            {
                Marshal.QueryInterface(objectPointer, ref riid, out ppvSite);

                if (ppvSite == IntPtr.Zero)
                {
                    throw new Win32Exception(-2147467262);
                }
            }
            finally
            {
                if (objectPointer != IntPtr.Zero)
                {
                    Marshal.Release(objectPointer);

                    objectPointer = IntPtr.Zero;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void SetSite(object pUnkSite)
        {
            this.site = pUnkSite;
        }

        protected abstract string GetFileName(T element);

        protected void LogException(Exception ex)
        {
            string path = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "log");

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(new string('-', 50));
                sw.WriteLine(ex.ToString());
                sw.WriteLine();
                sw.WriteLine(ex.Message);
                sw.WriteLine();
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine();
            }
        }

        protected virtual void OnError(Exception ex)
        {
        }

        protected virtual void OnFileGenerated(string filePath, bool isNewAdded)
        {
        }

        #endregion Methods
    }


    }


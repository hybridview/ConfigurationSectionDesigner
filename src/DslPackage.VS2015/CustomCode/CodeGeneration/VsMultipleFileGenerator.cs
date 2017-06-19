using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ConfigurationSectionDesigner.CustomCode.CodeGeneration;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Linq;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.CodeDom.Compiler;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using EnvDTE100;
using Microsoft.VisualStudio.Designer.Interfaces;


/*
 * Based on the code found here:
 * http://www.codeproject.com/KB/cs/VsMultipleFileGenerator.aspx
 */

namespace ConfigurationSectionDesigner
{


    [CLSCompliant(false)]
    public abstract class VsMultipleFileGenerator<TIterativeElement> : IEnumerable<TIterativeElement>,
        IVsSingleFileGenerator, IObjectWithSite
    {
        #region Visual Studio Specific Fields
        private object _site;
        private ServiceProvider _serviceProvider = null;
        internal const int S_OK = 0; // VSConstants.S_OK
        internal const int E_FAIL = unchecked((int)0x80004005);

        internal const string vsOutputWindowPaneName = "ConfigurationSectionDesigner OUTPUT";

        #endregion

        //public abstract class VsMultipleFileGenerator2<T> : IEnumerable<T>, IVsSingleFileGenerator, IObjectWithSite

        #region Our Fields

        private string _bstrInputFileContents;
        private string _wszInputFilePath;
        private EnvDTE.Project _project;
        private CodeDomProvider _codeDomProvider;

        private List<string> _oldFileNames;
        private List<string> _newFileNames;
        #endregion

        [CLSCompliant(false)]
        protected EnvDTE.Project Project
        {
            get
            {
                return _project;
            }
        }

        /// <summary>
        /// Returns a CodeDomProvider object for the language of the project containing
        /// the project item the generator was called on
        /// </summary>
        /// <returns>A CodeDomProvider object</returns>
        protected CodeDomProvider CodeProvider
        {
            get
            {
                if (_codeDomProvider == null)
                {
                    //Query for IVSMDCodeDomProvider/SVSMDCodeDomProvider for this project type
                    IVSMDCodeDomProvider provider = SiteServiceProvider.GetService(typeof(SVSMDCodeDomProvider)) as IVSMDCodeDomProvider;
                    if (provider != null)
                    {
                        _codeDomProvider = provider.CodeDomProvider as CodeDomProvider;
                    }
                    else
                    {
                        //In the case where no language specific CodeDom is available, fall back to C#
                        _codeDomProvider = CodeDomProvider.CreateProvider("C#");
                    }
                }
                return _codeDomProvider;
            }
        }

        /// <summary>
        ///  contains the name of the default namespace for the current solution or folder; it’s a useful piece of information to have for generating code.
        /// </summary>
        protected string RootNamespace
        {
            get
            {
                if (_project == null) return null;
                //Diagnostics.DebugWrite("_project.Properties.Count={0}", _project.Properties.Count);
                //Diagnostics.DebugWrite("_project.Name={0}", _project.Name);
                EnvDTE.Property property = _project.Properties.Item("RootNamespace");
                if (property == null) return null;

                return property.Value.ToString();
            }
        }

        /// <summary>
        /// contains the contents of the input file as a single string; this might seem redundant since we already know the path, but what can I say – it's convenient, saves one line of code.
        /// </summary>
        protected string InputFileContents
        {
            get
            {
                return _bstrInputFileContents;
            }
        }

        /// <summary>
        /// contains the path of the file for which content is being generated.
        /// </summary>
        protected string InputFilePath
        {
            get
            {
                return _wszInputFilePath;
            }
        }

        protected object Site
        {
            get { return _site; }
        }

        [CLSCompliant(false)]
        protected ServiceProvider SiteServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleServiceProvider = _site as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    _serviceProvider = new ServiceProvider(oleServiceProvider);
                }
                return _serviceProvider;
            }
        }

        private Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress _generatorProgress;
        /// <summary>
        /// Interface to the VS shell object we use to tell our progress while we are generating.
        ///  is an interface that we can use to tell Visual Studio how long the operation will take. This is only useful if your 
        ///  custom tool does something that takes a long time.
        /// </summary>
        protected Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress GeneratorProgress
        {
            get
            {
                return _generatorProgress;
            }
        }

        public VsMultipleFileGenerator()
        {
            Diagnostics.DebugWrite("VsMultipleFileGenerator() ctor");
            _oldFileNames = new List<string>();
            _newFileNames = new List<string>();
            // EnsureActiveProjectSet();
        }

        /*
        private void EnsureActiveProjectSet()
        {
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));

            Array activeProjects = (Array)dte.ActiveSolutionProjects;
          
            if (activeProjects.Length > 0)
            {
                _project = (Project)activeProjects.GetValue(0);
                Diagnostics.DebugWrite("VsMultipleFileGenerator() => EsnureActiveProjectSet() => Found active project '" + _project.FullName + "'");
            }
            else
            {
                // Designer open, but different project active. Must find project another way.


                //_project = (Project)activeProjects.GetValue(0);
                Diagnostics.DebugWrite("VsMultipleFileGenerator() => EsnureActiveProjectSet() => Found active project MANUALLY '" + _project.FullName + "'");
            }

        }
        */

        public abstract IEnumerator<TIterativeElement> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract string GetFileName(TIterativeElement element);
        public abstract byte[] GenerateContent(TIterativeElement element);


        public abstract byte[] GenerateDefaultContent();


        #region IObjectWithSite Members

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (this._site == null)
            {
                throw new Win32Exception(-2147467259);
            }

            IntPtr objectPointer = Marshal.GetIUnknownForObject(this._site);

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

        public void SetSite(object pUnkSite)
        {
            this._site = pUnkSite;
        }

        #endregion

        /// <summary>
        /// Implements the IVsSingleFileGenerator.DefaultExtension method. 
        /// Returns the extension of the generated file
        /// </summary>
        /// <param name="pbstrDefaultExtension">Out parameter, will hold the extension that is to be given to the output file name. The returned extension must include a leading period</param>
        /// <returns>S_OK if successful, E_FAIL if not</returns>
        public abstract int DefaultExtension(out string pbstrDefaultExtension);




        /// <summary>
        /// Implements the IVsSingleFileGenerator.Generate method.
        /// Executes the transformation and returns the newly generated output file, whenever a custom tool is loaded, or the input file is saved
        /// </summary>
        /// <remarks>
        /// When this is called, the current generator enumerator consists of a list of file extentions that we are going to generate.
        /// </remarks>
        /// <param name="wszInputFilePath">The full path of the input file. May be a null reference (Nothing in Visual Basic) in future releases of Visual Studio, so generators should not rely on this value</param>
        /// <param name="bstrInputFileContents">The contents of the input file. This is either a UNICODE BSTR (if the input file is text) or a binary BSTR (if the input file is binary). If the input file is a text file, the project system automatically converts the BSTR to UNICODE</param>
        /// <param name="wszDefaultNamespace">This parameter is meaningful only for custom tools that generate code. It represents the namespace into which the generated code will be placed. If the parameter is not a null reference (Nothing in Visual Basic) and not empty, the custom tool can use the following syntax to enclose the generated code</param>
        /// <param name="rgbOutputFileContents">[out] Returns an array of bytes to be written to the generated file. You must include UNICODE or UTF-8 signature bytes in the returned byte array, as this is a raw stream. The memory for rgbOutputFileContents must be allocated using the .NET Framework call, System.Runtime.InteropServices.AllocCoTaskMem, or the equivalent Win32 system call, CoTaskMemAlloc. The project system is responsible for freeing this memory</param>
        /// <param name="pcbOutput">[out] Returns the count of bytes in the rgbOutputFileContent array</param>
        /// <param name="pGenerateProgress">A reference to the IVsGeneratorProgress interface through which the generator can report its progress to the project system</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns E_FAIL</returns>
        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput,
            IVsGeneratorProgress pGenerateProgress)
        {
            // TODO: There are a number of nested try catch blocks that should be cleaned up.  
            // Make sure exception handling is properly laid out. This function has high 
            // complexity (probably too high with cycl over 30), which makes error handling important. What 
            // exceptions escape (ie. not handled)? What halts file generation? etc...
            try
            {
                // EnsureActiveProjectSet();

                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "");
                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, string.Format("------ CSD file generation started: Configuration document: {0} ------", InputFilePath));

                Diagnostics.DebugWrite("VsMultipleFileGenerator => VSMFG.");

                ProjectItem configurationSectionModelFile = null;

                _bstrInputFileContents = bstrInputFileContents;
                _wszInputFilePath = wszInputFilePath;
                _generatorProgress = pGenerateProgress;
                _oldFileNames.Clear();
                _newFileNames.Clear();

                // Look through all the projects in the solution
                // The _project holds DTE reference, so not needed here.
                //DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
                //Diagnostics.DebugWrite("VSMFG.Generate >> dte.Solution.Projects.Count = {0}.", dte.Solution.Projects.Count);

                VsOutputWindowPaneManager.OutputWindowWrite(vsOutputWindowPaneName, "* Searching for configuration project handle... ");

                Project project = null;
                ProjectItem projectItem = null;
                if (!VsHelper.FindProjectContainingFile(InputFilePath, out projectItem, out project))
                {
                    // TODO: Will this fail if NEW diagram is created but not saved? Confirm. I THINK empty diagram is always saved to disk upon creation.
                    throw new ApplicationException("Could not retrieve Visual Studio ProjectItem and/or parent project containing this CSD diagram. Has this diagram been saved yet after being created?");
                }
                _project = project;

                // obtain a reference to the current project as an IVsProject type
                IVsProject vsProject = VsHelper.ToVsProject(_project); // ABM - Issue 10435: 2nd WindowsService project results in FAILURE here.
                Diagnostics.DebugWrite("VH.TFP >> IVsProject vsProject = VsHelper.ToVsProject( project='{0}' )", _project.Name);

                int iFound = 0;
                uint itemId = 0;

                // this locates, and returns a handle to our source file, as a ProjectItem
                // TODO: 20140908: Might not be needed due to the new way I locate projectitem and project.
                VSDOCUMENTPRIORITY[] prio = new VSDOCUMENTPRIORITY[(int)VSDOCUMENTPRIORITY.DP_Standard];
                int result = vsProject.IsDocumentInProject(InputFilePath, out iFound, prio, out itemId); // ABM - Issue 10435: WindowsService project results in iFound=0!!!

                #region VSITEMID Documentation
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
                 */
                /*
                 * http://visual-studio.todaysummary.com/q_visual-studio_76086.html
                 * 
                 * http://msdn.microsoft.com/en-us/subscriptions/downloads/microsoft.visualstudio.shell.interop.ivshierarchy(v=vs.100).aspx
                 * The IVsHierarchy interface is a generic interface to a hierarchy of nodes. Each node, including the root node, can have arbitrary 
                 * properties associated with it. Each node on the hierarchy object is identified using a cookie (VSITEMID), which indicates a particular 
                 * node. This cookie is invisible to the consumer of IVsHierarchy, and is typically a pointer to some private data maintained by the 
                 * hierarchy's implementation.

    A VSITEMID is a DWORD uniquely identifying a node within a hierarchy. Itemids from one IVsHierarchy may not be passed to another hierarchy. Also, note that itemids have a limited lifetime, as indicated by events fired by the hierarchy, so holding on to itemids for long durations will require either the sinking of these events, or the conversion of the itemid into a canonical, persistable form.

    An item in a hierarchy can be a leaf node, a container of other items, or a link into some other hierarchy using GetNestedHierarchy.

    The IVsHierarchy interface is not used only for project hierarchies. For example, the Server Explorer window implements the IVsHierarchy interface to display its hierarchy, which is not a project hierarchy.

    There are times when it is useful to query a hierarchy about various virtual nodes, such as the hierarchy itself or the selected nodes within the hierarchy. Where such virtual nodes are potentially of interest, one of the predefined VSITEMID values may be passed.

    The environment views a project as a hierarchy, that is, a tree of nodes in which the nodes are project items. Each node also has a set of associated properties, and provides hierarchy management for VSPackages that implement project hierarchies.

    Notes to Implementers
    Implemented by VSPackages that create their own project hierarchy.

    Notes to Callers
    Called by the environment to get and set hierarchy properties.*/

                #endregion

                if (result != VSConstants.S_OK)
                    throw new Exception("Unexpected error calling IVsProject.IsDocumentInProject.");

                Diagnostics.DebugWrite("VH.TFP >> vsProject.IsDocumentInProject(inputFilePath, out iFound={0}, pdwPriority, out itemId={1}).", iFound, itemId);

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
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem");
                    }
                }
                else
                {
                    VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "error: Unable to retrieve Visual Studio ProjectItem. File generation halted.");
                    throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem. Try running the tool again.");
                }

                // ABM: Code below was the complex and error prone way to find project/items. Interface exists to easily 
                // locate these items. This interface is used to set current project as local variable instead of here.
                /**/
                // [7296] FILE = ~\ConfigurationSectionDesigner\Debugging\Sample.csd.
                if (!VsHelper.TryFindProjectItemAndParentProject(InputFilePath, out _project, out configurationSectionModelFile) || _project == null)
                {
                    VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "error: Unable to retrieve Visual Studio ProjectItem. File generation halted.");
                    throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem. Try running the tool again.");
                }
                /**/
                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "found!");

                // Check out this CSD file. DISABLED: Too much can go wrong the way this generator is currently structured.
                /*if (_project.DTE.SourceControl.IsItemUnderSCC(InputFilePath) && !_project.DTE.SourceControl.IsItemCheckedOut(InputFilePath))
                { _project.DTE.SourceControl.CheckOutItem(InputFilePath); } */

                // Check for source control integration for the CSD item and check it out of needed.
                //if (VsHelper.IsItemUnderSourceControl(dte, configurationSectionModelFile))
                if (_project.DTE.SourceControl.IsItemUnderSCC(InputFilePath)
                    && !_project.DTE.SourceControl.IsItemCheckedOut(InputFilePath))
                {
                    // ABM: For simplicity, we checkout the CSD file and all subitems. All file operations later down the chain will not have a SC issue.
                    // REASON: This generator has a unique design where null generated content means "don't write to file at this step". For cs files, 
                    //         the write to file occurs outside of the iteration later on. For diagram, it never happens. All of these conditions 
                    //         make selective SC checkout more complicated than the possible performance improvement is worth. Opinions?

                    VsHelper.CheckoutItem(_project.DTE, configurationSectionModelFile);
                }

                #region new

                // Get names of all of the existing files under CSD.
                foreach (ProjectItem childItem in configurationSectionModelFile.ProjectItems)
                {
                    _oldFileNames.Add(childItem.Name);
                }
                #endregion

                // now we can start our work, iterate across all the 'elements' in our source file.
                // Each of these items is a file extension.
                foreach (TIterativeElement item in this)
                {
                    // This try catch only exists for debugging purposes.
                    try
                    {
                        // obtain a name for this target file
                        string fileName = GetFileName(item);
                        Diagnostics.DebugWrite("VSMFG.Generate >> Filename for current element in loop is '{0}'", fileName);

                        // add it to the tracking cache
                        _newFileNames.Add(fileName);

                        // fully qualify the file on the filesystem
                        //string strFile = Path.Combine(wszInputFilePath.Substring(0, wszInputFilePath.LastIndexOf(Path.DirectorySeparatorChar)), fileName);
                        string strFile = Path.Combine(Path.GetDirectoryName(wszInputFilePath), fileName);

                        bool isNewAdded = !_oldFileNames.Contains(fileName);

                        /*
                        if (!isNewAdded)
                        {
                            if (_project.DTE.SourceControl.IsItemUnderSCC(strFile) && !_project.DTE.SourceControl.IsItemCheckedOut(strFile))
                            {
                                Diagnostics.DebugWrite("VSMFG.Generate >> Checking file out from source control '{0}'", fileName);
                                _project.DTE.SourceControl.CheckOutItem(strFile);
                            }
                        }*/

                        // create the file
                        FileStream fs = null;

                        try
                        {
                            // generate our target file content
                            byte[] data = GenerateContent(item); // NOTE: For .diagram and .cs, this will be null, so those will not be written.

                            // if data is null, it means to ignore the contents of the generated file (no write to file).
                            if (data == null) continue;

                            fs = File.Create(strFile);

                            #region Replaced code: Remove later.
                            /*if (!isNewAdded)
                            {
                                // If the (xsd or config) file already exists, only save the data if the generated file is different than the existing file.
                                if (!Util.IsDataEqual(File.ReadAllBytes(strFile), data))
                                {
                                    // Check out this file.
                                    if (_project.DTE.SourceControl.IsItemUnderSCC(strFile) &&
                                        !_project.DTE.SourceControl.IsItemCheckedOut(strFile))
                                    {
                                        Diagnostics.DebugWrite("VSMFG.Generate >> Checking file out from source control '{0}'", fileName);
                                        _project.DTE.SourceControl.CheckOutItem(strFile);
                                    }

                                    Diagnostics.DebugWrite("VSMFG.Generate >> File data has changed for file '{0}'. Re-using existing file...", strFile);
                                    fs = File.Open(strFile, FileMode.Truncate);
                                }
                            }
                            else
                            {
                                // create the file
                                fs = File.Create(strFile);
                            }*/

                            //if (fs != null) // We only write if file is modified or new.
                            //{

                            #endregion

                            VsOutputWindowPaneManager.OutputWindowWrite(vsOutputWindowPaneName, "* Generating the " + item + " file...");

                            // write it out to the stream
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                            OnFileGenerated(strFile, isNewAdded);

                            // add the newly generated file to the solution, as a child of the source file
                            //if (!configurationSectionModelFile.ProjectItems.Cast<ProjectItem>().Any(pi => pi.Name == fileName))
                            if (isNewAdded)
                            {
                                ProjectItem itm = configurationSectionModelFile.ProjectItems.AddFromFile(strFile);
                                /*
                                 * Here you may wish to perform some addition logic such as, setting a custom tool for the target file if it
                                 * is intented to perform its own generation process.
                                 * Or, set the target file as an 'Embedded Resource' so that it is embedded into the final Assembly.
                         
                                EnvDTE.Property prop = itm.Properties.Item("CustomTool");
                                //// set to embedded resource
                                itm.Properties.Item("BuildAction").Value = 3;
                                if (String.IsNullOrEmpty((string)prop.Value) || !String.Equals((string)prop.Value, typeof(AnotherCustomTool).Name))
                                {
                                    prop.Value = typeof(AnotherCustomTool).Name;
                                }
                                */
                            }
                            //}

                        }
                        catch (Exception e)
                        {
                            //GeneratorProgress.GeneratorError( false, 0, string.Format( "{0}\n{1}", e.Message, e.StackTrace ), -1, -1 );
                            //GeneratorProgress.GeneratorError(0, 0, string.Format("{0}\n{1}", e.Message, e.StackTrace), 0, 0);

                            if (File.Exists(strFile))
                            {
                                // TODO: ABM - Should we write this error to the file (breaking the build), or just show error and keep old file?
                                File.WriteAllText(strFile, "An exception occured while running the CsdFileGenerator on this file. See the Error List for details. E=" + e);
                            }
                            throw;
                        }
                        finally
                        {
                            if (fs != null)
                                fs.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // This is here for debugging purposes, as setting a breakpoint here can be very helpful
                        Diagnostics.DebugWrite("VSMFG.Generate >> EXCEPTION: {0}", ex);
                        throw;
                    }
                }

                // perform some clean-up, making sure we delete any old (stale) target-files
                VsOutputWindowPaneManager.OutputWindowWrite(vsOutputWindowPaneName, "* Cleaning up existing files... ");
                foreach (ProjectItem childItem in configurationSectionModelFile.ProjectItems)
                {
                    string next;
                    DefaultExtension(out next);

                    if (!(childItem.Name.EndsWith(next) || _newFileNames.Contains(childItem.Name)))
                    {
                        // then delete it
                        childItem.Delete();
                    }
                }
                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "complete!");

                VsOutputWindowPaneManager.OutputWindowWrite(vsOutputWindowPaneName, "* Generating the class file... ");

                // generate our default content for our 'single' file
                byte[] defaultData = null;
                try
                {
                    // This will call GenerateAllContent(string.Format("{0}-gen", [cs or vb]));
                    defaultData = GenerateDefaultContent();
                }
                catch (Exception ex)
                {
                    Diagnostics.DebugWrite("VSMFG.Generate >> EXCEPTION: {0}", ex);
                    throw;
                    //GeneratorProgress.GeneratorError( false, 0, string.Format( "{0}\n{1}", ex.Message, ex.StackTrace ), -1, -1 );
                    //GeneratorProgress.GeneratorError(0, 0, string.Format("{0}\n{1}", ex.Message, ex.StackTrace), 0, 0);
                }
                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "complete!");

                if (defaultData == null)
                    defaultData = new byte[0];
                // return our default data (code behind file), so that Visual Studio may write it to disk.

                /*
                 * You need to write the bytes of the generated file into this variable. However, you cannot 
                 * do it directly (hence the IntPtr[] type) – instead, you must use the System.Runtime.InteropServices.AllocCoTaskMem 
                 * allocator to create the memory and write type bytes in there.
                 */
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(defaultData.Length);

                Marshal.Copy(defaultData, 0, rgbOutputFileContents[0], defaultData.Length);

                // must be set to the number of bytes that we wrote to rgbOutputFileContents.
                pcbOutput = (uint)defaultData.Length;

                //string codefileName = GetFileName(CodeFileExtension);
                //OnFileGenerated(codefileName, isNewAdded);

                VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "========== CSD file generation: complete! ==========");

            }
            catch (Exception ex)
            {
                // Currently, all exceptions are rethrown to be caught here.
                OnError(ex);

                LogException(ex);

                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
            }

            return 0;
        }

        protected void LogException(Exception ex)
        {
            // TODO: Write exception details and stack trace to a proper location.
        }

        protected virtual void OnError(Exception ex)
        {
            // Handle error here.
            // TODO: Should use resources and error handling system that shows user friendly message, but logs exception stack.

            string msg = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
            VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, "[ ERROR ENCOUNTERED! ] " + msg);
            VsOutputWindowPaneManager.OutputWindowFocus(vsOutputWindowPaneName);
            GeneratorProgress.GeneratorError(0, 0, msg, 0, 0);

        }

        protected virtual void OnFileGenerated(string filePath, bool isNewAdded)
        {
            // 
            VsOutputWindowPaneManager.OutputWindowWriteLine(vsOutputWindowPaneName, string.Format(" Generated file {0}.", filePath));
        }

        /// <summary>
        /// Method that will communicate an error via the shell callback mechanism
        /// </summary>
        /// <param name="level">Level or severity</param>
        /// <param name="message">Text displayed to the user</param>
        /// <param name="line">Line number of error</param>
        /// <param name="column">Column number of error</param>
        protected virtual void GeneratorError(uint level, string message, uint line, uint column)
        {
            Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress progress = GeneratorProgress;
            if (progress != null)
            {
                progress.GeneratorError(0, level, message, line, column);
            }
        }
    }
}


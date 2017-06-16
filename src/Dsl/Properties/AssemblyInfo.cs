#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Resources;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"Configuration Section Designer")]
[assembly: AssemblyDescription(@"Configuration Section Designer")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany( @"Configuration Section Designer Team" )]
[assembly: AssemblyProduct(@"ConfigurationSectionDesigner")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"2.0.1.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"ConfigurationSectionDesigner.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100F36A35E21B47CB5707AB432B958366692194F99C089B801C47D6FA3635097031F85A596FB87C9170CB646B8B3DA39E0281E5C882049AFD8A8A5A21A84D411A8E173DCFAA4E987534F9F05FD7FC635FDB9D0C914D5C4460E823DBAD03B27B4830878C08797EF5F631719538292AEBCCFD801AA3058A178DD38EBDB0A9531AB5AF")]

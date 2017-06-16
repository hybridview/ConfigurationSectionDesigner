using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace ConfigurationSectionDesigner
{
    // Attribute used to register our custom tool with Visual Studio

    class FileGenerationRegistrationAttribute : RegistrationAttribute
    {
        private string _packageGuid;
        private string _generatorClsid;
        private string _editorFactoryGuid;
        private Type _generatorType;

        private const string CSharpGeneratorsGuid = "{fae04ec1-301f-11d3-bf4b-00c04f79efbc}";
        private const string CSharpProjectGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";

        private const string VBGeneratorsGuid = "{164B10B9-B200-11D0-8C61-00A0C91E29D5}";
        private const string VBProjectGuid = "{F184B08F-C81C-45f6-A57F-5ABD9991F28F}";

        public FileGenerationRegistrationAttribute( string packageGuid, string generatorClsid, string editorFactoryGuid, Type generatorType )
        {
            if( packageGuid == null )
                throw new ArgumentNullException( "packageGuid" );
            if( generatorClsid == null )
                throw new ArgumentNullException( "generatorClsid" );
            if( editorFactoryGuid == null )
                throw new ArgumentNullException( "editorFactoryGuid" );
            if( generatorType == null )
                throw new ArgumentNullException( "generatorType" );

            _packageGuid = packageGuid;
            _generatorClsid = generatorClsid;
            _generatorType = generatorType;
            _editorFactoryGuid = editorFactoryGuid;
        }

        public override void Register( RegistrationAttribute.RegistrationContext context )
        {
            try
            {
                context.Log.Write( "Registering Csd generator ... " );

                // register class
                Key key = context.CreateKey( @"CLSID" );
                Key subKey = key.CreateSubkey( _generatorClsid );
                subKey.SetValue( "ThreadingModel", "Both" );
                subKey.SetValue( "InprocServer32", Path.Combine( Environment.SystemDirectory, "mscoree.dll" ) );
                subKey.SetValue( "Class", _generatorType.FullName );
                subKey.SetValue( "Assembly", _generatorType.Assembly.FullName );
                subKey.Close();
                key.Close();

                // register custom generator
                key = context.CreateKey( @"Generators\" + CSharpGeneratorsGuid );
                subKey = key.CreateSubkey( "CsdFileGenerator" );
                subKey.SetValue( string.Empty, "Configuration Section Designer Generator" );
                subKey.SetValue( "CLSID", _generatorClsid );
                subKey.SetValue( "GeneratesDesignTimeSource", 1 );
                subKey.Close();
                key.Close();

                key = context.CreateKey( @"Generators\" + VBGeneratorsGuid );
                subKey = key.CreateSubkey( "CsdFileGenerator" );
                subKey.SetValue( string.Empty, "Configuration Section Designer Generator" );
                subKey.SetValue( "CLSID", _generatorClsid );
                subKey.SetValue( "GeneratesDesignTimeSource", 1 );
                subKey.Close();
                key.Close();

                // register .csd editor notification
                key = context.CreateKey( @"Projects\" + CSharpProjectGuid + @"\FileExtensions" );
                subKey = key.CreateSubkey( ".csd" );
                subKey.SetValue( "EditorFactoryNotify", _editorFactoryGuid );
                subKey.Close();
                key.Close();
                context.Log.WriteLine( "Success." );

                key = context.CreateKey( @"Projects\" + VBProjectGuid + @"\FileExtensions" );
                subKey = key.CreateSubkey( ".csd" );
                subKey.SetValue( "EditorFactoryNotify", _editorFactoryGuid );
                subKey.Close();
                key.Close();
                context.Log.WriteLine( "Success." );
            }
            catch( Exception e )
            {
                context.Log.WriteLine( "Failure: " + e );
            }
        }

        public override void Unregister( RegistrationAttribute.RegistrationContext context )
        {
            try
            {
                context.Log.Write( "Unregistering Configuration Section Designer Generator... " );
                context.RemoveKey( @"CLSID\" + _generatorClsid );
                context.RemoveKey( @"Generators\" + CSharpGeneratorsGuid + @"\CsdFileGenerator" );
                context.RemoveKey( @"Generators\" + VBGeneratorsGuid + @"\CsdFileGenerator" );
                context.RemoveKey( @"Projects\" + CSharpProjectGuid + @"\FileExtensions\.csd" );
                context.RemoveKey( @"Projects\" + VBProjectGuid + @"\FileExtensions\.csd" );
                context.Log.WriteLine( "Success." );
            }
            catch( Exception e )
            {
                context.Log.WriteLine( "Failure: " + e );
            }
        }
    }
}

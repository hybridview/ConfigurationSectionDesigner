using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    /// <summary>
    /// Provides helper methods for naming elements and properties.
    /// </summary>
    internal static class NamingHelper
    {
        [Flags]
        public enum ValidationOptions
        {
            None = 0,
            IsRequired = 1,
            IsXml = 2,
            IsNamespace = 4

        }

        /// <summary>
        /// Converts an identifier to camelCase.
        /// </summary>
        /// <param name="identifier">The identifier to convert.</param>
        /// <returns>The given identifier with the first character converted to lower case.</returns>
        public static string ToCamelCase(string identifier)
        {
            string newIdentifier = identifier;
            if (!string.IsNullOrEmpty(newIdentifier))
            {
                newIdentifier = newIdentifier.Substring(0, 1).ToLower() + newIdentifier.Substring(1);
            }
            return newIdentifier;
        }

        // Checks whether name is a valid variable name. If empty, no check performed.
        // [20121217] Changing to simple SPACE check for now (most common validation issue). Was causing some issues and I do not have time to resolve yet.
        /*
        public static bool IsValidName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.IndexOf(' ') >= 0)
            {
                return false;
            }

            // MATCH:       myName, my0Name, myName0, MyName`
            // NOT MATCH:   0myName, my Name
            bool valid = false;
            Regex re = new Regex("^[^0-9\\.][a-zA-Z0-9\\.\\:]+[\\`]?$");
            if (re.IsMatch(name))
            {
                valid = true;
                if (name.ToLower().StartsWith("config"))
                {
                    valid = false;
                }
            }
            return valid;
            //return (re.IsMatch(name));
        }
        */

        #region "New Validators"



        /// <summary>
        /// Validate the class name of the element.
        /// </summary>
        /// <remarks>
        /// NOTE: The actual class name of the element might have no reserved word restrictions, since it is not used in the XML. Visit this...
        /// 
        /// Validate the 'name' property of type element (In 'Elements' section on CSD diagram).
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static bool TryValidateElementsItemNameProperty(string name, ValidationOptions options, out string msg)
        {
            return _TryValidateValueBase("Element", "Name", name, name, new List<string>(){
                "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                "location", // Implicit is not allowed, and can only start with "location" for implicit names.
                "config"}, options, out msg);
        }

        /// <summary>
        /// Validate a name property from Section.
        /// 
        /// 
        /// </summary>
        /// <param name="typeBase"></param>
        /// <param name="sectName"></param>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static bool TryValidateSectionNameProperty(ModelElement typeBase, string sectName, string name, ValidationOptions options, out string msg)
        {
            /*
            return _TryValidateValueBase(typeBase.GetType().Name, "Name", sectName, name, 
                new List<string>(){
                    "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                    "location", // Implicit is not allowed, and can only equal "location" for implicit names.
                    "^config"} // Implicit is not allowed, and can only START with "config" for implicit names.
                , options, out msg);
            */

            // TODO: Section has always allowed name to start with 'config'. Revisit 'implicit' definition from NETReferenceSource.
            // Issue might be sections are allowed certain props in situations.

            return _TryValidateValueBase(typeBase.GetType().Name, "Name", sectName, name,
               new List<string>(){
                    "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                    "location"}
               , options, out msg);
        }

        /// <summary>
        /// Validate the 'namespace' property.
        /// 
        /// NOTE: All namespaces need the same validation, which is why we dont see name like 'TryValidateSectionNamespaceProperty' for this method.
        /// </summary>
        /// <param name="typeBase"></param>
        /// <param name="elemName"></param>
        /// <param name="val"></param>
        /// <param name="options"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static bool TryValidateNamespace(ModelElement typeBase, string elemName, string val, ValidationOptions options, out string msg)
        {
            options |= ValidationOptions.IsNamespace;
            return _TryValidateValueBase(typeBase.GetType().Name, "Namespace", elemName, val, null, options, out msg);
        }

        /*
        internal static bool TryValidateSectionName(string sectName, string name, out string msg)
        {
            // TODO: This may be similar to Element, where there is actually no reserved word restriction. Investigate and update.
            //return _TryValidateValueBase(type, "name", name, invalidStartStrings, options, out msg);

            return _TryValidateValueBase("Section", "Name", sectName, name, new List<string>(){
                "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                "location", // Implicit is not allowed, and can only start with "location" for implicit names.
                "config"}, // Implicit is not allowed, and can only start with "config" for implicit names.
                ValidationOptions.None,
                out msg);
        }
        */
        /*
        internal static bool TryValidatePropertyNameForElement(string name, out string msg)
        {
            return _TryValidateNameBase("Element", name, new List<string>(){
                "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                "location", // Implicit is not allowed, and can only start with "location" for implicit names.
                "config"}, // Implicit is not allowed, and can only start with "config" for implicit names.
            out msg);
        }
        */

        /// <summary>
        /// Validate a name property from an object in 'Attributes' part.
        /// </summary>
        /// <remarks>
        /// Not certain all of the restrictions are covered here, as it was difficult to find in .NET ref source.
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static bool TryValidateAttributesItemNameProperty(string objName, string name, ValidationOptions options, out string msg)
        {
            #region "Analysis of System.Configuration Source:"

            // NOTE: Only validate prop name when IsDefaultCollection and name is empty.
            /*
             if ((options & ConfigurationPropertyOptions.IsDefaultCollection) != 0 &&
                String.IsNullOrEmpty(name)) {
                name = DefaultCollectionPropertyName;
            }
            else {
                ValidatePropertyName(name);
            }
             */




            /*
            
            ValidatePropertyName(name) {
                 if (string.IsNullOrEmpty(name)) {
                    throw new ArgumentException(SR.GetString(SR.String_null_or_empty), "name");
                }

                if (BaseConfigurationRecord.IsReservedAttributeName(name)) {
                    throw new ArgumentException(SR.GetString(SR.Property_name_reserved, name));
                }
            }
             */


            /*
            // We reserve all attribute names starting with config or lock
            internal static bool IsReservedAttributeName(string name) {
                if (StringUtil.StartsWith(name, "config") ||
                    StringUtil.StartsWith(name, "lock")) {
                    return true;
                }
                else {
                    return false;
                }
            }
             */
            #endregion

            return _TryValidateValueBase("Property", "Name", objName, name, new List<string>(){
                "^lock",    // SR.Property_name_reserved.
                "^config"}, // SR.Property_name_reserved.
            options,
            out msg);
        }

        /// <summary>
        /// Only basic validation for objects that have unknown validation requirements.
        /// </summary>
        /// <param name="typeBase">Reference to the ModelElement so we can find it's name.</param>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static bool TryValidateBaseName(ModelElement typeBase, string name, ValidationOptions options, out string msg)
        {
            return _TryValidateValueBase(typeBase.GetType().Name, "Name", name, name, null, ValidationOptions.None, out msg);
        }
        private static bool _TryValidateValueBase(string type, string subject, string objName, string val,
            List<string> invalidStartStrings, ValidationOptions options, out string msg)
        {
            #region "Analysis of System.Configuration Source:"
            /*
        From http://www.w3.org/Addressing/

          reserved    = ';' | '/' | '?' | ':' | '@' | '&' | '=' | '+' | '$' | ','

        From Platform SDK

          reserved    = '\' |  '/' | '|' | ':' |  '"' |  '<' | '>'

        */
            /*
            Analysis of System.Configuration Source:
             
            NOTE: Element/section names themselves have no known reserved word restrictions. Restrictions are only present 
                    in property name definitions. However, I will still present my findings below. We will need to revisit 
                    the System.Configuration source to confirm.
             
             For section and element names, these are not allowed.
               "configProtectedData", // Implicit is not allowed, and that is an "implicit" name.
                "location", // Implicit is not allowed, and can only start with "location" for implicit names.
                "config"}, // Implicit is not allowed, and can only start with "config" for implicit names.
             
             For property names, these are not allowed.
              "lock", 
                "config"}, // Implicit is not allowed, and can only start with "config" for implicit names.
             
             */

            /* SECTION NAME CHECKS
             
            if (String.IsNullOrEmpty(name)) {
                throw new ConfigurationErrorsException(SR.GetString(SR.Config_tag_name_invalid), errorInfo);
            }

            // must be a valid name in xml, so that it can be used as an element
            // n.b. - it also excludes forward slash '/'
            try {
                XmlConvert.VerifyName(name);
            }
            // Do not let the exception propagate as an XML exception,
            // for we want errors in the section name to be treated as local errors,
            // not global ones.
            catch (Exception e) {
                throw ExceptionUtil.WrapAsConfigException(SR.GetString(SR.Config_tag_name_invalid), e, errorInfo);
            }
            
            // Checks if == "configProtectedData"
            if (IsImplicitSection(name)) {
                if (allowImplicit) {
                    // avoid test below for strings starting with "config"
                    return;
                }
                else {
                    throw new ConfigurationErrorsException(SR.GetString(SR.Cannot_declare_or_remove_implicit_section, name), errorInfo);
                }
            }

            if (StringUtil.StartsWith(name, "config")) {
                throw new ConfigurationErrorsException(SR.GetString(SR.Config_tag_name_cannot_begin_with_config), errorInfo);
            }
            // Checks if == "location"
            if (name == KEYWORD_LOCATION) {
                throw new ConfigurationErrorsException(SR.GetString(SR.Config_tag_name_cannot_be_location), errorInfo);
            }
            
            */

            #endregion

            msg = "";

            string messageBuilder = "";

            if (options.HasFlag(ValidationOptions.IsXml))
            {
                subject = "'Xml " + subject + "'";
            }
            messageBuilder = "The " + type + " " + subject;
            // Fix redundant messaging.
            if (objName != val)
            {
                messageBuilder += " for " + objName + " '" + val + "'";
            }
            else
            {
                messageBuilder += " '" + val + "'";
            }
            //EXAMPLE: context.LogError("The Xml Section Name is required and cannot be an empty string.", "RequiredProperty", this);

            /**
             * Required Check.
             * */
            if (options.HasFlag(ValidationOptions.IsRequired) && string.IsNullOrEmpty(val))
            {
                msg = messageBuilder + " cannot be empty.";
                return false;
            }

            string errMsgPartial = "";

            /**
             * Reserved Word Check.
             * */
            // + Cannot be named "configProtectedData", "location", "config" (Implicit is not allowed, and that is an "implicit" name).

            if (!_ValidateForReservedWords(val, invalidStartStrings, out errMsgPartial))
            {
                msg = messageBuilder + errMsgPartial;
                return false;
            }

            /**
             * Invalid chars Check.
             **/
            if (!string.IsNullOrEmpty(val))
            {
                if (options.HasFlag(ValidationOptions.IsXml))
                {
                    // XML
                    if (options.HasFlag(ValidationOptions.IsNamespace))
                    {
                        if (!_ValidateXmlNamespace(val, out errMsgPartial))
                        {
                            msg = messageBuilder + errMsgPartial;
                            return false;
                        }
                    }
                    else
                    {
                        if (!_ValidateXmlName(val, out errMsgPartial))
                        {
                            msg = messageBuilder + errMsgPartial;
                            return false;
                        }
                    }
                }
                else
                {
                    // C#
                    if (options.HasFlag(ValidationOptions.IsNamespace))
                    {
                        if (!_ValidateCLRNamespace(val, out errMsgPartial))
                        {
                            msg = messageBuilder + errMsgPartial;
                            return false;
                        }
                    }
                    else
                    {
                        if (!_ValidateCLRName(val, out errMsgPartial))
                        {
                            msg = messageBuilder + errMsgPartial;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool _ValidateForReservedWords(string val, List<string> invalidStartStrings, out string errMsgPartial)
        {
            errMsgPartial = "";

            bool hasInvalidWord = false;
            if (invalidStartStrings != null && invalidStartStrings.Count > 0)
            {
                string adj = "";
                string reservedWord = "";
                foreach (string inv in invalidStartStrings)
                {
                    if (inv.Substring(0, 1) == "^")
                    {
                        adj = "start with";
                        reservedWord = inv.Substring(1);
                        if (val.ToLower().StartsWith(reservedWord.ToLower()))
                        {
                            hasInvalidWord = true;
                        }
                    }
                    else
                    {
                        adj = "equal";
                        if (val.ToLower() == inv.ToLower())
                        {
                            hasInvalidWord = true;
                            reservedWord = inv.ToLower();
                        }
                    }
                    if (hasInvalidWord)
                    {
                        break;
                    }
                }
                if (hasInvalidWord)
                {
                    errMsgPartial = " cannot " + adj + " reserved word '" + reservedWord + "'";
                    return false;
                }
            }

            return true;
        }

        private static bool _ValidateCLRName(string val, out string errMsgPartial)
        {
            errMsgPartial = "";

            string regexStrStart = "^[^0-9\\.][";
            string regexStrAllowedChar = "a-zA-Z0-9_";
            string regexStrEnd = "]+[\\`]?$";
            string regexFriendly = "alphanumeric, start with a letter, cannot have special characters or spaces";

            Regex re = new Regex(regexStrStart + regexStrAllowedChar + regexStrEnd);

            if (!re.IsMatch(val))
            {
                errMsgPartial = " must be " + regexFriendly + ".";
                return false;
            }
            return true;
        }

        private static bool _ValidateCLRNamespace(string val, out string errMsgPartial)
        {
            errMsgPartial = "";
            string regexStrStart = "^[^0-9\\.][";
            string regexStrAllowedChar = "a-zA-Z0-9_";
            string regexStrEnd = "]+[\\`]?$";
            string regexFriendly = "alphanumeric, start with a letter, cannot have special characters or spaces";
            // Allow dots.
            regexStrAllowedChar += "\\.";
            regexFriendly += ", must be valid namespace";
            Regex re = new Regex(regexStrStart + regexStrAllowedChar + regexStrEnd);
            if (!re.IsMatch(val))
            {
                errMsgPartial = " must be " + regexFriendly + ".";
                return false;
            }
            return true;
        }
        private static bool _ValidateXmlNamespace(string val, out string errMsgPartial)
        {
            errMsgPartial = "";
            // Short-circuit check to see if valid url is used for ns.
            if (Uri.IsWellFormedUriString(val, UriKind.Absolute))
            {
                //isValid = true;
                return true;
            }

            string regexStrStart = "^[^0-9\\.][";
            string regexStrAllowedChar = "a-zA-Z0-9_";
            string regexStrEnd = "]+[\\`]?$";
            string regexFriendly = "alphanumeric, start with a letter, cannot have special characters or spaces";
            // Allow dots.
            regexStrAllowedChar += "\\.";
            regexFriendly += ", must be valid XML namespace";

            // Allow URN: for XML namespace.
            // regexStrAllowedChar += "\\:";
            Regex re = new Regex(regexStrStart + regexStrAllowedChar + regexStrEnd);
            if (!re.IsMatch(val))
            {
                errMsgPartial = " must be " + regexFriendly + ".";
                return false;
            }
            return true;
        }

        private static bool _ValidateXmlName(string val, out string errMsgPartial)
        {
            errMsgPartial = "";
            try
            {
                XmlConvert.VerifyName(val);
            }
            catch (Exception e)
            {
                errMsgPartial = " must be a valid XML name: " + e.Message;
                return false;
            }
            return true;
        }



        #endregion


        // Checks whether name is a valid variable name. If empty, no check performed.
        /*
        public static bool IsValidOrEmptyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            else
            {
                return (IsValidName(name));
            }
        }
        */

        // Checks whether name is a valid xml name.
        // [20121217] Changing to simple SPACE check for now (most common validation issue). Was causing some issues and I do not have time to resolve yet.
        /*
        public static bool IsValidXmlName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.IndexOf(' ') >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            // MATCH:       myName, my0Name, myName0, MyName`
            // NOT MATCH:   0myName, my Name
            //Regex re = new Regex("^[^0-9\\.][a-zA-Z0-9\\.\\:]+[\\`]?$");
            //return re.IsMatch(name);
        }
         */
        /*
        public static bool IsValidOrEmptyXmlName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            else
            {
                return (IsValidXmlName(name));
            }
        }*/

        /// <summary>
        /// Determines whether property should be validated.
        /// 
        /// For property model element, parent element is not available. However, we should prevent prop.IsDefaultCollection being set 
        /// when parent is not collection. Therefore, we presume that IsDefaultCollection setr to true means parent is collection.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="prop"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static bool RequiresValidation(string val, ConfigurationProperty prop, out NamingHelper.ValidationOptions options)
        {
            return RequiresValidation(val, prop.IsDefaultCollection, prop, out options);
        }

        /// <summary>
        /// Determines whether property should be validated.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isParentCollection"></param>
        /// <param name="prop"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static bool RequiresValidation(string val, bool isParentCollection, ConfigurationProperty prop, out NamingHelper.ValidationOptions options)
        {
            // TODO: This is NOT always required (ie. IsDefaultCollection)!

            options = NamingHelper.ValidationOptions.IsXml;

            bool isActualDefaultCollection = (prop.IsDefaultCollection && isParentCollection);
            if (!isActualDefaultCollection)
            {
                options |= NamingHelper.ValidationOptions.IsRequired;
            }

            bool skipValidation = prop.IsDefaultCollection && string.IsNullOrEmpty(val);
            return !skipValidation;
        }

    }
}
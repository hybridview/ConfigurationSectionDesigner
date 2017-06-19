using System;

namespace Samples.Configuration.ValidationSample
{

    /// <summary>
    /// Partial class with our validator implementation that is to be used in the generated code file.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: The NAME entered in the "Property Validators" found in "Configuration Explorer" is used to 
    /// generate a partial class in the generated code file (even if not assigned to anything in the diagram). 
    /// In this case, "CustomValidators" was entered in Name, so a partial class named 
    /// "CustomValidatorsCallbackValidatorClass" was generated. For "Callback", we entered "IsPrime", so that is 
    /// the name given for the implementation method.
    /// In THIS file, you create the same partial class definition, but create the actual implementation. The 
    /// generated partial class creates a stub to make this easier. 
    /// 
    /// This method defination was copied from the generated Validation.csd.cs, which 
    /// created a stub for us. The name and namespace of this class MUST be the same as the type converter class 
    /// that was generated (it is partial).
    /// </remarks>
    public partial class CustomValidatorsCallbackValidatorClass
    {
        
        public static void IsPrime(object value)
        {
            //throw new global::System.NotImplementedException();
            if (value == null)
                throw new ArgumentNullException("value");

            if (!(value is int))
                throw new ArgumentException("PrimeValidator only validates integers");

            int number = (int)value;
            switch (number)
            {
                case 2:
                case 3:
                case 5:
                case 7:
                case 11:
                case 13:
                case 17:
                case 19:
                case 23:
                case 29:
                case 31:
                case 37:
                case 41:
                case 43:
                case 47:
                case 53:
                case 59:
                case 61:
                case 67:
                case 71:
                case 73:
                case 79:
                case 83:
                case 89:
                case 97:
                    break;

                default:
                    throw new ArgumentException("The given value is not a prime number");
            }
        }
    }
}

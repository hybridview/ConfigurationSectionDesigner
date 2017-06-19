# Type conversion

Initially, all configuration data is read as a string and must be converted to the proper type to be used. For primitive types like integers and booleans, conversion is done automatically, but there are times when the default conversion is insufficient. Type converters can be used to handle custom conversion.

## Built-in converters

Some special cases have already been built into the .NET framework. To use these, simply pick one from the _Type Converter_ property.

## Custom converters

To use a custom converter, right click the _Configuration Section Model_ node in the _Configuration Section Explorer_ and select **Add New Custom Type Converter**. Give it a name and a type. On the property you wish to use it on, set the _Type Converter_ property to _Custom_ and the _Custom Type Converter_ to your desired custom type converter. The code generator will generate a class named what you named your custom converter, which will have two method calls that you need to implement. Instructions on how to properly create these methods are described in the comments of the generated code.
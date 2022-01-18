# ConfigurationSectionDesigner
Moved from CodePlex, which is closing.

## To Download the Compiled Extension: Click the [Releases](https://github.com/hybridview/ConfigurationSectionDesigner/releases) tab.

## Important note for those who wish to compile the VS2022 version:
For some reason, 2 required nuget packages refuse to install via package manager. I've seen reports of the issue on the web but no real solution except to download the nuget package manually and extract and reference the dlls. They are microsoft.visualstudio.threading and microsoft.visualstudio.utilities. You will need to download those packages, extract the dlls, and manually reference them in the DslPackage project to build this project. I wish I had a better solution.

## Project Description

A Visual Studio add-in that allows you to graphically design .NET Configuration Sections and automatically generates all the required code and a schema definition (XSD) for them.

![](https://github.com/hybridview/ConfigurationSectionDesigner/wiki/Home_CSD_Screen01_800_FX.png)

For installation, usage instructions, and more info, see the [project Wiki](https://github.com/hybridview/ConfigurationSectionDesigner/wiki).


## HELP REQUESTED
I have very little time to maintain this at the moment, so please feel free to fork and submit PULL requests. If you are providing good quality contributions and would like to be added as a maintainer of this project, let me know. Thank you!

## Future Goals
This tool has a lot of potential if some significant enhancements are implemented:
+ Ability to support AspNetCore JSON configuration/options.
+ More customization ability for generated code.
+ What else?

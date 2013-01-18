Exception templates for use with Microsoft Visual Studio 2005
-------------------------------------------------------------

Copy both of the zip files into ...\My Documents\Visual Studio 2005\Templates\ItemTemplates\Visual C#\

To create a new exception, chose "Add"->"New Item..." in VS, LocalModuleRaspException and ModuleRaspException 
should now be available. Create one ModuleException per module, and use it as a base class for your 
LocalModuleExceptions.

Together with each ModuleException a resource file called "ErrorMessages" should be created. 
In the resource file set name as the namespace+name of each exception type (delimited by underscores, 
'_', instead of dots, '.', and set the value to your error message for that particular type of 
exception.


using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("OBehave")]
[assembly: AssemblyProduct("OBehave")]
[assembly: AssemblyDescription("Simple Behavior Tree with a fluent API")]
[assembly: AssemblyCopyright("Copyright © 2012, Mohammad Bahij Abdulfatah")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
#if DEBUG
#   if PORTABLE_CLASS_LIBRARY
[assembly: AssemblyConfiguration("PCL-Debug")]
#   else
[assembly: AssemblyConfiguration("NET35-Debug")]
#   endif
#else
#   if PORTABLE_CLASS_LIBRARY
[assembly: AssemblyConfiguration("PCL-Release")]
#   else
[assembly: AssemblyConfiguration("NET35-Release")]
#   endif
#endif

[assembly: AssemblyVersion("0.1.0")]
[assembly: AssemblyFileVersion("0.1.0")]

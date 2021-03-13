using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ELibrary.Business.Aspects.PostSharp.ValidationAspects.FluentValidationAspects;
using ELibrary.Business.CrossCuttingConcerns.Validation.FluentValidation;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ELibrary.Business")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ELibrary.Business")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: FluentValidationAspect(typeof(BookValidator), AttributeTargetAssemblies = "using ELibrary.Business.Concrete")]
[assembly: FluentValidationAspect(typeof(EmployeeValidator), AttributeTargetAssemblies = "using ELibrary.Business.Concrete")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("2be144f0-8dac-43eb-ab56-aaee7c4cff4e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

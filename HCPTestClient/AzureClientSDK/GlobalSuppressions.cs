// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Makeing this as in Sync with EI's Request Type " , MessageId = "TBN", Scope = "type", Target = "AzureTBNClientSDK.AzureTBNClient")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Justification = "This has been in symmetric with Project Requirements Sections.", Scope = "namespace", Target = "AzureTBNClientSDK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This has been in symmetric with Project Requirements Sections.", MessageId = "SDK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This has been in symmetric with Project Requirements Sections.", MessageId = "TBN")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This has been in symmetric with Project Requirements Sections.", MessageId = "TBN", Scope = "namespace", Target = "AzureTBNClientSDK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "This has been in symmetric with Project Requirements Sections.", MessageId = "SDK", Scope = "namespace", Target = "AzureTBNClientSDK")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This Catch is required for Generic Exception rather than a Specific exeption as per project requirements.", Scope = "member", Target = "AzureTBNClientSDK.AzureTBNClient.#Send(AzureTBNClientSDK.AzureTBNServiceReference.RequestBase)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "This Catch is required for Generic Exception rather than a Specific exeption as per project requirements.", Scope = "member", Target = "AzureTBNClientSDK.AzureTBNClient.#CallOnPremiseSend(AzureTBNClientSDK.OnPremiseTBNServiceReference.RequestBase)")]

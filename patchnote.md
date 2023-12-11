## Patchnote V1.2.1

> # Warning!!! New version is incompatible to last version.

#### Changes:
* Added Builder Pattern
* delegates replaced to DelegatesRepo
* Added the opportunity to disable official commands
* Added summaries
* Condition has access to CmdEventArgs

## Patchnote V1.2.0

> # Warning!!! New version is incompatible to last version.

#### Changes:
* Added "author command", what shows in console after terminal started.
* Command args divided to
  * "ReturnTypes.Any"(every not-kwarg argument). All arguments are stored in "CmdEventArgs.Data"- List<string>.
  * "ReturnTypes.Kwargs"(arguments starts with '-', using as flags). All kwargs are stored in "CmdEventArgs.Kwargs"- List<string>.
  * "AnyAndKwargs" available too. All arguments are stored in "CmdEventArgs.Data"- List<string>. All kwargs are stored in "CmdEventArgs.Kwargs"- List<string>.
 Terminal automatically divides kwargs and args to Lists. All go in order of entry.
* Added "clear" command, what clears console.(help command updated)
* "ReturnTypes.Unknown" removed

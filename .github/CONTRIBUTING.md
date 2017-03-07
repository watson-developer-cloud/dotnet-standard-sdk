# Questions

If you are having problems using the APIs or have a question about the IBM Watson Services, please ask a question on [dW Answers][dw-answers] or [Stack Overflow][stack-overflow].

# Coding Standard

* Use spaces instead of tab characters, a tab should be 4 spaces.
* Class names should be `PascalCase` (e.g. SpeechToText), with no underscores `_`.
* Structures should follow the same naming standards as classes.
* Use [Allman style][allman-style] braces, where each brace begins on a new line. A single line statement block can go without braces but the block must be properly indented on its own line and it must not be nested in other statement blocks that use braces
* Use `_camelCase` for internal and private fields and use `readonly` where possible. Prefix instance fields with `_`, static fields with `s_` and thread static fields with `t_`. When used on static fields, `readonly` should come after `static` (i.e. `static readonly` not `readonly static`).
* No public variables, always use public properties unless there is no other workaround.
* `Properties` should be `PascalCase`, _no underscores_. (e.g. public bool IsReady { get; set; })
* `Constants` should be all upper case (e.g. static readonly string CONFIG_FILE = "/Config.json"). This includes enumerations.
* All `public functions` and types of all classes should be _fully documented_ using the XML comment style.
* `Local variables` should use camelCase. (e.g. var widgetConnector = new WidgetConnector())
* Use `protected` on variables & functions only if you plan to inherit from the class or there is a good chance we will need to be polymorphic.
* Use `region` to separate parts of a class based on functionality.
* Always specify the `visibility`, even if it's the default (i.e. private string _foo not string _foo). Visibility should be the first modifier (i.e. public abstract not abstract public).
* `Namespace` imports should be specified at the top of the file, outside of namespace declarations, after the license and should be sorted alphabetically.
* Avoid more than one empty line at any time. For example, do not have two blank lines between members of a type.
* Avoid spurious `free spaces`. For example avoid if (someVar == 0)..., where the dots mark the spurious free spaces. Consider enabling "View White Space (Ctrl+E, S)" if using Visual Studio, to aid detection.
* Only use `var` when it's obvious what the variable type is (i.e. var stream = new FileStream(...) not var stream = OpenStandardInput()).
* `Fields` should be specified at the top within type declarations.

# Issues

If you encounter an issue with the .NET Standard SDK, you are welcome to submit a [bug report][dotnet-sdk-issues]. Before that, please search for similar issues. It's possible somebody has already encountered this issue.

# Pull Requests

If you want to contribute to the repository, follow these steps:

1. Fork the repo.
1. Develop and test your code changes Make sure you work in a feature branch. **PLEASE do not do your work in `master`.**
1. Add a unit test for any new classes you add. Only refactoring and documentation changes require no new tests.
1. Run unit tests within Visual Studio.
1. Commit your changes.
1. Push to your fork and submit a pull request.

# Developer's Certificate of Origin 1.1

By making a contribution to this project, I certify that:

(a) The contribution was created in whole or in part by me and I
   have the right to submit it under the open source license
   indicated in the file; or

(b) The contribution is based upon previous work that, to the best
   of my knowledge, is covered under an appropriate open source
   license and I have the right under that license to submit that
   work with modifications, whether created in whole or in part
   by me, under the same open source license (unless I am
   permitted to submit under a different license), as indicated
   in the file; or

(c) The contribution was provided directly to me by some other
   person who certified (a), (b) or (c) and I have not modified
   it.

(d) I understand and agree that this project and the contribution
   are public and that a record of the contribution (including all
   personal information I submit with it, including my sign-off) is
   maintained indefinitely and may be redistributed consistent with
   this project or the open source license(s) involved.

[dw-answers]: https://developer.ibm.com/answers/questions/ask/?topics=watson
[stack-overflow]: http://stackoverflow.com/questions/ask?tags=ibm-watson
[allman-style]: http://en.wikipedia.org/wiki/Indent_style#Allman_style
[dotnet-sdk-issues]: https://github.com/watson-developer-cloud/dotnet-standard-sdk/issues

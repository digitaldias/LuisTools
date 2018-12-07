# NuanceToLuis
For users of Nuance that are curious about how their models compare to Microsoft LUIS, you can use this tool to quickly export your Nuance models into a [LuDown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Ludown) format for reviewing and converting to a working LUIS application. 

## The tool will optimize it's output for LUIS 
Utterances are handled differently between Nuance and LUIS. Whilst Nuance may require some example utterances where the only differences are the entity values, LUIS will make due with a single utterance containing an entity. Thus, during conversion, **NuanceToLuis** will remove duplicate utterances where the only differences are the sample entity values used.

## The suggested workflow is as follows: 
- Export your models from Nuance into **.trsx** files
- Convert the exported files to **.lu** using NuanceToLuis
- Review the LuDown files before exporting
- Convert the LuDown files into a LUIS friendly format using [LuDown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Ludown)
- Upload the final **.json** files to your LUIS workspace

## PreRequisites
In order to compile and run this tool you need to have a working **DotNet Core** installation on your machine (Windows, Mac, Linux are all ok for this). Installing and configuring DotNet core is beyond the scope of this document. *NuanceToLuis* was built using **dotnet core 2.1** for your reference.

### Building a Release version of the tool
Navigate to your cloned folder, change directory to the base folder for the NuanceToLuis solution and type: 
<pre>
   C:\code\LuisTools\NuanceToLuis> dotnet build digitaldias.sln --configuration Release
</pre>
Of course, if you are using Visual Studio, you simply right-click the project and selecct build.
The *-configuration* parameter is optional. If left blank, the dotnet instruction will default to **Debug** build.

## Running the program
The program is fairly straight forward to run, here is it's output when run without parameters (or when -h or --help is invoked)

<pre>
        -i | --inputPattern <*.trsx>        - Specify File input pattern, absolute or relative
        -o | --outputFolder <folderSpec>    - Destination folder for the generated *.lu files
        -f | --filter "[INTENT1,INTENT2]"   - List of intents to ignore

Example:

        dotnet NuanceToLuis.dll -i *.trsx -o C:\temp\luDownFiles -f "NO_INTENT, GLOBAL_*"
</pre>

The above example takes all .trsx files in the current folder and produces corresponding
.lu files in the folder 'C:\temp\luDownFiles'. Intents named 'NO_INTENT' or intents
starting with 'GLOBAL_' will be ignored.

## Collaborating
If you'd like to collaborate on this project, simply reach out for an invite. The rules for collaboration are: 
- Branches should be named after your username, i.e. "**digitaldias/nuancetoluis_footbar**"
- Please follow [this guideline](https://chris.beams.io/posts/git-commit/) for commit messages. They matter!
- The master branch does not accept direct commits. All commits require a pull request. 


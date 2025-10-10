; -- sync.iss --

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!
#define SemanticVersion() \
   GetVersionComponents("..\VPTInterface\bin\Release\VPTInterface.exe", Local[0], Local[1], Local[2], Local[3]), \
   Str(Local[0]) + "." + Str(Local[1]) + ((Local[2]>0) ? "." + Str(Local[2]) : "")
    

#define verStr_ StringChange(SemanticVersion(), '.', '-')

[Setup]
AppName=Jenks VPT Interface
AppVerName=Jenks VPT Interface V{#SemanticVersion()}
DefaultDirName={commonpf64}\Jenks\VPT\V{#SemanticVersion()}
OutputDir=Output
DefaultGroupName=Jenks
AllowNoIcons=yes
OutputBaseFilename=VPT_Interface_{#verStr_}
UsePreviousAppDir=no
UsePreviousGroup=no
UsePreviousSetupType=no
DisableProgramGroupPage=yes
PrivilegesRequired=admin

[Dirs]
Name: "{commondocs}\Jenks\VPT\Images";

[Files]
;Source: "..\HTS Controller\Images\HTS.ico"; DestDir: "{app}"; Flags: replacesameversion;
Source: "..\VPTInterface\bin\Release\*.*"; DestDir: "{app}"; Flags: replacesameversion;
Source: "D:\Development\Jenks\jenks-vpt\Test\*.png"; DestDir: "{commondocs}\Jenks\VPT\Images"; Flags: onlyifdoesntexist;
;Source: "..\CHANGELOG.md"; DestDir: "{app}"; Flags: replacesameversion;
Source: "D:\Development\Jenks\jenks-vpt\VPT Interface\MATLAB\VPT_PTB_Interface\for_redistribution\VTP_PTB_Interface_Installer.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

[Icons]
;Name: "{commondesktop}\VPT Interface"; Filename: "{app}\VPTInterface.exe"; IconFilename: "{app}\HTS.ico"; IconIndex: 0;
Name: "{commondesktop}\VPT Interface"; Filename: "{app}\VPTInterface.exe";

[Run]
Filename: "{tmp}\VTP_PTB_Interface_Installer.exe"; WorkingDir: "{tmp}"; StatusMsg: "Installing prerequisite..."; Flags: waituntilterminated


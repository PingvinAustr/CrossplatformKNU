## Yaroslav Chushenko - Crossplatform KNU - NuGet packages usage guide 

- Step 1:
Create class library project (e.g 'lab3')
- Step 2: 
Create local nuget repo (e.g 'LocalNugetRepository')
- Step 3: Configure NuGet to recognize the local repository:
    - Open package manager console
    - Type *nuget sources Add -Name "LocalRepo" -Source <Path_to_LocalNugetRepository_directory>* and press enter.
- Step 4: Package class library project as nupkg:
    - Create .nuspec file. Example of content:
    ``` 
    <package>
    <metadata>
    <id>YChushenko</id>
    <version>1.0.5</version>
    <authors>Yaroslav Chushenko</authors>
    <owners>Yaroslav Chushenko</owners>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>Package for lab3 of Crossplatform discipline in KNU</description>
    <releaseNotes>First release</releaseNotes>
    </metadata>
    <files>
      <file src="bin\Debug\net6.0\lab3.dll" target="lib\netstandard2.0" />
    </files>
    </package>
  ```
  - Step 5: Build class library project and pack it using e.g `nuget pack YChushenko.nuspec`
  - Step 6: Publish the package to the local repository using e,g `nuget push YChushenko.1.0.5.nupkg -Source "LocalRepo"`
  - Step 7: Create console application to use recently created nuget package
  - Step 8: Go to 'Project' - 'Manage NuGet packages' - 'Browse'. Select needed package and click Install. Do not forget to click 'PackageSource'-'LocalRepo' in NuGet UI
  - Step 9: Use NuGet in Console application.
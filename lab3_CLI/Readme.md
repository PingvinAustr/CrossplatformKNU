## Інструкція щодо використання NuGet packages 

- Крок 1:
Створти бібліотеку класів (наприклад, 'lab3')
- Крок 2: 
Створити локальний репозиторій nuget (наприклад, 'LocalNugetRepository')
- Крок 3: Сконфігурувати NuGet для визнання локального репозиторію:
    - Відкрити консоль менеджера пакетів
    - Ввести *nuget sources Add -Name "LocalRepo" -Source <Path_to_LocalNugetRepository_directory>* та виконати цю команду.
- Крок 4: Запакувати проєкт в форматі nupkg:
    - Створти файл .nuspec. Приклад:
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
  - Крок 5: Виконати білд проекту та запакувати його командою `nuget pack YChushenko.nuspec`
  - Крок 6: Опубліковати пакет в локальний репозиторій командою `nuget push YChushenko.1.0.5.nupkg -Source "LocalRepo"`
  - Крок 7: Створити консольний застосунок для використання створенного nuget пакету
  - Крок 8: Перейти до 'Project' - 'Manage NuGet packages' - 'Browse'. Обрати потрібний пакет й натиснути "Встановити". Перед цим треба обов'язково вибрати в полі зі списком 'PackageSource'-'LocalRepo', щоб побачити потрібний пакет.
  - Крок 9: Використати NuGet в консольному застосунку.
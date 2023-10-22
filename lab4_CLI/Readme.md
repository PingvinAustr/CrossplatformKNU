#### Лабораторна робота #4 - Dotnet tool & Vagrant

1. Створимо 2 нових проекта – lab4_classes, lab4_CLI й додамо в консольний застосунок референс на бібліотеку класів
2. Змінимо lab4_CLI.csproj щоб пакувати застосунок як dotnet tool:
   ``` 
   <Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<AssemblyName>Lab4App</AssemblyName>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
		<PackAsTool>true</PackAsTool>
		<ToolCommandName>lab4chushenko</ToolCommandName>
		<Version>1.0.0</Version>
		<PackageOutputPath>./nupkg</PackageOutputPath>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="McMaster.Extensions.CommandLineUtils" Version="4.1.0" />
	</ItemGroup>

	<ItemGroup>
		<ProjectReference Include="..\lab4_classes\lab4_classes.csproj" />
	</ItemGroup>
	</Project> 
	```
3. Створимо приватний локальний репозиторій за допомогою Baget. Для цього створимо baget.env:
   ``` 
    ApiKey=yaroslavapikey
	Storage__Type=FileSystem
	Storage__Path=/var/baget/packages
	Database__Type=Sqlite
	Database__ConnectionString=Data Source=/var/baget/baget.db
	Search__Type=Database
	AllowPackageOverwrites=True
	PackageDeletionBehavior=HardDelete 
	```
4. Виконаємо запуск за допомогою `docker run --rm --name nuget-server -d -p 5555:80 --env-file baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest`
5. Запакуємо створений застосунок за допомогою `dotnet pack --configuration Release` й опублікуємо до приватного репозиторію за допомогою `dotnet nuget push -s http://localhost:5555/v3/index.json -k yaroslavapikey Lab4App.1.0.0.nupkg`
   Тепер застосунок можна встановити командою: 
   `dotnet tool install --global --version 1.0.0 --add-source http://localhost:5555/v3/index.json --no-cache Lab4App`
6. Створимо 3 Vagrant файли - Windows, Linux, MacOS (їх можна переглянути в поточному репозиторії в папці VagrantFiles)
7. Після вдалого розгортання віртуальних машин зі встановленим на них застосунком виконаємо тестування за допомогою `vagrant powershell` або ж безпосередньо через VM.

PS: Розширений опис кожного кроку додано до звіту з ЛР, що розташовано в поточному репозиторії.
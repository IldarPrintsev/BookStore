Pre-requisites
	.NET Core 3.1 SDK
	NPM

Run Application
	open Command Prompt
	cd BookStore
	dotnet build
	cd BookStore\BookStore.WEB
	dotnet run –project
	open browser
	search "localhost:5000"

Run Unit Tests
	open Command Prompt
	cd BookStore
	dotnet test

About Application
	You can login as an User and Admin:
		Admin: Sign in using email - admin, password - 123. Admin can change book catalogue per localhost:5000/admin
		User: Sign up as a new user
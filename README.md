# Sharee

![ShareeBanner](Repository/sharee_banner.png "ShareeBanner")

**Sharee** is a project developed using the _C#_ language and framework _ASP.NET The core_.
It is a web application for file sharing between configured databases.

## Features

- A simple and intuitive interface that makes it easy to set up databases for sharing
- Authentication and authorization of users on the **custom authorization service** via a token to ensure data security.
- Swagger is configured in the application for a quick inspection of the possibilities
- There is no binding to the type of data to be exchanged on the server
- Setting up a working folder, regardless of static files

## Technologies

The Share project is developed using the following technologies:

- C# programming language for backend application development.
- Framework ASP.NET Core for creating a web application.
- HTML, CSS, Bootstrap and JavaScript libraries for user interface development.
- SQL Server database for storing information about databases, files and folders.

## Installing and launching the project

1. Clone the repository using the command: 

    `git clone https://github.com/lowern1ght/Sharee.git `

2. Install the necessary dependencies by running the command:

    `dotnet restore`

3. Configure the database connection in the: `appsettings.json`.

4. Perform database migrations using the command: 

    `dotnet ef database update`

5. Run the project by running the command:

    `dotnet run`

## Contribution to the project

If you are interested in developing the Sharee project, you can contribute by creating new features, fixing bugs, or improving existing code. Just make a fork of the repository, make the necessary changes and send a pull request.

We are always happy to welcome new participants and are ready to consider your suggestions!

## License

The Share project is distributed under the MIT license. Detailed information about the 
license can be found in the `LICENSE` file.
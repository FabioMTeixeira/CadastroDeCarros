
# AppCar

É um site que te ajuda a organizar seus carros

## Instalação

- Fazer o fork do repositório para sua conta;
- Executar git clone do seu fork no terminal para clonar o repositório, ou clonar de outra maneira;
- Instalar Install-Package Microsoft.Data.SqlClient no seu terminar;
- Criar o banco de dados no sql server Management CREATE DATABASE ShopCar;
- E depois criar as 3 tabelas com: 
- CREATE TABLE Fuel (
    Id INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(50),
    Status NVARCHAR(50));
- CREATE TABLE Color (
    Id INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(50),
    Status NVARCHAR(50));
- CREATE TABLE Car (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Plate NVARCHAR(255) NOT NULL UNIQUE,
    Renavam NVARCHAR(255) NOT NULL UNIQUE,
    ChassisNumber NVARCHAR(255) NOT NULL UNIQUE,
    EngineNumber NVARCHAR(255) NOT NULL UNIQUE,
    Brand NVARCHAR(255) NOT NULL UNIQUE,
    Model NVARCHAR(255) NOT NULL UNIQUE,
    ColorId INT FOREIGN KEY REFERENCES Color(Id),
    FuelId INT FOREIGN KEY REFERENCES Fuel(Id)
    YearFactory NVARCHAR(255) NOT NULL UNIQUE,
    Status NVARCHAR(255) NOT NULL UNIQUE);
## Aprendizados

Desenvolvi um projeto utilizando C# e o padrão MVC, o que me proporcionou um aprimoramento significativo em programação e estruturação de projetos. A experiência com o SQL Server também foi enriquecedora, melhorando minhas habilidades em bancos de dados. O projeto foi essencial para o meu crescimento profissional e estou entusiasmado para aplicar o que aprendi em futuras iniciativas.

## Stack utilizada

**Fullstack:** HTML, CSS(bootstrap), C#





## Autores

- [@FabioMTeixeira](https://github.com/FabioMTeixeira)


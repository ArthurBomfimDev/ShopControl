# ShopControl - Sistema de Gestão para Lojas em C# .NET 8.0

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) ![C#](https://img.shields.io/badge/C%23-12-blue) ![MySQL](https://img.shields.io/badge/MySQL-8.0-orange) ![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-brightgreen) ![GitHub](https://img.shields.io/github/license/ArthurBomfimDev/ShopControl)

## 📖 Sobre o Projeto

`ShopControl` é um sistema de software robusto e extensível, desenvolvido em **C# com .NET 8.0**, para a gestão de operações de uma loja. O projeto implementa funcionalidades de **CRUD (Create, Read, Update, Delete)** para as entidades essenciais de qualquer negócio: **Produtos, Marcas, Clientes e Pedidos**.

O principal objetivo deste projeto é servir como um portfólio técnico, demonstrando a aplicação de `padrões de design` e `princípios de software` avançados, como a separação de responsabilidades (SoC), o uso de polimorfismo, e a implementação de padrões como Unit of Work para garantir a consistência dos dados.

A arquitetura foi pensada para ser limpa, de fácil manutenção e escalável, utilizando uma camada de serviços para encapsular as regras de negócio e validações, e o Entity Framework Core para uma comunicação eficiente com o banco de dados MySQL.

## ✨ Funcionalidades

O sistema oferece um controle completo sobre as seguintes entidades:

* **Produtos:** Cadastro, edição, exclusão e consulta de produtos, incluindo informações como nome, descrição, preço e estoque.
* **Marcas:** Gerenciamento das marcas associadas aos produtos.
* **Clientes:** Cadastro e manutenção da base de clientes da loja.
* **Pedidos:** Criação e acompanhamento de pedidos de venda, vinculando clientes e produtos.

## 🚀 Tecnologias e Conceitos Implementados

Este projeto foi construído com uma stack moderna e aplicando diversos conceitos avançados de engenharia de software para garantir qualidade e eficiência.

### Stack Tecnológica

* **Linguagem:** C# 12
* **Plataforma:** .NET 8.0
* **Banco de Dados:** MySQL 8.0
* **ORM:** Microsoft Entity Framework Core 8.0
* **Comunicação:** API (a ser especificado: REST, gRPC, etc.)

### Arquitetura e Padrões de Design

* **Separation of Concerns (SoC):** A lógica é dividida em camadas distintas (Apresentação, Serviço, Dados) para facilitar a manutenção e o desenvolvimento.
* **Services Layer:** Uma camada de serviços foi criada para conter toda a lógica de negócio e validações, desacoplando as regras da camada de acesso a dados e da interface do usuário.
* **Unit of Work:** Utilizado para agrupar uma ou mais operações de banco de dados em uma única transação, garantindo a atomicidade e a integridade dos dados.
* **Data Transfer Objects (DTOs):** DTOs são usados para transferir dados entre as camadas do sistema, evitando a exposição das entidades do domínio e otimizando a comunicação.
* **Sistema de Notificação:** Implementado um sistema de notificações para centralizar e gerenciar os feedbacks de validação e de operações, permitindo uma comunicação clara sobre o resultado das ações.

### Recursos Avançados de C# e .NET

* **Polimorfismo e Classes Base:** Utilização de herança e polimorfismo para criar componentes reutilizáveis e flexíveis, especialmente nas entidades e serviços.
* **LINQ (Language-Integrated Query):** Usado extensivamente para consultas complexas e manipulação de coleções de dados de forma declarativa e eficiente.
* **Reflection:** Empregado para inspecionar metadados em tempo de execução, permitindo a criação de funcionalidades genéricas e dinâmicas, como validações em lote.
* **Implicit Operators:** Operadores implícitos são utilizados para simplificar a conversão entre tipos, como DTOs e Entidades, tornando o código mais limpo e legível.
* **Operações de I/O:** Manipulação de arquivos e streams para funcionalidades como importação/exportação de dados ou geração de relatórios.
* **Validação em Lote:** Capacidade de validar múltiplos objetos ou regras de negócio em uma única operação, otimizando o desempenho e a resposta do sistema.

## ⚙️ Como Executar o Projeto

Siga os passos abaixo para configurar e executar o projeto em seu ambiente de desenvolvimento.

### Pré-requisitos

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Um servidor MySQL (local ou remoto)
* Uma IDE de sua preferência (Visual Studio 2022, VS Code, JetBrains Rider)

### Instalação

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/ArthurBomfimDev/ShopControl.git](https://github.com/ArthurBomfimDev/ShopControl.git)
    cd ShopControl
    ```

2.  **Configure a Conexão com o Banco de Dados:**
    * Abra o arquivo `appsettings.json` (ou `appsettings.Development.json`).
    * Localize a seção `ConnectionStrings`.
    * Altere a string de conexão para apontar para o seu servidor MySQL, informando `server`, `database`, `user` e `password`.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ShopControlDB;Uid=root;Pwd=sua_senha_aqui;"
    }
    ```

3.  **Aplique as Migrations do Entity Framework:**
    * Navegue até o projeto que contém o `DbContext` (geralmente o projeto de Infraestrutura/Dados).
    * Execute o seguinte comando no terminal para criar o banco de dados e as tabelas:
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    * Inicie o projeto principal (geralmente o projeto de API ou UI) através de sua IDE ou usando o comando:
    ```bash
    dotnet run
    ```

## 👨‍💻 Contribuição

Contribuições são sempre bem-vindas! Se você tem alguma ideia para melhorar o projeto, sinta-se à vontade para criar uma *issue* ou enviar um *pull request*.

1.  Faça um *Fork* do projeto
2.  Crie uma nova *Branch* (`git checkout -b feature/sua-feature`)
3.  Faça o *Commit* de suas mudanças (`git commit -m 'Adiciona nova feature'`)
4.  Faça o *Push* para a *Branch* (`git push origin feature/sua-feature`)
5.  Abra um *Pull Request*
---

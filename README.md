Abrir um console como administrador na pasta raiz da aplicação.

No console, execute: `docker-compose build`.

Depois de criar as imagens no seu Docker local.

Novamente no console, execute: `docker-compose up -d`.

Com este procedimento realizado, verificar se no seu docker local os containers estao up.

Por padrão, as portas das aplicações estão configuradas da seguinte maneira:

**FrontServices**: http://localhost:4200
**BackEndServices**: http://localhost:5000

Para o serviço do **backend**, também temos a possibilidade de acessar o Swagger com a seguinte URL: http://localhost:5000/swagger/index.html.

Caso necessário, as portas podem ser alteradas no arquivo docker-compose.yml.

O banco de dados roda em memória.

<h4>Arquitetura Escolhida para o projeto BackEnd</h4>

O backend é dividido em camadas: api, domínio, aplicação, infra e IoC (para injeção de dependência).

As requisições são baseadas em commands e queries, utilizando o pattern CQRS utilizando a biblioteca ***MediatR***.

Os testes estão divididos em: Testes de Controller, Testes específicos do handler e uma pasta chamada Builder que utiliza a biblioteca Faker para criação de objetos para teste quando necessário. Continuando nos testes, foi utilizada uma biblioteca chamada ***FluentAssertions*** para realizar as validações dos resultados.

Também temos disponível o ***Swagger*** da aplicação para auxiliar nos testes.

O acesso aos dados foi realizado com o ***Entity Framework Core*** de maneira simples para a atual proposta, porém sempre pensando na melhor disposição de classes e objetos, seguindo os princípios do SOLID.

<h4>Arquitetura Escolhida para o projeto FrontEnd</h4>

O frontend está dividido em camadas para evitar violações ao SOLID.

As chamadas são feitas via HTML para o BackEnd.

Foi utilizado um projeto padrão ***Angular*** com ***Bootstrap*** para simplificar e agilizar o desenvolvimento.

<h4>Consolidação</h4>

Para simular a criação de um serviço específico de consolidação, porém de maneira mais fácil, optei por criar um job utilizando uma biblioteca mais simples chamada ***Quartz*** (podendo ser facilmente substituída por uma ***function*** na nuvem, ou um job ***HangFire***, por exemplo).

A cada 2 minutos, esse job inicia o seu processamento e marca as entradas que ainda não estão consolidadas e coloca a data de hoje.
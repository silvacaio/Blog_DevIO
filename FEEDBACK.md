# Feedback do Instrutor

#### 14/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Separação de responsabilidades
- Demonstrou conhecimento em Identity e JWT.
- Mapeou bem entidades no EF e suas ViewModels
- Mostrou entendimento do ecossistema de desenvolvimento em .NET
- Documentou bem o repositório (faltou o Feedback.MD adicionei)

## Pontos Negativos:

- Foi utilizado um padrão "tipo" DDD sem necessidade, o projeto não exige tal complexidade.
     - As entidades estão anêmicas
     - Existe um forte acoplamento do Identity dentro do domínio (erro grave)
     - Se todo processo de leitura e escrita é feito por repositórios não tem necessidade de domínio.
- Não criou a entidade principal "Autor"
- O Identity e Autor deveriam ser a mesma coisa, porém deve existir a entidade Autor e estar ligada a um registro do Identity.
- Dada a complexidade do projeto seria possível ter os projetos de aplicação web e uma camada Core (application) servindo aos 2.
- Algumas controllers estão padrão scaffolding, não existe validação de controle se o usuário pode ou não editar tal post (se ele é o dono ou um admin).
- O projeto não aparenta estar completo ainda.

## Sugestões:

- Unificar a criação do user + autor no mesmo processo. Utilize o ID do registro do Identity como o ID da PK do Autor, assim você mantém um link lógico entre os elementos.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.

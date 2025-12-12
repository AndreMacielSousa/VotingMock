# VotingSystemClients

Este repositório contém duas aplicações cliente desenvolvidas em **C# (.NET 8)**, concebidas para testar serviços **gRPC** no contexto do sistema de votação eletrónica definido na unidade curricular **Integração de Sistemas** da Universidade Aberta.

As aplicações cliente interagem com um servidor de mock que simula os serviços da Entidade de Votação, disponibilizado no seguinte repositório:

➡️ https://github.com/AndreMacielSousa/VotingMock

Embora o enunciado do projeto distinga conceptualmente a **Entidade de Registo (AR)** e a **Entidade de Votação (AV)**, a versão atual do mock disponibilizado implementa apenas os serviços associados à AV. Ainda assim, ambos os clientes foram desenvolvidos, cumprindo os requisitos funcionais da atividade.

---

## 📁 Estrutura do repositório

VotingSystemClients/

├── VoterClient/ # Cliente da Entidade de Registo (AR)

├── VotingClient/ # Cliente da Entidade de Votação (AV)

├── Protos/

│ ├── voter.proto # Contrato IssueVotingCredential

│ └── voting.proto # Contratos GetCandidates e Vote

└── README.md


Cada projeto referencia explicitamente o respetivo ficheiro `.proto` no seu ficheiro `.csproj`, permitindo a geração automática dos *stubs* gRPC durante o processo de compilação.

---

## 🛠️ 1. Pré-requisitos

Para executar as aplicações cliente é necessário:

- **.NET SDK 8.0** ou superior  
- Ferramenta `grpcurl` (opcional, para testes manuais aos serviços)  
- Servidor de mock **VotingMock** em execução  

---

## 🚀 2. Levantar o servidor de mock

Numa consola independente, executar:

```bash
git clone https://github.com/AndreMacielSousa/VotingMock.git
cd VotingMock
dotnet run
```
Após o arranque, o servidor ficará disponível em:
```bash
http://0.0.0.0:9091
```
## 🚀 3. Compilar este repositório

Numa nova consola:
```bash
git clone https://github.com/AndreMacielSousa/VotingSystemClients.git
cd VotingSystemClients
dotnet build
http://0.0.0.0:9091
```
## 🧩 4. Executar o VoterClient (Entidade de Registo)

⚠️ Nota
O mock atualmente disponível não implementa o serviço VoterRegistrationService.
O cliente foi desenvolvido de acordo com o contrato definido no enunciado e encontra-se preparado para funcionar assim que um servidor compatível seja disponibilizado.

Para executar:
```bash
dotnet run --project VoterClient
```

Fluxo da aplicação

1. Introdução do número de Cartão de Cidadão

2. Construção do pedido IssueVotingCredentialRequest

3. Apresentação da resposta devolvida pelo serviço (voting_credential, message)

## 🧩 5. Executar o VotingClient (Entidade de Votação)

Para executar:
```bash
dotnet run --project VotingClient
```
Fluxo da aplicação

1. Invocação do método GetCandidates, com apresentação da lista de candidatos

2. Solicitação ao utilizador de:

  * credencial de voto

  * identificador do candidato

3. Invocação do método Vote e apresentação da mensagem devolvida pelo servidor

## 🧪 6. Testes com grpcurl

6.1. Obter lista de candidatos

```bash
grpcurl -plaintext -proto Protos/voting.proto \
  localhost:9091 \
  voting.VotingService/GetCandidates
```
Exemplo de resposta:
```bash
{
  "candidates": [
    { "id": 1, "name": "Andre" },
    { "id": 2, "name": "Bruno" },
    { "id": 3, "name": "Carlos" }
  ]
}
```
6.2. Submeter voto
```bash
grpcurl -plaintext -proto Protos/voting.proto \
  -d "{\"voting_credential\": \"TESTE\", \"candidate_id\": 1}" \
  localhost:9091 \
  voting.VotingService/Vote
```
Exemplo de resposta:

```bash
{
  "message": "Credential already used."
}
```
Estes testes permitem validar o comportamento do servidor antes da execução dos mesmos casos de uso através das aplicações cliente desenvolvidas em C#.

## 📚 7. Licença

Código disponibilizado exclusivamente para fins académicos no âmbito da unidade curricular Integração de Sistemas, Universidade Aberta.



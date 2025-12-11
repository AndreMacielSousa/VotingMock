# VotingMock

Este repositório contém um servidor **gRPC de mock**, utilizado para testar as aplicações cliente desenvolvidas no âmbito do projeto de votação eletrónica da unidade curricular **Integração de Sistemas** da Universidade Aberta.

O mock implementa os serviços fundamentais da **Entidade de Votação (AV)**, permitindo validar chamadas gRPC dos clientes.

O repositório dos clientes encontra-se em:

➡️ https://github.com/AndreMacielSousa/VotingSystemClients

---

## 📁 Estrutura do repositório

VotingMock/
├── Program.cs / Startup.cs # Arranque do servidor gRPC
├── Services/ # Implementação dos serviços de votação
├── Properties/launchSettings.json
└── README.md

--

## 🛠️ 1. Pré-requisitos

- .NET SDK **8.0** ou superior

---

## 🚀 2. Executar o servidor


git clone https://github.com/AndreMacielSousa/VotingMock.git
cd VotingMock
dotnet run
O servidor arranca por omissão em:

http://0.0.0.0:9091


Deve ser mantido a correr numa consola dedicada durante os testes dos clientes.

## 3. Serviços gRPC expostos
🔷 VotingService

GetCandidates
Devolve a lista de candidatos disponíveis.

Vote(VoteRequest)
Recebe:

voting_credential

candidate_id
E devolve uma mensagem textual com o resultado, por exemplo:

"Credential already used."


⚠️ Nota
A Entidade de Registo (VoterRegistrationService) não está incluída neste mock.
O cliente correspondente poderá ser testado apenas quando existir servidor compatível.

## 4. Testes com grpcurl
4.1. Consultar candidatos
grpcurl -plaintext -proto ../VotingSystemClients/Protos/voting.proto \
  localhost:9091 \
  voting.VotingService/GetCandidates

4.2. Submeter voto
grpcurl -plaintext -proto ../VotingSystemClients/Protos/voting.proto \
  -d "{\"voting_credential\": \"TESTE\", \"candidate_id\": 1}" \
  localhost:9091 \
  voting.VotingService/Vote

## 🤝 5. Utilização prevista

Este mock é destinado exclusivamente a fins académicos.
Não implementa:

autenticação

criptografia

persistência fiável

qualquer mecanismo de segurança associado a sistemas reais de votação.

## 📚 6. Licença

Código disponibilizado exclusivamente para fins pedagógicos no contexto da Universidade Aberta.

VotingMock – Serviço gRPC de Registo e Votação (Mock)

Este repositório contém uma implementação mock dos serviços de Registo e Votação utilizados no projeto Voting System, desenvolvido no âmbito da unidade curricular Integração de Sistemas do Mestrado em Engenharia Informática e Tecnologia Web (MEIW).

Os serviços originais disponibilizados pela UTAD não se encontravam operacionalmente acessíveis no momento da realização da atividade, pelo que foi desenvolvido este servidor alternativo, compatível com os ficheiros voting.proto e voter.proto, permitindo:

testar a interação via grpcurl;

validar a integração cliente-servidor;

desenvolver aplicações cliente gRPC em C# ou outras linguagens;

disponibilizar um endpoint público através do Render.

🧩 1. Estrutura do Mock

O servidor implementa os seguintes serviços gRPC definidos nos .proto:

VotingService (voting.proto – namespace VotingSystem.Voting)

GetCandidates

Vote

GetResults

VoterRegistrationService (voter.proto – namespace VotingSystem)

IssueVotingCredential

Ambos são expostos dentro do mesmo servidor gRPC para simplificar o processo de deploy e testes.

📦 2. Requisitos

.NET SDK 8.0 ou superior
https://dotnet.microsoft.com/download

grpcurl
https://github.com/fullstorydev/grpcurl

Editor recomendado: Visual Studio Code

▶️ 3. Execução Local
3.1 Restaurar dependências
dotnet restore

3.2 Executar o servidor (localhost)
dotnet run --urls http://localhost:9091


O servidor ficará ativo em:

http://localhost:9091

🔬 4. Testes com grpcurl (local)

Colocar os ficheiros .proto numa pasta acessível, por exemplo:

C:\Protos\
   voting.proto
   voter.proto

4.1 Obter candidatos
grpcurl -plaintext -import-path "C:\Protos" -proto voting.proto \
  localhost:9091 voting.VotingService/GetCandidates

4.2 Emitir credencial de voto
grpcurl -plaintext -import-path "C:\Protos" -proto voter.proto \
  -d "{ \"citizen_card_number\": \"123456789\" }" \
  localhost:9091 voting.VoterRegistrationService/IssueVotingCredential

4.3 Submeter voto
grpcurl -plaintext -import-path "C:\Protos" -proto voting.proto \
  -d "{ \"voting_credential\": \"CRED-AAA-111\", \"candidate_id\": 1 }" \
  localhost:9091 voting.VotingService/Vote

4.4 Obter resultados
grpcurl -plaintext -import-path "C:\Protos" -proto voting.proto \
  localhost:9091 voting.VotingService/GetResults

🐳 5. Publicação no Render (Deploy com Docker)

O mock inclui um Dockerfile compatível com Render.

5.1 Criar imagem & publicar

Criar repositório no GitHub.

Fazer push do projeto:

git init
git add .
git commit -m "Initial mock gRPC service"
git remote add origin https://github.com/<user>/VotingMock.git
git push -u origin main

5.2 Deploy no Render

Aceder a https://render.com

New → Web Service

Escolher o repositório VotingMock

Configurar:

Environment: Docker

Instance Type: Free

Region: Frankfurt

Root Directory: (raiz do projeto)

Build Command: (deixar vazio)

Start Command: (Render usa ENTRYPOINT do Dockerfile)

Criar serviço

Após o deploy, será disponibilizado um domínio do tipo:

https://votingmock.onrender.com

🌍 6. Testes com grpcurl após o deploy (Render)

Render usa HTTPS, logo:

remover -plaintext

adicionar -insecure (se o certificado não for trusted)

Exemplo:
grpcurl -insecure -import-path "C:\Protos" -proto voting.proto \
  votingmock.onrender.com:443 voting.VotingService/GetCandidates

🛠️ 7. Implementação dos Serviços Mock
Principais comportamentos:
MockRegistrationService

70% das requisições devolvem credenciais válidas

30% devolvem credenciais inválidas (INVALID-*)

MockVotingService

Lista fixa de três candidatos

Contabilização simples em memória

Prevenção de voto duplicado por credencial

Resultados acumulativos enquanto o servidor estiver em execução

🧪 8. Considerações Técnicas

Este mock não persiste dados em base de dados.

O estado (votos, credenciais usadas) é reiniciado sempre que o serviço reinicia.

A implementação tem como objetivo suportar testes de integração e não ambientes de produção.

📚 9. Referências (ABNT)

MICROSOFT. Documentação oficial do .NET. Disponível em: https://dotnet.microsoft.com/
. Acesso em: 12 dez. 2025.

FULLSTORY DEV. grpcurl – Command-line tool for interacting with gRPC servers. Disponível em: https://github.com/fullstorydev/grpcurl
. Acesso em: 12 dez. 2025.

REIS, Arsénio. VotingSystem (repositório GitHub). Disponível em: https://github.com/arsenioreis/VotingSystem
. Acesso em: 12 dez. 2025.

UNIVERSIDADE DE TRÁS-OS-MONTES E ALTO DOURO. Integração de Sistemas — Enunciado do Projeto Voting System. Vila Real, 2025.
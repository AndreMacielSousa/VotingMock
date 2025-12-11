using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using VotingSystem.Voting;

public class MockVotingService : VotingService.VotingServiceBase
{
    private static readonly List<CandidateResult> Results = new()
    {
        new CandidateResult { Id = 1, Name = "Andre", Votes = 0 },
        new CandidateResult { Id = 2, Name = "Bruno",   Votes = 0 },
        new CandidateResult { Id = 3, Name = "Carlos", Votes = 0 }
    };

    private static readonly HashSet<string> UsedCredentials = new();

    public override Task<GetCandidatesResponse> GetCandidates(GetCandidatesRequest request, ServerCallContext context)
    {
        var response = new GetCandidatesResponse();
        response.Candidates.AddRange(
            Results.Select(r => new Candidate
            {
                Id = r.Id,
                Name = r.Name
            })
        );
        return Task.FromResult(response);
    }

    public override Task<VoteResponse> Vote(VoteRequest request, ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.VotingCredential))
            return Task.FromResult(new VoteResponse { Success = false, Message = "Missing credential." });

        if (UsedCredentials.Contains(request.VotingCredential))
            return Task.FromResult(new VoteResponse { Success = false, Message = "Credential already used." });

        UsedCredentials.Add(request.VotingCredential);

        var candidate = Results.FirstOrDefault(c => c.Id == request.CandidateId);
        if (candidate == null)
            return Task.FromResult(new VoteResponse { Success = false, Message = "Invalid candidate." });

        candidate.Votes++;

        return Task.FromResult(new VoteResponse { Success = true, Message = "Vote accepted." });
    }

    public override Task<GetResultsResponse> GetResults(GetResultsRequest request, ServerCallContext context)
    {
        var response = new GetResultsResponse();
        response.Results.AddRange(Results);
        return Task.FromResult(response);
    }
}

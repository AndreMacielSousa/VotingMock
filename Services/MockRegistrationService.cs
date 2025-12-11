using System;
using System.Threading.Tasks;
using Grpc.Core;
using VotingSystem;

public class MockRegistrationService : VoterRegistrationService.VoterRegistrationServiceBase
{
    private static readonly Random Rng = new();

    private static readonly string[] ValidCredentials =
    {
        "CRED-AAA-111",
        "CRED-BBB-222",
        "CRED-CCC-333"
    };

    public override Task<VoterResponse> IssueVotingCredential(VoterRequest request, ServerCallContext context)
    {
        bool issueValid = Rng.Next(0, 100) < 70;

        return Task.FromResult(new VoterResponse
        {
            IsEligible = true,
            VotingCredential = issueValid
                ? ValidCredentials[Rng.Next(ValidCredentials.Length)]
                : $"INVALID-{Guid.NewGuid():N}".Substring(0, 12)
        });
    }
}

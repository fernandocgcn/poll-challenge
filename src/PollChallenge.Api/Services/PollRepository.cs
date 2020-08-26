using PollChallenge.Model.Entities;
using System.Threading.Tasks;

namespace PollChallenge.Api.Services
{
    public interface PollRepository
    {
        Task<Poll> GetPollAsync(int id);
        void IncrementViewsQty(Poll poll);
        void AddNewPoll(Poll poll);
        Task<Option> GetOptionAsync(int pollId, int optionId);
        void IncrementVotesQty(Option option);
    }
}

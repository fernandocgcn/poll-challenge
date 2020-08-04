using Microsoft.EntityFrameworkCore;
using PollChallenge.Model.Data;
using PollChallenge.Model.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollChallenge.Api.Services
{
    public class PollSrv : IPollSrv
    {
        private readonly PollDbContext _pollDbContext;

        public PollSrv(PollDbContext pollDbContext)
            => _pollDbContext = pollDbContext;

        public async Task<Poll> GetPollAsync(int id)
            => await _pollDbContext.Polls
                .Include(poll => poll.Options)
                .Where(poll => poll.Id == id)
                .FirstAsync();

        public void IncrementViewsQty(Poll poll)
        {
            poll.ViewsQty++;
            _pollDbContext.Polls.Update(poll);
            _pollDbContext.SaveChangesAsync();
        }

        public void AddNewPoll(Poll poll)
        {
            if (poll?.Options is null || poll?.Options?.Count == 0)
                throw new ArgumentNullException(nameof(poll.Options));
            _pollDbContext.Polls.AddAsync(poll);
            _pollDbContext.SaveChangesAsync();
        }

        public async Task<Option> GetOptionAsync(int pollId, int optionId)
            => await _pollDbContext.Options
                .Where(option => option.Poll.Id == pollId && option.Id == optionId)
                .FirstAsync();

        public void IncrementVotesQty(Option option)
        {
            option.VotesQty++;
            _pollDbContext.Options.Update(option);
            _pollDbContext.SaveChangesAsync();
        }
    }
}

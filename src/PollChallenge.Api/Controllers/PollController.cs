using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PollChallenge.Api.Services;
using PollChallenge.Api.ViewModels;
using PollChallenge.Model.Entities;
using System;
using System.Threading.Tasks;

namespace PollChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollController : ControllerBase
    {
        private readonly PollRepository _pollRepository;
        private readonly IMapper _mapper;

        public PollController(PollRepository pollSrv, IMapper mapper)
        {
            _pollRepository = pollSrv;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PollGetVM>> GetPoll(int pollId)
        {
            try
            {
                var poll = await _pollRepository.GetPollAsync(pollId);
                _pollRepository.IncrementViewsQty(poll);
                return Ok(_mapper.Map<PollGetVM>(poll));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<object> PostPoll([FromBody] PollPostVM pollPostVM)
        {
            try
            {
                var poll = _mapper.Map<Poll>(pollPostVM);
                _pollRepository.AddNewPoll(poll);
                return CreatedAtAction(nameof(GetPoll), 
                    new { id = poll.Id }, new { poll_id = poll.Id });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("{id}/vote")]
        public async Task<ActionResult> PostVote(int pollId, [FromBody] VotePostVM votePostVM)
        {
            try
            {
                var option = await _pollRepository.GetOptionAsync(pollId, votePostVM.Id);
                _pollRepository.IncrementVotesQty(option);
                return CreatedAtAction(nameof(GetStats), new { pollId },
                    new { option_id = votePostVM.Id });
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<StatGetVM>> GetStats(int pollId)
        {
            try
            {
                var poll = await _pollRepository.GetPollAsync(pollId);
                return Ok(_mapper.Map<StatGetVM>(poll));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}

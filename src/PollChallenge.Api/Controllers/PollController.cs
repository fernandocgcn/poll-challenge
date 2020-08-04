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
        private readonly IPollSrv _pollSrv;
        private readonly IMapper _mapper;

        public PollController(IPollSrv pollSrv, IMapper mapper)
        {
            _pollSrv = pollSrv;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PollGetVM>> GetPoll(int id)
        {
            try
            {
                var poll = await _pollSrv.GetPollAsync(id);
                _pollSrv.IncrementViewsQty(poll);
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
                _pollSrv.AddNewPoll(poll);
                return CreatedAtAction(nameof(GetPoll), 
                    new { id = poll.Id }, new { poll_id = poll.Id });
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("{id}/vote")]
        public async Task<ActionResult> PostVote(int id, [FromBody] VotePostVM votePostVM)
        {
            try
            {
                var option = await _pollSrv.GetOptionAsync(id, votePostVM.Id);
                _pollSrv.IncrementVotesQty(option);
                return CreatedAtAction(nameof(GetStats), new { id },
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
        public async Task<ActionResult<StatGetVM>> GetStats(int id)
        {
            try
            {
                var poll = await _pollSrv.GetPollAsync(id);
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

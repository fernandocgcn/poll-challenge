using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollChallenge.Api.Controllers;
using PollChallenge.Api.Services;
using PollChallenge.Api.ViewModels;
using PollChallenge.Tests.Mocks;
using System.Linq;
using System.Threading.Tasks;

namespace PollChallenge.Tests.Controllers
{
    [TestClass]
    public class PollControllerTest : ServicesMock
    {
        private readonly IPollSrv _pollSrv;
        private readonly PollController _pollController;

        public PollControllerTest()
        {
            _pollSrv = new PollSrv(_dbContext);
            _pollController = new PollController(_pollSrv, _mapper);
        }

        [TestMethod]
        public async Task GetPoll_ShouldNotFindPoll()
        {
            // Arrange
            CreatePoll();

            // Act
            var result = await _pollController.GetPoll(int.MaxValue);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetPoll_ShouldReturnCorrectPoll()
        {
            // Arrange
            var expected = CreatePoll();

            // Act
            var result = await _pollController.GetPoll(expected.Id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(_mapper.Map<PollGetVM>(expected), ((OkObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task GetPoll_ShouldAddOneToViewsQty()
        {
            // Arrange
            var expected = CreatePoll();

            // Act
            await _pollController.GetPoll(expected.Id);
            await _pollController.GetPoll(expected.Id);
            await _pollController.GetPoll(expected.Id);

            // Assert
            Assert.AreEqual(expected.ViewsQty, 3L);
        }

        [TestMethod]
        public void PostPoll_ShouldReturnCorrectObject()
        {
            // Arrange
            var pollPostVm = new PollPostVM()
            {
                Description = "Poll",
                Options = new string[] { "Option1", "Option2" }
            };

            // Act
            var result1 = _pollController.PostPoll(pollPostVm);
            var result2 = _pollController.PostPoll(pollPostVm);

            // Assert
            Assert.IsInstanceOfType(result1.Result, typeof(CreatedAtActionResult));
            Assert.IsInstanceOfType(result2.Result, typeof(CreatedAtActionResult));
            var expected1 = ((CreatedAtActionResult)result1.Result).Value;
            var expected2 = ((CreatedAtActionResult)result2.Result).Value;
            Assert.AreEqual(1, expected1.GetType().GetProperty("poll_id").GetValue(expected1));
            Assert.AreEqual(2, expected2.GetType().GetProperty("poll_id").GetValue(expected2));
        }

        [TestMethod]
        public async Task PostVote_ShouldNotFindOption()
        {
            // Arrange
            var poll = CreatePoll();
            var votePostVM = new VotePostVM { Id = int.MaxValue };

            // Act
            var result = await _pollController.PostVote(poll.Id, votePostVM);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostVote_ShouldAddOneToVotesQty()
        {
            // Arrange
            var poll = CreatePoll();
            var option = poll.Options.First();
            var votePostVM = new VotePostVM { Id = option.Id };

            // Act
            await _pollController.PostVote(poll.Id, votePostVM);
            await _pollController.PostVote(poll.Id, votePostVM);
            await _pollController.PostVote(poll.Id, votePostVM);

            // Assert
            Assert.AreEqual(option.VotesQty, 3L);
        }

        [TestMethod]
        public async Task GetStats_ShouldReturnCorrectStats()
        {
            // Arrange
            var expected = CreatePoll();

            // Act
            var result = await _pollController.GetStats(expected.Id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(_mapper.Map<StatGetVM>(expected), ((OkObjectResult)result.Result).Value);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollChallenge.Api.Services;
using PollChallenge.Model.Entities;
using PollChallenge.Tests.Mocks;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollChallenge.Tests.Services
{
    [TestClass]
    public class PollRepositoryTest : ServicesMock
    {
        private readonly PollRepository _pollRepository;

        public PollRepositoryTest()
            => _pollRepository = new PollRepositoryImp(_dbContext);

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetPollAsync_ShouldNotFindPoll()
        {
            // Arrange
            CreatePoll();

            // Act
            await _pollRepository.GetPollAsync(int.MaxValue);
        }

        [TestMethod]
        public async Task GetPollAsync_ShouldReturnCorrectPoll()
        {
            // Arrange
            var expected = CreatePoll();

            // Act
            var result = await _pollRepository.GetPollAsync(expected.Id);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IncrementViewsQtyAsync_ShouldAddOneToViewsQty()
        {
            // Arrange
            var expected = CreatePoll();

            // Act
            _pollRepository.IncrementViewsQty(expected);
            _pollRepository.IncrementViewsQty(expected);
            _pollRepository.IncrementViewsQty(expected);

            // Assert
            Assert.AreEqual(expected.ViewsQty, 3L);
        }

        [TestMethod]
        public void AddNewPoll_ShouldNotAddPollWithoutOptions()
        {
            // Arrange
            var poll = new Poll() { Description = "Poll" };

            // Act - Assert
            Assert.ThrowsException<ArgumentNullException>(() => _pollRepository.AddNewPoll(poll));
        }

        [TestMethod]
        public void AddNewPoll_ShouldIncrementNewId()
        {
            // Arrange
            var expected1 = CreatePoll(false);
            var expected2 = CreatePoll(false);

            // Act
            _pollRepository.AddNewPoll(expected1);
            _pollRepository.AddNewPoll(expected2);

            // Assert
            Assert.AreEqual(expected1.Id, 1);
            Assert.AreEqual(expected2.Id, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetOptionAsync_ShouldNotFindOption()
        {
            // Arrange
            CreatePoll();

            // Act
            await _pollRepository.GetOptionAsync(int.MaxValue, int.MaxValue);
        }

        [TestMethod]
        public async Task GetOptionAsync_ShouldReturnCorrectOption()
        {
            // Arrange
            var poll = CreatePoll();
            var expected = poll.Options.First();

            // Act
            var result = await _pollRepository.GetOptionAsync(poll.Id, expected.Id);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IncrementVotesQty_ShouldAddOneToVotesQty()
        {
            // Arrange
            var poll = CreatePoll();
            var expected = poll.Options.ToArray();

            // Act
            _pollRepository.IncrementVotesQty(expected[0]);
            _pollRepository.IncrementVotesQty(expected[1]);
            _pollRepository.IncrementVotesQty(expected[0]);

            // Assert
            Assert.AreEqual(expected[0].VotesQty, 2L);
            Assert.AreEqual(expected[1].VotesQty, 1L);
        }
    }
}

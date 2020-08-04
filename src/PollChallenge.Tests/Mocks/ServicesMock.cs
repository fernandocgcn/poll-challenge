using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollChallenge.Api.ViewModels;
using PollChallenge.Model.Data;
using PollChallenge.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PollChallenge.Tests.Mocks
{
    [TestClass]
    public class ServicesMock
    {
        protected readonly PollDbContext _dbContext;
        protected readonly IMapper _mapper;

        public ServicesMock()
        {
            var _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<PollDbContext>()
                    .UseSqlite(_connection)
                    .Options;

            _dbContext = new PollDbContext(options);
            _dbContext.Database.EnsureCreated();

            _mapper = CreateMapper();
        }

        protected Poll CreatePoll(bool attached = true)
        {
            var poll = new Poll() { Description = "Poll" };
            poll.Options = new List<Option>()
            {
                new Option() { Id = 1, Description = "Option1" },
                new Option() { Id = 2, Description = "Option2" }
            };
            if (attached)
            {
                _dbContext.AddAsync(poll);
                _dbContext.SaveChangesAsync();
            }
            return poll;
        }

        private IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Option, OptionGetVM>();
                cfg.CreateMap<Poll, PollGetVM>();

                cfg.CreateMap<PollPostVM, Poll>().ForMember(dst => dst.Options,
                        map => map.MapFrom(scr => scr.Options.Select
                        ((desc, index) => new Option { Id = index + 1, Description = desc })));

                cfg.CreateMap<Option, VoteGetVM>();
                cfg.CreateMap<Poll, StatGetVM>();
            });
            return mapperConfig.CreateMapper();
        }
    }
}

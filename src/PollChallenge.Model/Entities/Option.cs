using System;
using System.Collections.Generic;

namespace PollChallenge.Model.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public long VotesQty { get; set; }
        public Poll Poll { get; set; }

        public override bool Equals(object obj)
            => obj is Option option &&
                   Id == option.Id &&
                   EqualityComparer<Poll>.Default.Equals(Poll, option.Poll);
        public override int GetHashCode() => HashCode.Combine(Id, Poll);
    }
}

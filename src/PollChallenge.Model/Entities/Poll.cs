using System;
using System.Collections.Generic;

namespace PollChallenge.Model.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public long ViewsQty { get; set; }
        public ICollection<Option> Options { get; set; }

        public override bool Equals(object obj)
            => obj is Poll poll && Id == poll.Id;
        public override int GetHashCode() => HashCode.Combine(Id);
    }
}

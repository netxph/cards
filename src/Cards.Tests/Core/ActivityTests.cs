using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Cards.Core;

namespace Cards.Tests.Core
{
    public class ActivityTests
    {

        public class InitializeActivity : TestCase<Activity>
        {
            protected override Func<Activity> Given()
            {
                return () => new Activity();
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIsZero()
            {
                Its.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldCardIDIsZero()
            {
                Its.CardID.Should().Be(0);
            }

            [Fact]
            public void ShouldCardChangeTypeIsModify()
            {
                Its.ChangeType.Should().Be(CardChangeType.Modify);
            }

            [Fact]
            public void ShouldStampDateShouldBeDateMin()
            {
                Its.StampDate.Should().Be(DateTime.MinValue);
            }


        }


    }
}

using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Cards.Core;
using Moq;
using Moq.Protected;

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
            public void ShouldCurrentAreaIDIsZero()
            {
                Its.CurrentAreaID.Should().Be(0);
            }

            [Fact]
            public void ShouldCommentIsNull()
            {
                Its.Comment.Should().BeNull();
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

        public class CreateMethod : TestCase<Activity>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Activity> Given()
            {
                var dateProvider = new Mock<IDateProvider>();
                dateProvider
                    .Setup(dp => dp.UtcNow())
                    .Returns(NOW);

                Activity.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();

                Activity activity = null;
                repository
                    .Setup(r => r.CreateActivity(It.IsAny<Activity>()))
                    .Callback<Activity>(a => activity = a)
                    .Returns(() => activity);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Activity.Create(1, 1, CardChangeType.Modify, "with comments");
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldCardIDHasValue()
            {
                Its.CardID.Should().Be(1);
            }

            [Fact]
            public void ShouldCurrentAreaIDHasValue()
            {
                Its.CurrentAreaID.Should().Be(1);
            }

            [Fact]
            public void ShouldChangeTypeIdModify()
            {
                Its.ChangeType.Should().Be(CardChangeType.Modify);
            }

            [Fact]
            public void ShouldCommentHasValue()
            {
                Its.Comment.Should().Be("with comments");
            }

            [Fact]
            public void ShouldStampDateIsNow()
            {
                Its.StampDate.Should().Be(NOW);
            }

        }

    }
}

using Cards.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.ComponentModel.DataAnnotations;

namespace Cards.Tests.Core
{
    public class CardTests
    {

        public class InitializeCard : TestCase<Card>
        {
            protected override Card Given()
            {
                var card = new Card();

                return card;
            }

            [Fact]
            public void ShouldIDIsZero()
            {
                Subject.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldNameIsNull()
            {
                Subject.Name.Should().BeNull();
            }

            [Fact]
            public void ShouldAreaIsNull()
            {
                Subject.Area.Should().BeNull();
            }
        }

        public class CreateMethod : TestCase<Card>
        {

            protected override Card Given()
            {
                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Cards = new FakeDbSet<Card>() });

                new DbFactory(factory.Object);

                return Card.Create("A sample task");
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsASampleTask()
            {
                Subject.Name.Should().Be("A sample task");
            }
        }

        public class CreateMethod_Invalid : TestCase<Action>
        {
            protected override Action Given()
            {
                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Cards = new FakeDbSet<Card>() });

                new DbFactory(factory.Object);

                return () => Card.Create("");
            }

            [Fact]
            public void ShouldThrowValidationError()
            {
                Subject.ShouldThrow<ValidationException>();
            }
        }



    }
}

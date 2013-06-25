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
using System.Data.Entity.Validation;

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
                Subject.AreaID.Should().Be(0);
            }
        }

        public class UpdateMethod : TestCase<Card>
        {

            protected override Card Given()
            {
                var cards = new FakeDbSet<Card>();
                cards.Add(new Card()
                {
                    ID = 1,
                    Name = "Original task",
                    AreaID = 1
                });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Cards = cards });

                new DbFactory(factory.Object);

                return Card.Update(1, "Updated task", 2);
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIs1()
            {
                Subject.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameIsUpdated()
            {
                Subject.Name.Should().Be("Updated task");
            }

            [Fact]
            public void ShouldAreaIs2()
            {
                Subject.AreaID.Should().Be(2);
            }
        }

        public class UpdateMethod_Invalid : TestCase
        {
            protected override void Initialize()
            {
                var cards = new FakeDbSet<Card>();
                cards.Add(new Card()
                {
                    ID = 1,
                    Name = "Original task",
                    AreaID = 1
                });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Cards = cards });

                new DbFactory(factory.Object);
            }

            [Fact]
            public void ShouldReturnNullIfItemIsNotFound()
            {
                var card = Card.Update(99, "Not exist", 1);
                card.Should().BeNull();
            }

            [Fact]
            public void ShouldThrowValidationExceptionIfNameIsEmptyOrNull()
            {
                Action updateCard = () => Card.Update(1, string.Empty, 1);
                updateCard.ShouldThrow<DbEntityValidationException>();
            }

            [Fact]
            public void ShouldThrowValidationExceptionIfAreaIsMissing()
            {
                Action updateCard = () => Card.Update(1, "Valid", 0);
                updateCard.ShouldThrow<DbEntityValidationException>();
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

                return Card.Create("A sample task", 1);
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

        public class CreateMethod_Invalid : TestCase
        {
            protected override void Initialize()
            {
                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Cards = new FakeDbSet<Card>() });

                new DbFactory(factory.Object);
            }

            [Fact]
            public void ShouldThrowValidationErrorWhenNameIsEmpty()
            {
                Action createCard = () => Card.Create(string.Empty, 1);

                createCard.ShouldThrow<ValidationException>();
            }

            [Fact]
            public void ShouldThrowValidationErrorWhenAreaIsMissing()
            {
                Action createCard = () => Card.Create("Test", 0);

                createCard.ShouldThrow<ValidationException>();
            }
        }

    }
}

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
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Card Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

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

            [Fact]
            public void ShouldCreatedIsNow()
            {
                Subject.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Subject.IsActive.Should().BeTrue();
            }
        }

        public class UpdateMethod : TestCase<Card>
        {

            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Card Given()
            {
                var card = new Card()
                {
                    ID = 1,
                    Name = "Card",
                    AreaID = 1,
                    ModifiedDateUtc = DateTime.MinValue,
                    CreatedDateUtc = DateTime.MinValue
                };

                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindCard(It.IsAny<int>()))
                    .Returns(card);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

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

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldCreatedDateNotIsNow()
            {
                Subject.CreatedDateUtc.Should().NotBe(NOW);
            }

        }

        public class UpdateMethod_Invalid : TestCase
        {
            protected override void Initialize()
            {
                var card = new Card()
                {
                    ID = 1,
                    Name = "Card",
                    AreaID = 1
                };

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.UpdateCard(It.IsAny<Card>()))
                    .Callback<Card>(c =>
                    {
                        Validator.ValidateObject(c, new ValidationContext(c), true);
                    });
                repository
                    .Setup(r => r.FindCard(1))
                    .Returns(card);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

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
                updateCard.ShouldThrow<ValidationException>();
            }

            [Fact]
            public void ShouldThrowValidationExceptionIfAreaIsMissing()
            {
                Action updateCard = () => Card.Update(1, "Valid", 0);
                updateCard.ShouldThrow<ValidationException>();
            }
        }

        public class DeleteMethod : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Card Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindCard(1))
                    .Returns(new Card()
                    {
                        ID = 1,
                        AreaID = 1,
                        CreatedDateUtc = DateTime.MinValue,
                        ModifiedDateUtc = DateTime.MinValue,
                        Name = "Card",
                        IsActive = true
                    });

                Card card = null;
                repository
                    .Setup(r => r.UpdateCard(It.IsAny<Card>()))
                    .Callback<Card>(c => card = c)
                    .Returns(() => card);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return Card.Delete(1);
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeActive()
            {
                Subject.IsActive.Should().BeFalse();
            }

            [Fact]
            public void ShouldIdIs1()
            {
                Subject.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Subject.Name.Should().Be("Card");
            }

            [Fact]
            public void ShouldCreatedDateIsNotNow()
            {
                Subject.CreatedDateUtc.Should().NotBe(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }

        }


        public class CreateMethod : TestCase<Card>
        {

            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Card Given()
            {
                Card card = null;

                var dateProvider = new Mock<IDateProvider>();
                dateProvider
                    .Setup(dp => dp.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.CreateCard(It.IsAny<Card>()))
                    .Callback<Card>((c) => card = c)
                    .Returns(() => card);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return Card.Create("Card", 1);
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsASampleTask()
            {
                Subject.Name.Should().Be("Card");
            }

            [Fact]
            public void ShouldCreatedDateIsAfterMinValue()
            {
                Subject.CreatedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldCreatedDateIsNow()
            {
                Subject.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsAfterMinValue()
            {
                Subject.ModifiedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Subject.IsActive.Should().BeTrue();
            }
        }

        public class CreateMethod_Invalid : TestCase
        {
            protected override void Initialize()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.CreateCard(It.IsAny<Card>()))
                    .Callback<Card>(card =>
                    {
                        Validator.ValidateObject(card, new ValidationContext(card), true);
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

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

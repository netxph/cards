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
using Speculous;

namespace Cards.Tests.Core
{
    public class CardTests
    {

        public class InitializeCard : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Card> Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

                var card = new Card();

                return () => card;
            }

            [Fact]
            public void ShouldIDIsZero()
            {
                Its.ID.Should().Be(0);
            }

            [Fact]
            public void ShouldNameIsNull()
            {
                Its.Name.Should().BeNull();
            }

            [Fact]
            public void ShouldAreaIsNull()
            {
                Its.AreaID.Should().Be(0);
            }

            [Fact]
            public void ShouldCreatedIsNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

        }

        public class GetCard : TestCase<Card>
        {
            DateTime NOW = new DateTime(2013, 12, 1);
            DateTime DATE = new DateTime(2013, 11, 1);

            protected override Func<Card> Given()
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
                        Name = "Sample task",
                        AreaID = 1,
                        ModifiedDateUtc = DATE,
                        CreatedDateUtc = DATE
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Card.Get(1);
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDHasValue()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Sample task");
            }

            [Fact]
            public void ShouldAreaIDHasValue()
            {
                Its.AreaID.Should().Be(1);
            }

            [Fact]
            public void ShouldModifiedDateHasValue()
            {
                Its.ModifiedDateUtc.Should().Be(DATE);
            }

            [Fact]
            public void ShouldCreatedDateHasValue()
            {
                Its.CreatedDateUtc.Should().Be(DATE);
            }

        }

        public class UpdateMethod : TestCase<Card>
        {

            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Card> Given()
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

                return () => Card.Update(1, "Updated task", 2);

            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIs1()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameIsUpdated()
            {
                Its.Name.Should().Be("Updated task");
            }

            [Fact]
            public void ShouldAreaIs2()
            {
                Its.AreaID.Should().Be(2);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldCreatedDateNotIsNow()
            {
                Its.CreatedDateUtc.Should().NotBe(NOW);
            }

            public class UpdateActivity : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override Func<Card> Given()
                {
                    var dateProvider = new Mock<IDateProvider>();
                    dateProvider
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);

                    Activity.DateProvider = dateProvider.Object;

                    return () => Card.Update(1, "Updated task", 2);
                }

                [Fact]
                public void ShouldCreateActivity()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.IsAny<Activity>()), Times.Once());
                }

                [Fact]
                public void ShouldActivityCardIDHasValue()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.CardID == 1)), Times.Once());
                }

                [Fact]
                public void ShouldActivityAreaIDHasValue()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.CurrentAreaID == 2)), Times.Once());
                }

                [Fact]
                public void ShouldActivityChangeTypeIsTransfer()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.ChangeType == CardChangeType.Transfer)), Times.Once());
                }

                [Fact]
                public void ShouldActivityStampDateIsNow()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.StampDate == NOW)), Times.Once());
                }

            }

            public class UpdateActivityModify : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override Func<Card> Given()
                {
                    var dateProvider = new Mock<IDateProvider>();
                    dateProvider
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);

                    Activity.DateProvider = dateProvider.Object;

                    return () => Card.Update(1, "Some update", 1);
                }

                [Fact]
                public void ShouldChangeTypeIsModify()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.ChangeType == CardChangeType.Modify)), Times.Once());

                }
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

            protected override Action Given()
            {
                return null;
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

            protected override Func<Card> Given()
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

                return () => Card.Delete(1);
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeActive()
            {
                Its.IsActive.Should().BeFalse();
            }

            [Fact]
            public void ShouldIdIs1()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Card");
            }

            [Fact]
            public void ShouldCreatedDateIsNotNow()
            {
                Its.CreatedDateUtc.Should().NotBe(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            public class DeleteActivity : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override Func<Card> Given()
                {
                    var dateProvider = new Mock<IDateProvider>();
                    dateProvider
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);

                    Activity.DateProvider = dateProvider.Object;

                    return () => Card.Delete(1);
                }

                [Fact]
                public void ShouldCreateActivity()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.IsAny<Activity>()), Times.Once());
                }

                [Fact]
                public void ShouldCardIDIs1()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.CardID == 1)), Times.Once());
                }

                [Fact]
                public void ShouldAreaIDIs1()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.CurrentAreaID == 1)), Times.Once());
                }

                [Fact]
                public void ShouldChangeTypeIsDelete()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.ChangeType == CardChangeType.Delete)), Times.Once());
                }

                [Fact]
                public void ShouldStampDateIsNow()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.StampDate == NOW)), Times.Once());

                }

            }


        }

        public class CreateMethod : TestCase<Card>
        {

            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Card> Given()
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

                return () => Card.Create("Card", 1);
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsASampleTask()
            {
                Its.Name.Should().Be("Card");
            }

            [Fact]
            public void ShouldCreatedDateIsAfterMinValue()
            {
                Its.CreatedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldCreatedDateIsNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsAfterMinValue()
            {
                Its.ModifiedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

            public class CreateActivity : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override Func<Card> Given()
                {
                    var dateProvider = new Mock<IDateProvider>();
                    dateProvider
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);

                    Activity.DateProvider = dateProvider.Object;

                    return () => Card.Create("Card", 1);
                }

                [Fact]
                public void ShouldCreateActivity()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.IsAny<Activity>()), Times.Once());
                }

                [Fact]
                public void ShouldActivityAreaIDIs1()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.CurrentAreaID == 1)), Times.Once());
                }

                [Fact]
                public void ShouldActivityChangeTypeIsTransfer()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.ChangeType == CardChangeType.Transfer)), Times.Once());
                }

                [Fact]
                public void ShouldActivityStampDateIsNow()
                {
                    Subject();

                    Mock.Get(DbFactory.Create())
                        .Verify(d => d.CreateActivity(It.Is<Activity>(a => a.StampDate == NOW)), Times.Once());
                }
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

            protected override Action Given()
            {
                return null;
            }
        }

        public class GetViewMethod : TestCase<CardView>
        {

            readonly DateTime NOW = new DateTime(2013, 1, 6);

            protected override Func<CardView> Given()
            {
                var dateProvider = new Mock<IDateProvider>();
                dateProvider
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = dateProvider.Object;

                var card = new Card()
                {
                    AreaID = 1,
                    ID = 1,
                    IsActive = true,
                    Name = "Sample card",
                    CreatedDateUtc = new DateTime(2013, 1, 1),
                    ModifiedDateUtc = new DateTime(2013, 1, 1)
                };
                
                return () => card.GetView();
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldAreaIDHasValue()
            {
                Its.AreaID.Should().Be(1);
            }

            [Fact]
            public void ShouldIDHasValue()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldIsActiveIsTrue()
            {
                Its.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Sample card");
            }

            [Fact]
            public void ShouldAgeHasValue()
            {
                Its.Age.Should().Be(5);
            }

            [Fact]
            public void ShouldAgeTextHasValue()
            {
                Its.AgeText.Should().Be("5");
            }

            public class GetViewMethod_ParseMessage : TestCase<CardView>
            {
                protected override Func<CardView> Given()
                {
                    var repository = new Mock<ICardRepository>();
                    repository
                        .Setup(r => r.FindAllLabels())
                        .Returns(new List<Label>() 
                        {
                            new Label()
                            {
                                ID = 1,
                                Name = "Label",
                                Color = "Red"
                            }
                        });

                    var factory = new Mock<DbFactory>();
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                    new DbFactory(factory.Object);

                    DateTime NOW = new DateTime(2013, 1, 6);

                    var dateProvider = new Mock<IDateProvider>();
                    dateProvider
                        .Setup(d => d.UtcNow())
                        .Returns(NOW);

                    Card.DateProvider = dateProvider.Object;

                    var card = new Card()
                    {
                        Name            = "Message #label",
                        ModifiedDateUtc = NOW.AddDays(-5)
                    };

                    return () => card.GetView();
                }

                [Fact]
                public void ShouldNameIsMessage()
                {
                    Its.Name.Should().Be("Message");
                }

                [Fact]
                public void ShouldLabelsIsNotNull()
                {
                    Its.Labels.Should().NotBeNull();
                }

                [Fact]
                public void ShouldCountIs1()
                {
                    Its.Labels.Count.Should().Be(1);
                }

                [Fact]
                public void ShouldNameIsLabel()
                {
                    Its.Labels[0].Name.Should().Be("Label");
                }

                [Fact]
                public void ShouldColorIsRed()
                {
                    Its.Labels[0].Color.Should().Be("Red");
                }

                [Fact]
                public void ShouldDaysLastUpdatedHaveValue()
                {
                    Its.DaysSinceLastUpdate.Should().Be(5);
                }

                [Fact]
                public void ShouldDaysLastUpdatedTextHaveValue()
                {
                    Its.DaysSinceLastUpdateText.Should().Be("5");
                }
            }

            public class GetViewMethod_AgeText : TestCase<CardView>
            {
                protected override Func<CardView> Given()
                {
                    return null;
                }

                [Fact]
                public void ShouldReturnNew()
                {
                    Card.DateProvider = new DateProvider();

                    var card = new Card()
                    {
                        CreatedDateUtc = DateTime.UtcNow
                    };

                    card.GetView().AgeText.Should().Be("new");
                }

                [Fact]
                public void ShouldReturnOld()
                {
                    Card.DateProvider = new DateProvider();

                    var card = new Card()
                    {
                        CreatedDateUtc = DateTime.UtcNow.AddDays(-30)
                    };

                    card.GetView().AgeText.Should().Be("aged");
                }

                [Fact]
                public void ShouldReturnNumber()
                {
                    Card.DateProvider = new DateProvider();                    

                    var card = new Card()
                    {
                        CreatedDateUtc = DateTime.UtcNow.AddDays(-5)
                    };

                    card.GetView().AgeText.Should().Be("5");
                }

            }

        }

        public class InitializeCardWithNoDaysSinceLastUpdate : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Card> Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

                var card = new Card();

                return () => card;
            }

            [Fact]
            public void ShouldDaysSinceLastUpdateBeZero()
            {
                Its.DaysSinceLastUpdate.Should().Be(0);
            }
        }

        public class InitializeCardWithDaysSinceLastUpdate : TestCase<Card>
        {
            readonly DateTime CREATEDATE = new DateTime(2013, 1, 1);

            protected override Func<Card> Given()
            {
                var staleCard = new Card()
                {
                    ID              = 1,
                    AreaID          = 1,
                    Name            = "Card",
                    CreatedDateUtc  = CREATEDATE,
                    ModifiedDateUtc = CREATEDATE
                };

                var date = new Mock<IDateProvider>();
                date
                    .Setup(r => r.UtcNow())
                    .Returns(CREATEDATE.AddDays(1));

                Card.DateProvider = date.Object;

                return () => staleCard;
            }

            [Fact]
            public void ShouldDaysStaleNotBeZero()
            {
                Its.DaysSinceLastUpdate.Should().NotBe(0);
            }

            [Fact]
            public void ShouldDaysStaleBeOne()
            {
                Its.DaysSinceLastUpdate.Should().Be(1);
            }
        }

        public class UpdateMethodAndGetDaysSinceLastUpdate : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Card> Given()
            {
                var cardToUpdate = new Card()
                {
                    ID              = 1,
                    AreaID          = 1,
                    Name            = "CardToEdit",
                    CreatedDateUtc  = DateTime.MinValue,
                    ModifiedDateUtc = DateTime.MinValue
                };

                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Card.DateProvider = date.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindCard(It.IsAny<int>()))
                    .Returns(cardToUpdate);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Card.Update(1, "updatedCard", 1);
            }
            
            [Fact]
            public void ShouldCardNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldCardNameBeUpdatedCard()
            {
                Its.Name.Should().Be("updatedCard");
            }

            [Fact]
            public void ShouldDaysSinceLastUpdateBeZero()
            {
                Its.DaysSinceLastUpdate.Should().Be(0);
            }
        }

        public class GetCardAndDaysSinceLastUpdate : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);
            readonly DateTime CREATED = new DateTime(2013, 11, 1);

            protected override Func<Card> Given()
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
                        Name = "Inactive for 30 days",
                        AreaID = 1,
                        ModifiedDateUtc = CREATED,
                        CreatedDateUtc = CREATED
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Card.Get(1);
            }

            [Fact]
            public void ShouldCardNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameBeInactiveFor30Days()
            {
                Its.Name.Should().Be("Inactive for 30 days");
            }

            [Fact]
            public void ShouldDaysLastUpdateBe30()
            {
                Its.DaysSinceLastUpdate.Should().Be(30);
            }
        }

    }
}

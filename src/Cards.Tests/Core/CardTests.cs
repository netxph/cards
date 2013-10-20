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
            public void ShouldDescriptionIsNull()
            {
                Its.Description.Should().BeNull();
            }

            [Fact]
            public void ShouldDueDateIsMaxDate()
            {
                Its.DueDateUtc.Should().Be(DateTime.MaxValue);
            }

            [Fact]
            public void ShouldDifficultyIsNormal()
            {
                Its.Difficulty.Should().Be(Settings.NORMAL_DIFFICULTY);
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

            protected override void Initialize()
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

                Define<IDateProvider>(() =>
                {
                    date
                        .Setup(d => d.UtcNow())
                        .Returns(NOW);

                    return date.Object;
                });

                var repository = new Mock<ICardRepository>();

                Define<ICardRepository>(() =>
                {
                    repository
                        .Setup(r => r.FindCard(It.IsAny<int>()))
                        .Returns(card);

                    return repository.Object;
                });

                var factory = new Mock<DbFactory>();

                Define<DbFactory>(() =>
                {
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(New<ICardRepository>());

                    return factory.Object;
                });
            }

            protected override Func<Card> Given()
            {
                Card.DateProvider = New<IDateProvider>();
                new DbFactory(New<DbFactory>());

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
            public void ShouldAreaIs2()
            {
                Its.AreaID.Should().Be(2);
            }

            [Fact]
            public void ShouldNameNotChanged()
            {
                Its.Name.Should().Be("Card");
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

            public class UpdateMethod_Modify : TestCase<Card>
            {

                readonly DateTime NOW = new DateTime(2013, 12, 1);
                readonly DateTime LATER = new DateTime(2013, 12, 15);

                protected override Func<Card> Given()
                {
                    AccountCache.Reset();

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

                    repository.Setup(r => r.FindAllAccounts())
                        .Returns(new List<Account>()
                        {
                            new Account()
                            {
                                ID = 1,
                                Name = "John Doe",
                                Alias = "john",
                                Email = "john.doe@company.com",
                                IsActive = true
                            }
                        });

                    var factory = new Mock<DbFactory>();
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                    new DbFactory(factory.Object);

                    return () => Card.Update(1, "Updated task @john", 1, "Description", LATER, 1);
                }

                [Fact]
                public void ShouldNotBeNull()
                {
                    Subject().Should().NotBeNull();
                }

                [Fact]
                public void ShouldNameHasChanged()
                {
                    Its.Name.Should().Be("Updated task");
                }

                [Fact]
                public void ShouldDescriptionHasValue()
                {
                    Its.Description.Should().Be("Description");
                }

                [Fact]
                public void ShouldDifficultyIsNotNormal()
                {
                    Its.Difficulty.Should().Be(1);
                }

                [Fact]
                public void ShouldDueDateIsLater()
                {
                    Its.DueDateUtc.Should().Be(LATER);
                }

                [Fact]
                public void ShouldAssignedToIsNotNull()
                {
                    Its.AssignedTo.Should().NotBeNull();
                }

            }


            public class UpdateActivity : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override void Initialize()
                {
                    UseContext(new UpdateMethod());

                    Mock.Get(New<IDateProvider>())
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);
                }

                protected override Func<Card> Given()
                {
                    Activity.DateProvider = New<IDateProvider>();
                    new DbFactory(New<DbFactory>());

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

                protected override void Initialize()
                {
                    UseContext(new UpdateMethod());

                    Mock.Get(New<IDateProvider>())
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);
                }

                protected override Func<Card> Given()
                {
                    AccountCache.Reset();
                    Activity.DateProvider = New<IDateProvider>();
                    new DbFactory(New<DbFactory>());

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

        public class DeleteMethod : TestCase<Card>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override void Initialize()
            {
                

                Define<IDateProvider>(() =>
                {
                    var dateProvider = new Mock<IDateProvider>();

                    dateProvider
                        .Setup(d => d.UtcNow())
                        .Returns(NOW);

                    return dateProvider.Object;
                });

                Define<ICardRepository>(() =>
                {
                    var repository = new Mock<ICardRepository>();
                    Card card = null;

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

                    repository
                        .Setup(r => r.UpdateCard(It.IsAny<Card>()))
                        .Callback<Card>(c => card = c)
                        .Returns(() => card);

                    return repository.Object;
                });

                Define<DbFactory>(() =>
                {
                     var factory = new Mock<DbFactory>();

                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(New<ICardRepository>());

                    return factory.Object;
                });
            }

            protected override Func<Card> Given()
            {
                Card.DateProvider = New<IDateProvider>();
                new DbFactory(New<DbFactory>());

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

                protected override void Initialize()
                {
                    UseContext(new DeleteMethod());

                    Mock.Get(Get<IDateProvider>())
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);
                }

                protected override Func<Card> Given()
                {
                    Activity.DateProvider = New<IDateProvider>();
                    new DbFactory(New<DbFactory>());

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

            protected override void Initialize()
            {
                var dateProvider = new Mock<IDateProvider>();

                Define<IDateProvider>(() =>
                {
                    dateProvider
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);

                    return dateProvider.Object;
                });

                var repository = new Mock<ICardRepository>();
                Card card = null;

                Define<ICardRepository>(() =>
                {
                    repository
                        .Setup(r => r.CreateCard(It.IsAny<Card>()))
                        .Callback<Card>((c) => card = c)
                        .Returns(() => card);

                    return repository.Object;
                });

                var factory = new Mock<DbFactory>();

                Define<DbFactory>(() =>
                {
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(New<ICardRepository>());

                    return factory.Object;
                });

                
            }

            protected override Func<Card> Given()
            {
                Card.DateProvider = New<IDateProvider>();
                new DbFactory(New<DbFactory>());

                return () => Card.Create("Card", 1);
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Card");
            }

            [Fact]
            public void ShouldDescriptionIsNull()
            {
                Its.Description.Should().BeNull();
            }

            [Fact]
            public void ShouldDueDateIsMaxDate()
            {
                Its.DueDateUtc.Should().Be(DateTime.MaxValue);
            }

            [Fact]
            public void ShouldDifficultyIsNormal()
            {
                Its.Difficulty.Should().Be(Settings.NORMAL_DIFFICULTY);
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

            public class ParseAssigned : TestCase<Card>
            {
                protected override Func<Card> Given()
                {
                    AccountCache.Reset();

                    Card card = null;

                    var repository = new Mock<ICardRepository>();
                    repository
                        .Setup(r => r.FindAllAccounts())
                        .Returns(new List<Account>()
                        {
                            new Account()
                            {
                                ID = 1,
                                Name = "John Doe",
                                Alias = "John",
                                Email = "john.doe@company.com",
                                IsActive = true
                            }
                        });
                    repository
                        .Setup(r => r.CreateCard(It.IsAny<Card>()))
                        .Callback<Card>(c => card = c)
                        .Returns(() => new Card() 
                        { 
                            ID = 1,
                            Name = card.Name,
                            AssignedTo = card.AssignedTo 
                        });

                    var factory = new Mock<DbFactory>();
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                    new DbFactory(factory.Object);

                    return () => Card.Create("Card @john", 1);
                }

                [Fact]
                public void ShouldNotReturnNull()
                {
                    Subject().Should().NotBeNull();
                }

                [Fact]
                public void ShouldAssignedToIsNotNull()
                {
                    Its.AssignedTo.Should().NotBeNull();
                }

                [Fact]
                public void ShouldAssignedToNameHasValue()
                {
                    Its.AssignedTo.Name.Should().Be("John Doe");
                }

                [Fact]
                public void ShouldAliasHasValue()
                {
                    Its.AssignedTo.Alias.Should().Be("John");
                }

                [Fact]
                public void ShouldEmailHasValue()
                {
                    Its.AssignedTo.Email.Should().Be("john.doe@company.com");
                }

                [Fact]
                public void ShouldNameHasNoAssignTag()
                {
                    Its.Name.Should().Be("Card");
                }
            }

            public class CreateActivity : TestCase<Card>
            {
                readonly DateTime NOW = new DateTime(2013, 12, 1);

                protected override void Initialize()
                {
                    UseContext(new CreateMethod());

                    Mock.Get(New<IDateProvider>())
                        .Setup(dp => dp.UtcNow())
                        .Returns(NOW);
                }

                protected override Func<Card> Given()
                {
                    Card.DateProvider = New<IDateProvider>();
                    new DbFactory(New<DbFactory>());

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
                    LabelCache.Reset();

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
                        ModifiedDateUtc = NOW.AddDays(-6)
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
                    Its.DaysSinceLastUpdate.Should().Be(6);
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
                AccountCache.Reset();

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

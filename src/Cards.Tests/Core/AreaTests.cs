using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq.Protected;
using Moq;
using System.ComponentModel.DataAnnotations;
using Cards.Core;
using Speculous;

namespace Cards.Tests.Core
{
    public class AreaTests
    {

        public class InitializeArea : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Area> Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Area.DateProvider = date.Object;

                var area = new Area();

                return () => area;
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
            public void ShouldCardsIsNull()
            {
                Its.Cards.Should().BeNull();
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldCreateDateIsNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }
        }

        public class GetMethod : TestCase<Area>
        {
            protected override Func<Area> Given()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindArea(1))
                    .Returns(new Area()
                    {
                        ID = 1,
                        Name = "IceBox"
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.Get(1);
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDNotZero()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("IceBox");
            }
        }

        public class GetAllMethod : TestCase<List<Area>>
        {
            protected override Func<List<Area>> Given()
            {
                var areas = new List<Area>()
                {
                    new Area()
                    {
                        ID = 1,
                        Name = "Area"
                    }
                };

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindAllArea())
                    .Returns(areas);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.GetAll();
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            {
                Subject().Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldContainOneItem()
            {
                Its.Count.Should().Be(1);
            }
        }

        public class PutMethod : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Area> Given()
            {
                Area area = null;

                var dateProvider = new Mock<IDateProvider>();
                dateProvider
                    .Setup(date => date.UtcNow())
                    .Returns(NOW);

                Area.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindArea(1))
                    .Returns(() => new Area()
                    {
                        ID = 1,
                        Name = "Backlog",
                        CreatedDateUtc = DateTime.MinValue,
                        ModifiedDateUtc = DateTime.MinValue
                    });
                repository
                    .Setup(r => r.UpdateArea(It.IsAny<Area>()))
                    .Callback<Area>((a) => area = a)
                    .Returns(() => area);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.Update(1, "Updated");
            }

            [Fact]
            public void ShouldNotReturnNull()
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
                Its.Name.Should().Be("Updated");
            }

            [Fact]
            public void ShouldCreatedDateShouldNotChange()
            {
                Its.CreatedDateUtc.Should().Be(DateTime.MinValue);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

            public class Invalid : TestCase<Area>
            {
                protected override Func<Area> Given()
                {
                    return () => Area.Update(99, "Invalid");
                }

                [Fact]
                public void ShouldReturnNull()
                {
                    Subject().Should().BeNull();
                }

            }


        }


        public class PostMethod : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Area> Given()
            {
                Area area = null;

                var dateProvider = new Mock<IDateProvider>();
                dateProvider
                    .Setup(date => date.UtcNow())
                    .Returns(NOW);

                Area.DateProvider = dateProvider.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.CreateArea(It.IsAny<Area>()))
                    .Callback<Area>((a) => area = a)
                    .Returns(() => area);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.Create("Area");
            }

            [Fact]
            public void ShouldAddNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsNewArea()
            {
                Its.Name.Should().Be("Area");
            }

            [Fact]
            public void ShouldBeActive()
            {
                Its.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldCreatedDateIsAfterMinValue()
            {
                Its.CreatedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldCreatedDateIsUtcNow()
            {
                Its.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsAfterMinValue()
            {
                Its.ModifiedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldModifiedDateIsUtcNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }

        }

        public class CreateMethod_Invalid : TestCase
        {
            protected override Action Given()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.CreateArea(It.IsAny<Area>()))
                    .Callback<Area>(area =>
                    {
                        Validator.ValidateObject(area, new ValidationContext(area), true);
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.Create("");
            }

            [Fact]
            public void ShouldThrowValidationError()
            {
                Subject.ShouldThrow<ValidationException>();
            }
        }

        public class DeleteMethod : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Func<Area> Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Area.DateProvider = date.Object;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindArea(1))
                    .Returns(new Area()
                    {
                        ID = 1,
                        Name = "Area",
                        CreatedDateUtc = DateTime.MinValue,
                        ModifiedDateUtc = DateTime.MinValue
                    });

                Area area = null;

                repository
                    .Setup(r => r.UpdateArea(It.IsAny<Area>()))
                    .Callback<Area>(a => area = a)
                    .Returns(() => area);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Area.Delete(1);
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIs1()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Area");
            }

            [Fact]
            public void ShouldNotBeActive()
            {
                Its.IsActive.Should().BeFalse();
            }

            [Fact]
            public void ShouldCreatedIsNotNow()
            {
                Its.CreatedDateUtc.Should().NotBe(NOW);
            }

            [Fact]
            public void ShouldModifiedIsNow()
            {
                Its.ModifiedDateUtc.Should().Be(NOW);
            }
        }

        public class FindMethod_MultiLabels : TestCase<List<AreaView>>
        {

            protected override void Initialize()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindAllArea())
                    .Returns(new List<Area>()
                    {
                        new Area()
                        {
                            ID = 1,
                            Name = "Backlog",
                            IsActive = true,
                            Cards = new List<Card>()
                            {
                                new Card()
                                {
                                    Name = "A bug #bug"
                                },
                                new Card()
                                {
                                    Name = "Not a bug #test"
                                }
                            }
                        }
                    });

                repository
                    .Setup(r => r.FindAllLabels())
                    .Returns(new List<Label>()
                    {
                        new Label()
                        {
                            Name = "Bug",
                            Color = "Red"
                        },
                        new Label()
                        {
                            Name = "Test",
                            Color = "Green"
                        }
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);
            }

            protected override Func<List<AreaView>> Given()
            {
                LabelCache.Reset();

                return () => Area.Find(new FilterCardRequest() { CardLabel = "bug;test" });
            }

            [Fact]
            public void ShouldReturnAllMatches()
            {
                Its[0].Cards.Count.Should().Be(2);
            }

            [Fact]
            public void ShouldCardsHasBug()
            {
                Its.Any(a => a.Cards.Any(c => c.Labels.Any(l => l.Name == "Bug")));
            }

            [Fact]
            public void ShouldCardsHasTest()
            {
                Its.Any(a => a.Cards.Any(c => c.Labels.Any(l => l.Name == "Test")));
            }
        }

        public class FindMethod : TestCase<List<AreaView>>
        {
            protected override void Initialize()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindAllArea())
                    .Returns(new List<Area>()
                    {
                        new Area()
                        {
                            ID = 1,
                            Name = "Backlog",
                            IsActive = true,
                            Cards = new List<Card>()
                            {
                                new Card()
                                {
                                    Name = "A bug #bug"
                                },
                                new Card()
                                {
                                    Name = "Not a bug #test"
                                }
                            }
                        }
                    });
                
                repository
                    .Setup(r => r.FindAllLabels())
                    .Returns(new List<Label>()
                    {
                        new Label()
                        {
                            Name = "Bug",
                            Color = "Red"
                        }
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);                
            }

            protected override Func<List<AreaView>> Given()
            {
                LabelCache.Reset();
                
                return () => Area.Find(new FilterCardRequest());
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            {
                Its.Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldReturnAllCards()
            {
                Its[0].Cards.Count.Should().Be(2);
            }

            public class FindMethod_Labels : TestCase<List<AreaView>>
            {

                protected override void Initialize()
                {

                    var repository = new Mock<ICardRepository>();
                    repository
                        .Setup(r => r.FindAllArea())
                        .Returns(new List<Area>()
                    {
                        new Area()
                        {
                            ID = 1,
                            Name = "Backlog",
                            IsActive = true,
                            Cards = new List<Card>()
                            {
                                new Card()
                                {
                                    Name = "A bug #bug"
                                },
                                new Card()
                                {
                                    Name = "Not a bug #test"
                                }
                            }
                        }
                    });

                    repository
                        .Setup(r => r.FindAllLabels())
                        .Returns(new List<Label>()
                    {
                        new Label()
                        {
                            Name = "Bug",
                            Color = "Red"
                        }
                    });

                    var factory = new Mock<DbFactory>();
                    factory.Protected()
                        .Setup<ICardRepository>("OnCreateDb")
                        .Returns(repository.Object);

                    new DbFactory(factory.Object);
                }

                protected override Func<List<AreaView>> Given()
                {
                    LabelCache.Reset();

                    return () => Area.Find(new FilterCardRequest() { CardLabel = "bug" });
                }

                [Fact]
                public void ShouldReturn1Card()
                {
                    Its[0].Cards.Count.Should().Be(1);
                }

                [Fact]
                public void ShouldCardHasLabelBug()
                {
                    Its[0].Cards[0].Labels.Any(l => l.Name == "Bug").Should().BeTrue();
                }
            }
        }

        public class GetViewMethod : TestCase<AreaView>
        {

            protected override Func<AreaView> Given()
            {
                var area = new Area()
                {
                    ID = 1,
                    Name = "Backlog",
                    IsActive = true,
                    Cards = new List<Card>() 
                    {
                        new Card()
                        {
                            ID = 1,
                            AreaID = 1,
                            IsActive = true,
                            Name = "Sample card"
                        }
                    }
                };

                return () => area.GetView();
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
            public void ShouldNameHasValue()
            {
                Its.Name.Should().Be("Backlog");
            }
            
            [Fact]
            public void ShouldIsActiveIsTrue()
            {
                Its.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldCardsNotNull()
            {
                Its.Cards.Should().NotBeNull();
            }

        }

    }
}

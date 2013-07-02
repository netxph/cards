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

namespace Cards.Tests.Core
{
    public class AreaTests
    {

        public class InitializeArea : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Area Given()
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                Area.DateProvider = date.Object;

                var area = new Area();

                return area;
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
            public void ShouldCardsIsNull()
            {
                Subject.Cards.Should().BeNull();
            }

            [Fact]
            public void ShouldBeActive()
            {
                Subject.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldCreateDateIsNow()
            {
                Subject.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }
        }

        public class GetAllMethod : TestCase<List<Area>>
        {
            protected override List<Area> Given()
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

                return Area.GetAll();
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNotBeEmpty()
            { 
                Subject.Should().NotBeEmpty();
            }

            [Fact]
            public void ShouldContainOneItem()
            {
                Subject.Count.Should().Be(1);
            }
        }

        public class CreateMethod : TestCase<Area>
        {
            readonly DateTime NOW = new DateTime(2013, 12, 1);

            protected override Area Given()
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

                return Area.Create("Area");
            }

            [Fact]
            public void ShouldAddNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsNewArea()
            {
                Subject.Name.Should().Be("Area");
            }

            [Fact]
            public void ShouldBeActive()
            {
                Subject.IsActive.Should().BeTrue();
            }

            [Fact]
            public void ShouldCreatedDateIsAfterMinValue()
            {
                Subject.CreatedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldCreatedDateIsUtcNow()
            {
                Subject.CreatedDateUtc.Should().Be(NOW);
            }

            [Fact]
            public void ShouldModifiedDateIsAfterMinValue()
            {
                Subject.ModifiedDateUtc.Should().BeAfter(DateTime.MinValue);
            }

            [Fact]
            public void ShouldModifiedDateIsUtcNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }
            
        }

        public class CreateMethod_Invalid : TestCase<Action>
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

            protected override Area Given()
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

                return Area.Delete(1);
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldIDIs1()
            {
                Subject.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameHasValue()
            {
                Subject.Name.Should().Be("Area");
            }

            [Fact]
            public void ShouldNotBeActive()
            {
                Subject.IsActive.Should().BeFalse();
            }

            [Fact]
            public void ShouldCreatedIsNotNow()
            {
                Subject.CreatedDateUtc.Should().NotBe(NOW);
            }

            [Fact]
            public void ShouldModifiedIsNow()
            {
                Subject.ModifiedDateUtc.Should().Be(NOW);
            }
        }


    }
}

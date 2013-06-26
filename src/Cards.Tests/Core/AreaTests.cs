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
            protected override Area Given()
            {
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
            protected override Area Given()
            {
                var area = new Area()
                {
                    ID = 1,
                    Name = "Area"
                };

                var repository = new Mock<ICardRepository>();
                repository.Setup(r => r.CreateArea(It.IsAny<Area>()))
                    .Returns(area);

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

    }
}

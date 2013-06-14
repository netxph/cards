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
                var areas = new FakeDbSet<Area>();
                areas.Add(new Area() { ID = 1, Name = "Backlog" });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Areas = areas });

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
                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Areas = new FakeDbSet<Area>() });

                new DbFactory(factory.Object);

                return Area.Create("NewArea");
            }

            [Fact]
            public void ShouldAddNotReturnNull()
            {
                Subject.Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsNewArea()
            {
                Subject.Name.Should().Be("NewArea");
            }
            
        }

        public class CreateMethod_Invalid : TestCase<Action>
        {
            protected override Action Given()
            {
                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<CardsDb>("OnCreateDb")
                    .Returns(new CardsDb() { Areas = new FakeDbSet<Area>() });

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

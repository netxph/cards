using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Cards.Core;
using Moq;
using Moq.Protected;

namespace Cards.Tests.Core
{
    public class LabelTests
    {

        public class Initialize : TestCase<Label>
        {
            protected override Func<Label> Given()
            {
                return () => new Label();
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
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
            public void ShouldColorIsNull()
            {
                Its.Color.Should().BeNull();
            }
        }

        public class AddLabelMethod : TestCase<Label>
        {
            protected override Func<Label> Given()
            {
                Label label = null;

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.CreateLabel(It.IsAny<Label>()))
                    .Callback<Label>(l => label = l)
                    .Returns(() => label);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);
                

                return () => Label.Create("Bug", "Red");
            }

            [Fact]
            public void ShouldNotBeNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsBug()
            {
                Its.Name.Should().Be("Bug");
            }

            [Fact]
            public void ShouldColorIsRed()
            {
                Its.Color.Should().Be("Red");
            }
        }

        public class GetAllMethod : TestCase<List<Label>>
        {
            protected override Func<List<Label>> Given()
            {
                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindAllLabels())
                    .Returns(new List<Label>() 
                    {
                        new Label() 
                        {
                            ID = 1,
                            Name = "Bug",
                            Color = "Red"
                        }
                    });

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Label.GetAll();
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldCountIs1()
            {
                Its.Count.Should().Be(1);
            }

            [Fact]
            public void ShouldIDHasValue()
            {
                Its[0].ID.Should().Be(1);
            }

            [Fact]
            public void ShouldNameIsBug()
            {
                Its[0].Name.Should().Be("Bug");
            }

            [Fact]
            public void ShouldColorIsRed()
            {
                Its[0].Color.Should().Be("Red");
            }
        }
        
    }
}

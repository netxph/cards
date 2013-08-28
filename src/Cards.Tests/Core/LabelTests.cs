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

        public class DeleteLabelMethod : TestCase<Label>
        {
            protected override Func<Label> Given()
            {
                LabelCache.Reset();

                Queue<List<Label>> resultQueue = new Queue<List<Label>>();

                var label = new Label()
                {
                    ID = 1,
                    Name = "Bug",
                    Color = "Red"
                };

                var withValues = new List<Label>()
                {
                    label
                };

                var withoutValues = new List<Label>();

                resultQueue.Enqueue(withValues);
                resultQueue.Enqueue(withoutValues);

                var repository = new Mock<ICardRepository>();
                repository
                    .Setup(r => r.FindAllLabels())
                    .Returns(() => resultQueue.Dequeue());

                repository
                    .Setup(r => r.DeleteLabel(It.Is<Label>(l => l.Name == "Bug")))
                    .Returns(label);

                var factory = new Mock<DbFactory>();
                factory.Protected()
                    .Setup<ICardRepository>("OnCreateDb")
                    .Returns(repository.Object);

                new DbFactory(factory.Object);

                return () => Label.Delete("bug");
            }

            [Fact]
            public void ShouldNotReturnNull()
            {
                Subject().Should().NotBeNull();
            }

            [Fact]
            public void ShouldNameIsBug()
            {
                Its.Name.Should().Be("Bug");
            }

            [Fact]
            public void ShouldIDIs1()
            {
                Its.ID.Should().Be(1);
            }

            [Fact]
            public void ShouldColorIsRed()
            {
                Its.Color.Should().Be("Red");
            }

            [Fact]
            public void ShouldCacheIsReset()
            {
                LabelCache.GetLabel("Red").Should().Be(null);
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

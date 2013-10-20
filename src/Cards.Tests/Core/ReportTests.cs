using Cards.Core;
using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace Cards.Tests.Core
{
    public class ReportTests : TestCase<Report>
    {
        readonly DateTime NOW = new DateTime(2013, 12, 1);

        protected override void Initialize()
        {
            Define<IDateProvider>(() =>
            {
                var date = new Mock<IDateProvider>();
                date
                    .Setup(d => d.UtcNow())
                    .Returns(NOW);

                return date.Object;
            });

            Define<ICardRepository>(() =>
            {
                var repository = new Mock<ICardRepository>();


                repository
                    .Setup(r => r.FindAllArea())
                    .Returns(() =>
                    {
                        Card.DateProvider = New<IDateProvider>();

                        return new List<Area>() 
                        {
                            new Area() 
                            {
                                Name = "Backlog",
                                Cards = new List<Card>()
                                {
                                    new Card() 
                                    { 
                                        CreatedDateUtc = NOW,
                                        ModifiedDateUtc = NOW
                                    },
                                    new Card() 
                                    { 
                                        CreatedDateUtc = NOW.AddDays(-30),
                                        ModifiedDateUtc = NOW
                                    },
                                    new Card() 
                                    { 
                                        CreatedDateUtc = NOW.AddDays(-10),
                                        ModifiedDateUtc = NOW.AddDays(-10)
                                    }
                                }
                            }
                        };
                    });

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

        protected override Func<Report> Given()
        {
            new DbFactory(New<DbFactory>());
            Report.DateProvider = New<IDateProvider>();

            return () => Report.GetSummary();
        }

        [Fact]
        public void ShouldNotReturnNull()
        {
            Subject().Should().NotBeNull();
        }

        [Fact]
        public void ShouldNewItemsHasValue()
        {
            Its.NewItems.Should().Be(1);
        }

        [Fact]
        public void ShouldAgedHasValue()
        {
            Its.Aged.Should().Be(1);
        }

        [Fact]
        public void ShouldStaleItemsHasValue()
        {
            Its.StaleItems.Should().Be(1);
        }

        [Fact]
        public void ShouldAreasIsNotNull()
        {
            Its.Areas.Should().NotBeNull();
        }

        [Fact]
        public void ShouldAreasIsNotEmpty()
        {
            Its.Areas.Count.Should().Be(1);
        }
        
        [Fact]
        public void ShouldAreasFirstItemNameHasValue()
        {
            Its.Areas.Keys.ToList()[0].Should().Be("Backlog");
        }

        [Fact]
        public void ShouldAreasCountIs3()
        {
            Its.Areas["Backlog"].Should().Be(3);
        }
    }
}

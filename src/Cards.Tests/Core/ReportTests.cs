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
                                Cards = new List<Card>()
                                {
                                    new Card() { CreatedDateUtc = NOW }
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
    }
}

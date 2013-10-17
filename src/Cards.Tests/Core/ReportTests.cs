using Cards.Core;
using Speculous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Cards.Tests.Core
{
    public class ReportTests : TestCase<Report>
    {
        protected override Func<Report> Given()
        {
            return () => Report.GetSummary();
        }

        //[Fact]
        public void ShouldNotReturnNull()
        {
            Subject().Should().NotBeNull();
        }

        //[Fact]
        public void ShouldNewItemsHasValue()
        {
            Its.NewItems.Should().Be(1);
        }
    }
}

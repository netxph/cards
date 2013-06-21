using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests
{

    public abstract class TestCase : IDisposable
    {
        public TestCase()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Destroy()
        {

        }

        public void Dispose()
        {
            Destroy();
        }
    }


    public abstract class TestCase<T> : TestCase
    {

        public T Subject { get; set; }

        protected abstract T Given();

        protected override void Initialize()
        {
            base.Initialize();
            Subject = Given();
        }
        
    }
}

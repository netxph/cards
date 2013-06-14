using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests
{
    public abstract class TestCase<T> : IDisposable
    {

        protected T Subject { get; set; }

        public TestCase()
        {
            Initialize();
            Subject = Given();
        }

        protected virtual void Initialize()
        {
            
        }

        protected abstract T Given();

        protected virtual void Destroy()
        {
            
        }

        public void Dispose()
        {
            Destroy();    
        }
        
    }
}

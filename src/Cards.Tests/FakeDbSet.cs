using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Tests
{
    public class FakeDbSet<T> : IDbSet<T>, IQueryable<T>
        where T : class
    {
        private HashSet<T> _data;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
        }

        public T Add(T entity)
        {
            Validator.ValidateObject(entity, new ValidationContext(entity), true);

            _data.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(_data); }
        }

        public T Remove(T entity)
        {
            _data.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _data.AsQueryable().Provider; }
        }
    }
}

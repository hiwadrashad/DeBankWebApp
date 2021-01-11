using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DeBank.Library.GeneralMethods
{
    public class Filter<T>
    {
        public Filter(Filter<T> filter)
        {
            Sources = filter.Sources;
            Filters = filter.Filters;
        }

        public Filter(IEnumerable<T> sources, List<Func<T, bool>> filters = null)
        {
            Sources = sources;

            if(filters == null)
            {
                filters = new List<Func<T, bool>>();
            }

            Filters = filters;
        }

        public IEnumerable<T> Sources { get; internal set; }
        private List<Func<T, bool>> Filters { get; set; }

        public Filter<T> AddFilter(Func<T, bool> filter)
        {
            Filters.Add(filter);

            return this;
        }

        public IEnumerable<T> Execute()
        {
            List<T> result = new List<T>();

            foreach(var filter in Filters)
            {
                foreach (var source in Sources)
                {
                    if (filter.Invoke(source))
                    {
                        result.Add(source);
                    }
                }
            }

            return result;
        }
    }
}

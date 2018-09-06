using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;

namespace LandonApi.Models
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        public int Size { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link First { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Previous { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Next { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Last { get; set; }

        public static PagedCollection<T> Create(Link self, T[] items, int size, PagingOptions pagingOptions)
        {
            return new PagedCollection<T>
            {
                Self = self,
                Value = items,
                Size = size,
                Offset = pagingOptions.Offset,
                Limit = pagingOptions.Limit,
                First = self,
                Next = GetNextLink(self, size, pagingOptions),
                Previous = GetPreviousLink(self, size, pagingOptions),
                Last = GetLastLink(self, size, pagingOptions)
            };
        }

        private static Link GetNextLink(Link self, int size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null || pagingOptions?.Offset == null) return null;

            var limit = pagingOptions.Limit.Value;
            var offset = pagingOptions.Offset.Value;

            var next = offset + limit;
            if (next >= size) return null;

            return SetNewLink(self, limit, next);
        }

        private static Link GetLastLink(Link self, int size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null) return null;

            var limit = pagingOptions.Limit.Value;

            if (size <= limit) return null;

            var offset = Math.Ceiling((size - (double)limit) / limit) * limit;

            return SetNewLink(self, limit, offset);
        }

        private static Link GetPreviousLink(Link self, int size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Limit == null) return null;
            if (pagingOptions?.Offset == null) return null;

            var limit = pagingOptions.Limit.Value;
            var offset = pagingOptions.Offset.Value;

            if (offset == 0)
            {
                return null;
            }

            if (offset > size)
            {
                return GetLastLink(self, size, pagingOptions);
            }

            var previous = Math.Max(offset - limit, 0);

            if (previous <= 0)
            {
                return self;
            }

            return SetNewLink(self, limit, previous);
        }

        private static Link SetNewLink(Link self, int limit, double offset)
        {
            var parameters = new RouteValueDictionary(self.RouteValues)
            {
                ["limit"] = limit,
                ["offset"] = offset
            };
            var newLink = Link.ToCollection(self.RouteName, parameters);

            return newLink;
        }
    }
}

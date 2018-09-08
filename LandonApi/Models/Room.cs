using LandonApi.Infrastructure;

namespace LandonApi.Models
{
    public class Room : Resource
    {
        [Searchable]
        [Sortable]
        public string Name { get; set; }

        [Searchable]
        [Sortable(Default = true)]
        public decimal Rate { get; set; }
    }
}

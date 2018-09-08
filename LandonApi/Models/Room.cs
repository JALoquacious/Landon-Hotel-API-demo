using LandonApi.Infrastructure;

namespace LandonApi.Models
{
    public class Room : Resource
    {
        [Sortable]
        public string Name { get; set; }

        [Sortable(Default = true)]
        public decimal Rate { get; set; }
    }
}

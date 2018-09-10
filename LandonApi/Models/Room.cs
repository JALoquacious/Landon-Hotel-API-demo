using LandonApi.Infrastructure;

namespace LandonApi.Models
{
    public class Room : Resource
    {
        [Sortable]
        [SearchableString]
        public string Name { get; set; }

        [Sortable(Default = true)]
        [SearchableDecimal]
        public decimal Rate { get; set; }
    }
}

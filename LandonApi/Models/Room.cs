﻿namespace LandonApi.Models
{
    public class Room : Resource
    {
        [Sortable]
        public string Name { get; set; }

        [Sortable]
        public decimal Rate { get; set; }
    }
}

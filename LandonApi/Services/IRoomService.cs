﻿using LandonApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(Guid id, CancellationToken ct);
    }
}
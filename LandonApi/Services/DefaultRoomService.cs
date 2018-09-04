using AutoMapper;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiContext _context;

        public DefaultRoomService(HotelApiContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomAsync(Guid roomId, CancellationToken ct)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == roomId, ct);

            if (entity == null) return null;

            return Mapper.Map<Room>(entity);
        }
    }
}

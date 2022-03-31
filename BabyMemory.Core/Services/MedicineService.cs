using BabyMemory.Core.Common;
using BabyMemory.Core.Contracts;
using BabyMemory.Infrastructure.Data;
using BabyMemory.Infrastructure.Data.Models;
using BabyMemory.Infrastructure.Models;

namespace BabyMemory.Core.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicineAsync(MedicineAddViewModel model)
        {
            Medicine medicine = new Medicine
            {
                Name = model.Name,
                Description = model.Description
            };
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }
    }
}

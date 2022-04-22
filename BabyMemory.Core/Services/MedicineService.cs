namespace BabyMemory.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Models;
    using Infrastructure.Models;

    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicineAsync(MedicineAddViewModel model)
        {
            Medicine medicine = new()
            {
                Name = model.Name,
                Description = model.Description
            };
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }
    }
}

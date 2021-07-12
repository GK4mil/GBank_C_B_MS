using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Application.Contracts.Persistence
{
    public interface IBillRepository : IAsyncRepository<Bill>
    {
        Task<List<Bill>> GetBillsByUserId(int userId);
        Task<List<Bill>> FindBybillNumber(String billnr);

    }
}

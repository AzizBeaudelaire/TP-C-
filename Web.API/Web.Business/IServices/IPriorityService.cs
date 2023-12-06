using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
using Web.Business;
using Web.Business.Models;

namespace WebApi.IServices
{
    public interface IPriorityService
    {
        List<Priority> GetPriorities();
        Priority GetPriorityById(int id);
        void AddPriority(Priority priority);
        void UpdatePriority(int id, Priority updatedPriority);
        void DeletePriority(int id);
    }
}
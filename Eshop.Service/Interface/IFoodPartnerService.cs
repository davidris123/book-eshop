using Eshop.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Interface
{
    public interface IFoodPartnerService
    {
        List<FoodPartner> GetFoodPartnerList();
    }
}

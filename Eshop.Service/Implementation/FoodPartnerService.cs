using Eshop.DomainEntities.Domain;
using Eshop.Service.Interface;
using EShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Implementation
{
    public class FoodPartnerService : IFoodPartnerService
    {
        private readonly IRepository<FoodPartner> _foodPartnerRepository;

        public FoodPartnerService(IRepository<FoodPartner> foodPartnerRepository)
        {
            _foodPartnerRepository = foodPartnerRepository;
        }

        public List<FoodPartner> GetFoodPartnerList()
        {
            return _foodPartnerRepository.GetAll().ToList();
        }
    }
}

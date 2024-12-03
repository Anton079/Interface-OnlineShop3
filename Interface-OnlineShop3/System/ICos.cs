using Interface_OnlineShop3.System.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.System
{
    public interface ICos
    {
        bool AddCos(OrderDetailsDto detailsDto);

        
        IList<OrderDetailsDto> GetAll();

        bool RemoveFromCos(string productName);

        bool EditQuantity(string productName, int newQuantity);

        void Clear();
    }
}

using Interface_OnlineShop3.Cos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Cos
{
    public interface ICos
    {
        bool AddCos(OrderDetailsDto detailsDto);

        IList<OrderDetailsDto> GetAll();


    }
}

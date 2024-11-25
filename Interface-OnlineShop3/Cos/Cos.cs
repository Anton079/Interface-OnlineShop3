using Interface_OnlineShop3.Cos.DTOs;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Cos
{
    public class Cos : ICos
    {
        IList<OrderDetailsDto> orderDetailsDtos;

        IProductQueryService productQueryService;
        

        public Cos(IProductQueryService productQueryService)
        {
            orderDetailsDtos = new List<OrderDetailsDto>();
            this.productQueryService = productQueryService;
        }

        public bool AddCos(OrderDetailsDto detailsDto) { 
        
         Product product=   this.productQueryService.FindProductById(detailsDto.ProductId);

            if (detailsDto.Quantity > product.Stock)
            {

                return false;
            }


            foreach(OrderDetailsDto details in orderDetailsDtos)
            {
                
                if(details.ProductName == detailsDto.ProductName)
                {
                    details.Quantity += detailsDto.Quantity;
                    product.Stock -= detailsDto.Quantity;
                    return true;
                }
                else
                {
                    orderDetailsDtos.Add(details);
                    return true;
                }

            }
            return false;
        }

        public IList<OrderDetailsDto> GetAll()
        {
            return this.orderDetailsDtos;
        }

        
    }
}

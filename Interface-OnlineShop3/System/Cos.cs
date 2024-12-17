using Interface_OnlineShop3.System.DTOs;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Interface_OnlineShop3.System
{
    public class Cos : ICos
    {
        IList<OrderDetailsDto> orderDetailsDtos;

        IProductQueryService productQueryService;
        
        public Cos(IProductQueryService productQueryService)
        {
            this.orderDetailsDtos = new List<OrderDetailsDto>();
            this.productQueryService = productQueryService;
        }

        //CRUD
        public bool AddCos(OrderDetailsDto detailsDto) { 
            
            Product product=this.productQueryService.FindProductById(detailsDto.ProductId);

            if (detailsDto.Quantity > product.Stock)
            {
                return false;
            }

            OrderDetailsDto orderDetails =this.FindDetailByProductName(product.Name);

            if (orderDetails==null)
            {   
                this.orderDetailsDtos.Add(detailsDto);
            }
            else
            {
                orderDetails.Quantity += detailsDto.Quantity;
               
            }
            product.Stock -= detailsDto.Quantity;
            

            return true;
        }

        public IList<OrderDetailsDto> GetAll()
        {
            return this.orderDetailsDtos;
        }

        //todo:functie findProduct in cos by product name
        public OrderDetailsDto FindDetailByProductName(string productName)
        {
            foreach(OrderDetailsDto detailsDto in this.orderDetailsDtos)
            {
                if(detailsDto.ProductName == productName)
                {
                    return detailsDto;
                }
            }
            return null;
        }

        public int FindIdByProductName(string productName)
        {
            foreach (OrderDetailsDto detailsDto in this.orderDetailsDtos)
            {
                if (detailsDto.ProductName == productName)
                {
                    return detailsDto.ProductId;
                }
            }
            return -1;
        }

        public bool RemoveFromCos(string productName)
        {
            OrderDetailsDto orderDetails = this.FindDetailByProductName(productName);

            if (orderDetails == null) return false;

            this.orderDetailsDtos.Remove(orderDetails);

            Product product = this.productQueryService.FindProductById(orderDetails.ProductId);
            if (product != null)
                product.Stock += orderDetails.Quantity;

            return true;
        }
        
        public bool EditQuantity(string productName, int newQuantity){

            if(newQuantity <= 0) return false;

            OrderDetailsDto orderDetails = this.FindDetailByProductName(productName);

            if(orderDetails == null) return false;

            Product product = this.productQueryService.FindProductById(orderDetails.ProductId);

            if(product != null )
            {
                product.Stock += orderDetails.Quantity;
                product.Stock -= newQuantity;
                orderDetails.Quantity = newQuantity;
                return true;
            }

            return false;
        }

        public void Clear()
        {
            orderDetailsDtos.Clear();
        }
    }
}

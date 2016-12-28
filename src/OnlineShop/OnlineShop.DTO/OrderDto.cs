using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace OnlineShop.DTO
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime PlacingDate { get; set; }

        [DataMember]
        public StatusDto Status { get; set; }

        [DataMember]
        public List<OrderItemDto> OrderItems { get; set; }

        [DataMember]
        public UserChildDto User { get; set; }

        [DataMember]
        public double TotalPrice
        {
            get
            {
                return OrderItems.Sum(orderItem => orderItem.TotalPrice);
            }
        }

        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public enum ResourceEnums
    {
        Product = 0,
        DistributionServices = 1
    }

    public class CartItem
    {
        public ResourceEnums ResourceType { get; set; }
        public string ResourceId { get; set; }

    }

}

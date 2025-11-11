using AutoMapper;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using WebApi_Oficial2.DTO;
using WebApi_Oficial2.Models;

namespace WebApi_Oficial2.Mappings {
    public class DTOMappingProfile : Profile {

        public DTOMappingProfile() {

            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();


        }
    }
}

using AutoMapper;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Common.Mappings.MappingProfile;

namespace Application.Despatches.Commands.EditDespatch
{
    public class EditDespatchCommand : IRequest<List<string>>, IMapFrom<Despatch>
    {
        public int Id { get; set; }
        public string IndentingDept { get; set; }
        public string ReferenceNo { get; set; }
        public string Purpose { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Despatch, EditDespatchCommand>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despatches.Commands.CreateDespatch
{
   public  class CreateDespatchCommand :IRequest<List<string>>
    {
        public string IndentingDept { get; set; }
        public string ReferenceNo { get; set; }
        public string Purpose { get; set; }
        public string SendTo { get; set; }

    }
}

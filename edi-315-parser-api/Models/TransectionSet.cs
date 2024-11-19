using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edi_315_parser_api.Models
{
    public class TransectionSet
    {
        public ST ST { get; set; }
        public B4 B4 { get; set; }
        public List<N9> N9 { get; set; } = new List<N9>();
        public Q2 Q2 { get; set; }
        public List<ShipmentStatus> ShipmentStatus { get; set; } = new List<ShipmentStatus>();
        public List<PortTerminal> PortTerminal { get; set; } = new List<PortTerminal>();
    }
}

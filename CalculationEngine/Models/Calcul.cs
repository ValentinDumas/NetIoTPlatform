using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceEndpoint.Repositories
{
    public class Calcul
    {
        public int id { get; set; }

        public int domain_id { get; set; }

        public string type { get; set; }

        public float average { get; set; }

        public float sum { get; set; }

        public float max { get; set; }

        public float min { get; set; }

        public DateTime date { get; set; }
    }
}


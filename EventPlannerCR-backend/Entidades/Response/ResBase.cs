﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBase
    {
        public bool Resultado { get; set; }
        public List<Error> error { get; set; }
    }
}

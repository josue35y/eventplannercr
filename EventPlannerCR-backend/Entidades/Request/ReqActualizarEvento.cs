﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class ReqActualizarEvento : ReqBase
    {
        public Evento Evento { get; set; }
        public byte[] Imagen { get; set; }
    }
}

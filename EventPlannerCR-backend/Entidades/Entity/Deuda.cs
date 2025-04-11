﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Deuda
    {
        public int idDeuda {  get; set; }
        public Usuario Usuario { get; set; }
        public string Motivo { get; set; }
        public Decimal Total {  get; set; }
        public DateTime FechaCreacion {  get; set; }
    }
}

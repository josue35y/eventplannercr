﻿using System.Collections.Generic;

namespace EventPlannerCR_backend.Entidades
{
    public class ResBuscarDeuda : ResBase
    {
        public List<Deudor> Deudor { get; set; }
    }
}
namespace EventPlannerCR_backend.Entidades
{
    public class ReqActualizarEvento : ReqBase
    {
        public Evento Evento { get; set; }
        public byte[] Imagen { get; set; }
    }
}

using WebApplicationAPIDemo.Model;

namespace WebAplicationAPIRestDemo.DAL.Model
{
    public class ItemKanBan
    {
        public long id {  get; set; }
        public string tasca { get; set; }
        public string estat { get; set; }
        public string color { get; set; }
        public string dataStart { get; set; }
        public string dataFinish { get; set; }
        public Responsable Responsable { get; set; }
    }
}

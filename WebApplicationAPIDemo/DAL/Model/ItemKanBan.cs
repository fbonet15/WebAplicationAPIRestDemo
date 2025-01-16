using WebApplicationAPIDemo.Model;

namespace WebAplicationAPIRestDemo.DAL.Model
{
    public class ItemKanBan
    {
        public long _id {  get; set; }
        public string _tasca { get; set; }
        public string _estat { get; set; }
        public string _color { get; set; }
        public string _dataStart { get; set; }
        public string _dataFinish { get; set; }
        public Responsable _responsable { get; set; }
    }
}

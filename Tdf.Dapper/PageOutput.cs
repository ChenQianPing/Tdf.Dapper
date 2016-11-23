using System.Collections;

namespace Tdf.Dapper
{
    public class PageOutput
    {
        public int Total { get; set; }
        public IEnumerable Records { get; set; }
    }
}

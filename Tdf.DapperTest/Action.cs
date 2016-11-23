using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.DapperTest
{
    [Table("Act_Action")]
    public class Action
    {
        [Key]
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }
        public int ActionValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace HelloSap
{

    [Table("Stotras")]
    class Stotra
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Name { get; set; }
        [MaxLength(10000)]
        public string Content { get; set; }
    }

    class StotraInternal
    {
        public StotraInternal(string name, Type targetType)
        {
            this.Name = name;
            this.TargetType = targetType;
        }

        public string Name { private set; get; }

        public Type TargetType { get; set; }
    }
}

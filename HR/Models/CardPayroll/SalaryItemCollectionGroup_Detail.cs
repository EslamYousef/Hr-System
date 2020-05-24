using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.CardPayroll
{
    [Table("SalaryItemCollectionGroup_Detail")]
    public class SalaryItemCollectionGroup_Detail
    {
        [Key]
        public int ID { get; set; }
        public string CollectionId { get; set; }
        public string CodeGroupID { get; set; }
        public Nullable<int> SortIndex { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public virtual SalaryItemCollectionGroup_Header SalaryItemCollectionGroup_Header { get; set; }
        public string CodeGroupDescription { get; set; }

    }
}
using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }

        [Display(Name = "Status")]
        public SaleStatus StatusId { get; set; }

        public virtual Seller Seller { get; set; }

        [Display(Name = "Seller")]
        public int SellerId { get; set; }

        public SalesRecord()
        {
        }

        public SalesRecord(DateTime date, double amount, SaleStatus statusId, Seller seller)
        {
            Date = date;
            Amount = amount;
            StatusId = statusId;
            SellerId = seller.Id;
        }
    }
}

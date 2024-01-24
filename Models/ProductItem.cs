using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock_Invoice.Models;

public partial class ProductItem
{
    public int PId { get; set; }

    public  string?  PName { get; set; }

    public  decimal? Cgst { get; set; }

    public  decimal? Sgst { get; set; }

    public  decimal? PPrice { get; set; }

    public  int? PQuantity { get; set; }

    public DateTime? ExpDate { get; set; }
}

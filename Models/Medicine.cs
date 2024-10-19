namespace hospital.Models;
using System.ComponentModel.DataAnnotations;

public class medicine
{
    [Key]
    public int EId { get; set; }

    public string Ename { get; set; }

    public string Eprize { get; set; }

    public string ImageUrl3 { get; set; }
    public Nullable<int> flag { get; set; }
    public string Eavailability { get; set; }
    public string Edescription { get; set; }
    public string Edetails { get; set; }
}
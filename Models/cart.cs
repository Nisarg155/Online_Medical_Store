namespace hospital.Models;
using System.ComponentModel.DataAnnotations;
public class cart
{
    [Key]
    public int Cid { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public Nullable<int> qty { get; set; }
    public Nullable<int> price { get; set; }
    public Nullable<int> bill { get; set; }
    public string Cemail { get; set; }
}
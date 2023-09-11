
#pragma warning disable CS8618  //#1

using System.ComponentModel.DataAnnotations;  //connected to line 11 this will bring itself in when iadd the KEY

namespace Delicious.Models; //#2

public class Dish
{
    [Key]
    public int DishID { get; set; }


    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Chef Name is required.")]
    public string ChefName { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Calories is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0.")]
    public int Calories { get; set; }

    [Required(ErrorMessage = "Tastiness is required.")]
    [Range(1, 5, ErrorMessage = "Tastiness must be between 1 and 5.")]
    public int Tastiness { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

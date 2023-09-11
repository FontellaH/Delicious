// #12 Set up the Pet Controller and copy everything from the Home Controller to this file... update the controller to pet istead of home

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Delicious.Models;

namespace Delicious.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;


    private DishContext _context;  //// #1 add the connection here

    public DishController(ILogger<DishController> logger, DishContext context) //#2 add MyContext to constructor, and to the feild
    {
        _logger = logger;
        _context = context;  // #3 add this to connect
    }





    //This is the Home Page
    [HttpGet("")]
    public IActionResult Dashboard()
    {
        //Gets a list of dishes from my data source and order them by CreatedAt
        List<Dish> sortedDishesByCreatedAt = _context.Dishs.OrderByDescending(d => d.CreatedAt).ToList();


        return View(sortedDishesByCreatedAt);   // Pass the ordered collection to the view
    }



    //Create Start Point
    [HttpGet("dishes/new")]
    public ViewResult NewDish()
    {
        return View();
    }

    //Creating new dish
    [HttpPost("dishes/new")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("NewDish", newDish);
    }


    //View one page

    [HttpGet("dishes/{id}")]
    public IActionResult ViewDish(int id)
    {
        var dish = _context.Dishs.Find(id);
        if (dish == null)
        {
            return NotFound();
        }
        return View(dish);
    }


    ///DeleteRoute

    [HttpPost("dishes/delete/{id}", Name = "DeleteDish")]
    public IActionResult DeleteDish(int id)
    {
        var dishToDelete = _context.Dishs.Find(id);

        if (dishToDelete == null)
        {
            return NotFound(); // Dish not found
        }

        _context.Dishs.Remove(dishToDelete);
        _context.SaveChanges();

        return RedirectToAction("Dashboard"); // Redirect to the dashboard 
    }


    //Edit Route

    [HttpGet("dishes/edit/{id}", Name = "EditDish")]
    public IActionResult EditDish(int id)
    {
        var dishToEdit = _context.Dishs.Find(id);
        if (dishToEdit == null)
        {
            return NotFound();
        }
        return View(dishToEdit);
    }

    [HttpPost("dishes/update/{id}", Name = "UpdateDish")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateDish(Dish updatedDish)
    {
        if (ModelState.IsValid)
        {
            var existingDish = _context.Dishs.Find(updatedDish.DishID);
            if (existingDish == null)
            {
                return NotFound(); // Dish not found
            }

            // Update the existing dish with values from updatedDish
            existingDish.ChefName = updatedDish.ChefName;
            existingDish.Name = updatedDish.Name;
            existingDish.Calories = updatedDish.Calories;
            existingDish.Tastiness = updatedDish.Tastiness;
            existingDish.Description = updatedDish.Description;

            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction("Dashboard"); // Redirect to the dashboard with the updated information
        }

        // If ModelState is not valid, return to the edit view with errors
        return View("EditDish", updatedDish);
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


//#13 set up the index.cshtml in View/Home
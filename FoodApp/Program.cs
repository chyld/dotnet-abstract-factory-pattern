using System;
using System.Collections.Generic;

namespace FoodApp
{
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    public interface IMealPlanFactory {
        public IMenu GenerateLunchesMenu();
        public IMenu GenerateDessertsMenu();
        public IShoppingList GenerateShoppingList();
    }

    public interface IMenu {
        public void PrintMenu();
    }

    public interface IShoppingList {
        public List<string> MakeShoppingList();
    }
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    public class KetoDessertMenu : IMenu {
        public void PrintMenu()
            => Console.WriteLine("Peanut butter chocolate bars, sugar-free cheesecake");
    }
    public class KetoLunchMenu : IMenu {
        public void PrintMenu()
            => Console.WriteLine("Scrambled Eggs, Creamed Spinach, Guacamole");
    }
    public class KetoShoppingList: IShoppingList {
        public List<string> MakeShoppingList()
            => new() { "butter", "meat", "kale", "peanut butter", "dark chocolate", "ricotta" };
    }
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    public class VegDessertMenu : IMenu {
        public void PrintMenu()
            => Console.WriteLine("Brownie, Orange Shebert, Blackberry Crisp");
    }
    public class VegLunchMenu : IMenu {
        public void PrintMenu()
            => Console.WriteLine("Black Bean Soup, Green Curry, Salad");
    }
    public class VegShoppingList: IShoppingList {
        public List<string> MakeShoppingList()
            => new() { "black beans, spices, kale, cucumber", "mangoes", "apples", "pears" };
    }
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    public class KetoMealPlanFactory : IMealPlanFactory {
        public IMenu GenerateDessertsMenu() {
            return new KetoDessertMenu();
        }

        public IMenu GenerateLunchesMenu() {
            return new KetoLunchMenu();
        }

        public IShoppingList GenerateShoppingList() {
            return new KetoShoppingList();
        }
    }
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    public class VegMealPlanFactory : IMealPlanFactory {
        public IMenu GenerateDessertsMenu() {
            return new VegDessertMenu();
        }

        public IMenu GenerateLunchesMenu() {
            return new VegLunchMenu();
        }

        public IShoppingList GenerateShoppingList() {
            return new VegShoppingList();
        }
    }
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------------------- //
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Welcome to Meal Planner.");
            Console.Write("Enter preferred meal type (keto/veg): ");
            var customerDiet = Console.ReadLine();
            var factory = GetFactoryForDietType(customerDiet);
            var lunch = factory.GenerateLunchesMenu();
            var dessert = factory.GenerateDessertsMenu();
            var shoppingList = factory.GenerateShoppingList();

            Console.WriteLine("Lunch Menu");
            lunch.PrintMenu();
            Console.WriteLine("Dessert Menu");
            dessert.PrintMenu();
            Console.WriteLine("Shopping List");
            shoppingList.MakeShoppingList().ForEach(item => Console.WriteLine(item));
        }

        public static IMealPlanFactory GetFactoryForDietType(string dietType) {
            switch(dietType){
                case "keto":
                    return new KetoMealPlanFactory();
                    break;
                case "veg":
                    return new VegMealPlanFactory();
                    break;
                default:
                    return new VegMealPlanFactory();
            }
        }
    }
}

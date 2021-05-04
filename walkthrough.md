### What is a Factory Pattern

The factory pattern is a way of replacing manual object instantiation with delegation to a class whose purpose is to create objects.

### What is the Abstract Factory Pattern

The Abstract Factory Pattern provides a way to encapsulate a group of individual factories that have a common theme without specifying their concrete classes. In normal usage, the client software creates a concrete implementation of the abstract factory and then uses the generic interface of the factory to create the concrete objects that are part of the theme. The client does not know or care which concrete objects it gets from each of these internal factories, since it uses only the generic interfaces of their products. This pattern separates the details of implementation of a set of objects from their general usage and relies on object composition, as object creation is implemented in methods exposed in the factory interface.

The best way to think of the Abstract factory pattern, is that it is a super factory, or a *factory of factories*. Typically it is an interface which is responsible for creating a factory of related objects without explicitly specifying the derived classes.

### Overview

This example uses the Abstract Factory creational pattern to help fulfill a meal planning application for customers who wish to follow a specific type of diet.  Depending on the customer's diet, a different Meal Plan is generated, which contains methods in this example for generating lunch or dessert menus, which provide lists of ingredients, meals, and diet summaries. One of the benefits of this approach is that all of the data associated with a specific diet, such as the foods involved in meal prep and grocery lists, can belong to specific factories, increasing the ability to ensure compatibility across the various products of a MealPlanFactory.

## Code

Create the `IMenu` and `IShoppingList` interfaces. The client will interact with the product through these interfaces.

```csharp
    public interface IMenu {
        public void PrintMenu();
    }

    public interface IShoppingList {
        public List<string> MakeShoppingList();
    }
```

Here we create the `Keto` product, consisting of a Dessert and Lunch Menu. Also contains a Shopping List.

```csharp
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
```

Now, we create the `Veg` product, also consisting of a Dessert and Lunch Menu ... and Shopping List.

```csharp
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
```

Now that we have products, we need a mechanism to create them. We will use the `IMealPlanFactory` interface, which when implemented, will allow for us to create a variety of product flavors. Here we have three methods that need to be implemented.

```csharp
    public interface IMealPlanFactory {
        public IMenu GenerateLunchesMenu();
        public IMenu GenerateDessertsMenu();
        public IShoppingList GenerateShoppingList();
    }
```

The `KetoMealPlanFactory` is a concrete representation of the `IMealPlanFactory` interface. With this class, we will be able to create a Desert Menu, Lunch Menu and Shopping List. Notice that the return type of each of these methods is an `interface`.

```csharp
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
```

Just as above, we will do the same for the `VegMealPlanFactory`.

```csharp
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
```

Now it is time to put all the ingredients together and create a simple console-based application.

* We ask the user for their desired meal type (keto or veg)
* We then get a specfic factory that represents the requested meal type
* Once we have a factory, we call the three methods on it to get a Dessert Menu, Lunch Menu an Shopping List
* We then display Menus and the Shopping List

```csharp
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
```

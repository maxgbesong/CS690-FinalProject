namespace FinalProject;

using Spectre.Console;

public class ConsoleUI
{
    DataManager dataManager;

    public ConsoleUI()
    {
        dataManager = new DataManager();
    }

    public void Show()
    {
        var role = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Welcome to Elena's Community Kitchen!\n\nPlease select your role:")
            .AddChoices(new[]
            {
                nameof(Role.Volunteer),
                nameof(Role.Manager)
            }));
        
        if (role == nameof(Role.Volunteer))
        {
            HandleVolunteerMenu();
        }
        else if (role == nameof(Role.Manager))
        {
            HandleManagerMenu();
        }
    }

    public void HandleVolunteerMenu()
    {
        string menuOption = "";
        do
        {
            menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select an option:")
                .AddChoices(new[]
                {
                    "View families",
                    "Assign meals",
                    "Exit"
                }));
            
            if (menuOption == "View families")
            {
                HandleVolunteerViewFamilies();
            }
            else if (menuOption == "Assign meals")
            {
                HandleVolunteerAssignMeals();
            }
        }
        while (menuOption != "Exit");
    }

    public void HandleVolunteerViewFamilies()
    {
        DisplayAllFamilies();
    }

    public void HandleVolunteerAssignMeals()
    {
        List<Family> unassignedFamilies = Reporter.GetUnassignedFamilies(dataManager.families);

        if (unassignedFamilies.Count == 0)
        {
            AnsiConsole.WriteLine("All families have received meals today!");
        }
        else
        {
            var family = AnsiConsole.Prompt(
                new SelectionPrompt<Family>()
                .Title("Select a family:")
                .AddChoices(unassignedFamilies));
            
            AnsiConsole.WriteLine("Assigning meals for the " + family.name + " family.");
            AnsiConsole.WriteLine("Total meals needed: " + Reporter.CountFamilyMeals(family).ToString());

            AnsiConsole.WriteLine("Breakdown of meals needed by dietary restrictions:");
            var table = new Table();
            table.AddColumn("Dietary Type");
            table.AddColumn("Meals Needed");
            foreach(DietType diet in Enum.GetValues(typeof(DietType)))
            {
                if (Reporter.CountFamilyMealsForDietType(family, diet) > 0)
                {
                    table.AddRow(diet.ToString(), Reporter.CountFamilyMealsForDietType(family, diet).ToString());
                }
            }
            AnsiConsole.Write(table);
            
            AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Confirm that all dietary restrictions have been observed before continuing.")
                    .AddChoices(new[]
                    {
                        "Assign meals"
                    }));
            
            family.assignedMeal = true;
        }
    }

    public void HandleManagerMenu()
    {
        string menuOption = "";
        do
        {
            menuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select an option:")
                .AddChoices(new[]
                {
                    "Track families",
                    "Track meals",
                    //"Track ingredients",
                    "Exit"
                }));
            
            if (menuOption == "Track families")
            {
                HandleManagerTrackFamilies();
            }
            else if (menuOption == "Track meals")
            {
                HandleManagerTrackMeals();
            }
            else if (menuOption == "Track ingredients")
            {
                HandleManagerTrackIngredients();
            }
        }
        while (menuOption != "Exit");
    }

    public void HandleManagerTrackFamilies()
    {
        var familyOption = "";
        do
        {
            familyOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select an option:")
                .AddChoices(new[]
                {
                    "View families",
                    "Add family",
                    "Remove family",
                    "Back to manager menu"
                }));
            
            if (familyOption == "View families")
            {
                DisplayAllFamilies();
            }
            else if (familyOption == "Add family")
            {
                var familyName = AnsiConsole.Prompt(new TextPrompt<string>("Enter family name: "));

                List<FamilyMember> members = new List<FamilyMember>();
                string moreMembers = "Yes";
                do
                {
                    var memberName = AnsiConsole.Prompt(new TextPrompt<string>("Enter family member name: "));
                    var memberDiet = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Select: " + memberName + "'s dietary restrictions:")
                        .AddChoices(new[]
                        {
                            nameof(DietType.NoRestrictions),
                            nameof(DietType.LactoseFree),
                            nameof(DietType.GlutenFree),
                            nameof(DietType.NutFree),
                            nameof(DietType.Vegetarian),
                            nameof(DietType.Vegan),
                            nameof(DietType.Kosher)
                        }));
                    
                    members.Add(new FamilyMember(memberName, (DietType)Enum.Parse(typeof(DietType), memberDiet)));

                    moreMembers = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Add another family member?")
                        .AddChoices(new[]
                        {
                            "Yes",
                            "No"
                        }));
                }
                while (moreMembers == "Yes");

                dataManager.AddFamily(new Family(familyName, members));
                AnsiConsole.WriteLine("Added " + familyName + " family.");
            }
            else if (familyOption == "Remove family")
            {
                if (dataManager.families.Count > 0)
                {
                    var family = AnsiConsole.Prompt(
                        new SelectionPrompt<Family>()
                        .Title("Select a family to remove:")
                        .AddChoices(dataManager.families));
                    dataManager.removeFamily(family);
                }
                else
                {
                    AnsiConsole.WriteLine("There are no recorded families yet.");
                }
            }
        }
        while (familyOption != "Back to manager menu");
    }

    public void HandleManagerTrackMeals()
    {
        AnsiConsole.WriteLine("Total meals still needed today: " + Reporter.CountTotalMeals(dataManager.families).ToString());

        AnsiConsole.WriteLine("Breakdown of meals needed by dietary restrictions:");
        var table = new Table();
        table.AddColumn("Dietary Type");
        table.AddColumn("Meals Needed");
        foreach(DietType diet in Enum.GetValues(typeof(DietType)))
        {
            table.AddRow(diet.ToString(), Reporter.CountTotalMealsForDietType(dataManager.families, diet).ToString());
        }
        AnsiConsole.Write(table);
    }

    public void HandleManagerTrackIngredients()
    {
        
    }

    public void DisplayAllFamilies()
    {
        var table = new Table();
        table.AddColumn("Family Name");
        table.AddColumn("Received Meal");
        table.AddColumn("Number of Members");
        table.AddColumn("Family Members");
        foreach(var family in dataManager.families)
        {
            table.AddRow(family.name, family.assignedMeal.ToString(), family.members.Count.ToString(), family.listMemberNames());
        }
        AnsiConsole.Write(table);
    }
}
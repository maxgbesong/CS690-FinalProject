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
            string menuOption = "";
            do
            {
                menuOption = "Exit"; // change this
            }
            while (menuOption != "Exit");
        }
        else if (role == nameof(Role.Manager))
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
                        "Track ingredients",
                        "Exit"
                    }));
                
                if (menuOption == "Track families")
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
                        }
                        else if (familyOption == "Remove family")
                        {
                            
                        }
                    }
                    while (familyOption != "Back to manager menu");
                }
                else if (menuOption == "Track meals")
                {
                    
                }
                else if (menuOption == "Track ingredients")
                {
                    
                }
            }
            while (menuOption != "Exit");
        }
    }
}
namespace FinalProject;

public class Reporter
{
    public static int CountTotalMeals(List<Family> families)
    {
        int totalMeals = 0;
        foreach (Family family in families)
        {
            if (!family.assignedMeal)
            {
                totalMeals += family.members.Count;
            }
        }
        return totalMeals;
    }

    public static int CountTotalMealsForDietType(List<Family> families, DietType diet)
    {
        int count = 0;
        foreach (Family family in families)
        {
            if (!family.assignedMeal)
            {
                foreach (FamilyMember member in family.members)
                {
                    if (member.dietType == diet)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public static int CountFamilyMeals(Family family)
    {
        if (family.assignedMeal)
        {
            return 0;
        }
        return family.members.Count;
    }

    public static int CountFamilyMealsForDietType(Family family, DietType diet)
    {
        if (family.assignedMeal)
        {
            return 0;
        }

        int count = 0;
        foreach (FamilyMember member in family.members)
        {
            if (member.dietType == diet)
            {
                count++;
            }
        }
        return count;
    }

    public static List<Family> GetUnassignedFamilies(List<Family> families)
    {
        List<Family> unassignedFamilies = new List<Family>();

        foreach (Family family in families)
        {
            if (!family.assignedMeal)
            {
                unassignedFamilies.Add(family);
            }
        }

        return unassignedFamilies;
    }
}
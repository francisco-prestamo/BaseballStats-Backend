namespace Entities;

class Series
{
    int ID; // primary key
    
    // Season Relation
    int SeasonId;
    Season Season;

    string Name;
    string Type;
    
    Date StartDate;
    Date EndDate;
}
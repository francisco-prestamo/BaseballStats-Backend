namespace Entities;

class Game
{
    int Team1ID; // primary key attribute 1
    Team Team1;

    int Team2ID; // primary key attribute 2
    Team Team2;

    Date Date; // primary key attribute 3

    bool Winner1;
    int Runs1;
    int Runs2;

    // Series Relation
    int SeriesID;
    Series Series;
}
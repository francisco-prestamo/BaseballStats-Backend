namespace Entities;

class PlayerInSeries
{
    int PlayerID; // primary key attribute 1;
    Player Player;

    int SeriesID; // primary key attribute 2;
    Series Series;

    int? TeamID;
    Team Team;
}
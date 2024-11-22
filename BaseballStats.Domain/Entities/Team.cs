namespace Entities;

class Team
{
    int ID; // primary key
    string Name;
    string Initials;
    string RepresentedEntity;
    string Color;


    // Technical Director Relation
    int TechnicalDirectorId;
    TechnicalDirector TechnicalDirector;
}
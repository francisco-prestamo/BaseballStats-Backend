using BaseballStats.Application.Features.Team.GetTeamStarPlayers;
using BaseballStats.Application.Utilities;
using FastEndpoints;
using iText.Layout.Borders;
using iText.Layout.Element;

namespace BaseballStats.Application.Features.Reports.TeamStarPlayers;

public class TeamStarPlayersCommandHandler : CommandHandler<TeamStarPlayersCommand, FileInfo>
{
    public override async Task<FileInfo> ExecuteAsync(TeamStarPlayersCommand command, CancellationToken ct = new CancellationToken())
    {
        var data = await new GetTeamStarPlayersCommand()
        {
            SeasonId = command.SeasonId,
            SeriesId = command.SeriesId,
            TeamId = command.TeamId
        }.ExecuteAsync(ct);

        var document = DocumentTools.OpenDocument("reports", "team-star-players.pdf");

        document.AddTitle("Star Players");

        document.AddLine();

        document.Add(new Paragraph("\n"));

        var table = new Table(2);
        table.AddCell(new Cell().Add(new Paragraph("Season: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(command.SeasonId.ToString())).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Series: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(command.SeriesId.ToString())).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Team: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(command.TeamId.ToString())).SetBorder(Border.NO_BORDER));

        document.Add(table);

        document.Add(new Paragraph("\n"));

        table = new Table(7).UseAllAvailableWidth();
        table.AddCell("Id");
        table.AddCell("Name");
        table.AddCell("Age");
        table.AddCell("Years of Experience");
        table.AddCell("Batting Average");
        table.AddCell("Position");
        table.AddCell("Effectiveness");

        foreach (var player in data)
        {
            table.AddCell(player.Player.Id.ToString());
            table.AddCell(player.Player.Name);
            table.AddCell(player.Player.Age.ToString());
            table.AddCell(player.Player.YearsOfExperience.ToString());
            table.AddCell(player.Player.BattingAverage?.ToString("0.000"));
            table.AddCell(player.Position);
            table.AddCell(player.Effectiveness.ToString("0.000"));
        }

        document.Add(table);

        document.Close();

        return new FileInfo("reports/team-star-players.pdf");
    }
}
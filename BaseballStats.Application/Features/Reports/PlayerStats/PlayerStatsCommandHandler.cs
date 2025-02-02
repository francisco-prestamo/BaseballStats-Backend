using BaseballStats.Application.Utilities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using iText.Layout.Borders;
using iText.Layout.Element;

namespace BaseballStats.Application.Features.Reports.PlayerStats;

public class PlayerStatsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PlayerStatsCommand, FileInfo>
{
    public override async Task<FileInfo> ExecuteAsync(PlayerStatsCommand command, CancellationToken ct = default(CancellationToken))
    {
        await DatabaseValidations(command);

        var player = (await unitOfWork.Repository<Domain.Entities.Player>().GetByIdAsync(command.PlayerId))!;

        var data = GetData(command);
        
        var docName = "player-stats.pdf";

        var document = DocumentTools.OpenDocument("reports", docName);
    
        document.AddTitle("Player Stats For " + player.Name);

        document.AddLine();

        document.Add(new Paragraph("\n"));

        var table = new Table(2).UseAllAvailableWidth();

        table.AddCell(new Cell().Add(new Paragraph("Player Name: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(player.Name.ToString())).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Age: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(player.Age.ToString())).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Years of Experience: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(player.YearsOfExperience.ToString())).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Batting Average: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(player.BattingAverage.HasValue ? player.BattingAverage.Value.ToString() : "Not Defined")).SetBorder(Border.NO_BORDER));

        document.Add(table);

        document.Add(new Paragraph("\n"));

        document.Add(new Paragraph("Teams Played in by Series:"));

        table = new Table(3);
        table.AddCell(new Cell().Add(new Paragraph("Series Name")));
        table.AddCell(new Cell().Add(new Paragraph("Series Start Date")));
        table.AddCell(new Cell().Add(new Paragraph("Team Name")));

        foreach (var pis in data)
        {
            table.AddCell(new Cell().Add(new Paragraph(pis.SeriesName.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(pis.SeriesStartDate.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(pis.TeamName.ToString())));                
        }

        document.Add(table);

        document.Close();

        return new FileInfo("reports/" + docName);
    }

    private List<(string SeriesName, DateOnly SeriesStartDate, string TeamName)> GetData(PlayerStatsCommand command)
    {
        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;
        var team_table = unitOfWork.Repository<Domain.Entities.Team>().DbSet;

        var data = (
            from pis in playerInSeries_table
            join s in series_table on pis.SeriesId equals s.Id
            join t in team_table on pis.TeamId equals t.Id
            where pis.PlayerId == command.PlayerId
            select new {
                SeriesName = s.Name,
                SeriesStartDate = s.StartDate,
                TeamName = t.Name
            }
        ).ToList().Select(x => (x.SeriesName, x.SeriesStartDate, x.TeamName)).ToList();

        return data;

    }

    private async Task DatabaseValidations(PlayerStatsCommand command)
    {
        var player = await unitOfWork.Repository<Domain.Entities.Player>().GetByIdAsync(command.PlayerId);
        if (player == null)
            ThrowError("Player not found");
    }
}


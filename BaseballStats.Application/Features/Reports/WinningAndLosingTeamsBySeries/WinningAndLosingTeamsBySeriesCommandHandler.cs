using BaseballStats.Application.Utilities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using iText.Layout.Element;

namespace BaseballStats.Application.Features.Reports.WinningAndLosingTeamsBySeries;

public class WinningAndLosingTeamsBySeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<WinningAndLosingTeamsBySeriesCommand, FileInfo>
{
    public override async Task<FileInfo> ExecuteAsync(WinningAndLosingTeamsBySeriesCommand command, CancellationToken ct = default(CancellationToken))
    { 
        var data = GetData();
        var document = DocumentTools.OpenDocument("reports", "WinningAndLosingTeamsBySeriesReport.pdf"); 

        document.AddTitle("Winning And Losing Teams By Series Report");

        document.AddLine();

        document.Add(new Paragraph("\n"));

        var table = new Table(5);

        table.AddCell(new Cell().Add(new Paragraph("Series Name")));
        table.AddCell(new Cell().Add(new Paragraph("Series Type")));
        table.AddCell(new Cell().Add(new Paragraph("Series Start Date")));
        table.AddCell(new Cell().Add(new Paragraph("Winning Team")));
        table.AddCell(new Cell().Add(new Paragraph("Losing Team")));

        foreach (var stats in data)
        {
            table.AddCell(new Cell().Add(new Paragraph(stats.SeriesName)));
            table.AddCell(new Cell().Add(new Paragraph(stats.SeriesType)));
            table.AddCell(new Cell().Add(new Paragraph(stats.SeriesStartDate.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(stats.WinningTeamName)));

            table.AddCell(new Cell().Add(new Paragraph(stats.LosingTeamName)));
        }

        document.Add(table);

        document.Close();

        await Task.CompletedTask;

        return new FileInfo("reports/WinningAndLosingTeamsBySeriesReport.pdf");
    }

    private List<(long SeriesId, string SeriesName, string SeriesType, DateOnly SeriesStartDate, long WinningTeamId, string WinningTeamName, long LosingTeamId, string LosingTeamName)> GetData()
    {
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;
        var team_table = unitOfWork.Repository<Domain.Entities.Team>().DbSet;

        var team1Data = (
            from game in game_table
            group game by new { game.Team1Id, game.SeriesId } into g
            select new
            {
                TeamId = g.Key.Team1Id,
                g.Key.SeriesId,
                Wins = g.Count(x => x.Winner1),
                Losses = g.Count(x => !x.Winner1)
            }
        );

        var team2Data = (
            from game in game_table
            group game by new { game.Team2Id, game.SeriesId } into g
            select new
            {
                TeamId = g.Key.Team2Id,
                g.Key.SeriesId,
                Wins = g.Count(x => !x.Winner1),
                Losses = g.Count(x => x.Winner1)
            }
        );

        var teamResults = (
            from teamInSeriesDuplicate in team1Data.Union(team2Data)
            group teamInSeriesDuplicate by new { teamInSeriesDuplicate.SeriesId, teamInSeriesDuplicate.TeamId } into g
            select new
            {
                g.Key.SeriesId,
                g.Key.TeamId,
                Wins = g.Sum(x => x.Wins),
                Losses = g.Sum(x => x.Losses)
            }
        );

        var seriesResults = (
            from teamresult in teamResults
            group teamresult by teamresult.SeriesId into g
            select new
            {
                SeriesId = g.Key,
                WinningTeamId = g.OrderByDescending(x => x.Wins).ThenBy(x => x.Losses).First().TeamId,
                LosingTeamId = g.OrderByDescending(x => x.Losses).ThenBy(x => x.Wins).First().TeamId
            }
        );

        var data = (
            from seriesResult in seriesResults
            join series in series_table on seriesResult.SeriesId equals series.Id
            join winningTeam in team_table on seriesResult.WinningTeamId equals winningTeam.Id
            join losingTeam in team_table on seriesResult.LosingTeamId equals losingTeam.Id
            orderby series.StartDate descending
            select new {
                seriesResult.SeriesId,
                SeriesName = series.Name,
                SeriesType = series.Type,
                series.StartDate,
                seriesResult.WinningTeamId,
                WinningTeamName = winningTeam.Name,
                seriesResult.LosingTeamId,
                LosingTeamName = losingTeam.Name
            }
        ).ToList().Select(x => (x.SeriesId, x.SeriesName, x.SeriesType, x.StartDate, x.WinningTeamId, x.WinningTeamName, x.LosingTeamId, x.LosingTeamName)).ToList();
        return data;
    }
}


using BaseballStats.Application.Utilities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Reports.WinTeamsBySeries;

public class WinTeamsBySeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<WinTeamsBySeriesCommand, FileInfo>
{
    public override async Task<FileInfo> ExecuteAsync(WinTeamsBySeriesCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidationsAsync(command);

        var data = await GetData(command);

        var document = DocumentTools.OpenDocument("reports", "WinTeamsBySeriesReport.pdf");

        document.AddTitle("Win Teams By Series Report");

        document.AddLine();

        document.Add(new Paragraph("\n"));

        var table = new Table(2);
        table.AddCell(new Cell().Add(new Paragraph("Season: ")).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph(data.SeasonId.ToString())).SetBorder(Border.NO_BORDER));

        document.Add(table);

        document.Add(new Paragraph("\n"));

        table = new Table(6);
        table.AddCell(new Cell().Add(new Paragraph("Serie Id")));
        table.AddCell(new Cell().Add(new Paragraph("Serie Name")));
        table.AddCell(new Cell().Add(new Paragraph("Team Id")));
        table.AddCell(new Cell().Add(new Paragraph("Team Name")));
        table.AddCell(new Cell().Add(new Paragraph("Win Games")));
        table.AddCell(new Cell().Add(new Paragraph("Technical Director")));

        foreach (var stats in data.SeriesStatsDto)
        {
            table.AddCell(new Cell().Add(new Paragraph(stats.SerieId.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(stats.SerieName)));
            table.AddCell(new Cell().Add(new Paragraph(stats.TeamId.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(stats.TeamWinnerName)));
            table.AddCell(new Cell().Add(new Paragraph(stats.WinGames.ToString())));
            table.AddCell(new Cell().Add(new Paragraph(stats.TechnicalDirector)));
        }

        document.Add(table);

        document.Close();

        return new FileInfo("reports/WinTeamsBySeriesReport.pdf");
    }

    private async Task DatabaseValidationsAsync(WinTeamsBySeriesCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);
        if (season == null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);
    }

    private async Task<WinTeamsBySeriesDto> GetData(WinTeamsBySeriesCommand command)
    {
        var series = unitOfWork.Repository<Domain.Entities.Series>().DbSet;
        var games = unitOfWork.Repository<Domain.Entities.Game>().DbSet;

        var gamesInSeason =
            (from serie in series
                where serie.SeasonId == command.SeasonId
                join game in games on serie.Id equals game.SeriesId
                select new { serie, game }).ToList();

        foreach (var x in gamesInSeason)
        {
            Console.WriteLine(x.serie.Id + " " + x.game.Team1Id + " " + x.game.Team2Id + " " + x.game.Winner1);
        }

        var seriesIds = gamesInSeason.DistinctBy(x => x.serie.Id).Select(x => new { x.serie.Id, x.serie.Name }).ToList();

        var data = new WinTeamsBySeriesDto()
        {
            SeasonId = command.SeasonId
        };

        foreach (var element in seriesIds)
        {
            var seriesId = element.Id;
            var serieName = element.Name;

            var winnerTeamId = gamesInSeason.Where(x => x.serie.Id == seriesId)
                .GroupBy(x => x.game.Winner1 ? x.game.Team1Id : x.game.Team2Id)
                .OrderByDescending(x => x.Count()).FirstOrDefault()?.Key;

            if (winnerTeamId is null)
                continue;

            var winGames = gamesInSeason.Where(x => x.serie.Id == seriesId)
                .GroupBy(x => x.game.Winner1 ? x.game.Team1Id : x.game.Team2Id)
                .OrderByDescending(x => x.Count()).FirstOrDefault()!.Count();

            var team = await unitOfWork.Repository<Domain.Entities.Team>().GetByIdAsync(winnerTeamId);
            var technicalDirector = await unitOfWork.Repository<Domain.Entities.Identity.TechnicalDirector>().GetByIdAsync(team!.TechnicalDirectorId);

            var seriesStatsDto = new SeriesStatsDto
            {
                SerieId = seriesId,
                SerieName = serieName,
                TeamId = winnerTeamId.Value,
                TeamWinnerName = team.Name,
                WinGames = winGames,
                TechnicalDirector = technicalDirector!.Username
            };

            data.SeriesStatsDto.Add(seriesStatsDto);
        }

        data.SeriesStatsDto.Sort((x, y) => x.SerieId.CompareTo(y.SerieId));

        return data;
    }
}
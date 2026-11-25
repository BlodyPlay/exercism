using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    private class TeamStats
    {
        public string Name { get; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        public int MatchesPlayed => Wins + Draws + Losses;
        public int Points => Wins * 3 + Draws;

        public TeamStats(string name)
        {
            Name = name;
        }
    }

    public static void Tally(Stream inStream, Stream outStream)
    {
        var teams = new Dictionary<string, TeamStats>();

        using (var reader = new StreamReader(inStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(';');
                string team1 = parts[0];
                string team2 = parts[1];
                string result = parts[2];

                if (!teams.ContainsKey(team1))
                {
                    teams[team1] = new TeamStats(team1);
                }

                if (!teams.ContainsKey(team2))
                {
                    teams[team2] = new TeamStats(team2);
                }

                switch (result)
                {
                    case "win":
                        teams[team1].Wins++;
                        teams[team2].Losses++;
                        break;
                    case "loss":
                        teams[team1].Losses++;
                        teams[team2].Wins++;
                        break;
                    case "draw":
                        teams[team1].Draws++;
                        teams[team2].Draws++;
                        break;
                }
            }
        }

        var sortedTeams = teams.Values
            .OrderByDescending(t => t.Points)
            .ThenBy(t => t.Name, StringComparer.Ordinal);

        using (var writer = new StreamWriter(outStream, new UTF8Encoding(false), bufferSize: 1024, leaveOpen: true))
        {
            writer.Write($"{"Team",-30} | {"MP",2} | {"W",2} | {"D",2} | {"L",2} | {"P",2}");

            foreach (var team in sortedTeams)
            {
                writer.Write($"\n{team.Name,-30} | {team.MatchesPlayed,2} | {team.Wins,2} | {team.Draws,2} | {team.Losses,2} | {team.Points,2}");
            }
        }
    }
}
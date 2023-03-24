// See https://aka.ms/new-console-template for more information

using Architecture.Diagrams;
using C4Sharp.Elements.Plantuml.IO;

public static class Program
{
    public static void Main(string[] args)
    {
        var diagrams = new[] { new BattleGameOverview().Build() };

        var path = Path.Join("..", "..", "..", "..", "assets");

        new PlantumlContext()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            //.UseStandardLibraryBaseUrl()
            .Export(path, diagrams);
    }
}
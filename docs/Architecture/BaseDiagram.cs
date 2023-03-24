using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Elements.Plantuml;
using C4Sharp.Elements.Plantuml.Constants;

namespace Architecture;

public abstract class BaseDiagram: DiagramBuildRunner
{
    protected override IElementStyle? SetStyle()
    {
        return new ElementStyle()
            .UpdateElementStyle(ElementName.Person, "#000000", "#000000")
            .UpdateElementStyle(ElementName.Container, "#ffffff", "#000000", "#000000", false, Shape.RoundedBoxShape)            
            .UpdateElementStyle(ElementName.Component, "#ffffff", "#000000", "#000000", false, Shape.RoundedBoxShape)
            .UpdateElementStyle(ElementName.Container, "#ffffff", "#000000", "#000000", false, Shape.RoundedBoxShape)
            .UpdateElementStyle(ElementName.ExternalSystem, "#f4f4f4", "#000000", "#000000", false, Shape.RoundedBoxShape);
    }

    protected override IElementTag? SetTags()
    {
        return new ElementTag()
            .AddElementTag("microservice", "#f4f4f4", "#000000", "#000000", false, Shape.EightSidedShape);
    }    
}
﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Faultr.Internal;

internal sealed class MethodReceiver : ISyntaxReceiver
{
    public List<MethodDeclarationSyntax> MethodCandidates { get; } = new();
    public List<MethodDeclarationSyntax> AttributedMethodCandidates { get; } = new();
    public List<PropertyDeclarationSyntax> PropertyCandidates { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is MethodDeclarationSyntax methodDeclaration)
        {
            if (methodDeclaration.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.ToString().Contains("AddExceptions"))))
            {
                AttributedMethodCandidates.Add(methodDeclaration);
            }

            MethodCandidates.Add(methodDeclaration);
        }

        if (syntaxNode is PropertyDeclarationSyntax propertyDeclaration)
        {
            PropertyCandidates.Add(propertyDeclaration);
        }
    }
}

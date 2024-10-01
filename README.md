# Expressive

Expressive is a cross-platform expression parsing and evaluation framework. The cross-platform nature is achieved through compiling for `.NET Standard` so it will run on practically any platform.

Documentation on how to use the framework can be found [here](https://github.com/bijington/expressive/wiki).

## Usage Example

```c
var expression = new Expression("1+2");
var result = expression.Evaluate();
```

## Another usage Example ( Inject your expression object where you want and evaluate formulas supplying them as argument of the "Evaluate" method )
```c
var expression = new Expression();
var result = expression.Evaluate("1+2");
```

For further detail of usage please see the ([Usage wiki page](https://github.com/bijington/expressive/wiki/Usage))

## Playground

A full playground can be found at: https://bijington.github.io/expressive-playground/ (Currently under development so be gentle :))

## Releases

Expressive is available via:

* [GitHub releases](https://github.com/bijington/expressive/releases/latest)
* [NuGet package](https://www.nuget.org/packages/ExpressiveParser/)

